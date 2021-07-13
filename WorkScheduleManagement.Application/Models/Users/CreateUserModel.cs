namespace WorkScheduleManagement.Application.Models.Users
{
    public class CreateUserModel
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string FullName { get; set; }
        
        
        public string Role { get; set; }
        
        public string Position { get; set; }
        
        public string PhoneNumber { get; set; }
    }
}