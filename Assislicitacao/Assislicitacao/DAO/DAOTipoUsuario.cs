using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOTipoUsuario : IDAOGeneric {
        public IFacadeDatabase database {  get; set; }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM TIPOS_USUARIO;" ;

            database = new FacadeSQLServer();

            List<EntidadeDominio> List = new();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            List.Add(new TipoUsuario {
                                Id = reader.GetInt32(reader.GetOrdinal("TPU_Id")),
                                Tipo= reader.GetString(reader.GetOrdinal("TPU_Tipo")),
                            });
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

        public EntidadeDominio SelectOneWhereId(int Id) {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
