using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("ESTADOS")]
    [Index(nameof(Uf), IsUnique = true)]
    public class Estado : EntidadeDominio{
        [Key]
        [Column("EST_ID")]
        public int Id { get; set; }

        [Required]
        [Column("EST_UF")]
        public string Uf { get; set; }

        //Relacionamentos - Relation
        public List<Municipio> Municipios { get; set; }
    }
}
