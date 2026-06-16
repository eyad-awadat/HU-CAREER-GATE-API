using AutoMapper;
using HUCAREERGATE.Data;

namespace HUCAREERGATE.DTO
{
    [AutoMap(typeof(TaskQuestion), ReverseMap = true)]
    public class QuestionsDto
    {
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
