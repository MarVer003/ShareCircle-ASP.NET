using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareCircle.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skupina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeSkupine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumNastanka = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skupina", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Uporabnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priimek = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumPrijave = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uporabnik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ClanSkupine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_uporabnika = table.Column<int>(type: "int", nullable: false),
                    ID_skupine = table.Column<int>(type: "int", nullable: false),
                    DatumPridruzitve = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanSkupine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClanSkupine_Skupina_ID_skupine",
                        column: x => x.ID_skupine,
                        principalTable: "Skupina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClanSkupine_Uporabnik_ID_uporabnika",
                        column: x => x.ID_uporabnika,
                        principalTable: "Uporabnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strosek",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_placnika = table.Column<int>(type: "int", nullable: false),
                    ID_skupine = table.Column<int>(type: "int", nullable: false),
                    StevilkaStroska = table.Column<int>(type: "int", nullable: false),
                    CelotniZnesek = table.Column<float>(type: "real", nullable: false),
                    DatumPlacila = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strosek", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Strosek_Skupina_ID_skupine",
                        column: x => x.ID_skupine,
                        principalTable: "Skupina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Strosek_Uporabnik_ID_placnika",
                        column: x => x.ID_placnika,
                        principalTable: "Uporabnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vracilo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_dolznika = table.Column<int>(type: "int", nullable: false),
                    ID_skupine = table.Column<int>(type: "int", nullable: false),
                    StevilkaVracila = table.Column<int>(type: "int", nullable: false),
                    ZnesekVracila = table.Column<float>(type: "real", nullable: false),
                    DatumVracila = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vracilo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vracilo_Skupina_ID_skupine",
                        column: x => x.ID_skupine,
                        principalTable: "Skupina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vracilo_Uporabnik_ID_dolznika",
                        column: x => x.ID_dolznika,
                        principalTable: "Uporabnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RazdelitevStroska",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_stroska = table.Column<int>(type: "int", nullable: false),
                    ID_dolznika = table.Column<int>(type: "int", nullable: false),
                    Znesek = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RazdelitevStroska", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RazdelitevStroska_Strosek_ID_stroska",
                        column: x => x.ID_stroska,
                        principalTable: "Strosek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RazdelitevStroska_Uporabnik_ID_dolznika",
                        column: x => x.ID_dolznika,
                        principalTable: "Uporabnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClanSkupine_ID_skupine",
                table: "ClanSkupine",
                column: "ID_skupine");

            migrationBuilder.CreateIndex(
                name: "IX_ClanSkupine_ID_uporabnika",
                table: "ClanSkupine",
                column: "ID_uporabnika");

            migrationBuilder.CreateIndex(
                name: "IX_RazdelitevStroska_ID_dolznika",
                table: "RazdelitevStroska",
                column: "ID_dolznika");

            migrationBuilder.CreateIndex(
                name: "IX_RazdelitevStroska_ID_stroska",
                table: "RazdelitevStroska",
                column: "ID_stroska");

            migrationBuilder.CreateIndex(
                name: "IX_Strosek_ID_placnika",
                table: "Strosek",
                column: "ID_placnika");

            migrationBuilder.CreateIndex(
                name: "IX_Strosek_ID_skupine",
                table: "Strosek",
                column: "ID_skupine");

            migrationBuilder.CreateIndex(
                name: "IX_Vracilo_ID_dolznika",
                table: "Vracilo",
                column: "ID_dolznika");

            migrationBuilder.CreateIndex(
                name: "IX_Vracilo_ID_skupine",
                table: "Vracilo",
                column: "ID_skupine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanSkupine");

            migrationBuilder.DropTable(
                name: "RazdelitevStroska");

            migrationBuilder.DropTable(
                name: "Vracilo");

            migrationBuilder.DropTable(
                name: "Strosek");

            migrationBuilder.DropTable(
                name: "Skupina");

            migrationBuilder.DropTable(
                name: "Uporabnik");
        }
    }
}
