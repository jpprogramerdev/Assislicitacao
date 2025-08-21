using Assislicitacao.Models;

namespace Assislicitacao.Strategy.Interface {
    public interface IStrategyFiltro {
        public List<T> Executar<T>(List<T> listFiltro, params (string tipoFiltro, string valor)[] filtros);//FILTRAR POR DIVERSOS CRITERIOS
        public List<T> Executar<T>(List<T> listBancoDeDados, int usuarioId);//FILTRAR POR USUARIO
        public List<T> Executar<T>(List<T> listFiltro, List<string> statusNaoPermetidos);//FILTRAR STATUS QUE NÃO SEJA SUSPENSA, REVOGADA OU ADJUDICADA/HOMOLAGADA
    }
}
