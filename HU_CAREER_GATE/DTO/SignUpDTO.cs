using System.ComponentModel.DataAnnotations;

namespace HUCAREERGATE.DTO
{
    public class SignUpDTO
    {

        [Required(ErrorMessage = "Email is requierd")]
        [EmailAddress(ErrorMessage = "Please fill valied Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is requierd")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role Name is requierd")]
        public string RoleName { get; set; }
    }
}
