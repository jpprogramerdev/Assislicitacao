﻿using Assislicitacao.DAO;
using Assislicitacao.DAO.Intefaces;
using Assislicitacao.Facade.Interfaces;
using Assislicitacao.Models;

namespace Assislicitacao.Facade {
    public class FacadeEmailEmpresa : IFacadeGeneric {
        public IDAOGeneric DAO { get; set; }

        public bool Apagar(int Id) {
            DAO = new DAOEmailEmpresa();
            return DAO.Delete(Id);
        }

        public bool Atualizar(EntidadeDominio entidadeDominio) {
            DAO = new DAOEmailEmpresa();
            if (DAO.Delete(entidadeDominio.Id) && DAO.Insert(entidadeDominio)) {
                return true;
            }
            return false;
        }

        public bool Salvar(EntidadeDominio entidade) {
            DAO = new DAOEmailEmpresa();
            return DAO.Insert(entidade);
        }

        public List<EntidadeDominio> SelecionarTodos() {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> SelecionarTodosPeloId(int Id) {
            throw new NotImplementedException();
        }

        public EntidadeDominio SelecionaUnicoPeloId(int Id) {
            throw new NotImplementedException();
        }
    }
}
