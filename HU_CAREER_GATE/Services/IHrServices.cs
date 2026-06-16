using HUCAREERGATE.DTO;

namespace HUCAREERGATE.Services
{
    public interface IHrServices
    {
        int SaveAccount(HrDTO hrDTO, string userId);
        HrDTO LodeHr(string userId);
        void UpdateHr(HrDTO hrDTO, string userId);
        int GetTaskCount(int id);
        int GetCountHr();
    }
}