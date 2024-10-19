USE Assisliticacao;

INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Pregão Eletrônico', 'PE');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Pregão Presencial', 'PR');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Dispensa de licitação', 'DL');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Chamamento publico', 'CP');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES('Credenciamento', 'CC');

INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor preço por item');
INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor preço por lote');
INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor preço por grupo');
INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor preço global');
INSERT INTO TIPOS_DISPUTA(TDP_TIPO) VALUES('Menor preço total');

INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('Compras GOV', 'https://www.gov.br/compras/pt-br');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('BLL', 'https://bllcompras.com/Home/Login');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('Portal de Compras Publicas', 'https://operacao.portaldecompraspublicas.com.br/18/loginext/');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('BNC', 'https://bnccompras.com/Home/Login');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('AMMLICITA', 'https://app.ammlicita.org.br/login/');
INSERT INTO PORTAIS(PRT_NOME, PRT_LINK) VALUES('NOVOBBMNET', 'https://novobbmnet.com.br/');

