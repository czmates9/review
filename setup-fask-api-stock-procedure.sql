USE [FASK];
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE [dbo].[fask_mnozstvinasklade2]
    @itemnmbr nvarchar(40),
    @location nvarchar(11)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CONVERT(float, SUM([QTY]))
    FROM [dbo].[FASK_ZASOBY]
    WHERE [ITEMNMBR] = @itemnmbr
      AND [LOCNCODE] = @location;
END;
GO

