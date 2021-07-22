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
    public class GetUserCountOfUnusedVacationDaysPastYears
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
                int totalPastYearsVacationDays = 0;

                for (var i = 2021; i < DateTime.Today.Year; i++)
                {
                    var startD = new DateTime(i, 1, 1);
                    var endD = new DateTime(i, 12, 31);

                    var userVacationRequests = await _context.VacationRequests
                        .Where(r => r.DateFrom > startD &&
                                    r.DateTo < endD &&
                                    r.Creator.Id == request.Id &&
                                    r.RequestStatus.Id == RequestStatus.Approved)
                        .ToListAsync();
                    var yearVacationDays = userVacationRequests.Aggregate(0,
                        (cur, vacationRequest) =>
                            cur + Convert.ToInt32(vacationRequest.DateTo.Subtract(vacationRequest.DateFrom).TotalDays) +
                            1);

                    totalPastYearsVacationDays += yearVacationDays;
                }

                return totalPastYearsVacationDays;
            }
        }
    }
}