using HUCAREERGATE.DTO;

namespace HUCAREERGATE.Services
{
    public interface IStudentService
    {
        int AddStudent(StudentDTO studentDTO, string userId);
        StudentDTO GetStudentById(string userId);
        void UpdateStudent(StudentDTO studentDTO, string userId);
        int GetCountApplication(string userId);
        int GetCountStudent();
        List<StudentDTO> GetStudentM();
    }
}