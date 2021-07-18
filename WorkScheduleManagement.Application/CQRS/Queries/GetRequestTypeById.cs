using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Data.Enums;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public class GetRequestTypeById
    {
        public record Query(RequestType Id) : IRequest<RequestTypes>;

        public class Handler : IRequestHandler<Query, RequestTypes>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<RequestTypes> Handle(Query request, CancellationToken cancellationToken)
            {
                var status = await _context.RequestTypes.Where(t => t.Id == request.Id).FirstOrDefaultAsync();
                return status;
            }
        }
    }
}