/****** Object:  Table [dbo].[FASK_ZASOBY_IMPORT_POHODA_SKzNC]     ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_ZASOBY_IMPORT_POHODA_SKzNC](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[DefDod] [bit] NOT NULL,
	[RefAg] [int] NULL,
	[IDS_SKz] [nvarchar](255) NULL,
	[ID_sSklad] [int] NULL,
	[RefAD] [int] NULL,
	[Firma] [nvarchar](255) NULL,
	[NakupC] [numeric](19, 5) NULL,
	[RefCM] [int] NULL,
	[CmKurs] [numeric](19, 5) NULL,
	[EAN] [nvarchar](20) NULL,
	[MJEAN] [varchar](10) NULL,
	[MJkoefEAN] [numeric](19, 5) NULL,
	[Pozn] [nvarchar](255) NULL,
	[Status_Err] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FASK_ZASOBY_IMPORT_POHODA_SKzNC] ADD  CONSTRAINT [DF_FASK_ZASOBY_IMPORT_POHODA_SKzNC_DefDod]  DEFAULT ((0)) FOR [DefDod]
GO

ALTER TABLE [dbo].[FASK_ZASOBY_IMPORT_POHODA_SKzNC] ADD  CONSTRAINT [DF_FASK_ZASOBY_IMPORT_POHODA_SKzNC_Status_Err]  DEFAULT ((0)) FOR [Status_Err]
GO



/**************************************************************************************/
