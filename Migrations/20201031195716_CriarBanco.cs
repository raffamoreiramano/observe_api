using Microsoft.EntityFrameworkCore.Migrations;

namespace Observe.Migrations
{
    public partial class CriarBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CID = table.Column<string>(maxLength: 128, nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<int>(nullable: false),
                    CRM = table.Column<string>(maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medico_Usuario_UID",
                        column: x => x.UID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<int>(nullable: false),
                    Idade = table.Column<int>(nullable: false),
                    Doencas = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    Alergias = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    Remedios = table.Column<string>(type: "NVARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Paciente_Usuario_UID",
                        column: x => x.UID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MID = table.Column<int>(nullable: false),
                    PID = table.Column<int>(nullable: false),
                    Remedios = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Receita_Medico_MID",
                        column: x => x.MID,
                        principalTable: "Medico",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receita_Paciente_PID",
                        column: x => x.PID,
                        principalTable: "Paciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medico_UID",
                table: "Medico",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_UID",
                table: "Paciente",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_MID",
                table: "Receita",
                column: "MID");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_PID",
                table: "Receita",
                column: "PID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
