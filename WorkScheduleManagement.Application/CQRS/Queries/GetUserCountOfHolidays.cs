using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Enums;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetUserCountOfHolidays
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
                var holidayRequest = await _context.HolidayRequest
                    .Include(r => r.HolidayList)
                    .Where(r => r.Creator.Id == request.Id && r.RequestStatus.Id == RequestStatus.Approved)
                    .ToListAsync();

                return holidayRequest.Aggregate(0, (cur, next) => cur + next.HolidayList.Count);
            }
        }
    }
}