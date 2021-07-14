﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.RequestStatuses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RequestStatuses");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.RequestTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RequestTypes");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApproverId")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatorId")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("RequestStatusesId")
                        .HasColumnType("integer");

                    b.Property<int?>("RequestTypesId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("RequestStatusesId");

                    b.HasIndex("RequestTypesId");

                    b.ToTable("Requests");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Request");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.RequestsDetails.OverworkingDays", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("RequestId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("OverworkingDays");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.RequestsDetails.RemotePlans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("RequestId")
                        .HasColumnType("uuid");

                    b.Property<string>("WorkingPlan")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("RemotePlans");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Roles.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int?>("PositionId")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<int>("UnusedVacationDaysCount")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("PositionId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Users.UserPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("NumberOfWorkingHours")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserPositions");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.VacationTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("VacationTypes");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.DayOffInsteadOverworkingRequest", b =>
                {
                    b.HasBaseType("WorkScheduleManagement.Data.Entities.Requests.Request");

                    b.Property<string>("ReplacerId")
                        .HasColumnType("text")
                        .HasColumnName("DayOffInsteadOverworkingRequest_ReplacerId");

                    b.HasIndex("ReplacerId");

                    b.HasDiscriminator().HasValue("DayOffInsteadOverworkingRequest");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.DayOffInsteadVacationRequest", b =>
                {
                    b.HasBaseType("WorkScheduleManagement.Data.Entities.Requests.Request");

                    b.Property<string>("ReplacerId")
                        .HasColumnType("text")
                        .HasColumnName("DayOffInsteadVacationRequest_ReplacerId");

                    b.HasIndex("ReplacerId");

                    b.HasDiscriminator().HasValue("DayOffInsteadVacationRequest");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.HolidayRequest", b =>
                {
                    b.HasBaseType("WorkScheduleManagement.Data.Entities.Requests.Request");

                    b.Property<string>("ReplacerId")
                        .HasColumnType("text");

                    b.HasIndex("ReplacerId");

                    b.HasDiscriminator().HasValue("HolidayRequest");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.RemoteWorkRequest", b =>
                {
                    b.HasBaseType("WorkScheduleManagement.Data.Entities.Requests.Request");

                    b.HasDiscriminator().HasValue("RemoteWorkRequest");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.VacationRequest", b =>
                {
                    b.HasBaseType("WorkScheduleManagement.Data.Entities.Requests.Request");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsShifting")
                        .HasColumnType("boolean");

                    b.Property<string>("ReplacerId")
                        .HasColumnType("text")
                        .HasColumnName("VacationRequest_ReplacerId");

                    b.Property<int?>("VacationTypeId")
                        .HasColumnType("integer");

                    b.HasIndex("ReplacerId");

                    b.HasIndex("VacationTypeId");

                    b.HasDiscriminator().HasValue("VacationRequest");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Roles.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Roles.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.Request", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverId");

                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("WorkScheduleManagement.Data.Entities.RequestStatuses", "RequestStatuses")
                        .WithMany()
                        .HasForeignKey("RequestStatusesId");

                    b.HasOne("WorkScheduleManagement.Data.Entities.RequestTypes", "RequestTypes")
                        .WithMany()
                        .HasForeignKey("RequestTypesId");

                    b.Navigation("Approver");

                    b.Navigation("Creator");

                    b.Navigation("RequestStatuses");

                    b.Navigation("RequestTypes");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.RequestsDetails.OverworkingDays", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Requests.DayOffInsteadOverworkingRequest", "Request")
                        .WithMany("OverworkingDays")
                        .HasForeignKey("RequestId");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.RequestsDetails.RemotePlans", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Requests.RemoteWorkRequest", "Request")
                        .WithMany("RemotePlans")
                        .HasForeignKey("RequestId");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.UserPosition", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.DayOffInsteadOverworkingRequest", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", "Replacer")
                        .WithMany()
                        .HasForeignKey("ReplacerId");

                    b.Navigation("Replacer");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.DayOffInsteadVacationRequest", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", "Replacer")
                        .WithMany()
                        .HasForeignKey("ReplacerId");

                    b.Navigation("Replacer");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.HolidayRequest", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", "Replacer")
                        .WithMany()
                        .HasForeignKey("ReplacerId");

                    b.Navigation("Replacer");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.VacationRequest", b =>
                {
                    b.HasOne("WorkScheduleManagement.Data.Entities.Users.ApplicationUser", "Replacer")
                        .WithMany()
                        .HasForeignKey("ReplacerId");

                    b.HasOne("WorkScheduleManagement.Data.Entities.VacationTypes", "VacationType")
                        .WithMany()
                        .HasForeignKey("VacationTypeId");

                    b.Navigation("Replacer");

                    b.Navigation("VacationType");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.DayOffInsteadOverworkingRequest", b =>
                {
                    b.Navigation("OverworkingDays");
                });

            modelBuilder.Entity("WorkScheduleManagement.Data.Entities.Requests.RemoteWorkRequest", b =>
                {
                    b.Navigation("RemotePlans");
                });
#pragma warning restore 612, 618
        }
    }
}
