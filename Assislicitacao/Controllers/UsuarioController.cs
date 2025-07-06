using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class UsuarioController : Controller {
        private readonly IFacadeTipoUsuario _facadeTipoUsuario;
        private readonly IFacadeUsuario _facadeUsuario;

        public UsuarioController(IFacadeTipoUsuario facadeTipoUsuario, IFacadeUsuario facadeUsuario) {
            _facadeTipoUsuario = facadeTipoUsuario;
            _facadeUsuario = facadeUsuario;
        }

        public IActionResult RegistrarUsuario() {
            var usuarioViewModel = new UsuarioViewModel {
                Usuario = new(),
                TipoUsuario = _facadeTipoUsuario.Selecionar().Cast<TipoUsuario>()
            };

            return View(usuarioViewModel);
        }

        public IActionResult ExibirUsuarios() {
            var usuarios = _facadeUsuario.Selecionar().Cast<Usuario>();

            return View(usuarios);
        }

        [HttpGet]
        public IActionResult EditarUsuario(int id) {
            var usuarioViewModel = new UsuarioViewModel {
                Usuario = _facadeUsuario.Selecionar().Cast<Usuario>().FirstOrDefault(u => u.Id == id),
                TipoUsuario = _facadeTipoUsuario.Selecionar().Cast<TipoUsuario>()
            };

            return View(usuarioViewModel);
        }

        [HttpGet]
        public IActionResult ConfirmarExclusaoUsuario(int id) {
            var Usuario = _facadeUsuario.Selecionar().Cast<Usuario>().FirstOrDefault(u => u.Id == id);

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
        public IActionResult ExcluirUsuario(Usuario Usuario) {
            try {
                _facadeUsuario.Apagar(Usuario);
                TempData["SucessoExclusao"] = "Sucesso ao excluir o usuario";
            } catch (Exception ex) {
                TempData["FalhaExclusao"] = "Falha ao excluir o usuario";
            }

            return RedirectToAction("ExibirUsuarios", "Usuario");
        }
    }
}
