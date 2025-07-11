using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("TIPOS_LICITACAO")]
    public class TipoLicitacao : EntidadeDominio {
        [Key]
        [Column("TPL_ID")]
        public int Id { get; set; }
        [Required]
        [Column("TPL_TIPO")]
        public string Tipo { get; set; }
        [Required]
        [Column("TPL_SIGLA")]
        public string Sigla { get; set; }

        //Relacionamentos - Relation
        public List<Licitacao> Licitacoes { get; set; }
    }
}
