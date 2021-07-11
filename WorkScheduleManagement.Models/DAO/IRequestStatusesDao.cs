using System;
using System.Threading.Tasks;
using WorkScheduleManagement.Models.Models;

namespace WorkScheduleManagement.Models.DAO
{
    public interface IRequestStatusesDao
    {
        Task<RequestStatusesModel> Get(Guid id);
    }
}