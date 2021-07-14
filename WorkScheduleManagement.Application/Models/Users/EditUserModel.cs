using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Application.Models.Users
{
    public class EditUserModel
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string Role { get; set; }
        
        [Required]
        public int Position { get; set; }
        
        public List<UserPosition> AllPositions { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
    }
}