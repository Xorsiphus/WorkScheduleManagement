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
    public static class GetUserCountOfUsedVacationDays
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
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);

                var vacationRequests = await _context.VacationRequests
                    .Where(r => r.DateFrom > firstDay && 
                                r.DateTo < lastDay && 
                                r.Creator.Id == request.Id &&
                                r.RequestStatus.Id == RequestStatus.Approved)
                    .ToListAsync();

                return vacationRequests.Aggregate(0,
                    (cur, vacationRequest) =>
                        cur + Convert.ToInt32(vacationRequest.DateTo.Subtract(vacationRequest.DateFrom).TotalDays) + 1);
            }
        }
    }
}