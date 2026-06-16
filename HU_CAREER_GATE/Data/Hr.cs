using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HUCAREERGATE.Data
{
    [Table("HRs")]
    public class Hr
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(40)]
        
        public string Country { get; set; }
        public string City { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string? ProfileImgName { get; set; }

        [StringLength(255)]
        public string? CvName { get; set; }
        public List<HRTask> HRTasks { get; set; }

        public bool IsActive { get; set; } = true;
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

    }
}
