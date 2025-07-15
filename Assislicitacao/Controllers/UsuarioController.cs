using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assislicitacao.Controllers {
    public class UsuarioController : Controller {
        private readonly IFacadeTipoUsuario _facadeTipoUsuario;
        private readonly IFacadeUsuario _facadeUsuario;

        public UsuarioController(IFacadeTipoUsuario facadeTipoUsuario, IFacadeUsuario facadeUsuario) {
            _facadeTipoUsuario = facadeTipoUsuario;
            _facadeUsuario = facadeUsuario;
        }

        public async Task<IActionResult> RegistrarUsuario() {
            if(HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var usuarioViewModel = new UsuarioViewModel {
                Usuario = new(),
                TipoUsuario = (await _facadeTipoUsuario.Selecionar()).Cast<TipoUsuario>()
            };

            return View(usuarioViewModel);
        }

        public async Task<IActionResult> ExibirUsuarios() {
            var usuarios = (await _facadeUsuario.Selecionar()).Cast<Usuario>();

            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuario(int id) {
            var usuarioViewModel = new UsuarioViewModel {
                Usuario = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Id == id),
                TipoUsuario =(await _facadeTipoUsuario.Selecionar()).Cast<TipoUsuario>()
            };

            return View(usuarioViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarExclusaoUsuario(int id) {
            var Usuario = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Id == id);

            return View(Usuario);
        }

        [HttpPost]
        public IActionResult SalvarUsuario(Usuario Usuario) {
            try {
                _facadeUsuario.Inserir(Usuario);
                TempData["SucessoCadastro"] = "Sucesso ao cadastrar o usuario";
            } catch (Exception ex) {
                TempData["FalhaCadastro"] = "Falha ao cadastrar o usuario";
            }
            return RedirectToAction("RegistrarUsuario", "Usuario");
        }

        [HttpPost]
        public IActionResult AtualizarUsuario(Usuario Usuario) {
            try {
                _facadeUsuario.Atualizar(Usuario);
                TempData["SucessoAtualizar"] = "Sucesso ao atualizar o usuario";
            } catch (Exception ex) {
                TempData["FalhaAtualziar"] = "Falha ao atualizar o usuario";
            }

            return RedirectToAction("ExibirUsuarios", "Usuario");
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirUsuario(Usuario Usuario) {
            try {
                await _facadeUsuario.Apagar(Usuario);
                TempData["SucessoExclusao"] = "Sucesso ao excluir o usuario";
            } catch (Exception ex) {
                TempData["FalhaExclusao"] = "Falha ao excluir o usuario";
            }

            return RedirectToAction("ExibirUsuarios", "Usuario");
        }
    }
}
