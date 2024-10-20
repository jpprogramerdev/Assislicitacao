using System.Data.SqlClient;

namespace Assislicitacao.Facade.Interfaces {
    public interface IFacadeDatabase {
        public SqlConnection AbrirConexao();
        public void FecharConexao(SqlConnection conn);
    }
}
