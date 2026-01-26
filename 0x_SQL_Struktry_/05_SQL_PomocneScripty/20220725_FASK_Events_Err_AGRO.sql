/*---script for alter table FaskEvents_Err---*/
/*
---- 25.7. 2022 by Matou Rathouzsky
*/
---------------------------------------------
--definuj DB nad kterou se to ma provadet!!
USE [Agro_fask]
GO
---------------------------------------------


ALTER TABLE [dbo].[FASK_EventsErr]
	add [loginid] [nvarchar](20) NOT NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [machineid] [nvarchar](20) NOT NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [dateeve] [datetime] NOT NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [qty] [numeric](19, 5) NULL default(0);
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [qtyReal] [numeric](19, 5) NULL default(0);
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [description] [nvarchar](max) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [barcodeReaded] [nvarchar](50) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [barcodeSended] [nvarchar](50) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [zakazka] [nvarchar](20) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [popis] [nvarchar](10) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [faskGUID] [uniqueidentifier] NOT NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [reportType] [nvarchar](1) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [isProcessed] [datetime] NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [IDO] [nvarchar](10) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [scan1] [nvarchar](255) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [scan2] [nvarchar](255) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [scan3] [nvarchar](255) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [sensor] [nvarchar](50) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [material] [nvarchar](255) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [productionGuid] [uniqueidentifier] NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [VPH] [nvarchar](30) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [VPPol] [int] NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [EAN_IS] [nvarchar](31) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [IS_ID] [nvarchar](40) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [NMBRPAL] [nvarchar](50) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [status] [int] NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [QTYPACK] [numeric](19, 5) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [PackType] [nvarchar](50) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [WEIGHT] [numeric](19, 5) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [BarcodeT] [tinyint] NULL default(0);
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [REZ_1] [nvarchar](100) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [REZ_2] [nvarchar](100) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [REZ_3] [nvarchar](100) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [REZ_4] [nvarchar](100) NULL;
	go
	ALTER TABLE [dbo].[FASK_EventsErr]
	add [REZ_5] [nvarchar](100) NULL;
	go
