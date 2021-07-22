namespace WorkScheduleManagement.Application.Models.Users
{
    public class UserTableModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        
        public string FullName { get; set; }
        
        public int CountOfHolidays { get; set; }
        
        public int CountOfUnusedVacationDays { get; set; }
        
        public int CountOfBusinessDays { get; set; }
        
        public int CountOfUnusedVacationDaysPastYears { get; set; }
    }
}