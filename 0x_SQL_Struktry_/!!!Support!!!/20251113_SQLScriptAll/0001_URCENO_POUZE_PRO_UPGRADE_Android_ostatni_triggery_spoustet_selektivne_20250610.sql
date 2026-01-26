

-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 13.12. 2023
-- Kontroloval: Ing. Skøivánek Jan
-- Date: 09.06.2025
-- Description:	Vytvorene trigry pro android insert z CZMST_PE do tabulky CZMST_SE
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_SE_insert]

@CountEntries [int],
@SOPNUMBE [nvarchar](30),
@ITEMNMBR [nvarchar](40),
@ITEMTYPE [nvarchar](11),
@ITEMDESC [nvarchar](100),
@VNDDOCNM [nvarchar](21),
@VNDITNUM [nvarchar](60),
@ORD [int],
@CZ_CarKod [nvarchar](70),
@SKL_ID [nvarchar](20),
@LOCNCODE [nvarchar](11),
@MJ [nvarchar](10),
@QTYSHPPD [numeric](19, 5),
@QTYPACK [numeric](19, 5),
@CZ_DatVyr_Track [tinyint],
@CZ_DatVyr_Delka [smallint],
@CZ_SerNum_Track [tinyint],
@CZ_SerNum_Delka [smallint],
@CZ_SW_Track [tinyint],
@CZ_SW_Delka [smallint],
@CZ_Doslo [tinyint],
@Note [nvarchar](100),
@TYPEPAL [nvarchar](10),
@QTYPAL [numeric](19, 5),
@PRIORITY [tinyint],
@PRINTED [tinyint],
@USERID [int],
@DEX_ROW_ID [int],
@CZ_REZ1_Track [tinyint],
@CZ_REZ2_Track [tinyint],
@ITEMCODE [nvarchar](70),
@WEIGHT [numeric](19, 5),
@Realization_Start [datetime],
@Realization_Stop [datetime],
@CZ_Expirace_Track [tinyint]


AS
--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int

	BEGIN

--Zacatek transakce
BEGIN TRAN

		BEGIN	
			--Vlozeni poctu nasnimanych
INSERT INTO [dbo].[CZMST_SE]
           ([CountEntries],[SOPNUMBE],[ITEMNMBR],[ITEMTYPE],[ITEMDESC],[VNDDOCNM],[VNDITNUM],[ORD],[CZ_CarKod],[SKL_ID],[LOCNCODE],[MJ],[QTYSHPPD],[QTYPACK],[CZ_DatVyr_Track],[CZ_DatVyr_Delka]
           ,[CZ_SerNum_Track],[CZ_SerNum_Delka],[CZ_SW_Track],[CZ_SW_Delka],[CZ_Doslo],[Note],[TYPEPAL],[QTYPAL],[PRIORITY],[PRINTED],[USERID],[CZ_REZ1_Track],[CZ_REZ2_Track],[ITEMCODE]
           ,[WEIGHT],[Realization_Start],[Realization_Stop],[CZ_Expirace_Track])
     VALUES
           (
        @CountEntries,
        @SOPNUMBE,
        @ITEMNMBR,
        @ITEMTYPE,
        @ITEMDESC,
        @VNDDOCNM,
        @VNDITNUM,
        @ORD,
        @CZ_CarKod,
        @SKL_ID,
        @LOCNCODE,
        @MJ,
        @QTYSHPPD,
        @QTYPACK,
        @CZ_DatVyr_Track,
        @CZ_DatVyr_Delka,
        @CZ_SerNum_Track,
        @CZ_SerNum_Delka,
        @CZ_SW_Track,
        @CZ_SW_Delka,
        @CZ_Doslo,
        @Note,
        @TYPEPAL,
        @QTYPAL,
        @PRIORITY,
        @PRINTED,
        @USERID,
        --@DEX_ROW_ID,
        @CZ_REZ1_Track,
        @CZ_REZ2_Track,
        @ITEMCODE,
        @WEIGHT,
        @Realization_Start,
        @Realization_Stop,
        @CZ_Expirace_Track
		   )
		END

				--Zjisteni zda v prvnim insertu nastala chyba
		SELECT @ins1_error = @@ERROR

		--Test na chybu vlozeni
		IF (@ins1_error = 0)
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
END



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 13.12. 2023
-- Kontroloval: Ing. Skøivánek Jan
-- Date: 09.06.2025
-- Description:	insert do tabulky CZMST_I4
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_I4_insert]

	@CountEntries [int],
	@CE_Orig [int],
	@ITEMNMBR [nvarchar](40),
	@CZ_CarKod [nvarchar](70),
	@LOCNCODE [nvarchar](11),
	@SKL_ID [nvarchar](20),
	@VNDITNUM [nvarchar](60),
	@MJ [nvarchar](10),
	@QUANTITY [numeric](19, 5),
	@QUANTITYMJ [numeric](19, 5),
	@QTYPACK [numeric](19, 5),
	@SERLNMBR [nvarchar](50),
	@DATEDONE [nvarchar](8),
	@TIMEDONE [nvarchar](6),
	@USERID [int],
	@DEX_ROW_ID [int],
	@GUID [uniqueidentifier],
	@O_Checked [bit],
	@INPUT_MODE [tinyint],
	@ID_TERMINAL [int],
	@ITEMCODE [nvarchar](70),
	@REZ_1 [nvarchar](50),
	@REZ_2 [nvarchar](50),
	@WEIGHT [numeric](19, 5),
	@Expirace [datetime]



AS
--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int

	BEGIN

--Zacatek transakce
BEGIN TRAN

		BEGIN	
			--Vlozeni poctu nasnimanych
