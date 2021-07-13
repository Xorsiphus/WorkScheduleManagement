using System.Collections.Generic;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class DayOffInsteadOverworkingRequest : Request
    {
        public ApplicationUser Replacer { get; set; }
        
        public IList<OverworkingDays> OverworkingDays { get; set; }
    }
}