using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class UsuarioVinculadoEmpresaViewModel {
        public Empresa Empresa { get; set; }
        public List<int> UsuariosSelecionadosIds { get; set; } = new();
        public List<Usuario> Usuarios { get; set; } = new();
    }
}
