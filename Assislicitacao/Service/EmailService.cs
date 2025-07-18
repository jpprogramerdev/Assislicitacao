using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace Assislicitacao.Service {
    public class EmailService {
        private readonly string _emailRemetente;
        private readonly string _senha;

        public EmailService(IConfiguration configuration) {
            _emailRemetente = configuration["EMAIL_REMETENTE"];
            _senha = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

            if (string.IsNullOrWhiteSpace(_senha))
                throw new InvalidOperationException("A variável de ambiente 'EMAIL_PASSWORD' não está definida.");
        }

        public async Task EnviarEmail(string destinatario, string assunto, string mensagem) {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailRemetente));
            email.To.Add(MailboxAddress.Parse(destinatario));
            email.Subject = assunto;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mensagem };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailRemetente, _senha);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
