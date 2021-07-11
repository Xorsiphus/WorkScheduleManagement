using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class DayOffInsteadVacationRequest : Request
    {
        public ApplicationUser Replacer { get; set; }
    }
}