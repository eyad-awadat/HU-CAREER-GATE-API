using AutoMapper;
using HUCAREERGATE.Data;
using HUCAREERGATE.DTO;
using Microsoft.EntityFrameworkCore;

namespace HUCAREERGATE.Services
{
    public class TaskServices : ITaskServices
    {
        public HUContext context;
        public IMapper mapper;
        public TaskServices(HUContext _context,IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public int CreateTask(HRTaskDTO taskDTO, string userId)
        {
            var hr = context.Hrs.FirstOrDefault(h => h.UserId == userId);

            if (hr == null)
                throw new Exception("HR not found");

            HRTask task = mapper.Map<HRTask>(taskDTO);
            task.UserId = userId;
            task.HrId = hr.Id;
            context.HRTasks.Add(task);
            context.SaveChanges();
            return task.Id;
        }

        public List<HRTaskDTO> LodeTask(string userId)
        {
            var hr = context.Hrs.FirstOrDefault(h => h.UserId == userId);

            if (hr == null)
                return new List<HRTaskDTO>();

            List<HRTask> hrTasks = context.HRTasks.Where(t => t.HrId == hr.Id && t.IsActive == true).ToList();

            List<HRTaskDTO> hRTaskDTOs = mapper.Map<List<HRTaskDTO>>(hrTasks);
            if(hRTaskDTOs != null)
            {
                return hRTaskDTOs;
            }
            else
            {
                return null;
            }
            
        }
        public List<HRTaskDTO> LodeDeactiveTask(string userId)
        {
            var hr = context.Hrs.FirstOrDefault(h => h.UserId == userId);

            if (hr == null)
                return new List<HRTaskDTO>();

            List<HRTask> hrTasks = context.HRTasks.Where(t => t.HrId == hr.Id && t.IsActive == false).ToList();

            List<HRTaskDTO> hRTaskDTOs = mapper.Map<List<HRTaskDTO>>(hrTasks);
            if (hRTaskDTOs != null)
            {
                return hRTaskDTOs;
            }
            else
            {
                return null;
            }

        }
        public bool DeactiveTask(int taskId)
        {
            HRTask task = context.HRTasks.Find(taskId);
            if(task != null)
            {
                task.IsActive = false;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public HRTaskDTO GetTaskDetails(int id)
        {
            HRTask hRTask = context.HRTasks.Find(id);
            HRTaskDTO taskDTO = mapper.Map<HRTaskDTO>(hRTask);

            if (taskDTO != null)
            {
                return taskDTO;
            }
            else
            {
                return null;
            }
        }
        public void saveQuestions(int taskId, List<QuestionsDto> questions)
        {
            var taskQuestions = mapper.Map<List<TaskQuestion>>(questions);

            foreach (var q in taskQuestions)
            {
                q.TaskId = taskId;
            }

            context.taskQuestions.AddRange(taskQuestions);
            context.SaveChanges();
        }
        public List<QuestionsDto> getQuestion(int taskId)
        {
            List <TaskQuestion> taskQuestions = context.taskQuestions.Where(i => i.TaskId == taskId).ToList();
            List<QuestionsDto> questions = mapper.Map<List<QuestionsDto>>(taskQuestions);
            if(questions != null)
            {
                return questions;
            }
            else
            {
                return null;
            }
            
        }
        public List<HRTaskDTO> SearchJob(string JopLevel , string JobTaype)
        {
            IQueryable<HRTask> query = context.HRTasks.Where(t => t.IsActive == true);
            if (!string.IsNullOrEmpty(JobTaype) && !string.IsNullOrEmpty(JopLevel) && JobTaype != null && JopLevel != null)
            {
                query = query.Where(q => q.JobType == JobTaype && q.JobLevel == JopLevel);
            }
            List<HRTask> tasks = query.Include(i => i.Hr).ToList();
            List<HRTaskDTO> hRTaskDTOs = mapper.Map<List<HRTaskDTO>>(tasks);
            return hRTaskDTOs;
        }
        public int GetCountJob()
        {
            int CountJob = context.HRTasks.Count();
            return CountJob;
        }
        public List<object> GetTopJobTypes()
        {
            var Total = context.HRTasks.Count();
            var data = context.HRTasks.GroupBy(t => t.JobType).Select(g => new { JobType = g.Key, Percentage = (g.Count() * 100.0) / Total }).OrderByDescending(p => p.Percentage).Take(5).ToList();
            return data.Cast<object>().ToList();
        }
    }
}
