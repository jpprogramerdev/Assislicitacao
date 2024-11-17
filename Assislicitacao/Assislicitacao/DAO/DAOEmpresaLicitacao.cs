using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using Assislicitacao.ViewModel;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOEmpresaLicitacao : IDAOGeneric {
        public IFacadeDatabase database { get; set; }
        
        public bool Delete(int id) {
            string Delete = "DELETE LICITACOES_EMPRESAS WHERE LEM_EMP_ID = @Id";

            database = new FacadeSQLServer();

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Delete, conn)) {
                        query.Parameters.AddWithValue("@Id", id);
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool Insert(EntidadeDominio entidade) {
            string Insert = "INSERT INTO LICITACOES_EMPRESAS (LEM_LCT_ID, LEM_EMP_ID, LEM_CONFIRMACAO,LEM_USU_ID) VALUES (@LicitacaoId,  @EmpresaId, 0, @UsuarioId);";

            database = new FacadeSQLServer();

            EmpresaLicitacao EmpresaLicitacao = (EmpresaLicitacao)entidade;

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        query.Parameters.AddWithValue("@LicitacaoId", EmpresaLicitacao.Licitacao.Id);
                        query.Parameters.AddWithValue("@EmpresaId", EmpresaLicitacao.Empresa.Id);
                        query.Parameters.AddWithValue("@UsuarioId", EmpresaLicitacao.Licitacao.Usuario.Id);
                        query.ExecuteNonQuery();

                    }
                    database.FecharConexao(conn);
                }
                return true;
            }catch (Exception ex) {
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
            Licitacao Licitacao = (Licitacao) entidade;

            string Update = "UPDATE LICITACOES_EMPRESAS SET LEM_CONFIRMACAO = 0 WHERE LEM_LCT_ID = @LicitacaoId AND LEM_EMP_ID = @EmpresaId";

            if (!Licitacao.Confirmacao) {
                Update = "UPDATE LICITACOES_EMPRESAS SET LEM_CONFIRMACAO = 1 WHERE LEM_LCT_ID = @LicitacaoId AND LEM_EMP_ID = @EmpresaId;";
            }

            database = new FacadeSQLServer();

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Update, conn)) {
                        query.Parameters.AddWithValue("@LicitacaoId", Licitacao.Id);
                        query.Parameters.AddWithValue("@EmpresaId", Licitacao.Empresa.Id);
                        query.ExecuteNonQuery();
                    }
                    database.FecharConexao(conn);
                }
                return true;
            }catch(Exception ex) {
                return false;
            }
        }
    }
}
