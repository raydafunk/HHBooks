using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HHBooks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a83529aa-bf63-414d-8d6f-a8dcf25ffc13", null, "User", "USER" },
                    { "f1986e71-d5d2-442f-a6c1-a9aa935cc3b2", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0e2f0b81-f935-4761-8585-b1cf3e2c6827", 0, "f281d689-99ce-4498-864d-a60f440501a8", "adminhhbookstore.com", false, "System", "Admin", false, null, "ADMINHHBOOKSTORE.COM", "ADMINHHBOOKSTORE.COM", "AQAAAAIAAYagAAAAEDiT3FCXhTF6kp4XNG78DxUOm3/xJ0GR0cgSp1xE2oajFjrQkC7S9/SJjw7X/kL2iQ==", null, false, "0c6a4162-92ab-4132-827b-4d277559993f", false, "adminhhbookstore.com" },
                    { "333bee64-2460-4a6e-a275-6a67f88a714b", 0, "8b0dce5a-3b88-49c0-b37b-0c3164711cc8", "userhhbookstore.com", false, "System", "User", false, null, "USERHHBOOKSTORE.COM", "USERHHBOOKSTORE.COM", "AQAAAAIAAYagAAAAEOGE7ignM946Yfn5DGRnxqanQADhTmU4NSQEfoKiBDZY6W3HPsaU3SINjH/v7Uwiyg==", null, false, "a2a7a699-0560-4dcd-bd3d-0ca367a85944", false, "userhhbookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f1986e71-d5d2-442f-a6c1-a9aa935cc3b2", "0e2f0b81-f935-4761-8585-b1cf3e2c6827" },
                    { "a83529aa-bf63-414d-8d6f-a8dcf25ffc13", "333bee64-2460-4a6e-a275-6a67f88a714b" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f1986e71-d5d2-442f-a6c1-a9aa935cc3b2", "0e2f0b81-f935-4761-8585-b1cf3e2c6827" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a83529aa-bf63-414d-8d6f-a8dcf25ffc13", "333bee64-2460-4a6e-a275-6a67f88a714b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a83529aa-bf63-414d-8d6f-a8dcf25ffc13");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1986e71-d5d2-442f-a6c1-a9aa935cc3b2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e2f0b81-f935-4761-8585-b1cf3e2c6827");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "333bee64-2460-4a6e-a275-6a67f88a714b");
        }
    }
}
