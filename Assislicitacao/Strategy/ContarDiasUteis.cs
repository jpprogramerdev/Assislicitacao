using Assislicitacao.DTO.APIResponse;
using Assislicitacao.Strategy.Interface;

namespace Assislicitacao.Strategy {
    public class ContarDiasUteis : IStrategyDiasUteis {
        public List<DateTime> Executar(int diasUteis, List<FeriadosResponse> Feriados, DateTime DataInicial) {
            int diasAdicionados = 0;
            DateTime dataFinal = DataInicial;
            List<DateTime> Datas = new();
            

            while(diasAdicionados < diasUteis) {
                dataFinal = dataFinal.AddDays(1);

                bool fimDeSemana = dataFinal.DayOfWeek == DayOfWeek.Saturday || dataFinal.DayOfWeek == DayOfWeek.Sunday;
                bool ehFeriado = Feriados.Any(feriado => DateTime.Parse(feriado.date).Date == dataFinal.Date);

                if (!fimDeSemana && !ehFeriado) {
                    diasAdicionados++;
                    Datas.Add(dataFinal);
                }
            }

            return Datas;
        }
    }
}
