using System.Security.Claims;
using HUCAREERGATE.DTO;
using HUCAREERGATE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUCAREERGATE.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public IStudentService studentService;
        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }

        [Authorize(Roles = "Seeker")]
        [HttpPost("CreateAccount")]
        public IActionResult AddStudent([FromForm] StudentDTO studentDTO)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            
            if (ModelState.IsValid)
            {
                if (studentDTO.ProfileImg != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images".ToString());
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + studentDTO.ProfileImg.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    studentDTO.ProfileImg.CopyTo(new FileStream(FilePath, FileMode.Create));

                    studentDTO.ProfileImgName = FileName;
                }
                if (studentDTO.Cv != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "File", "Cv".ToString());
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + studentDTO.Cv.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    studentDTO.Cv.CopyTo(new FileStream(FilePath, FileMode.Create));

                    studentDTO.CvName = FileName;
                }
                
                int id = studentService.AddStudent(studentDTO,userId);
                return Ok(new { StudentId = id });
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("GetStudent")]
        public IActionResult GetAllStudents()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var student = studentService.GetStudentById(userId);

            if (student == null)
                return NotFound("Student not found");

            return Ok(student);
        }

        [Authorize(Roles = "Seeker")]
        [HttpPut("UpdateStudent")]
        public IActionResult Update([FromForm] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                if (studentDTO.ProfileImg != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images".ToString());
                    if (!string.IsNullOrEmpty(studentDTO.ProfileImgName))
                    {
                        string OldPath = Path.Combine(UplodeFolder, studentDTO.ProfileImgName);
                        if (System.IO.File.Exists(OldPath))
                        {
                            System.IO.File.Delete(OldPath);
                        }
                    }
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + studentDTO.ProfileImg.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    studentDTO.ProfileImg.CopyTo(new FileStream(FilePath, FileMode.Create));

                    studentDTO.ProfileImgName = FileName;
                }
                if (studentDTO.Cv != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "File", "Cv".ToString());
                    if (!string.IsNullOrEmpty(studentDTO.CvName))
                    {
                        string OldPath = Path.Combine(UplodeFolder, studentDTO.CvName);
                        if (System.IO.File.Exists(OldPath))
                        {
                            System.IO.File.Delete(OldPath);
                        }
                    }
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + studentDTO.Cv.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    studentDTO.Cv.CopyTo(new FileStream(FilePath, FileMode.Create));

                    studentDTO.CvName = FileName;
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                    return Unauthorized();
                studentService.UpdateStudent(studentDTO,userId);

                return Ok();
            }
            else
            {
                return BadRequest(); 
            }
        }

        [Authorize(Roles = "Seeker")]
        [HttpGet("GetCountTaskSubmited")]
        public IActionResult GetCountTask()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int count = studentService.GetCountApplication(userId);
            return Ok(count);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetCountStudent")]
        public IActionResult GetCountStudent()
        {
            int CountStudnt = studentService.GetCountStudent();
            return Ok(CountStudnt);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Get-Students")]
        public IActionResult GetStudentsM()
        {
            List<StudentDTO> students = studentService.GetStudentM();
            return Ok(students);
        }
    }
}
