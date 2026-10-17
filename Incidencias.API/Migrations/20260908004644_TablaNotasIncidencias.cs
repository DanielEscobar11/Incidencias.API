using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Incidencias.API.Migrations
{
    /// <inheritdoc />
    public partial class TablaNotasIncidencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotaIncidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaNota = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaIncidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaIncidencias_AspNetUsers_CreadoPorId",
                        column: x => x.CreadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotaIncidencias_CreadoPorId",
                table: "NotaIncidencias",
                column: "CreadoPorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaIncidencias");
        }
    }
}
