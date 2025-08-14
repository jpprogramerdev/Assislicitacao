using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assislicitacao.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoValorGanho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LCEM_VALOR_GANHO",
                table: "LICITACOES_EMPRESAS",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LCEM_VALOR_GANHO",
                table: "LICITACOES_EMPRESAS");
        }
    }
}
