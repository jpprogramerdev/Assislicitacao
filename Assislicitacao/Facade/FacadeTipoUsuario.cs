using Assislicitacao.DAO;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using System.Threading.Tasks;

namespace Assislicitacao.Facade {
    public class FacadeTipoUsuario : IFacadeTipoUsuario{
        private readonly IDAOTipoUsuario _dao;

        public FacadeTipoUsuario(IDAOTipoUsuario dao) {
            _dao = dao;
        }

        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _dao.SelectAll();


        public Task Inserir(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public Task Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public Task Apagar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
