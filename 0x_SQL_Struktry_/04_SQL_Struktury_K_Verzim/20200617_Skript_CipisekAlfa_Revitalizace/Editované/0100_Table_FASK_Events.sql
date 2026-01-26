/****** Object:  Table [dbo].[FASK_Events]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[description] [nvarchar](max) NULL,
	[barcodeReaded] [nvarchar](50) NOT NULL,
	[barcodeSended] [nvarchar](50) NOT NULL,
	[zakazka] [nvarchar](20) NULL,
	[popis] [nvarchar](10) NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[reportType] [nvarchar](1) NOT NULL,
	[isProcessed] [datetime] NULL,
	[IDO] [nvarchar](10) NULL,
	[scan1] [nvarchar](255) NULL,
	[scan2] [nvarchar](255) NULL,
	[scan3] [nvarchar](255) NULL,
	[sensor] [nvarchar](50) NULL,
	[material] [nvarchar](255) NULL,
	[productionGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_FASK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Events_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/**************************************************************************************/