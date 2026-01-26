/****** Object:  Table [dbo].[CZMST_SkladLokace_Stav]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_Stav](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[QTYSHPPD_DEF] [numeric](19, 5) NOT NULL DEFAULT ((0)),
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[DATECHANGE] [datetime] NOT NULL,
	[EXPIRATION] [datetime] NULL,
	[QTYSHPPD_DEF_DATE] [datetime] NULL,
	[QTY_OWNER] [numeric](19, 5) NOT NULL DEFAULT ((0)),
	[PRAC_ID_OWNER] [nvarchar](30) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/