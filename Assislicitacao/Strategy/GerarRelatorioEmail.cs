using Assislicitacao.Models;
using Assislicitacao.Models.Enum;
using Assislicitacao.Strategy.Interface;

namespace Assislicitacao.Strategy {
    public class GerarRelatorioEmail : IStrategyRelatorioEmail {
        public string Gerar(EntidadeDominio entidadeDominio, RelatorioLicitacao filtroRelatorio) {
            var empresa = (Empresa)entidadeDominio;

            string mensagem = $"Relatório de Licitações da empresa {empresa.RazaoSocial}";

            empresa.Licitacoes = empresa.Licitacoes.OrderBy(l => l.Licitacao.Data).ToList();

            if (filtroRelatorio.PeriodoRelatorio == PeriodoRelatorio.Semanal) {
                var hoje = DateTime.Today;

                var inicioSemana = hoje.AddDays(-(int)hoje.DayOfWeek);

                var segunda = inicioSemana.AddDays(1);
                var sexta = segunda.AddDays(4);

                empresa.Licitacoes = empresa.Licitacoes
                    .Where(l => l.Licitacao.Data.Date >= segunda && l.Licitacao.Data.Date <= sexta)
                    .OrderBy(l => l.Licitacao.Data)
                    .ToList();
            } else if (filtroRelatorio.PeriodoRelatorio == PeriodoRelatorio.Mensal) {
                var hoje = DateTime.Today;

                var primeiroDiaMes = new DateTime(hoje.Year, hoje.Month, 1);

                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

                empresa.Licitacoes = empresa.Licitacoes
                .Where(l => l.Licitacao.Data.Date >= primeiroDiaMes &&
                            l.Licitacao.Data.Date <= ultimoDiaMes)
                .OrderBy(l => l.Licitacao.Data)
                .ToList();
            } else if (filtroRelatorio.PeriodoRelatorio == PeriodoRelatorio.Anual) {
                var hoje = DateTime.Today;
                var primeiroDiaDoAno = new DateTime(hoje.Year, 1, 1);
                var ultimoDiaDoAno = new DateTime(hoje.Year, 12, 31);

                empresa.Licitacoes = empresa.Licitacoes
                    .Where(l => l.Licitacao.Data.Date >= primeiroDiaDoAno &&
                                l.Licitacao.Data.Date <= ultimoDiaDoAno)
                    .OrderBy(l => l.Licitacao.Data)
                    .ToList();
            }


            foreach (var Licitacao in empresa.Licitacoes) {
                mensagem += $"<br/><br/>{Licitacao.Licitacao.TipoLicitacao.Sigla} {Licitacao.Licitacao.Municipio.Nome} /  {Licitacao.Licitacao.Municipio.Estado.Uf}- {Licitacao.Licitacao.Objeto} - {Licitacao.Licitacao.Data.ToString("dd/MM/yyyy")} <strong>({Licitacao.Licitacao.StatusLicitacao.Status})</strong>";
            }

            return mensagem;
        }
    }
}
