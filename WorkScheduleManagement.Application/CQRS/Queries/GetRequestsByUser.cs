using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetRequestsByUser
    {
        public record Query : IRequest<List<Request>>;

        public class Handler : IRequestHandler<Query, List<Request>>
        {
            private readonly AppDbContext _context;
            
            public Handler(AppDbContext context)
            {
                _context = context;
            }
            
            public async Task<List<Request>> Handle(Query request, CancellationToken cancellationToken)
            {
                var positions = await _context.Requests.ToListAsync();
                return positions;
            }
        }
    }
}