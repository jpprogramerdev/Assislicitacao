using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("PORTES_EMPRESA")]
    [Index(nameof(Porte), IsUnique = true)]
    public class PorteEmpresa : EntidadeDominio {
        [Key]
        [Column("PTE_ID")]
        public int Id { get; set; }
        [Required]
        [Column("PTE_PORTE")]
        public string Porte { get; set; }

        //Relacionamentos - Relation
        public List<Empresa> Empresas { get; set; }
    }
}