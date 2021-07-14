using Microsoft.AspNetCore.Identity;

namespace WorkScheduleManagement.Data.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public UserPosition Position { get; set; }

        public int UnusedVacationDaysCount { get; set; }
    }
}