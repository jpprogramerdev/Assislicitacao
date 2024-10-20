using Assislicitacao.DAO.Intefaces;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOSQLServer : IDAODatabase {
        public string StrConn { get; private set; }
        public DAOSQLServer() {
            StrConn = "Server=GORDOX\\SQLEXPRESS;Database=Assisliticacao;Integrated Security=SSPI;TrustServerCertificate=True;";
        }

        public SqlConnection Open() {
            try {
                SqlConnection conn = new(StrConn);
                conn.Open();
                return conn;
            } catch (Exception ex) {
                return null;
            }
        }

        public void Close(SqlConnection conn) {
            conn.Close();
        }
    }
}
