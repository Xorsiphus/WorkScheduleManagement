using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Application.Models.Requests
{
    public class RequestCreationModel
    {
        public string Id { get; set; }

        public string Creator { get; set; }

        public ICollection<RequestTypes> AllTypes { get; set; }
        
        public RequestType Type { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }
        
        [Required]
        public DateTime DateTo { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime SentAt { get; set; }

        public string Comment { get; set; }

        public ICollection<UserIdNameModel> AllUsers { get; set; }

        public string Replacer { get; set; }

        [Required]
        public bool IsShifting { get; set; }

        public int CalculatedNumberOfDays { get; set; }
        
        public ICollection<VacationTypes> AllVacationTypes { get; set; }

        public VacationType VacationType { get; set; }
        
    }
}