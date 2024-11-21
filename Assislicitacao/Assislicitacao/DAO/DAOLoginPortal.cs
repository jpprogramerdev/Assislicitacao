using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;

namespace Assislicitacao.DAO {
    public class DAOLoginPortal : IDAOGeneric {
        public IFacadeDatabase database { get; set; }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public bool Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM Vw_Logins;";

            database = new FacadeSQLServer();

            List<EntidadeDominio> List = new();
            Dictionary<int, Empresa> EmpresasMap = new();

            using (SqlConnection conn = database.AbrirConexao()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Empresa Empresa = new();
                            Portal Portal = new();
                            LoginPortal LoginPortal = new();

                            int IdEmpresa = reader.GetInt32(reader.GetOrdinal("EMP_Id"));

                            if (!EmpresasMap.ContainsKey(IdEmpresa)) {
                                Empresa.Id = IdEmpresa;
                                Empresa.CNPJ = reader.IsDBNull(reader.GetOrdinal("EMP_CNPJ")) ? null : reader.GetString(reader.GetOrdinal("EMP_CNPJ"));
                                Empresa.RazaoSocial = reader.IsDBNull(reader.GetOrdinal("EMP_RAZAO_SOCIAL"))? null : reader.GetString(reader.GetOrdinal("EMP_RAZAO_SOCIAL"));
                                Empresa.NomeFantasia = reader.IsDBNull(reader.GetOrdinal("EMP_NOME_FANTASIA")) ? null : reader.GetString(reader.GetOrdinal("EMP_NOME_FANTASIA"));

                                EmpresasMap[IdEmpresa] = Empresa;
                            }

                            Empresa = EmpresasMap[IdEmpresa];

                            if (!reader.IsDBNull(reader.GetOrdinal("LNP_Id"))) {
                                int IdLoginPortal = reader.GetInt32(reader.GetOrdinal("LNP_Id"));
                                if (!Empresa.Logins.Any(l => l.Id == IdLoginPortal)) {
                                    LoginPortal.Id = IdLoginPortal;
                                    LoginPortal.Login = reader.IsDBNull(reader.GetOrdinal("LNP_LOGIN")) ? null : reader.GetString(reader.GetOrdinal("LNP_LOGIN"));
                                    LoginPortal.Senha = reader.IsDBNull(reader.GetOrdinal("LNP_SENHA")) ? null : reader.GetString(reader.GetOrdinal("LNP_SENHA"));

                                    if (!reader.IsDBNull(reader.GetOrdinal("PRT_ID"))) {
                                        LoginPortal.Portal = new Portal {
                                            Id = reader.GetInt32(reader.GetOrdinal("PRT_ID")),
                                            Nome = reader.IsDBNull(reader.GetOrdinal("PRT_NOME")) ? null : reader.GetString(reader.GetOrdinal("PRT_NOME")),
                                            Link = reader.IsDBNull(reader.GetOrdinal("PRT_LINK")) ? null: reader.GetString(reader.GetOrdinal("PRT_LINK"))
                                        };
                                    }

                                    Empresa.Logins.Add(LoginPortal);
                                }
                            }
                        }
                    }
                }
                database.FecharConexao(conn);
            }

            List.AddRange(EmpresasMap.Values);

            return List;
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
