using Microsoft.EntityFrameworkCore.Migrations;

namespace Observe.Migrations
{
    public partial class GerarIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Paciente_UID",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Medico_UID",
                table: "Medico");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CID",
                table: "Usuario",
                column: "CID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_UID",
                table: "Paciente",
                column: "UID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medico_UID",
                table: "Medico",
                column: "UID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_CID",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_UID",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Medico_UID",
                table: "Medico");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_UID",
                table: "Paciente",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_UID",
                table: "Medico",
                column: "UID");
        }
    }
}
