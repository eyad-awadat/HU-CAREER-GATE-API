using HUCAREERGATE.DTO;

namespace HUCAREERGATE.Services
{
    public interface ITaskSubmissionServices
    {
        bool CanStartTask(string userId, int taskId);
        TaskSubmissionDTO StartTask(string userId, int taskId);
        TaskSubmissionDTO GetTime(string userId, int taskId);
        void SaveTaskApplyDeteils(string userId, TaskSubmissionDTO taskSubmissionDTO);
        List<TaskSubmissionDTO> GetAllSubmisstion(string userId);
        List<TaskSubmissionDTO> GetStudentDetailsInTask(int TaskId);
        TaskSubmissionDTO GetInfoTask(string userId, int taskId);
        int GetCountApplication(int TaskId);
        TaskSubmissionDTO GetStudentProfileTask(int studentId, int taskId);
        int GetCountTaskSubmit();
        int GetCountAccept();
        int GetCountReject();
    }
}