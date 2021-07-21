using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkScheduleManagement.Application.Models.Requests.RequestsDetails;
using WorkScheduleManagement.Application.ModelValidators;
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
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [RequestDateValidation]
        public DateTime DateFrom { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [RequestDateValidation]
        public DateTime DateTo { get; set; }
        
        public IList<DateTime> CustomDays { get; set; }
        
        public IList<string> RemotePlans { get; set; }

        public string Comment { get; set; }

        public IEnumerable<UserIdNameModel> AllReplacerUsers { get; set; }

        public string Replacer { get; set; }
        
        public IEnumerable<UserIdNameModel> AllApproverUsers { get; set; }

        public string Approver { get; set; }

        [Required]
        public bool IsShifting { get; set; }

        public ICollection<VacationTypes> AllVacationTypes { get; set; }

        public VacationType VacationType { get; set; }
        
    }
}