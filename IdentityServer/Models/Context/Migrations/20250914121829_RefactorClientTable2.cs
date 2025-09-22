using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Models.Context.Migrations
{
    /// <inheritdoc />
    public partial class RefactorClientTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "ClientSecrets",
                schema: "identitydb",
                table: "Client",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string[]),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "ClientGrantTypes",
                schema: "identitydb",
                table: "Client",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string[]),
                oldType: "jsonb",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string[]>(
                name: "ClientSecrets",
                schema: "identitydb",
                table: "Client",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string[]>(
                name: "ClientGrantTypes",
                schema: "identitydb",
                table: "Client",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "jsonb");
        }
    }
}
