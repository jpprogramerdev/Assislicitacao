using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeEmpresa : IFacadeGeneric {
        public IDAOGeneric DAO { get; set; }

        public bool Apagar(int Id) {
            throw new NotImplementedException();
        }

        public bool Atualizar(EntidadeDominio entidadeDominio) {
            DAO = new DAOEmpresa();
            return DAO.Update(entidadeDominio);
        }

        public bool Salvar(EntidadeDominio entidade) {
            DAO = new DAOEmpresa();
            return DAO.Insert(entidade);
        }

        public List<EntidadeDominio> SelecionarTodos() {
            DAO = new DAOEmpresa();
            return DAO.Select();
        }

        public List<EntidadeDominio> SelecionarTodosPeloId(int Id) {
            throw new NotImplementedException();
        }

        public EntidadeDominio SelecionaUnicoPeloId(int Id) {
            DAO = new DAOEmpresa();
            return DAO.SelectOneWhereId(Id);
        }
    }
}
