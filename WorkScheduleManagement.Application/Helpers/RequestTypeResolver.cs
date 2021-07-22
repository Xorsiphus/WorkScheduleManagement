using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Application.Helpers
{
    public static class RequestTypeResolver
    {
        public static Request Resolve(RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.OnHoliday:
                    return new HolidayRequest();
                case RequestType.OnVacation:
                    return new VacationRequest();
                case RequestType.OnRemoteWork:
                    return new RemoteWorkRequest();
                case RequestType.OnDayOffInsteadOverworking:
                    return new DayOffInsteadOverworkingRequest();
                case RequestType.OnDayOffInsteadVacation:
                    return new DayOffInsteadVacationRequest();
                default:
                    return null;
            }
        }
        
    }
}