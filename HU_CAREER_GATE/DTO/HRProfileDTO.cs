namespace HU_CAREER_GATE.DTO
{
    public class HRProfileDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string CompanyName { get; set; }
        public bool IsFreeLancer { get; set; }

        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string ProfileImgName { get; set; }
        public string CvName { get; set; }
        public string LastJobPosted { get; set; }
        public int AcceptedStudents { get; set; }
        public int TotalTasks { get; set; }
        public bool IsActive { get; set; }
    }
}
