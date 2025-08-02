using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class LicitacaoViewModel {
        public Licitacao Licitacao { get; set; }
        public List<Empresa> Empresas { get; set; } = new List<Empresa>();
        public List<Estado> Estados { get; set; } = new List<Estado>();
        public List<int> EmpresasSelecionadasIds { get; set; } = new List<int>();
        public List<TipoLicitacao> TiposLicitacao { get; set; } = new List<TipoLicitacao>();
        public List<PortalLicitacao> PortaisLicitacoes { get; set; } = new List<PortalLicitacao>();
        
    }
}
