using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class HolidayRequest : Request
    {
        public ApplicationUser Replacer { get; set; }
    }
}