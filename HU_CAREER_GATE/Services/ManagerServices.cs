using AutoMapper;
using HU_CAREER_GATE.DTO;
using HUCAREERGATE.Data;
using HUCAREERGATE.DTO;
using Microsoft.EntityFrameworkCore;

namespace HU_CAREER_GATE.Services
{
    public class ManagerServices : IManagerServices
    {
        public IMapper mapper;
        public HUContext context;
        public ManagerServices(IMapper _mapper,
                                HUContext _context)
        {
            mapper = _mapper;
            context = _context;
        }
        public List<UserListDTO> GetStudentsForUsers()
        {
            return context.Students
            .Include(s => s.User)
            .Select(s => new UserListDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.User.Email,
                Phone = s.Phone,
                IsActive = s.IsActive,
                ProfileImgName = s.ProfileImgName,
                Type = "Seeker"
            })
            .ToList();
        }
        public List<UserListDTO> GetHRsForUsers()
        {
            return context.Hrs
            .Include(h => h.User)
            .Select(h => new UserListDTO
            {
                Id = h.Id,
                Name = h.Name,
                Email = h.User.Email,
                Phone = h.Phone,
                IsActive = h.IsActive,
                ProfileImgName = h.ProfileImgName,
                Type = "Recruiter"
            })
            .ToList();
        }
        public StudentProfileDTO GetProfileSeeker(int id)
        {
            var data = context.Students
            .Where(s => s.Id == id)
            .Select(s => new StudentProfileDTO
            {
                Id = s.Id,
                Name = s.Name,
                JobType = s.JobType,
                JobLevel = s.JobLevel,
                State = s.State,
                GPA = s.GPA,
                Country = s.Country,
                City = s.City,
                Phone = s.Phone,
                IsActive = s.IsActive,
                ProfileImgName = s.ProfileImgName,
                CvName = s.CvName,

                Email = s.User.Email,

                TotalApplications = s.TaskSubmissions.Count(),

                LastTaskName = s.TaskSubmissions
                    .OrderByDescending(t => t.Id)
                    .Select(t => t.HRTask.TaskSubject)
                    .FirstOrDefault(),

                Status = s.TaskSubmissions
                    .OrderByDescending(t => t.Id)
                    .Select(t => t.Status)
                    .FirstOrDefault()
            })
            .FirstOrDefault();

            return data;
        }
        public HRProfileDTO GetProfileHR(int id)
        {
            var data = context.Hrs
            .Where(h => h.Id == id)
            .Select(h => new HRProfileDTO
            {
                Id = h.Id,
                Name = h.Name,
                CompanyName = h.CompanyName,
                Country = h.Country,
                City = h.City,
                Phone = h.Phone,
                IsActive = h.IsActive,
                ProfileImgName = h.ProfileImgName,
                CvName = h.CvName,
                Email = h.User.Email,

                TotalTasks = h.HRTasks.Count(),

                LastJobPosted = h.HRTasks
                    .OrderByDescending(t => t.Id)
                    .Select(t => t.TaskSubject)
                    .FirstOrDefault(),

                AcceptedStudents = h.HRTasks
                    .SelectMany(t => t.TaskSubmissions)
                    .Count(s => s.Status == "Accepted")
            })
            .FirstOrDefault();

            return data;
        }
        public bool DeactivateStudentUser(int id)
        {
            var resulet = context.Students.Find(id);
            if(resulet.IsActive != false)
            {
                resulet.IsActive = false;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public bool DeactivateHrUser(int id)
        {
            var resulet = context.Hrs.Find(id);
            if (resulet.IsActive != false)
            {
                resulet.IsActive = false;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool ActivateStudentUser(int id)
        {
            var student = context.Students.Find(id);
            if (student == null) return false;

            student.IsActive = true;
            context.SaveChanges();
            return true;
        }

        public bool ActivateHrUser(int id)
        {
            var hr = context.Hrs.Find(id);
            if (hr == null) return false;

            hr.IsActive = true;
            context.SaveChanges();
            return true;
        }
        public List<UserListDTO> SearchUser(string? name, string? phone, string? type, bool? isActive)
        {
            IQueryable<Student> seekerQuery = context.Students.Include(s => s.User);
            IQueryable<Hr> hrQuery = context.Hrs.Include(h => h.User);
            if (!string.IsNullOrEmpty(name))
            {
                seekerQuery = seekerQuery.Where(s => s.Name.Contains(name));
                hrQuery = hrQuery.Where(h => h.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                seekerQuery = seekerQuery.Where(s => s.Phone.Contains(phone));
                hrQuery = hrQuery.Where(h => h.Phone.Contains(phone));
            }
            if (isActive.HasValue)
            {
                seekerQuery = seekerQuery.Where(s => s.IsActive == isActive.Value);
                hrQuery = hrQuery.Where(h => h.IsActive == isActive.Value);
            }
            if (type == "Seeker")
            {
                return seekerQuery.Select(s => new UserListDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.User.Email,
                    Phone = s.Phone,
                    IsActive = s.IsActive,
                    ProfileImgName = s.ProfileImgName,
                    Type = "Seeker"
                }).ToList();
            }
            else if (type == "Recruiter")
            {
                return hrQuery.Select(h => new UserListDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    Email = h.User.Email,
                    Phone = h.Phone,
                    IsActive = h.IsActive,
                    ProfileImgName = h.ProfileImgName,
                    Type = "Recruiter"
                }).ToList();
            }
            else
            {
                var seekers = seekerQuery.Select(s => new UserListDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.User.Email,
                    Phone = s.Phone,
                    IsActive = s.IsActive,
                    ProfileImgName = s.ProfileImgName,
                    Type = "Seeker"
                }).ToList();

                var hrs = hrQuery.Select(h => new UserListDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    Email = h.User.Email,
                    Phone = h.Phone,
                    IsActive = h.IsActive,
                    ProfileImgName = h.ProfileImgName,
                    Type = "Recruiter"
                }).ToList();

                return seekers.Concat(hrs).ToList();
            }
        }
        public List<TopHrDto> GetTopHRs()
        {
            var topHRs = context.Hrs
                .Select(hr => new TopHrDto
                {
                    Name = hr.Name,
                    JobsCount = hr.HRTasks.Count()
                })
                .OrderByDescending(x => x.JobsCount)
                .Take(3)
                .ToList();

            return topHRs;
        }
        public List<MonthlyDecisionDTO> GetMonthlyDecisions(string filter = "year_2026")
        {
            var query = context.taskSubmissions
                .Where(t => (t.Status == "Accepted" || t.Status == "Rejected")
                         && t.DecisionDate != null);

            if (filter.StartsWith("year_"))
            {
                int year = int.Parse(filter.Split('_')[1]);

                query = query.Where(t => t.DecisionDate.Value.Year == year);

                var data = query
                    .GroupBy(t => t.DecisionDate.Value.Month)
                    .Select(g => new {
                        Month = g.Key,
                        Accepted = g.Count(t => t.Status == "Accepted"),
                        Rejected = g.Count(t => t.Status == "Rejected")
                    })
                    .ToList();

                return Enumerable.Range(1, 12).Select(m => new MonthlyDecisionDTO
                {
                    Month = m,
                    Year = year,
                    Accepted = data.FirstOrDefault(d => d.Month == m)?.Accepted ?? 0,
                    Rejected = data.FirstOrDefault(d => d.Month == m)?.Rejected ?? 0
                }).ToList();
            }
            else // month
            {
                var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                query = query.Where(t =>
                    t.DecisionDate.Value.Month == DateTime.Now.Month &&
                    t.DecisionDate.Value.Year == DateTime.Now.Year);

                var data = query
                    .GroupBy(t => t.DecisionDate.Value.Day)
                    .Select(g => new {
                        Day = g.Key,
                        Accepted = g.Count(t => t.Status == "Accepted"),
                        Rejected = g.Count(t => t.Status == "Rejected")
                    })
                    .ToList();

                return Enumerable.Range(1, daysInMonth).Select(d => new MonthlyDecisionDTO
                {
                    Month = DateTime.Now.Month,
                    Day = d,
                    Year = DateTime.Now.Year,
                    Accepted = data.FirstOrDefault(x => x.Day == d)?.Accepted ?? 0,
                    Rejected = data.FirstOrDefault(x => x.Day == d)?.Rejected ?? 0
                }).ToList();
            }
        }

        public List<int> GetDecisionYears()
        {
            return context.taskSubmissions
                .Where(t => t.DecisionDate != null)
                .Select(t => t.DecisionDate.Value.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();
        }
    }
}
