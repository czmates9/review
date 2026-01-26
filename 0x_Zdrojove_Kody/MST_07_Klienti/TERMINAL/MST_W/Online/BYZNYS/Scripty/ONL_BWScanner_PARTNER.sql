/****** Object:  UserDefinedFunction [ONL_PARTNER]    Script Date: 11/24/2011 13:32:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP FUNCTION [ONL_BWSCANNER_PARTNER]
GO

/*
24.11.2011 - Upraveno (Pøevzato z RF-Scanner)
Autor: Jiøí Skøivánek (FASK, spol. s r.o.)
Schváleno: (GOSVO)
*/
CREATE FUNCTION [ONL_BWSCANNER_PARTNER]
/* 
	 Seznam partneru
 [SN] 03.06.09 {25483} - rozšíøení PSÈ z 6 na 9 znakù
*/
(
    @typ tinyint,
	@str varchar(20)  = ''
)
RETURNS @partner table 
(	
	klic_odb int,
	nazev varchar(40) COLLATE database_default,
	adresa1 varchar(35) COLLATE database_default,
	adresa2 varchar(35) COLLATE database_default,
	ico char(8) COLLATE database_default,
	dic varchar(14) COLLATE database_default,
	psc char(9) COLLATE database_default,
	local tinyint,
	mistodod varchar(60),
	klic_dos int
)
AS
BEGIN
	if @typ=0
		INSERT @partner  select KLIC_ODB2,isnull(ODBERATEL,''),
		isnull(ADRESA1,''),isnull(ADRESA2,''),isnull(ICO,''),isnull(DIC,''),isnull(PSC,''),
		case when (isnull(ZEM,'CZ')='CZ') then 1 else 0 end,'',KLIC_DOS
		from FACISODB with(NOLOCK) 
		where  isnull(KLIC_DOS,0)= convert(int,@str) 
	else 
	if @typ=1
		INSERT @partner  select KLIC_ODB2,isnull(ODBERATEL,''),
		isnull(ADRESA1,''),isnull(ADRESA2,''),isnull(ICO,''),isnull(DIC,''),isnull(PSC,''),
		case when (isnull(ZEM,'CZ')='CZ') then 1 else 0 end,'',KLIC_DOS
		from FACISODB with(NOLOCK) 
		where  isnull(KLIC_ODB2,0)= convert(int,@str) 
	else 
	if @typ=2
		INSERT @partner  select KLIC_ODB2,isnull(ODBERATEL,''),
		isnull(ADRESA1,''),isnull(ADRESA2,''),isnull(ICO,''),isnull(DIC,''),isnull(PSC,''),
		case when (isnull(ZEM,'CZ')='CZ') then 1 else 0 end,'',KLIC_DOS
		from FACISODB with(NOLOCK) 
		where isnull(ODBERATEL,'') COLLATE SQL_Latin1_General_Cp1251_CI_AS 
					   like @str+'%'
		--where isnull(ODBERATEL,'') like @str+'%' 
	else 
	if @typ=3
		INSERT @partner  select KLIC_ODB2,isnull(ODBERATEL,''),
		isnull(ADRESA1,''),isnull(ADRESA2,''),isnull(ICO,''),isnull(DIC,''),isnull(PSC,''),
		case when (isnull(ZEM,'CZ')='CZ') then 1 else 0 end,'',KLIC_DOS
		from FACISODB with(NOLOCK) 
		where  isnull(ICO,'')=@str 
	else 
	if @typ=4
		INSERT @partner  select KLIC_ODB2,isnull(ODBERATEL,''),
		isnull(ADRESA1,''),isnull(ADRESA2,''),isnull(ICO,''),isnull(DIC,''),isnull(PSC,''),
		case when (isnull(ZEM,'CZ')='CZ') then 1 else 0 end,'',KLIC_DOS
		from FACISODB with(NOLOCK) 
		where  isnull(DIC,'')=@str
	--else 
	--if @typ=5
	--	INSERT @partner  select distinct F.KLIC_ODB2,isnull(F.ODBERATEL,''),
	--	isnull(F.ADRESA1,''),isnull(F.ADRESA2,''),isnull(F.ICO,''),isnull(F.DIC,''),isnull(PSC,''),
	--	case when (isnull(F.ZEM,'CZ')='CZ') then 1 else 0 end,isnull(H.MISTODOD,''),KLIC_DOS
	--	from FACISODB F with(NOLOCK)
	--	inner join SKLAD_PR H with(NOLOCK) on F.KLIC_ODB2=H.KLIC_ODB
	--	inner join SKLREZZB ZB with(NOLOCK) on H.CIS_DOKL=ZB.CIS_DOKL
	--	where isnull(H.MISTODOD,'') like @str+'%' and isnull(H.PRIKAZ,0)=1
	--			and ZB.MNOZSTVI<>ZB.REALIZOVAN
	return
END
GO


