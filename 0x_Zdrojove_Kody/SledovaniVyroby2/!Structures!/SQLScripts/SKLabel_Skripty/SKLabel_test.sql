INSERT INTO [SKLabelCentral].[dbo].[FASK_Events]
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
           ,[sensor])
     VALUES
           ('loginid'
           ,'machineid'
           ,GETDATE() --'dateeve'
           ,10 --'qty'
           ,10 --'qtyReal'
           ,'description'
           ,'barcodeReaded'
           ,'barcodeSended'
           ,'zakazka'
           ,'popis'
           ,NEWID() --'faskGUID'
           ,'O' --'reportType'
           ,NULL --'isProcessed'
           ,'IDO'
           ,'scan1'
           ,'scan2'
           ,'scan3'
           ,'sensor')
GO

SELECT top 10 *
  FROM [SKLabelCentral].[dbo].[FASK_Events] 
  order by dateeve desc
GO

SELECT *
  FROM [SKLabel_Cil].[dbo].[ANET_K2]
  order by id_zaznamu desc
GO


