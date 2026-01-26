/****** Object:  Table [dbo].[CZ_UKOL] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[CreatorID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[State] [nvarchar](1) NOT NULL,
	[Kind] [nvarchar](1) NULL,
	[Type] [nvarchar](2) NULL,
	[Priority] [int] NOT NULL,
	[PartnerID] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZ_UKOL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (N'Nezadáno') FOR [Description]
GO
ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (N'N') FOR [State]
GO
ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT ((3)) FOR [Priority]
GO
/**************************************************************************************/