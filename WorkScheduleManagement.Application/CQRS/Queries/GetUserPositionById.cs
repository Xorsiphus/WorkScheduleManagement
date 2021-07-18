using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Users;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetPositionById
    {
        public record Query(int Id) : IRequest<UserPosition>;

        public class Handler : IRequestHandler<Query, UserPosition>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<UserPosition> Handle(Query request, CancellationToken cancellationToken)
            {
                var position = await _context.UserPositions.Where(p => p.Id == request.Id).FirstOrDefaultAsync();
                return position;
            }
        }
    }
}