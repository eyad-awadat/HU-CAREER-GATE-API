using AutoMapper;
using HUCAREERGATE.Data;
using HUCAREERGATE.DTO;
using Microsoft.EntityFrameworkCore;

namespace HUCAREERGATE.Services
{
    public class TaskSubmissionServices : ITaskSubmissionServices
    {
        public HUContext context;
        public IMapper mapper;
        public TaskSubmissionServices(HUContext _context,IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public bool CanStartTask(string userId, int taskId)
        {
            Student student = context.Students
                .FirstOrDefault(s => s.UserId == userId);

            if (student == null)
                return false;

            var submission = context.taskSubmissions
                .FirstOrDefault(x => x.StudentId == student.Id && x.TaskId == taskId);

            if (submission == null)
                return true;

            if (submission.IsSubmitted)
                return false;

            return true;
        }
        public TaskSubmissionDTO StartTask(string userId, int taskId)
        {
            Student student = context.Students
                .FirstOrDefault(s => s.UserId == userId);

            if (student == null)
                return null;

            TaskSubmission existing = context.taskSubmissions
                .FirstOrDefault(x => x.StudentId == student.Id && x.TaskId == taskId);

            if (existing != null)
                return null;

            HRTask task = context.HRTasks.FirstOrDefault(x => x.Id == taskId);

            int durationMinutes = (task.TimeInHours * 60) + task.TimeInMinutes;

            TaskSubmission submission = new TaskSubmission
            {
                StudentId = student.Id,
                TaskId = taskId,
                StartTime = DateTime.Now,
                TimeEnd = DateTime.Now.AddMinutes(durationMinutes)
            };

            context.taskSubmissions.Add(submission);
            context.SaveChanges();

            return new TaskSubmissionDTO
            {
                TaskId = submission.TaskId,
                StudentId = submission.StudentId,
                StartTime = submission.StartTime,
                TimeEnd = submission.TimeEnd
            };
        }

        public TaskSubmissionDTO GetTime(string userId, int taskId)
        {
            Student student = context.Students.FirstOrDefault(s => s.UserId == userId);

            if (student == null)
                return null;

            var submission = context.taskSubmissions.FirstOrDefault(x => x.StudentId == student.Id && x.TaskId == taskId);
            TaskSubmissionDTO submissionDTO = new TaskSubmissionDTO()
            {
                Id = submission.Id,
                TaskId = submission.TaskId,
                StudentId = submission.StudentId,
                Score = submission.Score,
                StartTime = submission.StartTime,
                TimeEnd = submission.TimeEnd,
                TimeTakenInMinutes = submission.TimeTakenInMinutes,

            };

            if(submissionDTO != null)
            {

                return submissionDTO;
            }
            else
            {
                return null;
            }
        }
        public void SaveTaskApplyDeteils(string userId , TaskSubmissionDTO taskSubmissionDTO)
        {

            Student student = context.Students.FirstOrDefault(s => s.UserId == userId);

            TaskSubmission submission = context.taskSubmissions.FirstOrDefault(x => x.StudentId == student.Id && x.TaskId == taskSubmissionDTO.TaskId);
            if(submission != null)
            {
                submission.Score = taskSubmissionDTO.Score;
                submission.CodeSubmission = taskSubmissionDTO.CodeSubmission;
                submission.TimeTakenInMinutes = taskSubmissionDTO.TimeTakenInMinutes;
                submission.IsSubmitted = taskSubmissionDTO.IsSubmitted;
                context.SaveChanges();
            }
        }
        public List<TaskSubmissionDTO> GetAllSubmisstion(string userId)
        {
            Student student = context.Students.FirstOrDefault( i => i.UserId == userId);
            List<TaskSubmission> submission = context.taskSubmissions.Where(i => i.StudentId == student.Id && i.IsSubmitted == true ).Include(t => t.HRTask).ThenInclude(t => t.Hr).OrderByDescending(x => x.Score).ToList();
            List<TaskSubmissionDTO> taskSubmissions = mapper.Map<List<TaskSubmissionDTO>>(submission);
            if(taskSubmissions != null)
            {
                return taskSubmissions;
            }
            else
            {
                return null;
            }
            
        }
        public TaskSubmissionDTO GetStudentProfileTask(int studentId ,int taskId)
        {
            TaskSubmission submission = context.taskSubmissions.Where(i => i.StudentId == studentId && i.IsSubmitted == true && i.TaskId == taskId).Include(t => t.Student).ThenInclude(s => s.User).FirstOrDefault();
            TaskSubmissionDTO taskSubmissions = mapper.Map<TaskSubmissionDTO>(submission);
            taskSubmissions.StudentEmail = submission.Student.User.Email;
            if (taskSubmissions != null)
            {
                return taskSubmissions;
            }
            else
            {
                return null;
            }

        }
        public List<TaskSubmissionDTO> GetStudentDetailsInTask(int TaskId)
        {
            List<TaskSubmission> taskSubmissions = context.taskSubmissions.Where(i => i.TaskId == TaskId && i.IsSubmitted == true).Include(t => t.Student).OrderByDescending(x => x.Score).ToList();
            List<TaskSubmissionDTO> submissionDTOs = mapper.Map<List<TaskSubmissionDTO>>(taskSubmissions);
            if(submissionDTOs != null)
            {
                return submissionDTOs;
            }
            else
            {
                return null;
            }
        }
        public TaskSubmissionDTO GetInfoTask(string userId, int taskId)
        {
            var student = context.Students.FirstOrDefault(s => s.UserId == userId);
            if (student is null)
            {
                return null;
                 
            }
            var TaskInfo = context.taskSubmissions.Where(i => i.StudentId == student.Id && i.TaskId == taskId).Include(t => t.HRTask).FirstOrDefault();
            TaskSubmissionDTO taskSubmissions = mapper.Map<TaskSubmissionDTO>(TaskInfo);
            if (taskSubmissions != null)
            {
                return taskSubmissions;
            }
            else
            {
                return null;
            }
        }
        public int GetCountApplication(int TaskId)
        {
            int taskSubmissions = context.taskSubmissions.Where(i => i.TaskId == TaskId && i.IsSubmitted == true).Count();
            return taskSubmissions;
        }
        public int GetCountTaskSubmit()
        {
            int CountTaskSubmit = context.taskSubmissions.Where(t => t.IsSubmitted == true).Count();
            return CountTaskSubmit;
        }
        public int GetCountAccept()
        {
            int CountAccept = context.taskSubmissions.Where(a => a.Status == "Accepted").Count();
            return CountAccept;
        }
        public int GetCountReject()
        {
            int CountReject = context.taskSubmissions.Where(a => a.Status == "Rejected").Count();
            return CountReject;
        }

    }
}
