using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;

namespace Assislicitacao.Strategy {
    public class GerarCaminhoImagem : IStrategy {
        public void Executar(EntidadeDominio EntidadeDominio) {
            Usuario usuario = (Usuario)EntidadeDominio;

            var caminhoSalvar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FotosPerfil");

            if (!Directory.Exists(caminhoSalvar)) {
                Directory.CreateDirectory(caminhoSalvar);
            }

            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(usuario.FotoPerfil.FileName);

            var caminhoArquivo = Path.Combine(caminhoSalvar, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create)) {
                usuario.FotoPerfil.CopyTo(stream);
            }

            usuario.FotoPerfilUrl = $"/FotosPerfil/{nomeArquivo}";
        }
    }
}
