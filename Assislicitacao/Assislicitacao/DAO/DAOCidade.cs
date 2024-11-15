using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOCidade : IDAOGeneric {
        public IFacadeDatabase database { get; set; }
        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelectAllWhereId(int id) {
            string Select = "SELECT * FROM CIDADES WHERE CID_EST_ID = @Id";

            List<EntidadeDominio> ListCiaddes = new();

            database = new FacadeSQLServer();


            using(SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    query.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Cidade Cidade = new Cidade {
                                Id = reader.GetInt32(reader.GetOrdinal("CID_ID")),
                                Nome = reader.GetString(reader.GetOrdinal("CID_NOME"))
                            };

                            ListCiaddes.Add(Cidade);
                        }
                    }
                }
                database.FecharConexao(conn);
            }
            return ListCiaddes;
        }

        public EntidadeDominio SelectOneWhereId(int Id) {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
