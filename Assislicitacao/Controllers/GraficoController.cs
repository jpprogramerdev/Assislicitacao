using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace Assislicitacao.Controllers {
    public class GraficoController : Controller{
        private readonly IFacadeEmpresa _facadeEmpresa;
        public GraficoController(IFacadeEmpresa facadeEmpresa) {
            _facadeEmpresa = facadeEmpresa;
        }

        public async Task<IActionResult> Grafico(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(emp => emp.Id == id);

            if(empresa == null) {
                return View();
            }

            var licitacoes = empresa.Licitacoes.Where(l => l.ValorGanho != null && l.Licitacao.Data.Year == DateTime.Now.Year).GroupBy(l => l.Licitacao.Data.Month).ToList();

            var meses = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };

            var valoresPorMes = new decimal[12];
            var valorTotal = licitacoes.Sum(g => g.Sum(l => l.ValorGanho));

            foreach (var group in licitacoes) {
                int mesIndex = group.Key - 1;
                valoresPorMes[mesIndex] = group.Sum(l => l.ValorGanho);
            }

            ViewBag.Meses = meses;
            ViewBag.ValoresPorMes = valoresPorMes;
            ViewBag.ValorTotal = valorTotal;

            return View();
        }

    }
}
