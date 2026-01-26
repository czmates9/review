/****** Object:  Table [dbo].[CZPRO_VPP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZPRO_VPP](
	[CountEntries] [int] NOT NULL DEFAULT ('1'),
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMMJ] [nvarchar](5) NULL,
	[VNDDOCNMP] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[ORD] [int] NOT NULL DEFAULT ((0)),
	[BarcodeP] [nvarchar](31) NOT NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYDOKON] [numeric](19, 5) NOT NULL DEFAULT ((0)),
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[QTYPACKMJ] [nvarchar](5) NULL,
	[TIMEMODE] [int] NOT NULL DEFAULT ((0)),
	[TIMEPREP] [real] NOT NULL,
	[TIMEUNIT] [real] NOT NULL,
	[DtProdT] [tinyint] NOT NULL,
	[DtProdL] [smallint] NOT NULL,
	[SerNumT] [tinyint] NOT NULL,
	[SerNumL] [smallint] NOT NULL,
	[VerT] [tinyint] NOT NULL,
	[VerL] [smallint] NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[LSTMod] [datetime] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL
 CONSTRAINT [PK_CZPRO_VPP] PRIMARY KEY CLUSTERED 
(
	[SOPNUMBE] ASC,
	[ITEMNMBR] ASC,
	[CountEntries] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/