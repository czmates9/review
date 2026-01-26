/* tabulka uzivatelu */
CREATE TABLE FASK_CONS_Logins
(
	id varchar(10) primary key NOT NULL, -- ID uzivatele 
	firstname varchar(20) NOT NULL, -- køestní jméno
	surname varchar(50) NOT NULL, -- pøíjmení
	psswd varchar(10) NOT NULL -- heslo
)

/* pristupova prava do konzole ... nyni se vyuziva pouze ADM pro pristup do konfigurace aplikace */
CREATE TABLE FASK_CONS_LoginsAuth
(
	id varchar(10) primary key NOT NULL, -- ID osoby
	ADM smallint DEFAULT 0 NOT NULL,	-- administrator
	opr_select smallint,	-- uživatel má oprávnìní zobrazovat záznamy
	opr_insert smallint,	-- uživatel má oprávnìní vytváøet nové záznamy
	opr_edit smallint,	-- uživatel má oprávnìní upravovat záznamy
	opr_delete smallint	-- uživatel má oprávnìní zobrazovat data
)