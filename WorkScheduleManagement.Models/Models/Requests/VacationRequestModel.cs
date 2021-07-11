using System;
using WorkScheduleManagement.Data.Constant;

namespace WorkScheduleManagement.Models.Models.Requests
{
    public class VacationRequestModel : RequestAbstractModel
    {
        public VacationType VacationType { get; set; }

        public string Replacer { get; set; }
        
        public DateTime VacationDateFrom { get; set; }
        
        public DateTime VacationDateTo { get; set; }

        public bool IsShifting { get; set; }
    }
}