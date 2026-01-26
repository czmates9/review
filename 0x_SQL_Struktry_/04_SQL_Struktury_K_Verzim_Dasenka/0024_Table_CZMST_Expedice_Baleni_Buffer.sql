/****** Object:  Table [dbo].[CZMST_Expedice_Baleni_Buffer]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Buffer](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/