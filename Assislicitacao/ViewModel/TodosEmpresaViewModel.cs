using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class TodosEmpresaViewModel {
        public List<Empresa> Empresas { get; set; } = new List<Empresa>();
        public List<PorteEmpresa> PorteEmpresas { get; set; } = new List<PorteEmpresa>();
    }
}
