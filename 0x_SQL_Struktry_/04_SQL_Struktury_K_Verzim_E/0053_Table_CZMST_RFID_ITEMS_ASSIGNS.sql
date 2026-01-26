/****** Object:  Table [dbo].[CZMST_RFID_ITEMS_ASSIGNS]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_RFID_ITEMS_ASSIGNS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SEQUENCENMBR] [int] NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NULL,
	[DOCUMENTNMBR] [nvarchar](20) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[M_ID] [nvarchar](100) NULL,
	[M_TID] [nvarchar](100) NULL,
	[M_EPC] [nvarchar](100) NULL,
	[M_USER] [nvarchar](150) NULL,
	[M_RESERVED] [nvarchar](100) NULL,
	[O_M_ID] [nvarchar](100) NULL,
	[O_M_TID] [nvarchar](100) NULL,
	[O_M_EPC] [nvarchar](100) NULL,
	[O_M_USER] [nvarchar](150) NULL,
	[O_M_RESERVED] [nvarchar](100) NULL,
	[TerminalID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Created_T] [datetime] NOT NULL,
	[Created_S] [datetime] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_RFID_ITEMS_ASSIGNS] ADD  DEFAULT (getdate()) FOR [Created_S]
GO
/**************************************************************************************/