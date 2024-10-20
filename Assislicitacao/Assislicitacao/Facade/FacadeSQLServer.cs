using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using System.Data.SqlClient;

namespace Assislicitacao.Facade {
    public class FacadeSQLServer : IFacadeDatabase {
        
        public SqlConnection AbrirConexao() {
            IDAODatabase SQLServer = new DAOSQLServer();
            return SQLServer.Open();
        }

        public void FecharConexao(SqlConnection conn) {
            IDAODatabase SQLServer = new DAOSQLServer();
            SQLServer.Close(conn);
        }
    }
}
