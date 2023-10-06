using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaColunaCPFParaPacientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Pacientes",
                type: "varchar(14)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Pacientes");
        }
    }
}
