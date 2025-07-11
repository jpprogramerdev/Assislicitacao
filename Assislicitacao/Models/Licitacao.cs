using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("LICITACOES")]
    public class Licitacao : EntidadeDominio {
        [Key]
        [Column("LCT_ID")]
        public int Id { get; set; }
        [Required]
        [Column("LCT_OBJETO")]
        public string Objeto { get; set; }
        [Required]
        [Column("LCT_DATA")]
        public DateTime Data { get; set; }
        [Column("LCT_VALOR_ESTIMADO")]
        public decimal? ValorEstimado { get; set; }

        //Relacionamentos - Relation
        [Required]
        [Column("LCT_TPL_ID")]
        public int TipoLicitacaoId { get; set; }
        [ForeignKey("TipoLicitacaoId")]
        public TipoLicitacao TipoLicitacao { get; set; }

        [Required]
        [Column("LCT_MUC_ID")]
        public int MunicipioId { get; set; }
        [ForeignKey("MunicipioId")]
        public Municipio Municipio { get; set; }

        [Required]
        [Column("LCT_PTL_ID")]
        public int PortalLicitacaoId { get; set; }
        [ForeignKey("PortalLicitacaoId")]
        public PortalLicitacao PortalLicitacao { get; set; }
        public List<LicitacaoEmpresa> Empresas { get; set; }

        [Required]
        [Column("LCT_STL_ID")]
        public int StatusLicitacaoId { get; set; }
        [ForeignKey("StatusLicitacaoId")]
        public StatusLicitacao StatusLicitacao { get; set; }
    }
}
