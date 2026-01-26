+++++++++++++++++++

SELECT * FROM (
		Select
--		"_rowid_",
		ROW_NUMBER() OVER ( 
			--ORDER BY ITEMNMBR DESC
			--ORDER BY ITEMDESC DESC
			--ORDER BY ITEMDESC, ITEMNMBR
		) RowNum,
		* 
		from 
		(
			Select * 
			FROM CZMST_I1
			where 
			1=1
			--and CZ_CarKod like '%-01%'
			--and ITEMDESC like '%c%'
			--order by ITEMNMBR DESC
			ORDER BY ITEMDESC
		)
) x
WHERE 1=1
--	and	x.RowNum BETWEEN 0 and 1000
	and	x.RowNum > 0
ORDER BY x.RowNum
LIMIT 1000


++++++++++++++++++++++++++++

SELECT
	ROW_NUMBER () OVER ( 
		--ORDER BY ITEMNMBR
		ORDER BY ITEMDESC, ITEMNMBR
	) RowNum,
	*
FROM
(
Select * 
FROM CZMST_I1
where 
1=1
and CZ_CarKod like '%-01%'
--order by ITEMDESC
) x
LIMIT 200



+++++++++++++++++++++++++++++++++++++++++++++++++++++
//https://www.sqlitetutorial.net/sqlite-window-functions/sqlite-row_number/

// nejak takto ... 
//select count(*) from Users;

//SELECT
//    ROW_NUMBER () OVER ( 
//        ORDER BY FIRSTNAME desc 
//    ) RowNum,
//    *
//FROM
//    users;


// ===>> takto ... ???
//Select * FROM (
//    SELECT
//        ROW_NUMBER () OVER ( 
//            ORDER BY FIRSTNAME desc 
//        ) RowNum,
//        *
//    FROM
//        users
//) t
//where t.RowNum > 3 LIMIT 2

// >>>>>>> ?takto? <<<<<<<
//SELECT
//    ROW_NUMBER () OVER ( 
//        --ORDER BY ITEMNMBR
//        ORDER BY ITEMDESC, ITEMNMBR
//    ) RowNum,
//    *
//FROM
//(
//Select * 
//FROM CZMST_I1
//where 
//1=1
//and CZ_CarKod like '%-01%'
//--order by ITEMDESC
//) x
//LIMIT 200
