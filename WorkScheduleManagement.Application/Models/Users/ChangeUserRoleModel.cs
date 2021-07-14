using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WorkScheduleManagement.Application.Models.Users
{
    public class ChangeUserRoleModel
    {
        public string UserId { get; set; }
        
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        
        public ChangeUserRoleModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}