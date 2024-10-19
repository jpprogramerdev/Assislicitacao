namespace Assislicitacao.Models {
    public class Licitacao {
        public string Numero { get; set; } //cada licitacao é identificada pela adminitração publica por um numero. EX: PREGÃO ELETRÔNICO 90020/2024
        public string Objeto { get; set; } //objeto é o que será ofertado/contratado pela administração publica. EX: AQUISIÇÃO DE LUMINARIAS LED
        public DateTime Data { get; set; }
        public double ValorEstimado { get; set; }
        public bool Confirmado { get; set; }
        public Cidade Cidade { get; set; }
        public Orgao Orgao { get; set; }
        public Portal Portal { get; set; }
        public TipoLicitacao TipoLicitacao { get; set; }
        public TipoDisputa TipoDisputa { get; set; }
    }
}
