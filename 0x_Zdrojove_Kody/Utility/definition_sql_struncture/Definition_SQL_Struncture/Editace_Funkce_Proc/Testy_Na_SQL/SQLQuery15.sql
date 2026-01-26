
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



INSERT  @sp_names
SELECT  name
FROM    sys.Procedures;




    SET @sp_name = (SELECT  name
                    FROM    @sp_names
                    WHERE   ID = 1);

SELECT @sp_name

    INSERT INTO @HelpText
    EXEC sp_HelpText @sp_name;

    SELECT  @text = COALESCE(@text + ' ' + Val, Val)
    FROM    @HelpText;

	SELECT @text

	    IF @text LIKE '%StwPh%'
    BEGIN
        --SELECT @text;
		SELECT 'ANO'
    END
    ELSE --Not found, should be added.
    BEGIN
	SELECT 'NE'
        --SET @text = REPLACE(@text, 'CREATE PROCEDURE', 'ALTER PROCEDURE');

        --DECLARE @Find NVARCHAR(255);
        --SET @Find = 'BEGIN';

        --SET @text = STUFF(@text, CHARINDEX(@Find, @text), LEN(@Find), @Find + CHAR(13) + CHAR(10) + SPACE(4) + 'SET NOCOUNT ON;');

        --EXECUTE sp_executesql @text;
    END