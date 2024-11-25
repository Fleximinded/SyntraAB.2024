using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Syntra.EF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: "30d172efd236406baf16e6eb6c3eefd2");

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: "ebe1a3392e65415782e2e320b1afd058");

            migrationBuilder.DeleteData(
                table: "TaskDescriptions",
                keyColumn: "Id",
                keyValue: "3ea3b2bbdbaa4b97af58e7cc62b46f4d");

            migrationBuilder.DeleteData(
                table: "TaskDescriptions",
                keyColumn: "Id",
                keyValue: "5b4101cbf38e47409f414996e2ad45b6");

            migrationBuilder.DeleteData(
                table: "TaskDescriptions",
                keyColumn: "Id",
                keyValue: "8bb065659acf49878f4a1a2c496a6506");

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: "30d172efd236406baf16e6eb6c3eefd2");

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: "ebe1a3392e65415782e2e320b1afd058");

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "BirthDate", "FirstName", "GenderId", "LastName", "MiddleName", "NickName" },
                values: new object[,]
                {
                    { "bcde72f6281d4bd8a2c26d20e9c426a4", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", 3, "Doe", null, null },
                    { "fd9713ca181f42b0b240748513a2bed1", new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", 3, "Doe", null, null }
                });

            migrationBuilder.InsertData(
                table: "TaskDescriptions",
                columns: new[] { "Id", "Description", "HourPrice", "Name" },
                values: new object[,]
                {
                    { "2cac6daa1b094c2e81488533a796e10b", "Fun with C# and so", 95m, "Web development" },
                    { "c349cc5579b34813a6d78b210004969d", "Fun with HTML and so", 85m, "Web design" },
                    { "ced815152e2d4096836c64d164a0f28b", "No fun at all...", 280m, "Writing documentation" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "EmployeeNumber", "EndDate", "HireDate" },
                values: new object[,]
                {
                    { "bcde72f6281d4bd8a2c26d20e9c426a4", "123", null, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "fd9713ca181f42b0b240748513a2bed1", "124", null, new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: "bcde72f6281d4bd8a2c26d20e9c426a4");

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: "fd9713ca181f42b0b240748513a2bed1");

            migrationBuilder.DeleteData(
                table: "TaskDescriptions",
                keyColumn: "Id",
                keyValue: "2cac6daa1b094c2e81488533a796e10b");

            migrationBuilder.DeleteData(
                table: "TaskDescriptions",
                keyColumn: "Id",
                keyValue: "c349cc5579b34813a6d78b210004969d");

            migrationBuilder.DeleteData(
                table: "TaskDescriptions",
                keyColumn: "Id",
                keyValue: "ced815152e2d4096836c64d164a0f28b");

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: "bcde72f6281d4bd8a2c26d20e9c426a4");

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: "fd9713ca181f42b0b240748513a2bed1");

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
        }
    }
}
