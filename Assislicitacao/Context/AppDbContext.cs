using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Assislicitacao.Context {
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<PorteEmpresa> PortesEmpresa { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Licitacao> Licitacoes { get; set; }
        public DbSet<PortalLicitacao> PortaisLicitacoes{ get; set; }
        public DbSet<StatusLicitacao> StatusLicitacaos { get; set; }
        public DbSet<TipoLicitacao> TiposLicitacao { get; set; }
    }
}
