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
    public class GetRequestStatusById
    {
        public record Query(RequestStatus Id) : IRequest<RequestStatuses>;

        public class Handler : IRequestHandler<Query, RequestStatuses>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<RequestStatuses> Handle(Query request, CancellationToken cancellationToken)
            {
                var status = await _context.RequestStatuses.Where(t => t.Id == request.Id).FirstOrDefaultAsync();
                return status;
            }
        }
    }
}