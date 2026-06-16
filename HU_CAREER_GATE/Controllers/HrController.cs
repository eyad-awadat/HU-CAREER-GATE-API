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
    public class HrController : ControllerBase
    {
        public IHrServices hrServices;
        public HrController(IHrServices _hrServices)
        {
            hrServices = _hrServices;
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPost("CreateAccountHr")]
        public IActionResult CreateAccount([FromForm]HrDTO hrDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {
                if(hrDTO.ProfileImg != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images".ToString());
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() +"_"+ hrDTO.ProfileImg.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    hrDTO.ProfileImg.CopyTo(new FileStream(FilePath, FileMode.Create));

                    hrDTO.ProfileImgName = FileName;
                }
                if(hrDTO.Cv != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "File", "Cv".ToString());
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + hrDTO.Cv.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    hrDTO.Cv.CopyTo(new FileStream(FilePath, FileMode.Create));

                    hrDTO.CvName = FileName;
                }

                int id = hrServices.SaveAccount(hrDTO,userId);
                return Ok(new { hrId = id });
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpGet("GetHr")]
        public IActionResult GetHrs()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            HrDTO hrDTO = hrServices.LodeHr(userId);
            if(hrDTO != null)
            {
                return Ok(hrDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPut("UpdateHR")]
        public IActionResult UpdateInfoHr([FromForm]HrDTO hrDTO)
        {
            if (ModelState.IsValid)
            {
                if (hrDTO.ProfileImg != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images".ToString());
                    if (!string.IsNullOrEmpty(hrDTO.ProfileImgName))
                    {
                        string OldPath = Path.Combine(UplodeFolder, hrDTO.ProfileImgName);
                        if (System.IO.File.Exists(OldPath))
                        {
                            System.IO.File.Delete(OldPath);
                        }
                    }
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + hrDTO.ProfileImg.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    hrDTO.ProfileImg.CopyTo(new FileStream(FilePath, FileMode.Create));

                    hrDTO.ProfileImgName = FileName;
                }
                if (hrDTO.Cv != null)
                {
                    string UplodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "File", "Cv".ToString());
                    if (!string.IsNullOrEmpty(hrDTO.CvName))
                    {
                        string OldPath = Path.Combine(UplodeFolder, hrDTO.CvName);
                        if (System.IO.File.Exists(OldPath))
                        {
                            System.IO.File.Delete(OldPath);
                        }
                    }
                    if (!Directory.Exists(UplodeFolder))
                    {
                        Directory.CreateDirectory(UplodeFolder);
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + hrDTO.Cv.FileName;
                    string FilePath = Path.Combine(UplodeFolder, FileName);
                    hrDTO.Cv.CopyTo(new FileStream(FilePath, FileMode.Create));

                    hrDTO.CvName = FileName;
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                    return Unauthorized();
                hrServices.UpdateHr(hrDTO,userId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
           
        }

        [Authorize(Roles = "Recruiter")]
        [HttpGet("GetCountTask")]
        public IActionResult GetNumberTask(int id)
        {
            int countTask = hrServices.GetTaskCount(id);
            if (countTask != null)
            {
                return Ok(countTask);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetCountHR")]
        public IActionResult GetCountHR()
        {
            int CountHr = hrServices.GetCountHr();
            return Ok(CountHr);
        }
    }
}
