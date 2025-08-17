using Assislicitacao.Models.Enum;

namespace Assislicitacao.Models {
    public class RelatorioLicitacao {
        //Classe feito para ajdar na filtragem de relatorio.
        //Ou seja, feita para ser usada para decidir como será gerado o relatorio sendo ele possíve por email ou pdf, sendo possível filtrar mensal, anual ou semanal.
        public TipoRelatorio TipoRelatorio { get; set; }
        public PeriodoRelatorio PeriodoRelatorio { get; set; }
        public int EmpresaId { get; set; }
    }
}
