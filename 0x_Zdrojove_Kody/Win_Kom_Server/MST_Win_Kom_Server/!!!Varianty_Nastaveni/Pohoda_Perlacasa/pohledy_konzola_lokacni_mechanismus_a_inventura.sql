/* View do stavu lokacniho mechanismu (inventura) */
CREATE VIEW [dbo].[fask_view_stav_naplneni_inventury]
AS
SELECT     'Inventura' as [Type], zbozi.ITEMDESC as ITEMDESC, '' as [DOCUMENT_NUMBER], stav.ITEMNMBR, stav.QTYSHPPD_DEF as QTYSHPPD, stav.SKL_ID, stav.LOCNCODE, stav.SERLTNUM, null as CountEntries, zbozi.VNDITNUM as CZ_CarKod, i4.USERID as USER_ID, i4.ID_TERMINAL as ID_TERMINAL, stav.QTYSHPPD_DEF_DATE as dateeve
FROM         dbo.CZMST_SkladLokace_Stav stav
left join CZMST095 zbozi on zbozi.ITEMNMBR = stav.itemnmbr
left join CZMST_I4 i4 on stav.ITEMNMBR = i4.ITEMNMBR and stav.LOCNCODE = i4.LOCNCODE
;

/* View do pohybu lokacniho mechanismu */
CREATE VIEW [dbo].[fask_view_lokacni_mechanismus_pohyby]
AS
SELECT     pohyb.POHYB_TYPE as [Type], zbozi.ITEMDESC as ITEMDESC, pohyb.DOCUMENT_NUMBER as [DOCUMENT_NUMBER], pohyb.ITEMNMBR, pohyb.QTYSHPPD as QTYSHPPD, pohyb.SKL_ID_SRC as SKL_ID, pohyb.LOCNCODE_SRC as LOCNCODE, pohyb.SERLTNUM, pohyb.CountEntries, zbozi.VNDITNUM as CZ_CarKod, pohyb.UserID as USER_ID, pohyb.TermID as ID_TERMINAL, pohyb.dateeveT as dateeve
FROM         dbo.CZMST_SkladLokace_StavPohyb pohyb
left join CZMST095 zbozi on zbozi.ITEMNMBR = pohyb.itemnmbr
;


CREATE VIEW [dbo].[fask_view_pohyby_aktualni]
as
	select [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve	
		from fask_view_lokacni_mechanismus_pohyby
	UNION ALL
	select [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve	
		from fask_view_stav_naplneni_inventury
;


/* pohled na ciselnik lokaci */
CREATE VIEW [dbo].[CZMST094] as
select 
[SKL_ID]
,[LOCNCODE]
,[TYPE]
,[Barcode]
,[Description]
,[DEX_ROW_ID]
from [dbo].[CZMST_SkladLokace_Mapa] mapa
GO