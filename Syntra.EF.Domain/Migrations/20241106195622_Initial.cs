using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Syntra.EF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    GenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    NickName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskDescriptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    HourPrice = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Vat = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ContactId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Person_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    EmployeeNumber = table.Column<string>(type: "TEXT", nullable: false),
                    HireDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeRegistrations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DescriptionId = table.Column<string>(type: "TEXT", nullable: false),
                    ClientInfoId = table.Column<string>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeRegistrations_Clients_ClientInfoId",
                        column: x => x.ClientInfoId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeRegistrations_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeRegistrations_TaskDescriptions_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "TaskDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "BirthDate", "FirstName", "GenderId", "LastName", "MiddleName", "NickName" },
                values: new object[,]
                {
                    { "30d172efd236406baf16e6eb6c3eefd2", new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", 3, "Doe", null, null },
                    { "ebe1a3392e65415782e2e320b1afd058", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", 3, "Doe", null, null }
                });

            migrationBuilder.InsertData(
                table: "TaskDescriptions",
                columns: new[] { "Id", "Description", "HourPrice", "Name" },
                values: new object[,]
                {
                    { "3ea3b2bbdbaa4b97af58e7cc62b46f4d", "Fun with HTML and so", 85m, "Web design" },
                    { "5b4101cbf38e47409f414996e2ad45b6", "Fun with C# and so", 95m, "Web development" },
                    { "8bb065659acf49878f4a1a2c496a6506", "No fun at all...", 280m, "Writing documentation" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "EmployeeNumber", "EndDate", "HireDate" },
                values: new object[,]
                {
                    { "30d172efd236406baf16e6eb6c3eefd2", "124", null, new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "ebe1a3392e65415782e2e320b1afd058", "123", null, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ContactId",
                table: "Clients",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrations_ClientInfoId",
                table: "TimeRegistrations",
                column: "ClientInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrations_DescriptionId",
                table: "TimeRegistrations",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrations_EmployeeId",
                table: "TimeRegistrations",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeRegistrations");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "TaskDescriptions");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
