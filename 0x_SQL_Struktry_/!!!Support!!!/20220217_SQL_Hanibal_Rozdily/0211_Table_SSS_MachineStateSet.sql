/****** Object:  Table [dbo].[MachineStateSet]    ******/

CREATE TABLE [MachineStateSet](
	[IP] [nvarchar](20) NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[S1] [int] NULL,
	[S2] [int] NULL,
	[S3] [int] NULL,
	[S4] [int] NULL,
	[S5] [int] NULL,
	[S6] [int] NULL,
	[S7] [int] NULL,
	[S8] [int] NULL,
	[S9] [int] NULL,
	[S10] [int] NULL,
	[S11] [int] NULL,
	[S12] [int] NULL,
	[LastError] [nvarchar] (50) NULL
 CONSTRAINT [PK_MachineStateSet] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/**************************************************************************************/