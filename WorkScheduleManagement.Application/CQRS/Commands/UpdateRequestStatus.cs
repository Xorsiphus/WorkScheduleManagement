using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Commands
{
    public static class UpdateRequestStatus
    {
        public record Command(Request Request) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var originRequest = await _context.Requests.Include(r => r.RequestStatus).FirstOrDefaultAsync(r => request.Request.Id == r.Id);
                originRequest.RequestStatus = await _context.RequestStatuses.FirstOrDefaultAsync(s => s.Id == request.Request.RequestStatus.Id);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}