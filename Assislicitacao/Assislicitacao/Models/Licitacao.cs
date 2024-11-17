namespace Assislicitacao.Models {
    public class Licitacao : EntidadeDominio {
        public Licitacao(){
            Cidade = new();
            Portal = new();
            TipoLicitacao = new();
            TipoDisputa = new();
            Usuario = new();
        }

        public string Numero { get; set; } //cada licitacao é identificada pela adminitração publica por um numero. EX: PREGÃO ELETRÔNICO 90020/2024
        public string Objeto { get; set; } //objeto é o que será ofertado/contratado pela administração publica. EX: AQUISIÇÃO DE LUMINARIAS LED
        public DateTime Data { get; set; }
        public double ValorEstimado { get; set; }
        public Cidade Cidade { get; set; }
        public Portal Portal { get; set; }
        public TipoLicitacao TipoLicitacao { get; set; }
        public TipoDisputa TipoDisputa { get; set; }
        public Empresa Empresa { get; set; } 
        public bool Confirmacao { get; set; }
        public Usuario Usuario { get; set; } //Toda licitação tem um usuario responsavel por participar, acompanhar etc
    }
}
