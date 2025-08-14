using Assislicitacao.Models;

namespace Assislicitacao.Facade.Interface {
    public interface IFacadeLicitacao : IFacadeGeneric{
        public Task AtualizarConfirmacao(EntidadeDominio entidade);
        public Task SalvarVitoriaLicitacao(EntidadeDominio entidade);
    }
}
