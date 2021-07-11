using System;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class VacationRequest : Request
    {
        public VacationTypes VacationType { get; set; }

        public Guid Replacer { get; set; }
        
        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }

        public bool IsShifting { get; set; }
        
        
    }
}