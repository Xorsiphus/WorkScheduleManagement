using System.Collections.Generic;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class DayOffInsteadVacationRequest : Request
    {
        public ApplicationUser Replacer { get; set; }
        
        public IList<DaysInsteadVacation> DaysInsteadVacation { get; set; }
    }
}