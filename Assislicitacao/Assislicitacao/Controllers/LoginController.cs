using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assislicitacao.Controllers {
    public class LoginController : Controller {
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(Usuario UsuarioLogin) {
            IFacadeGeneric facadeUsuario = new FacadeUsuario();

            List<Usuario> ListUsuario = facadeUsuario.SelecionarTodos().Cast<Usuario>().ToList();

            Usuario UsuarioEncontrado = ListUsuario.FirstOrDefault(u => u.Email.EnderecoEmail == UsuarioLogin.Email.EnderecoEmail && u.Senha == UsuarioLogin.Senha); 
            
            if(UsuarioEncontrado != null) {
                var Claims = new List<Claim> {
                    new Claim("Nome", UsuarioEncontrado.Nome),
                    new Claim("Id",UsuarioEncontrado.Id.ToString()),

                    new Claim("TipoId", UsuarioEncontrado.Tipo.Id.ToString()),
                    new Claim("Tipo", UsuarioEncontrado.Tipo.Tipo)
                };

                var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("ExibirTodasLicitacoes", "Licitacao");
            }

            TempData["FalhaLogin"] = "Email e/ou senha invalidos. Tente novamente";
            return View("Login");
        }

        public IActionResult Desconectar() {
            HttpContext.SignOutAsync();
            return View("Login");
        }
    }
}
