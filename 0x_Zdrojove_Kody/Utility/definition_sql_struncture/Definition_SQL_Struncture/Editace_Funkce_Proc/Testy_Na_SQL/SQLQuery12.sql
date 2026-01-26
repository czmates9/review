--Pomocná tabulka
DECLARE @HelpText TABLE
(
    Val NVARCHAR(MAX)
);

--Deklarace další tabulky
DECLARE @sp_names TABLE
(
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(128)
);

--Deklarace promennych
DECLARE @sp_count INT,
        @count INT = 0,
        @sp_name NVARCHAR(128),
        @text NVARCHAR(MAX);


--Vytažení všech procedru, ID a Name
INSERT  @sp_names
SELECT  name
FROM    sys.Procedures;

--Vytažení poètu procedur
SET @sp_count = (SELECT COUNT(1) FROM sys.Procedures);

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


    IF @text LIKE '%SET NOCOUNT ON%'
    BEGIN
        SELECT @text;
    END
    ELSE --Not found, should be added.
    BEGIN
        SET @text = REPLACE(@text, 'CREATE PROCEDURE', 'ALTER PROCEDURE');

        DECLARE @Find NVARCHAR(255);
        SET @Find = 'BEGIN';

        SET @text = STUFF(@text, CHARINDEX(@Find, @text), LEN(@Find), @Find + CHAR(13) + CHAR(10) + SPACE(4) + 'SET NOCOUNT ON;');

        EXECUTE sp_executesql @text;
    END
END