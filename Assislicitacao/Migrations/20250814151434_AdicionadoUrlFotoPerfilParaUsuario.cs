using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assislicitacao.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoUrlFotoPerfilParaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LICITACOES_EMPRESAS_EmpresaId",
                table: "LICITACOES");

            migrationBuilder.DropIndex(
                name: "IX_LICITACOES_EmpresaId",
                table: "LICITACOES");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "LICITACOES");

            migrationBuilder.AddColumn<string>(
                name: "USU_URL_IMAGEM",
                table: "USUARIOS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "USU_URL_IMAGEM",
                table: "USUARIOS");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "LICITACOES",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LICITACOES_EmpresaId",
                table: "LICITACOES",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_LICITACOES_EMPRESAS_EmpresaId",
                table: "LICITACOES",
                column: "EmpresaId",
                principalTable: "EMPRESAS",
                principalColumn: "EMP_ID");
        }
    }
}
