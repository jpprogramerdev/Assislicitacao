using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeTipoLicitacao : IFacadeTipoLicitacao {
        private readonly IDAOTipoLicitacao _daoTipoLicitacao;

        public FacadeTipoLicitacao(IDAOTipoLicitacao daoTipoLicitacao) {
            _daoTipoLicitacao = daoTipoLicitacao;
        }

        public Task Apagar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public Task Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public Task Inserir(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _daoTipoLicitacao.SelectAll();
    }
}
