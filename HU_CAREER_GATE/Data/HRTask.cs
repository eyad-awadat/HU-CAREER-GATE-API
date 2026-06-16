using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HUCAREERGATE.Data
{
    [Table("HRTask")]
    public class HRTask
    {
        public int Id { get; set; }
        public string TaskSubject { get; set; }
        public string TaskDescription { get; set; }
        public string JobLevel { get; set; }
        public string JobType { get; set; }

        [Range(0, 3)]
        public int TimeInHours { get; set; }

        [Range(0, 59)]
        public int TimeInMinutes { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime TimeEnd { get; set; }
        public string? TaskPdfName { get; set; }

        [ForeignKey("Hr")]
        public int HrId { get; set; }
        public Hr Hr { get; set; }

        public List<TaskSubmission> TaskSubmissions { get; set; }
        public List<TaskQuestion> TaskQuestions { get; set; }
        public List<TaskViolation> taskViolations { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
