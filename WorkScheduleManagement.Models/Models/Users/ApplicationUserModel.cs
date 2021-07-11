using Microsoft.AspNetCore.Identity;

namespace WorkScheduleManagement.Models.Models.Users
{
    public class ApplicationUserModel : IdentityUser
    {
        public int CountOfDays { get; set; }
    }
}