using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class LoginController : Controller {
        private readonly IFacadeUsuario _facadeUsuario;

        public LoginController(IFacadeUsuario facadeUsuario) {
            _facadeUsuario = facadeUsuario;
        }

        public IActionResult Login() {
            return View();
        }

        public IActionResult Logout() {
            HttpContext.Session.Clear();

            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> VerificarLogin(LoginViewModel Login) {
            var usuario = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Email == Login.Email && u.Senha == Login.Senha);

            if(usuario != null) {
                HttpContext.Session.SetInt32("usuarioId", usuario.Id);
                HttpContext.Session.SetString("usuarioNome", usuario.Nome);
                HttpContext.Session.SetString("usuarioEmail", usuario.Email);

                return RedirectToAction("ExibirTodasLicitacao", "Licitacao");
            }

            TempData["ErroLogin"] = "Email e/ou senha inválidos";

            return View("Login");
        }
    }
}
