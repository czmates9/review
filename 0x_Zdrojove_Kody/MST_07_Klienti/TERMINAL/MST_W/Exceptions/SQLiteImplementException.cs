using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Exceptions
{

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

    //public class SQLiteImplementException : NotImplementedException
    //{
    //    // Summary:
    //    //     Initializes a new instance of the System.NotImplementedException class with
    //    //     default properties.
    //    public SQLiteImplementException()
    //        : base()
    //    {
    //    }
    //    //
    //    // Summary:
    //    //     Initializes a new instance of the System.NotImplementedException class with
    //    //     a specified error message.
    //    //
    //    // Parameters:
    //    //   message:
    //    //     The error message that explains the reason for the exception.
    //    public SQLiteImplementException(string message)
    //        : base(message)
    //    {
    //    }
    //    //
    //    // Summary:
    //    //     Initializes a new instance of the System.NotImplementedException class with
    //    //     a specified error message and a reference to the inner exception that is
    //    //     the cause of this exception.
    //    //
    //    // Parameters:
    //    //   message:
    //    //     The error message that explains the reason for the exception.
    //    //
    //    //   inner:
    //    //     The exception that is the cause of the current exception. If the inner parameter
    //    //     is not null, the current exception is raised in a catch block that handles
    //    //     the inner exception.
    //    public SQLiteImplementException(string message, Exception innerException)
    //        : base(message, innerException)
    //    {
    //    }
    //}
}
