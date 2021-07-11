using System;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public abstract class Request : IEntity
    {
        public Guid Id { get; set; }

        public ApplicationUser Creator { get; set; }

        public RequestTypes RequestTypes { get; set; }
        
        public RequestStatuses RequestStatuses { get; set; }

        public ApplicationUser Approver { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime SentAt { get; set; }

        public string Comment { get; set; }
    }
}