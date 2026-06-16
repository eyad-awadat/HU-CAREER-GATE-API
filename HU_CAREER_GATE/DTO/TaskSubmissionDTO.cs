using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HUCAREERGATE.Data;

namespace HUCAREERGATE.DTO
{
    [AutoMap(typeof(TaskSubmission),ReverseMap =true)]
    public class TaskSubmissionDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public HRTaskDTO? HRTask { get; set; }
        public int? StudentId { get; set; }
        public StudentDTO? Student { get; set; }
        public int Score { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int TimeTakenInMinutes { get; set; }
        public string CodeSubmission { get; set; }
        public bool IsSubmitted { get; set; }
        public string? Status { get; set; }
        public string? StudentEmail { get; set; }
        public string? FeedBack { get; set; }



    }
}
