using Assislicitacao.Exceptions;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Service;
using Assislicitacao.Strategy;
using Assislicitacao.Strategy.Interface;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assislicitacao.Controllers {
    public class LicitacaoController : Controller {
        private readonly IFacadeEmpresa _facadeEmpresa;
        private readonly IFacadeTipoLicitacao _facadeTipoLicitacao;
        private readonly IFacadePortalLicitacao _facadePortalLicitacao;
        private readonly IFacadeLicitacao _facadeLicitacao;
        private readonly IFacadeEmail _facadeEmail;
        private readonly IFacadeEstado _facadeEstado;
        private readonly IFacadeStatusLicitacao _facadeStatusLicitacao;
        private readonly IStrategyFiltro _filtrarLicitacoes;


        public LicitacaoController(IFacadeEmpresa facadeEmpresa, IFacadeTipoLicitacao facadeTipoLicitacao, IFacadePortalLicitacao facadePortalLicitacao, IFacadeLicitacao facadeLicitacao, IFacadeEmail facadeEmail, IFacadeEstado facadeEstado, IFacadeStatusLicitacao facadeStatusLicitacao, IStrategyFiltro filtrarLicitacoes) {
            _facadeEmpresa = facadeEmpresa;
            _facadeTipoLicitacao = facadeTipoLicitacao;
            _facadePortalLicitacao = facadePortalLicitacao;
            _facadeLicitacao = facadeLicitacao;
            _facadeEmail = facadeEmail;
            _facadeEstado = facadeEstado;
            _facadeStatusLicitacao = facadeStatusLicitacao;
            _filtrarLicitacoes = filtrarLicitacoes;
        }


        [HttpGet]
        public async Task<IActionResult> ExibirTodasLicitacao(string? filtroCidade, string? filtroTipoLicitacao, string? filtroData, string? filtroStatusLicitacao, string? filtroObjeto, bool? mostrarSuspensos) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var usuarioId = HttpContext.Session.GetInt32("usuarioId");

            var licitacoes = await _facadeLicitacao.Selecionar();

            var licitacoesFiltro = new List<Licitacao>();

            licitacoesFiltro = _filtrarLicitacoes.Executar(licitacoes.Cast<Licitacao>().ToList(), (int)usuarioId);

            if (mostrarSuspensos != true) {
                licitacoesFiltro = _filtrarLicitacoes.Executar(licitacoesFiltro, new List<string> { "ADJUDICADA/HOMOLAGADA", "REVOGADO", "SUSPENSO" });
            }

            licitacoesFiltro = _filtrarLicitacoes.Executar(licitacoesFiltro,
                ("cidade", filtroCidade ?? string.Empty),
                ("tipo", filtroTipoLicitacao ?? string.Empty),
                ("status", filtroStatusLicitacao ?? string.Empty),
                ("objeto", filtroObjeto ?? string.Empty),
                ("data", filtroData ?? string.Empty)
            );


            var todasLicitacoesViewModel = new TodasLicitacoesViewModel {
                Licitacoes = licitacoesFiltro,
                StatusLicitacoes = (await _facadeStatusLicitacao.Selecionar()).Cast<StatusLicitacao>().ToList(),
                TiposLicitacoes = (await _facadeTipoLicitacao.Selecionar()).Cast<TipoLicitacao>().ToList()
            };



            return View(todasLicitacoesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EscolherEmpresaVitoria(int licitacaoId) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }
            var Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == licitacaoId);
            if (Licitacao == null) {
                TempData["ErroLicitacao"] = "Licitação não encontrada.";
                return RedirectToAction("ExibirTodasLicitacao");
            }
            return View(Licitacao);
        }

        [HttpGet]
        public async Task<IActionResult> ExibirLicitacao(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }
            var Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == id);
            if (Licitacao == null) {
                TempData["ErroLicitacao"] = "Licitação não encontrada.";
                return RedirectToAction("ExibirTodasLicitacao");
            }
            return View(Licitacao);
        }

        public async Task<IActionResult> ConfirmarVitoria(int empresaId, int licitacaoId) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var EmpresaVencedora = new LicitacaoEmpresa {
                Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == licitacaoId),
                Empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == empresaId)
            };

            return View(EmpresaVencedora);
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

        public async Task<IActionResult> AlterarStatus(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Usuario = new Usuario();
            Usuario.Id = (int)HttpContext.Session.GetInt32("usuarioId");


            var LicitacaoViewModel = new LicitacaoViewModel {
                Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == id),
                StatusLicitacao = (await _facadeStatusLicitacao.Selecionar()).Cast<StatusLicitacao>().ToList()
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
        public async Task<IActionResult> ConfirmarLicitacao(int id) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Licitacao = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().FirstOrDefault(l => l.Id == id);
            return View(Licitacao);
        }

        [HttpPost]
        public async Task<IActionResult> AtaualizarConfirmacaoLicitacao(Licitacao Licitacao) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            try {
                await _facadeLicitacao.AtualizarConfirmacao(Licitacao);
                TempData["SucessoAtualizarLicitacao"] = "Sucesso ao atualizar a licitação";
            } catch (Exception ex) {
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
                        Licitacao.Empresas.Add(new LicitacaoEmpresa { Empresa = EmpresaSelcionada });
                    }
                }

                Licitacao.Municipio.Nome = Licitacao.Municipio.Nome.ToUpper().Trim();
                Licitacao.StatusLicitacaoId = 1;

                await _facadeLicitacao.Inserir(Licitacao);


                Licitacao.TipoLicitacao = (await _facadeTipoLicitacao.Selecionar()).Cast<TipoLicitacao>().FirstOrDefault(tpl => tpl.Id == Licitacao.TipoLicitacaoId);
                Licitacao.PortalLicitacao = (await _facadePortalLicitacao.Selecionar()).Cast<PortalLicitacao>().FirstOrDefault(ptl => ptl.Id == Licitacao.PortalLicitacaoId);

                await _facadeEmail.EnviarNotificacaoNovaLicitacao(Licitacao);

                TempData["SucessoSalvarLicitacao"] = "Sucesso ao salavar Licitação";
            } catch (DataAnteriorADataAtualException dataEx) {
                TempData["FalhaSalvarLicitacao"] = dataEx.Message;
            } catch (Exception ex) {
                TempData["FalhaSalvarLicitacao"] = $"Falha ao salavar Licitação: {ex}";
            }

            return RedirectToAction("CadastrarLicitacao", "Licitacao");
        }

        [HttpPost]
        public async Task<IActionResult> SalvarStatusLicitacao(LicitacaoViewModel LicitacaoViewModel) {
            if (HttpContext.Session.GetInt32("usuarioId") == null) {
                TempData["ErroLogin"] = "É necessário estar logado";
                return RedirectToAction("Login", "Login");
            }

            var Licitacao = LicitacaoViewModel.Licitacao;

            Licitacao.Empresas = (await _facadeLicitacao.Selecionar()).Cast<Licitacao>().First(l => l.Id == Licitacao.Id).Empresas;
            try {
                await _facadeLicitacao.Atualizar(Licitacao);
                TempData["SucessoAtualizarLicitacao"] = "Sucesso ao atualizar a licitação";
            } catch (Exception ex) {
                TempData["ErrorAtualizarLicitcao"] = $"Falha ao salavar Licitação: {ex}";
            }

            return RedirectToAction("ExibirTodasLicitacao", "Licitacao");
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

        [HttpPost]
        public async Task<IActionResult> SalvarVitoria(LicitacaoEmpresa LicitacaoEmpresa) {
            await _facadeLicitacao.SalvarVitoriaLicitacao(LicitacaoEmpresa);

            TempData["SucessoAtualizarLicitacao"] = "Parabéns pela vitória na licitação.";

            return RedirectToAction("ExibirTodasLicitacao", "Licitacao");
        }
    }
}
