
ALTER    PROCEDURE [dbo].[CZPRO_A2_insert]

@PRODLINE [char] (2) ,
@PRODSTAT [char] (1) ,
@ITEMNMBR [char] (13) ,
@QUANTITY [numeric](19, 0),
@QUANT_NA [numeric] (19, 0),	--novy parametr
@DATEEVE [char] (21) ,
@USERPRO [char] (12) ,
@USERIS [char] (12) ,
@DATEPROC [char] (21) ,
@REZ1 [char] (12) ,
@INDEX [char] (12),              --novy parametr, pocitany zaznam(counter) zapisu z koncentratoru ...
@SERLTNUM [nvarchar] (255) = NULL	 --3.7.2017 JiS, parametr Sarze vyrobku
AS

--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int, @ins2_error int

--Zacatek transakce
BEGIN TRAN

--Vlozeni skutecne nasnimanych dat
--30.8.2010 JiS: v nove uprave koncentratoru lze zadavat odpocet, ktery je zaznamenan zapornym cislem odvodu v QUANTITY
IF (@QUANTITY <> 0)
BEGIN	
	--Vlozeni poctu nasnimanych
	INSERT INTO [dbo].[CZPRO_A2]([PRODLINE], [PRODSTAT], [ITEMNMBR], [OITMNMBR], [QUANTITY], [DATEEVE], [USERPRO], [USERIS], [DATEPROC], [REZ1], [INDEX], [SERLTNUM])
	VALUES(@PRODLINE, @PRODSTAT, @ITEMNMBR, @ITEMNMBR, @QUANTITY, @DATEEVE, @USERPRO, @USERIS, @DATEPROC, @REZ1, @INDEX, @SERLTNUM)
END

--Zjisteni zda v prvnim insertu nastala chyba
SELECT @ins1_error = @@ERROR

--Pokud je pocet nepruchodu vetsi jak nula, pak vlozit dalsi zaznamy
IF (@QUANT_NA > 0)
BEGIN	
	--Vlozeni poctu nepruchodu
	INSERT INTO [dbo].[CZPRO_A2]([PRODLINE], [PRODSTAT], [ITEMNMBR], [OITMNMBR], [QUANTITY], [DATEEVE], [USERPRO], [USERIS], [DATEPROC], [REZ1], [INDEX], [SERLTNUM])
	VALUES(@PRODLINE, @PRODSTAT, @ITEMNMBR, 'N/A', @QUANT_NA, @DATEEVE, @USERPRO, @USERIS, @DATEPROC, @REZ1, @INDEX, @SERLTNUM)
END

--Zjisteni zda v druhem insertu nastala chyba
SELECT @ins2_error = @@ERROR

--Test na chybu vlozeni
IF (@ins1_error = 0 AND @ins2_error = 0)
BEGIN
	--Chyba nenastala => potvrzeni transakce
	PRINT 'ZAZNAM BYL USPESNE VLOZEN'
	COMMIT TRAN
END
ELSE
BEGIN
	--Nastala chyba zruseni transakce
	PRINT 'NASTALA CHYBA PRI VKLADANI HODNOT DO DB'
	ROLLBACK TRAN
END


GO


