using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("LICITACOES_EMPRESAS")]
    public class LicitacaoEmpresa : EntidadeDominio{
        [Key]
        [Column("LCEM_ID")]
        public int Id { get; set; }

        [Column("LCEM_VALOR_GANHO")]
        public decimal ValorGanho { get; set; }

        [Required]
        [Column("LCEM_LCT_ID")]
        public int LicitacaoId { get; set; }
        [ForeignKey("LicitacaoId")]
        public Licitacao Licitacao { get; set; }
        [Required]
        [Column("LCEM_EMP_ID")]
        public int EmpresaId { get; set; }
        [ForeignKey("EmpresaId")]
        public Empresa Empresa{ get; set; }
        [Column("LCEM_CONFIRMACAO_PARTICIPACAO")]
        public bool ConfirmacaoParticipacao { get; set; }

    }
}
