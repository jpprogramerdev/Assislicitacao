using Assislicitacao.Models;
using Assislicitacao.Strategy.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Assislicitacao.Strategy {
    public class CriptografarSenha : IStrategy {
        public void Executar(EntidadeDominio EntidadeDominio) {
            Usuario Usuario = (Usuario)EntidadeDominio;

            var hash = SHA256.Create();
            var codificcao = new ASCIIEncoding();
            var array = codificcao.GetBytes(Usuario.Senha);

            array = hash.ComputeHash(array);

            var stringSenha = new StringBuilder();

            foreach(var c in array) {
                stringSenha.Append(c.ToString("x2"));
            }
            Usuario.Senha = stringSenha.ToString();
        }
    }
}
