using Assislicitacao.DTO.APIResponse;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Service;

namespace Assislicitacao.Facade {
    public class FacadeEmpresa : IFacadeEmpresa {
        private readonly ReceitaWsService _receitaService;

        public FacadeEmpresa(ReceitaWsService receitaService) {
            _receitaService = receitaService;
        }

        public void Apagar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Inserir(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public async Task<EmpresaReceitaWsResponse> ObterEmpresaCNPJ(string cnpj) {
            return await _receitaService.ConsultarCnpj(cnpj);
        }

        public IEnumerable<EntidadeDominio> Selecionar() {
            throw new NotImplementedException();
        }
    }
}
