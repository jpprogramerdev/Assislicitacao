INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES ('ADMINISTRADOR DE SISTEMA');
INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES ('OPERADOR DE LICITAÇÕES');
INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES ('REPRESENTANTE LEGAL');


--Tipo Licitação
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Pregão Eletrônico', 'PE');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Pregão Presencial', 'PP');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Dispensa de Licitação', 'DL');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Chamamento Público', 'CP');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Concorrencia', 'CC');
INSERT INTO TIPOS_LICITACAO(TPL_TIPO, TPL_SIGLA) VALUES ('Credenciamento', 'CD');

INSERT INTO ESTADOS (EST_UF)
VALUES 
('AC'), -- Acre
('AL'), -- Alagoas
('AP'), -- Amapá
('AM'), -- Amazonas
('BA'), -- Bahia
('CE'), -- Ceará
('DF'), -- Distrito Federal
('ES'), -- Espírito Santo
('GO'), -- Goiás
('MA'), -- Maranhão
('MT'), -- Mato Grosso
('MS'), -- Mato Grosso do Sul
('MG'), -- Minas Gerais
('PA'), -- Pará
('PB'), -- Paraíba
('PR'), -- Paraná
('PE'), -- Pernambuco
('PI'), -- Piauí
('RJ'), -- Rio de Janeiro
('RN'), -- Rio Grande do Norte
('RS'), -- Rio Grande do Sul
('RO'), -- Rondônia
('RR'), -- Roraima
('SC'), -- Santa Catarina
('SP'), -- São Paulo
('SE'), -- Sergipe
('TO'); -- Tocantins