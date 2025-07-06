using Assislicitacao.Models;

namespace Assislicitacao.Facade.Interface {
    public interface IFacadeGeneric {
        public IEnumerable<EntidadeDominio> Selecionar();
        public void Inserir (EntidadeDominio entidade);
        public void Atualizar(EntidadeDominio entidade);
        public void Apagar(EntidadeDominio entidade);

    }
}
