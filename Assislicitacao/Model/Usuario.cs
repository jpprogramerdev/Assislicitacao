using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Model {
    [Table("USUARIOS")]
    public class Usuario {
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
    }
}
