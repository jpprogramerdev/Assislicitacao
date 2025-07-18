using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Service;

namespace Assislicitacao.Facade {
    public class FacadeEmail : IFacadeEmail {
        private readonly EmailService _emailService;

        public FacadeEmail(EmailService emailService) {
            _emailService = emailService;
        }

        public async Task EnviarNotificacaoNovaLicitacao(EntidadeDominio entidade) {
            var Licitacao = (Licitacao)entidade;

            foreach (var empresa in Licitacao.Empresas) {
                string assunto = $"{Licitacao.TipoLicitacao.Sigla} {Licitacao.Municipio.Nome.ToUpper()} - {Licitacao.Objeto} - {Licitacao.Data.ToString("dd/MM/yyyy")}";
                string mensagem = $"Olá, <strong>{empresa.Empresa.RazaoSocial.Split(' ')[0]}</strong>. <br>" +
                    $"<br>Foi adicionado uma nova licitação para sua empresa, confira no sistema:<br>" +
                    $"<br>Tipo Licitação: {Licitacao.TipoLicitacao.Tipo}" +
                    $"<br>Objeto : {Licitacao.Objeto}" +
                    $"<br>Municipio / UF : {Licitacao.Municipio}" +
                    $"<br>Data: {Licitacao.Data.ToString("dd/MM/yyyy")}" +
                    $"<br>Portal: <a href='{Licitacao.PortalLicitacao.link}'>{Licitacao.PortalLicitacao.Nome}</a>";
                foreach (var usuario in empresa.Empresa.UsusariosVinculados) {
                    string email = usuario.Email;
                    await _emailService.EnviarEmail(email, assunto, mensagem);
                }
            }
        }
    }
}
