using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Users;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetPositions
    {
        public record Query : IRequest<List<UserPosition>>;

        public class Handler : IRequestHandler<Query, List<UserPosition>>
        {
            private readonly AppDbContext _context;
            
            public Handler(AppDbContext context)
            {
                _context = context;
            }
            
            public async Task<List<UserPosition>> Handle(Query request, CancellationToken cancellationToken)
            {
                var positions = await _context.UserPositions.ToListAsync();
                return positions == null ? null : positions;
            }
        }
    }
}