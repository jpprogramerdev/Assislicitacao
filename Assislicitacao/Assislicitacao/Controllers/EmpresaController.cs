using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class EmpresaController : Controller {
        public IActionResult Cadastrar() {
            return View();
        }
    }
}
