using System.Data.Common;
using System.Data.SqlClient;

namespace Assislicitacao.Facade.Interfaces {
    public interface IFacadeDatabase {
        public DbConnection AbrirConexao();
        public void FecharConexao(DbConnection conn);
    }
}
