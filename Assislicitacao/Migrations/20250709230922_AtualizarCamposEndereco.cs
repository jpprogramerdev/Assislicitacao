using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assislicitacao.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarCamposEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "END_COMPLEMENTO",
                table: "ENDERECOS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "END_COMPLEMENTO",
                table: "ENDERECOS");
        }
    }
}
