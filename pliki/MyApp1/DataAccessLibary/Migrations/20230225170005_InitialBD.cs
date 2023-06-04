using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibary.Migrations
{
    /// <inheritdoc />
    public partial class InitialBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zamówienia",
                columns: table => new
                {
                    IDzamówienia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaKolumnyWTwojejBazieDanych2 = table.Column<int>(type: "int", nullable: false),
                    IDpracownika = table.Column<int>(type: "int", nullable: false),
                    DataZamówienia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataWymagana = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataWysyłki = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDspedytora = table.Column<int>(type: "int", nullable: false),
                    Fracht = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    NazwaOdbiorcy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresOdbiorcy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiastoOdbiorcy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionOdbiorcy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodPocztowyOdbiorcy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KrajOdbiorcy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamówienia", x => x.IDzamówienia);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zamówienia");
        }
    }
}
