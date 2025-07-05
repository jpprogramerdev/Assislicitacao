using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class UsuarioViewModel {
        public Usuario Usuario { get; set; }
        public IEnumerable<TipoUsuario> TipoUsuario { get; set; }
    }
}
