using System.Security.Claims;
using System.Text.Json;
using HUCAREERGATE.Data;
using Microsoft.EntityFrameworkCore;

namespace HUCAREERGATE.Services
{
    public class TaskViolationServices : ITaskViolationServices
    {
        public HUContext context;
        public TaskViolationServices(HUContext _context)
        {
            context = _context;
        }
        public TaskViolation Start(string userId,int taskId)
        {
            Student student = context.Students.FirstOrDefault(i => i.UserId == userId);

            if (student == null)
                return null;

            TaskViolation record = context.TaskViolation.FirstOrDefault(i => i.StudentId == student.Id && i.TaskId == taskId);
            if (record != null)
                return record;

            if (record == null)
            {
                record = new TaskViolation()
                {
                    TaskId = taskId,
                    StudentId = student.Id,
                    Violations = 0,
                    IsBlocked = false,
                    Reasons = "[]"
                };
                    context.TaskViolation.Add(record);
                    context.SaveChanges();
            }
            return record;
        }
        public TaskViolation AddViolatuion(string userId, int taskId , string reason)
        {
            Student student = context.Students.FirstOrDefault(i => i.UserId == userId);

            if (student == null)
                return null;

            TaskViolation record = context.TaskViolation.FirstOrDefault(i => i.StudentId == student.Id && i.TaskId == taskId);

            if (record == null)
                return null;

            if (record.IsBlocked)
                return record;

            if (reason == "Exited fullscreen mode")
            {
                record.Violations = 3;
                record.IsBlocked = true;
            }
            else
            {
                record.Violations++;
            }

            var reasonsList = string.IsNullOrEmpty(record.Reasons)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(record.Reasons);

            reasonsList.Add(reason);

            record.Reasons = JsonSerializer.Serialize(reasonsList);

            if (record.Violations >= 3)
            {
                record.IsBlocked = true;
            }

            record.LastUpdated = DateTime.Now;

            context.SaveChanges();

            return record;
        }
        public TaskViolation GetStatus(string userId, int taskId)
        {
            Student student = context.Students.FirstOrDefault(i => i.UserId == userId);

            if (student == null)
                return null;

            var record = context.TaskViolation.FirstOrDefault(i => i.StudentId == student.Id && i.TaskId == taskId);

            if (record == null)
            {
              
                return new TaskViolation
                {
                    StudentId = student.Id,
                    TaskId = taskId,
                    Violations = 0,
                    IsBlocked = false,
                    Reasons = "[]"
                };
            }

            return record;
        }
    }
}
