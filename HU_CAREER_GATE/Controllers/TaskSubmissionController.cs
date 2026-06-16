using System.Security.Claims;
using System.Xml.Linq;
using HUCAREERGATE.Data;
using HUCAREERGATE.DTO;
using HUCAREERGATE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HUCAREERGATE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskSubmissionController : ControllerBase
    {
        private readonly ITaskSubmissionServices submissionServices;
        private readonly HUContext context;
        private readonly IEmailSender emailSender;

        public TaskSubmissionController(ITaskSubmissionServices _submissionServices,HUContext _context,IEmailSender _emailSender)
        {
            submissionServices = _submissionServices;
            context = _context;
            emailSender = _emailSender;
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("can-start/{taskId}")]
        public IActionResult CanStartTask(int taskId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            bool result = submissionServices.CanStartTask(userId, taskId);

            return Ok(result);
        }

        [Authorize(Roles = "Seeker")]
        [HttpPost("StartTask/{taskId}")]
        public IActionResult Start(int taskId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = submissionServices.StartTask(userId,taskId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("You already started this task");
            }
            
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("GetTimeEnd/{TaskId}")]
        public IActionResult GetTime(int TaskId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            TaskSubmissionDTO submissionDTO = submissionServices.GetTime(userId,TaskId);

            if(submissionDTO != null)
            {
                return Ok(submissionDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Seeker")]
        [HttpPost("SubmitTask")]
        public IActionResult SubmitTask(TaskSubmissionDTO taskSubmissionDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                submissionServices.SaveTaskApplyDeteils(userId,taskSubmissionDTO);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("GetApplicationTask")]
        public IActionResult GetApplicationTask()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<TaskSubmissionDTO> submissionDTOs = submissionServices.GetAllSubmisstion(userId);
            if(submissionDTOs != null)
            {
                return Ok(submissionDTOs);
            }
            else
            {
                return null;
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpGet("GetStudentProfileTask")]
        public IActionResult GetStudentProfileTask(int studentId,int taskId)
        {
            TaskSubmissionDTO submissionDTOs = submissionServices.GetStudentProfileTask(studentId,taskId);
            if (submissionDTOs != null)
            {
                return Ok(submissionDTOs);
            }
            else
            {
                return null;
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpGet("GetStudentDetailsinTask")]
        public IActionResult GetApplicationStudent(int Taskid)
        {
            List < TaskSubmissionDTO > submissionDTOs = submissionServices.GetStudentDetailsInTask(Taskid);
            if (submissionDTOs != null)
            {
                return Ok(submissionDTOs);
            }
            else
            {
                return null;
            }
        }

        [Authorize(Roles = "Seeker,Recruiter")]
        [HttpGet("GetStudentApplication")]
        public IActionResult GetCountApplicationStudent(int Taskid)
        {
            int count = submissionServices.GetCountApplication(Taskid);
            return Ok(count);
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("GetInfoTask/{taskId}")]
        public IActionResult GetInfoTask(int taskId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = submissionServices.GetInfoTask(userId, taskId);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPost("decision")]
        public async Task<IActionResult> MakeDecision([FromBody] DecisionDto dto)
        {
            try
            {
                var submission = context.taskSubmissions
                    .Include(t => t.Student)
                        .ThenInclude(s => s.User)
                    .Include(t => t.HRTask)
                        .ThenInclude(t => t.Hr)
                            .ThenInclude(h => h.User)
                    .FirstOrDefault(i => i.Id == dto.SubmissionId);

                if (submission == null)
                    return NotFound("Submission not found");
                submission.Status = dto.Decision;
                submission.FeedBack = dto.Comment;
                submission.DecisionDate = DateTime.Now;
                await context.SaveChangesAsync();
                var studentUser = submission.Student?.User;
                var studentName = submission.Student?.Name ?? "Student";
                var hr = submission.HRTask?.Hr;
                var hrUser = hr?.User;
                if (studentUser == null || hr == null || hrUser == null)
                    return Ok("Saved but missing user data");
                var email = studentUser.Email;
                if (string.IsNullOrEmpty(email))
                    return Ok("Saved but no email");
                var hrName = hr.Name ?? "HR";
                var hrEmail = hrUser.Email ?? "";
                var hrPhone = hr.Phone ?? "";
                var companyName = hr.CompanyName ?? "";
                var location = $"{hr.City ?? ""}, {hr.Country ?? ""}";
                var jobTitle = submission.HRTask?.TaskSubject ?? "";
                var body = $@"
<html>
<body style='margin:0;padding:0;background:#f4f6f8;font-family:Segoe UI,Arial,sans-serif;'>

<table width='100%' cellpadding='0' cellspacing='0' style='background:#f4f6f8;padding:30px 0;'>
<tr>
<td align='center'>

<table width='600' cellpadding='0' cellspacing='0' style='background:#ffffff;border-radius:10px;overflow:hidden;box-shadow:0 4px 15px rgba(0,0,0,0.08);'>

<!-- HEADER -->
<tr>
<td style=""
text-align:center;
padding:25px;
border-bottom:1px solid #eee;
font-family:Segoe UI, Arial, sans-serif;
"">

<span style=""
font-size:28px;
font-weight:800;
color:#2f3e73;
letter-spacing:1px;
"">
HU CAREER
</span>

<span style=""
font-size:28px;
font-weight:800;
color:#36b7c9;
letter-spacing:1px;
"">
 GATE
</span>

</td>
</tr>

<!-- TITLE -->
<tr>
<td style='padding:30px 40px 10px 40px;text-align:center;'>

<h2 style='margin:0;color:#111;font-weight:600;'>Application Update</h2>

</td>
</tr>

<!-- MESSAGE -->
<tr>
<td style='padding:10px 40px;font-size:15px;color:#444;'>

<p>Hello <b>{studentName}</b>,</p>

<p>
Your application status for the following position has been updated.
</p>

</td>
</tr>

<!-- STATUS -->
<tr>
<td style='padding:10px 40px;'>

<div style='
padding:15px;
border-radius:8px;
font-size:18px;
font-weight:bold;
text-align:center;
color:white;
background:{(dto.Decision == "Accepted" ? "#16a34a" : "#dc2626")};
'>
{(dto.Decision == "Accepted" ? "Application Accepted 🎉" : "Application Rejected")}
</div>

</td>
</tr>

<!-- JOB INFO -->
<tr>
<td style='padding:25px 40px;'>

<table width='100%' style='border-collapse:collapse;font-size:14px;'>

<tr>
<td style='padding:10px;border-bottom:1px solid #eee;'><b>Position</b></td>
<td style='padding:10px;border-bottom:1px solid #eee;'>{jobTitle}</td>
</tr>

<tr>
<td style='padding:10px;border-bottom:1px solid #eee;'><b>Company</b></td>
<td style='padding:10px;border-bottom:1px solid #eee;'>{companyName}</td>
</tr>

<tr>
<td style='padding:10px;border-bottom:1px solid #eee;'><b>Location</b></td>
<td style='padding:10px;border-bottom:1px solid #eee;'>{location}</td>
</tr>

</table>

</td>
</tr>

<!-- FEEDBACK -->
<tr>
<td style='padding:10px 40px;'>

<div style='background:#f9fafb;padding:15px;border-radius:8px;font-size:14px;color:#333;'>

<b>HR Feedback</b>

<p style='margin-top:8px;'>{dto.Comment}</p>

</div>

</td>
</tr>

<!-- BUTTON -->
<tr>
<td align='center' style='padding:30px;'>

<a href='mailto:{hrEmail}'
style='
background:#2563eb;
color:white;
padding:12px 28px;
border-radius:6px;
text-decoration:none;
font-weight:600;
display:inline-block;
'>
Contact HR
</a>

</td>
</tr>

<!-- FOOTER -->
<tr>
<td style='background:#f9fafb;padding:20px;text-align:center;font-size:12px;color:#777;'>

HU Career Gate Platform <br>
Connecting Students with Opportunities

</td>
</tr>

</table>

</td>
</tr>
</table>

</body>
</html>
";
                try
                {
                    await emailSender.SendEmailAsync(email, "HR Decision", body);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Email Error: " + ex.Message);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetCountTaskSubmit")]
        public IActionResult GetCountTaskSubmit()
        {
            int CountTaskSubmit = submissionServices.GetCountTaskSubmit();
            return Ok(CountTaskSubmit);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("accepted-count")]
        public IActionResult GetCountAccept()
        {
            int CountAccept = submissionServices.GetCountAccept();
            return Ok(CountAccept);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Reject-count")]
        public IActionResult GetCountReject()
        {
            int CountReject = submissionServices.GetCountReject();
            return Ok(CountReject);
        }

    }
}
