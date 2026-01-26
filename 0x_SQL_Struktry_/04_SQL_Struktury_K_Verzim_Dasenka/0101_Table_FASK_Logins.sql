/****** Object:  Table [dbo].[FASK_Logins]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Logins](
	[USERID] [nvarchar](20) NOT NULL,
	[firstname] [nvarchar](20) NOT NULL,
	[surname] [nvarchar](50) NOT NULL,
	[psswd] [nvarchar](50) NOT NULL,
	[CREATED] [datetime] NULL DEFAULT (getdate()),
	[VALIDFROM] [datetime] NULL DEFAULT (getdate()),
	[VALIDTO] [datetime] NULL,
	[RFID] [nvarchar](20) NULL,
 CONSTRAINT [PK_FASK_Logins] PRIMARY KEY CLUSTERED 
(
	[USERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/