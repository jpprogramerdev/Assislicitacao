using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class CidadeController : Controller {
        public JsonResult GetCidadePorEstado(int estadoId) {
            return View();
        }
    }
}
