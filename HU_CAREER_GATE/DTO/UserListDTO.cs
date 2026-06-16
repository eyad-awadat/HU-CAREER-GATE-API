namespace HU_CAREER_GATE.DTO
{
    public class UserListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public string? ProfileImgName { get; set; }
        public bool IsActive { get; set; }
    }
}
