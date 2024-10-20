using Assislicitacao.Models;

namespace Assislicitacao.Facade.Interfaces {
    public interface IFacadeGeneric {
        public bool Salvar(EntidadeDominio entidade);
        public List<EntidadeDominio> SelecionarTodos();
        public List<EntidadeDominio> SelecionarTodosPeloId(int Id);
    }
}
