using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeLicitacao : IFacadeLicitacao {
        private readonly IDAOMunicipio _daoMunicipio;
        private readonly IDAOLicitacao _daoLicitacao;
        private readonly IDAOEstado _daoEstado;

        public FacadeLicitacao(IDAOMunicipio daoMunicipio, IDAOLicitacao daoLicitacao, IDAOEstado daoEstado) {
            _daoMunicipio = daoMunicipio;
            _daoLicitacao = daoLicitacao;
            _daoEstado = daoEstado;
        }

        public Task Apagar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public async Task Atualizar(EntidadeDominio entidade) {
            Licitacao Licitacao = (Licitacao)entidade;

            var Municipio = (await _daoMunicipio.SelectAll()).Cast<Municipio>().FirstOrDefault(mun => mun.Nome == Licitacao.Municipio.Nome);
            if (Municipio == null) {
                await _daoMunicipio.Insert(Licitacao.Municipio);
            }

            Licitacao.Municipio = (await _daoMunicipio.SelectAll()).Cast<Municipio>().FirstOrDefault(mun => mun.Nome == Licitacao.Municipio.Nome);

            await _daoLicitacao.Update(Licitacao);
        }

        public async Task AtualizarConfirmacao(EntidadeDominio entidade) => await _daoLicitacao.UpdateConfirmacao(entidade);

        public async Task Inserir(EntidadeDominio entidade) {
            Licitacao Licitacao = (Licitacao)entidade;

            var Municipio = (await _daoMunicipio.SelectAll()).Cast<Municipio>().FirstOrDefault(mun => mun.Nome == Licitacao.Municipio.Nome);
            if (Municipio == null) {
               await _daoMunicipio.Insert(Licitacao.Municipio);
            }

            var Estado = (await _daoEstado.SelectAll()).Cast<Estado>().FirstOrDefault(est => est.Id == Licitacao.Municipio.EstadoId);


            Licitacao.Municipio.Estado = Estado;
            Licitacao.Municipio = (await _daoMunicipio.SelectAll()).Cast<Municipio>().FirstOrDefault(mun => mun.Nome == Licitacao.Municipio.Nome);

            await _daoLicitacao.Insert(Licitacao);
        }

        public async Task<IEnumerable<EntidadeDominio>> Selecionar() => await _daoLicitacao.SelectAll();
    }
}
