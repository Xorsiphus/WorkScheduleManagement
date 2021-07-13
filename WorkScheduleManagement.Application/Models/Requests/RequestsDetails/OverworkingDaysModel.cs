using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkScheduleManagement.Application.Models.Requests.RequestsDetails
{
    public class OverworkingDaysModel
    {
        [BindNever]
        public string Id { get; set; }

        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }
    }
}