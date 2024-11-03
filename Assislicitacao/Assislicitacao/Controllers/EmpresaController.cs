using Microsoft.AspNetCore.Mvc;
using Assislicitacao.Models;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Facade;

namespace Assislicitacao.Controllers {
    public class EmpresaController : Controller {
        public IActionResult Cadastrar() {
            return View();
        }

        public IActionResult ExibirTodasEmpresas() {
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
            List<Empresa> ListEmpresa = facadeEmpresa.SelecionarTodos().Cast<Empresa>().ToList();

            return View(ListEmpresa);
        }

        [HttpPost]
        public IActionResult Salvar(Empresa Empresa) {
            IFacadeGeneric facadeEndereco = new FacadeEndereco();
            IFacadeGeneric facadeTelefones = new FacadeTelefone();
            IFacadeGeneric facadeEmail = new FacadeEmail();
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
            IFacadeGeneric facadeEmailEmpresa = new FacadeEmailEmpresa();

            facadeEndereco.Salvar(Empresa.Endereco);
            facadeTelefones.Salvar(Empresa);
            facadeEmail.Salvar(Empresa);

            if (facadeEmpresa.Salvar(Empresa) && facadeEmailEmpresa.Salvar(Empresa)) {
                TempData["SucessoCadastroEmpresa"] = "Sucesso ao cadastrar a empresa";
            }

            return RedirectToAction("Cadastrar", "Empresa");
        }
    }
}
