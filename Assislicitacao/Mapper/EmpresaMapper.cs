using Assislicitacao.DTO.APIResponse;
using Assislicitacao.Models;

namespace Assislicitacao.Mapper {
    public class EmpresaMapper {
        public static Empresa ConverteEmpresaResponseToEmpresa(EmpresaReceitaWsResponse EmpresaReceitaWsResponse) => new Empresa {
            CNPJ = EmpresaReceitaWsResponse.cnpj,
            RazaoSocial = EmpresaReceitaWsResponse.nome,
            Endereco = new Endereco {
                Logradouro = EmpresaReceitaWsResponse.logradouro,
                Numero = EmpresaReceitaWsResponse.numero,
                Bairro = EmpresaReceitaWsResponse.bairro,
                CEP = EmpresaReceitaWsResponse.cep,
                Municipio = new Municipio {
                    Nome = EmpresaReceitaWsResponse.municipio,
                    Estado = new Estado {
                        Uf = EmpresaReceitaWsResponse.uf
                    }
                }
            },
            PorteEmpresa = new PorteEmpresa {
                Porte = EmpresaReceitaWsResponse.porte
            }
        };
    }
}
