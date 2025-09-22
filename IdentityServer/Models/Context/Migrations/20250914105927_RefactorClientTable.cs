using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Models.Context.Migrations
{
    /// <inheritdoc />
    public partial class RefactorClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientGrantType",
                schema: "identitydb");

            migrationBuilder.DropTable(
                name: "ClientScope",
                schema: "identitydb");

            migrationBuilder.DropTable(
                name: "ClientSecret",
                schema: "identitydb");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                schema: "identitydb",
                table: "Client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string[]>(
                name: "ClientGrantTypes",
                schema: "identitydb",
                table: "Client",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "ClientSecrets",
                schema: "identitydb",
                table: "Client",
                type: "jsonb",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientApiScope",
                schema: "identitydb",
                columns: table => new
                {
                    ClientIndex = table.Column<Guid>(type: "uuid", nullable: false),
                    ApiScopeIndex = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientApiScope", x => new { x.ClientIndex, x.ApiScopeIndex });
                    table.ForeignKey(
                        name: "FK_ClientApiScope_ApiScope_ApiScopeIndex",
                        column: x => x.ApiScopeIndex,
                        principalSchema: "identitydb",
                        principalTable: "ApiScope",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientApiScope_Client_ClientIndex",
                        column: x => x.ClientIndex,
                        principalSchema: "identitydb",
                        principalTable: "Client",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientApiScope_ApiScopeIndex",
                schema: "identitydb",
                table: "ClientApiScope",
                column: "ApiScopeIndex");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientApiScope",
                schema: "identitydb");

            migrationBuilder.DropColumn(
                name: "ClientGrantTypes",
                schema: "identitydb",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientSecrets",
                schema: "identitydb",
                table: "Client");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                schema: "identitydb",
                table: "Client",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ClientGrantType",
                schema: "identitydb",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientIndex = table.Column<Guid>(type: "uuid", nullable: false),
                    GrantType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGrantType", x => x.Index);
                    table.ForeignKey(
                        name: "FK_ClientGrantType_Client_ClientIndex",
                        column: x => x.ClientIndex,
                        principalSchema: "identitydb",
                        principalTable: "Client",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientScope",
                schema: "identitydb",
                columns: table => new
                {
                    ClientIndex = table.Column<Guid>(type: "uuid", nullable: false),
                    ApiScopeIndex = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScope", x => new { x.ClientIndex, x.ApiScopeIndex });
                    table.ForeignKey(
                        name: "FK_ClientScope_ApiScope_ApiScopeIndex",
                        column: x => x.ApiScopeIndex,
                        principalSchema: "identitydb",
                        principalTable: "ApiScope",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientScope_Client_ClientIndex",
                        column: x => x.ClientIndex,
                        principalSchema: "identitydb",
                        principalTable: "Client",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSecret",
                schema: "identitydb",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientIndex = table.Column<Guid>(type: "uuid", nullable: false),
                    Secret = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecret", x => x.Index);
                    table.ForeignKey(
                        name: "FK_ClientSecret_Client_ClientIndex",
                        column: x => x.ClientIndex,
                        principalSchema: "identitydb",
                        principalTable: "Client",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientGrantType_ClientIndex",
                schema: "identitydb",
                table: "ClientGrantType",
                column: "ClientIndex");

            migrationBuilder.CreateIndex(
                name: "IX_ClientScope_ApiScopeIndex",
                schema: "identitydb",
                table: "ClientScope",
                column: "ApiScopeIndex");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSecret_ClientIndex",
                schema: "identitydb",
                table: "ClientSecret",
                column: "ClientIndex");
        }
    }
}
