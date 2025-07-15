using Assislicitacao.Exceptions;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
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


        public LicitacaoController(IFacadeEmpresa facadeEmpresa, IFacadeTipoLicitacao facadeTipoLicitacao, IFacadePortalLicitacao facadePortalLicitacao, IFacadeLicitacao facadeLicitacao) {
            _facadeEmpresa = facadeEmpresa;
            _facadeTipoLicitacao = facadeTipoLicitacao;
            _facadePortalLicitacao = facadePortalLicitacao;
            _facadeLicitacao = facadeLicitacao;
        }

        public async Task<IActionResult> ExibirTodasLicitacao() {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var licitacoes = await _facadeLicitacao.Selecionar();
            return View(licitacoes);
        }

        public async Task<IActionResult> CadastrarLicitacao() {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var LicitacaoViewModel = new LicitacaoViewModel {
                Empresas = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().ToList(),
                TiposLicitacao = (await _facadeTipoLicitacao.Selecionar()).Cast<TipoLicitacao>().ToList(),
                PortaisLicitacoes = (await _facadePortalLicitacao.Selecionar()).Cast<PortalLicitacao>().ToList()
            };

            return View(LicitacaoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditarLicitacao(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var LicitacaoViewModel = new LicitacaoViewModel {
                Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == id),
                Empresas = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().ToList(),
                TiposLicitacao = (await _facadeTipoLicitacao.Selecionar()).Cast<TipoLicitacao>().ToList(),
                PortaisLicitacoes = (await _facadePortalLicitacao.Selecionar()).Cast<PortalLicitacao>().ToList()
            };

            return View(LicitacaoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarLicitacao (int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == id);
            return View(Licitacao);
        }

        [HttpPost]
        public async Task<IActionResult> AtaualizarConfirmacaoLicitacao (Licitacao Licitacao) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
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
                TempData["ErroLogin"] = "É nécessário estar logado";
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
                TempData["SucessoSalvarLicitacao"] = "Sucesso ao salavar Licitação";
            }catch (DataAnteriorADataAtualException dataEx){
                TempData["FalhaSalvarLicitacao"] = dataEx.Message;
            } catch(Exception ex) {
                TempData["FalhaSalvarLicitacao"] = $"Falha ao salavar Licitação: {ex}";
            }

            return RedirectToAction("CadastrarLicitacao", "Licitacao");
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarLicitacao(LicitacaoViewModel LicitacaoViewModel) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É nécessário estar logado";
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
