/****** Object:  StoredProcedure [dbo].[fask_proc_EditFuncProc]     ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Tadeas Divacky
-- Create date:		25.8.2020 
-- Description:	Jedná se o proceduru sloužící pro hromadnou editaci Funkci a Procedur
-- =============================================
CREATE PROCEDURE [dbo].[fask_proc_EditFuncProc] 
	@TEXT_OLD nvarchar(100),
	@TEXT_NEW nvarchar(100)
	AS
BEGIN
	SET NOCOUNT ON;


DECLARE @sp_names TABLE
(
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(MAX)
);

--Pomocná tabulka
DECLARE @HelpText TABLE
(
    Val NVARCHAR(MAX)
);


--Deklarace promennych
DECLARE @sp_count INT,
        @count INT = 0,
        @sp_name NVARCHAR(128),
        @text NVARCHAR(MAX);

INSERT  @sp_names
SELECT
    sch.name+'.'+ob.name AS Name
FROM 
     sys.objects AS ob
     LEFT JOIN sys.schemas AS sch ON
            sch.schema_id = ob.schema_id
     LEFT JOIN sys.sql_modules AS mod ON
            mod.object_id = ob.object_id
WHERE mod.definition IS NOT NULL
AND ob.type_desc in (
'SQL_INLINE_TABLE_VALUED_FUNCTION',
'SQL_SCALAR_FUNCTION',
'SQL_TABLE_VALUED_FUNCTION',
'SQL_STORED_PROCEDURE'
)

SET @sp_count = (SELECT COUNT(1) FROM @sp_names)

--Cyklus přes všechny
WHILE (@sp_count > @count)
BEGIN
    SET @count = @count + 1; -- inkrement pro projiti všech procedur
    SET @text = N''; -- Prazdny text

	--Vytažení Name procedruz podle jedinečneho ID pořadí
    SET @sp_name = (SELECT  name
                    FROM    @sp_names
                    WHERE   ID = @count);

	-- Vytažení Obsahu textu tela procedury
    INSERT INTO @HelpText
    EXEC sp_HelpText @sp_name;

	--Vytažení obsahu  textu procedury
    SELECT  @text = COALESCE(@text + ' ' + Val, Val)
    FROM    @HelpText;

	--Smazani tmp promenne 
    DELETE FROM @HelpText;


    IF @text LIKE '%' + @TEXT_OLD + '%'
    BEGIN
		IF @text LIKE '%CREATE PROCEDURE%'
			BEGIN
				SET @text = REPLACE(@text, 'CREATE PROCEDURE', 'ALTER PROCEDURE');
			END
		ELSE IF @text LIKE '%CREATE FUNCTION%'
			BEGIN
				SET @text = REPLACE(@text, 'CREATE FUNCTION', 'ALTER FUNCTION');
			END

			SET @text = REPLACE(@text, @TEXT_OLD, @TEXT_NEW);

			EXECUTE sp_executesql @text;
    END
END

END
GO
/**************************************************************************************/

