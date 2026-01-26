/****** Object:  Table [dbo].[CZMST_I1P]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_I1P](
	[Countentries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[TAX] [numeric](19, 5) NULL,
	[PRICE0] [numeric](19, 5) NULL,
	[PRICE1] [numeric](19, 5) NULL,
	[PRICE2] [numeric](19, 5) NULL,
	[PRICE3] [numeric](19, 5) NULL,
	[PRICE4] [numeric](19, 5) NULL,
	[PRICE5] [numeric](19, 5) NULL,
	[PRICE0H] [nvarchar](50) NULL,
	[PRICE1H] [nvarchar](50) NULL,
	[PRICE2H] [nvarchar](50) NULL,
	[PRICE3H] [nvarchar](50) NULL,
	[PRICE4H] [nvarchar](50) NULL,
	[PRICE5H] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/**************************************************************************************/