namespace HUCAREERGATE.DTO
{
    public class TaskViolationDTO
    {
        public int Violations { get; set; }
        public bool IsBlocked { get; set; }
        public List<string> Reasons { get; set; }
    }
}
