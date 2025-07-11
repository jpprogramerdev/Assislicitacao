using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assislicitacao.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionamentoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "USU_TPU_ID",
                table: "USUARIOS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_USU_TPU_ID",
                table: "USUARIOS",
                column: "USU_TPU_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIOS_TIPOS_USUARIO_USU_TPU_ID",
                table: "USUARIOS",
                column: "USU_TPU_ID",
                principalTable: "TIPOS_USUARIO",
                principalColumn: "TPU_ID",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIOS_TIPOS_USUARIO_USU_TPU_ID",
                table: "USUARIOS");

            migrationBuilder.DropIndex(
                name: "IX_USUARIOS_USU_TPU_ID",
                table: "USUARIOS");

            migrationBuilder.DropColumn(
                name: "USU_TPU_ID",
                table: "USUARIOS");
        }
    }
}
