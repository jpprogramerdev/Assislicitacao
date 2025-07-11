using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class LicitacaoViewModel {
        public Licitacao Licitacao { get; set; }
        public List<Empresa> Empresas { get; set; }
        public List<int> EmpresasSelecionadasIds { get; set; } = new List<int>();
        public List<TipoLicitacao> TiposLicitacao { get; set; }
        public List<PortalLicitacao> PortaisLicitacoes { get; set; }
        
    }
}
