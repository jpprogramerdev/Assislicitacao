using Assislicitacao.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("TIPOS_USUARIO")]
    public class TipoUsuario : EntidadeDominio {
        [Key]
        [Column("TPU_ID")]
        public int Id { get; set; }
        [Required]
        [Column("TPU_TIPO")]
        public string Tipo { get; set; }
        
        //Relacionamentos - Relation
        public List<Usuario> Usuarios { get; set; }
    }
}
