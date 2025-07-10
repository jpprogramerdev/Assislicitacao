using Assislicitacao.Context;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Assislicitacao.DAO {
    public class DAOPorteEmpresa : IDAOPorteEmpresa {
        private readonly AppDbContext _context;

        public DAOPorteEmpresa(AppDbContext context) {
            _context = context;
        }

        public Task Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public async Task Insert(EntidadeDominio entidade) {
            _context.PortesEmpresa.Add((PorteEmpresa)entidade);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EntidadeDominio>> SelectAll() => await _context.PortesEmpresa.ToListAsync();

        public Task Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
