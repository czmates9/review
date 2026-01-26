
--Deklarace promennych
DECLARE @sp_count INT = 5,
        @count INT = 0


WHILE (@sp_count > @count)
BEGIN
    SET @count = @count + 1; -- inkrement pro projiti všech procedur

	SELECT @count

END