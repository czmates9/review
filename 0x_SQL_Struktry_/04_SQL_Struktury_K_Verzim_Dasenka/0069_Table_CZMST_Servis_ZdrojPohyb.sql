/****** Object:  Table [dbo].[CZMST_Servis_ZdrojPohyb]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Servis_ZdrojPohyb](
	[IDZdroj] [nvarchar](20) NOT NULL,
	[IDStav] [nvarchar](20) NOT NULL,
	[IDCinnost] [nvarchar](20) NULL,
	[Modified] [datetime] NOT NULL,
	[IDTerminal] [int] NOT NULL,
	[IDUser] [int] NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[CinnostValue] [nvarchar](50) NULL,
	[CinnostType] [nvarchar](2) NULL,
	[CinnostOznaceni] [nvarchar](50) NULL,
	[CountEntries] [int] NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NULL,
	[GPS_X] [float] NULL,
	[GPS_Y] [float] NULL,
	[GPS_Z] [int] NULL,
	[dateeveS] [datetime] NULL,
	[dateExported] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

CREATE NONCLUSTERED INDEX [IX_FASK_CZMST_Servis_ZdrojPohyb_Guid] ON [dbo].[CZMST_Servis_ZdrojPohyb]
(
	[GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


ALTER TABLE [dbo].[CZMST_Servis_ZdrojPohyb] ADD  DEFAULT (getdate()) FOR [dateeveS]
GO
/**************************************************************************************/