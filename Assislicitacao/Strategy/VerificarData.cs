using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;
using Assislicitacao.Exceptions;

namespace Assislicitacao.Strategy {
    public class VerificarData : IStrategy {
        public void Executar(EntidadeDominio EntidadeDominio) {
            var Licitacao = (Licitacao)EntidadeDominio;

            DateTime DataHoje = DateTime.Now;

            if(Licitacao.Data < DataHoje) {
                throw new DataAnteriorADataAtualException();
            }
        }
    }
}
