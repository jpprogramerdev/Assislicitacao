using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("MUNICIPIOS")]
    [Index(nameof(Nome), nameof(EstadoId), IsUnique = true)]
    public class Municipio : EntidadeDominio{
        [Key]
        [Column("MUC_ID")]
        public int Id { get; set; }

        [Required]
        [Column("MUC_NOME")]
        public string Nome { get; set; }

        //Relacionamentos - Relation
        [Column("MUC_USU_ID")]
        public int EstadoId { get; set; }
        [ForeignKey("EstadoId")]
        public Estado Estado { get; set; }
        public List<Endereco> Enderecos{get;set;}
    }
}
