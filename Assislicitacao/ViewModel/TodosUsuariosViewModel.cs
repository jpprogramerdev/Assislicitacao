using Assislicitacao.Models;

namespace Assislicitacao.ViewModel {
    public class TodosUsuariosViewModel {
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<TipoUsuario> TipoUsuarios { get; set; } = new List<TipoUsuario>();
    }
}
