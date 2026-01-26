/****** Object:  Table [dbo].[FASK_Machines]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Machines](
	[id] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_FASK_Machines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/