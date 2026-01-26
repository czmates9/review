/****** Object:  Table [dbo].[MachineStateSetHistory]    ******/

CREATE TABLE [MachineStateSetHistory](
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
) ON [PRIMARY]
GO

/**************************************************************************************/