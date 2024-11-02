using Microsoft.AspNetCore.Mvc;
using Assislicitacao.Models;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Facade;

namespace Assislicitacao.Controllers {
    public class EmpresaController : Controller {
        public IActionResult Cadastrar() {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(Empresa Empresa) {
            IFacadeGeneric facadeEndereco = new FacadeEndereco();
            IFacadeGeneric facadeTelefones = new FacadeTelefone();
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();

            facadeEndereco.Salvar(Empresa.Endereco);
            facadeTelefones.Salvar(Empresa);

            if (facadeEmpresa.Salvar(Empresa)) {
                TempData["SucessoCadastroEmpresa"] = "Sucesso ao cadastrar a empresa";
            }

            return RedirectToAction("Cadastrar", "Empresa");
        }
    }
}
