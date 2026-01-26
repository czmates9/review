/****** Object:  StoredProcedure [dbo].[FASK_GeneratorSN_Pro_CZPRO_VPP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Tadeas Divacky
-- Create date: 9.2.2023
-- Description:	Generator SN pro CZPRO_VPP
-- =============================================
CREATE PROCEDURE [dbo].[FASK_GeneratorSN_Pro_CZPRO_VPP]
	-- Add the parameters for the stored procedure here
	@CountEntries int,
    @SOPNUMBE nvarchar(30),
    @ITEMNMBR nvarchar(40),
    @ITEMDESC nvarchar(100),
    @ITEMMJ nvarchar(5),
    @VNDITNUM nvarchar(60),
    @QTYSHPPD numeric(19,5),
    @QTYPACK numeric(19,5),
    @TIMEMODE int,
    @TIMEPREP real,
	@TIMEUNIT real,
    @SerNumT tinyint,
    @BarcodeT tinyint,
	@OD int ,
	@Pocet int,
		@N int ,
	@prefix nvarchar(50),
	@CZ_Rez1_Track tinyint,
	@CZ_Rez2_Track tinyint,
	@CZ_Rez3_Track tinyint,
	@CZ_Rez4_Track tinyint,
	@CZ_Rez5_Track tinyint,
	@WEIGHT_TARA numeric(19,5),
	@WEIGHT_NETTO numeric(19,5),
	@WEIGHT_TOL_PLUS numeric(19,5),
	@WEIGHT_TOL_MINUS numeric(19,5)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    -- Insert statements for procedure here
INSERT INTO [dbo].[CZPRO_VPP]
           ([CountEntries]
           ,[SOPNUMBE]
           ,[ITEMNMBR]
           ,[ITEMTYPE]
           ,[ITEMDESC]
           ,[ITEMMJ]
           ,[VNDDOCNMP]
           ,[VNDITNUM]
           ,[ORD]
           ,[BarcodeP]
           ,[LOCNCODE]
           ,[QTYSHPPD]
           ,[QTYDOKON]
           ,[QTYPACK]
           ,[QTYPACKMJ]
           ,[TIMEMODE]
           ,[TIMEPREP]
           ,[TIMEUNIT]
           ,[DtProdT]
           ,[DtProdL]
           ,[SerNumT]
           ,[SerNumL]
           ,[VerT]
           ,[VerL]
           ,[TermID]
           ,[LSTMod]
           ,[Realization_Start]
           ,[Realization_Stop]
           ,[BarcodeT]
	   ,[CZ_REZ1_Track]
	   ,[CZ_REZ2_Track]
	   ,[CZ_REZ3_Track]
	   ,[CZ_REZ4_Track]
	   ,[CZ_REZ5_Track]
	   ,[WEIGHT_TARA]
	   ,[WEIGHT_NETTO]
	   ,[WEIGHT_TOL_PLUS]
	   ,[WEIGHT_TOL_MINUS]
)
		   SELECT 
		   @CountEntries as CountEntries
      , @SOPNUMBE as SOPNUMBE
      ,@ITEMNMBR as ITEMNMBR
      ,'' as ITEMTYPE
      ,@ITEMDESC as ITEMDESC
      ,@ITEMMJ as ITEMMJ
      ,'' as VNDDOCNMP
      ,@VNDITNUM as VNDITNUM
      ,'1' as ORD
      ,[value] as BarcodeP
      ,'' as LOCNCODE
      ,1 as QTYSHPPD
      ,0 as QTYDOKON
      ,@QTYPACK as QTYPACK
      ,'' as QTYPACKMJ
      ,@TIMEMODE as TIMEMODE
      ,@TIMEPREP as TIMEPREP
      ,@TIMEUNIT as TIMEUNIT
      ,0 as DtProdT
      ,0 as DtProdL
      ,@SerNumT as SerNumT
      ,0 as SerNumL
      ,0 as VerT
      ,0 as VerL
      ,0 as TermID
      ,GETDATE() as LSTMod
      ,null as Realization_Start
      ,null as Realization_Stop
      ,@BarcodeT as BarcodeT
      ,@CZ_Rez1_Track as CZ_REZ1_Track
      ,@CZ_Rez2_Track as CZ_REZ2_Track
      ,@CZ_Rez3_Track as CZ_REZ3_Track
      ,@CZ_Rez4_Track as CZ_REZ4_Track
      ,@CZ_Rez5_Track as CZ_REZ5_Track
      ,@WEIGHT_TARA as [WEIGHT_TARA]
      ,@WEIGHT_NETTO as [WEIGHT_NETTO]
      ,@WEIGHT_TOL_PLUS as [WEIGHT_TOL_PLUS]
      ,@WEIGHT_TOL_MINUS as [WEIGHT_TOL_MINUS]
 FROM [dbo].[FASK_GeneratorRad] ( @OD ,@Pocet ,@N ,@prefix)

END



GO

/**************************************************************************************/