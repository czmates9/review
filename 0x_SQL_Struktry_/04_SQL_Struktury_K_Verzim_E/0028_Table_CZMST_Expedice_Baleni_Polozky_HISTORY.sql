/****** Object:  Table [dbo].[CZMST_Expedice_Baleni_Polozky_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Polozky_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Polozky_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]
GO
/**************************************************************************************/