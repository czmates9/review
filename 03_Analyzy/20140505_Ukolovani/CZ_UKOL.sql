USE [FASK_04.00]
GO

/****** Object:  Table [dbo].[CZ_UKOL]    Script Date: 05/05/2014 16:56:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CZ_UKOL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [ntext] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[CreatorID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[State] [nvarchar](1) NOT NULL,
	[Kind] [nvarchar](1) NULL,
	[Type] [nvarchar](1) NULL,
	[Priority] [int] NOT NULL,
	[PartnerID] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZ_UKOL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identifikátor úkolu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Struèný název úkolu pro pøehled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Znìní úkolu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kód úkolu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID uživatele, který úkol vytvoøil ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datum a èas vytvoøení úkolu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'DateCreated'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datum od kdy úkol nabývá platnosti' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'DateFrom'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datum kdy úkol pozbývá platnosti' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'DateTo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Stav úkolu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'State'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Druh informace' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'Kind'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Typ informace' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Priorita (0-nejvyšší)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'Priority'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identifikátor partnera, na nìhož je úkol vázán. Lze dotáhnout touto vazbou informace o partnerovi...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL', @level2type=N'COLUMN',@level2name=N'PartnerID'
GO

ALTER TABLE [dbo].[CZ_UKOL] ADD  CONSTRAINT [DF_CZ_UKOL_Popis]  DEFAULT (N'Nezadáno') FOR [Description]
GO

ALTER TABLE [dbo].[CZ_UKOL] ADD  CONSTRAINT [DF_CZ_UKOL_Created]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[CZ_UKOL] ADD  CONSTRAINT [DF_CZ_UKOL_State]  DEFAULT (N'N') FOR [State]
GO

ALTER TABLE [dbo].[CZ_UKOL] ADD  CONSTRAINT [DF_CZ_UKOL_Priority]  DEFAULT ((3)) FOR [Priority]
GO


