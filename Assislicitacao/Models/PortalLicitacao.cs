using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("PORTAIS_LICITACAO")]
    public class PortalLicitacao : EntidadeDominio {
        [Key]
        [Column("PTL_ID")]
        public int Id { get; set; }
        [Required]
        [Column("PTL_NOME")]
        public string Nome { get; set; }
        [Required]
        [Column("PTL_LINK")]
        public string link { get; set; }

        //Relacionamentos - relation
        public List<Licitacao> Licitacoes { get; set; }
    }
}
