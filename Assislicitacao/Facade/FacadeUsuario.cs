using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeUsuario : IFacadeUsuario {
        private readonly IDAOUsuario _dao;

        public FacadeUsuario(IDAOUsuario dao) {
            _dao = dao;
        }
        
        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _dao.SelectAll();

        public async Task Inserir(EntidadeDominio entidade) =>  await _dao.Insert(entidade);

        public async Task Atualizar(EntidadeDominio entidade) {
           await _dao.Update(entidade);
        }

        public async Task Apagar(EntidadeDominio entidade) {
            await _dao.Delete((Usuario)entidade);
        }
    }
}
