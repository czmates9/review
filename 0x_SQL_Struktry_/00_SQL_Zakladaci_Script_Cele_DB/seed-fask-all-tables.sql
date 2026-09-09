SET NOCOUNT ON;
SET XACT_ABORT ON;
DBCC TRACEON(460);

DECLARE @RunToken nvarchar(20) = FORMAT(GETDATE(), 'yyyyMMddHHmmss');

CREATE TABLE #Before
(
    object_id int PRIMARY KEY,
    schema_name sysname NOT NULL,
    table_name sysname NOT NULL,
    row_count bigint NOT NULL
);

INSERT #Before (object_id, schema_name, table_name, row_count)
SELECT t.object_id, s.name, t.name, SUM(p.rows)
FROM sys.tables t
JOIN sys.schemas s ON s.schema_id = t.schema_id
JOIN sys.partitions p ON p.object_id = t.object_id AND p.index_id IN (0, 1)
WHERE t.is_ms_shipped = 0
GROUP BY t.object_id, s.name, t.name;

BEGIN TRANSACTION;

DECLARE @ObjectId int, @Schema sysname, @Table sysname;
DECLARE table_cursor CURSOR LOCAL FAST_FORWARD FOR
SELECT t.object_id, s.name, t.name
FROM sys.tables t
JOIN sys.schemas s ON s.schema_id = t.schema_id
WHERE t.is_ms_shipped = 0
  AND t.name NOT IN (N'FASK_Machines', N'FASK_Operations', N'FASK_Operations_Next', N'FASK_Logins_Auth', N'VLoginsGroups')
ORDER BY CASE WHEN t.name IN (N'FASK_MachineType', N'FASK_Logins', N'FASK_AGENDA', N'Groups') THEN 0 ELSE 1 END,
         t.object_id;