INSERT INTO [dbo].[CZMST_I4]
           ([CountEntries]
           ,[CE_Orig]
           ,[ITEMNMBR]
           ,[CZ_CarKod]
           ,[LOCNCODE]
           ,[SKL_ID]
           ,[VNDITNUM]
           ,[MJ]
           ,[QUANTITY]
           ,[QUANTITYMJ]
           ,[QTYPACK]
           ,[SERLNMBR]
           ,[DATEDONE]
           ,[TIMEDONE]
           ,[USERID]
           ,[GUID]
           ,[O_Checked]
           ,[INPUT_MODE]
           ,[ID_TERMINAL]
           ,[ITEMCODE]
           ,[REZ_1]
           ,[REZ_2]
           ,[WEIGHT]
           ,[Expirace])
     VALUES
           (
	@CountEntries,
	@CE_Orig,
	@ITEMNMBR,
	@CZ_CarKod,
	@LOCNCODE,
	@SKL_ID,
	@VNDITNUM,
	@MJ,
	@QUANTITY,
	@QUANTITYMJ,
	@QTYPACK,
	@SERLNMBR,
	@DATEDONE,
	@TIMEDONE,
	@USERID,
	--@DEX_ROW_ID IDENTITY(1,1),
	@GUID,
	@O_Checked,
	@INPUT_MODE,
	@ID_TERMINAL,
	@ITEMCODE,
	@REZ_1,
	@REZ_2,
	@WEIGHT,
	@Expirace

		   )
		END

				--Zjisteni zda v prvnim insertu nastala chyba
		SELECT @ins1_error = @@ERROR

		--Test na chybu vlozeni
		IF (@ins1_error = 0)
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
END


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 5.3. 2023
-- Kontroloval: Ing. Skøivánek Jan
-- Date: 09.06.2025
-- Description:	insert do tabulky CZMST_PI
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_PI_insert]

 @CountEntries [int] ,
 @PONUMBER [nvarchar](30) ,
 @ORD [int] ,
 @ITEMNMBR [nvarchar](40) ,
 @VNDDOCNM [nvarchar](21) ,
 @VNDITNUM [nvarchar](60) ,
 @SKL_ID [nvarchar](20) ,
 @LOCNCODE [nvarchar](11) ,
 @MJ [nvarchar](10) ,
 @QTYSHPPD [numeric](19, 5) ,
 @QTYSHPPDMJ [numeric](19, 5) ,
 @QTYPACK [numeric](19, 5) ,
 @SERLTNUM [nvarchar](50) ,
 @KOD_SW [nvarchar](11) ,
 @DAT_VYROBY [nvarchar](11) ,
 @DATEDONE [nvarchar](8) ,
 @TIMEDONE [nvarchar](6) ,
 @CZ_CarKod [nvarchar](70) ,
 @REZ_1 [nvarchar](50) ,
 @REZ_2 [nvarchar](50) ,
 @USER_ID [int] ,
 @DEX_ROW_ID [int] ,
 @GUID [uniqueidentifier] ,
 @INPUT_MODE [tinyint] ,
 @ID_TERMINAL [int] ,
 @WEIGHT [numeric](19, 5) ,
 @NMBRPAL [nvarchar](50) ,
 @TYPEPAL [nvarchar](10) ,
 @ITEMCODE [nvarchar](70) ,
 @Expirace [datetime] ,
 @AttributeToSN [nvarchar](50)



AS
--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int

	BEGIN

--Zacatek transakce
BEGIN TRAN

		BEGIN	
			--Vlozeni poctu nasnimanych
INSERT INTO [dbo].[CZMST_PI]
           ([CountEntries]
           ,[PONUMBER]
           ,[ORD]
           ,[ITEMNMBR]
           ,[VNDDOCNM]
           ,[VNDITNUM]
           ,[SKL_ID]
           ,[LOCNCODE]
           ,[MJ]
           ,[QTYSHPPD]
           ,[QTYSHPPDMJ]
           ,[QTYPACK]
           ,[SERLTNUM]
           ,[KOD_SW]
           ,[DAT_VYROBY]
           ,[DATEDONE]
           ,[TIMEDONE]
           ,[CZ_CarKod]
           ,[REZ_1]
           ,[REZ_2]
           ,[USER_ID]
           ,[GUID]
           ,[INPUT_MODE]
           ,[ID_TERMINAL]
           ,[WEIGHT]
           ,[NMBRPAL]
           ,[TYPEPAL]
           ,[ITEMCODE]
           ,[Expirace]
           ,[AttributeToSN])
     VALUES
           (
	@CountEntries,
@PONUMBER,
@ORD,
@ITEMNMBR,
@VNDDOCNM,
@VNDITNUM,
@SKL_ID,
@LOCNCODE,
@MJ,
@QTYSHPPD,
@QTYSHPPDMJ,
@QTYPACK,
@SERLTNUM,
@KOD_SW,
@DAT_VYROBY,
@DATEDONE,
@TIMEDONE,
@CZ_CarKod,
@REZ_1,
@REZ_2,
@USER_ID,
---@DEX_ROW_ID,
@GUID,
@INPUT_MODE,
@ID_TERMINAL,
@WEIGHT,
@NMBRPAL,
@TYPEPAL,
@ITEMCODE,
@Expirace,
@AttributeToSN


		   )
		END

				--Zjisteni zda v prvnim insertu nastala chyba
		SELECT @ins1_error = @@ERROR

		--Test na chybu vlozeni
		IF (@ins1_error = 0)
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
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 4.4. 2024
-- Kontroloval: Ing. Skøivánek Jan
-- Date: 09.06.2025
-- Description:	insert do tabulky Production
-- =============================================
CREATE PROCEDURE [dbo].[Production_insert]

	@CountEntries int,
    @SOPNUMBE nvarchar(30),
    @ITEMNMBR nvarchar(40),
    @ITEMTYPE nvarchar(11),
    @ITEMMJ nvarchar(5),
    @ITEMDESC nvarchar(100),
    @ORD int,
    @TIMEMODE int,
    @TIMEPREPSTART datetime,
    @TIMEPREPSTOP datetime,
    @TIMEPREP real,
    @TIMEUNIT real,
    @TIMESTART datetime,
    @TIMESTOP datetime,
    @TIMECORSTART datetime,
    @TIMECORSTOP datetime,
    @TIMECOR real,
    @TIMECRID int,
    @TIMECRIDTYPE tinyint,
    @id int,
    @loginid nvarchar(20),
    @machineid nvarchar(20),
    @operationid nvarchar(16),
    @dateeve datetime,
    @qty numeric(19, 5),
    @qtyReal numeric(19, 5),
    @QTYPACK numeric(19, 5),
    @QTYPACKMJ nvarchar(5),
    @description nvarchar(max),
    @BarcodeP nvarchar(31),
    @UserID nvarchar(20),
    @TermID tinyint,
    @ISOK datetime,
    @GUID uniqueidentifier,
    @SOUBEHGUID uniqueidentifier,
    @CORRGUID uniqueidentifier,
    @qtyOld numeric(19, 5),
    @idVS nvarchar(10),
    @dateedit datetime,
    @SKL_ID nvarchar(20),
    @LOCNCODE nvarchar(11),
    @SERLTNUM nvarchar(50),
    @EXPIRATION nvarchar(50),
    @NMBRPAL nvarchar(50),
    @TYPEPAL nvarchar(10),
    @PackType nvarchar(50),
    @status int,
    @WEIGHT numeric(19, 5),
    @STORNOGUID uniqueidentifier,
    @REZ_1 nvarchar(100),
    @REZ_2 nvarchar(100),
    @REZ_3 nvarchar(100),
    @REZ_4 nvarchar(100),
    @REZ_5 nvarchar(100),
    @WEIGHT_OLD numeric(19, 5)

AS
--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int

	BEGIN

--Zacatek transakce
BEGIN TRAN

		BEGIN	
			--Vlozeni poctu nasnimanych
