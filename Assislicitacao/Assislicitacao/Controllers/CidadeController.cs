using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assislicitacao.Controllers {
    public class CidadeController : Controller {
        [HttpGet]
        public JsonResult GetCidadePorEstado(int estadoId) {
            IFacadeGeneric facadeCidade = new FacadeCidade();
            var Cidades = CastingCidade(facadeCidade.SelecionarTodosPeloId(estadoId));
            return Json(Cidades.Select(c => new { c.Id, c.Nome }));
        }

        private List<Cidade> CastingCidade(List<EntidadeDominio> ListEntidade) {
            List<Cidade> Cidades = new();

            foreach(Cidade Cidade in ListEntidade) {
                Cidades.Add(Cidade);
            }

            return Cidades;
        }
    }
}
