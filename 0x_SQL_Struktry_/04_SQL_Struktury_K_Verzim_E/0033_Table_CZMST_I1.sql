/****** Object:  Table [dbo].[CZMST_I1]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I1](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[ITEMDESC] [nvarchar](100) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[QUANTITY] [numeric](19, 5) NOT NULL,
	[DMJ] [nvarchar](200) NULL,
	[DATEDONE] [datetime] NOT NULL,
	[IntegerValue] [smallint] NOT NULL,
	[TIMESPRT] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Find] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[TerminalID] [tinyint] NOT NULL,
	[O_TID] [tinyint] NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_REZ2_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_Expirace_Track] [tinyint] NOT NULL DEFAULT((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/