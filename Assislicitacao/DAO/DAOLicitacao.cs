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
            .ThenInclude(le => le.UsusariosVinculados)
         .Include(l => l.TipoLicitacao)
         .Include(l => l.PortalLicitacao)
         .Include(l => l.StatusLicitacao)
         .Include(l => l.Municipio)
            .ThenInclude(lm => lm.Estado) //Estado do municipío que será a licitação
         .ToListAsync();

        public async Task Update(EntidadeDominio entidade) {
            var LicitacaoAtualizada = (Licitacao)entidade;

            var LicitacaoDB = (await SelectAll()).Cast<Licitacao>().FirstOrDefault(l => l.Id == LicitacaoAtualizada.Id);

            if (LicitacaoDB == null) {
                throw new Exception("Licitação não encontrada");
            }

            LicitacaoDB.Objeto = LicitacaoAtualizada.Objeto;
            LicitacaoDB.Data = LicitacaoAtualizada.Data;
            LicitacaoDB.ValorEstimado = LicitacaoAtualizada.ValorEstimado;

            LicitacaoDB.TipoLicitacaoId = LicitacaoAtualizada.TipoLicitacaoId;
            LicitacaoDB.MunicipioId = LicitacaoAtualizada.MunicipioId;
            LicitacaoDB.PortalLicitacaoId = LicitacaoAtualizada.PortalLicitacaoId;

            LicitacaoDB.StatusLicitacaoId = LicitacaoAtualizada.StatusLicitacaoId;

            LicitacaoDB.Empresas = LicitacaoAtualizada.Empresas;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateConfirmacao(EntidadeDominio entidade) {
            var LicitacaoAtualizada = (Licitacao)entidade;

            var LicitacaoDB = (await SelectAll()).Cast<Licitacao>().FirstOrDefault(l => l.Id == LicitacaoAtualizada.Id);

            if(LicitacaoDB == null) {
                throw new Exception("Licitação não encontrada");
            }

            LicitacaoDB.Empresas.Clear();

            foreach(var LicitacaoEmpresa in LicitacaoAtualizada.Empresas) {
                LicitacaoDB.Empresas.Add(new LicitacaoEmpresa {
                    EmpresaId = LicitacaoEmpresa.EmpresaId,
                    LicitacaoId = LicitacaoEmpresa.LicitacaoId,
                    ConfirmacaoParticipacao = LicitacaoEmpresa.ConfirmacaoParticipacao
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
