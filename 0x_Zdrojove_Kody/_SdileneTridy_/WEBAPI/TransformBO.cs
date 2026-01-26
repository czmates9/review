using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Fask.WEBAPI
{

    /// <summary>
    /// Popis parametrù univerzalnych
    /// </summary>
    /// <typeparam name="T">T - jedna se o Business object "Tabulky"</typeparam>
    /// <typeparam name="U">U - jedna se o Business object "Radku"</typeparam>
    /// <typeparam name="V">V - jedna se o typovy datatable "Tabulka"</typeparam>
    /// <typeparam name="W">W - jedna se o typovy datatable "Radek"</typeparam>
    public class BusinessObject<T, U, V, W>
    {
        /// <summary>
        /// c'tor
        /// </summary>
        public BusinessObject()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public T GetBOFromDT(DataTable dt)
        {

            try
            {

                List<U> us = new List<U>();

                T dt_tmp = (T)Activator.CreateInstance(typeof(T)); // Tvorba instance
                U row_xxx = (U)Activator.CreateInstance(typeof(U)); // ??? hmmm

                System.Reflection.BindingFlags BF =
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic;

                var fieldValues = dt_tmp.GetType().GetFields(BF);
                var fieldRow = row_xxx.GetType().GetFields(BF);


                foreach (DataRow row in dt.Rows)
                {
                    bool FlagFirtsRS = true;
                    U row_tmp = (U)Activator.CreateInstance(typeof(U)); // ??? hmmm

                    foreach (DataColumn column in dt.Columns)
                    {
                        foreach (var variable in fieldRow)
                        {
                            string MenoPromenne = RemoveVata(variable.Name);

                            if (MenoPromenne == "RowState" && FlagFirtsRS)
                            {
                                var propInfo = row_tmp.GetType().GetProperty(MenoPromenne);
                                if (propInfo != null)
                                {
                                    propInfo.SetValue(row_tmp, row.RowState, null);

                                    if (row.RowState == DataRowState.Added)
                                        row.AcceptChanges();


                                    if(row.RowState == DataRowState.Deleted)
                                        row.RejectChanges();
                                    

                                    FlagFirtsRS = false;
                                }
                            }

                            if (MenoPromenne == column.ColumnName)
                            {
                                var propInfo = row_tmp.GetType().GetProperty(MenoPromenne);
                                if (propInfo != null)
                                {

                                    var x = row[column.ColumnName];

                                    string typ_hodnota = x.GetType().Name;
                                    string typ_hodnota2 = x.GetType().FullName;

                                    if (x is string)
                                        propInfo.SetValue(row_tmp, (string)row[column.ColumnName], null);
                                    else if (x is int)
                                        propInfo.SetValue(row_tmp, (int)row[column.ColumnName], null);
                                    else if (x is Int16)
                                        propInfo.SetValue(row_tmp, (Int16)row[column.ColumnName], null);
                                    else if (x is DateTime)
                                        propInfo.SetValue(row_tmp, (DateTime)row[column.ColumnName], null);
                                    else if (x is Byte)
                                        propInfo.SetValue(row_tmp, (Byte)row[column.ColumnName], null);
                                    else if (x is Int32)
                                        propInfo.SetValue(row_tmp, (Int32)row[column.ColumnName], null);
                                    else if (x is Decimal)
                                        propInfo.SetValue(row_tmp, (Decimal)row[column.ColumnName], null);
                                    else if (x is Single)
                                        propInfo.SetValue(row_tmp, (Single)row[column.ColumnName], null);
                                    else if (x is Guid)
                                        propInfo.SetValue(row_tmp, (Guid)row[column.ColumnName], null);
                                    else
                                        propInfo.SetValue(row_tmp, null, null);

                                }
                                break;
                            }
                        }
                    }

                    us.Add(row_tmp);
                }


                if (fieldValues.Length == 1)
                {
                    string MenoPromenne = RemoveVata(fieldValues[0].Name);

                    var propInfo = dt_tmp.GetType().GetProperty(MenoPromenne);
                    if (propInfo != null)
                    {
                        propInfo.SetValue(dt_tmp, us.ToArray(), null);
                    }
                }

                return dt_tmp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public V GetDTFromBO(T bo)
        {

            try
            {
                DataRowState RS = DataRowState.Unchanged;
                ///Vytvoreni instanci
                // T dt_tmp = (T)Activator.CreateInstance(typeof(T)); // Tvorba instance
                U row_tmp = (U)Activator.CreateInstance(typeof(U)); // Radek
                V dt_typ_tmp = (V)Activator.CreateInstance(typeof(V)); // Radek

                DataTable d = null;


                if (dt_typ_tmp is DataTable)
                {
                    d = dt_typ_tmp as DataTable;
                }


                //nastaveni flagu pro vraceni seznamu promennych
                System.Reflection.BindingFlags BF =
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic;

                //vraceni seznamu promennych z BO
                var fieldValues = bo.GetType().GetFields(BF);
                var fieldRow = row_tmp.GetType().GetFields(BF);

                if (fieldValues.Length == 1)
                {
                    string MenoPromenne = RemoveVata(fieldValues[0].Name);

                    var propInfo = bo.GetType().GetProperty(MenoPromenne);
                    if (propInfo != null)
                    {
                        var x = propInfo.GetValue(bo);

                        if (x is U[])
                        {
                            U[] uuu = x as U[];
                            List<U> iii = uuu.ToList<U>();

                            foreach (U item in iii)
                            {
                                var row = d.NewRow();
                                var fieldRadek = item.GetType().GetFields(BF);

                                foreach (var variable in fieldRadek)
                                {
                                    string MenoPromenne2 = RemoveVata(variable.Name);

                                    if (MenoPromenne2 == "RowState")
                                    {
                                        var propInfo2 = item.GetType().GetProperty(MenoPromenne2);
                                        if (propInfo2 != null)
                                        {
                                            var vvv = propInfo2.GetValue(item);
                                            if (vvv is System.Data.DataRowState)
                                            {
                                                RS = (DataRowState)vvv;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        var propInfo2 = item.GetType().GetProperty(MenoPromenne2);
                                        if (propInfo2 != null)
                                        {
                                            var vvv = propInfo2.GetValue(item);

                                            if (vvv == null)
                                                row[MenoPromenne2] = DBNull.Value;
                                            else
                                                row[MenoPromenne2] = vvv;
                                        }
                                    }
                                }

                                d.Rows.Add(row);

                                row.AcceptChanges();

                                switch (RS)
                                {
                                    case DataRowState.Added:
                                        row.SetAdded();
                                        break;
                                    case DataRowState.Deleted:
                                        row.Delete();
                                        break;
                                    case DataRowState.Modified:
                                        row.SetModified();
                                        break;
                                    default:
                                        break;
                                }

                            }
                        }
                    }
                }

                //pozor zmena, kdyztak odkomentovat !!!
                //d.AcceptChanges();

                return dt_typ_tmp;

                //    foreach (DataRow row in dt.Rows)
                //{
                //    U row_tmp = (U)Activator.CreateInstance(typeof(U)); // ??? hmmm

                //    foreach (DataColumn column in dt.Columns)
                //    {



                //            if (MenoPromenne == column.ColumnName)
                //            {
                //                var propInfo = row_tmp.GetType().GetProperty(MenoPromenne);
                //                if (propInfo != null)
                //                {
                //                    propInfo.SetValue(row_tmp, row[column.ColumnName], null);
                //                }
                //                break;
                //            }
                //        }
                //    }

                //    us.Add(row_tmp);
                //}


                //if (fieldValues.Length == 1)
                //{
                //    string MenoPromenne = RemoveVata(fieldValues[0].Name);

                //    var propInfo = dt_tmp.GetType().GetProperty(MenoPromenne);
                //    if (propInfo != null)
                //    {
                //        propInfo.SetValue(dt_tmp, us.ToArray(), null);
                //    }
                //}

                //return dt_tmp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Metoda pro odstraneni textu okolo promenne
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private string RemoveVata(string txt)
        {
            string MenoPromenne = txt.Replace("<", "");
            return MenoPromenne.Replace(">k__BackingField", "");
        }

    }
}

