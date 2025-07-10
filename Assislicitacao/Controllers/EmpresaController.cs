using Assislicitacao.DTO.APIResponse;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Mapper;
using Assislicitacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assislicitacao.Controllers {
    public class EmpresaController : Controller {
        private readonly IFacadeEmpresa _facadeEmpresa;

        public EmpresaController(IFacadeEmpresa facadeEmpresa) {
            _facadeEmpresa = facadeEmpresa;
        }

        public IActionResult ConsultarCNPJ() {
            return View();
        }

        public async Task<IActionResult> ExibirTodasEmpresas() {
            List<Empresa> ListEmpresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().ToList();

            foreach(Empresa Empresa in ListEmpresa) {
                Empresa.CNPJ = Convert.ToUInt64(Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            }

            return View(ListEmpresa);
        }

        [HttpGet]
        public async Task<IActionResult> ExibirEmpresa(int id) {
            Empresa Empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == id);

            return View(Empresa);
        }

        [HttpGet]
        public async Task<IActionResult> EditarEmpresa(int id) {
            Empresa Empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == id);

            return View(Empresa);
        }

        [HttpPost]
        public async Task<IActionResult> ExibirCNPJ(Empresa Empresa) {
            if ((await _facadeEmpresa.Selecionar()).Cast<Empresa>().Any(emp => emp.CNPJ == Empresa.CNPJ)) {
                TempData["CNPJJacCadastrado"] = "CNPJ já cadastrado no sistema";
                return RedirectToAction("ConsultarCNPJ", "Empresa");
            }

            var empresa = await _facadeEmpresa.ObterEmpresaCNPJ(Empresa.CNPJ);

            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarEmpresa(EmpresaReceitaWsResponse EmpresaReceitaWsResponse) {
            try {
                await _facadeEmpresa.Inserir(EmpresaMapper.ConverteEmpresaResponseToEmpresa(EmpresaReceitaWsResponse));
                TempData["EmpresaSalva"] = "Empresa cadastrada com sucesso";
            } catch (Exception ex) {
                TempData["FalhaSalvarEmpresa"] = "Falha ao salvar empresa: " + ex.Message;
            }
            
            return RedirectToAction("ConsultarCNPJ", "Empresa");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarExclusaoEmpresa(int id) {
            Empresa Empresa = (await _facadeEmpresa.Selecionar()).Cast<Empresa>().FirstOrDefault(e => e.Id == id);

            return View(Empresa);
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirEmpresa (Empresa Empresa) {
            await _facadeEmpresa.Apagar(Empresa);
            return RedirectToAction("ExibirTodasEmpresas", "Empresa");
        }
    }
}
