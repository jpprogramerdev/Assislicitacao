using Assislicitacao.Context;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.DAO {
    public class DAOUsuario : IDAOUsuario{
        public readonly AppDbContext _context;

        public DAOUsuario(AppDbContext context) {
            _context = context;
        }

        public void Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) =>  _context.Add((Usuario)entidade);

        public IEnumerable<EntidadeDominio> SelectAll() {
            throw new NotImplementedException();
        }

        public void Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
