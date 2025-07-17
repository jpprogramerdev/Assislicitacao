using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assislicitacao.Models {
    [Table("ENDERECOS")]
    public class Endereco : EntidadeDominio {
        [Key]
        [Column("END_ID")]
        public int Id { get; set; }
        [Required]
        [Column("END_LOGRADOURO")]
        public string Logradouro { get; set; }
        [Required]
        [Column("END_NUMERO")]
        public string Numero { get; set; }
        [Required]
        [Column("END_BAIRRO")]
        public string Bairro { get; set; }
        [Required]
        [Column("END_CEP")]
        public string CEP { get; set; }
        [Column("END_COMPLEMENTO")]
        public string? Complemento { get; set; }

        //Relatcionamentos - Relation
        [Column("END_MUC_ID")]
        public int MunicipioId { get; set; }
        [ForeignKey("MunicipioId")]
        public Municipio Municipio { get; set; }
        public List<Empresa> Empresas { get; set; }
    }
}
