/****** Object:  Table [dbo].[MachinesDefinition]    ******/

CREATE TABLE [MachinesDefinition](
	[IP] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[MType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MachinesDefinition] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/**************************************************************************************/