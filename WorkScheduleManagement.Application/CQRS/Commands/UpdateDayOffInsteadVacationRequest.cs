using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Commands
{
    public static class UpdateDayOffInsteadVacationRequest
    {
        public record Command(DayOffInsteadVacationRequest Request) : IRequest<bool>;

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
                    .DayOffInsteadVacation
                    .Where(d => d.Request.Id == request.Request.Id)
                    .ToList();
                _context.DayOffInsteadVacation.RemoveRange(oldDates);
                
                _context.DayOffInsteadVacationRequest.Update(request.Request);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}