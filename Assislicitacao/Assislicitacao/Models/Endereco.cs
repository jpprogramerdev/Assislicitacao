namespace Assislicitacao.Models {
    public class Endereco  : EntidadeDominio{
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public Cidade Cidade { get; set; }
    }
}
