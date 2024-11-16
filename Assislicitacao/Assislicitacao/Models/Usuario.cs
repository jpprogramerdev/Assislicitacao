namespace Assislicitacao.Models {
    public class Usuario : EntidadeDominio {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}
