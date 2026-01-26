USE [Servis_FAD]
GO

/****** Object:  StoredProcedure [dbo].[fask_Servis_Generate_Predloha_Data]    Script Date: 05/24/2016 07:58:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO












-- =============================================
-- Create date: 23.03.2016
-- Description:	Funkce pro vygenerovani dat nove davky
-- =============================================
CREATE PROCEDURE [dbo].[fask_Servis_Generate_Predloha_Data] 
	-- Add the parameters for the stored procedure here
	@CountEntries int	-- cislo davky
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- 20.5.2016 PeV: puvodni funkcionalita (okruh na davku) zakomentovana, pouziva se vsechny okruhy daneho odberatele na davku ...
	/*
	select zdrojstav.IDZdroj, zdrojstav.IDStav, zdrojstav.IDCinnost, zdrojstav.Modified, zdrojstav.IDTerminal, zdrojstav.IDUser, zdrojstav.GUID, zdrojstav.CinnostValue, zdrojstav.CinnostType, predloha.CountEntries, okruh.ODB_ID, predloha.OkruhID 
                from CZMST_Servis_Predloha as predloha 
                join CZMST_Servis_Okruh as okruh on okruh.ID = predloha.OkruhID 
                join CZMST_Servis_ZdrojSeznam as seznam on seznam.ID = okruh.ZdrojSeznamID 
                join CZMST_Servis_ZdrojStav as zdrojstav on zdrojstav.IDZdroj=seznam.ZdrojID 
                where
                predloha.CountEntries= @CountEntries 
                */
	select zdrojstav.IDZdroj, zdrojstav.IDStav, zdrojstav.IDCinnost, zdrojstav.Modified, zdrojstav.IDTerminal, zdrojstav.IDUser, zdrojstav.GUID, zdrojstav.CinnostValue, zdrojstav.CinnostType, predloha.CountEntries, predloha.ODB_ID, okruh.ID as OkruhID
    from CZMST_Servis_Predloha as predloha 
        join CZMST_Servis_Okruh as okruh 
			on okruh.ODB_ID = predloha.ODB_ID 
			/* 27.2.2017 JiS - pozadavek na stazeni pouze daneho okruhu odberatele dle davky */
			and case 
				when isnull(predloha.OkruhID,'')='' THEN 1
				when predloha.OkruhID=okruh.ID THEN 1
				else 0
				END = 1
        join CZMST_Servis_ZdrojSeznam as seznam on seznam.ID = okruh.ZdrojSeznamID 
        join CZMST_Servis_ZdrojStav as zdrojstav on zdrojstav.IDZdroj=seznam.ZdrojID 
    where
        predloha.CountEntries= @CountEntries 
END













GO


