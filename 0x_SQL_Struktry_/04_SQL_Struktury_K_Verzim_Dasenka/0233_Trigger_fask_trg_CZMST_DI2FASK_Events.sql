/****** Object:  Trigger [dbo].[fask_trg_CZMST_DI2FASK_Events]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 2.3.2023
-- Description:	Trigger medzi DI a FASK_Events
-- =============================================
CREATE TRIGGER [dbo].[fask_trg_CZMST_DI2FASK_Events] 
   ON  [dbo].[CZMST_DI] 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[FASK_Events]
           ([loginid]
           ,[machineid]
           ,[dateeve]
           ,[qty]
           ,[qtyReal]
           ,[description]
           ,[barcodeReaded]
           ,[barcodeSended]
           ,[zakazka]
           ,[popis]
           ,[faskGUID]
           ,[reportType]
           ,[isProcessed]
           ,[IDO]
           ,[scan1]
           ,[scan2]
           ,[scan3]
           ,[sensor]
           ,[material]
           ,[productionGuid]
           ,[VPH]
           ,[VPPol]
           ,[EAN_IS]
           ,[IS_ID]
           ,[NMBRPAL]
           ,[status]
           ,[QTYPACK]
           ,[PackType]
           ,[WEIGHT]
           ,[BarcodeT]
           ,[REZ_1]
           ,[REZ_2]
           ,[REZ_3]
           ,[REZ_4]
           ,[REZ_5])
			select 
			DI.USER_ID as loginid 
           ,'' as machineid
           ,GETDATE() as dateeve
           ,DI.QTYSHPPD as qty
           ,0 as qtyReal
           ,'' as description
           ,DI.VNDITNUM as barcodeReaded
           ,DI.VNDITNUM as barcodeSended
           ,'' as zakazka
           ,'' as popis
           ,DI.GUID as faskGUID
           ,'' as reportType
           ,null as isProcessed
           ,'' as IDO
           ,'' as scan1
           ,'' as scan2
           ,'' as scan3
           ,'' as sensor
           ,'' as material
           ,null as productionGuid
           ,'' as VPH
           ,'' as VPPol
           ,DI.VNDITNUM as EAN_IS
           ,DI.ITEMNMBR as IS_ID
           ,'' as NMBRPAL
           ,null as status
           ,DI.QTYPACK as QTYPACK
           ,'' as PackType
           ,null as WEIGHT
           ,0 as BarcodeT
           ,'' as REZ_1
           ,'' as REZ_2
           ,'' as REZ_3
           ,'' as REZ_4
           ,'' as REZ_5
		   from inserted as DI
END

GO

/**************************************************************************************/