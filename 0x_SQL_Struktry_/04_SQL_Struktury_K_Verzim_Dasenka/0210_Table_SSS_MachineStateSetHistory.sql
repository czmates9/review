/****** Object:  Table [dbo].[MachineStateSetHistory]    ******/

CREATE TABLE [MachineStateSetHistory](
	[IP] [nvarchar](20) NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[S0] [int] NULL,
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
	[counter_0] [int] NULL,
	[counter_1] [int] NULL,
	[counter_2] [int] NULL,
	[counter_3] [int] NULL,
	[counter_4] [int] NULL,
	[counter_5] [int] NULL,
	[counter_6] [int] NULL,
	[counter_7] [int] NULL,
	[counter_8] [int] NULL,
	[counter_9] [int] NULL,
	[counter_10] [int] NULL,
	[counter_11] [int] NULL,
	[LastError] [nvarchar] (50) NULL,
	[ID_group] int NOT NULL default(1)
) ON [PRIMARY]
GO

/**************************************************************************************/