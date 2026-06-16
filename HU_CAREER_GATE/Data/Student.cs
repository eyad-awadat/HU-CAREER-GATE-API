using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HUCAREERGATE.Data
{
    [Table("Students")]
    public class Student
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [Range(0, 4)]
        public double GPA { get; set; }
        public string JobType { get; set; }
        public string JobLevel { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string? ProfileImgName { get; set; }

        [StringLength(255)]
        public string? CvName { get; set; }

        public bool IsActive { get; set; } = true;
        public List<TaskSubmission> TaskSubmissions { get; set; }

        public List<TaskViolation> taskViolations { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
