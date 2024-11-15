using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class EmpresaLicitacao : EntidadeDominio{
        public Empresa Empresa{ get; set; }
        public Licitacao Licitacao { get; set; }
    }
}
