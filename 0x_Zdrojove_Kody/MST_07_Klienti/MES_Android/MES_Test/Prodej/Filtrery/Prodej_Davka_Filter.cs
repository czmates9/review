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
    public class Prodej_Davka_Filter : Filter
    {
        private readonly Prodej_Davka_Item_Adapter _adapter;
        public Prodej_Davka_Filter(Prodej_Davka_Item_Adapter adapter)
        {
            _adapter = adapter;
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            try
            {
                var returnObj = new FilterResults();

                var results = new Prodej_Davka_Seznam(new ObservableCollection<Prodej_Davka_Item>());


                if (_adapter.PolozkaSeznam_Original == null)
                {
                    _adapter.PolozkaSeznam_Original = new Prodej_Davka_Seznam(new ObservableCollection<Prodej_Davka_Item>());

                    foreach (var item in _adapter.PolozkaSeznam.mItems)
                    {
                        _adapter.PolozkaSeznam_Original.mItems.Add(item);
                    }
                }

                if (constraint == null || string.IsNullOrEmpty(constraint.ToString()))
                {
                    foreach (var item in _adapter.PolozkaSeznam_Original.mItems)
                    {
                        results.mItems.Add(item);
                    }

                    returnObj.Values = results;
                    return returnObj;
                }

                if (_adapter.PolozkaSeznam_Original != null && _adapter.PolozkaSeznam_Original.mItems.Any())
                {

                    var xx = _adapter.PolozkaSeznam_Original.mItems.Where(x => x.CisloDavky.ToString().Contains(constraint.ToString()));

                    foreach (var item in xx)
                    {
                        results.mItems.Add(item);
                    }
                }

                returnObj.Values = results;

                constraint.Dispose();

                return returnObj;
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
                    if (values is Prodej_Davka_Seznam)
                    {
                        var a = (Prodej_Davka_Seznam)values;

                        _adapter.PolozkaSeznam.mItems.Clear();

                        foreach (var item in a.mItems)
                        {
                            _adapter.PolozkaSeznam.mItems.Add(item);
                        }
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