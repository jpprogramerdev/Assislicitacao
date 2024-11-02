using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOEmpresa : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            string Insert = "INSERT INTO EMPRESAS(EMP_CNPJ, EMP_RAZAO_SOCIAL, EMP_NOME_FANTASIA, EMP_TLF_ID, EMP_EQD_ID, EMP_END_ID) VALUES " +
                            "(@CNPJ, @RazaoSocial, @NomeFantasia, " +
                            "(SELECT TLF_ID FROM TELEFONES WHERE TLF_NUMERO = @Telefone), " +
                            "@EnquadramentoId, " +
                            "(SELECT END_ID FROM ENDERECOS WHERE " +
                            "END_LOGRADOURO = @Logradouro AND " +
                            "END_NUMERO = @NumeroEndereco AND " +
                            "END_CEP = @CEP AND " +
                            "END_COMPLEMENTO = @Complemento AND " +
                            "END_BAIRRO = @Bairro AND " +
                            "END_CID_ID = @CidadeId));";

            database = new FacadeSQLServer();

            Empresa empresa = (Empresa)entidade;

            try {
                using (SqlConnection conn = database.AbrirConexao()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        //Parametros emrpesa
                        query.Parameters.AddWithValue("@CNPJ", empresa.CNPJ);
                        query.Parameters.AddWithValue("@RazaoSocial",empresa.RazaoSocial);
                        query.Parameters.AddWithValue("@NomeFantasia",empresa.NomeFantasia);
                        query.Parameters.AddWithValue("@Telefone", empresa.TelefoneContato);
                        query.Parameters.AddWithValue("@EnquadramentoId", empresa.Enquadramento.Id);
                        query.Parameters.AddWithValue("@EnderecoId",empresa.Endereco.Id);

                        //Parametro endereço
                        query.Parameters.AddWithValue("@Logradouro", empresa.Endereco.Logradouro);
                        query.Parameters.AddWithValue("@NumeroEndereco", empresa.Endereco.Numero);
                        query.Parameters.AddWithValue("@CEP", empresa.Endereco.CEP);
                        query.Parameters.AddWithValue("@Bairro", empresa.Endereco.Bairro);
                        query.Parameters.AddWithValue("@CidadeId", empresa.Endereco.Cidade.Id);

                        if (string.IsNullOrEmpty(empresa.Endereco.Complemento)) {
                            query.Parameters.AddWithValue("@Complemento", DBNull.Value);
                        } else {
                            query.Parameters.AddWithValue("@Complemento", empresa.Endereco.Complemento);
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

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
