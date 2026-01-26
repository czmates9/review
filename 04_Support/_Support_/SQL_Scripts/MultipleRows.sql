--CREATE TABLE TestTable
--(
-- ID INT IDENTITY(1,1),
-- Col1 varchar(10),
-- Repeats INT
--)

--INSERT INTO TESTTABLE
--VALUES ('A',2), ('B',4),('C',1),('D',0)

select * from testtable

--declare @repeats int
--set @repeats = 1

--WITH x AS 
--(
--  --SELECT TOP (SELECT MAX(Repeats)+1 FROM TestTable) rn = ROW_NUMBER() 
--  SELECT TOP (
--	SELECT MAX(Repeats)+1 FROM TestTable
--	) rn = ROW_NUMBER()  OVER (ORDER BY [object_id])
--  FROM sys.all_columns 
--  ORDER BY [object_id]
--)
--SELECT * FROM x
--CROSS JOIN TestTable AS d
----WHERE x.rn <= d.Repeats 
--WHERE x.rn <= d.Repeats
--ORDER BY Col1;


declare @repeats int
set @repeats = 3

SELECT * FROM 
(
  SELECT TOP (
	--SELECT MAX(Repeats)+1 FROM TestTable
	@repeats
	) rn = ROW_NUMBER()  OVER (ORDER BY [object_id])
  FROM sys.all_columns 
  ORDER BY [object_id]
) as x
CROSS JOIN TestTable AS d
--WHERE x.rn <= d.Repeats 
WHERE x.rn <= @repeats
ORDER BY Col1;