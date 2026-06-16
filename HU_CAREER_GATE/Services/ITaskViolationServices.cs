using HUCAREERGATE.Data;

namespace HUCAREERGATE.Services
{
    public interface ITaskViolationServices
    {
        TaskViolation Start(string userId, int taskId);
        TaskViolation AddViolatuion(string userId, int taskId, string reason);
        TaskViolation GetStatus(string userId, int taskId);
    }
}