using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class UsuarioController : Controller {
        public IActionResult MinhaAgenda() {
            if (User.Identity.IsAuthenticated) {
                return View();
            }

            TempData["AutenticacaoNecessaria"] = "Você deve está autenticado para acessar o sistema";
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public IActionResult GerarAgendaUsuario() {
            try {
                if (User.Identity.IsAuthenticated) {
                    IFacadeGeneric facadeLicitacao = new FacadeLicitacao();

                    List<Licitacao> licitacoes = facadeLicitacao.SelecionarTodos().Cast<Licitacao>().ToList();

                    int usuarioId = int.Parse(User.FindFirst("Id").Value);

                    List<Licitacao> eventos = licitacoes
                        .Where(l => l.Usuario.Id == usuarioId && l.Confirmacao)
                        .ToList();


                    return Json(eventos);
                }
                TempData["AutenticacaoNecessaria"] = "Você deve está autenticado para acessar o sistema";
                return RedirectToAction("Login", "Login");
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GerarRelatorioMinhaAgenda() {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();
            IRelatorioStrategy GerarPDF = new GerarRelatorioLicitacoes();

            List<Licitacao> licitacoes = facadeLicitacao.SelecionarTodos().Cast<Licitacao>().ToList();

            int usuarioId = int.Parse(User.FindFirst("Id").Value);

            List<Licitacao> eventos = licitacoes
                .Where(l => l.Usuario.Id == usuarioId && l.Confirmacao)
                .ToList();


           GerarPDF.Executar(eventos.Cast<EntidadeDominio>().ToList());

            return View("MinhaAgenda");
        }
    }
}
