/****** Object:  Table [dbo].[FASK_Events]    Script Date: 03/02/2010 10:36:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_Events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nchar](20) NOT NULL,
	[machineid] [nchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[description] [ntext] NULL,
	[barcodeReaded] [nchar](50) NOT NULL,
	[barcodeSended] [nchar](50) NOT NULL,
	[zakazka] [nchar](20) NULL,
	[popis] [nchar](10) NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[reportType] [nchar](1) NOT NULL,
	[isProcessed] [datetime] NULL,
 CONSTRAINT [PK_FASK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) ON [PRIMARY],
 CONSTRAINT [IX_FASK_Events_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


ALTER TABLE [dbo].[FASK_Events] ADD  CONSTRAINT [DF_FASK_Events_reportType]  DEFAULT (N'D') FOR [reportType]
GO



/****** Object:  Table [dbo].[FASK_Logins]    Script Date: 03/02/2010 10:38:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_Logins](
	[id] [nchar](20) NOT NULL,
	[firstname] [nvarchar](20) NOT NULL,
	[surname] [nchar](50) NOT NULL,
	[psswd] [nchar](10) NOT NULL,
 CONSTRAINT [PK_FASK_Logins] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO



/****** Object:  Table [dbo].[FASK_Machines]    Script Date: 03/02/2010 10:38:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_Machines](
	[id] [nchar](20) NOT NULL,
	[name] [nchar](10) NOT NULL,
	[description] [nchar](50) NOT NULL,
 CONSTRAINT [PK_FASK_Machines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO



/****** Object:  Table [dbo].[FASK_StatusTypes]    Script Date: 03/02/2010 10:38:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_StatusTypes](
	[statusid] [nchar](10) NOT NULL,
	[statusdesc] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO





/****** Object:  Table [dbo].[FASK_UserEvents]    Script Date: 03/02/2010 10:39:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nchar](20) NOT NULL,
	[machineid] [nchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[statusid] [nchar](10) NOT NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_FASK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) ON [PRIMARY],
 CONSTRAINT [IX_FASK_UserEvents_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO





/****** Object:  Trigger [dbo].[Replicator]    Script Date: 03/03/2010 09:50:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[Replicator] ON [dbo].[FASK_Events] FOR INSERT
AS
IF @@ROWCOUNT<>0

BEGIN
BEGIN TRAN

DECLARE @Zpracovano tinyint
DECLARE @Pracoviste int
DECLARE @Osadka int
DECLARE @Dilec char(14)
DECLARE @Mnozstvi smallint
DECLARE @Datumcas datetime
DECLARE @Udalost char(1)
DECLARE @rowGUID uniqueidentifier

DECLARE @QtyReal numeric(19,5)
SELECT @QtyReal = qtyReal FROM INSERTED

DECLARE @BarcodeSended nchar(50)
SELECT @BarcodeSended = barcodeSended FROM INSERTED

DECLARE @LoginID nchar(20)
SELECT @LoginID = loginid FROM INSERTED

DECLARE @MachineID nchar(20)
SELECT @MachineID = machineid FROM INSERTED

-- machineid bez predcislia 1440
IF (LEN(@MachineID)-4 > 0)
	SELECT @Pracoviste = CAST(SUBSTRING( @MachineID, 5, LEN(@MachineID)-4 ) AS INTEGER);
ELSE
	SELECT @Pracoviste = '';

	
-- loginid bez predcislia 120 
IF (LEN(@LoginID)-3 > 0)
	SELECT @Osadka = CAST(SUBSTRING( @LoginID, 4, LEN(@LoginID)-3 ) AS INTEGER)
ELSE
	SELECT @Osadka = '';
	
SELECT @Dilec = zakazka FROM INSERTED
SELECT @Mnozstvi = CAST(@QtyReal AS smallint)
SELECT @Datumcas = dateeve FROM INSERTED
SELECT @Udalost = reportType FROM INSERTED
SELECT @rowGUID = faskGuid FROM INSERTED

INSERT INTO [dbo].[NDP_FASK]
(Pracoviste,
Osadka,
Dilec,
Mnozstvi,
Datumcas,
Udalost,
rowGUID)
VALUES
(@Pracoviste,
@Osadka,
@Dilec,
@Mnozstvi,
@Datumcas,
@Udalost,
@rowGUID)

UPDATE FASK_Events
SET isProcessed = GETDATE()
FROM FASK_Events Y
JOIN Inserted I ON Y.ID = I.ID

COMMIT TRAN

END

GO






