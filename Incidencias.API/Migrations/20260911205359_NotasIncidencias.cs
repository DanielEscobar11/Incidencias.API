using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Incidencias.API.Migrations
{
    /// <inheritdoc />
    public partial class NotasIncidencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IncidenciaId",
                table: "NotaIncidencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NotaIncidencias_IncidenciaId",
                table: "NotaIncidencias",
                column: "IncidenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaIncidencias_Incidencias_IncidenciaId",
                table: "NotaIncidencias",
                column: "IncidenciaId",
                principalTable: "Incidencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaIncidencias_Incidencias_IncidenciaId",
                table: "NotaIncidencias");

            migrationBuilder.DropIndex(
                name: "IX_NotaIncidencias_IncidenciaId",
                table: "NotaIncidencias");

            migrationBuilder.DropColumn(
                name: "IncidenciaId",
                table: "NotaIncidencias");
        }
    }
}
