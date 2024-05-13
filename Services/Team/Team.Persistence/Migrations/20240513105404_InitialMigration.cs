using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpeningTimeSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningTimeSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    AmountOfAppointments = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHoursTimeSlots",
                columns: table => new
                {
                    OpeningHoursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpeningTimeSlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHoursTimeSlots", x => new { x.OpeningHoursId, x.OpeningTimeSlotId });
                    table.ForeignKey(
                        name: "FK_OpeningHoursTimeSlots_OpeningHours_OpeningHoursId",
                        column: x => x.OpeningHoursId,
                        principalTable: "OpeningHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpeningHoursTimeSlots_OpeningTimeSlots_OpeningTimeSlotId",
                        column: x => x.OpeningTimeSlotId,
                        principalTable: "OpeningTimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamOpeningHours",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpeningHoursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamOpeningHours", x => new { x.TeamId, x.OpeningHoursId });
                    table.ForeignKey(
                        name: "FK_TeamOpeningHours_OpeningHours_OpeningHoursId",
                        column: x => x.OpeningHoursId,
                        principalTable: "OpeningHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamOpeningHours_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHoursTimeSlots_OpeningTimeSlotId",
                table: "OpeningHoursTimeSlots",
                column: "OpeningTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamOpeningHours_OpeningHoursId",
                table: "TeamOpeningHours",
                column: "OpeningHoursId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningHoursTimeSlots");

            migrationBuilder.DropTable(
                name: "TeamOpeningHours");

            migrationBuilder.DropTable(
                name: "OpeningTimeSlots");

            migrationBuilder.DropTable(
                name: "OpeningHours");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
