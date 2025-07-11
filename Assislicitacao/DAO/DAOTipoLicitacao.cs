using Assislicitacao.Context;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Assislicitacao.DAO {
    public class DAOTipoLicitacao : IDAOTipoLicitacao {
        private readonly AppDbContext _context;

        public DAOTipoLicitacao(AppDbContext context) {
            _context = context;
        }
        public Task Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public Task Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EntidadeDominio>> SelectAll() => await _context.TiposLicitacao.ToListAsync();

        public Task Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
