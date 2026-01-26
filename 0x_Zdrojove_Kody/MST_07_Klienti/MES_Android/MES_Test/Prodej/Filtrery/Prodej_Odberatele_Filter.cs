using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using Java.Lang;

namespace MES_Android.Prodej
{
    public class Prodej_Odberatele_Filter : Filter
    {
        private readonly Prodej_Odberatele_Item_Adapter _adapter;
        public Prodej_Odberatele_Filter(Prodej_Odberatele_Item_Adapter adapter)
        {
            _adapter = adapter;
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            try
            {

                //1. Vytvorim returnObj který vrací tahle metoda
                //2. Vytvorim objekt results, který se vrací nahraje do predchoziho objektu

                //3. Pokud je PolozkaSeznam_Original, to zneamena ten seznam pred hledanim NULL 
                //   tak sem asi ešte nehledal, a měl bych si udelat zalohu stavajiciho

                //4. Predávany parametr constraint je null, anebo nic neobsahuje, tak do vystupu nakopiruju cely original
                // a nasledne vrati to co vyplnil

                //5. Skontoluju či mam udelanou zalohu, a pak se pokusim najiv v zalohe odpovidajici řadky, a ty si nakopiruju do results
                // ten nakoren returnu ven jak vysledek


                using (constraint)
                {
                    // 1
                    var returnObj = new FilterResults();

                    // 2
                    var results = new Prodej_Odberatele_Seznam(new Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable());

                    // 3
                    if (_adapter.PolozkaSeznam_Original == null)
                    {
                        _adapter.PolozkaSeznam_Original = new Prodej_Odberatele_Seznam((Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable)_adapter.PolozkaSeznam.mItems.Copy());
                        _adapter.PolozkaSeznam_Original.mItems.AcceptChanges();
                    }

                    // 4
                    if (constraint == null || string.IsNullOrEmpty(constraint.ToString()))
                    {

                        results = null;
                        results = new Prodej_Odberatele_Seznam((Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable)_adapter.PolozkaSeznam_Original.mItems.Copy());
                        results.mItems.AcceptChanges();


                        returnObj.Values = results;
                        //constraint.Dispose();
                        return returnObj;
                    }

                    // 5
                    if (_adapter.PolozkaSeznam_Original != null && _adapter.PolozkaSeznam_Original.mItems.Count > 0)
                    {

                        for (int i = 0; i < _adapter.PolozkaSeznam_Original.mItems.Count; i++)
                        {
                            Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row item = _adapter.PolozkaSeznam_Original.mItems[i];


                            if (item.odb_desc.Contains(constraint.ToString().Trim()))
                            {
                                var row = results.mItems.NewCZMST090Row();
                                foreach (System.Data.DataColumn col in _adapter.PolozkaSeznam_Original.mItems.Columns)
                                {
                                    row[col.ColumnName] = item[col.ColumnName] == null ? DBNull.Value : item[col.ColumnName];
                                }
                                results.mItems.AddCZMST090Row(row);
                            }

                        }


                        returnObj.Values = results;
                        return returnObj;

                    }

                    returnObj.Values = results;
                    return returnObj;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            try
            {
                using (var values = results.Values)
                {
                    if (values is Prodej_Odberatele_Seznam)
                    {
                        var a = (Prodej_Odberatele_Seznam)values;

                        _adapter.PolozkaSeznam.mItems.Clear();
    
                        foreach (var itemo in a.mItems.Rows)
                        {

                            Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row item = (Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row)itemo;

                            var row = _adapter.PolozkaSeznam.mItems.NewCZMST090Row();
                            foreach (System.Data.DataColumn col in _adapter.PolozkaSeznam_Original.mItems.Columns)
                            {
                                row[col.ColumnName] = item[col.ColumnName] == null ? DBNull.Value : item[col.ColumnName];
                            }

                            _adapter.PolozkaSeznam.mItems.AddCZMST090Row(row);
                        }

                        _adapter.PolozkaSeznam.mItems.AcceptChanges();
                    }


                }
                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

    }

}