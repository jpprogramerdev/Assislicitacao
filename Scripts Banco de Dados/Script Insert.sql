INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES ('ADMINISTRADOR DE SISTEMA');
INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES ('OPERADOR DE LICITA��ES');
INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES ('REPRESENTANTE LEGAL');


--Tipo Licita��o
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Preg�o Eletr�nico', 'PE');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Preg�o Presencial', 'PP');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Dispensa de Licita��o', 'DL');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Chamamento P�blico', 'CP');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Concorrencia', 'CC');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Credenciamento', 'CD');

INSERT INTO ESTADOS (EST_UF)
VALUES 
('AC'), -- Acre
('AL'), -- Alagoas
('AP'), -- Amap�
('AM'), -- Amazonas
('BA'), -- Bahia
('CE'), -- Cear�
('DF'), -- Distrito Federal
('ES'), -- Esp�rito Santo
('GO'), -- Goi�s
('MA'), -- Maranh�o
('MT'), -- Mato Grosso
('MS'), -- Mato Grosso do Sul
('MG'), -- Minas Gerais
('PA'), -- Par�
('PB'), -- Para�ba
('PR'), -- Paran�
('PE'), -- Pernambuco
('PI'), -- Piau�
('RJ'), -- Rio de Janeiro
('RN'), -- Rio Grande do Norte
('RS'), -- Rio Grande do Sul
('RO'), -- Rond�nia
('RR'), -- Roraima
('SC'), -- Santa Catarina
('SP'), -- S�o Paulo
('SE'), -- Sergipe
('TO'); -- Tocantins