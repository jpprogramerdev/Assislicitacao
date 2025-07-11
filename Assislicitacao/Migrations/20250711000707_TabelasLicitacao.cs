using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assislicitacao.Migrations {
    public partial class TabelasLicitacao : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "PORTAIS_LICITACAO",
                columns: table => new {
                    PTL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PTL_NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PTL_LINK = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_PORTAIS_LICITACAO", x => x.PTL_ID);
                });

            migrationBuilder.CreateTable(
                name: "STATUS_LICITACAO",
                columns: table => new {
                    STL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STL_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_STATUS_LICITACAO", x => x.STL_ID);
                });

            migrationBuilder.CreateTable(
                name: "TIPOS_LICITACAO",
                columns: table => new {
                    TPL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TPL_TIPO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TPL_SIGLA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_TIPOS_LICITACAO", x => x.TPL_ID);
                });

            migrationBuilder.CreateTable(
                name: "LICITACOES",
                columns: table => new {
                    LCT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LCT_OBJETO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LCT_DATA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LCT_VALOR_ESTIMADO = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LCT_CONFIRMACAO_PARTICIPACAO = table.Column<bool>(type: "bit", nullable: true),
                    LCT_TPL_ID = table.Column<int>(type: "int", nullable: false),
                    LCT_MUC_ID = table.Column<int>(type: "int", nullable: false),
                    LCT_PTL_ID = table.Column<int>(type: "int", nullable: false),
                    LCT_STL_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_LICITACOES", x => x.LCT_ID);
                    table.ForeignKey(
                        name: "FK_LICITACOES_MUNICIPIOS_LCT_MUC_ID",
                        column: x => x.LCT_MUC_ID,
                        principalTable: "MUNICIPIOS",
                        principalColumn: "MUC_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LICITACOES_PORTAIS_LICITACAO_LCT_PTL_ID",
                        column: x => x.LCT_PTL_ID,
                        principalTable: "PORTAIS_LICITACAO",
                        principalColumn: "PTL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LICITACOES_STATUS_LICITACAO_LCT_STL_ID",
                        column: x => x.LCT_STL_ID,
                        principalTable: "STATUS_LICITACAO",
                        principalColumn: "STL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LICITACOES_TIPOS_LICITACAO_LCT_TPL_ID",
                        column: x => x.LCT_TPL_ID,
                        principalTable: "TIPOS_LICITACAO",
                        principalColumn: "TPL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        onDelete: ReferentialAction.Cascade); // este pode ser cascade

                    table.ForeignKey(
                        name: "FK_EmpresaLicitacao_LICITACOES_LicitacoesId",
                        column: x => x.LicitacoesId,
                        principalTable: "LICITACOES",
                        principalColumn: "LCT_ID",
                        onDelete: ReferentialAction.NoAction); // ⛔ corrigido aqui
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaLicitacao_LicitacoesId",
                table: "EmpresaLicitacao",
                column: "LicitacoesId");

            migrationBuilder.CreateIndex(
                name: "IX_LICITACOES_LCT_MUC_ID",
                table: "LICITACOES",
                column: "LCT_MUC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LICITACOES_LCT_PTL_ID",
                table: "LICITACOES",
                column: "LCT_PTL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LICITACOES_LCT_STL_ID",
                table: "LICITACOES",
                column: "LCT_STL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LICITACOES_LCT_TPL_ID",
                table: "LICITACOES",
                column: "LCT_TPL_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "EmpresaLicitacao");

            migrationBuilder.DropTable(
                name: "LICITACOES");

            migrationBuilder.DropTable(
                name: "PORTAIS_LICITACAO");

            migrationBuilder.DropTable(
                name: "STATUS_LICITACAO");

            migrationBuilder.DropTable(
                name: "TIPOS_LICITACAO");
        }
    }
}
