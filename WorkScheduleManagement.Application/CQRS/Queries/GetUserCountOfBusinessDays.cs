using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Enums;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetUserCountOfBusinessDays
    {
        public record Query(string Id) : IRequest<int>;

        public class Handler : IRequestHandler<Query, int>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                var startD = new DateTime(DateTime.Now.Year, 1, 1);
                var endD = DateTime.Today;

                double calcBusinessDays =
                    1 + ((endD - startD).TotalDays * 5 -
                         (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

                if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
                if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

                var userHolidays = _context.HolidayRequest
                    .Include(r => r.HolidayList)
                    .Where(r => r.Creator.Id == request.Id && r.RequestStatus.Id == RequestStatus.Approved)
                    .ToListAsync().Result
                    .Aggregate(0, (cur, next) => cur + next.HolidayList.Count);

                var userVacations = _context.VacationRequests
                    .Where(r => r.DateFrom > startD &&
                                r.DateTo < new DateTime(DateTime.Now.Year, 12, 31) &&
                                r.Creator.Id == request.Id &&
                                r.RequestStatus.Id == RequestStatus.Approved)
                    .ToListAsync().Result
                    .Aggregate(0,
                        (cur, vacationRequest) =>
                            cur +
                            Convert.ToInt32(vacationRequest.DateTo.Subtract(vacationRequest.DateFrom).TotalDays) +
                            1);

                var userDayOffInsteadVacation = _context.DayOffInsteadVacationRequest
                    .Include(r => r.DaysOffInsteadVacation)
                    .Where(r => r.Creator.Id == request.Id && r.RequestStatus.Id == RequestStatus.Approved)
                    .ToListAsync().Result
                    .Aggregate(0, (cur, vacationRequest) => cur + vacationRequest.DaysOffInsteadVacation.Count);

                var userRemoteWorkRequests = await _context.RemoteWorkRequest
                    .Include(r => r.RemotePlans)
                    .Where(r => r.Creator.Id == request.Id && r.RequestStatus.Id == RequestStatus.Approved)
                    .ToListAsync();

                var userRemoteWorks =
                    userRemoteWorkRequests.Aggregate(0, (cur, workRequest) => cur + workRequest.RemotePlans.Count);

                return Convert.ToInt32(calcBusinessDays - userHolidays - userVacations - userDayOffInsteadVacation +
                                       userRemoteWorks);
            }
        }
    }
}