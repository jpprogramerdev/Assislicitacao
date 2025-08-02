using Assislicitacao.DAO.Interface;
using Assislicitacao.DTO.APIResponse;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;
using Assislicitacao.Service;
using System.Threading.Tasks;

namespace Assislicitacao.Facade {
    public class FacadeEmpresa : IFacadeEmpresa {
        private readonly ReceitaWsService _receitaService;
        private readonly IDAOEstado _daoEstado;
        private readonly IDAOMunicipio _daoMunicipio;
        private readonly IDAOPorteEmpresa _daoPorteEmpresa;
        private readonly IDAOEndereco _daoEndereco;
        private readonly IDAOEmpresa _daoEmpresa;

        public FacadeEmpresa(ReceitaWsService receitaService, IDAOEstado daoEstado, IDAOMunicipio daoMunicipio, IDAOPorteEmpresa daoPorteEmpresa, IDAOEndereco daoEndereco, IDAOEmpresa daoEmpresa) {
            _daoEstado = daoEstado;
            _daoMunicipio = daoMunicipio;
            _daoPorteEmpresa = daoPorteEmpresa;
            _daoEndereco = daoEndereco;
            _daoEmpresa = daoEmpresa;
            _receitaService = receitaService;
        }

        public async Task Apagar(EntidadeDominio entidade) {
            await _daoEmpresa.Delete(entidade);
        }

        public async Task Atualizar(EntidadeDominio entidade) {
            await _daoEmpresa.Update(entidade);
        }

        public async Task Inserir(EntidadeDominio entidade) {
            Empresa Empresa = (Empresa)entidade;

            var estado = (await _daoEstado.SelectAll()).Cast<Estado>().FirstOrDefault(est => est.Uf == Empresa.Endereco.Municipio.Estado.Uf);
            Empresa.Endereco.Municipio.Estado = estado;

            var municipio = (await _daoMunicipio.SelectAll()).Cast<Municipio>().FirstOrDefault(mun => mun.Nome == Empresa.Endereco.Municipio.Nome);
            if (municipio == null) {
                await _daoMunicipio.Insert(Empresa.Endereco.Municipio);
            }
            Empresa.Endereco.Municipio = (await _daoMunicipio.SelectAll()).Cast<Municipio>().FirstOrDefault(mun => mun.Nome == Empresa.Endereco.Municipio.Nome);

            var endereco= (await _daoEndereco.SelectAll()).Cast<Endereco>().FirstOrDefault(end => end.CEP == Empresa.Endereco.CEP && end.Logradouro == Empresa.Endereco.Logradouro && end.Numero == Empresa.Endereco.Numero);
            if (endereco == null) {
                await _daoEndereco.Insert(Empresa.Endereco);
            }
            Empresa.Endereco = (await _daoEndereco.SelectAll()).Cast<Endereco>().FirstOrDefault(end => end.CEP == Empresa.Endereco.CEP && end.Logradouro == Empresa.Endereco.Logradouro && end.Numero == Empresa.Endereco.Numero);

            var porte = (await _daoPorteEmpresa.SelectAll()).Cast<PorteEmpresa>().FirstOrDefault(prt => prt.Porte == Empresa.PorteEmpresa.Porte);
            if (porte == null) {
                await _daoPorteEmpresa.Insert(Empresa.PorteEmpresa);
            }
            Empresa.PorteEmpresa = (await _daoPorteEmpresa.SelectAll()).Cast<PorteEmpresa>().FirstOrDefault(prt => prt.Porte == Empresa.PorteEmpresa.Porte);

            await _daoEmpresa.Insert(Empresa);
        }

        public async Task<EmpresaReceitaWsResponse> ObterEmpresaCNPJ(string cnpj) {
            return await _receitaService.ConsultarCnpj(cnpj);
        }

        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _daoEmpresa.SelectAll();
    }
}
