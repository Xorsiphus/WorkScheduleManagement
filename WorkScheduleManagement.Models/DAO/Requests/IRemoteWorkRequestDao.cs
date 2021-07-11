using System;
using System.Threading.Tasks;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Models.Models.Requests;

namespace WorkScheduleManagement.Models.DAO.Requests
{
    public interface IRemoteWorkRequestDao
    {
        Task<RemoteWorkRequestModel> Get(Guid id);
        Task<RemoteWorkRequestModel> Create(RemoteWorkRequestModel taskModel);
        Task<RemoteWorkRequestModel> Update(RemoteWorkRequestModel taskModel);
    }
}