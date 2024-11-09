using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Exceptions;
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
            string Insert = "INSERT INTO EMPRESAS(EMP_CNPJ, EMP_RAZAO_SOCIAL, EMP_NOME_FANTASIA, EMP_TLF_ID, EMP_EQD_ID, EMP_END_ID) " +
                            "OUTPUT INSERTED.EMP_ID " +
                            "VALUES " +
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


                        empresa.Id = (int)query.ExecuteScalar();
                    }
                    database.FecharConexao(conn);
                }
                return true;
            } catch (SqlException ex) {
                throw new DuplicateCNPJException("CNPJ já cadastrado no sistema");
            }
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM Vw_DadosEmpresa";

            database = new FacadeSQLServer();

            List<EntidadeDominio> ListEmpresas = new();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {

                            Empresa Empresa = new Empresa {
                                Id = reader.GetInt32(reader.GetOrdinal("EMP_Id")),
                                CNPJ = reader.GetString(reader.GetOrdinal("EMP_CNPJ")),
                                RazaoSocial = reader.GetString(reader.GetOrdinal("EMP_RAZAO_SOCIAL")),
                                NomeFantasia = reader.GetString(reader.GetOrdinal("EMP_NOME_FANTASIA")),
                                EmailsContato = reader.GetString(reader.GetOrdinal("EML_EMAIL")),
                                TelefoneContato = reader.GetString(reader.GetOrdinal("TLF_NUMERO")),
                                Enquadramento = new Enquadramento {
                                    Id = reader.GetInt32(reader.GetOrdinal("EQD_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("EQD_TIPO")),
                                    Sigla = reader.GetString(reader.GetOrdinal("EQD_SIGLA"))
                                },
                                Endereco = new Endereco {
                                    Id = reader.GetInt32(reader.GetOrdinal("END_ID")),
                                    Logradouro = reader.GetString(reader.GetOrdinal("END_LOGRADOURO")),
                                    Numero = reader.GetString(reader.GetOrdinal("END_NUMERO")),
                                    CEP = reader.GetString(reader.GetOrdinal("END_CEP")),
                                    Complemento = reader.GetString(reader.GetOrdinal("END_COMPLEMENTO")),
                                    Bairro = reader.GetString(reader.GetOrdinal("END_BAIRRO")),
                                    Cidade = new Cidade {
                                        Id = reader.GetInt32(reader.GetOrdinal("CID_ID")),
                                        Nome = reader.GetString(reader.GetOrdinal("CID_NOME")),
                                        Estado = new Estado {
                                            Id = reader.GetInt32(reader.GetOrdinal("EST_ID")),
                                            Nome = reader.GetString(reader.GetOrdinal("EST_NOME")),
                                            UF = reader.GetString(reader.GetOrdinal("EST_UF"))
                                        }
                                    }
                                }
                            };

                            ListEmpresas.Add(Empresa);
                        }
                    }
                }
                database.FecharConexao(conn);
            }

            return ListEmpresas;
        }

        public List<EntidadeDominio> SelectAllWhereId(int id) {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
