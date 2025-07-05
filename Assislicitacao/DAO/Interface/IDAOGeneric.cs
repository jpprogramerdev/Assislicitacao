using Assislicitacao.Models;

namespace Assislicitacao.DAO.Interface {
    public interface IDAOGeneric {
        public IEnumerable<EntidadeDominio> SelectAll();
        public void Insert(EntidadeDominio entidade);
        public void Delete(EntidadeDominio entidade);
        public void Update(EntidadeDominio entidade);
    }
}
