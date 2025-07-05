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

        public void Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public IEnumerable<EntidadeDominio> SelectAll() => _context.TipoUsuarios;

        public void Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
