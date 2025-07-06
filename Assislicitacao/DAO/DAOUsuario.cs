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

        public IEnumerable<EntidadeDominio> SelectAll() => _context.Usuarios.Include(u => u.TipoUsuario);

        public async Task Update(EntidadeDominio entidade) {

            Usuario UsuarioAtualizado = (Usuario)entidade;

            Usuario UsuarioDb = _context.Usuarios.FirstOrDefault(u => u.Id == UsuarioAtualizado.Id);

            UsuarioDb.Nome = UsuarioAtualizado.Nome;
            UsuarioDb.Email = UsuarioAtualizado.Email;
            UsuarioDb.TipoId = UsuarioAtualizado.TipoId;
            UsuarioDb.Senha = string.IsNullOrEmpty(UsuarioAtualizado.Senha) ? UsuarioDb.Senha : UsuarioAtualizado.Senha;

            await _context.SaveChangesAsync();
        }
    }
}
