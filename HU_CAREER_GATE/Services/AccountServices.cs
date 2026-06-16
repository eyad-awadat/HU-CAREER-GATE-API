using HUCAREERGATE.Data;
using HUCAREERGATE.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HUCAREERGATE.Services
{
    public class AccountServices:IAccountServices
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly HUContext context;

        public AccountServices(UserManager<IdentityUser> _userManager,
                                SignInManager<IdentityUser> _signInManager,
                                 RoleManager<IdentityRole> _roleManager,
                                  HUContext context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            this.context = context;
        }
        public async Task<IdentityResult> CreateAccount(SignUpDTO signUpDTO)
        {
            IdentityUser user = new IdentityUser()
            {
                Email = signUpDTO.Email,
                UserName = signUpDTO.Email
            };
            var result = await userManager.CreateAsync(user, signUpDTO.Password);
            if (result.Succeeded)
            {
                switch (signUpDTO.RoleName?.ToLower())
                {
                    case "recruiter":
                        await userManager.AddToRoleAsync(user, "Recruiter");
                        break;

                    default:
                        await userManager.AddToRoleAsync(user, "Seeker");
                        break;
                }
            }
            if (!result.Succeeded)
            {
                await userManager.DeleteAsync(user);
            }
            return result;
        }
        public async Task<SignInResult> Authanticate(SignInDTO signInDTO)
        {
            var result = await signInManager.PasswordSignInAsync(signInDTO.Email, signInDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(signInDTO.Email);
                var roles = await userManager.GetRolesAsync(user);

                if (roles.Contains("Seeker"))
                {
                    var student = context.Students.FirstOrDefault(s => s.UserId == user.Id);
                    if (student != null && !student.IsActive)
                    {
                        await signInManager.SignOutAsync();
                        return SignInResult.NotAllowed;
                    }
                }
                else if (roles.Contains("Recruiter"))
                {
                    var hr = context.Hrs.FirstOrDefault(h => h.UserId == user.Id);
                    if (hr != null && !hr.IsActive)
                    {
                        await signInManager.SignOutAsync();
                        return SignInResult.NotAllowed;
                    }
                }
            }

            return result;
        }
        public async Task<IList<string>> GetUserRole(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return await userManager.GetRolesAsync(user);
        }
        public async Task<IdentityUser> GetUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

    }

}
