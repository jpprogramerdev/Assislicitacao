using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
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
            try {
                IStrategy CriptografarSenha = new CriptografarSenha();

                var usuarioLogin = new Usuario { Senha = Login.Senha, Email = Login.Email };

                CriptografarSenha.Executar(usuarioLogin);

                var usuarioDB = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Email == usuarioLogin.Email && u.Senha == usuarioLogin.Senha);

                if (usuarioDB != null) {
                    HttpContext.Session.SetInt32("usuarioId", usuarioDB.Id);
                    HttpContext.Session.SetString("usuarioNome", usuarioDB.Nome);
                    HttpContext.Session.SetString("usuarioEmail", usuarioDB.Email);
                    HttpContext.Session.SetString("usuarioTipoUsuario", usuarioDB.TipoUsuario.Tipo);
                    HttpContext.Session.SetString("usuarioFoto", usuarioDB.FotoPerfilUrl);

                    return RedirectToAction("ExibirTodasLicitacao", "Licitacao");
                }
            }catch(Exception ex) {
                TempData["ErroLogin"] = "Email e/ou senha inválidos";
            }
            TempData["ErroLogin"] = "Email e/ou senha inválidos";

            return View("Login");
        }
    }
}
