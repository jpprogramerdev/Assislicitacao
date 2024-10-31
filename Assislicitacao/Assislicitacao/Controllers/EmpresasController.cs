using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class EmpresasController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
