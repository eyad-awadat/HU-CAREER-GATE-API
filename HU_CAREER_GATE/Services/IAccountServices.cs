using HUCAREERGATE.DTO;
using Microsoft.AspNetCore.Identity;

namespace HUCAREERGATE.Services
{
    public interface IAccountServices
    {
        Task<IdentityResult> CreateAccount(SignUpDTO signUpDTO);
        Task<IList<string>> GetUserRole(string userName);
        Task<SignInResult> Authanticate(SignInDTO signInDTO);
        Task<IdentityUser> GetUserByEmail(string email);
    }
}
