using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Models.Context.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "identitydb",
                table: "ApiResource",
                columns: new[] { "Index", "Name", "Secret" },
                values: new object[,]
                {
                    { new Guid("67756a77-a691-4597-91f5-573e2db344dc"), "web.api", new Guid("9070e5fe-db28-4d9a-b19b-31a77592be20") },
                });

            migrationBuilder.InsertData(
                schema: "identitydb",
                table: "ApiScope",
                columns: new[] { "Index", "DisplayName", "Name" },
                values: new object[,]
                {
                    { new Guid("d45fffbd-4e4b-4005-b00c-cad3e63c02cb"), "Web App Resoure", "web.scope" },
                    { new Guid("d12a09e1-781a-4cfb-93e5-5cc5bc0ae926"), "Offline Access", "offline_access" }
                });

            migrationBuilder.InsertData(
                schema: "identitydb",
                table: "Client",
                columns: new[] { "Index", "ClientId" },
                values: new object[,]
                {
                    { new Guid("c822068a-2b09-47d6-87f9-5fae675919bb"), "webapp" }
                });

            migrationBuilder.InsertData(
                schema: "identitydb",
                table: "ClientGrantType",
                columns: new[] { "Index", "GrantType", "ClientIndex" },
                values: new object[,]
                {
                    { new Guid("dde251e5-a57e-4883-8a57-aaca03ea0488"), "password", new Guid("c822068a-2b09-47d6-87f9-5fae675919bb") }
                });

            migrationBuilder.InsertData(
                schema: "identitydb",
                table: "ClientScope",
                columns: new[] { "ClientIndex", "ApiScopeIndex" },
                values: new object[,]
                {
                    { new Guid("c822068a-2b09-47d6-87f9-5fae675919bb"), new Guid("d45fffbd-4e4b-4005-b00c-cad3e63c02cb") },
                    { new Guid("c822068a-2b09-47d6-87f9-5fae675919bb"), new Guid("d12a09e1-781a-4cfb-93e5-5cc5bc0ae926") }
                });

            migrationBuilder.InsertData(
                schema: "identitydb",
                table: "ClientSecret",
                columns: new[] { "Index", "Secret", "ClientIndex" },
                values: new object[,]
                {
                    { new Guid("f00e2dcf-1a74-4fb8-9fcb-4dbcb8b0cea3"), "secret", new Guid("c822068a-2b09-47d6-87f9-5fae675919bb") },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identitydb",
                table: "ApiResource",
                keyColumn: "Index",
                keyValue: new Guid("67756a77-a691-4597-91f5-573e2db344dc"));

            migrationBuilder.DeleteData(
                schema: "identitydb",
                table: "ApiResource",
                keyColumn: "Index",
                keyValue: new Guid("adffadc0-b2a8-4bf3-9203-8d6251d36092"));

            migrationBuilder.DeleteData(
                schema: "identitydb",
                table: "ApiScope",
                keyColumn: "Index",
                keyValue: new Guid("4c4da98c-cec2-4dd6-920e-b5bc812189bc"));

            migrationBuilder.DeleteData(
                schema: "identitydb",
                table: "ApiScope",
                keyColumn: "Index",
                keyValue: new Guid("d45fffbd-4e4b-4005-b00c-cad3e63c02cb"));

            migrationBuilder.DeleteData(
                schema: "identitydb",
                table: "Client",
                keyColumn: "Index",
                keyValue: new Guid("79fc80be-e2ee-495d-b1b7-c59d93c26833"));

            migrationBuilder.DeleteData(
                schema: "identitydb",
                table: "Client",
                keyColumn: "Index",
                keyValue: new Guid("c822068a-2b09-47d6-87f9-5fae675919bb"));
        }
    }
}
