
-- =============================================
-- CO-author:	 Ing. Rathouzsky Matous
-- Modification: 7.4. 2025
-- Description:  verze zakládacího scriptu SSS pro databázi Fask s.r.o
-- =============================================


---------------------------------------------
----Zde odkomentuj a zadej název databáze----

--USE [nazev_databaze]
--GO

---------------------------------------------


/**************************************************************************************/
/****** Object:  Table [dbo].[MachinesDefinition]    ******/

CREATE TABLE [MachinesDefinition](
	[IP] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[MType] [nvarchar](50) NOT NULL,
	[PORT] int NULL,
	[ID_group] int NOT NULL default(1),
 CONSTRAINT [PK_MachinesDefinition] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/**************************************************************************************/
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
/****** Object:  Table [dbo].[MachineStateSet]    ******/

CREATE TABLE [MachineStateSet](
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
	[ID_group] int NOT NULL default(1),

 CONSTRAINT [PK_MachineStateSet] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/**************************************************************************************/
/****** Object:  Table [dbo].[fask_trg_machinestatesethistory]    ******/

-- =============================================
-- Author:		Ing. Jiøí Skøivánek
-- Create date: 20.5.2014
-- Description:	Trigger pro ukládání historie stavu strojù
-- =============================================
CREATE TRIGGER [dbo].[fask_trg_machinestatesethistory] 
   ON  [dbo].[MachineStateSet] 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--inserted
	IF EXISTS(SELECT * FROM inserted)
	BEGIN
		INSERT INTO MachineStateSetHistory 
			SELECT * FROM inserted
	END        

END

GO

CREATE TABLE [dbo].[MachinesDefinitionMeasurement](
	[IP] [nvarchar](20) NOT NULL,
	[IP_M] [nvarchar](20) NOT NULL,
	[Description_M] [nvarchar](100) NULL,
	[MType_M] [nvarchar](50) NOT NULL,
	[PORT_M] [int] NULL,
	[ID_group_M] [int] NULL
) ON [PRIMARY]
GO