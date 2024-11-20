namespace Assislicitacao.Models {
    public class Empresa : EntidadeDominio{
        public Empresa() {
            Enquadramento = new();
            Endereco = new();
            Licitacoes = new();
            Portais = new();
        }

        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string TelefoneContato { get; set; }
        public Email EmailContato { get; set; }
        public Enquadramento Enquadramento { get; set; }
        public Endereco Endereco  { get; set; }
        public List<Licitacao> Licitacoes { get; set; }
        public List<Portal> Portais { get; set; }

    }
}
