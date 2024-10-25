using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOLicitacao : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelectAllWhereId(int id) {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
