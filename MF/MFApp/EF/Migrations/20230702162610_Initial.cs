using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MFApp.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nips",
                columns: table => new
                {
                    Nip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Regon = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    RestorationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasVirtualAccounts = table.Column<bool>(type: "bit", nullable: false),
                    StatusVat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Krs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestorationBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDenialBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLegalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovalBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    ResidenceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDenialDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nips", x => x.Nip);
                });

            migrationBuilder.CreateTable(
                name: "NipAccountNumber",
                columns: table => new
                {
                    Nip = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NipAccountNumber", x => new { x.Nip, x.Number });
                    table.ForeignKey(
                        name: "FK_NipAccountNumber_Nips_Nip",
                        column: x => x.Nip,
                        principalTable: "Nips",
                        principalColumn: "Nip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Nip = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => new { x.Nip, x.Type });
                    table.ForeignKey(
                        name: "FK_Person_Nips_Nip",
                        column: x => x.Nip,
                        principalTable: "Nips",
                        principalColumn: "Nip",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NipAccountNumber");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Nips");
        }
    }
}
