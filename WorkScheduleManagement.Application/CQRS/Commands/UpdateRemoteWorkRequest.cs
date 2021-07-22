using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Commands
{
    public static class UpdateRemoteWorkRequest
    {
        public record Command(RemoteWorkRequest Request) : IRequest<bool>;

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
                    .RemotePlans
                    .Where(d => d.Request.Id == request.Request.Id)
                    .ToList();
                _context.RemotePlans.RemoveRange(oldDates);
                
                _context.RemoteWorkRequest.Update(request.Request);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}