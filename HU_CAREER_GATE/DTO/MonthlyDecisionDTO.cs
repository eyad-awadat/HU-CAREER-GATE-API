namespace HU_CAREER_GATE.DTO
{
    public class MonthlyDecisionDTO
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public int Accepted { get; set; }
        public int Rejected { get; set; }
        public string MonthName => Day > 0
            ? $"Day {Day}"
            : new DateTime(Year, Month, 1).ToString("MMM");
    }
}
