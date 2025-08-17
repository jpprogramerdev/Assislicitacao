using Assislicitacao.Models;

namespace Assislicitacao.Strategy {
    public interface IStrategyRelatorioEmail{
        public string Gerar(EntidadeDominio entidadeDominio, RelatorioLicitacao filtroRelatorio);
    }
}
