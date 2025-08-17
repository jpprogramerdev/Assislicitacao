using Assislicitacao.Models;
using Assislicitacao.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Assislicitacao.Strategy.Interface;
using Azure;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Globalization;

namespace Assislicitacao.Strategy {
    public class GerarRelatorioPDF : IStrategyRelatorioPDF {
        public byte[] Gerar(EntidadeDominio EntidadeDominio, RelatorioLicitacao filtroRelatorio) {
            var empresa = (Empresa)EntidadeDominio;

            empresa.Licitacoes = empresa.Licitacoes.OrderBy(l => l.Licitacao.Data).ToList();

            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container => {
                container.Page(page => {
                    page.Size(PageSizes.A4);
                    page.Margin(3, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));


                    page.Header()
                        .Row(row => {
                            row.ConstantItem(180).Image("wwwroot/Logos/LogoAssislicitacao.png");
                        });

                    page.Content()
                        .PaddingVertical(1, Unit.Millimetre)
                        .Column(x => {
                            x.Item()
                                .Text("Relatório de Licitações\n")
                                .Bold()
                                .FontSize(30);

                            foreach (var licitacao in empresa.Licitacoes) {
                                x.Item().Text(t => {
                                    t.Span($"{licitacao.Licitacao.TipoLicitacao.Sigla} {licitacao.Licitacao.Municipio.Nome}/{licitacao.Licitacao.Municipio.Estado.Uf}  - {licitacao.Licitacao.Objeto} - {licitacao.Licitacao.Data.ToString("dd/MM/yyyy")}\n");
                                    t.Span($"Portal da Licitação: ");

                                    t.Hyperlink(licitacao.Licitacao.PortalLicitacao.Nome, licitacao.Licitacao.PortalLicitacao.link)
                                        .FontColor(Colors.Blue.Darken2);

                                    t.Span($"\nEstimado: {(licitacao.Licitacao.ValorEstimado == null ? "SIGILOSO" : $"R$ {licitacao.Licitacao.ValorEstimado.Value.ToString("N2", new CultureInfo("pt-BR"))}")}\n\n");

                                });
                            }
                        });
                });
            });
            
           return document.GeneratePdf();
        }
    }
}
