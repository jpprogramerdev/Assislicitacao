using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Assislicitacao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Assislicitacao.Controllers {
    public class LicitacaoController : Controller {
        
        public IActionResult Cadastrar() {
            return View();
        }

        public IActionResult VincularLicitacaoComEmpresa() {
            EmpresaLicitacao EmpresaLicitacao = new();
            EmpresaLicitacao.Licitacao =  JsonConvert.DeserializeObject<Licitacao>(TempData["Licitacao"].ToString());
            return View(EmpresaLicitacao);
        }

        public IActionResult ExibirTodasLicitacoes() {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();

            return View(facadeLicitacao.SelecionarTodos().Cast<Licitacao>());
        }

        [HttpGet]
        public IActionResult AtualizarLicitacao(int id) {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();
            List<Licitacao> ListLicitacoes = facadeLicitacao.SelecionarTodos().Cast<Licitacao>().ToList();

            Licitacao Licitacao = ListLicitacoes.FirstOrDefault(l => l.Id == id);
            return View(Licitacao);
        }

        [HttpPost]
        public IActionResult Salvar(Licitacao Licitacao) {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();

            if (!facadeLicitacao.Salvar(Licitacao)) {
                TempData["FalhaCadastroLicitacao"] = "Falha ao cadastrar licitação";
                return RedirectToAction("Cadastrar", "Licitacao");

            }

            TempData["Licitacao"] = JsonConvert.SerializeObject(Licitacao);
            return RedirectToAction("VincularLicitacaoComEmpresa", "Licitacao");
        }

        [HttpGet]
        public IActionResult Apagar(int id) {
            IFacadeGeneric facadelicitacao = new FacadeLicitacao();

            if (facadelicitacao.Apagar(id)) {
                TempData["LicitacaoApagadaSucesso"] = "Licitação apagada com sucesso";
            } else {
                TempData["LicitacaoApagadaFalha"] = "Falha ao apagar licitação";
            }

            return RedirectToAction("ExibirTodasLicitacoes", "Licitacao");
        }

        [HttpPost]
        public IActionResult Atualizar(Licitacao Licitacao) {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();
            facadeLicitacao.Atualizar(Licitacao);
            return RedirectToAction("ExibirTodasLicitacoes", "Licitacao");

        }

        [HttpPost]
        public IActionResult Vincular(EmpresaLicitacao EmpresaLicitacao) {
            IFacadeGeneric facadeEmpresaLicitacao = new FacadeEmpresaLicitacao();

            if (!facadeEmpresaLicitacao.Salvar(EmpresaLicitacao)) {
                TempData["FalhaVinculação"] = "Falha ao vincular empresa com licitação";
                return RedirectToAction("VincularLicitacaoComEmpresa", "Licitacao");
            } else {
                TempData["SucessoVinculação"] = "Sucesso ao cadastrar participação na licitação";
            }

            return RedirectToAction("Cadastrar", "Licitacao");
        }

        [HttpGet]
        public IActionResult AtualizarConfirmacao(int licitacaoId, int empresaId) {
            IFacadeGeneric facadeLicitacao = new FacadeLicitacao();
            IFacadeGeneric facadeEmpresaLicitacao = new FacadeEmpresaLicitacao();

            List<Licitacao> List = facadeLicitacao.SelecionarTodos().Cast<Licitacao>().ToList();

            Licitacao Licitacao = List.FirstOrDefault(L => L.Id == licitacaoId && L.Empresa.Id == empresaId);

            if (Licitacao != null) {
                if (facadeEmpresaLicitacao.Atualizar(Licitacao)) {
                    TempData["SucessoAtualizacao"] = Licitacao.Confirmacao == true ? "Licitação desmarcada com sucesso" : "Licitação confirmada com sucesso";
                    return RedirectToAction("ExibirTodasLicitacoes", "Licitacao");
                }
                TempData["FalhaAtualizacao"] = "Falha ao atualizar confirmação.";
                return RedirectToAction("ExibirTodasLicitacoes", "Licitacao");
            }

            TempData["FalhaAtualizacao"] = "Falha ao atualizar confirmação. Licitação vazia";
            return RedirectToAction("ExibirTodasLicitacoes", "Licitacao");
        }
    }
}