INSERT INTO [dbo].[Production]
           ([CountEntries]
      ,[SOPNUMBE]
      ,[ITEMNMBR]
      ,[ITEMTYPE]
      ,[ITEMMJ]
      ,[ITEMDESC]
      ,[ORD]
      ,[TIMEMODE]
      ,[TIMEPREPSTART]
      ,[TIMEPREPSTOP]
      ,[TIMEPREP]
      ,[TIMEUNIT]
      ,[TIMESTART]
      ,[TIMESTOP]
      ,[TIMECORSTART]
      ,[TIMECORSTOP]
      ,[TIMECOR]
      ,[TIMECRID]
      ,[TIMECRIDTYPE]
     -- ,[id]
      ,[loginid]
      ,[machineid]
      ,[operationid]
      ,[dateeve]
      ,[qty]
      ,[qtyReal]
      ,[QTYPACK]
      ,[QTYPACKMJ]
      ,[description]
      ,[BarcodeP]
      ,[UserID]
      ,[TermID]
      ,[ISOK]
      ,[GUID]
      ,[SOUBEHGUID]
      ,[CORRGUID]
      ,[qtyOld]
      ,[idVS]
      ,[dateedit]
      ,[SKL_ID]
      ,[LOCNCODE]
      ,[SERLTNUM]
      ,[EXPIRATION]
      ,[NMBRPAL]
      ,[TYPEPAL]
      ,[PackType]
      ,[status]
      ,[WEIGHT]
      ,[STORNOGUID]
      ,[REZ_1]
      ,[REZ_2]
      ,[REZ_3]
      ,[REZ_4]
      ,[REZ_5]
      ,[WEIGHT_OLD])
     VALUES
           (
@CountEntries,
@SOPNUMBE,
@ITEMNMBR,
@ITEMTYPE,
@ITEMMJ,
@ITEMDESC,
@ORD,
@TIMEMODE,
@TIMEPREPSTART,
@TIMEPREPSTOP,
@TIMEPREP,
@TIMEUNIT,
@TIMESTART,
@TIMESTOP,
@TIMECORSTART,
@TIMECORSTOP,
@TIMECOR,
@TIMECRID,
@TIMECRIDTYPE,
--@id,
@loginid,
@machineid,
@operationid,
@dateeve,
@qty,
@qtyReal,
@QTYPACK,
@QTYPACKMJ,
@description,
@BarcodeP,
@UserID,
@TermID,
@ISOK,
@GUID,
@SOUBEHGUID,
@CORRGUID,
@qtyOld,
@idVS,
@dateedit,
@SKL_ID,
@LOCNCODE,
@SERLTNUM,
@EXPIRATION,
@NMBRPAL,
@TYPEPAL,
@PackType,
@status,
@WEIGHT,
@STORNOGUID,
@REZ_1,
@REZ_2,
@REZ_3,
@REZ_4,
@REZ_5,
@WEIGHT_OLD


		   )
		END

				--Zjisteni zda v prvnim insertu nastala chyba
		SELECT @ins1_error = @@ERROR

		--Test na chybu vlozeni
		IF (@ins1_error = 0)
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
END


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 4.4. 2024
-- Kontroloval: Ing. Skøivánek Jan
-- Date: 09.06.2025
-- Description:	preliti dat z SI do PI a do I4 a do Production
-- =============================================
CREATE TRIGGER [dbo].[Trigger_SI_do_I4_PI_Production] 
   ON  [dbo].[CZMST_SI] 
   FOR INSERT

AS

BEGIN

       SET NOCOUNT ON;

	--deklarace promennych
DECLARE @CountEntries [int]
DECLARE @SOPNUMBE [nvarchar](30)
DECLARE @ITEMNMBR [nvarchar](40)
DECLARE @ORD [int]
DECLARE @VNDDOCNM [nvarchar](21)
DECLARE @VNDITNUM [nvarchar](60)
DECLARE @CZ_CarKod [nvarchar](70)
DECLARE @SKL_ID [nvarchar](20)
DECLARE @LOCNCODE [nvarchar](11)
DECLARE @MJ [nvarchar](10)
DECLARE @QTYSHPPD [numeric](19, 5)
DECLARE @QTYPACK [numeric](19, 5)
DECLARE @QTYSHPPDMJ [numeric](19, 5)
DECLARE @SERLTNUM [nvarchar](50)
DECLARE @KOD_SW [nvarchar](11)
DECLARE @DAT_VYROBY [nvarchar](11)
DECLARE @REZ_1 [nvarchar](50)
DECLARE @REZ_2 [nvarchar](50)
DECLARE @ODBER_ID [nvarchar](12)
DECLARE @DATEDONE [nvarchar](8)
DECLARE @TIMEDONE [nvarchar](6)
DECLARE @USER_ID [int]
DECLARE @TYPEPAL [nvarchar](10)
DECLARE @NMBRPAL [nvarchar](50)
DECLARE @PRINTED [tinyint]
DECLARE @DEX_ROW_ID [int]
DECLARE @GUID [uniqueidentifier]
DECLARE @INPUT_MODE [tinyint]
DECLARE @ID_TERMINAL [int]
DECLARE @ITEMCODE [nvarchar](70)
DECLARE @WEIGHT [numeric](19, 5)
DECLARE @Expirace [datetime]


  -- Insert statements for trigger here

       BEGIN TRAN;

     

       BEGIN        

           DECLARE curPol CURSOR LOCAL STATIC FOR

           SELECT DEX_ROW_ID

             FROM inserted;

 

           OPEN curPol;

 

           WHILE ( 1 = 1 ) BEGIN

           FETCH NEXT FROM curPol INTO @DEX_ROW_ID;

 

           IF ( @@FETCH_STATUS <> 0 )

                 BREAK;

 

                    SELECT
@CountEntries = i.CountEntries,
@SOPNUMBE = i.SOPNUMBE,
@ITEMNMBR = i.ITEMNMBR,
@ORD = i.ORD,
@VNDDOCNM = i.VNDDOCNM,
@VNDITNUM = i.VNDITNUM,
@CZ_CarKod = i.CZ_CarKod,
@SKL_ID = i.SKL_ID,
@LOCNCODE = i.LOCNCODE,
@MJ = i.MJ,
@QTYSHPPD = i.QTYSHPPD,
@QTYPACK = i.QTYPACK,
@QTYSHPPDMJ = i.QTYSHPPDMJ,
@SERLTNUM = i.SERLTNUM,
@KOD_SW = i.KOD_SW,
@DAT_VYROBY = i.DAT_VYROBY,
@REZ_1 = i.REZ_1,
@REZ_2 = i.REZ_2,
@ODBER_ID = i.ODBER_ID,
@DATEDONE = i.DATEDONE,
@TIMEDONE = i.TIMEDONE,
@USER_ID = i.USER_ID,
@TYPEPAL = i.TYPEPAL,
@NMBRPAL = i.NMBRPAL,
@PRINTED = i.PRINTED,
@DEX_ROW_ID = i.DEX_ROW_ID,
@GUID = i.GUID,
@INPUT_MODE = i.INPUT_MODE,
@ID_TERMINAL = i.ID_TERMINAL,
@ITEMCODE = i.ITEMCODE,
@WEIGHT = i.WEIGHT,
@Expirace = i.Expirace

	from INSERTED i
	  WHERE i.DEX_ROW_ID = @DEX_ROW_ID;


	  --select @CountEntries @SOPNUMBE @ORD

	 DECLARE @ItemTypeResult NVARCHAR(255);  -- Upravte délku podle skuteèných potøeb


