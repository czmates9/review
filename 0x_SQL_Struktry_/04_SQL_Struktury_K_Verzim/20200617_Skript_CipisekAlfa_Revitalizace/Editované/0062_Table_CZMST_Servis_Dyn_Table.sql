/****** Object:  Table [dbo].[CZMST_Servis_Dyn_Table]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Dyn_Table](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZMST_Servis_Dyn_Table] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/