using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Dados.Migrations
{
    /// <inheritdoc />
    public partial class TrocarIdadePorDataNascimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Pacientes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Pacientes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Pacientes");

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