SELECT TOP 1
    @ItemTypeResult = [ITEMTYPE]
FROM
    [CZMST_SE]
WHERE
    [CountEntries] = @CountEntries
    AND [SOPNUMBE] = @SOPNUMBE
    AND [ORD] = @ORD;

-- Použití hodnoty ItemTypeResult podle potøeby
-- ...

-- Pøíklad výpisu hodnoty ItemTypeResult
PRINT @ItemTypeResult;

-- Podmínka IF
IF @ItemTypeResult = 'P'
BEGIN
    -- Akce, která se provede, pokud ItemTypeResult je rovno 'P'
    PRINT 'ItemTypeResult je P. Provedeno další akce pro P.';

	EXECUTE dbo.CZMST_PI_insert 

@CountEntries,
@SOPNUMBE,--@PONUMBER,
@ORD,
@ITEMNMBR,
@VNDDOCNM,
@VNDITNUM,
@SKL_ID,
@LOCNCODE,
@MJ,
@QTYSHPPD,
@QTYSHPPDMJ,
@QTYPACK,
@SERLTNUM,
@KOD_SW,
@DAT_VYROBY,
@DATEDONE,
@TIMEDONE,
@CZ_CarKod,
@REZ_1,
@REZ_2,
@USER_ID,
@DEX_ROW_ID,
@GUID,
@INPUT_MODE,
@ID_TERMINAL,
@WEIGHT,
@NMBRPAL,
@TYPEPAL,
@ITEMCODE,
@Expirace,
null;   --@AttributeToSN;

END

ELSE IF @ItemTypeResult = 'I'

BEGIN
    -- Akce, která se provede, pokud ItemTypeResult je rovno 'I'
	-- JaS 20250609 zkontrolovat na reálných datech
    PRINT 'ItemTypeResult je I. Provedeno další akce pro I.';
	EXECUTE dbo.CZMST_I4_insert 
   @CountEntries,
	NULL,--@CE_Orig,
	@ITEMNMBR,
	@CZ_CarKod,
	@LOCNCODE,
	@SKL_ID,
	@VNDITNUM,
	@MJ,
	@QTYSHPPD,--@QUANTITY,
	@QTYSHPPD,--@QUANTITYMJ,
	@QTYPACK,
	@SERLTNUM,--@SERLNMBR,
	@DATEDONE,
	@TIMEDONE,
	NULL,--@USERID,
	@DEX_ROW_ID,
	@GUID,
	0,--@O_Checked,
	@INPUT_MODE,
	@ID_TERMINAL,
	@ITEMCODE,
	@REZ_1,
	@REZ_2,
	@WEIGHT,
	@Expirace;



END

ELSE IF @ItemTypeResult = 'V'

BEGIN
    -- Akce, která se provede, pokud ItemTypeResult je rovno 'V'
    -- JaS 20250609 zkontrolovat na reálných datech
    PRINT 'ItemTypeResult je V. Provedeno další akce pro V.';

declare @aktualniCas datetime

set @aktualniCas = GETDATE()

EXECUTE dbo.Production_insert 
	@CountEntries,
    @SOPNUMBE,
    @ITEMNMBR,
    '',--66, -- @ITEMTYPE,
    @MJ,--66, -- @ITEMMJ,
    '',--66, -- @ITEMDESC,
    @ORD,
    0, -- @TIMEMODE,
    null, -- @TIMEPREPSTART,
    null, -- @TIMEPREPSTOP,
    null, -- @TIMEPREP,
    null, -- @TIMEUNIT,
    null, -- @TIMESTART,
    null, -- @TIMESTOP,
    null, -- @TIMECORSTART,
    null, -- @TIMECORSTOP,
   null, -- 66, -- @TIMECOR,
   null, -- 66, -- @TIMECRID,
   null, -- 66, -- @TIMECRIDTYPE,
    66, -- @id,
    @USER_ID, -- @loginid,
    @ID_TERMINAL, -- @machineid,
    null, -- @operationid,
    @aktualniCas, -- @dateeve,
    @QTYSHPPD, -- @qty,
    @QTYSHPPD ,--66, -- @qtyReal,
    @QTYPACK,
    0, -- @QTYPACKMJ,
    '', -- @description,
    @CZ_CarKod , -- @BarcodeP,
    @USER_ID,--66, -- @UserID,
   @ID_TERMINAL ,-- 66, -- @TermID,
    null, -- @ISOK,
    @GUID,
    null, -- @SOUBEHGUID,
    null, -- @CORRGUID,
    0, -- @qtyOld,
    @QTYSHPPD, -- @idVS,
    null, -- @dateedit,
    @SKL_ID,
    @LOCNCODE,
    @SERLTNUM,
   @Expirace ,-- 66, -- @EXPIRATION,
    @NMBRPAL,
    @TYPEPAL,
    '', -- @PackType,
    '', -- @status,
    @WEIGHT,
    null, -- @STORNOGUID,
    @REZ_1,
    @REZ_2,
    null, --  '', -- @REZ_3,
    null, --  '', -- @REZ_4,
    null, --  '', -- @REZ_5,
    null; -- 0 -- @WEIGHT_OLD;


END

ELSE

BEGIN
    -- Akce, která se provede, pokud ItemTypeResult není ani 'P', ani 'I'
    PRINT 'ItemTypeResult není ani P, ani I. Provedeno jiné akce.';

END;




     END;

 

           CLOSE curPol;

           DEALLOCATE curPol;

 

    END;

 

       COMMIT TRAN;

 

