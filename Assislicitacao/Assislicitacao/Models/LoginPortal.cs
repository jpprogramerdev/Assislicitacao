namespace Assislicitacao.Models {
    public class LoginPortal : EntidadeDominio{
        public string Login { get; set; }
        public string Senha { get; set; }
        public Portal Portal { get; set; }
    }
}
