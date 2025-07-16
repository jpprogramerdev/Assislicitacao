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

        public async Task<IEnumerable<EntidadeDominio>> SelectAll() => await _context.Empresas.Include(emp => emp.Endereco).ThenInclude(end => end.Municipio).ThenInclude(muc => muc.Estado).Include(emp => emp.PorteEmpresa).Include(usu => usu.UsusariosVinculados).ToListAsync();

        public async Task Update(EntidadeDominio entidade) {
            var empresa = (Empresa)entidade;

            var empresaDB = await _context.Empresas.FirstOrDefaultAsync(emp => emp.Id == empresa.Id);

            if (empresaDB != null) {
                var UsuariosSelecionadosId = empresa.UsusariosVinculados.Select(user => user.Id).ToList();

                var usuariosDB = await _context.Usuarios.Where(u => UsuariosSelecionadosId.Contains(u.Id)).ToListAsync();

                empresaDB.UsusariosVinculados.Clear();

                empresaDB.UsusariosVinculados.AddRange(usuariosDB);
            }

            await _context.SaveChangesAsync();
        }
    }
}
