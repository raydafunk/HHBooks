using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHBooks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e2f0b81-f935-4761-8585-b1cf3e2c6827",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b777eb7c-9530-4a78-88fc-47f302d9f881", "admin@hhbookstore.com", "ADMIN@HHBOOKSTORE.COM", "ADMIN@HHBOOKSTORE.COM", "AQAAAAIAAYagAAAAECHaSeb+ZMB5EJa8hhxjz5O0z81QxTVyDmHbzpJo2ME/yfUf7wjjxpftC1/i6BLi9g==", "e20de294-05f5-4557-8555-0548ddf9e1f1", "admin@hhbookstore.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "333bee64-2460-4a6e-a275-6a67f88a714b",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "7b852429-377d-417c-a714-c4e04da71807", "user@hhbookstore.com", "USER@HHBOOKSTORE.COM", "USERHH@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEJwdfZN60Qp4Re7Wgh/GYrRwCFRwsH9OEGwYVTIZZXyod6+El/GFTGL18a1N3mDzBw==", "457979ac-7fa0-47e7-8ee6-0a3020d8fb4d", "user@hhbookstore.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e2f0b81-f935-4761-8585-b1cf3e2c6827",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f281d689-99ce-4498-864d-a60f440501a8", "adminhhbookstore.com", "ADMINHHBOOKSTORE.COM", "ADMINHHBOOKSTORE.COM", "AQAAAAIAAYagAAAAEDiT3FCXhTF6kp4XNG78DxUOm3/xJ0GR0cgSp1xE2oajFjrQkC7S9/SJjw7X/kL2iQ==", "0c6a4162-92ab-4132-827b-4d277559993f", "adminhhbookstore.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "333bee64-2460-4a6e-a275-6a67f88a714b",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "8b0dce5a-3b88-49c0-b37b-0c3164711cc8", "userhhbookstore.com", "USERHHBOOKSTORE.COM", "USERHHBOOKSTORE.COM", "AQAAAAIAAYagAAAAEOGE7ignM946Yfn5DGRnxqanQADhTmU4NSQEfoKiBDZY6W3HPsaU3SINjH/v7Uwiyg==", "a2a7a699-0560-4dcd-bd3d-0ca367a85944", "userhhbookstore.com" });
        }
    }
}
