using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Model {
    [Table("TIPOS_USUARIO")]
    public class TipoUsuario {
        [Key]
        [Column("TPU_ID")]
        public int Id { get; set; }
        [Required]
        [Column("TPU_TIPO")]
        public string Tipo { get; set; }
    }
}
