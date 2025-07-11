using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("STATUS_LICITACAO")]
    public class StatusLicitacao : EntidadeDominio {
        [Key]
        [Column("STL_ID")]
        public int Id { get; set; }
        [Required]
        [Column("STL_STATUS")]
        public string Status { get; set; }

        //Relacionamentos - Relation
        public List<Licitacao> Licitacoes { get; set; }
    }
}
