using Assislicitacao.Models;

namespace Assislicitacao.Facade.Interface {
    public interface IFacadeEmail {
        public Task EnviarNotificacaoNovaLicitacao(EntidadeDominio entidade);
        public Task EnviarEmail(string destinatario, string assunto, string mensagem);
    }
}