END;


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ing. Matouš Rathouzský
-- Create date: 3.4. 2024
-- Description:	triger ktery vytvori zaznam v SE se strukturou VPP, na zaklade zaznamu VPH se zmenou prvku ACTIVE na 1
--                na zaklade zaznamu VPH se zmenou prvku ACTIVE na 0 vymaze vsechny zaznamy z SE tykajicich se Countentries z VPH
-- =============================================
CREATE TRIGGER [dbo].[trg_CZPRO_VPH_ActiveChange_new]
ON [dbo].[CZPRO_VPH]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Active) -- Zkontrolujeme, zda byl aktualizován sloupec Active
    BEGIN
        -- Deklarace promìnných pro VPH
        DECLARE @VPH_CountEntries int;
        DECLARE @VPH_SOPNUMBE nvarchar(30);
        DECLARE @VPH_SOPTYPE nvarchar(11);
        DECLARE @VPH_SOPDESC nvarchar(100);
        DECLARE @VPH_VNDDOCNMH nvarchar(21);
        DECLARE @VPH_BarcodeH nvarchar(31);
        DECLARE @VPH_LOCNCODE nvarchar(11);
        DECLARE @VPH_DateProd smallint;
        DECLARE @VPH_Rez1 nvarchar(50);
        DECLARE @VPH_Rez2 nvarchar(50);
        DECLARE @VPH_TermID tinyint;
        DECLARE @VPH_LSTMod datetime;
        DECLARE @VPH_DEX_ROW_ID int;
        DECLARE @VPH_Active tinyint;
        DECLARE @VPH_USERID int;

        -- Deklarace promìnných pro VPP
        DECLARE @VPP_CountEntries int;
        DECLARE @VPP_SOPNUMBE nvarchar(30);
        DECLARE @VPP_ITEMNMBR nvarchar(40);
        DECLARE @VPP_ITEMTYPE nvarchar(11);
        DECLARE @VPP_ITEMDESC nvarchar(100);
        DECLARE @VPP_ITEMMJ nvarchar(5);
        DECLARE @VPP_VNDDOCNMP nvarchar(21);
        DECLARE @VPP_VNDITNUM nvarchar(60);
        DECLARE @VPP_ORD int;
        DECLARE @VPP_BarcodeP nvarchar(31);
        DECLARE @VPP_LOCNCODE nvarchar(11);
        DECLARE @VPP_QTYSHPPD numeric(19, 5);
        DECLARE @VPP_QTYDOKON numeric(19, 5);
        DECLARE @VPP_QTYPACK numeric(19, 5);
        DECLARE @VPP_QTYPACKMJ nvarchar(5);
        DECLARE @VPP_TIMEMODE int;
        DECLARE @VPP_TIMEPREP real;
        DECLARE @VPP_TIMEUNIT real;
        DECLARE @VPP_DtProdT tinyint;
        DECLARE @VPP_DtProdL smallint;
        DECLARE @VPP_SerNumT tinyint;
        DECLARE @VPP_SerNumL smallint;
        DECLARE @VPP_VerT tinyint;
        DECLARE @VPP_VerL smallint;
        DECLARE @VPP_TermID tinyint;
        DECLARE @VPP_LSTMod datetime;
        DECLARE @VPP_Realization_Start datetime;
        DECLARE @VPP_Realization_Stop datetime;
        DECLARE @VPP_BarcodeT tinyint;
        DECLARE @VPP_CZ_REZ1_Track tinyint;
        DECLARE @VPP_CZ_REZ2_Track tinyint;
        DECLARE @VPP_CZ_REZ3_Track tinyint;
        DECLARE @VPP_CZ_REZ4_Track tinyint;
        DECLARE @VPP_CZ_REZ5_Track tinyint;
        DECLARE @VPP_WEIGHT_TARA numeric(19, 5);
        DECLARE @VPP_WEIGHT_NETTO numeric(19, 5);
        DECLARE @VPP_WEIGHT_TOL_PLUS numeric(19, 5);
        DECLARE @VPP_WEIGHT_TOL_MINUS numeric(19, 5);
		
        DECLARE @VPP_DEX_ROW_ID int;
        -- Získání aktualizovaných dat
        SELECT 
            @VPH_CountEntries = inserted.CountEntries,
            @VPH_SOPNUMBE = inserted.SOPNUMBE,
            @VPH_SOPTYPE = inserted.SOPTYPE,
            @VPH_SOPDESC = inserted.SOPDESC,
            @VPH_VNDDOCNMH = inserted.VNDDOCNMH,
            @VPH_BarcodeH = inserted.BarcodeH,
            @VPH_LOCNCODE = inserted.LOCNCODE,
            @VPH_DateProd = inserted.DateProd,
            @VPH_Rez1 = inserted.Rez1,
            @VPH_Rez2 = inserted.Rez2,
            @VPH_TermID = inserted.TermID,
            @VPH_LSTMod = inserted.LSTMod,
            @VPH_DEX_ROW_ID = inserted.DEX_ROW_ID,
            @VPH_Active = inserted.Active,
            @VPH_USERID = inserted.USERID
        FROM inserted;

        -- Pokud byla hodnota sloupce Active zmìnìna z 1 na 0
        --IF EXISTS (SELECT 1 FROM deleted WHERE Active = 1)
		IF EXISTS (SELECT 1 FROM deleted WHERE Active <> 0)
        BEGIN
            -- Pokud se hodnota zmìnila z 1 na 0, provede smazání záznamu z tabulky CZMST_SE
            IF EXISTS (SELECT 1 FROM inserted WHERE Active = 0)
			--IF NOT EXISTS (SELECT 1 FROM inserted WHERE Active = 1)
            BEGIN
                DELETE FROM [dbo].CZMST_SE 
                WHERE CountEntries = @VPH_CountEntries;
            END;
        END;

        -- Pokud byla hodnota sloupce Active zmìnìna z 0 na 1
        --IF EXISTS (SELECT 1 FROM deleted WHERE Active = 0)
		IF EXISTS (SELECT 1 FROM deleted WHERE Active <> 1)
        BEGIN
            -- Pokud se hodnota zmìnila na 1, vytvoø nový záznam v tabulce CZMST_SE
            IF EXISTS (SELECT 1 FROM inserted WHERE Active = 1)
            BEGIN
                DECLARE dataCursor CURSOR FOR
                SELECT 
                    [CountEntries],
                    [SOPNUMBE],
                    [ITEMNMBR],
                    [ITEMTYPE],
                    [ITEMDESC],
                    [ITEMMJ],
                    [VNDDOCNMP],
                    [VNDITNUM],
                    [ORD],
                    [BarcodeP],
                    [LOCNCODE],
                    [QTYSHPPD],
                    [QTYDOKON],
                    [QTYPACK],
                    [QTYPACKMJ],
                    [TIMEMODE],
                    [TIMEPREP],
                    [TIMEUNIT],
                    [DtProdT],
                    [DtProdL],
                    [SerNumT],
                    [SerNumL],
                    [VerT],
                    [VerL],
                    [TermID],
                    [LSTMod],
                    [DEX_ROW_ID],
                    [Realization_Start],
                    [Realization_Stop],
                    [BarcodeT],
                    [CZ_REZ1_Track],
                    [CZ_REZ2_Track],
                    [CZ_REZ3_Track],
                    [CZ_REZ4_Track],
                    [CZ_REZ5_Track],
                    [WEIGHT_TARA],
                    [WEIGHT_NETTO],
                    [WEIGHT_TOL_PLUS],
                    [WEIGHT_TOL_MINUS]
                FROM dbo.CZPRO_VPP
                WHERE CountEntries = @VPH_CountEntries
                  AND SOPNUMBE = @VPH_SOPNUMBE
                  --AND LOCNCODE = @VPH_LOCNCODE
                  --AND TermID = @VPH_TermID
                  --AND LSTMod = @VPH_LSTMod;

                OPEN dataCursor;
                FETCH NEXT FROM dataCursor INTO @VPP_CountEntries, @VPP_SOPNUMBE, @VPP_ITEMNMBR, @VPP_ITEMTYPE, @VPP_ITEMDESC, @VPP_ITEMMJ, @VPP_VNDDOCNMP, @VPP_VNDITNUM, @VPP_ORD, @VPP_BarcodeP, @VPP_LOCNCODE, @VPP_QTYSHPPD, @VPP_QTYDOKON, @VPP_QTYPACK, @VPP_QTYPACKMJ, @VPP_TIMEMODE, @VPP_TIMEPREP, @VPP_TIMEUNIT, @VPP_DtProdT, @VPP_DtProdL, @VPP_SerNumT, @VPP_SerNumL, @VPP_VerT, @VPP_VerL, @VPP_TermID, @VPP_LSTMod, @VPP_DEX_ROW_ID, @VPP_Realization_Start, @VPP_Realization_Stop, @VPP_BarcodeT, @VPP_CZ_REZ1_Track, @VPP_CZ_REZ2_Track, @VPP_CZ_REZ3_Track, @VPP_CZ_REZ4_Track, @VPP_CZ_REZ5_Track, @VPP_WEIGHT_TARA, @VPP_WEIGHT_NETTO, @VPP_WEIGHT_TOL_PLUS, @VPP_WEIGHT_TOL_MINUS;

                -- Cyklus WHILE pro zápis záznamù do tabulky CZMST_SE
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    -- Zde doplòte kód pro vložení záznamu do tabulky CZMST_SE na základì hodnot z tabulky VPP
					-- JaS 20250609 zkontrolovat na reálných datech
					----------------------------------------SE zapis start--------------------------------------------------------------------

