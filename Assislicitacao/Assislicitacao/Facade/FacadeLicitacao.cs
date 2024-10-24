using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeLicitacao : IFacadeGeneric {
        public IDAOGeneric DAO { get; set; }
        public bool Salvar(EntidadeDominio entidade) {
            DAO = new DAOLicitacao();
            return DAO.Insert(entidade);
        }

        public List<EntidadeDominio> SelecionarTodos() {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelecionarTodosPeloId(int Id) {
            throw new NotImplementedException();
        }
    }
}
