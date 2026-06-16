using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HUCAREERGATE.Data;

namespace HUCAREERGATE.DTO
{
    [AutoMap(typeof(Hr), ReverseMap = true)]
    public class HrDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Student Name is required ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Combany Name of Hr is required ")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Is Free Lanser is required ")]
        public bool IsFreeLanser { get; set; }

        public string? Status { get; set; }

        [Required(ErrorMessage = "Country of Student Name is required ")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City of Student Name is required ")]
        public string City { get; set; }

        [RegularExpression(@"07(7|8|9)\d{7}", ErrorMessage = "Invaled Input Phone Number")]
        public string Phone { get; set; }
        public IFormFile? ProfileImg { get; set; }
        public string? ProfileImgName { get; set; }
        public IFormFile? Cv { get; set; }
        public string? CvName { get; set; }
        public string? Email { get; set; }

    }
}
