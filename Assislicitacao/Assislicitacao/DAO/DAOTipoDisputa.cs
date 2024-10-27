using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOTipoDisputa : IDAOGeneric {
        public IFacadeDatabase database { get; set; }
        
        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM TIPOS_DISPUTA;";

            List<EntidadeDominio> ListaTipo = new();

            database = new FacadeSQLServer();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            TipoDisputa TipoDisputa = new TipoDisputa {
                                Id = reader.GetInt32(reader.GetOrdinal("TDP_ID")),
                                Tipo = reader.GetString(reader.GetOrdinal("TDP_TIPO"))
                            };

                            ListaTipo.Add(TipoDisputa);
                        }
                    }
                }
                database.FecharConexao(conn);
            }

            return ListaTipo;
        }

        public List<EntidadeDominio> SelectAllWhereId(int id) {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
