using System;
using System.Threading.Tasks;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Models.Models.Requests;

namespace WorkScheduleManagement.Models.DAO.Requests
{
    public interface IHolidayRequestDao
    {
        Task<HolidayRequestModel> Get(Guid id);
        Task<HolidayRequestModel> Create(HolidayRequestModel taskModel);
        Task<HolidayRequestModel> Update(HolidayRequestModel taskModel);
    }
}