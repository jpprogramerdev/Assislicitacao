using Assislicitacao.Models;

namespace Assislicitacao.Strategy.Interface
{
    public interface IRelatorioStrategy{
        public void Executar(List<EntidadeDominio> List);
    }
}
