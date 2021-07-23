using System;
using FluentValidation;

namespace WorkScheduleManagement.Application.ModelValidators
{
    public class RequestDateValidator : AbstractValidator<DateTime>
    {
        public RequestDateValidator()
        {
            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Некорректная дата");
        }
    }
}