using System.ComponentModel.DataAnnotations.Schema;

namespace HUCAREERGATE.Data
{
    [Table("TaskViolation")]
    public class TaskViolation
    {
        public int Id { get; set; }

        [ForeignKey("HRTask")]
        public int TaskId { get; set; }
        public HRTask HRTask { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int Violations { get; set; } = 0;

        public bool IsBlocked { get; set; } = false;

        public string Reasons { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
