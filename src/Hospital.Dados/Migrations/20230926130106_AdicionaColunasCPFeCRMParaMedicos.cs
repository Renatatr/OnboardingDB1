using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaColunasCPFeCRMParaMedicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Medicos",
                type: "varchar(14)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CRM",
                table: "Medicos",
                type: "varchar(8)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "CRM",
                table: "Medicos");
        }
    }
}
