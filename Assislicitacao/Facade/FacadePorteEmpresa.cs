using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadePorteEmpresa : IFacadePorteEmpresa {
        private readonly IDAOPorteEmpresa _daoPorteEmpresa;
        public FacadePorteEmpresa(IDAOPorteEmpresa daoPorteEmpresa) {
            _daoPorteEmpresa = daoPorteEmpresa;
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

        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _daoPorteEmpresa.SelectAll();
    }
}
