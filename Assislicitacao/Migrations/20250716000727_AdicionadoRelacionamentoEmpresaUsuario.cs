using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assislicitacao.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoRelacionamentoEmpresaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpresaUsuario",
                columns: table => new
                {
                    EmpresasVinculadasId = table.Column<int>(type: "int", nullable: false),
                    UsusariosVinculadosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaUsuario", x => new { x.EmpresasVinculadasId, x.UsusariosVinculadosId });
                    table.ForeignKey(
                        name: "FK_EmpresaUsuario_EMPRESAS_EmpresasVinculadasId",
                        column: x => x.EmpresasVinculadasId,
                        principalTable: "EMPRESAS",
                        principalColumn: "EMP_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaUsuario_USUARIOS_UsusariosVinculadosId",
                        column: x => x.UsusariosVinculadosId,
                        principalTable: "USUARIOS",
                        principalColumn: "USU_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaUsuario_UsusariosVinculadosId",
                table: "EmpresaUsuario",
                column: "UsusariosVinculadosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpresaUsuario");
        }
    }
}
