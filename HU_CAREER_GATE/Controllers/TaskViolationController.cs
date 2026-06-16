using System.Security.Claims;
using System.Text.Json;
using HUCAREERGATE.Data;
using HUCAREERGATE.DTO;
using HUCAREERGATE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUCAREERGATE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Seeker")]
    public class TaskViolationController : ControllerBase
    {
        public ITaskViolationServices taskViolationServices;
        public TaskViolationController(ITaskViolationServices _taskViolationServices)
        {
            taskViolationServices = _taskViolationServices;
        }

        [HttpPost("start/{taskId}")]
        public IActionResult StartTask(int taskId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            TaskViolation record = taskViolationServices.Start(userId,taskId);
            if (record != null)
            {
                return Ok(new TaskViolationDTO
                {
                    Violations = record.Violations,
                    IsBlocked = record.IsBlocked,
                    Reasons = string.IsNullOrEmpty(record.Reasons)? new List<string>(): JsonSerializer.Deserialize<List<string>>(record.Reasons)
                });
            }
               return Unauthorized();
        }

        [HttpPost("violation/{taskId}")]
        public IActionResult AddViolation(int taskId, [FromBody] string reason)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            TaskViolation record = taskViolationServices.AddViolatuion(userId, taskId, reason);
            if (record == null)
                return NotFound();

            if (record.IsBlocked)
                return BadRequest("Blocked");

            var reasonsList = string.IsNullOrEmpty(record.Reasons)? new List<string>(): JsonSerializer.Deserialize<List<string>>(record.Reasons) ?? new List<string>();

            return Ok(new TaskViolationDTO
            {
                Violations = record.Violations,
                IsBlocked = record.IsBlocked,
                Reasons = reasonsList
            });

        }

        [HttpGet("status/{taskId}")]
        public IActionResult GetStatus(int taskId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var record = taskViolationServices.GetStatus(userId, taskId);

            var reasonsList = string.IsNullOrEmpty(record.Reasons)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(record.Reasons);

            return Ok(new
            {
                isBlocked = record.IsBlocked,
                violations = record.Violations,
                reasons = reasonsList 
            });
        }
    }
}
