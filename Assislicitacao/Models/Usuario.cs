using Assislicitacao.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("USUARIOS")]
    public class Usuario : EntidadeDominio {
        [Key]
        [Column("USU_ID")]
        public int Id { get; set; }
        [Required]
        [Column("USU_NOME")]
        public string Nome { get; set; }
        [Required]
        [Column("USU_SENHA")]
        public string Senha { get; set; }
        [Required]
        [Column("USU_EMAIL")]
        public string Email { get; set; }

        //Relacionamentos - Relation
        [Column("USU_TPU_ID")]
        public int TipoId { get; set; }
        [ForeignKey("TipoId")]
        public TipoUsuario TipoUsuario { get; set; }
        public List<Empresa> EmpresasVinculadas { get; set; }
    }
}
