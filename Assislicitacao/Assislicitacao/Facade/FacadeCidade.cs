using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeCidade : IFacadeGeneric {
        public IDAOGeneric DAO { get; set; }
        public bool Salvar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelecionarTodos() {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelecionarTodosPeloId(int Id) {
            DAO = new DAOCidade();
            return DAO.SelectAllWhereId(Id);
        }
    }
}
