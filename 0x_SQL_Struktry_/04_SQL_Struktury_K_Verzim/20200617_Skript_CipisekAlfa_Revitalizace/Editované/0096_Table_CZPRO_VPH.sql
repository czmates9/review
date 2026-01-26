/****** Object:  Table [dbo].[CZPRO_VPH]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZPRO_VPH](
	[CountEntries] [int] NOT NULL DEFAULT ('1'),
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[SOPTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[SOPDESC] [nvarchar](100) NULL,
	[VNDDOCNMH] [nvarchar](21) NULL,
	[BarcodeH] [nvarchar](31) NOT NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[DateProd] [smallint] NOT NULL,
	[Rez1] [nvarchar](50) NOT NULL,
	[Rez2] [nvarchar](50) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[LSTMod] [datetime] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [tinyint] NOT NULL DEFAULT ((1)),
 CONSTRAINT [PK_CZPRO_VPH] PRIMARY KEY CLUSTERED 
(
	[SOPNUMBE] ASC,
	[CountEntries] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/