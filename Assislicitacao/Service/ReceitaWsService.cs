using Assislicitacao.DTO.APIResponse;

namespace Assislicitacao.Service {
    public class ReceitaWsService {
        private readonly HttpClient _httpClient;

        public ReceitaWsService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<EmpresaReceitaWsResponse> ConsultarCnpj(string cnpj) {
            var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<EmpresaReceitaWsResponse>();
            }
            return null;
        }
    }
}
