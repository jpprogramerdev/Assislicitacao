using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;

namespace Assislicitacao.Strategy {
    public class FiltrarUsuarios : IStrategyFiltro {
        public List<T> Executar<T>(List<T> listFiltro, params (string tipoFiltro, string valor)[] filtros) {
            var usuariosFiltro = listFiltro.Cast<Usuario>().ToList();

            foreach (var filtro in filtros) {
                if (string.IsNullOrWhiteSpace(filtro.valor)) continue;
                switch (filtro.tipoFiltro) {
                    case "tipo":
                        usuariosFiltro = usuariosFiltro
                            .Where(l => l.TipoUsuario.Tipo.Equals(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    case "nome":
                        usuariosFiltro = usuariosFiltro
                            .Where(l => l.Nome.Contains(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    case "email":
                        usuariosFiltro = usuariosFiltro
                            .Where(l => l.Email.Contains(filtro.valor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                }
            }

            return usuariosFiltro.Cast<T>().ToList();
        }

        public List<T> Executar<T>(List<T> listBancoDeDados, int usuarioId) {
            throw new NotImplementedException();
        }

        public List<T> Executar<T>(List<T> listFiltro, List<string> statusNaoPermetidos) {
            throw new NotImplementedException();
        }
    }
}
