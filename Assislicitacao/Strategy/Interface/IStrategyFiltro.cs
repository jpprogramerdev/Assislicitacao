using Assislicitacao.Models;

namespace Assislicitacao.Strategy.Interface {
    public interface IStrategyFiltro {
        public List<Licitacao> Executar(List<Licitacao> licitacoesFiltro, params (string tipoFiltro, string valor)[] filtros);//FILTRAR POR DIVERSOS CRITERIOS
        public List<Licitacao> Executar(List<Licitacao> licitacoesBancoDeDados, int usuarioId);//FILTRAR POR USUARIO
        public List<Licitacao> Executar(List<Licitacao> licitacoesFiltro, List<string> statusNaoPermetidos);//FILTRAR STATUS QUE NÃO SEJA SUSPENSA, REVOGADA OU ADJUDICADA/HOMOLAGADA
    }
}
