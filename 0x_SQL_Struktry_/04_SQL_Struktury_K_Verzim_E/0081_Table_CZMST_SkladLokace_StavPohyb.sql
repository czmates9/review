/****** Object:  Table [dbo].[CZMST_SkladLokace_StavPohyb]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_StavPohyb](
	[id] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[DOCUMENT_NUMBER] [nvarchar](30) NULL,
	[POHYB_TYPE] [nvarchar](2) NULL,
	[POHYB_SRC] [nvarchar](2) NULL,
	[SOURCE] [nvarchar](2) NULL,
	[CountEntries] [int] NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[SKL_ID_SRC] [nvarchar](20) NULL,
	[SKL_ID_DST] [nvarchar](20) NULL,
	[LOCNCODE_SRC] [nvarchar](11) NULL,
	[LOCNCODE_DST] [nvarchar](11) NULL,
	[UserID] [int] NOT NULL,
	[TermID] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[dateeveS] [datetime] NOT NULL,
	[dateeveT] [datetime] NOT NULL,
 CONSTRAINT [PK_CZMST_SkladLokace_StavPohyb] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

CREATE NONCLUSTERED INDEX [IX_FASK_CZMST_SkladLokace_StavPohyb_Guid] ON [dbo].[CZMST_SkladLokace_StavPohyb]
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/**************************************************************************************/