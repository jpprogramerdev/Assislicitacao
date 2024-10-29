using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class LicitacaoController : Controller {
        
        public IActionResult Cadastrar() {
            return View();
        }
        public IActionResult ExibirTodasLicitacoes() {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();

            return View(facadeLicitacao.SelecionarTodos().Cast<Licitacao>());
        }

        [HttpGet]
        public IActionResult AtualizarLicitacao(int id) {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();
            List<Licitacao> ListLicitacoes = facadeLicitacao.SelecionarTodos().Cast<Licitacao>().ToList();

            Licitacao Licitacao = ListLicitacoes.FirstOrDefault(l => l.Id == id);
            return View(Licitacao);
        }

        [HttpPost]
        public IActionResult Salvar(Licitacao Licitacao) {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();

            if (!facadeLicitacao.Salvar(Licitacao)) {
                TempData["FalhaCadastroLicitacao"] = "Falha ao cadastrar licitação";
            } else {
                TempData["SucessoCadastroLicitacao"] = "Licitação cadastrada com sucesso";
            }

            return RedirectToAction("Cadastrar","Licitacao");
        }

        [HttpGet]
        public IActionResult Apagar(int id) {
            IFacadeGeneric facadelicitacao = new FacadeLicitacao();

            if (facadelicitacao.Apagar(id)) {
                TempData["LicitacaoApagadaSucesso"] = "Licitação apagada com sucesso";
            } else {
                TempData["LicitacaoApagadaFalha"] = "Falha ao apagar licitação";
            }

            return RedirectToAction("ExibirTodasLicitacoes", "Licitacao");
        }
    }
}
