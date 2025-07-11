using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
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
            var licitacoes = await _facadeLicitacao.Selecionar();
            return View(licitacoes);
        }

        public async Task<IActionResult> CadastrarLicitacao() {
            var LicitacaoViewModel = new LicitacaoViewModel {
                Empresas = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().ToList(),
                TiposLicitacao = (await _facadeTipoLicitacao.Selecionar()).Cast<TipoLicitacao>().ToList(),
                PortaisLicitacoes = (await _facadePortalLicitacao.Selecionar()).Cast<PortalLicitacao>().ToList()
            };

            return View(LicitacaoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarLicitacao(LicitacaoViewModel LicitacaoViewModel) {
            var Licitacao = LicitacaoViewModel.Licitacao;

            try {
                Licitacao.Empresas = new List<Empresa>();

                foreach (var empresaId in LicitacaoViewModel.EmpresasSelecionadasIds) {
                    var EmpresaSelcionada = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(emp => emp.Id == empresaId);

                    if (EmpresaSelcionada != null) {
                        Licitacao.Empresas.Add(EmpresaSelcionada);
                    }
                }

                Licitacao.Municipio.Nome = Licitacao.Municipio.Nome.ToUpper().Trim();
                Licitacao.StatusLicitacaoId = 1;

                await _facadeLicitacao.Inserir(Licitacao);
                TempData["SucessoSalvarLicitacao"] = "Sucesso ao salavar Licitação";
            } catch(Exception ex) {
                TempData["FalhaSalvarLicitacao"] = $"Falha ao salavar Licitação: {ex}";
            }

            return RedirectToAction("CadastrarLicitacao", "Licitacao");

        }
    }
}
