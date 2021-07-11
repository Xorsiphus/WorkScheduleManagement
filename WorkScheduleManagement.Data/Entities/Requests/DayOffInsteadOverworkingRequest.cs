using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class DayOffInsteadOverworkingRequest : Request
    {
        public ApplicationUser Replacer { get; set; }
    }
}