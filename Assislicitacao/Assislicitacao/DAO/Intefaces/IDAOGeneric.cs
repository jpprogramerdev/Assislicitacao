using Assislicitacao.Models;

namespace Assislicitacao.DAO.Intefaces {
    public interface IDAOGeneric {
        public bool Insert(EntidadeDominio entidade);
        public List<EntidadeDominio> Select();
        public bool Delete(int id);
        public bool Update(EntidadeDominio entidade);
        public List<EntidadeDominio> SelectAllWhereId(int id);
    }
}
