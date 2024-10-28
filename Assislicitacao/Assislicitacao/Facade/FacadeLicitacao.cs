using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeLicitacao : IFacadeGeneric {
        public IDAOGeneric DAO { get; set; }

        public bool Apagar(int Id) {
            DAO = new DAOLicitacao();
            return DAO.Delete(Id);
        }

        public bool Salvar(EntidadeDominio entidade) {
            DAO = new DAOLicitacao();
            return DAO.Insert(entidade);
        }

        public List<EntidadeDominio> SelecionarTodos() {
            DAO = new DAOLicitacao();
            return DAO.Select();
        }

        public List<EntidadeDominio> SelecionarTodosPeloId(int Id) {
            throw new NotImplementedException();
        }
    }
}
