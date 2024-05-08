using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBiblioteca.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LibrosME",
                columns: table => new
                {
                    IDLibro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreAutor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoAutor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tematica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TituloLibro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Editorial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibrosME", x => x.IDLibro);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibrosME");
        }
    }
}
