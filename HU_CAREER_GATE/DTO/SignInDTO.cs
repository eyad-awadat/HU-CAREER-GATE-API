using System.ComponentModel.DataAnnotations;

namespace HUCAREERGATE.DTO
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Email is requierd")]
        [EmailAddress(ErrorMessage = "Please fill valied Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is requierd")]
        public string Password { get; set; }
    }
}
