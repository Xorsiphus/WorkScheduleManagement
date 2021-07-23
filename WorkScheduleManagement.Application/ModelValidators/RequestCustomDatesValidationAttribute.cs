using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WorkScheduleManagement.Application.ModelValidators
{
    public class RequestCustomDatesValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var customDates = (IList<DateTime>)value;

            if (customDates == null)
                return true;

            foreach (var date in customDates)
            {
                if (date < DateTime.Today)
                    return false;
            }
            
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, "Некорректная дата");
        }
    }
}