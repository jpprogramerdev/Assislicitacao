using Assislicitacao.Models;

namespace Assislicitacao.Strategy.Interface {
    public interface IStrategyRelatorioPDF {
        public byte[] Gerar(EntidadeDominio entidadeDominio);
    }
}
