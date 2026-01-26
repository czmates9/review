using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZZS_Servis_096_AD
{

   public class Pracovnici
    {
       public static bool DeletePracovnici()
	{
           
           System.Data.SqlClient.SqlTransaction trans = null;
           System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.ConnectionString);
                connection.Open();

                DataSets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new DataSets.PracovniciTableAdapters.CZMST096TableAdapter();
                ta_096.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_096.MyTransaction = trans;

                ta_096.Delete();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            } 
	}


       public static bool InsertPracovnici(DataSets.Pracovnici.CZMST096Row PracovniciRow)
       {
           System.Data.SqlClient.SqlTransaction trans = null;
           System.Data.SqlClient.SqlConnection connection = null;

           try
           {
               connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.ConnectionString);
               connection.Open();

               DataSets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new DataSets.PracovniciTableAdapters.CZMST096TableAdapter();
               ta_096.Connection = connection;
               trans = connection.BeginTransaction(IsolationLevel.Serializable);
               ta_096.MyTransaction = trans;

               ta_096.Insert(
                   PracovniciRow.prac_id,
                   PracovniciRow.prac_desc,
                   PracovniciRow.prac_typ,
                   PracovniciRow.prac_carcode);


               if (trans != null)
                   trans.Commit();

               return true;
           }
           catch
           {
               try
               {
                   if (trans != null)
                       trans.Rollback();
               }
               catch { }
               throw;
           }
           finally
           {
               if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                   connection.Close();
           }
       }


       public static void SynchronizacePracovniciAD()
       {

           try
           {
               Pracovnici.DeletePracovnici();

               using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, Properties.Settings.Default.DomenaAD.Trim(), Properties.Settings.Default.Login, Properties.Settings.Default.PassWord))
               {

                   DataSets.Pracovnici ds1 = new DataSets.Pracovnici();

                   using (System.DirectoryServices.AccountManagement.PrincipalSearcher searcher = new System.DirectoryServices.AccountManagement.PrincipalSearcher(new System.DirectoryServices.AccountManagement.UserPrincipal(context)))
                   {

                       foreach (System.DirectoryServices.AccountManagement.Principal result in searcher.FindAll())
                       {

                           System.DirectoryServices.DirectoryEntry de = result.GetUnderlyingObject() as System.DirectoryServices.DirectoryEntry;
                           AD.ADUserDetail detrail = AD.ADUserDetail.GetUser(de);


                           if (!string.IsNullOrEmpty(detrail.DISPLAYNAME))
                           {
                               DataSets.Pracovnici.CZMST096Row row = ds1.CZMST096.NewCZMST096Row();

                               row.prac_id = detrail.ID;             // ID  30
                               row.prac_desc = detrail.DISPLAYNAME;  // 40
                               row.prac_typ = "-";                   // 3
                               row.prac_carcode = detrail.ID;        // 21 

                               ds1.CZMST096.AddCZMST096Row(row);

                               Pracovnici.InsertPracovnici(row);
                           }
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               Log.Write(ex.Message);
               //throw;
           }

       }


    }
}
