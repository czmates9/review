/****** Object:  Table [dbo].[FASK_Operations]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FASK_Operations](
	[machinetype] [nvarchar](10) NOT NULL,
	[IDO] [nvarchar](10) NOT NULL,
	[NAZEV] [nvarchar](50) NOT NULL,
	[CK] [nvarchar](30) NOT NULL,
	[SCAN1] [tinyint] NULL,
	[SCAN2] [tinyint] NULL,
	[SCAN3] [tinyint] NULL,
	[SCANZAKAZKA] [tinyint] NULL,
	[SENSOR] [tinyint] NULL,
	[VOLNA] [bit] NULL,
	[START] [bit] NULL,
	[KONEC] [bit] NULL,
	[SPHLAVICKA] [nvarchar](50) NULL,
	[SPINFO] [nvarchar](50) NULL,
	[SPZAKAZKA] [nvarchar](50) NULL,
	[KONTROLAMAT] [bit] NULL,
	[SPMATERIAL] [nvarchar](50) NULL,
	[LOGIN] [bit] NULL,
 CONSTRAINT [PK_FASK_Operations] PRIMARY KEY CLUSTERED 
(
	[machinetype] ASC,
	[IDO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [FASK_Operations] ADD  CONSTRAINT [DF_FASK_Operations_VOLNA]  DEFAULT ((0)) FOR [VOLNA]
GO

ALTER TABLE [FASK_Operations] ADD  CONSTRAINT [DF_FASK_Operations_START]  DEFAULT ((0)) FOR [START]
GO

ALTER TABLE [FASK_Operations] ADD  CONSTRAINT [DF_FASK_Operations_END]  DEFAULT ((0)) FOR [KONEC]
GO

/**************************************************************************************/