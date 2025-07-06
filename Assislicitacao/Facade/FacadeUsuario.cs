using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeUsuario : IFacadeUsuario {
        private readonly IDAOUsuario _dao;

        public FacadeUsuario(IDAOUsuario dao) {
            _dao = dao;
        }
        
        public IEnumerable<EntidadeDominio> Selecionar() => _dao.SelectAll();

        public void Inserir(EntidadeDominio entidade) =>  _dao.Insert(entidade);

        public void Atualizar(EntidadeDominio entidade) {
            _dao.Update(entidade);
        }

        public void Apagar(EntidadeDominio entidade) {
            _dao.Delete(entidade);
        }
    }
}
