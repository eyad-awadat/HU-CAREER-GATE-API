using AutoMapper;
using HUCAREERGATE.Data;
using HUCAREERGATE.DTO;
using Microsoft.EntityFrameworkCore;

namespace HUCAREERGATE.Services
{
    public class StudentService : IStudentService
    {
        public IMapper mapper;
        public HUContext context;
        public StudentService(IMapper _mapper,
                                HUContext _context)
        {
            mapper = _mapper;
            context = _context;
        }
        public int AddStudent(StudentDTO studentDTO , string userId)
        {
            Student student = mapper.Map<Student>(studentDTO);
            student.UserId = userId;
            context.Students.Add(student);
            context.SaveChanges();
            return student.Id;
        }
        public StudentDTO GetStudentById(string userId)
        {
            Student student = context.Students.Include(s => s.User).FirstOrDefault(s => s.UserId == userId);
            StudentDTO studentDTO = mapper.Map<StudentDTO>(student);
            studentDTO.Email = student.User.Email;
            if (studentDTO != null)
            {
                return studentDTO;
            }
            else
            {
                return null;
            }
            
        }
        public void UpdateStudent(StudentDTO studentDTO, string userId)
        {
            var student = context.Students.FirstOrDefault(s => s.UserId == userId);
            if (student != null)
            {
                student.Name = studentDTO.Name;
                student.State = studentDTO.State;
                student.GPA = studentDTO.GPA;
                student.JobType = studentDTO.JobType;
                student.JobLevel = studentDTO.JobLevel;
                student.Country = studentDTO.Country;
                student.City = studentDTO.City;
                student.Phone = studentDTO.Phone;
                student.ProfileImgName = studentDTO.ProfileImgName;
                student.CvName = studentDTO.CvName;
                context.SaveChanges();
            }   

        }
        public int GetCountApplication(string userId)
        {
            int studentCountTask = context.Students.Where(i => i.UserId == userId).Select(c => c.TaskSubmissions.Count()).FirstOrDefault();
            return studentCountTask;
        }
        public int GetCountStudent()
        {
            int CountStudent = context.Students.Count();
            return CountStudent;
        }
        public List<StudentDTO> GetStudentM()
        {
            return context.Students
        .Include(i => i.User)
        .Select(i => new StudentDTO
        {
            Id = i.Id,
            Name = i.Name,
            State = i.State,
            Phone = i.Phone,
            GPA = i.GPA,
            Country = i.Country,
            City = i.City,
            JobType = i.JobType,
            JobLevel = i.JobLevel,
            ProfileImgName = i.ProfileImgName,
            CvName = i.CvName,
            Email = i.User.Email,
         
        })
        .ToList();

        }
    }
}
