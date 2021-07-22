using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Data.Enums;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetRequestById
    {
        public record Query(string Id) : IRequest<Request>;

        public class Handler : IRequestHandler<Query, Request>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Request> Handle(Query request, CancellationToken cancellationToken)
            {
                var receivedRequest = await _context
                    .Requests
                    .Where(r => r.Id.ToString() == request.Id)
                    .Include(r => r.Approver)
                    .Include(r => r.RequestTypes)
                    .FirstOrDefaultAsync();

                switch (receivedRequest.RequestTypes.Id)
                {
                    case RequestType.OnHoliday:
                        var holidayRequest = await _context.HolidayRequest
                            .Where(r => r.Id == receivedRequest.Id)
                            .Include(r => r.HolidayList)
                            .Include(r => r.Replacer)
                            .FirstOrDefaultAsync();
                        
                        return new HolidayRequest
                        {
                            Id = holidayRequest.Id,
                            RequestTypes = receivedRequest.RequestTypes,
                            Approver = receivedRequest.Approver,
                            Replacer = holidayRequest.Replacer,
                            HolidayList = holidayRequest.HolidayList,
                            Comment = holidayRequest.Comment
                        };
                    case RequestType.OnVacation:
                        var vacationRequest = await _context.VacationRequests
                            .Where(r => r.Id == receivedRequest.Id)
                            .Include(r => r.VacationType)
                            .Include(r => r.Replacer)
                            .FirstOrDefaultAsync();
                        
                        return new VacationRequest
                        {
                            Id = vacationRequest.Id,
                            RequestTypes = receivedRequest.RequestTypes,
                            Approver = receivedRequest.Approver,
                            Replacer = vacationRequest.Replacer,
                            VacationType = vacationRequest.VacationType,
                            Comment = vacationRequest.Comment,
                            DateFrom = vacationRequest.DateFrom,
                            DateTo = vacationRequest.DateTo,
                            IsShifting = vacationRequest.IsShifting
                        };
                    case RequestType.OnRemoteWork:
                        var remoteWorkRequest = await _context.RemoteWorkRequest
                            .Where(r => r.Id == receivedRequest.Id)
                            .Include(r => r.RemotePlans)
                            .FirstOrDefaultAsync();
                        
                        return new RemoteWorkRequest
                        {
                            Id = remoteWorkRequest.Id,
                            RequestTypes = receivedRequest.RequestTypes,
                            Approver = receivedRequest.Approver,
                            RemotePlans = remoteWorkRequest.RemotePlans,
                            Comment = remoteWorkRequest.Comment,
                        };
                    case RequestType.OnDayOffInsteadOverworking:
                        var dayOffInsteadOverworkingRequest = await _context.DayOffInsteadOverworkingRequest
                            .Where(r => r.Id == receivedRequest.Id)
                            .Include(r => r.Replacer)
                            .Include(r => r.DaysOffInsteadOverworking)
                            .FirstOrDefaultAsync();
                        
                        return new DayOffInsteadOverworkingRequest
                        {
                            Id = dayOffInsteadOverworkingRequest.Id,
                            RequestTypes = receivedRequest.RequestTypes,
                            Approver = receivedRequest.Approver,
                            Replacer = dayOffInsteadOverworkingRequest.Replacer,
                            Comment = dayOffInsteadOverworkingRequest.Comment,
                            DateFrom = dayOffInsteadOverworkingRequest.DateFrom,
                            DateTo = dayOffInsteadOverworkingRequest.DateTo,
                            DaysOffInsteadOverworking = dayOffInsteadOverworkingRequest.DaysOffInsteadOverworking
                        };
                    case RequestType.OnDayOffInsteadVacation:
                        var dayOffInsteadVacationRequest = await _context.DayOffInsteadVacationRequest
                            .Where(r => r.Id == receivedRequest.Id)
                            .Include(r => r.Replacer)
                            .Include(r => r.DaysOffInsteadVacation)
                            .FirstOrDefaultAsync();
                        
                        return new DayOffInsteadVacationRequest()
                        {
                            Id = dayOffInsteadVacationRequest.Id,
                            RequestTypes = receivedRequest.RequestTypes,
                            Approver = receivedRequest.Approver,
                            Replacer = dayOffInsteadVacationRequest.Replacer,
                            Comment = dayOffInsteadVacationRequest.Comment,
                            DaysOffInsteadVacation = dayOffInsteadVacationRequest.DaysOffInsteadVacation
                        };
                    default:
                        return null;
                }
            }
        }
    }
}