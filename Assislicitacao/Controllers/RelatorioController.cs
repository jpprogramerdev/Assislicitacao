using Assislicitacao.Facade;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assislicitacao.Controllers {
    public class RelatorioController : Controller {

        private readonly IFacadeEmpresa _facadeEmpresa;
        private readonly IStrategyRelatorioPDF _gerarPdf;

        public RelatorioController(IFacadeEmpresa facadeEmpresa, IStrategyRelatorioPDF gerarPdf) {
            _facadeEmpresa = facadeEmpresa;
            _gerarPdf = gerarPdf;
        }

        public async Task<IActionResult> GerarRelatorioLicitacoes(int empresaId) {

            var empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == empresaId);

            var pdfBytes = _gerarPdf.Gerar(empresa);

            Response.Headers.Add("Content-Disposition", "inline; filename=Relatorio.pdf");
            return File(pdfBytes, "application/pdf");
        }
    }
}
