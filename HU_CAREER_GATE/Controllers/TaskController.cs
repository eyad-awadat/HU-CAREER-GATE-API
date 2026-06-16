using System.Security.Claims;
using HUCAREERGATE.DTO;
using HUCAREERGATE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace HUCAREERGATE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public ITaskServices taskServices;
        public IAIServices aIServices;
        public TaskController(ITaskServices _taskServices, IAIServices _aIServices)
        {
            taskServices = _taskServices;
            aIServices = _aIServices;
        }
        [Authorize(Roles = "Recruiter")]
        [HttpPost("CreateTask")]
        public IActionResult AddTask([FromForm]HRTaskDTO taskDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {
                if (taskDTO.TaskPdf != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "File", "Cv".ToString());
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + taskDTO.TaskPdf.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    taskDTO.TaskPdf.CopyTo(new FileStream(FilePath, FileMode.Create));

                    taskDTO.TaskPdfName = FileName;
                }

                int id = taskServices.CreateTask(taskDTO, userId);
                var questions = JsonConvert.DeserializeObject<List<QuestionsDto>>(taskDTO.Questions);

                taskServices.saveQuestions(id, questions);

                return Ok(new { taskid = id });
            }
            else
            {
                return BadRequest();
            }
            
        }

        [Authorize(Roles = "Recruiter")]
        [HttpGet("GetTasks")]
        public IActionResult GetTask()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<HRTaskDTO> taskDTOs = taskServices.LodeTask(userId);
            if(taskDTOs != null)
            {
                return Ok(taskDTOs);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("GetTaskInfo/{id}")]
        public IActionResult GetTaskInfo(int id)
        {
            HRTaskDTO taskDTO = taskServices.GetTaskDetails(id);
            if (taskDTO != null)
            {
                return Ok(taskDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPut("DeactivateTask/{taskId}")]
        public IActionResult DeactivateTask(int taskId)
        {
            var Result = taskServices.DeactiveTask(taskId);
            if(Result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpGet("GetDeactiveTask")]
        public IActionResult GetDeactiveTask()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<HRTaskDTO> taskDTOs = taskServices.LodeDeactiveTask(userId);
            if (taskDTOs != null)
            {
                return Ok(taskDTOs);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("SearchJob")]
        public IActionResult SearchJob(string? JobLevel,string? JopType)
        {
            List<HRTaskDTO> taskDTOs = taskServices.SearchJob(JobLevel, JopType);
            if(taskDTOs != null)
            {
                return Ok(taskDTOs);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPost("generateQuestions")]
        public async Task<IActionResult> GenerateQuestions([FromBody] GenerateQuestionsDTO dto)
        {
            var questions = await aIServices.GenerateQuestions(dto.Description);

            return Ok(questions);
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPost("regenerateQuestion")]
        public async Task<IActionResult> RegenerateQuestion([FromBody] GenerateQuestionsDTO dto)
        {
            var question = await aIServices.RegenerateQuestion(dto.Description);
            return Ok(question);
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("GetQuestions")]
        public IActionResult GetQuestionsTask(int taskId)
        {
            List<QuestionsDto> questions = taskServices.getQuestion(taskId);
            if(questions != null)
            {
                return Ok(questions);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetCountJob")]
        public IActionResult GetCountJob()
        {
            int CountJob = taskServices.GetCountJob();
            return Ok(CountJob);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("top-job-types")]
        public IActionResult GetTopJobTypes()
        {
            var data = taskServices.GetTopJobTypes();
            return Ok(data);
        }
    }
}
