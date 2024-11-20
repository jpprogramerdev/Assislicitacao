using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOEmail : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            string Insert = "INSERT INTO EMAILS(EML_EMAIL) VALUES (@Email)";

            database = new FacadeSQLServer();

            Email Email = (Email)entidade;
            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        query.Parameters.AddWithValue("@Email", Email.EnderecoEmail);
                        query.ExecuteNonQuery();
                    }
                    database.FecharConexao(conn);
                }
                return true;
            } catch(Exception ex) {
                return false;
            }
        }

        public List<EntidadeDominio> Select() {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelectAllWhereId(int id) {
            throw new NotImplementedException();
        }

        public EntidadeDominio SelectOneWhereId(int Id) {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
