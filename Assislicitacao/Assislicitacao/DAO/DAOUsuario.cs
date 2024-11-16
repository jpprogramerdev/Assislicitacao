using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOUsuario : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM Vw_DadosUsuario;";

            database = new FacadeSQLServer();

            List<EntidadeDominio> ListUsuario = new();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            ListUsuario.Add(new Usuario {
                                Id = reader.GetInt32(reader.GetOrdinal("USU_ID")),
                                Nome = reader.GetString(reader.GetOrdinal("USU_NOME")),
                                Senha = reader.GetString(reader.GetOrdinal("USU_SENHA")),
                                Email = reader.GetString(reader.GetOrdinal("EML_EMAIL")),
                                Tipo = new TipoUsuario {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPU_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                                }
                            });
                        }
                    }
                }
            }
            return ListUsuario;
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
