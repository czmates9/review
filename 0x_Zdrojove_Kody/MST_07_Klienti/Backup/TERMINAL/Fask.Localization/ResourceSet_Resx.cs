using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Resources;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Globalization;

namespace Fask.Localization
{
    public class ResourceSet_Resx : System.Resources.ResourceSet
    {

        //public override void Close()
        //{
        //    base.Close();
        //}

        private Dictionary<string, object> localizationData = new Dictionary<string, object>();

        public ResourceSet_Resx(List<string> fileNames)
        {
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            sw.Start();

            localizationData = Fask.Localization.LocalizationSupport.LoadFormLocalizationDataFromFiles(fileNames);
            System.Diagnostics.Debug.WriteLine("ResourceSet_Resx LoadFormLocalizationDataFromFiles " + sw.ElapsedMilliseconds);      
            ReadResources();
        }

        protected override void ReadResources()
        {
            //// nacteni z resx. dle cultury ... 
            //this.Table.Add("df_VNDDOCNM.Location", new System.Drawing.Point(30, 150));
            ////...

            foreach (string key in localizationData.Keys)
            {
                this.Table.Add(key, localizationData[key]);
            }

        }
    }

}
