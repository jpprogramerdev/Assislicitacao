using Assislicitacao.DTO.APIResponse;
using Assislicitacao.Models;

namespace Assislicitacao.Facade.Interface {
    public interface IFacadeEmpresa : IFacadeGeneric {
        public Task<EmpresaReceitaWsResponse> ObterEmpresaCNPJ(string cnpj);
    }
}
