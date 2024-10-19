namespace Assislicitacao.Models {
    public class Orgao : EntidadeDominio {
        public string Nome { get; set;}
        public Cidade Cidade { get; set;}
        public Estado Estado { get; set;}
    }
}
