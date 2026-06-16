using System.ComponentModel.DataAnnotations;

namespace HUCAREERGATE.DTO
{
    public class RoleDTO
    {
        public string? id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
    }
}
