using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOEnquadramento : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM ENQUADRAMENTOS;";

            List<EntidadeDominio> List = new();

            database = new FacadeSQLServer();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Enquadramento Enquadramento = new Enquadramento {
                                Id = reader.GetInt32(reader.GetOrdinal("EQD_ID")),
                                Tipo = reader.GetString(reader.GetOrdinal("EQD_TIPO")),
                                Sigla = reader.GetString(reader.GetOrdinal("EQD_SIGLA"))
                            };

                            List.Add(Enquadramento);
                        }
                    }
                }
                database.FecharConexao(conn);
            }
            return List; 
        }

        public List<EntidadeDominio> SelectAllWhereId(int id) {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
