using HUCAREERGATE.DTO;

namespace HUCAREERGATE.Services
{
    public interface IAIServices
    {
        Task<List<QuestionDTO>> GenerateQuestions(string taskDescription);
        Task<QuestionDTO> RegenerateQuestion(string taskDescription);
    }
}
