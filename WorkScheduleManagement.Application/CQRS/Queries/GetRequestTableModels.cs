using System;
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
using WorkScheduleManagement.Data.Enums;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetRequestTableModels
    {
        public record Query(string UserId) : IRequest<List<ReqeustTableModel>>;

        public class Handler : IRequestHandler<Query, List<ReqeustTableModel>>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(AppDbContext context, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public async Task<List<ReqeustTableModel>> Handle(Query request, CancellationToken cancellationToken)
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
                
                return requests.ConvertAll(r =>
                {
                    string selectedDays;
                    int countOfDays;
                    
                    switch (r.RequestTypes.Id)
                    {
                        case RequestType.OnHoliday:
                            var holidayList = _context.HolidayList
                                .Where(l => l.Request.Id == r.Id)
                                .ToList();
                            countOfDays = holidayList.Count;
                            selectedDays = string.Join( "; ", holidayList.ConvertAll(l => l.Date.ToString("dd/MM/yyyy")));
                            break;
                        
                        case RequestType.OnVacation:
                            var fullRequest = _context.VacationRequests.FirstOrDefault(c => c.Id == r.Id);
                            countOfDays = Convert.ToInt32(fullRequest?.DateTo.Subtract(fullRequest.DateFrom).TotalDays) + 1;
                            selectedDays = fullRequest?.DateFrom.ToString("dd/MM/yyyy") +
                                           " - " +
                                           fullRequest?.DateTo.ToString("dd/MM/yyyy");
                            break;
                        
                        case RequestType.OnRemoteWork:
                            var remotePlans = _context.RemotePlans
                                .Where(l => l.Request.Id == r.Id)
                                .ToList();
                            countOfDays = remotePlans.Count;
                            selectedDays = string.Join( "; ", remotePlans.ConvertAll(l => l.Date.ToString("dd/MM/yyyy")));
                            break;
                        
                        case RequestType.OnDayOffInsteadOverworking:
                            var dayOffInsteadOverworking = _context.DayOffInsteadOverworking
                                .Where(l => l.Request.Id == r.Id)
                                .ToList();
                            countOfDays = dayOffInsteadOverworking.Count;
                            selectedDays = string.Join( "; ", dayOffInsteadOverworking.ConvertAll(l => l.Date.ToString("dd/MM/yyyy")));
                            break;
                        
                        case RequestType.OnDayOffInsteadVacation:
                            var dayOffInsteadVacation = _context.DayOffInsteadVacation
                                .Where(l => l.Request.Id == r.Id)
                                .ToList();
                            countOfDays = dayOffInsteadVacation.Count;
                            selectedDays = string.Join( "; ", dayOffInsteadVacation.ConvertAll(l => l.Date.ToString("dd/MM/yyyy")));
                            break;
                        default:
                            selectedDays = "";
                            countOfDays = 0;
                            break;
                    }
                    
                    return new ReqeustTableModel
                    {
                        Id = r.Id.ToString(),
                        Creator = r.Creator.FullName,
                        RequestType = r.RequestTypes.Name,
                        RequestStatus = r.RequestStatus.Name,
                        SelectedDates = selectedDays,
                        CountOfVacationDays = countOfDays
                    };
                });
            }
        }
    }
}