using Microsoft.AspNetCore.Identity;

namespace WorkScheduleManagement.Data.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public int VacationDaysCount { get; set; }
    }
}