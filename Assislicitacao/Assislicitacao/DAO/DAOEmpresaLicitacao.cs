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
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            string Insert = "INSERT INTO LICITACOES_EMPRESAS (LEM_LCT_ID, LEM_EMP_ID, LEM_CONFIRMACAO) VALUES (@LicitacaoId,  @EmpresaId, 0);";

            database = new FacadeSQLServer();

            EmpresaLicitacao EmpresaLicitacao = (EmpresaLicitacao)entidade;

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        query.Parameters.AddWithValue("@LicitacaoId", EmpresaLicitacao.Licitacao.Id);
                        query.Parameters.AddWithValue("@EmpresaId", EmpresaLicitacao.Empresa.Id);
                        query.ExecuteNonQuery();
                    }
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
            throw new NotImplementedException();
        }
    }
}
