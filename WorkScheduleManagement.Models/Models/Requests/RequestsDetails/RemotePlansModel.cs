﻿using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkScheduleManagement.Models.Models.Requests.RequestsDetails
{
    public class RemotePlansModel
    {
        [BindNever]
        public string Id { get; set; }

        public DateTime Date { get; set; }
        
        public string WorkingPlan { get; set; }
    }
}