using System;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

#nullable disable

namespace Projekt_SBD.Migrations
{
    /// <inheritdoc />
    public partial class InitSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    Id_klienta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.Id_klienta);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    Id_Pracownika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.Id_Pracownika);
                });

            migrationBuilder.CreateTable(
                name: "Przedmioty",
                columns: table => new
                {
                    Id_Przedmiotu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dostepnosc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmioty", x => x.Id_Przedmiotu);
                });

            migrationBuilder.CreateTable(
                name: "Raporty",
                columns: table => new
                {
                    Id_Raportu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typ_Raportu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Generacji = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Pracownika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raporty", x => x.Id_Raportu);
                    table.ForeignKey(
                        name: "FK_Raporty_Pracownicy_Id_Pracownika",
                        column: x => x.Id_Pracownika,
                        principalTable: "Pracownicy",
                        principalColumn: "Id_Pracownika",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wypozyczenia",
                columns: table => new
                {
                    Id_Wypozyczenia = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Klienta = table.Column<int>(type: "int", nullable: false),
                    Id_Przedmiotu = table.Column<int>(type: "int", nullable: false),
                    Data_Wypozyczenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_zwrotu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wypozyczenia", x => x.Id_Wypozyczenia);
                    table.ForeignKey(
                        name: "FK_Wypozyczenia_Klienci_Id_Klienta",
                        column: x => x.Id_Klienta,
                        principalTable: "Klienci",
                        principalColumn: "Id_klienta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wypozyczenia_Przedmioty_Id_Przedmiotu",
                        column: x => x.Id_Przedmiotu,
                        principalTable: "Przedmioty",
                        principalColumn: "Id_Przedmiotu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zwroty",
                columns: table => new
                {
                    Id_Zwrotu = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Wypozyczenia = table.Column<long>(type: "bigint", nullable: false),
                    Data_zwrotu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stan_Przedmiotu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uwagi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zwroty", x => x.Id_Zwrotu);
                    table.ForeignKey(
                        name: "FK_Zwroty_Wypozyczenia_Id_Wypozyczenia",
                        column: x => x.Id_Wypozyczenia,
                        principalTable: "Wypozyczenia",
                        principalColumn: "Id_Wypozyczenia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Raporty_Id_Pracownika",
                table: "Raporty",
                column: "Id_Pracownika");

            migrationBuilder.CreateIndex(
                name: "IX_Wypozyczenia_Id_Klienta",
                table: "Wypozyczenia",
                column: "Id_Klienta");

            migrationBuilder.CreateIndex(
                name: "IX_Wypozyczenia_Id_Przedmiotu",
                table: "Wypozyczenia",
                column: "Id_Przedmiotu");

            migrationBuilder.CreateIndex(
                name: "IX_Zwroty_Id_Wypozyczenia",
                table: "Zwroty",
                column: "Id_Wypozyczenia",
                unique: true);
            migrationBuilder.Sql(@"
            CREATE PROCEDURE DodajWypozyczenie
                @Id_Klienta NUMERIC(38, 0),
                @Id_Przedmiotu NUMERIC(38, 0),
                @DataWypozyczenia SMALLDATETIME
            AS
            BEGIN
                INSERT INTO Wypozyczenia (Id_Klienta, Id_Przedmiotu, Data_Wypozyczenia)
                VALUES (@Id_Klienta, @Id_Przedmiotu, @DataWypozyczenia);

                UPDATE Przedmioty
                SET Dostepnosc = '0'
                WHERE Id_Przedmiotu = @Id_Przedmiotu;
            END
            ");
            migrationBuilder.Sql(@"CREATE PROCEDURE ZwrocPrzedmiot
                @Id_Wypozyczenia NUMERIC(38, 0),
                @Data_zwrotu SMALLDATETIME,
                @Stan_Przedmiotu VARCHAR(20),
                @Uwagi VARCHAR(200)
            AS
            BEGIN
                INSERT INTO Zwroty(Id_Wypozyczenia, Data_zwrotu, Stan_Przedmiotu, Uwagi)
                VALUES(@Id_Wypozyczenia, @Data_zwrotu, @Stan_Przedmiotu, @Uwagi);

                UPDATE Wypozyczenia
                SET Data_zwrotu = @Data_zwrotu, Status = 'Zakonczone'
                WHERE Id_Wypozyczenia = @Id_Wypozyczenia;
                END;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raporty");

            migrationBuilder.DropTable(
                name: "Zwroty");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Wypozyczenia");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Przedmioty");
        }
    }
}
