using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("EMPRESAS")]
    [Index(nameof(CNPJ), IsUnique = true)]
    public class Empresa : EntidadeDominio{
        [Key]
        [Column("EMP_ID")]
        public int Id { get; set; }
        [Required]
        [Column("EMP_CNPJ")]
        public string CNPJ { get; set; }
        [Required]
        [Column("EMP_RAZAO_SOCIAL")]
        public string RazaoSocial {get;set;}

        //Relacionamentos - Relation
        [Required]
        [Column("EMP_END_ID")]
        public int EnderecoId { get; set; }
        [ForeignKey("EnderecoId")]
        public Endereco Endereco { get; set; }

        [Required]
        [Column("EMP_PTE_ID")]
        public int PorteEmrpesaId { get; set; }
        [ForeignKey("PorteEmrpesaId")]
        public PorteEmpresa PorteEmpresa { get; set; }
        public List<LicitacaoEmpresa> Licitacoes { get; set; }
        public List<Usuario> UsusariosVinculados { get; set; }
    }
}
