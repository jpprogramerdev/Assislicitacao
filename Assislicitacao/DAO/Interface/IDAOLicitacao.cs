using Assislicitacao.Models;

namespace Assislicitacao.DAO.Interface {
    public interface IDAOLicitacao: IDAOGeneric {
        public Task UpdateConfirmacao(EntidadeDominio entidade);
    }
}
