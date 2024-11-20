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

        public IActionResult Cadastrar() {
            if (User.Identity.IsAuthenticated) {
                Usuario Usuario = new Usuario();
                Usuario.Id = int.Parse(User.FindFirst("Id").Value);
                Usuario.Nome = User.FindFirst("Nome").Value;

                Usuario.Tipo = new TipoUsuario();
                Usuario.Tipo.Id = int.Parse(User.FindFirst("TipoId").Value);
                Usuario.Tipo.Tipo = User.FindFirst("Tipo").Value;

                if(Usuario.Tipo.Id > 3) {
                    return View("MinhaAgenda");
                }

                return View();
            }

            TempData["AutenticacaoNecessaria"] = "Você deve está autenticado para acessar o sistema";
            return RedirectToAction("Login", "Login");
        }

        public IActionResult ExibirTodosUsuarios() {
            if (User.Identity.IsAuthenticated) {
                Usuario Usuario = new Usuario();
                IFacadeGeneric facadeUsuario = new FacadeUsuario();

                List<Usuario> ListUsuarios = facadeUsuario.SelecionarTodos().Cast<Usuario>().ToList();

                return View(ListUsuarios);
            }

            TempData["AutenticacaoNecessaria"] = "Você deve está autenticado para acessar o sistema";
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult Salvar(Usuario Usuario) {
            if (User.Identity.IsAuthenticated) {
                IFacadeGeneric facadeEmail = new FacadeEmail();
                IFacadeGeneric facadeUsuario = new FacadeUsuario();

                facadeEmail.Salvar(Usuario.Email);

                if (facadeUsuario.Salvar(Usuario)) {
                    TempData["SucessoCadastrarUsuario"] = "Usuario cadastrado com sucesso";
                } else {
                    TempData["FalhaCadastrarUsuario"] = "Falha ao cadastrar usuario";
                }

                return View("Cadastrar");
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