OPEN table_cursor;
FETCH NEXT FROM table_cursor INTO @ObjectId, @Schema, @Table;

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @Columns nvarchar(max), @Values nvarchar(max), @Sql nvarchar(max);

    SELECT
        @Columns = STRING_AGG(CONVERT(nvarchar(max), QUOTENAME(c.name)), N','),
        @Values = STRING_AGG(CONVERT(nvarchar(max),
            CASE
                WHEN ty.name IN (N'nvarchar',N'nchar',N'varchar',N'char',N'text',N'ntext') THEN
                    CASE
                        WHEN c.name=N'USERID' AND @Table=N'FASK_Logins' THEN N'LEFT(N''T''+@RunToken+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2),' + CONVERT(nvarchar(10),c.max_length/2) + N')'
                        WHEN c.name=N'id' AND @Table=N'Groups' THEN N'N''TG_''+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2)'
                        WHEN c.name IN (N'USERID',N'loginid') AND @Table <> N'FASK_Logins' THEN N'N''0'''
                        WHEN c.name IN (N'ITEMNMBR',N'KOD') THEN N'LEFT(N''TEST_ITEM_''+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2),' + CONVERT(nvarchar(10), CASE WHEN c.max_length=-1 THEN 4000 WHEN ty.name IN (N'nvarchar',N'nchar',N'ntext') THEN c.max_length/2 ELSE c.max_length END) + N')'
                        WHEN c.name IN (N'SOPNUMBE',N'PONUMBER') THEN N'LEFT(N''TEST_ORDER_''+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2),' + CONVERT(nvarchar(10), CASE WHEN c.max_length=-1 THEN 4000 WHEN ty.name IN (N'nvarchar',N'nchar',N'ntext') THEN c.max_length/2 ELSE c.max_length END) + N')'
                        WHEN c.name=N'VNDITNUM' THEN N'N''TEST_EAN_''+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2)'
                        WHEN c.name IN (N'SKL_ID',N'skl_id') THEN N'LEFT(N''TEST_SKL_''+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2),' + CONVERT(nvarchar(10), CASE WHEN c.max_length=-1 THEN 4000 WHEN ty.name IN (N'nvarchar',N'nchar',N'ntext') THEN c.max_length/2 ELSE c.max_length END) + N')'
                        WHEN c.name IN (N'LOCNCODE',N'KOD_LOK') THEN N'LEFT(N''TEST_LOC_''+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2),' + CONVERT(nvarchar(10), CASE WHEN c.max_length=-1 THEN 4000 WHEN ty.name IN (N'nvarchar',N'nchar',N'ntext') THEN c.max_length/2 ELSE c.max_length END) + N')'
                        WHEN c.name LIKE N'SER%N%MBR' OR c.name=N'SERLTNUM' THEN N'LEFT(N''TEST_SERIAL_''+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2),' + CONVERT(nvarchar(10), CASE WHEN c.max_length=-1 THEN 4000 WHEN ty.name IN (N'nvarchar',N'nchar',N'ntext') THEN c.max_length/2 ELSE c.max_length END) + N')'
                        ELSE N'LEFT(SUBSTRING(N''ABCDEFGHIJ'',@i,1)+N''_''+RIGHT(N''00''+CONVERT(nvarchar(2),@i),2)+N''_''+@RunToken+N''_''+CONVERT(nvarchar(12),' + CONVERT(nvarchar(12), @ObjectId) + N')+N''_' + REPLACE(c.name,'''','''''') + N''',' + CONVERT(nvarchar(10), CASE WHEN c.max_length=-1 THEN 4000 WHEN ty.name IN (N'nvarchar',N'nchar',N'ntext') THEN c.max_length/2 ELSE c.max_length END) + N')'
                    END
                WHEN ty.name=N'uniqueidentifier' THEN N'NEWID()'
                WHEN ty.name=N'bit' THEN N'CONVERT(bit,@i%2)'
                WHEN ty.name=N'tinyint' THEN N'CONVERT(tinyint,@i)'
                WHEN ty.name=N'smallint' THEN N'CONVERT(smallint,@i)'
                WHEN ty.name=N'int' THEN N'CONVERT(int,' + CONVERT(nvarchar(12), ABS(@ObjectId % 10000000)) + N'*100+@i)'
                WHEN ty.name=N'bigint' THEN N'CONVERT(bigint,' + CONVERT(nvarchar(12), @ObjectId) + N')*100+@i'
                WHEN ty.name IN (N'decimal',N'numeric') THEN N'CONVERT(' + ty.name + N'('+CONVERT(nvarchar(3),c.precision)+N','+CONVERT(nvarchar(3),c.scale)+N'),CASE WHEN '+CONVERT(nvarchar(3),c.precision-c.scale)+N'=1 THEN ((@i-1)%9)+1 ELSE @i END)'
                WHEN ty.name IN (N'money',N'smallmoney',N'float',N'real') THEN N'CONVERT(' + ty.name + N',@i)'
                WHEN ty.name=N'date' THEN N'CONVERT(date,DATEADD(day,-@i,GETDATE()))'
                WHEN ty.name=N'datetime' THEN N'CONVERT(datetime,DATEADD(second,@i,GETDATE()))'
                WHEN ty.name=N'smalldatetime' THEN N'CONVERT(smalldatetime,DATEADD(minute,@i,GETDATE()))'
                WHEN ty.name=N'datetime2' THEN N'CONVERT(datetime2,DATEADD(second,@i,GETDATE()))'
                WHEN ty.name=N'time' THEN N'CONVERT(time,DATEADD(second,@i,GETDATE()))'
                WHEN ty.name=N'binary' OR ty.name=N'varbinary' OR ty.name=N'image' THEN N'CONVERT(varbinary(max),NEWID())'
                WHEN ty.name=N'xml' THEN N'CONVERT(xml,N''<test run="''+@RunToken+N''" />'')'
                ELSE N'NULL'
            END), N',') WITHIN GROUP (ORDER BY c.column_id)
    FROM sys.columns c
    JOIN sys.types ty ON ty.user_type_id = c.user_type_id
    WHERE c.object_id=@ObjectId AND c.is_identity=0 AND c.is_computed=0 AND ty.name NOT IN(N'timestamp',N'rowversion');

    IF @Columns IS NOT NULL
    BEGIN
        RAISERROR(N'Seeding %s.%s', 0, 1, @Schema, @Table) WITH NOWAIT;
        SET @Sql=N'DECLARE @i int=1; WHILE @i<=10 BEGIN INSERT '+QUOTENAME(@Schema)+N'.'+QUOTENAME(@Table)+N' ('+@Columns+N') VALUES ('+@Values+N'); SET @i+=1; END;';
        EXEC sys.sp_executesql @Sql, N'@RunToken nvarchar(20)', @RunToken=@RunToken;
    END;

    FETCH NEXT FROM table_cursor INTO @ObjectId, @Schema, @Table;
