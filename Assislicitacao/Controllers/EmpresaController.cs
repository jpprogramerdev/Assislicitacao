using Assislicitacao.DTO.APIResponse;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Mapper;
using Assislicitacao.Models;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Assislicitacao.Controllers {
    public class EmpresaController : Controller {
        private readonly IFacadeEmpresa _facadeEmpresa;
        private readonly IFacadeUsuario _facadeUsuarios;
        private readonly IFacadeTipoUsuario _facadeTipoUsuario;
        private readonly IFacadeEmail _facadeEmail;

        public EmpresaController(IFacadeEmpresa facadeEmpresa, IFacadeUsuario facadeUsuarios, IFacadeTipoUsuario facadeTipoÚsuario, IFacadeEmail facadeEmail) {
            _facadeEmpresa = facadeEmpresa;
            _facadeUsuarios = facadeUsuarios;
            _facadeTipoUsuario = facadeTipoÚsuario;
            _facadeEmail = facadeEmail;
        }

        public IActionResult ConsultarCNPJ() {
            if (HttpContext.Session.GetInt32("usuarioId") != null && HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        public async Task<IActionResult> ExibirTodasEmpresas() {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Usuario = new Usuario();
            Usuario.Id = (int)HttpContext.Session.GetInt32("usuarioId");

            List<Empresa> ListEmpresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().Where(user => user.UsusariosVinculados.Any(u => u.Id == Usuario.Id)).ToList();

            foreach(Empresa Empresa in ListEmpresa) {
                Empresa.CNPJ = Convert.ToUInt64(Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            }

            return View(ListEmpresa);
        }

        [HttpGet]
        public async Task<IActionResult> ExibirEmpresa(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            Empresa Empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == id);


            return View(Empresa);
        }

        [HttpGet]
        public async Task<IActionResult> EditarEmpresa(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessárioe estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") == "OPERADOR DE LICITAÇÕES") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            Empresa Empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == id);

            return View(Empresa);
        }

        [HttpPost]
        public async Task<IActionResult> ExibirInfomarcoeEmpresaPosConsulta(Empresa Empresa) {
            if (HttpContext.Session.GetInt32("usuarioId") != null && HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            if ((await _facadeEmpresa.Selecionar()).Cast<Empresa>().Any(emp => emp.CNPJ == Empresa.CNPJ)) {
                TempData["CNPJJacCadastrado"] = "CNPJ já cadastrado no sistema";
                return RedirectToAction("ConsultarCNPJ", "Empresa");
            }

            Empresa.CNPJ = Empresa.CNPJ.Replace(".", "");
            Empresa.CNPJ = Empresa.CNPJ.Replace("/", "");
            Empresa.CNPJ = Empresa.CNPJ.Replace("-", "");

            var empresa = await _facadeEmpresa.ObterEmpresaCNPJ(Empresa.CNPJ);

            empresa.TiposUsuario = (await _facadeTipoUsuario.Selecionar()).Cast<TipoUsuario>().Where(tipo => tipo.Tipo == "REPRESENTANTE LEGAL" || tipo.Tipo == "SÓCIO-ADMINISTRADOR").ToList();

            return View(empresa);
        }

        public async Task<IActionResult> EnviarRelatorioLicitacoes(int empresaId) {
            var empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == empresaId);

            string mensagem = $"Relatório de Licitações da empresa {empresa.RazaoSocial}";

            empresa.Licitacoes = empresa.Licitacoes.Where(l => l.Licitacao.Data > DateTime.Now).OrderBy(l => l.Licitacao.Data).ToList();

            foreach(var Licitacao in empresa.Licitacoes) {
                mensagem += $"<br/><br/>{Licitacao.Licitacao.TipoLicitacao.Sigla} {Licitacao.Licitacao.Municipio.Nome} /  {Licitacao.Licitacao.Municipio.Estado.Uf}- {Licitacao.Licitacao.Objeto} - {Licitacao.Licitacao.Data.ToString("dd/MM/yyyy")} <strong>({Licitacao.Licitacao.StatusLicitacao.Status})</strong>";
            }
            
            var email = HttpContext.Session.GetString("usuarioEmail");

            try {
                await _facadeEmail.EnviarEmail(email, "Relatório de Licitações", mensagem);
                TempData["SucessoEnvioEmail"] = "Relatório enviado com sucesso!";
            } catch (Exception ex) {
                TempData["FalhaEnvioEmail"] = "Falha ao enviar o relatório: " + ex.Message;
            }

            return RedirectToAction("ExibirTodasEmpresas", "Empresa");

        }

        public IActionResult ConfirmarVincularUsuarios() {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") == "OPERADOR DE LICITAÇÕES") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            var UsuarioVinculadoEmpresaJson = TempData["UsuarioEmpresaJson"].ToString();

            if(TempData["UsuarioEmpresaJson"] == null) {
                TempData["UsuarioNaoEncontrado"] = "Usuário não encontrado";
                return RedirectToAction("PesquisarUsuariosPorEmail", "Usuario");
            }

            var UsuarioVinculadoEmpresa = JsonConvert.DeserializeObject<UsuarioVinculadoEmpresaViewModel>(UsuarioVinculadoEmpresaJson);

            return View(UsuarioVinculadoEmpresa);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarVinculacao(UsuarioVinculadoEmpresaViewModel UsuarioVinculadoEmpresa) {
            try {
                var Empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(emp => emp.Id == UsuarioVinculadoEmpresa.Empresa.Id);
                var Usuario = (await _facadeUsuarios.Selecionar()).Cast<Usuario>().FirstOrDefault(usu => usu.Id == UsuarioVinculadoEmpresa.Usuario.Id);

                if (Empresa == null) {
                    TempData["FalhaVincular"] = "Empresa não localizada";
                    return RedirectToAction("ExibirTodasEmpresas", "Empresa");
                }

                Empresa.UsusariosVinculados.Add(Usuario);

                await _facadeEmpresa.Atualizar(Empresa);

                TempData["SucessoVincular"] = "Sucesso ao vincular os usuários";
            } catch (Exception ex) {
                TempData["FalhaVincular"] = "Falha ao vincular os usuários";
            }

            return RedirectToAction("ExibirTodasEmpresas", "Empresa");
        }

        [HttpPost]
        public async Task<IActionResult> SalvarEmpresa(EmpresaReceitaWsResponse EmpresaReceitaWsResponse) {
            var usuario = EmpresaReceitaWsResponse.Usuario;

            IStrategy CriptografarSenha = new CriptografarSenha();

            try {
                if ((await _facadeUsuarios.Selecionar()).Cast<Usuario>().Any(u => u.Email == usuario.Email)) {
                    TempData["FalhaSalvarEmpresa"] = "Email do Administrador/Representante Legal já cadastrado";
                    return RedirectToAction("ConsultarCNPJ", "Empresa");
                }

                CriptografarSenha.Executar(usuario);

                await _facadeUsuarios.Inserir(usuario);

                var empresa = EmpresaMapper.ConverteEmpresaResponseToEmpresa(EmpresaReceitaWsResponse);

                empresa.UsusariosVinculados = [usuario];

                await _facadeEmpresa.Inserir(empresa);
                TempData["EmpresaSalva"] = "Empresa cadastrada com sucesso";
            } catch (Exception ex) {
                TempData["FalhaSalvarEmpresa"] = "Falha ao salvar empresa: " + ex.Message;
            }

            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["SucessoCadastroLogin"] = "Sucesso ao se cadastrar. Por favor, efetue o Login";
                return RedirectToAction("Login", "Login");
            }

            return RedirectToAction("ConsultarCNPJ", "Empresa");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarExclusaoEmpresa(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            Empresa Empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == id);

            return View(Empresa);
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirEmpresa (Empresa Empresa) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            if (HttpContext.Session.GetString("usuarioTipoUsuario") != "ADMINISTRADOR DE SISTEMA") {
                TempData["ErroLogin"] = "Você não tem permissão para acessar esta página. Por medidas de segurança será deslogado.";
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }

            await _facadeEmpresa.Apagar(Empresa);
            return RedirectToAction("ExibirTodasEmpresas", "Empresa");
        }
    }
}
