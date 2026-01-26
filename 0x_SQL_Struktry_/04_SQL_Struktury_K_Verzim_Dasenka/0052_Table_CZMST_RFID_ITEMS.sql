/****** Object:  Table [dbo].[CZMST_RFID_ITEMS]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_RFID_ITEMS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[SEQUENCENMBR] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_RFID_ITEMS] ADD  DEFAULT ((0)) FOR [SEQUENCENMBR]
GO
/**************************************************************************************/