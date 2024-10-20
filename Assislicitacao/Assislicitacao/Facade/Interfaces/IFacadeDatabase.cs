using System.Data.SqlClient;

namespace Assislicitacao.Facade.Interfaces {
    public interface IFacadeSQLServer {
        public SqlConnection AbrirConexao();
        public void FecharConexao(SqlConnection conn);
    }
}
