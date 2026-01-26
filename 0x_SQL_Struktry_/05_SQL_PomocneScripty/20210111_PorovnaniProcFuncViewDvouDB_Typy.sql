SELECT 
O.*,
C.*
 FROM 
 (SELECT
    sch.name+'.'+ob.name AS [Object], 
    ob.create_date, 
    ob.modify_date, 
    ob.type_desc, 
    mod.definition
FROM 
     FASKPOH_HANIBAL_TEST_Cip.sys.objects AS ob
     LEFT JOIN FASKPOH_HANIBAL_TEST_Cip.sys.schemas AS sch ON
            sch.schema_id = ob.schema_id
     LEFT JOIN FASKPOH_HANIBAL_TEST_Cip.sys.sql_modules AS mod ON
            mod.object_id = ob.object_id
WHERE mod.definition IS NOT NULL ) as O
left join 

 (SELECT
    sch.name+'.'+ob.name AS [Object], 
    ob.create_date, 
    ob.modify_date, 
    ob.type_desc, 
    mod.definition
FROM 
     FASKSB_AgroTherm.sys.objects AS ob
     LEFT JOIN FASKSB_AgroTherm.sys.schemas AS sch ON
            sch.schema_id = ob.schema_id
     LEFT JOIN FASKSB_AgroTherm.sys.sql_modules AS mod ON
            mod.object_id = ob.object_id
WHERE mod.definition IS NOT NULL ) as C
ON C.Object = O.Object AND C.type_desc = O.type_desc
WHERE O.definition != C.definition