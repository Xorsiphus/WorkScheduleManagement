using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Application.Models.Requests
{
    public class RequestAbstractModel
    {
        [BindNever]
        public Guid Id { get; set; }

        public string Creator { get; set; }

        public RequestType Type { get; set; }

        public RequestStatus Status { get; set; }

        public string Approver { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime SentAt { get; set; }

        public string Comment { get; set; }

        public int CountOfDaysInRequest { get; set; }
    }
}