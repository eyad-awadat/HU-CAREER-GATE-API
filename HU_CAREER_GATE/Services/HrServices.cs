using AutoMapper;
using HUCAREERGATE.Data;
using HUCAREERGATE.DTO;
using Microsoft.EntityFrameworkCore;

namespace HUCAREERGATE.Services
{
    public class HrServices : IHrServices
    {
        public HUContext context;
        public IMapper mapper;
        public HrServices(HUContext _context,IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public int SaveAccount(HrDTO hrDTO ,string userId)
        {
            Hr hr = mapper.Map<Hr>(hrDTO);
            hr.UserId = userId;
            context.Hrs.Add(hr);
            context.SaveChanges();
            return hr.Id;
        }
        public HrDTO LodeHr(string userId)
        {
            Hr hr = context.Hrs.Include(s => s.User).FirstOrDefault(s => s.UserId == userId);
            HrDTO hrDTO = mapper.Map<HrDTO>(hr);
            hrDTO.Email = hr.User.Email;
            if(hrDTO != null)
            {
                return hrDTO;
            }
            else
            {
                return null;
            }
        }
        public void UpdateHr(HrDTO hrDTO, string userId)
        {
            Hr hr = context.Hrs.FirstOrDefault(s => s.UserId == userId);
            if (hr != null)
            {
                hr.Name = hrDTO.Name;
                hr.CompanyName = hrDTO.CompanyName;
            
                hr.Country = hrDTO.Country;
                hr.City = hrDTO.City;
                hr.ProfileImgName = hrDTO.ProfileImgName;
                hr.CvName = hrDTO.CvName;
                context.SaveChanges();
            }
        }
        public int GetTaskCount(int id)
        {
            int CountTask = context.Hrs.Where( e => e.Id==id ).Select(t => t.HRTasks.Count()).FirstOrDefault();
            return CountTask;
        }
        public int GetCountHr()
        {
            int CountHr = context.Hrs.Count();
            return CountHr;
        }
    }
}
