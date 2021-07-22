using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Commands
{
    public static class UpdateHolidayRequest
    {
        public record Command(HolidayRequest Request) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var oldDates = _context
                    .HolidayList
                    .Where(d => d.Request.Id == request.Request.Id)
                    .ToList();
                _context.HolidayList.RemoveRange(oldDates);

                _context.HolidayRequest.Update(request.Request);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}