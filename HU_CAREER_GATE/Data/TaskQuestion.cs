using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HUCAREERGATE.Data
{
    [Table("TaskQuestions")]
    public class TaskQuestion
    {
        public int Id { get; set; }
        
        public string Question { get; set; }

        [StringLength(255)]
        public string OptionA { get; set; }

        [StringLength(255)]
        public string OptionB { get; set; }

        [StringLength(255)]
        public string OptionC { get; set; }

        [StringLength(255)]
        public string OptionD { get; set; }

        [StringLength(10)]
        public string CorrectAnswer { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public HRTask Task { get; set; }
    }
}
