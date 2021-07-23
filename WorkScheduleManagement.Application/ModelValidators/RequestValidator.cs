using System;
using FluentValidation;
using WorkScheduleManagement.Application.Models.Requests;

namespace WorkScheduleManagement.Application.ModelValidators
{
    public class RequestValidator : AbstractValidator<RequestCreationModel>
    {
        public RequestValidator()
        {
            RuleFor(x => x.DateFrom)
                
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Некорректная дата");
            // .SetValidator(new RequestDateValidator());
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo)
                .WithMessage("Некорректный промежуток дат");

            RuleFor(x => x.DateTo)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Некорректная дата");
            // .SetValidator(new RequestDateValidator());

            RuleForEach(x => x.CustomDays)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Некорректная дата");
            // .SetValidator(new RequestDateValidator());
        }
    }
}