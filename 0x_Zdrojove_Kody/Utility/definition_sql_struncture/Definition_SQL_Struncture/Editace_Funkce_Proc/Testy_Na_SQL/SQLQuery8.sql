
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

--Informativní testy
--SELECT * from @sp_names

SET @sp_count = (SELECT COUNT(1) FROM @sp_names)

--Informativní testy
--SELECT @sp_count

--Cyklus pøes všechny
WHILE (@sp_count > @count)
BEGIN
    SET @count = @count + 1; -- inkrement pro projiti všech procedur
    SET @text = N''; -- Prazdny text

	--Vytažení Name procedruz podle jedineèneho ID poøadí
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


    IF @text LIKE '%StwPh%'
    BEGIN
		IF @text LIKE '%CREATE PROCEDURE%'
			BEGIN
				--SET @text = REPLACE(@text, 'CREATE PROCEDURE', 'ALTER PROCEDURE');
				SELECT 'Proc'
			END
		ELSE IF @text LIKE '%CREATE FUNCTION%'
			BEGIN
				--SET @text = REPLACE(@text, 'CREATE FUNCTION', 'ALTER FUNCTION');
				SELECT 'Funkce'
			END

        SELECT 'Ano';
    END
    ELSE 
    BEGIN
		SELECT 'NE'
    END
END