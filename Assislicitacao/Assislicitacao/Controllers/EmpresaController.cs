using Microsoft.AspNetCore.Mvc;
using Assislicitacao.Models;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Facade;
using System.Data.SqlClient;
using Assislicitacao.Exceptions;
using Assislicitacao.Strategy.Interface;
using Assislicitacao.Strategy;

namespace Assislicitacao.Controllers {
    public class EmpresaController : Controller {
        public IActionResult Cadastrar() {
            if (User.Identity.IsAuthenticated) {
                return View();
            }

            TempData["AutenticacaoNecessaria"] = "Você deve está autenticado para acessar o sistema";
            return RedirectToAction("Login", "Login");
        }

        public IActionResult ExibirTodasEmpresas() {
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();

            if (User.Identity.IsAuthenticated) {
                List<Empresa> ListEmpresa = facadeEmpresa.SelecionarTodos().Cast<Empresa>().ToList();

                return View(ListEmpresa);
            }

            TempData["AutenticacaoNecessaria"] = "Você deve está autenticado para acessar o sistema";
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public IActionResult EditarEmpresa(int id) {
            if (User.Identity.IsAuthenticated) {
                IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
                List<Empresa> ListEmpresa = facadeEmpresa.SelecionarTodos().Cast<Empresa>().ToList();
                Empresa Empresa = ListEmpresa.FirstOrDefault(e => e.Id == id);

                return View(Empresa);
            }

            TempData["AutenticacaoNecessaria"] = "Você deve está autenticado para acessar o sistema";
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult Salvar(Empresa Empresa) {
            IFacadeGeneric facadeEndereco = new FacadeEndereco();
            IFacadeGeneric facadeTelefones = new FacadeTelefone();
            IFacadeGeneric facadeEmail = new FacadeEmail();
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
            IFacadeGeneric facadeEmailEmpresa = new FacadeEmailEmpresa();

            try {
                facadeEndereco.Salvar(Empresa.Endereco);
                facadeTelefones.Salvar(Empresa);
                facadeEmail.Salvar(Empresa);

                if (facadeEmpresa.Salvar(Empresa)) {
                    facadeEmailEmpresa.Salvar(Empresa);
                    TempData["SucessoCadastroEmpresa"] = "Sucesso ao cadastrar empresa";
                }
            } catch (DuplicateCNPJException ex) {
                TempData["CNPJDuplicado"] = ex.Message;
            }

            return RedirectToAction("Cadastrar", "Empresa");
        }

        [HttpPost]
        public IActionResult Atualizar(Empresa Empresa) {
            IFacadeGeneric facadeEndereco = new FacadeEndereco();
            IFacadeGeneric facadeTelefones = new FacadeTelefone();
            IFacadeGeneric facadeEmail = new FacadeEmail();
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
            IFacadeGeneric facadeEmailEmpresa = new FacadeEmailEmpresa();


            try {
                facadeEndereco.Salvar(Empresa.Endereco);
                facadeTelefones.Salvar(Empresa);
                facadeEmail.Salvar(Empresa);

                if (facadeEmpresa.Atualizar(Empresa) && facadeEmailEmpresa.Atualizar(Empresa)) {
                    TempData["SucessoAcaoEmpresa"] = "Sucesso ao atualizar a empresa";
                }
            } catch (DuplicateCNPJException ex) {
                TempData["CNPJDuplicado"] = ex.Message;
            }

            return RedirectToAction("ExibirTodasEmpresas", "Empresa");
        }

        [HttpGet]
        public IActionResult Apagar(int Id) {
            if (User.Identity.IsAuthenticated) {
                IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
                IFacadeGeneric facadeEmailEmpresa = new FacadeEmailEmpresa();
                IFacadeGeneric facadeLicitacoesEmpresa = new FacadeEmpresaLicitacao();

                if (facadeEmailEmpresa.Apagar(Id) && facadeLicitacoesEmpresa.Apagar(Id) && facadeEmpresa.Apagar(Id)) {
                    TempData["SucessoAcaoEmpresa"] = "Sucesso ao deletar a empresa";
                }

                return RedirectToAction("ExibirTodasEmpresas", "Empresa");
            }
            TempData["AutenticacaoNecessaria"] = "Você deve está autenticado para acessar o sistema";
            return RedirectToAction("Login", "Login");

        }
    }
}
