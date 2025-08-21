using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;

namespace Assislicitacao.Strategy {
    public class FiltrarEmpresa : IStrategyFiltro {
        public List<T> Executar<T>(List<T> listFiltro, params (string tipoFiltro, string valor)[] filtros) {
            var empresasFiltro = listFiltro.Cast<Empresa>().ToList();

            foreach (var filtro in filtros) {
                if (string.IsNullOrWhiteSpace(filtro.valor)) continue;
                switch (filtro.tipoFiltro) {
                    case "porte":
                        empresasFiltro = empresasFiltro
                            .Where(e => e.PorteEmpresa.Porte.Equals(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    case "nome":
                        empresasFiltro = empresasFiltro
                            .Where(e => e.RazaoSocial.Contains(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    case "cnpj":
                        empresasFiltro = empresasFiltro
                            .Where(e => e.CNPJ.Contains(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                }
            }
            return empresasFiltro.Cast<T>().ToList();
        }

        public List<T> Executar<T>(List<T> listBancoDeDados, int usuarioId) {
            throw new NotImplementedException();
        }

        public List<T> Executar<T>(List<T> listFiltro, List<string> statusNaoPermetidos) {
            throw new NotImplementedException();
        }
    }
}
