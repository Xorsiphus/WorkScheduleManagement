using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Application.Models.Requests;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Data.Entities.Users;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetRequestTableModels
    {
        public record Query(string UserId) : IRequest<List<RequestsTableModel>>;

        public class Handler : IRequestHandler<Query, List<RequestsTableModel>>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(AppDbContext context, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public async Task<List<RequestsTableModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                var userRoles = await _userManager.GetRolesAsync(user);
                List<Request> requests;

                if (userRoles.Contains("admin") || userRoles.Contains("director") || userRoles.Contains("supervisor"))
                {
                    requests = await _context.Requests
                        .Include(r => r.Creator)
                        .Include(r => r.RequestTypes)
                        .Include(r => r.RequestStatus)
                        .ToListAsync();
                }
                else
                {
                    requests = await _context.Requests
                        .Where(r => r.Creator.Id == request.UserId)
                        .Include(r => r.Creator)
                        .Include(r => r.RequestTypes)
                        .Include(r => r.RequestStatus)
                        .ToListAsync();
                }
                
                return requests.ConvertAll(r => new RequestsTableModel
                {
                    Id = r.Id.ToString(),
                    Creator = r.Creator.FullName,
                    RequestType = r.RequestTypes.Name,
                    RequestStatus = r.RequestStatus.Name,
                    SelectedDates = "",
                    CountOfVacationDays = 0
                });
            }
        }
    }
}