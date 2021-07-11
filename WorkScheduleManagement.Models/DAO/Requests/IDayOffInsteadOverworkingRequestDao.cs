using System;
using System.Threading.Tasks;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Models.Models.Requests;

namespace WorkScheduleManagement.Models.DAO.Requests
{
    public interface IDayOffInsteadOverworkingRequestDao
    {
        Task<DayOffInsteadOverworkingRequestModel> Get(Guid id);
        Task<DayOffInsteadOverworkingRequestModel> Create(DayOffInsteadOverworkingRequestModel taskModel);
        Task<DayOffInsteadOverworkingRequestModel> Update(DayOffInsteadOverworkingRequestModel taskModel);
    }
}