using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Assislicitacao.Strategy {
    public class GerarRelatorioLicitacoes : IRelatorioStrategy {
        public void Executar(List<EntidadeDominio> List) {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            List<Licitacao>Licitacoes = List.Cast<Licitacao>().ToList();

            var document = Document.Create(container => {
                container.Page(page => {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Header().Text($"Relatório {DateTime.Now.ToString("dd/MM/yyyy")}\n").Bold().FontSize(20);

                    page.Content().Column(column => {
                        foreach (Licitacao Licitacao in Licitacoes) {
                            column.Item().Text($"Data: {Licitacao.Data:dd/MM/yyyy}\n" +
                                               $"{Licitacao.TipoLicitacao.Sigla} {Licitacao.Cidade.Nome} - {Licitacao.Objeto}\n" +
                                               $"Portal: {Licitacao.Portal.Nome} \n\n\n").FontSize(12);
                        }
                    });

                    page.Footer().Text(text => {
                        text.CurrentPageNumber();
                        text.Span(" de ");
                        text.TotalPages();
                    });
                });
            });

            document.GeneratePdfAndShow();
        }
    }
}
