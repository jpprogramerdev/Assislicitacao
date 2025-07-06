using Assislicitacao.DTO.APIResponse;

namespace Assislicitacao.Facade.Interface {
    public interface IFacadeEmpresa : IFacadeGeneric {
        public Task<EmpresaReceitaWsResponse> ObterEmpresaCNPJ(string cnpj);
    }
}
