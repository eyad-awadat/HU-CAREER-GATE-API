using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HUCAREERGATE.DTO;
using HUCAREERGATE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HUCAREERGATE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountServices accountServices;
        public IConfiguration configuration;
        public IStudentService studentService;
        public AccountController(IAccountServices _accountServices,
                                    IConfiguration _configuration,
                                       IStudentService _studentService)
        {
            accountServices = _accountServices;
            configuration = _configuration;
            studentService = _studentService;
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(SignUpDTO signUpDTO)
        {
            var result = await accountServices.CreateAccount(signUpDTO);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new{ errors = result.Errors.Select(e => e.Description)});
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(SignInDTO signInDTO)
        {
            var result = await accountServices.Authanticate(signInDTO);
            if (result.Succeeded)
            {
                var user = await accountServices.GetUserByEmail(signInDTO.Email);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,signInDTO.Email),
                    new Claim("UniqueValue",Guid.NewGuid().ToString())
                };
                var roles = await accountServices.GetUserRole(signInDTO.Email);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var AuthSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:securitykey"]));

                var token = new JwtSecurityToken
                    (

                        issuer: configuration["JWT:validIssuer"],
                        audience: configuration["JWT:validAudience"],
                        expires: DateTime.Now.AddDays(5),
                        claims: claims,
                        signingCredentials: new SigningCredentials(AuthSecurityKey, SecurityAlgorithms.HmacSha256)

                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            else if (result.IsNotAllowed)
            {
                return Unauthorized("Your account has been deactivated. Please contact support.");
            }
            else
            {
                return Unauthorized("Invalid email or password.");
            }
        }

    }
    
}
