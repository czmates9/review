USE [FASK_04.00]
GO

/****** Object:  Table [dbo].[CZ_UKOL_UZIV]    Script Date: 05/05/2014 16:56:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CZ_UKOL_UZIV](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UkolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[State] [nvarchar](1) NOT NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [int] NULL,
	[Note] [nvarchar](200) NULL,
	[DateNotify] [datetime] NULL,
	[DateFinished] [datetime] NULL,
 CONSTRAINT [PK_CZ_UKOL_UZIV] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identifikátor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identifikátor úkolu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'UkolID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID uživatele, kterému je úkol urèen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Stav úkolu uživatele (Vytvoøeno, Pøijato, Splnìno, Zrušeno ...)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'State'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datum zmìny' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'DateChanged'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID uživatele, který zmìnil stav úkolu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'UserIDChanged'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Poznámka uživatele' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'Note'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datum a èas upozornìní nastavené uživatelem' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'DateNotify'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datum a èas splnìní úkolu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CZ_UKOL_UZIV', @level2type=N'COLUMN',@level2name=N'DateFinished'
GO

ALTER TABLE [dbo].[CZ_UKOL_UZIV]  WITH CHECK ADD  CONSTRAINT [FK_CZ_UKOL_UZIV_CZ_UKOL] FOREIGN KEY([UkolID])
REFERENCES [dbo].[CZ_UKOL] ([ID])
GO

ALTER TABLE [dbo].[CZ_UKOL_UZIV] CHECK CONSTRAINT [FK_CZ_UKOL_UZIV_CZ_UKOL]
GO

ALTER TABLE [dbo].[CZ_UKOL_UZIV]  WITH CHECK ADD  CONSTRAINT [FK_CZ_UKOL_UZIV_CZMSTPWD] FOREIGN KEY([UserID])
REFERENCES [dbo].[CZMSTPWD] ([ID])
GO

ALTER TABLE [dbo].[CZ_UKOL_UZIV] CHECK CONSTRAINT [FK_CZ_UKOL_UZIV_CZMSTPWD]
GO

ALTER TABLE [dbo].[CZ_UKOL_UZIV] ADD  CONSTRAINT [DF_CZ_UKOL_UZIV_State]  DEFAULT (N'V') FOR [State]
GO


