using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOPortal : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM PORTAIS";

            List<EntidadeDominio> ListaPortais = new();

            database = new FacadeSQLServer();

            using (SqlConnection conn = database.AbrirConexao()) {
                using(SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Portal Portal = new Portal {
                                Id = reader.GetInt32(reader.GetOrdinal("PRT_ID")),
                                Nome = reader.GetString(reader.GetOrdinal("PRT_NOME")),
                                Link = reader.GetString(reader.GetOrdinal("PRT_LINK"))
                            };

                            ListaPortais.Add(Portal);
                        }
                    }
                    database.FecharConexao(conn);
                }
            }
            return ListaPortais;
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
