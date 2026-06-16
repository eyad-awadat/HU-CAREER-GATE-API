using HU_CAREER_GATE.DTO;
using HUCAREERGATE.DTO;

namespace HU_CAREER_GATE.Services
{
    public interface IManagerServices
    {
        List<UserListDTO> GetHRsForUsers();
        List<UserListDTO> GetStudentsForUsers();
        StudentProfileDTO GetProfileSeeker(int id);
        HRProfileDTO GetProfileHR(int id);
        List<UserListDTO> SearchUser(string name, string phone, string type, bool? isActive);
        List<TopHrDto> GetTopHRs();
        bool DeactivateStudentUser(int id);
        bool DeactivateHrUser(int id);
        bool ActivateStudentUser(int id);
        bool ActivateHrUser(int id);
        List<MonthlyDecisionDTO> GetMonthlyDecisions(string filter = "year");
        List<int> GetDecisionYears();

    }
}