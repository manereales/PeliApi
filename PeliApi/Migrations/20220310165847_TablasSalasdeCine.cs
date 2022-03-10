using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliApi.Migrations
{
    public partial class TablasSalasdeCine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaDeCines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaDeCines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeliculasSalasDeCines",
                columns: table => new
                {
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    SalaDeCineId = table.Column<int>(type: "int", nullable: false),
                    PeliculasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasSalasDeCines", x => new { x.PeliculaId, x.SalaDeCineId });
                    table.ForeignKey(
                        name: "FK_PeliculasSalasDeCines_Peliculas_PeliculasId",
                        column: x => x.PeliculasId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeliculasSalasDeCines_SalaDeCines_SalaDeCineId",
                        column: x => x.SalaDeCineId,
                        principalTable: "SalaDeCines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 7,
                column: "FechaEstreno",
                value: new DateTime(2022, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasDeCines_PeliculasId",
                table: "PeliculasSalasDeCines",
                column: "PeliculasId");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasDeCines_SalaDeCineId",
                table: "PeliculasSalasDeCines",
                column: "SalaDeCineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculasSalasDeCines");

            migrationBuilder.DropTable(
                name: "SalaDeCines");

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 7,
                column: "FechaEstreno",
                value: new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