EXECUTE dbo.CZMST_SE_insert

                    @VPP_CountEntries,

                    @VPP_SOPNUMBE,--'Vyroba',--@SOPNUMBE,

                    @VPP_ITEMNMBR,

                    'V',--@ITEMTYPE,

                    @VPP_ITEMDESC,

                    @VPP_VNDDOCNMP,

                    @VPP_VNDITNUM,--66,--@CZ_CarKod,--@VNDITNUM,

                    @VPP_ORD,--66,--@CountEntries,--@ORD,

                    @VPP_BarcodeP,--66,--@CZ_CarKod,

                   '',-- null,--66,--@skl_id,

                    @VPP_LOCNCODE,

                    @VPP_ITEMMJ,--66,--@DMJ,--@MJ,

                    @VPP_QTYSHPPD,--66,--@QUANTITY,--@QTYSHPPD,

                    @VPP_QTYPACK,

                    0,--@CZ_DatVyr_Track,

                    0,--@CZ_DatVyr_Delka,

                    @VPP_SerNumT,-- 66,--@CZ_SerNum_Track,

                    @VPP_SerNumL,-- 0,--@CZ_SerNum_Delka,

                    @VPP_VerT ,--0,--@CZ_SW_Track,

                    @VPP_VerL ,--0,--@CZ_SW_Delka,

                   @VPP_TermID ,-- 0,--@CZ_Doslo,

                    @VPH_SOPDESC ,--'',--@Note,

                    '',--@TYPEPAL,

                   @VPP_QTYPACK ,-- 0,--@QTYPAL,

                    0,--@PRIORITY,

                    0,--@PRINTED,

                    0,--@USERID,

                    66,--@DEX_ROW_ID,

                    @VPP_CZ_REZ1_Track,--66,--@CZ_REZ1_Track,

                    @VPP_CZ_REZ2_Track,--66,--@CZ_REZ2_Track,

                   @VPP_BarcodeP,-- 66,--@ITEMCODE,

                  @VPP_WEIGHT_NETTO ,--  NULL,--@WEIGHT,

                 null,--  @VPP_Realization_Start,

                   null,-- @VPP_Realization_Stop,

                   0;-- 66;--@CZ_Expirace_Track;

----------------------------------------SE zapis konec--------------------------------------------------------------------


                    FETCH NEXT FROM dataCursor INTO @VPP_CountEntries, @VPP_SOPNUMBE, @VPP_ITEMNMBR, @VPP_ITEMTYPE, @VPP_ITEMDESC, @VPP_ITEMMJ, @VPP_VNDDOCNMP, @VPP_VNDITNUM, @VPP_ORD, @VPP_BarcodeP, @VPP_LOCNCODE, @VPP_QTYSHPPD, @VPP_QTYDOKON, @VPP_QTYPACK, @VPP_QTYPACKMJ, @VPP_TIMEMODE, @VPP_TIMEPREP, @VPP_TIMEUNIT, @VPP_DtProdT, @VPP_DtProdL, @VPP_SerNumT, @VPP_SerNumL, @VPP_VerT, @VPP_VerL, @VPP_TermID, @VPP_LSTMod, @VPP_DEX_ROW_ID, @VPP_Realization_Start, @VPP_Realization_Stop, @VPP_BarcodeT, @VPP_CZ_REZ1_Track, @VPP_CZ_REZ2_Track, @VPP_CZ_REZ3_Track, @VPP_CZ_REZ4_Track, @VPP_CZ_REZ5_Track, @VPP_WEIGHT_TARA, @VPP_WEIGHT_NETTO, @VPP_WEIGHT_TOL_PLUS, @VPP_WEIGHT_TOL_MINUS;
                END;

                CLOSE dataCursor;
                DEALLOCATE dataCursor;
            END;
        END;
    END;
END;



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:          Ing. Rathouzsky Matous
-- Create date: 5.3. 2024
-- Kontroloval: Ing. Skøivánek Jan
-- Date: 09.06.2025
-- Description:     preliti dat z PE do SE
-- uprava MH - cele do cursoru - inserted nemusi mit pouze jeden zaznam
-- =============================================

CREATE TRIGGER [dbo].[Trigger_PE_do_SE]

   ON  [dbo].[CZMST_PE]

   FOR INSERT

AS

BEGIN

       SET NOCOUNT ON;


	declare @CountEntries [int] 
	declare @PONUMBER [nvarchar](30) 
	declare @ITEMNMBR [nvarchar](40) 
	declare @ITEMDESC [nvarchar](100) 
	declare @ORD [int] 
	declare @VNDDOCNM [nvarchar](21) 
	declare @VNDITNUM [nvarchar](60) 
	declare @CZ_CarKod [nvarchar](70) 
	declare @SKL_ID [nvarchar](20) 
	declare @LOCNCODE [nvarchar](11) 
	declare @MJ [nvarchar](10) 
	declare @QTYSHPPD [numeric](19, 5) 
	declare @QTYPACK [numeric](19, 5) 
	declare @CZ_DatVyr_Track [tinyint] 
	declare @CZ_DatVyr_Delka [smallint] 
	declare @CZ_SerNum_Track [tinyint] 
	declare @CZ_SerNum_Delka [smallint] 
	declare @CZ_SW_Track [tinyint] 
	declare @CZ_SW_Delka [smallint] 
	declare @CZ_Doslo [tinyint] 
	declare @DEX_ROW_ID [int] 
	declare @WEIGHT [numeric](19, 5) 
	declare @NMBRPAL [nvarchar](50) 
	declare @TYPEPAL [nvarchar](10) 
	declare @ITEMCODE [nvarchar](70) 
	declare @SERLTNUM [nvarchar](50) 
	declare @CZ_REZ1_Track [tinyint] 
	declare @CZ_REZ2_Track [tinyint] 
	declare @Realization_Start [datetime] 
	declare @Realization_Stop [datetime] 
	declare @CZ_Expirace_Track [tinyint] 

 

    -- Insert statements for trigger here

       BEGIN TRAN;

     

       BEGIN        

           DECLARE curPol CURSOR LOCAL STATIC FOR

           SELECT DEX_ROW_ID

             FROM inserted;

 

           OPEN curPol;

 

           WHILE ( 1 = 1 ) BEGIN

           FETCH NEXT FROM curPol INTO @DEX_ROW_ID;

 

           IF ( @@FETCH_STATUS <> 0 )

                 BREAK;

 

