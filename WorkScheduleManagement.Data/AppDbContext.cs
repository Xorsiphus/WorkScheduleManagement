using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<DayOffInsteadVacationRequest> DayOffInsteadVacationRequest { get; set; }
        public DbSet<DayOffInsteadOverworkingRequest> DayOffInsteadOverworkingRequest { get; set; }
        public DbSet<HolidayRequest> HolidayRequest { get; set; }
        public DbSet<RemoteWorkRequest> RemoteWorkRequest { get; set; }

        public DbSet<OverworkingDays> OverworkingDays { get; set; }
        public DbSet<RemotePlans> RemotePlans { get; set; }

        public DbSet<RequestTypes> RequestTypes { get; set; }
        public DbSet<RequestStatuses> RequestStatuses { get; set; }
        public DbSet<VacationTypes> VacationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OverworkingDays>()
                .HasNoKey();
            
            modelBuilder.Entity<RemotePlans>()
                .HasNoKey();
        
            base.OnModelCreating(modelBuilder);
        }
    }
}