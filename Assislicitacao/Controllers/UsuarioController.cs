using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Assislicitacao.Controllers {
    public class UsuarioController : Controller {
        private readonly IFacadeTipoUsuario _facadeTipoUsuario;
        private readonly IFacadeUsuario _facadeUsuario;
        private readonly IEnumerable<IStrategyFiltro> _filtros;

        public UsuarioController(IFacadeTipoUsuario facadeTipoUsuario, IFacadeUsuario facadeUsuario, IEnumerable<IStrategyFiltro> filtros) {
            _facadeTipoUsuario = facadeTipoUsuario;
            _facadeUsuario = facadeUsuario;
            _filtros = filtros;
        }

        public async Task<IActionResult> MeuPerfil() {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }
            var usuario = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("usuarioId"));
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarFotoPerfil(Usuario Usuario) {
            IStrategy GerarCaminhoImagem = new GerarCaminhoImagem();
            GerarCaminhoImagem.Executar(Usuario);

            var UsuarioSession = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("usuarioId"));

            if(UsuarioSession == null) {
                TempData["FalhaFotoPerfil"] = "Falha ao atualizar a foto de perfil";
                return RedirectToAction("MeuPerfil", "Usuario");
            }

            UsuarioSession.FotoPerfilUrl = Usuario.FotoPerfilUrl;

            try {
                _facadeUsuario.Atualizar(UsuarioSession);
                HttpContext.Session.SetString("usuarioFoto", UsuarioSession.FotoPerfilUrl);
                TempData["SucessoFotoPerfil"] = "Sucesso ao atualizar a foto de perfil";
            } catch (Exception ex) {
                TempData["FalhaFotoPerfil"] = "Falha ao atualizar a foto de perfil";
            }

            return RedirectToAction("MeuPerfil", "Usuario");
        }

        public async Task<IActionResult> RegistrarUsuario() {
            var TiposUsuario = (await _facadeTipoUsuario.Selecionar()).Cast<TipoUsuario>();

            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TiposUsuario = TiposUsuario.Where(tpu => tpu.Tipo == "OPERADOR DE LICITAÇÕES");
            }

            var usuarioViewModel = new UsuarioViewModel {
                Usuario = new(),
                TipoUsuario = TiposUsuario
            };

            return View(usuarioViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ExibirUsuarios(string? filtroTipoUsuario, string? filtroNomeUsuario, string? filtroEmail) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }
            
            if(HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            var filtrarUsuarios = _filtros.OfType<FiltrarUsuarios>().FirstOrDefault();

            var todosUsuarioViewModel = new TodosUsuariosViewModel { 
                Usuarios = (await _facadeUsuario.Selecionar()).Cast<Usuario>().ToList(),
                TipoUsuarios = (await _facadeTipoUsuario.Selecionar()).Cast<TipoUsuario>().ToList()
            };

            todosUsuarioViewModel.Usuarios = filtrarUsuarios.Executar(
                todosUsuarioViewModel.Usuarios,
                ("tipo", filtroTipoUsuario ?? string.Empty),
                ("nome", filtroNomeUsuario ?? string.Empty),
                ("email", filtroEmail ?? string.Empty)
            );

            return View(todosUsuarioViewModel);
        }

        
        public IActionResult PesquisarUsuariosPorEmail(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") == "OPERADOR DE LICITAÇÕES") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            var UsuarioVinculadoEmpresa = new UsuarioVinculadoEmpresaViewModel {
                Empresa = new Empresa {
                    Id = id
                }
            };

            return View(UsuarioVinculadoEmpresa);
        }

        [HttpPost]
        public async Task<IActionResult> BuscarUsuario(UsuarioVinculadoEmpresaViewModel UsuarioVinculadoEmpresa) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") == "OPERADOR DE LICITAÇÕES") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            var usuarioFiltrado = new UsuarioVinculadoEmpresaViewModel {
                Usuario = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Email == UsuarioVinculadoEmpresa.Usuario.Email),
                Empresa = UsuarioVinculadoEmpresa.Empresa
            };

            if(usuarioFiltrado.Usuario == null) {
                TempData["UsuarioNaoEncontrado"] = "Usuário não encontrado";
                return RedirectToAction("PesquisarUsuariosPorEmail","Usuario");
            }

            var settings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            TempData["UsuarioEmpresaJson"] = JsonConvert.SerializeObject(usuarioFiltrado, settings);

            return RedirectToAction("ConfirmarVincularUsuarios","Empresa");
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuario(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            var usuarioViewModel = new UsuarioViewModel {
                Usuario = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Id == id),
                TipoUsuario =(await _facadeTipoUsuario.Selecionar()).Cast<TipoUsuario>()
            };

            return View(usuarioViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarExclusaoUsuario(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            var Usuario = (await _facadeUsuario.Selecionar()).Cast<Usuario>().FirstOrDefault(u => u.Id == id);

            return View(Usuario);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarUsuario(UsuarioViewModel UsuarioViewModel) {
            if (HttpContext.Session.GetInt32("usuarioId") != null && HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {

                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            var Usuario = UsuarioViewModel.Usuario;

            IStrategy CriptografarSenha = new CriptografarSenha();

            try {
                CriptografarSenha.Executar(Usuario);

                Usuario.EmpresasVinculadas = new List<Empresa>();

                Usuario.FotoPerfilUrl = "/FotosPerfil/fotoPerfilPadrao.jpg";

                await _facadeUsuario.Inserir(Usuario);
                TempData["SucessoCadastro"] = "Sucesso ao cadastrar o usuário";

                if (HttpContext.Session.GetInt32("usuarioId") == null) {
                    TempData["SucessoCadastroLogin"] = "Sucesso ao se cadastrar. Por favor, efetue o Login";
                    return RedirectToAction("Login", "Login");
                }

            } catch (Exception ex) {
                TempData["FalhaCadastro"] = "Falha ao cadastrar o usuário";
            }

            return RedirectToAction("RegistrarUsuario", "Usuario");
        }

        [HttpPost]
        public IActionResult AtualizarUsuario(Usuario Usuario) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

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
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

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
