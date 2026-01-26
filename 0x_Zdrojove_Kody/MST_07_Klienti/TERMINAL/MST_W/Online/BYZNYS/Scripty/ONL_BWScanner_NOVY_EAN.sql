/****** Object:  StoredProcedure [ONL_NOVY_EAN]    Script Date: 11/24/2011 13:32:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


DROP PROCEDURE [ONL_BWSCANNER_NOVY_EAN]
GO

/*
24.11.2011 - Upraveno (Pøevzato z RF-Scanner)
Autor: Jiøí Skøivánek (FASK, spol. s r.o.)
Schváleno: (GOSVO)
*/
CREATE PROCEDURE [ONL_BWSCANNER_NOVY_EAN]
/* 
 	Zápis nového EAN-kódu do SKDALCIS
 	Pøedpokládá nastavení EANù v dalších èíslech.
*/
(
	@klic_ma int  = 0,
	@eankod varchar(20)='' ,
	@dmj char(3) = '',
	@prepocet1 int = 1,
	@prepocet2 int = 1,
	@klic_odb int = NULL
)	
AS
BEGIN
	declare @cislomj int
	select @cislomj = null
	if not exists(select top 1 1 from SKLAD with (NOLOCK) 
			where KLIC_MA=@klic_ma)	begin
		--select STATUS='ERROR1' -- @klic_ma neexistuje 
		select STATUS='Materiál nenalezen' -- @klic_ma neexistuje 
		return 
	end 	
	if exists(select top 1 1 from SKDALCIS with (NOLOCK) 
			where KOD='EAN' and POMCISLO=@eankod) begin
		--select STATUS='ERROR2' -- @eankod jiz existuje 
		select STATUS='EAN kód již existuje' -- @eankod jiz existuje 
		return 
	end 	
	if isnull(@dmj,'')<>'' begin
		select @cislomj=J.CISLO from JEDNOTKY J with (NOLOCK)
			where ZKRATKAMJ=@dmj
		if (isnull(@cislomj,0)=0) begin
			--select STATUS='ERROR3' -- @dmj neexistuje 
			select STATUS='DMJ nenalezena' -- @dmj neexistuje 
			return 
		end
	end else begin	
		select @cislomj=S.HL_MJ, @dmj=S.MJ, @prepocet1=1, @prepocet2=1
			from SKLAD S with (NOLOCK)
	end 

	/*
	24.11.2011 - Jiøí Skøivánek(FASK)
	Rozšíøení o pøidání EAN a vazby partnera pøes sloupec FSIDENT, 
	který se využívá v BWScanner-FASK k vazbì EAN<->Partner
	*/	
	if ISNULL(@klic_odb, 0)<>0 begin
		declare @fsident char(4)
		set @fsident = null
		select @fsident=FSIDENT from FACISODB where KLIC_ODB2=@klic_odb

		insert into SKDALCIS (KLIC_MA, POMCISLO, KOD, D_MJ,MJ,PREPOCET1,PREPOCET2, FSIDENT)
			select @klic_ma, @eankod, 'EAN', @cislomj, @dmj, @prepocet1, @prepocet2, @fsident
	end else begin
		insert into SKDALCIS (KLIC_MA, POMCISLO, KOD, D_MJ,MJ,PREPOCET1,PREPOCET2)
			select @klic_ma, @eankod, 'EAN', @cislomj, @dmj, @prepocet1, @prepocet2
	end
		
	select STATUS='OK'
END
GO


