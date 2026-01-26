/****** Object:  Table [dbo].[FASK_FORMULARE]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_FORMULARE](
	[id] [int] IDENTITY(1,1) NOT NULL,
    [nazev_okna] [nvarchar](MAX) NOT NULL,
	[nazev] [nvarchar](MAX) NULL,
	[ord] [int] NULL,
	[typ] [nvarchar](2) NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[formular] [nvarchar](MAX) NULL,
 CONSTRAINT [PK_FASK_FORMULARE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

/**************************************************************************************/