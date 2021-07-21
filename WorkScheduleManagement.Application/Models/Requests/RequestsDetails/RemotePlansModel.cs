using System;
using WorkScheduleManagement.Data.Entities.Requests;

namespace WorkScheduleManagement.Application.Models.Requests.RequestsDetails
{
    public class RemotePlansModel
    {
        public string Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public string WorkingPlan { get; set; }

        public RemoteWorkRequest Request { get; set; }
    }
}