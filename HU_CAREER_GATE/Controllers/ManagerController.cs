using HU_CAREER_GATE.DTO;
using HU_CAREER_GATE.Services;
using HUCAREERGATE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HU_CAREER_GATE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServices managerServices;

        public ManagerController(IManagerServices _managerServices)
        {
            managerServices = _managerServices;
        }
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var students = managerServices.GetStudentsForUsers();
            var hrs = managerServices.GetHRsForUsers();

            var users = students
                        .Concat(hrs)
                        .OrderBy(u => u.Name)
                        .ToList();

            return Ok(users);
        }
        [HttpPut("DeactivateStudent/{id}")]
        public IActionResult DeactivateStudent(int id)
        {
             var Result = managerServices.DeactivateStudentUser(id);
            if(Result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            

        }

        [HttpPut("DeactivateHr/{id}")]
        public IActionResult DeactivateHr(int id)
        {
            var Result =  managerServices.DeactivateHrUser(id);
            if (Result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("ActivateStudent/{id}")]
        public IActionResult ActivateStudent(int id)
        {
            var Result = managerServices.ActivateStudentUser(id);
            if (Result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ActivateHr/{id}")]
        public IActionResult ActivateHr(int id)
        {
            var Result = managerServices.ActivateHrUser(id);
            if (Result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetProfile/{id}/{Type}")]
        public IActionResult GetProfile(int id ,string Type)
        {
            if(Type == "Seeker")
            {
                StudentProfileDTO studentProfileDTO = managerServices.GetProfileSeeker(id);
                return Ok(studentProfileDTO);
            }
            else
            {
                HRProfileDTO hRProfileDTO = managerServices.GetProfileHR(id);
                return Ok(hRProfileDTO);
            }
            
        }
        [HttpGet("SearchUser")]
        public IActionResult SearchUser(string? name, string? phone, string? type, bool? isActive)
        {
            List<UserListDTO> users = managerServices.SearchUser(name, phone, type ,isActive);
            return Ok(users);
        }
        [HttpGet("top-hr")]
        public IActionResult GetTopHR()
        {
            var result = managerServices.GetTopHRs();
            return Ok(result);
        }
        [HttpGet("monthly-decisions")]
        public IActionResult GetMonthlyDecisions(string filter = "year")
        {
            var result = managerServices.GetMonthlyDecisions(filter);
            return Ok(result);
        }
        [HttpGet("decision-years")]
        public IActionResult GetDecisionYears()
        {
            var years = managerServices.GetDecisionYears();
            return Ok(years);
        }

    }
}
