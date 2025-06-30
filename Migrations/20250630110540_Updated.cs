using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Inn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Inn);
                });

            migrationBuilder.CreateTable(
                name: "Founders",
                columns: table => new
                {
                    Inn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Founders", x => x.Inn);
                });

            migrationBuilder.CreateTable(
                name: "ClientFounder",
                columns: table => new
                {
                    ClientInn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoundersInn = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFounder", x => new { x.ClientInn, x.FoundersInn });
                    table.ForeignKey(
                        name: "FK_ClientFounder_Clients_ClientInn",
                        column: x => x.ClientInn,
                        principalTable: "Clients",
                        principalColumn: "Inn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientFounder_Founders_FoundersInn",
                        column: x => x.FoundersInn,
                        principalTable: "Founders",
                        principalColumn: "Inn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientFounder_FoundersInn",
                table: "ClientFounder",
                column: "FoundersInn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientFounder");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Founders");
        }
    }
}
