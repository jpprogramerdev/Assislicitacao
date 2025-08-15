using Assislicitacao.Context;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Assislicitacao.DAO {
    public class DAOUsuario : IDAOUsuario {
        public readonly AppDbContext _context;

        public DAOUsuario(AppDbContext context) {
            _context = context;
        }

        public async Task Delete(EntidadeDominio entidade) {
            _context.Remove((Usuario)entidade);
            await _context.SaveChangesAsync();
        }

        public async Task Insert(EntidadeDominio entidade) { 
            _context.Add((Usuario)entidade);
           await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EntidadeDominio>> SelectAll() => await _context.Usuarios.Include(u => u.TipoUsuario).Include(u => u.EmpresasVinculadas).ToListAsync();

        public async Task Update(EntidadeDominio entidade) {

            Usuario UsuarioAtualizado = (Usuario)entidade;

            Usuario UsuarioDb = _context.Usuarios.FirstOrDefault(u => u.Id == UsuarioAtualizado.Id);

            UsuarioDb.Nome = UsuarioAtualizado.Nome;
            UsuarioDb.Email = UsuarioAtualizado.Email;
            UsuarioDb.TipoId = UsuarioAtualizado.TipoId;
            UsuarioDb.Senha = string.IsNullOrEmpty(UsuarioAtualizado.Senha) ? UsuarioDb.Senha : UsuarioAtualizado.Senha;
            UsuarioDb.FotoPerfilUrl = UsuarioAtualizado.FotoPerfilUrl;

            await _context.SaveChangesAsync();
        }
    }
}
