using System.ComponentModel.DataAnnotations.Schema;

namespace HUCAREERGATE.Data
{
    [Table("TaskSubmission")]
    public class TaskSubmission
    {
        public int Id { get; set; }

        [ForeignKey("HRTask")]
        public int TaskId { get; set; }
        public HRTask HRTask { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int Score { get; set; } = 0;
        public DateTime? StartTime { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int TimeTakenInMinutes { get; set; } = 0;
        public string? CodeSubmission { get; set; }
        public bool IsSubmitted { get; set; } = false;
        public string Status { get; set; } = "Pending";
        public DateTime? DecisionDate { get; set; }
        public string? FeedBack { get; set; }

    }
}
