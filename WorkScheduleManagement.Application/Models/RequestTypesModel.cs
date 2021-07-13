using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkScheduleManagement.Application.Models
{
    public class RequestTypesModel
    {
        [BindNever]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}