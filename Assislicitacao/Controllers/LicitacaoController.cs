using Assislicitacao.Exceptions;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Service;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class LicitacaoController : Controller {
        private readonly IFacadeEmpresa _facadeEmpresa;
        private readonly IFacadeTipoLicitacao _facadeTipoLicitacao;
        private readonly IFacadePortalLicitacao _facadePortalLicitacao;
        private readonly IFacadeLicitacao  _facadeLicitacao;
        private readonly IFacadeEmail _facadeEmail;
        private readonly IFacadeEstado _facadeEstado;


        public LicitacaoController(IFacadeEmpresa facadeEmpresa, IFacadeTipoLicitacao facadeTipoLicitacao, IFacadePortalLicitacao facadePortalLicitacao, IFacadeLicitacao facadeLicitacao, IFacadeEmail facadeEmail, IFacadeEstado facadeEstado) {
            _facadeEmpresa = facadeEmpresa;
            _facadeTipoLicitacao = facadeTipoLicitacao;
            _facadePortalLicitacao = facadePortalLicitacao;
            _facadeLicitacao = facadeLicitacao;
            _facadeEmail = facadeEmail;
            _facadeEstado = facadeEstado;
        }

        public async Task<IActionResult> ExibirTodasLicitacao() {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var usuarioId = HttpContext.Session.GetInt32("usuarioId");

            var licitacoes = await _facadeLicitacao.Selecionar();

            var licitacoesFiltro = new List<Licitacao>();

            foreach(Licitacao Licitacao in licitacoes) {
                foreach(LicitacaoEmpresa LicitacaoEmpresa in Licitacao.Empresas) {
                    if(LicitacaoEmpresa.Empresa.UsusariosVinculados.Any(u => u.Id == usuarioId)) {
                        licitacoesFiltro.Add(Licitacao);
                    }
                    break;
                }
            }

            return View(licitacoesFiltro);
        }

        public async Task<IActionResult> CadastrarLicitacao() {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Usuario = new Usuario();
            Usuario.Id = (int)HttpContext.Session.GetInt32("usuarioId");

            var LicitacaoViewModel = new LicitacaoViewModel {
                Empresas = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().Where(user => user.UsusariosVinculados.Any(u => u.Id == Usuario.Id)).ToList(),
                TiposLicitacao = (await _facadeTipoLicitacao.Selecionar()).Cast<TipoLicitacao>().ToList(),
                PortaisLicitacoes = (await _facadePortalLicitacao.Selecionar()).Cast<PortalLicitacao>().ToList(),
                Estados = (await _facadeEstado.Selecionar()).Cast<Estado>().ToList()
            };

            return View(LicitacaoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditarLicitacao(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Usuario = new Usuario();
            Usuario.Id = (int)HttpContext.Session.GetInt32("usuarioId");

            var LicitacaoViewModel = new LicitacaoViewModel {
                Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == id),
                Empresas = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().Where(user => user.UsusariosVinculados.Any(u => u.Id == Usuario.Id)).ToList(),
                TiposLicitacao = (await _facadeTipoLicitacao.Selecionar()).Cast<TipoLicitacao>().ToList(),
                PortaisLicitacoes = (await _facadePortalLicitacao.Selecionar()).Cast<PortalLicitacao>().ToList()
            };

            return View(LicitacaoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarLicitacao (int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == id);
            return View(Licitacao);
        }

        [HttpPost]
        public async Task<IActionResult> AtaualizarConfirmacaoLicitacao (Licitacao Licitacao) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            try {
                await _facadeLicitacao.AtualizarConfirmacao(Licitacao);
                TempData["SucessoAtualizarLicitacao"] = "Sucesso ao atualizar a licitação";
            } catch(Exception ex) {
                TempData["ErrorAtualizarLicitcao"] = ex.Message;
            }
            return RedirectToAction("ExibirTodasLicitacao", "Licitacao");
        }

        [HttpPost]
        public async Task<IActionResult> SalvarLicitacao(LicitacaoViewModel LicitacaoViewModel) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Licitacao = LicitacaoViewModel.Licitacao;

            IStrategy VerificarData = new VerificarData(); 

            try {
                VerificarData.Executar(Licitacao);

                Licitacao.Empresas = new List<LicitacaoEmpresa>();

                foreach (var empresaId in LicitacaoViewModel.EmpresasSelecionadasIds) {
                    var EmpresaSelcionada = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(emp => emp.Id == empresaId);

                    if (EmpresaSelcionada != null) {
                        Licitacao.Empresas.Add(new LicitacaoEmpresa { Empresa = EmpresaSelcionada});
                    }
                }

                Licitacao.Municipio.Nome = Licitacao.Municipio.Nome.ToUpper().Trim();
                Licitacao.StatusLicitacaoId = 1;

                await _facadeLicitacao.Inserir(Licitacao);
                

                Licitacao.TipoLicitacao = (await _facadeTipoLicitacao.Selecionar()).Cast<TipoLicitacao>().FirstOrDefault(tpl => tpl.Id == Licitacao.TipoLicitacaoId);
                Licitacao.PortalLicitacao = (await _facadePortalLicitacao.Selecionar()).Cast<PortalLicitacao>().FirstOrDefault(ptl => ptl.Id == Licitacao.PortalLicitacaoId);

                await _facadeEmail.EnviarNotificacaoNovaLicitacao(Licitacao);

                TempData["SucessoSalvarLicitacao"] = "Sucesso ao salavar Licitação";
            } catch (DataAnteriorADataAtualException dataEx){
                TempData["FalhaSalvarLicitacao"] = dataEx.Message;
            } catch(Exception ex) {
                TempData["FalhaSalvarLicitacao"] = $"Falha ao salavar Licitação: {ex}";
            }

            return RedirectToAction("CadastrarLicitacao", "Licitacao");
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarLicitacao(LicitacaoViewModel LicitacaoViewModel) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Licitacao = LicitacaoViewModel.Licitacao;
            
            IStrategy VerificarData = new VerificarData();

            Licitacao.Empresas = new List<LicitacaoEmpresa>();

            try {
                VerificarData.Executar(Licitacao);

                foreach (var empresaId in LicitacaoViewModel.EmpresasSelecionadasIds) {
                    var EmpresaSelcionada = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(emp => emp.Id == empresaId);

                    if (EmpresaSelcionada != null) {
                        Licitacao.Empresas.Add(new LicitacaoEmpresa { Empresa = EmpresaSelcionada });
                    }
                }

                Licitacao.Municipio.Nome = Licitacao.Municipio.Nome.ToUpper().Trim();
                
                await _facadeLicitacao.Atualizar(Licitacao);
                TempData["SucessoAtualizarLicitacao"] = "Sucesso ao atualizar a licitação";
            } catch (DataAnteriorADataAtualException dataEx) {
                TempData["ErrorAtualizarLicitcao"] = dataEx.Message;
            } catch (Exception ex) {
                TempData["ErrorAtualizarLicitcao"] = $"Falha ao salavar Licitação: {ex}";
            }
            return RedirectToAction("ExibirTodasLicitacao", "Licitacao");
        }
    }
}
