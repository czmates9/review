/****** Object:  Table [dbo].[CZMST_Servis_ZdrojStav]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Servis_ZdrojStav](
	[IDZdroj] [nvarchar](20) NOT NULL,
	[IDStav] [nvarchar](20) NOT NULL,
	[IDCinnost] [nvarchar](20) NULL,
	[Modified] [datetime] NOT NULL,
	[IDTerminal] [int] NULL,
	[IDUser] [int] NULL,
	[GUID] [uniqueidentifier] NULL,
	[CinnostValue] [nvarchar](50) NULL,
	[CinnostType] [nvarchar](2) NULL,
	[CinnostOznaceni] [nvarchar](50) NULL,
	[CountEntries] [int] NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NULL,
	[GPS_X] [float] NULL,
	[GPS_Y] [float] NULL,
	[GPS_Z] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/