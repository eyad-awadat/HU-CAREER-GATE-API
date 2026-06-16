using HUCAREERGATE.DTO;

namespace HUCAREERGATE.Services
{
    public interface ITaskServices
    {
        int CreateTask(HRTaskDTO taskDTO, string userId);
        List<HRTaskDTO> LodeTask(string userId);
        List<HRTaskDTO> LodeDeactiveTask(string userId);
        HRTaskDTO GetTaskDetails(int id);
        void saveQuestions(int taskId, List<QuestionsDto> questions);
        List<QuestionsDto> getQuestion(int taskId);
        List<HRTaskDTO> SearchJob(string JopLevel, string JobTaype);
        int GetCountJob();
        List<object> GetTopJobTypes();
        bool DeactiveTask(int taskId);
    }
}