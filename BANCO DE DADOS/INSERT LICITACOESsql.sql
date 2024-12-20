USE Assisliticacao;

INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Preg�o Eletr�nico', 'PE');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Preg�o Presencial', 'PR');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Dispensa de licita��o', 'DL');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Chamamento publico', 'CP');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Credenciamento', 'CC');

INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor pre�o por item');
INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor pre�o por lote');
INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor pre�o por grupo');
INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor pre�o global');
INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor pre�o total');

INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('Compras GOV', 'https://www.gov.br/compras/pt-br');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('BLL', 'https://bllcompras.com/Home/Login');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('Portal de Compras Publicas', 'https://operacao.portaldecompraspublicas.com.br/18/loginext/');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('BNC', 'https://bnccompras.com/Home/Login');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('AMMLICITA', 'https://app.ammlicita.org.br/login/');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('NOVOBBMNET', 'https://novobbmnet.com.br/');

