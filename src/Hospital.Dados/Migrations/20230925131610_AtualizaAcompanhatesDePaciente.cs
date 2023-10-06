using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaAcompanhatesDePaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Acompanhante",
                table: "Pacientes",
                type: "varchar(80)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pacientes",
                keyColumn: "Acompanhante",
                keyValue: null,
                column: "Acompanhante",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Acompanhante",
                table: "Pacientes",
                type: "varchar(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
