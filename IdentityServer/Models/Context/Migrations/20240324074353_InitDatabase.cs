using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Models.Context.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identitydb");

            migrationBuilder.CreateTable(
                name: "ApiResource",
                schema: "identitydb",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Secret = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResource", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "ApiScope",
                schema: "identitydb",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScope", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "identitydb",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeResource",
                schema: "identitydb",
                columns: table => new
                {
                    ApiResourceIndex = table.Column<Guid>(type: "uuid", nullable: false),
                    ApiScopeIndex = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeResource", x => new { x.ApiScopeIndex, x.ApiResourceIndex });
                    table.ForeignKey(
                        name: "FK_ApiScopeResource_ApiResource_ApiResourceIndex",
                        column: x => x.ApiResourceIndex,
                        principalSchema: "identitydb",
                        principalTable: "ApiResource",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiScopeResource_ApiScope_ApiScopeIndex",
                        column: x => x.ApiScopeIndex,
                        principalSchema: "identitydb",
                        principalTable: "ApiScope",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientGrantType",
                schema: "identitydb",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uuid", nullable: false),
                    GrantType = table.Column<string>(type: "text", nullable: false),
                    ClientIndex = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Secret = table.Column<string>(type: "text", nullable: false),
                    ClientIndex = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_ApiScopeResource_ApiResourceIndex",
                schema: "identitydb",
                table: "ApiScopeResource",
                column: "ApiResourceIndex");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiScopeResource",
                schema: "identitydb");

            migrationBuilder.DropTable(
                name: "ClientGrantType",
                schema: "identitydb");

            migrationBuilder.DropTable(
                name: "ClientScope",
                schema: "identitydb");

            migrationBuilder.DropTable(
                name: "ClientSecret",
                schema: "identitydb");

            migrationBuilder.DropTable(
                name: "ApiResource",
                schema: "identitydb");

            migrationBuilder.DropTable(
                name: "ApiScope",
                schema: "identitydb");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "identitydb");
        }
    }
}
