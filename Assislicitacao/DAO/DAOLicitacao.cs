using Assislicitacao.Context;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Assislicitacao.DAO {
    public class DAOLicitacao : IDAOLicitacao {
        private readonly AppDbContext _context;
        public DAOLicitacao(AppDbContext context) {
            _context = context;
        }

        public Task Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public async Task Insert(EntidadeDominio entidade) {
            _context.Add((Licitacao)entidade);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EntidadeDominio>> SelectAll() =>
     await _context.Licitacoes
         .Include(l => l.Empresas)  //Empresas que poderão particiapr                         
             .ThenInclude(le => le.Empresa) //Dados de cada empresa
         .Include(l => l.TipoLicitacao)
         .Include(l => l.PortalLicitacao)
         .Include(l => l.StatusLicitacao)
         .ToListAsync();

        public Task Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
