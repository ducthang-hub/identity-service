using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Models.Context.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "identitydb",
                table: "AspNetUsers",
                columns: new[] { "Index", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "Id", "IsActive", "IsDeleted", "LockoutEnabled", "LockoutEnd", "ModifiedByUserId", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Provider", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cb3dc6a4-4564-4312-82d0-326ff5ba6c39", 0, "94a63d1f-7a86-4fce-a9cf-16179f93fdd3", null, null, true, "eb1b4c78-9eed-4a43-90e4-867e8770769b", true, false, false, null, null, null, null, null, "marist0103_pw", null, true, 0, "edf6ba1e-986b-4e3d-b7f3-78d20b77d70b", false, "marist" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identitydb",
                table: "AspNetUsers",
                keyColumn: "Index",
                keyValue: "cb3dc6a4-4564-4312-82d0-326ff5ba6c39");
        }
    }
}
