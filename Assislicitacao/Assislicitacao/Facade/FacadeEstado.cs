using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeEstado : IFacadeGeneric {
        public IDAOGeneric DAO { get; set; }

        public bool Apagar(int Id) {
            throw new NotImplementedException();
        }

        public bool Atualizar(EntidadeDominio entidadeDominio) {
            throw new NotImplementedException();
        }

        public bool Salvar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelecionarTodos() {
            DAO = new DAOEstado();
            return DAO.Select();
        }

        public List<EntidadeDominio> SelecionarTodosPeloId(int Id) {
            throw new NotImplementedException();
        }
    }
}
