using Assislicitacao.Context;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Assislicitacao.DAO {
    public class DAOEmpresa : IDAOEmpresa {
        private readonly AppDbContext _context;

        public DAOEmpresa(AppDbContext context) {
            _context = context;
        }

        public async Task Delete(EntidadeDominio entidade) {
            var Empresa = (Empresa)entidade;

            var EmpresaDB = await _context.Empresas.Include(e => e.Endereco).Include(e => e.PorteEmpresa).FirstOrDefaultAsync(e => e.Id == Empresa.Id);

            if(EmpresaDB != null){
                EmpresaDB.Endereco = null;
                EmpresaDB.PorteEmpresa = null;
                _context.Empresas.Remove(EmpresaDB);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Insert(EntidadeDominio entidade) {
            _context.Empresas.Add((Empresa)entidade);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EntidadeDominio>> SelectAll() => await _context.Empresas.Include(emp => emp.Endereco).ThenInclude(end => end.Municipio).ThenInclude(muc => muc.Estado).Include(emp => emp.PorteEmpresa).ToListAsync();

        public Task Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
