
-Pomoci tohoto dotazu se lze dostat k existujim založenym scriptom

SELECT
    sch.name+'.'+ob.name AS       [Object], 
    ob.create_date, 
    ob.modify_date, 
    ob.type_desc, 
    mod.definition
FROM 
     sys.objects AS ob
     LEFT JOIN sys.schemas AS sch ON
            sch.schema_id = ob.schema_id
     LEFT JOIN sys.sql_modules AS mod ON
            mod.object_id = ob.object_id
WHERE mod.definition IS NOT NULL --Selects only objects with the definition (code)



-Před založením by bylo možno dobre spravit PowerShell Sctipt který upravý potřebné soubory...


SELECT sm.object_id, OBJECT_NAME(sm.object_id) AS object_name, o.type, o.type_desc, sm.definition  
FROM sys.sql_modules AS sm  
JOIN sys.objects AS o ON sm.object_id = o.object_id  
ORDER BY o.type;  
GO  