using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetRequestVacationTypes
    {
        public record Query : IRequest<List<VacationTypes>>;

        public class Handler : IRequestHandler<Query, List<VacationTypes>>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<VacationTypes>> Handle(Query request, CancellationToken cancellationToken)
            {
                var vacationTypes = await _context.VacationTypes.ToListAsync();
                return vacationTypes;
            }
        }
    }
}