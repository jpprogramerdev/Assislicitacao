using Microsoft.AspNetCore.Mvc;
using Assislicitacao.Models;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Facade;
using System.Data.SqlClient;
using Assislicitacao.Exceptions;

namespace Assislicitacao.Controllers {
    public class EmpresaController : Controller {
        public IActionResult Cadastrar() {
            return View();
        }

        public IActionResult ExibirTodasEmpresas() {
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
            List<Empresa> ListEmpresa = facadeEmpresa.SelecionarTodos().Cast<Empresa>().ToList();

            return View(ListEmpresa);
        }

        [HttpGet]
        public IActionResult EditarEmpresa(int id) {
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
            List<Empresa> ListEmpresa = facadeEmpresa.SelecionarTodos().Cast<Empresa>().ToList();
            Empresa Empresa = ListEmpresa.FirstOrDefault(e => e.Id == id);

            return View(Empresa);
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

                if (facadeEmpresa.Salvar(Empresa) && facadeEmailEmpresa.Salvar(Empresa)) {
                    TempData["SucessoCadastroEmpresa"] = "Sucesso ao atualizar empresa";
                }
            } catch (DuplicateCNPJException ex){
                TempData["CNPJDuplicado"] = ex.Message;
            }

            return RedirectToAction("Cadastrar", "Empresa");
        }

        [HttpPost]
        public IActionResult Atualizar(Empresa Empresa){
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
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
            IFacadeGeneric facadeEmailEmpresa = new FacadeEmailEmpresa();

            if (facadeEmailEmpresa.Apagar(Id) && facadeEmpresa.Apagar(Id)) {
                TempData["SucessoAcaoEmpresa"] = "Sucesso ao deletar a empresa";
            }

            return RedirectToAction("ExibirTodasEmpresas", "Empresa");

        }
    }
}
