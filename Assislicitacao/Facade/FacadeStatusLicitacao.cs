using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeStatusLicitacao : IFacadeStatusLicitacao {
        private readonly IDAOStatusLicitacao _daoStatusLicitacao;

        public FacadeStatusLicitacao(IDAOStatusLicitacao daoStatusLicitacao) {
            _daoStatusLicitacao = daoStatusLicitacao;
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

        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _daoStatusLicitacao.SelectAll();
    }
}
