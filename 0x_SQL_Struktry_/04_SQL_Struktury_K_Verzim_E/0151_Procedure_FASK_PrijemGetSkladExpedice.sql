/****** Object:  StoredProcedure [dbo].[FASK_PrijemGetSkladExpedice]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- autor: Jiri Skrivanek
-- firma: FASK, spol. s r.o.
-- datum: 30.5.2018
-- =============================================
CREATE PROCEDURE [dbo].[FASK_PrijemGetSkladExpedice]
    @Itemnmbr			NVarChar(31),			-- ID polozky prijimane
    @MnozstviZadane		numeric(19,5),			-- pocet prijimanych terminalem
	@MnozstviNasnimane		numeric(19,5),			-- pocet prijatych terminalem
	-- out hodnoty
	@MnozstviDodavatelePozadovano	numeric(19,5) out,	-- požadováno od dodavatele(ů) 
													-- => Množství Dodat(objednáno) pro nevyřízené objednávky vydané
	@MnozstviDodavateleDodano		numeric(19,5) out,	-- dodáno od dodavatele(ů) 
													-- => Množství Dodáno (převedeno) pro nevyřízené objednávky vydané
	@MnozstviDodavateleDodat		numeric(19,5) out,	-- zbývá dodat od dodavatele(ů) 
													-- => Množství Dodat(objednáno) – Dodáno(převedeno) pro nevyřízené objednávky vydané
	@MnozstviOdberateliPozadovano	numeric(19,5) out,	-- požadováno odběrateli 
													-- => Množsvtí Dodat z nevyřízených objednávek přijatých
													-- => ??? (jen nekryté množství) 
													-- => ? zohlednit i příjemky, tedy kolik je materiálu na skladě?
	@MnozstviOdberatelumDodano		numeric(19,5) out,	-- dodáno odběratelům 
													-- => ??? (resp. Na sklad přijato z terminálu?)
													-- => Množsvtí Dodáno z nevyřízených objednávek přijatých
													-- => zohlednit i příjemky, tedy kolik je materiálu na skladě?
	@MnozstviOdberatelumDodat		numeric(19,5) out,	-- zbývá dodat odběratelům
													-- => Množsvtí Dodat – Dodáno z nevyřízených objednávek přijatých
													-- => ? zohlednit i příjemky, tedy kolik je materiálu na skladě?
	@Vysledek 			Numeric(19,5)	out		-- Vysledek vypoctu = (<Zbyva dodat odberatelum> - <prijato terminalem>)
AS
BEGIN
	SET NOCOUNT ON;

	Set	@MnozstviDodavatelePozadovano=0
	Set	@MnozstviDodavateleDodano=0
	Set	@MnozstviDodavateleDodat=0
	Set	@MnozstviOdberateliPozadovano=0
	Set	@MnozstviOdberatelumDodano=0
	Set	@MnozstviOdberatelumDodat=0
	Set	@Vysledek=0

	Declare 
		@Rezervovano numeric(19,5),
		@Reklamovano numeric(19,5),
		@Stav		 numeric(19,5)

	--Objednávky (SKz.ObjedP) + Rezervace (SKz.Rezer) + Reklamace (SKz.Reklam)  - Stav zásoby skladem (SKz.stavZ)  = počet kolik dát na expedici
	select 
		@MnozstviDodavateleDodat =	SKz.ObjedV,
		@MnozstviOdberatelumDodat = SKz.ObjedP,
		@Rezervovano =				SKz.Rezer,
		@Reklamovano =				SKz.Reklam,
		@Stav =						SKz.StavZ
	from StwPh_04535667_2020.dbo.Skz 
	where ID = @Itemnmbr

	Set @Vysledek = @MnozstviOdberatelumDodat + @Rezervovano + @Reklamovano - @Stav - @MnozstviNasnimane

	RETURN 0	-- ok... procedura prosla korektne ... 

END

GO
/**************************************************************************************/