using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class CidadeController : Controller {
        [HttpGet]
        public JsonResult GetCidadePorEstado(int estadoId)  {
            IFacadeGeneric facadeCidade = new FacadeCidade();
            var Cidades = facadeCidade.SelecionarTodosPeloId(estadoId).Cast<Cidade>().OrderBy(c => c.Nome);
            return Json(Cidades.Select(c => new { c.Id, c.Nome }));
        }
    }
}
