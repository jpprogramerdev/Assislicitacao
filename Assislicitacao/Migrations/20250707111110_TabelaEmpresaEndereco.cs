using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assislicitacao.Migrations
{
    /// <inheritdoc />
    public partial class TabelaEmpresaEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTADOS",
                columns: table => new
                {
                    EST_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EST_UF = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADOS", x => x.EST_ID);
                });

            migrationBuilder.CreateTable(
                name: "PORTES_EMPRESA",
                columns: table => new
                {
                    PTE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PTE_PORTE = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PORTES_EMPRESA", x => x.PTE_ID);
                });

            migrationBuilder.CreateTable(
                name: "MUNICIPIOS",
                columns: table => new
                {
                    MUC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MUC_NOME = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MUC_USU_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUNICIPIOS", x => x.MUC_ID);
                    table.ForeignKey(
                        name: "FK_MUNICIPIOS_ESTADOS_MUC_USU_ID",
                        column: x => x.MUC_USU_ID,
                        principalTable: "ESTADOS",
                        principalColumn: "EST_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ENDERECOS",
                columns: table => new
                {
                    END_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    END_LOGRADOURO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    END_NUMERO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    END_BAIRRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    END_CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    END_MUC_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECOS", x => x.END_ID);
                    table.ForeignKey(
                        name: "FK_ENDERECOS_MUNICIPIOS_END_MUC_ID",
                        column: x => x.END_MUC_ID,
                        principalTable: "MUNICIPIOS",
                        principalColumn: "MUC_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EMPRESAS",
                columns: table => new
                {
                    EMP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMP_CNPJ = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EMP_RAZAO_SOCIAL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMP_END_ID = table.Column<int>(type: "int", nullable: false),
                    EMP_PTE_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESAS", x => x.EMP_ID);
                    table.ForeignKey(
                        name: "FK_EMPRESAS_ENDERECOS_EMP_END_ID",
                        column: x => x.EMP_END_ID,
                        principalTable: "ENDERECOS",
                        principalColumn: "END_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EMPRESAS_PORTES_EMPRESA_EMP_PTE_ID",
                        column: x => x.EMP_PTE_ID,
                        principalTable: "PORTES_EMPRESA",
                        principalColumn: "PTE_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESAS_EMP_CNPJ",
                table: "EMPRESAS",
                column: "EMP_CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESAS_EMP_END_ID",
                table: "EMPRESAS",
                column: "EMP_END_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESAS_EMP_PTE_ID",
                table: "EMPRESAS",
                column: "EMP_PTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ENDERECOS_END_MUC_ID",
                table: "ENDERECOS",
                column: "END_MUC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ESTADOS_EST_UF",
                table: "ESTADOS",
                column: "EST_UF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MUNICIPIOS_MUC_NOME_MUC_USU_ID",
                table: "MUNICIPIOS",
                columns: new[] { "MUC_NOME", "MUC_USU_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MUNICIPIOS_MUC_USU_ID",
                table: "MUNICIPIOS",
                column: "MUC_USU_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PORTES_EMPRESA_PTE_PORTE",
                table: "PORTES_EMPRESA",
                column: "PTE_PORTE",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPRESAS");

            migrationBuilder.DropTable(
                name: "ENDERECOS");

            migrationBuilder.DropTable(
                name: "PORTES_EMPRESA");

            migrationBuilder.DropTable(
                name: "MUNICIPIOS");

            migrationBuilder.DropTable(
                name: "ESTADOS");
        }
    }
}
