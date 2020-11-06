using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Observe.Migrations
{
    public partial class IdadeParaNascimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Paciente");

            migrationBuilder.AddColumn<DateTime>(
                name: "Nascimento",
                table: "Paciente",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nascimento",
                table: "Paciente");

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
