using System;
using System.Threading.Tasks;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Models.Models.Requests;

namespace WorkScheduleManagement.Models.DAO.Requests
{
    public interface IDayOffInsteadVacationRequestDao
    {
        Task<DayOffInsteadVacationRequestModel> Get(Guid id);
        Task<DayOffInsteadVacationRequestModel> Create(DayOffInsteadVacationRequestModel taskModel);
        Task<DayOffInsteadVacationRequestModel> Update(DayOffInsteadVacationRequestModel taskModel);
    }
}