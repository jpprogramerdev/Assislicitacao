using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOEndereco : IDAOGeneric {
        public IFacadeDatabase database { get; set; }
        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            string Insert = "INSERT INTO ENDERECOS(END_LOGRADOURO, END_NUMERO, END_CEP, END_COMPLEMENTO, END_BAIRRO, END_CID_ID) " +
                            "VALUES(@Logradouro, @Numero, @CEP, @Complemento, @Bairro, @CidadeId);";

            database = new FacadeSQLServer();
            Endereco Endereco = (Endereco)entidade;

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new SqlCommand(Insert, conn)) {
                        query.Parameters.AddWithValue("@Logradouro", Endereco.Logradouro);
                        query.Parameters.AddWithValue("@Numero", Endereco.Numero);
                        query.Parameters.AddWithValue("@CEP", Endereco.CEP);
                        query.Parameters.AddWithValue("@Bairro", Endereco.Bairro);
                        query.Parameters.AddWithValue("@CidadeId", Endereco.Cidade.Id);

                        if (string.IsNullOrEmpty(Endereco.Complemento)) {
                            query.Parameters.AddWithValue("@Complemento", DBNull.Value);
                        } else {
                            query.Parameters.AddWithValue("@Complemento", Endereco.Complemento);
                        }

                        query.ExecuteNonQuery();
                    }
                    database.FecharConexao(conn);
                }
                return true;
            } catch (Exception ex) {
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
