using Assislicitacao.Context;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Assislicitacao.DAO {
    public class DAOTiposUsuario : IDAOTipoUsuario {
        public readonly AppDbContext _context;

        public DAOTiposUsuario(AppDbContext context) {
            _context = context;
        }

        public Task Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public Task Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public IEnumerable<EntidadeDominio> SelectAll() => _context.TipoUsuarios;

        public  Task Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
