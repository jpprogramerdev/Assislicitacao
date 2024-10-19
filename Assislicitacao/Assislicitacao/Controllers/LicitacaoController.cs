using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class LicitacaoController : Controller {
        public IActionResult Cadastrar() {
            return View();
        }
    }
}