END;

CLOSE table_cursor;
DEALLOCATE table_cursor;

-- Tables with declared or logical relationships are populated explicitly.
DECLARE @i int=1;
WHILE @i<=10
BEGIN
    DECLARE @suffix nvarchar(2)=RIGHT(N'00'+CONVERT(nvarchar(2),@i),2);
    DECLARE @machineType nvarchar(20)=(SELECT TOP(1) machinetype FROM dbo.FASK_MachineType ORDER BY NEWID());
    INSERT dbo.FASK_Machines(id,machinetype,name,description,koeficient)
    VALUES(N'TEST_M_'+@suffix,@machineType,N'Machine '+@suffix,N'Test machine '+@suffix,1);
    INSERT dbo.FASK_Operations(machinetype,IDO,NAZEV,CK,SCAN1,SCAN2,SCAN3,SCANZAKAZKA,SENSOR,VOLNA,START,KONEC,SPHLAVICKA,SPINFO,SPZAKAZKA,KONTROLAMAT,SPMATERIAL,LOGIN)
    VALUES(@machineType,N'TEST_OP_'+@suffix,N'Test operation '+@suffix,N'TEST_CK_'+@suffix,0,0,0,0,0,1,0,0,N'',N'',N'',0,N'',0);
    INSERT dbo.FASK_Logins_Auth(USERID,AGENDAID,AUTH)
    VALUES(LEFT(N'T'+@RunToken+@suffix,20),(SELECT TOP(1) AGENDAID FROM dbo.FASK_AGENDA ORDER BY NEWID()),1);
    INSERT dbo.VLoginsGroups(loginid,groupid)
    VALUES(LEFT(N'T'+@RunToken+@suffix,20),(SELECT TOP(1) LEFT(id,10) FROM dbo.Groups ORDER BY NEWID()));
    SET @i+=1;
END;

SET @i=1;
WHILE @i<=10
BEGIN
    DECLARE @mt nvarchar(20), @ido nvarchar(20), @next nvarchar(20);
    SELECT @mt=machinetype,@ido=IDO FROM
      (SELECT machinetype,IDO,ROW_NUMBER() OVER(ORDER BY IDO) rn FROM dbo.FASK_Operations WHERE IDO LIKE N'TEST_OP_%') q WHERE rn=@i;
    SET @next=NULL;
    SELECT TOP(1) @next=IDO FROM dbo.FASK_Operations WHERE machinetype=@mt AND IDO<>@ido ORDER BY IDO DESC;
    IF @next IS NULL SET @next=@ido;
    INSERT dbo.FASK_Operations_Next(machinetype,IDO,IDO_NEXT) VALUES(@mt,@ido,@next);
    SET @i+=1;
END;

COMMIT TRANSACTION;

DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS;

SELECT b.schema_name,b.table_name,b.row_count AS before_rows,
       SUM(p.rows) AS after_rows,SUM(p.rows)-b.row_count AS added_rows
FROM #Before b
JOIN sys.partitions p ON p.object_id=b.object_id AND p.index_id IN(0,1)
GROUP BY b.schema_name,b.table_name,b.row_count
ORDER BY b.schema_name,b.table_name;
