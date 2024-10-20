using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOEstado : IDAOGeneric {
        public IFacadeDatabase database { get; set; }
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM ESTADOS;";

            List<EntidadeDominio> ListEstados = new();

            database = new FacadeSQLServer();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Estado Estado = new Estado {
                                Id = reader.GetInt32(reader.GetOrdinal("EST_ID")),
                                Nome = reader.GetString(reader.GetOrdinal("EST_NOME")),
                                UF = reader.GetString(reader.GetOrdinal("EST_UF"))
                            };

                            ListEstados.Add(Estado);
                        }
                    }
                }
                database.FecharConexao(conn);
            }
            return ListEstados;
        }

        public List<EntidadeDominio> SelectAllWhereId() {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
