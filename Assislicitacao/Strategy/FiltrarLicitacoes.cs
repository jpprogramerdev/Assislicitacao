using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;
using System.Globalization;

namespace Assislicitacao.Strategy {
    public class FiltrarLicitacoes : IStrategyFiltro {

        public List<T> Executar<T>(List<T> listFiltro, params (string tipoFiltro, string valor)[] filtros) {
            var licitacoesFiltro = listFiltro.Cast<Licitacao>().ToList();

            foreach (var filtro in filtros) {
                if (string.IsNullOrWhiteSpace(filtro.valor)) continue;
                switch (filtro.tipoFiltro) {
                    case "tipo":
                        licitacoesFiltro = licitacoesFiltro
                            .Where(l => l.TipoLicitacao.Tipo.Equals(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    case "cidade":
                        licitacoesFiltro = licitacoesFiltro
                            .Where(l => l.Municipio.Nome.Contains(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    case "status":
                        licitacoesFiltro = licitacoesFiltro
                            .Where(l => l.StatusLicitacao.Status.Equals(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    case "objeto":
                        licitacoesFiltro = licitacoesFiltro
                            .Where(l => l.Objeto.Contains(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    case "data":
                        licitacoesFiltro = licitacoesFiltro
                            .Where(l => l.Data.Date == DateTime.Parse(filtro.valor).Date)
                            .ToList();
                        break;
                }
            }
            return licitacoesFiltro.Cast<T>().ToList();
        }

        public List<T> Executar<T>(List<T> listBancoDeDados, int usuarioId) {
            var licitacoesBancoDeDados = listBancoDeDados.Cast<Licitacao>().ToList();

            var licitacoesFiltro = new List<Licitacao>();

            foreach (Licitacao Licitacao in licitacoesBancoDeDados) {
                foreach (LicitacaoEmpresa LicitacaoEmpresa in Licitacao.Empresas) {
                    if (LicitacaoEmpresa.Empresa.UsusariosVinculados.Any(u => u.Id == usuarioId)) {
                        licitacoesFiltro.Add(Licitacao);
                    }
                    break;
                }
            }
            return licitacoesFiltro.Cast<T>().ToList();
        }

        public List<T> Executar<T>(List<T> listFiltro, List<string> statusNaoPermetidos) {
            var licitacoesFiltro = listFiltro.Cast<Licitacao>().ToList();

            licitacoesFiltro = licitacoesFiltro
                    .Where(l => l.Data >= DateTime.Now && !statusNaoPermetidos.Contains(l.StatusLicitacao.Status))
                    .ToList();

            return licitacoesFiltro.Cast<T>().ToList();
        }
    }
}
