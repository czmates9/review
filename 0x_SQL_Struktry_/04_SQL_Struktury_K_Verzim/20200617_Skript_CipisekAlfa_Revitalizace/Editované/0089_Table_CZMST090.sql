/****** Object:  Table [dbo].[CZMST090]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST090](
	[odb_id] [nvarchar](12) NOT NULL,
	[odb_desc] [nvarchar](31) NULL,
	[odb_typ] [nvarchar](3) NULL,
	[odb_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[odb_ico] [nvarchar](20) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[odb_misto] [nvarchar](100) NULL DEFAULT (''),
	[odb_ulice] [nvarchar](100) NULL DEFAULT (''),
	[odb_cisloOr] [nvarchar](15) NULL DEFAULT (''),
	[odb_psc] [nvarchar](15) NULL DEFAULT (''),
	[odb_dic] [nvarchar](15) NULL DEFAULT (''),
	[odb_Odberatel] [bit] NULL DEFAULT ((0)),
	[odb_Dodavatel] [bit] NULL DEFAULT ((0))
) ON [PRIMARY]

GO
/**************************************************************************************/