using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HUCAREERGATE.Data;

namespace HUCAREERGATE.DTO
{
    [AutoMap(typeof(HRTask), ReverseMap = true)]
    public class HRTaskDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Task Subject is required ")]
        public string TaskSubject { get; set; }

        [Required(ErrorMessage = "Task Description is required ")]
        public string TaskDescription { get; set; }

        [Required(ErrorMessage = "Job Level is required ")]
        public string JobLevel { get; set; }

        [Required(ErrorMessage = "Job Type is required ")]
        public string JobType { get; set; }

        [Required(ErrorMessage = "Time In Hours is required ")]
        [Range(0, 3)]
        public int TimeInHours { get; set; }

        [Required(ErrorMessage = "Time In Minutes is required ")]
        [Range(0, 59)]
        public int TimeInMinutes { get; set; }

        public IFormFile? TaskPdf { get; set; }
        public string? TaskPdfName { get; set; }
        public int? HrId { get; set; }
        public HrDTO? HR { get; set; }
        public string Questions { get; set; }


    }
}
