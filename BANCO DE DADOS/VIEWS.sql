USE Assisliticacao;

CREATE VIEW vw_LicitacoesDetalhadas
AS
	SELECT * FROM LICITACOES
	JOIN CIDADES ON LCT_CID_ID = CID_ID
	JOIN ESTADOS ON CID_EST_ID = EST_ID
	JOIN TIPOS_LICITACAO ON LCT_TPL_ID = TPL_ID
	JOIN PORTAIS ON LCT_PRT_ID = PRT_ID
	JOIN TIPOS_DISPUTA ON LCT_TDP_ID = TDP_ID
	JOIN LICITACOES_EMPRESAS ON LEM_LCT_ID = LCT_ID
	JOIN EMPRESAS ON EMP_ID = LEM_EMP_ID
	JOIN USUARIOS ON USU_ID = LEM_USU_ID


CREATE View Vw_DadosEmpresa AS
	SELECT * FROM EMPRESAS
	JOIN TELEFONES ON TLF_ID = EMP_TLF_ID
	JOIN ENQUADRAMENTOS ON EQD_ID = EMP_EQD_ID
	JOIN ENDERECOS ON END_ID = EMP_END_ID
	JOIN CIDADES ON CID_ID = END_CID_ID
	JOIN ESTADOS ON EST_ID = CID_EST_ID
	JOIN EMAILS_EMPRESAS ON EEP_EMP_ID = EMP_ID
	JOIN EMAILS ON EML_ID = EEP_EML_ID


CREATE VIEW Vw_DadosUsuario AS
	SELECT * FROM USUARIOS
	JOIN EMAILS ON EML_ID = USU_EML_ID
	JOIN TIPOS_USUARIO ON TPU_ID = USU_TPU_ID


CREATE VIEW Vw_Logins AS
	SELECT * FROM EMPRESAS E
	LEFT JOIN LOGINS_PORTAIS L ON E.EMP_ID = L.LNP_EMP_ID
	LEFT JOIN PORTAIS P ON L.LNP_PRT_ID = P.PRT_ID
	JOIN EMAILS_EMPRESAS EE ON EE.EEP_EMP_ID = E.EMP_ID
	JOIN EMAILS M ON M.EML_ID = EE.EEP_EML_ID;