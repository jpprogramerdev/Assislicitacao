using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOLicitacao : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(int id) {
            string Delete = "DELETE LICITACOES WHERE LCT_ID = @Id";

            database = new FacadeSQLServer();

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Delete, conn)) {
                        query.Parameters.AddWithValue("@Id", id);
                        query.ExecuteNonQuery();
                    }
                    database.FecharConexao(conn);
                }

                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool Insert(EntidadeDominio entidade) {
            string Insert = "INSERT INTO LICITACOES(LCT_NUMERO,LCT_OBJETO,LCT_DATA,LCT_ESTIMADO,LCT_CID_ID,LCT_PRT_ID,LCT_TPL_ID,LCT_TDP_ID) OUTPUT INSERTED.LCT_ID " +
                            "VALUES(@Numero, @Objeto, @Data, @Estimado, @CidadeID, @PortalID, @TipoLicitacaoID, @TipoDisputaID);";


            Licitacao Licitacao = (Licitacao)entidade;

            database = new FacadeSQLServer();

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        query.Parameters.AddWithValue("@Numero", Licitacao.Numero);
                        query.Parameters.AddWithValue("@Objeto", Licitacao.Objeto);
                        query.Parameters.AddWithValue("@Data", Licitacao.Data);
                        query.Parameters.AddWithValue("@Estimado", Licitacao.ValorEstimado);
                        query.Parameters.AddWithValue("@CidadeID", Licitacao.Cidade.Id);
                        query.Parameters.AddWithValue("@PortalID", Licitacao.Portal.Id);
                        query.Parameters.AddWithValue("@TipoLicitacaoID", Licitacao.TipoLicitacao.Id);
                        query.Parameters.AddWithValue("@TipoDisputaID", Licitacao.TipoDisputa.Id);
                        Licitacao.Id = (int)query.ExecuteScalar();
                    }
                    database.FecharConexao(conn);
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM vw_LicitacoesDetalhadas;";

            List<EntidadeDominio> ListLicitacoes = new();

            database = new FacadeSQLServer();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            ListLicitacoes.Add(new Licitacao {
                                Id = reader.GetInt32(reader.GetOrdinal("LCT_ID")),
                                Numero = reader.GetString(reader.GetOrdinal("LCT_NUMERO")),
                                Objeto = reader.GetString(reader.GetOrdinal("LCT_OBJETO")),
                                Data = reader.GetDateTime(reader.GetOrdinal("LCT_DATA")),
                                ValorEstimado = (double)reader.GetDecimal(reader.GetOrdinal("LCT_ESTIMADO")),
                                Confirmacao =  reader.GetBoolean(reader.GetOrdinal("LEM_CONFIRMACAO")),
                                TipoLicitacao = new TipoLicitacao {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPL_Id")),
                                    Sigla = reader.GetString(reader.GetOrdinal("TPL_SIGLA"))
                                },
                                TipoDisputa = new TipoDisputa {
                                    Id = reader.GetInt32(reader.GetOrdinal("TDP_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TDP_TIPO"))
                                },
                                Cidade = new Cidade {
                                    Id = reader.GetInt32(reader.GetOrdinal("CID_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("CID_NOME")),
                                    Estado = new Estado {
                                        Id = reader.GetInt32(reader.GetOrdinal("EST_ID")),
                                        UF = reader.GetString(reader.GetOrdinal("EST_UF"))
                                    }
                                },
                                Portal = new Portal {
                                    Id = reader.GetInt32(reader.GetOrdinal("PRT_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("PRT_NOME")),
                                    Link = reader.GetString(reader.GetOrdinal("PRT_LINK"))
                                },
                                Empresa = new Empresa {
                                    Id = reader.GetInt32(reader.GetOrdinal("EMP_Id")),
                                    CNPJ = reader.GetString(reader.GetOrdinal("EMP_CNPJ")),
                                    RazaoSocial = reader.GetString(reader.GetOrdinal("EMP_RAZAO_SOCIAL")),
                                    NomeFantasia = reader.GetString(reader.GetOrdinal("EMP_NOME_FANTASIA"))
                                },
                                Usuario = new Usuario {
                                    Id = reader.GetInt32(reader.GetOrdinal("USU_Id")),
                                    Nome = reader.GetString(reader.GetOrdinal("USU_Nome"))
                                }
                            });

                        }
                    }
                }
                database.FecharConexao(conn);
            }
            return ListLicitacoes;
        }

        public List<EntidadeDominio> SelectAllWhereId(int id) {
            throw new NotImplementedException();
        }

        public EntidadeDominio SelectOneWhereId(int Id) {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            string Update = "UPDATE LICITACOES SET " +
                                "LCT_NUMERO = @Numero," +
                                "LCT_OBJETO = @Objeto," +
                                "LCT_DATA = @Data," +
                                "LCT_ESTIMADO = @Estimado," +
                                "LCT_CID_ID = @CidadeID," +
                                "LCT_PRT_ID = @PortalID," +
                                "LCT_TPL_ID = @TipoLicitacaoID," +
                                "LCT_TDP_ID = @TipoDisputaID " +
                            "WHERE LCT_ID = @Id;";
            Licitacao Licitacao = (Licitacao)entidade;

            database = new FacadeSQLServer();

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Update, conn)) {
                        query.Parameters.AddWithValue("@Numero", Licitacao.Numero);
                        query.Parameters.AddWithValue("@Objeto", Licitacao.Objeto);
                        query.Parameters.AddWithValue("@Data", Licitacao.Data);
                        query.Parameters.AddWithValue("@Estimado", Licitacao.ValorEstimado);
                        query.Parameters.AddWithValue("@CidadeID", Licitacao.Cidade.Id);
                        query.Parameters.AddWithValue("@PortalID", Licitacao.Portal.Id);
                        query.Parameters.AddWithValue("@TipoLicitacaoID", Licitacao.TipoLicitacao.Id);
                        query.Parameters.AddWithValue("@TipoDisputaID", Licitacao.TipoDisputa.Id);
                        query.Parameters.AddWithValue("@Id", Licitacao.Id);
                        query.ExecuteNonQuery();
                    }
                    database.FecharConexao(conn);
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}
