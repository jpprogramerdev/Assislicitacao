using System.Data.SqlClient;

namespace Assislicitacao.DAO.Intefaces {
    public interface IDAODatabase {
        public SqlConnection Open();
        public void Close(SqlConnection conn);
    }
}
