using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOTipoLicitacao : IDAOGeneric {
        public IFacadeDatabase database { get; set; } 
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM TIPOS_LICITACAO;";

            database = new FacadeSQLServer();

            List<EntidadeDominio> ListaTipos = new();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            TipoLicitacao Tipo = new TipoLicitacao{
                                Id = reader.GetInt32(reader.GetOrdinal("TPL_ID")),
                                Tipo = reader.GetString(reader.GetOrdinal("TPL_TIPO")),
                                Sigla = reader.GetString(reader.GetOrdinal("TPL_SIGLA"))
                            };

                            ListaTipos.Add(Tipo);
                        }
                    }
                    database.FecharConexao(conn);
                }

                return ListaTipos;
            }
        }

        public List<EntidadeDominio> SelectAllWhereId() {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
