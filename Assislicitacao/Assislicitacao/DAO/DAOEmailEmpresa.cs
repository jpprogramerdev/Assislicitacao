using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOEmailEmpresa : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(int id) {
            IFacadeGeneric facadeEmpresa = new FacadeEmpresa();
            Empresa Empresa = (Empresa)facadeEmpresa.SelecionaUnicoPeloId(id);

            string Delete = "DELETE EMAILS_EMPRESAS WHERE EEP_EMP_ID = @Id AND EEP_EML_ID = (SELECT EML_ID FROM EMAILS WHERE EML_EMAIL = @Email);";

            database = new FacadeSQLServer();

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Delete, conn)) {
                        query.Parameters.AddWithValue("@Id", id);
                        query.Parameters.AddWithValue("@Email", Empresa.EmailsContato);
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool Insert(EntidadeDominio entidade) {
            string Insert = "INSERT INTO EMAILS_EMPRESAS(EEP_EML_ID,EEP_EMP_ID) VALUES ((SELECT EML_ID FROM EMAILS WHERE EML_EMAIL = @Email), @EmpresaId);";

            database = new FacadeSQLServer();

            Empresa Empresa = (Empresa)entidade;

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        query.Parameters.AddWithValue("@Email", Empresa.EmailsContato);
                        query.Parameters.AddWithValue("@EmpresaId", Empresa.Id);
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
