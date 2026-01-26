/****** Object:  Table [dbo].[FASK_Vyroba_TP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Vyroba_TP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_H] [nvarchar](50) NULL,
	[ID_L] [nvarchar](50) NULL,
	[ITEMNMBR_Def] [nvarchar](31) NULL,
	[DESC_Def] [nvarchar](100) NULL,
	[MJ_Def] [nvarchar](50) NULL,
	[ITEMNMBR_fol] [nvarchar](31) NULL,
	[DESC_Fol] [nvarchar](100) NULL,
	[MJ_Fol] [nvarchar](50) NULL,
	[koef] [nvarchar](50) NULL,
	[ID_USER] [nvarchar](50) NULL,
	[dateedit] [datetime] NULL,
	[alter] [nvarchar](1) NULL,
	[PUO] [nvarchar](1) NULL,
 CONSTRAINT [PK_FASK_Vyroba_TP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/