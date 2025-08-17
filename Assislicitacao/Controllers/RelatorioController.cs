using Assislicitacao.Facade;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Models.Enum;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assislicitacao.Controllers {
    public class RelatorioController : Controller {

        private readonly IFacadeEmpresa _facadeEmpresa;
        private readonly IFacadeEmail _facadeEmail;
        private readonly IStrategyRelatorioPDF _gerarPdf;
        private readonly IStrategyRelatorioPDF _gerarEmail;

        public RelatorioController(IFacadeEmpresa facadeEmpresa, IStrategyRelatorioPDF gerarPdf, IStrategyRelatorioPDF gerarEmail, IFacadeEmail facadeEmail) {
            _facadeEmpresa = facadeEmpresa;
            _gerarPdf = gerarPdf;
            _gerarEmail = gerarEmail;
            _facadeEmail = facadeEmail;
        }

        public IActionResult SelecionarTipoRelatorio(int empresaId) {
            var relatorioLicitacao = new RelatorioLicitacao {
                EmpresaId = empresaId
            };
            return View(relatorioLicitacao);
        }

        [HttpPost]
        public async Task<IActionResult> GerarRelatorioLicitacoes(RelatorioLicitacao RelatorioLicitacao) {

            var empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == RelatorioLicitacao.EmpresaId);

            if(RelatorioLicitacao.TipoRelatorio == TipoRelatorio.Email) {
                return RedirectToAction("EnviarRelatorioEmail", "Relatorio", new { empresaId = RelatorioLicitacao.EmpresaId });
            }

            var pdfBytes = _gerarPdf.Gerar(empresa,RelatorioLicitacao);

            ViewData["EmpresaRelatorio"] = empresa;

            Response.Headers.Add("Content-Disposition", "inline; filename=Relatorio.pdf");
            return File(pdfBytes, "application/pdf");
        }


        public async Task<IActionResult> EnviarRelatorioEmail(int empresaId) {
            var empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == empresaId);

            var email = HttpContext.Session.GetString("usuarioEmail");

            IStrategyRelatorioEmail gerarEmail = new GerarRelatorioEmail();

            try {
                await _facadeEmail.EnviarEmail(email, "Relatório de Licitações", gerarEmail.Gerar(empresa,null));
                TempData["SucessoEnvioEmail"] = "Relatório enviado com sucesso!";
            } catch (Exception ex) {
                TempData["FalhaEnvioEmail"] = "Falha ao enviar o relatório: " + ex.Message;
            }

            return RedirectToAction("ExibirTodasEmpresas", "Empresa");
        }
    }
}
