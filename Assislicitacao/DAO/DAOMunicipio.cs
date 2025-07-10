using Assislicitacao.Context;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Assislicitacao.DAO {
    public class DAOMunicipio : IDAOMunicipio {
        private readonly AppDbContext _context;

        public DAOMunicipio(AppDbContext context) {
            _context = context;
        }

        public Task Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public async Task Insert(EntidadeDominio entidade) {
            _context.Municipios.Add((Municipio)entidade);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<EntidadeDominio>> SelectAll() => await _context.Municipios.Include(muc => muc.Estado).ToListAsync();

        public Task Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
