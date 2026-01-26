/****** Object:  Table [dbo].[CZMST_TASK_VERIFY]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CZMST_TASK_VERIFY](
	[taskname] [nvarchar](50) NOT NULL,
	[verify_pwd] [nvarchar](50) NOT NULL,
	[taskdesc] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/**************************************************************************************/
