/****** Object:  Table [dbo].[FASK_Vyroba_PVP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Vyroba_PVP](
	[OBJ_NMBR] [nvarchar](17) NULL,
	[OBJ_DESC] [nvarchar](100) NULL,
	[OBJ_TYPE] [nvarchar](11) NULL,
	[OBJ_COMPANY] [nvarchar](255) NULL,
	[OBJ_DATE_FROM] [datetime] NULL,
	[OBJ_DATE_TO] [datetime] NULL,
	[OBJ_ORD] [int] NULL,
	[OBJ_ITEM_ORD] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DATE_ZAPLANOVANI] [datetime] NULL,
	[VP_PRPS] [bit] NOT NULL DEFAULT ((0)),
	[VP_PRPS_QTY] [numeric](19, 5) NULL,
	[VP_PRPS_SOPNUMBE] [nvarchar](100) NULL,
	[VP_PRDCT_QTY] [numeric](19, 5) NULL,
	[USERID] [int] NULL,
	[Ref_PVH] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/