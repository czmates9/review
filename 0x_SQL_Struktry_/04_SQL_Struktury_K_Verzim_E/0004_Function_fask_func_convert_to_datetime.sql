/****** Object:  UserDefinedFunction [dbo].[fask_func_convert_to_datetime]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fask_func_convert_to_datetime](@timedone nvarchar(6), @datedone nvarchar(8))
RETURNS DATETIME-- NVARCHAR(17)
AS 
BEGIN
	IF @timedone is null
	BEGIN
		return null;
	END	
	
	IF @datedone is null
	BEGIN
		return null;
	END
	
	if LEN(@timedone) < 6
	BEGIN
		return null;
	END
	
	if LEN(@datedone) < 8
	BEGIN
		return null;
	END
	/*
	DECLARE @dat varchar(14) = '20151003092957';
	declare @year varchar(4) = substring(@dat, 1, 4);
	declare @month varchar(2) = substring(@dat, 5, 2);
	declare @day varchar(2) = substring(@dat, 7, 2);
	declare @hour varchar(2) = substring(@dat, 9, 2);
	declare @min varchar(2) = substring(@dat, 11, 2);
	declare @sec varchar(2) = substring(@dat, 13, 2);
	*/
	--DECLARE @dat varchar(14) = '20151003092957';
	declare @year nvarchar(4) = substring(@datedone, 1, 4);
	declare @month nvarchar(2) = substring(@datedone, 5, 2);
	declare @day nvarchar(2) = substring(@datedone, 7, 2);
	declare @hour nvarchar(2) = substring(@timedone, 1, 2);
	declare @min nvarchar(2) = substring(@timedone, 3, 2);
	declare @sec nvarchar(2) = substring(@timedone, 5, 2);
	
	/* yyyy-mm-dd hh:mi:ss (24h) (ODBC canonical) */
	return CONVERT(datetime, @year + '-' + @month + '-' + @day + ' ' + @hour + ':' + @min + ':' + @sec, 120)
END

GO

/**************************************************************************************/