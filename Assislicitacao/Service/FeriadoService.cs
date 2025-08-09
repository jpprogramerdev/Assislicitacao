using Assislicitacao.DTO.APIResponse;
using System.Collections;
using System.Text.Json;

namespace Assislicitacao.Service {
    public class FeriadoService {
        private readonly HttpClient _httpClient;
        public FeriadoService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<FeriadosResponse>> ObterFeriados() {
            DateTime dataAtual = DateTime.Now;

            string year = dataAtual.Year.ToString();

            var url = $"https://brasilapi.com.br/api/feriados/v1/{year}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var json = await response.Content.ReadAsStringAsync();

                var feriados = JsonSerializer.Deserialize<List<FeriadosResponse>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return feriados ?? new List<FeriadosResponse>();
            }

            return new List<FeriadosResponse>();
        }
    }
}
