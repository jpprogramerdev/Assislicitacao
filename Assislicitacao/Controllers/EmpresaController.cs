using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class EmpresaController : Controller {
        private readonly IFacadeEmpresa _facadeEmpresa;

        public EmpresaController(IFacadeEmpresa facadeEmpresa) {
            _facadeEmpresa = facadeEmpresa;
        }

        public IActionResult ConsultarCNPJ() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExibirCNPJ(Empresa Empresa) {
            var empresa = await _facadeEmpresa.ObterEmpresaCNPJ(Empresa.CNPJ);

            return View(empresa);
        }
    }
}
