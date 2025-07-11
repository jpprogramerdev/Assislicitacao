using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadePortalLicitacao : IFacadePortalLicitacao {
        private readonly IDAOPortalLicitacao _daoPortalLicitacao;

        public FacadePortalLicitacao(IDAOPortalLicitacao daoPortalLicitacao) {
            _daoPortalLicitacao = daoPortalLicitacao;
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

        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _daoPortalLicitacao.SelectAll();
    }
}
