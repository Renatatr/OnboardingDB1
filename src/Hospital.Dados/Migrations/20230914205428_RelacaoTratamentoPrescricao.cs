using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Dados.Migrations
{
    /// <inheritdoc />
    public partial class RelacaoTratamentoPrescricao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_TratamentoId",
                table: "Prescricoes",
                column: "TratamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescricoes_Tratamentos_TratamentoId",
                table: "Prescricoes",
                column: "TratamentoId",
                principalTable: "Tratamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescricoes_Tratamentos_TratamentoId",
                table: "Prescricoes");

            migrationBuilder.DropIndex(
                name: "IX_Prescricoes_TratamentoId",
                table: "Prescricoes");
        }
    }
}
