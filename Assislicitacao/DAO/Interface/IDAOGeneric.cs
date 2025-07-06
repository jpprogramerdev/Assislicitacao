using Assislicitacao.Models;

namespace Assislicitacao.DAO.Interface {
    public interface IDAOGeneric {
        public IEnumerable<EntidadeDominio> SelectAll();
        public Task Insert(EntidadeDominio entidade);
        public Task Delete(EntidadeDominio entidade);
        public Task Update(EntidadeDominio entidade);
    }
}
