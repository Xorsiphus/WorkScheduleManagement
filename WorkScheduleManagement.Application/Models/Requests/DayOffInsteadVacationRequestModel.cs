using System.Collections.Generic;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;

namespace WorkScheduleManagement.Models.Models.Requests
{
    public class DayOffInsteadVacationRequestModel : RequestAbstractModel
    {
        public string Replacer { get; set; }
        
        public ICollection<OverworkingDays> OverworkingDays { get; set; }    
    }
}