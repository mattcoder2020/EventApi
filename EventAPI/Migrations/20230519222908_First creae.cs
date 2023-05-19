using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventAPI.Migrations
{
    public partial class Firstcreae : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeZone = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    ContactPerson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ContactPerson", "Description", "EndDateTime", "Location", "StartDateTime", "TimeZone", "Title" },
                values: new object[] { 1, "John Doe1", "Description 1", new DateTime(2023, 5, 30, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6593), "location 1", new DateTime(2023, 5, 20, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6608), "timezone 1", "Title 1" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ContactPerson", "Description", "EndDateTime", "Location", "StartDateTime", "TimeZone", "Title" },
                values: new object[] { 2, "John Doe2", "Description 2", new DateTime(2023, 6, 4, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6612), "location 2", new DateTime(2023, 5, 20, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6613), "timezone 2", "Title 2" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ContactPerson", "Description", "EndDateTime", "Location", "StartDateTime", "TimeZone", "Title" },
                values: new object[] { 3, "John Doe3", "Description 3", new DateTime(2023, 6, 9, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6615), "location 3", new DateTime(2023, 5, 20, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6616), "timezone 1", "Title 3" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ContactPerson", "Description", "EndDateTime", "Location", "StartDateTime", "TimeZone", "Title" },
                values: new object[] { 4, "John Doe3", "Description 3", new DateTime(2023, 6, 9, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6618), "location 3", new DateTime(2023, 5, 20, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6619), "timezone 1", "Title 4" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ContactPerson", "Description", "EndDateTime", "Location", "StartDateTime", "TimeZone", "Title" },
                values: new object[] { 5, "John Doe1", "Description 1", new DateTime(2023, 5, 30, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6620), "location 1", new DateTime(2023, 5, 20, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6621), "timezone 1", "Title 5" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ContactPerson", "Description", "EndDateTime", "Location", "StartDateTime", "TimeZone", "Title" },
                values: new object[] { 6, "John Doe2", "Description 2", new DateTime(2023, 6, 4, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6623), "location 2", new DateTime(2023, 5, 20, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6624), "timezone 2", "Title 6" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ContactPerson", "Description", "EndDateTime", "Location", "StartDateTime", "TimeZone", "Title" },
                values: new object[] { 7, "John Doe3", "Description 3", new DateTime(2023, 6, 9, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6626), "location 3", new DateTime(2023, 5, 20, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6626), "timezone 1", "Title 7" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ContactPerson", "Description", "EndDateTime", "Location", "StartDateTime", "TimeZone", "Title" },
                values: new object[] { 8, "John Doe3", "Description 3", new DateTime(2023, 6, 9, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6628), "location 3", new DateTime(2023, 5, 20, 6, 29, 7, 440, DateTimeKind.Local).AddTicks(6629), "timezone 1", "Title 8" });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "Id", "EventId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "Id", "EventId", "UserId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "Id", "EventId", "UserId" },
                values: new object[] { 3, 1, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_EventId",
                table: "Participants",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
