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
    }
}
