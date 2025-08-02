using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeEstado : IFacadeEstado {
        private readonly IDAOEstado _daoEstado;

        public FacadeEstado(IDAOEstado daoEstado) {
            _daoEstado = daoEstado;
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

        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _daoEstado.SelectAll();
    }
}
