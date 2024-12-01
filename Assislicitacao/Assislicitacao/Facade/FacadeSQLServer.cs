using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using System.Data.Common;
using System.Data.SqlClient;

namespace Assislicitacao.Facade {
    public class FacadeSQLServer : IFacadeDatabase {
        
        public DbConnection AbrirConexao() {
            IDAODatabase SQLServer = new DAOSQLServer();
            return SQLServer.Open();
        }

        public void FecharConexao(DbConnection conn) {
            IDAODatabase SQLServer = new DAOSQLServer();
            SQLServer.Close((SqlConnection)conn);
        }
    }
}
