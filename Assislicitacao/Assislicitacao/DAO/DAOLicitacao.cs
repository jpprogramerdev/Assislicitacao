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

            Licitacao Licitacao = (Licitacao)entidade;

            database = new FacadeSQLServer();

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Delete, conn)) {
                        query.Parameters.AddWithValue("@Id", Licitacao.Id);
                        query.ExecuteNonQuery();
                    }
                    database.FecharConexao(conn);
                }

                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public bool Insert(EntidadeDominio entidade) {
            string Insert = "INSERT INTO LICITACOES(LCT_NUMERO,LCT_OBJETO,LCT_DATA,LCT_ESTIMADO,LCT_CONFIRMACAO,LCT_CID_ID,LCT_PRT_ID,LCT_TPL_ID,LCT_TDP_ID) " +
                            "VALUES(@Numero, @Objeto, @Data, @Estimado, @Confirmacao, @CidadeID, @PortalID, @TipoLicitacaoID, @TipoDisputaID);";


            Licitacao Licitacao = (Licitacao)entidade;

            database = new FacadeSQLServer();

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        query.Parameters.AddWithValue("@Numero", Licitacao.Numero);
                        query.Parameters.AddWithValue("@Objeto", Licitacao.Objeto);
                        query.Parameters.AddWithValue("@Data", Licitacao.Data);
                        query.Parameters.AddWithValue("@Estimado", Licitacao.ValorEstimado);
                        query.Parameters.AddWithValue("@Confirmacao", Licitacao.Confirmado == true? 1: 0);
                        query.Parameters.AddWithValue("@CidadeID", Licitacao.Cidade.Id);
                        query.Parameters.AddWithValue("@PortalID", Licitacao.Portal.Id);
                        query.Parameters.AddWithValue("@TipoLicitacaoID", Licitacao.TipoLicitacao.Id);
                        query.Parameters.AddWithValue("@TipoDisputaID", Licitacao.TipoDisputa.Id);
                        query.ExecuteNonQuery();
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
                            Licitacao Licitacao = new Licitacao {
                                Id = reader.GetInt32(reader.GetOrdinal("LCT_ID")),
                                Numero = reader.GetString(reader.GetOrdinal("LCT_NUMERO")),
                                Objeto = reader.GetString(reader.GetOrdinal("LCT_OBJETO")),
                                Data = reader.GetDateTime(reader.GetOrdinal("LCT_DATA")),
                                ValorEstimado = (double)reader.GetDecimal(reader.GetOrdinal("LCT_ESTIMADO")),
                                Confirmado = reader.GetBoolean(reader.GetOrdinal("LCT_CONFIRMACAO"))
                            };

                            Licitacao.TipoLicitacao = new TipoLicitacao {
                                Sigla = reader.GetString(reader.GetOrdinal("TPL_SIGLA"))
                            };

                            Licitacao.TipoDisputa = new TipoDisputa {
                                Tipo = reader.GetString(reader.GetOrdinal("TDP_TIPO"))
                            };

                            Licitacao.Cidade = new Cidade {
                                Nome = reader.GetString(reader.GetOrdinal("CID_NOME"))
                            };

                            Licitacao.Portal = new Portal {
                                Nome = reader.GetString(reader.GetOrdinal("PRT_NOME")),
                                Link = reader.GetString(reader.GetOrdinal("PRT_LINK"))
                            };

                            Licitacao.Cidade.Estado = new Estado {
                                UF = reader.GetString(reader.GetOrdinal("EST_UF"))
                            };

                            ListLicitacoes.Add(Licitacao);
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

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
