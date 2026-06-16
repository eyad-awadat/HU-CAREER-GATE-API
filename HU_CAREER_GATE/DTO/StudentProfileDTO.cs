namespace HU_CAREER_GATE.DTO
{
    public class StudentProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobType { get; set; }
        public string JobLevel { get; set; }
        public string State { get; set; }
        public double GPA { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string ProfileImgName { get; set; }
        public string CvName { get; set; }

        public string LastTaskName { get; set; }
        public string Status { get; set; }

        public bool IsActive { get; set; }
        public int TotalApplications { get; set; }
    }
}
