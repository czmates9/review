	SELECT 
	TP.ID as ID_Zdroj, duplicity.pocet as PocetVyskytu, TP.ITEMNMBR_Def, TP.DESC_Def, duplicity.SKL_ID_Def, duplicity.SKL_DESC_Def, TP.ITEMNMBR_fol, Z.ITEMNMBR, TP.DESC_Fol, Z.ITEMDESC, TP.ITEMCODE_Fol, Z.ITEMCODE, TP.VNDITNUM_Fol, Z.VNDITNUM, TP.MJ_Fol, Z.MJ, TP.SKL_ID_Fol, Z.SKL_ID, skladZdroj.skl_desc as SKL_DESC_Fol, skladcil.skl_desc as SKL_DESC  
		FROM 
		( 
			SELECT 
			T.ID, T.ITEMNMBR_Def, T.DESC_Def, T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol, zas.ITEMCODE as ITEMCODE_Fol, zas.VNDITNUM as VNDITNUM_Fol, zas.SKL_ID as SKL_ID_Fol , zas_Vyr.SKL_ID as SKL_ID_Def 
			FROM FASK_Vyroba_TP as T 
			LEFT JOIN FASK_ZASOBY as zas ON T.ITEMNMBR_fol = zas.ITEMNMBR  
			LEFT JOIN FASK_ZASOBY as zas_Vyr ON T.ITEMNMBR_Def = zas_Vyr.ITEMNMBR  
			WHERE ITEMNMBR_fol is not null  
			--AND ITEMNMBR_Def = '281'
		) as TP 
		LEFT JOIN 
			(
				SELECT ITEMNMBR, ITEMDESC, MJ, ITEMCODE, VNDITNUM, SKL_ID 
				FROM FASK_ZASOBY 
				WHERE SKL_ID = '1'
			) as Z  ON TP.DESC_Fol = Z.ITEMDESC 
		LEFT JOIN 
			( 
				SELECT DESC_Fol, DESC_Def, COUNT(DESC_Fol) pocet , SKL_DESC_Def, SKL_ID_Def 
				FROM 
					( 
						SELECT 
						TP.ID, TP.ITEMNMBR_Def, TP.DESC_Def,  TP.SKL_ID_Def, TP.SKL_DESC_Def,  TP.ITEMNMBR_fol, Z.ITEMNMBR, TP.DESC_Fol, Z.ITEMDESC, TP.ITEMCODE_Fol ,Z.ITEMCODE,TP.VNDITNUM_Fol ,Z.VNDITNUM, TP.MJ_Fol, Z.MJ , TP.SKL_ID_Fol, Z.SKL_ID  
						FROM 
							(
								SELECT T.ID, T.ITEMNMBR_Def,T.DESC_Def,  zas_Vyr.SKL_ID as SKL_ID_Def, S_TP.skl_desc as SKL_DESC_Def ,T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol, zas.ITEMCODE as ITEMCODE_Fol, zas.VNDITNUM as VNDITNUM_Fol, zas.SKL_ID as SKL_ID_Fol  
								FROM FASK_Vyroba_TP as T 
								LEFT JOIN FASK_ZASOBY as zas ON T.ITEMNMBR_fol = zas.ITEMNMBR  
								LEFT JOIN FASK_ZASOBY as zas_Vyr ON T.ITEMNMBR_Def = zas_Vyr.ITEMNMBR  
								LEFT JOIN CZMST093 S_TP on S_TP.skl_id = zas_Vyr.SKL_ID  
								WHERE ITEMNMBR_fol is not null 
							) as TP  
						LEFT JOIN 
							(
								SELECT ITEMNMBR, ITEMDESC, MJ, ITEMCODE, VNDITNUM, SKL_ID 
								FROM FASK_ZASOBY 
								WHERE SKL_ID = '1'
							) as Z  ON TP.DESC_Fol = Z.ITEMDESC 
					) as FULLSELECT 
					Group by DESC_Fol, DESC_Def, SKL_DESC_Def, SKL_ID_Def 
			) duplicity on duplicity.DESC_Def=tp.DESC_Def and duplicity.DESC_Fol=tp.DESC_Fol and duplicity.SKL_ID_Def = tp.SKL_ID_Def
		LEFT JOIN CZMST093 skladZdroj on skladZdroj.skl_id = TP.SKL_ID_Fol 
		LEFT JOIN CZMST093 skladcil on skladcil.skl_id = Z.SKL_ID

		--WHERE Z.SKL_ID is not null
		WHERE Z.SKL_ID is  null