﻿using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadePortal : IFacadeGeneric {
        public IDAOGeneric DAO { get; set; }
        public bool Salvar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelecionarTodos() {
            DAO = new DAOPortal;
            return DAO.Select();
        }

        public List<EntidadeDominio> SelecionarTodosPeloId(int Id) {
            throw new NotImplementedException();
        }
    }
}