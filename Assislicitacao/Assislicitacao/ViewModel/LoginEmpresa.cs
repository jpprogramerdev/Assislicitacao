using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class LoginEmpresa : EntidadeDominio{
        public Empresa Empresa { get; set; }
        public LoginPortal  LoginPortal { get; set; }
    }
}
