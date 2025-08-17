using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;

namespace Assislicitacao.Strategy {
    public class GerarRelatorioEmail : IStrategyRelatorioEmail {
        public string Gerar(EntidadeDominio entidadeDominio, RelatorioLicitacao filtroRelatorio) {
            var empresa = (Empresa)entidadeDominio;

            string mensagem = $"Relatório de Licitações da empresa {empresa.RazaoSocial}";

            empresa.Licitacoes = empresa.Licitacoes.Where(l => l.Licitacao.Data > DateTime.Now).OrderBy(l => l.Licitacao.Data).ToList();

            foreach (var Licitacao in empresa.Licitacoes) {
                mensagem += $"<br/><br/>{Licitacao.Licitacao.TipoLicitacao.Sigla} {Licitacao.Licitacao.Municipio.Nome} /  {Licitacao.Licitacao.Municipio.Estado.Uf}- {Licitacao.Licitacao.Objeto} - {Licitacao.Licitacao.Data.ToString("dd/MM/yyyy")} <strong>({Licitacao.Licitacao.StatusLicitacao.Status})</strong>";
            }

            return mensagem;
        }
    }
}
