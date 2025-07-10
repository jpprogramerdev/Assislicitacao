using Assislicitacao.Models;

namespace Assislicitacao.Facade.Interface {
    public interface IFacadeGeneric {
        public Task<IEnumerable<EntidadeDominio>> Selecionar();
        public Task Inserir (EntidadeDominio entidade);
        public Task Atualizar(EntidadeDominio entidade);
        public Task Apagar(EntidadeDominio entidade);

    }
}
