using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetRequestTypes
    {
        public record Query : IRequest<List<RequestTypes>>;

        public class Handler : IRequestHandler<Query, List<RequestTypes>>
        {
            private readonly AppDbContext _context;
            
            public Handler(AppDbContext context)
            {
                _context = context;
            }
            
            public async Task<List<RequestTypes>> Handle(Query request, CancellationToken cancellationToken)
            {
                var types = await _context.RequestTypes.ToListAsync();
                return types;
            }
        }
    }
}