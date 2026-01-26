/****** Object:  Table [dbo].[FASK_MachineType]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FASK_MachineType](
	[machinetype] [nvarchar](10) NOT NULL,
	[machinetypename] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FASK_MachineType] PRIMARY KEY CLUSTERED 
(
	[machinetype] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/**************************************************************************************/