SELECT
    @CountEntries = i.CountEntries,
    @PONUMBER = i.PONUMBER,
    @ITEMNMBR = i.ITEMNMBR,
    @ITEMDESC = i.ITEMDESC,
    @ORD = i.ORD,
    @VNDDOCNM = i.VNDDOCNM,
    @VNDITNUM = i.VNDITNUM,
    @CZ_CarKod = i.CZ_CarKod,
    @SKL_ID = i.SKL_ID,
    @LOCNCODE = i.LOCNCODE,
    @MJ = i.MJ,
    @QTYSHPPD = i.QTYSHPPD,
    @QTYPACK = i.QTYPACK,
    @CZ_DatVyr_Track = i.CZ_DatVyr_Track,
    @CZ_DatVyr_Delka = i.CZ_DatVyr_Delka,
    @CZ_SerNum_Track = i.CZ_SerNum_Track,
    @CZ_SerNum_Delka = i.CZ_SerNum_Delka,
    @CZ_SW_Track = i.CZ_SW_Track,
    @CZ_SW_Delka = i.CZ_SW_Delka,
    @CZ_Doslo = i.CZ_Doslo,
    @DEX_ROW_ID = i.DEX_ROW_ID,
    @WEIGHT = i.WEIGHT,
    @NMBRPAL = i.NMBRPAL,
    @TYPEPAL = i.TYPEPAL,
    @ITEMCODE = i.ITEMCODE,
    @SERLTNUM = i.SERLTNUM,
    @CZ_REZ1_Track = i.CZ_REZ1_Track,
    @CZ_REZ2_Track = i.CZ_REZ2_Track,
    @Realization_Start = i.Realization_Start,
    @Realization_Stop = i.Realization_Stop,
    @CZ_Expirace_Track = i.CZ_Expirace_Track
FROM INSERTED i
                    WHERE i.DEX_ROW_ID = @DEX_ROW_ID;

 

                    EXECUTE dbo.CZMST_SE_insert

                    @CountEntries,

                    @PONUMBER,--@SOPNUMBE,

                    @ITEMNMBR,

                    'P',--@ITEMTYPE,

                    @ITEMDESC,

                    @VNDDOCNM,

                    @VNDITNUM,

                    @ORD,

                    @CZ_CarKod,

                    @skl_id,

                    @LOCNCODE,

                    @MJ,

                    @QTYSHPPD,

                    @QTYPACK,

                    @CZ_DatVyr_Track,

                    @CZ_DatVyr_Delka,

                    @CZ_SerNum_Track,

                    @CZ_SerNum_Delka,

                    @CZ_SW_Track,

                    @CZ_SW_Delka,

                    @CZ_Doslo,

                    '',--@Note,

                    @TYPEPAL,

                    0,--@QTYPAL,

                    3,--@PRIORITY,

                    0,--@PRINTED,

                    0,--@USERID,

                    @DEX_ROW_ID,

                    @CZ_REZ1_Track,

                    @CZ_REZ2_Track,

                    @ITEMCODE,

                    NULL,--@WEIGHT,

                    @Realization_Start,

                    @Realization_Stop,

                    @CZ_Expirace_Track;

            

           END;

 

           CLOSE curPol;

           DEALLOCATE curPol;

 

    END;

 

       COMMIT TRAN;

 

END;


