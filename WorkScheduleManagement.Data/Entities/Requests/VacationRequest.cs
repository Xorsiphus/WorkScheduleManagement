﻿using System;
using System.Collections.Generic;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class VacationRequest : Request
    {
        public VacationTypes VacationType { get; set; }

        public ApplicationUser Replacer { get; set; }
        
        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }

        public bool IsShifting { get; set; }

        public IList<OverworkingDays> OverworkingDays { get; set; }
    }
}