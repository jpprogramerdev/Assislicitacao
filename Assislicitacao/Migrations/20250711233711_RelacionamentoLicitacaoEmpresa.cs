using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assislicitacao.Migrations {
    /// <inheritdoc />
    public partial class RelacionamentoLicitacaoEmpresa : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "EmpresaLicitacao");

            migrationBuilder.DropColumn(
                name: "LCT_CONFIRMACAO_PARTICIPACAO",
                table: "LICITACOES");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "LICITACOES",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
    name: "LICITACOES_EMPRESAS",
    columns: table => new {
        LCEM_ID = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        LCEM_LCT_ID = table.Column<int>(type: "int", nullable: false),
        LCEM_EMP_ID = table.Column<int>(type: "int", nullable: false),
        LCEM_CONFIRMACAO_PARTICIPACAO = table.Column<bool>(type: "bit", nullable: false)
    },
    constraints: table => {
        table.PrimaryKey("PK_LICITACOES_EMPRESAS", x => x.LCEM_ID);
        table.ForeignKey(
            name: "FK_LICITACOES_EMPRESAS_EMPRESAS_LCEM_EMP_ID",
            column: x => x.LCEM_EMP_ID,
            principalTable: "EMPRESAS",
            principalColumn: "EMP_ID",
            onDelete: ReferentialAction.Cascade); // pode manter Cascade aqui

        table.ForeignKey(
            name: "FK_LICITACOES_EMPRESAS_LICITACOES_LCEM_LCT_ID",
            column: x => x.LCEM_LCT_ID,
            principalTable: "LICITACOES",
            principalColumn: "LCT_ID",
            onDelete: ReferentialAction.NoAction); // ALTERE AQUI
    });


            migrationBuilder.CreateIndex(
                name: "IX_LICITACOES_EmpresaId",
                table: "LICITACOES",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_LICITACOES_EMPRESAS_LCEM_EMP_ID",
                table: "LICITACOES_EMPRESAS",
                column: "LCEM_EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LICITACOES_EMPRESAS_LCEM_LCT_ID",
                table: "LICITACOES_EMPRESAS",
                column: "LCEM_LCT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LICITACOES_EMPRESAS_EmpresaId",
                table: "LICITACOES",
                column: "EmpresaId",
                principalTable: "EMPRESAS",
                principalColumn: "EMP_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_LICITACOES_EMPRESAS_EmpresaId",
                table: "LICITACOES");

            migrationBuilder.DropTable(
                name: "LICITACOES_EMPRESAS");

            migrationBuilder.DropIndex(
                name: "IX_LICITACOES_EmpresaId",
                table: "LICITACOES");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "LICITACOES");

            migrationBuilder.AddColumn<bool>(
                name: "LCT_CONFIRMACAO_PARTICIPACAO",
                table: "LICITACOES",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmpresaLicitacao",
                columns: table => new {
                    EmpresasId = table.Column<int>(type: "int", nullable: false),
                    LicitacoesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_EmpresaLicitacao", x => new { x.EmpresasId, x.LicitacoesId });
                    table.ForeignKey(
                        name: "FK_EmpresaLicitacao_EMPRESAS_EmpresasId",
                        column: x => x.EmpresasId,
                        principalTable: "EMPRESAS",
                        principalColumn: "EMP_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaLicitacao_LICITACOES_LicitacoesId",
                        column: x => x.LicitacoesId,
                        principalTable: "LICITACOES",
                        principalColumn: "LCT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaLicitacao_LicitacoesId",
                table: "EmpresaLicitacao",
                column: "LicitacoesId");
        }
    }
}
