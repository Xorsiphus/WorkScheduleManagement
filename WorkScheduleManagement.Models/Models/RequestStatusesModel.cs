﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkScheduleManagement.Models.Models
{
    public class RequestStatusesModel
    {
        [BindNever]
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}