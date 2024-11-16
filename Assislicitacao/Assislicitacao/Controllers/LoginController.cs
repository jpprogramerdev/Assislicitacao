using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class LoginController : Controller {
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(Usuario UsuarioLogin) {
            IFacadeGeneric facadeUsuario = new FacadeUsuario();

            List<Usuario> ListUsuario = facadeUsuario.SelecionarTodos().Cast<Usuario>().ToList();

            Usuario UsuarioEncontrado = ListUsuario.FirstOrDefault(u => u.Email == UsuarioLogin.Email && u.Senha == UsuarioLogin.Senha); 
            
            if(UsuarioEncontrado != null) {
                return RedirectToAction("ExibirTodasLicitacoes", "Licitacao");
            }

            TempData["FalhaLogin"] = "Email e/ou senha invalidos. Tente novamente";
            return View("Login");
        }
    }
}
