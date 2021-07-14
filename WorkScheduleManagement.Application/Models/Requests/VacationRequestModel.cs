using System;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Application.Models.Requests
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