/****** Object:  Trigger [dbo].[Trigger_SI_delete_I4_PI_Production]    Script Date: 22.04.2024 11:02:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 19.2. 2024
-- Kontroloval: Ing. Skøivánek Jan
-- Date: 09.06.2025
-- Description:	vymazani zaznamu pri delete v SI propis I4 a PI a Production
-- =============================================
create TRIGGER [dbo].[Trigger_SI_delete_I4_PI_Production] 
   ON  [dbo].[CZMST_SI] 
   FOR DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	--deklarace promennych
DECLARE @CountEntries [int]
DECLARE @SOPNUMBE [nvarchar](30)
DECLARE @ITEMNMBR [nvarchar](40)
DECLARE @ORD [int]
DECLARE @VNDDOCNM [nvarchar](21)
DECLARE @VNDITNUM [nvarchar](60)
DECLARE @CZ_CarKod [nvarchar](70)
DECLARE @SKL_ID [nvarchar](20)
DECLARE @LOCNCODE [nvarchar](11)
DECLARE @MJ [nvarchar](10)
DECLARE @QTYSHPPD [numeric](19, 5)
DECLARE @QTYPACK [numeric](19, 5)
DECLARE @QTYSHPPDMJ [numeric](19, 5)
DECLARE @SERLTNUM [nvarchar](50)
DECLARE @KOD_SW [nvarchar](11)
DECLARE @DAT_VYROBY [nvarchar](11)
DECLARE @REZ_1 [nvarchar](50)
DECLARE @REZ_2 [nvarchar](50)
DECLARE @ODBER_ID [nvarchar](12)
DECLARE @DATEDONE [nvarchar](8)
DECLARE @TIMEDONE [nvarchar](6)
DECLARE @USER_ID [int]
DECLARE @TYPEPAL [nvarchar](10)
DECLARE @NMBRPAL [nvarchar](50)
DECLARE @PRINTED [tinyint]
DECLARE @DEX_ROW_ID [int]
DECLARE @GUID [uniqueidentifier]
DECLARE @INPUT_MODE [tinyint]
DECLARE @ID_TERMINAL [int]
DECLARE @ITEMCODE [nvarchar](70)
DECLARE @WEIGHT [numeric](19, 5)
DECLARE @Expirace [datetime]


  -- Insert statements for trigger here

       BEGIN TRAN;

     

       BEGIN        

           DECLARE curPol CURSOR LOCAL STATIC FOR

           SELECT DEX_ROW_ID

             FROM deleted;

 

           OPEN curPol;

 

           WHILE ( 1 = 1 ) BEGIN

           FETCH NEXT FROM curPol INTO @DEX_ROW_ID;

 

           IF ( @@FETCH_STATUS <> 0 )

                 BREAK;

 

                    SELECT
@CountEntries = i.CountEntries,
@SOPNUMBE = i.SOPNUMBE,
@ITEMNMBR = i.ITEMNMBR,
@ORD = i.ORD,
@VNDDOCNM = i.VNDDOCNM,
@VNDITNUM = i.VNDITNUM,
@CZ_CarKod = i.CZ_CarKod,
@SKL_ID = i.SKL_ID,
@LOCNCODE = i.LOCNCODE,
@MJ = i.MJ,
@QTYSHPPD = i.QTYSHPPD,
@QTYPACK = i.QTYPACK,
@QTYSHPPDMJ = i.QTYSHPPDMJ,
@SERLTNUM = i.SERLTNUM,
@KOD_SW = i.KOD_SW,
@DAT_VYROBY = i.DAT_VYROBY,
@REZ_1 = i.REZ_1,
@REZ_2 = i.REZ_2,
@ODBER_ID = i.ODBER_ID,
@DATEDONE = i.DATEDONE,
@TIMEDONE = i.TIMEDONE,
@USER_ID = i.USER_ID,
@TYPEPAL = i.TYPEPAL,
@NMBRPAL = i.NMBRPAL,
@PRINTED = i.PRINTED,
@DEX_ROW_ID = i.DEX_ROW_ID,
@GUID = i.GUID,
@INPUT_MODE = i.INPUT_MODE,
@ID_TERMINAL = i.ID_TERMINAL,
@ITEMCODE = i.ITEMCODE,
@WEIGHT = i.WEIGHT,
@Expirace = i.Expirace

	from deleted i
	  WHERE i.DEX_ROW_ID = @DEX_ROW_ID;


	DELETE FROM [dbo].[CZMST_I4]
      WHERE GUID = @GUID

	  DELETE FROM [dbo].[CZMST_PI]
      WHERE GUID = @GUID

	  	  DELETE FROM [dbo].[Production]
      WHERE GUID = @GUID

	
     END;

 

           CLOSE curPol;

           DEALLOCATE curPol;

 

    END;

 

       COMMIT TRAN;

 

END;


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:          Ing. Rathouzsky Matous
-- Create date: 13.12. 2023
-- Kontroloval: Ing. Skøivánek Jan
-- Date: 09.06.2025
-- Description:     preliti dat z I1 do SE
-- uprava MH - cele do cursoru - inserted nemusi mit pouze jeden zaznam
-- =============================================

CREATE TRIGGER [dbo].[Trigger_I1_do_SE]

   ON  [dbo].[CZMST_I1]

   FOR INSERT

AS

BEGIN

       SET NOCOUNT ON;

 

       --deklarace promennych

       declare @CountEntries [int]

       declare @CE_Orig [int]

       declare @ITEMNMBR [nvarchar](40)

       declare @CZ_CarKod [nvarchar](70)

       declare @ITEMDESC [nvarchar](100)

       declare @LOCNCODE [nvarchar](11)

       declare @skl_id [nvarchar](20)

       declare @QUANTITY [numeric](19, 5)

       declare @DMJ [nvarchar](200)

       declare @DATEDONE [datetime]

       declare @IntegerValue [smallint]

       declare @TIMESPRT [smallint]

       declare @CZ_SerNum_Track [tinyint]

       declare @CZ_SerNum_Find [tinyint]

       declare @DEX_ROW_ID [int]

       declare @TerminalID [tinyint]

       declare @O_TID [tinyint]

       declare @REZ_1 [nvarchar](50)

       declare @REZ_2 [nvarchar](50)

       declare @ITEMCODE [nvarchar](70)

       declare @CZ_REZ1_Track [tinyint]

       declare @CZ_REZ2_Track [tinyint]

       declare @CZ_Expirace_Track [tinyint]

 

    -- Insert statements for trigger here

       BEGIN TRAN;

     

       BEGIN        

           DECLARE curPol CURSOR LOCAL STATIC FOR

           SELECT DEX_ROW_ID

             FROM inserted;

 

           OPEN curPol;

 

           WHILE ( 1 = 1 ) BEGIN

           FETCH NEXT FROM curPol INTO @DEX_ROW_ID;

 

           IF ( @@FETCH_STATUS <> 0 )

                 BREAK;

 

                    SELECT

                    @CountEntries = i.CountEntries,

                    @CE_Orig = i.CE_Orig,

                    @ITEMNMBR = i.ITEMNMBR,

                    @CZ_CarKod = i.CZ_CarKod,

                    @ITEMDESC = i.ITEMDESC,

                    @LOCNCODE = i.LOCNCODE,

                    @skl_id = i.skl_id,

                    @QUANTITY = i.QUANTITY,

                    @DMJ = i.DMJ,

                    @DATEDONE = i.DATEDONE,

                    @IntegerValue = i.IntegerValue,

                    @TIMESPRT = i.TIMESPRT,

                    @CZ_SerNum_Track = i.CZ_SerNum_Track,

                    @CZ_SerNum_Find = i.CZ_SerNum_Find,

                    @TerminalID = i.TerminalID,

                    @O_TID = i.O_TID,

                    @REZ_1 = i.REZ_1,

                    @REZ_2 = i.REZ_2,

                    @ITEMCODE = i.ITEMCODE,

                    @CZ_REZ1_Track = i.CZ_REZ1_Track,

                    @CZ_REZ2_Track = i.CZ_REZ2_Track,

                    @CZ_Expirace_Track = i.CZ_Expirace_Track

                    FROM INSERTED i

                    WHERE i.DEX_ROW_ID = @DEX_ROW_ID;

 

                    EXECUTE dbo.CZMST_SE_insert

                    @CountEntries,

                    'Inventura',--@SOPNUMBE,

                    @ITEMNMBR,

                    'I',--@ITEMTYPE,

                    @ITEMDESC,

                    '',--@VNDDOCNM,

                    @CZ_CarKod,--@VNDITNUM,

                    1,--@ORD,

                    @CZ_CarKod,

                    @skl_id,

                    @LOCNCODE,

                    @DMJ,--@MJ,

                    @QUANTITY,--@QTYSHPPD,

                    0,--@QTYPACK,

                    0,--@CZ_DatVyr_Track,

                    0,--@CZ_DatVyr_Delka,

                    @CZ_SerNum_Track,

                    0,--@CZ_SerNum_Delka,

                    0,--@CZ_SW_Track,

                    0,--@CZ_SW_Delka,

                    0,--@CZ_Doslo,

                    '',--@Note,

                    '',--@TYPEPAL,

                    0,--@QTYPAL,

                    3,--@PRIORITY,

                    0,--@PRINTED,

                    0,--@USERID,

                    @DEX_ROW_ID,

                    @CZ_REZ1_Track,

                    @CZ_REZ2_Track,

                    @ITEMCODE,

                    NULL,--@WEIGHT,

                    @DATEDONE,--@Realization_Start,

                    NULL,--@Realization_Stop,

                    @CZ_Expirace_Track;

            

           END;

 

           CLOSE curPol;

           DEALLOCATE curPol;

 

    END;

 

       COMMIT TRAN;

 

END;








-- ========================================================================================================================================================
--
--
--												MaR 22.4.2024 Vytvorene trigry pro android
--
--
--																Konec
-- ========================================================================================================================================================
