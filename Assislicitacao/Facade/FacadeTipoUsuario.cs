using Assislicitacao.DAO;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeTipoUsuario : IFacadeTipoUsuario{
        private readonly IDAOTipoUsuario _dao;

        public FacadeTipoUsuario(IDAOTipoUsuario dao) {
            _dao = dao;
        }

        public IEnumerable<EntidadeDominio> Selecionar() => _dao.SelectAll();


        public void Inserir(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Apagar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

    }
}
