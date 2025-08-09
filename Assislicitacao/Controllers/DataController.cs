using Assislicitacao.DTO.APIResponse;
using Assislicitacao.Service;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Assislicitacao.Controllers {
    public class DataController : Controller{
        private readonly FeriadoService _feriadoService;

        public DataController(FeriadoService feriadoService) {
            _feriadoService = feriadoService;
        }

        public IActionResult InformarDataInicial() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InformarDataFinal(DataViewModel DataViewModel) {
            IStrategyDiasUteis ContarDiasUteis = new ContarDiasUteis();

            List<FeriadosResponse> listFeriados = (await _feriadoService.ObterFeriados()).ToList();

            List<DateTime> Datas = ContarDiasUteis.Executar(6, listFeriados, DataViewModel.Data);

            return View(Datas);
        }
    }
}
