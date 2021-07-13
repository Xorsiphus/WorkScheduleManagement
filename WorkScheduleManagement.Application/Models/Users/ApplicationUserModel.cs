using Microsoft.AspNetCore.Identity;

namespace WorkScheduleManagement.Application.Models.Users
{
    public class ApplicationUserModel : IdentityUser
    {
        public int CountOfDays { get; set; }
    }
}