using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class LicitacaoController : Controller {
        
        public IActionResult Cadastrar() {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(Licitacao Licitacao) {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();

            if (!facadeLicitacao.Salvar(Licitacao)) {
                TempData["FalhaCadastroLicitacao"] = "Falha ao cadastrar licitação";
            } else {
                TempData["SucessoCadastroLicitacao"] = "Licitação cadastrada com sucesso";
            }

            return View("Cadastrar");
        }
    }
}
