using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Assislicitacao.Context {
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
