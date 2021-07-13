using System.Collections.Generic;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class RemoteWorkRequest : Request
    {
        public IList<RemotePlans> RemotePlans { get; set; }
    }
}