/****** Object:  Table [dbo].[FASK_Logins_Auth]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FASK_Logins_Auth](
	[USERID] [nvarchar](20) NOT NULL,
	[AGENDAID] [nvarchar](50) NULL,
	[AUTH] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/