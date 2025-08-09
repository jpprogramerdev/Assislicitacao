using Assislicitacao.DTO.APIResponse;

namespace Assislicitacao.Strategy.Interface {
    public interface IStrategyDiasUteis {
        public List<DateTime> Executar(int diasUteis, List<FeriadosResponse> Feriados, DateTime DataInicial);
    }
}
