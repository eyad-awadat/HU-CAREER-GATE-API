using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HUCAREERGATE.Data;

namespace HUCAREERGATE.DTO
{
    [AutoMap(typeof(Student), ReverseMap = true)]
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Student Name is required ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "State of Student is required ")]
        public string State { get; set; }
        [Required(ErrorMessage = "GPA of Student is required ")]
        public double GPA { get; set; }
        [Required(ErrorMessage = "Job type of Student is required ")]
        public string JobType { get; set; }
        [Required(ErrorMessage = "Job Level of Student Name is required ")]
        public string JobLevel { get; set; }
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
