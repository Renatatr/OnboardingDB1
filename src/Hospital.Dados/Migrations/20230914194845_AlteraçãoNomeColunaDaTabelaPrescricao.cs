using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AlteraçãoNomeColunaDaTabelaPrescricao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MedicamentoId",
                table: "Prescricoes",
                newName: "TratamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TratamentoId",
                table: "Prescricoes",
                newName: "MedicamentoId");
        }
    }
}
