using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class TodasLicitacoesViewModel {
        public List<Licitacao> Licitacoes { get; set; } = new List<Licitacao>();
        public List<StatusLicitacao> StatusLicitacoes { get; set; } = new List<StatusLicitacao>();
        public List<TipoLicitacao> TiposLicitacoes { get; set; } = new List<TipoLicitacao>(); 
    }
}
