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
    public class GetRequestVacationTypeById
    {
        public record Query(VacationType Id) : IRequest<VacationTypes>;

        public class Handler : IRequestHandler<Query, VacationTypes>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<VacationTypes> Handle(Query request, CancellationToken cancellationToken)
            {
                var vacationType = await _context.VacationTypes.Where(t => t.Id == request.Id).FirstOrDefaultAsync();
                return vacationType;
            }
        }
    }
}