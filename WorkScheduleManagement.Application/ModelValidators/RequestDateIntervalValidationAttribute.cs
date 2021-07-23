using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WorkScheduleManagement.Application.ModelValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class RequestDateIntervalValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var requestDate = (DateTime)value;
            if (requestDate == DateTime.MinValue)
                return true;
            return requestDate >= DateTime.Today;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, "Некорректная дата");
        }
    }
}