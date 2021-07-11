using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WorkScheduleManagement.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestTypesId = table.Column<int>(type: "integer", nullable: true),
                    RequestStatusesId = table.Column<int>(type: "integer", nullable: true),
                    Approver = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    DayOffInsteadOverworkingRequest_Replacer = table.Column<Guid>(type: "uuid", nullable: true),
                    DayOffInsteadVacationRequest_Replacer = table.Column<Guid>(type: "uuid", nullable: true),
                    Replacer = table.Column<Guid>(type: "uuid", nullable: true),
                    VacationTypeId = table.Column<int>(type: "integer", nullable: true),
                    VacationRequest_Replacer = table.Column<Guid>(type: "uuid", nullable: true),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsShifting = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_RequestStatuses_RequestStatusesId",
                        column: x => x.RequestStatusesId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_RequestTypes_RequestTypesId",
                        column: x => x.RequestTypesId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_VacationTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OverworkingDays",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_OverworkingDays_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemotePlans",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WorkingPlan = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_RemotePlans_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OverworkingDays_RequestId",
                table: "OverworkingDays",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RemotePlans_RequestId",
                table: "RemotePlans",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStatusesId",
                table: "Requests",
                column: "RequestStatusesId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestTypesId",
                table: "Requests",
                column: "RequestTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_VacationTypeId",
                table: "Requests",
                column: "VacationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OverworkingDays");

            migrationBuilder.DropTable(
                name: "RemotePlans");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "RequestTypes");

            migrationBuilder.DropTable(
                name: "VacationTypes");
        }
    }
}
