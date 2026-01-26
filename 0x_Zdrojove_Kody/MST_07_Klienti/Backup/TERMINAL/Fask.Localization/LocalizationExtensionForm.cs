using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;





namespace Fask.Localization
{
    public static class LocalizationExtensionForm
    {
        //private static System.Resources.ResourceManager resman;
        private static System.ComponentModel.ComponentResourceManager resman;

        private static ComponentResourceManager_resource resman_resource;

        private static ComponentResourceManager_ResX resman_resX; 

        /// <summary>
        /// Data vlastni lokalizace.
        /// </summary>
        //private static System.Collections.Specialized.NameValueCollection localizationData = null;
        //private static System.Collections.Generic.Dictionary<string, object> localizationData = null;
        //public static CultureInfo ci;
        public static void Localize(this Form form)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            try
            {
                //Logging.TracId id = new Fask.Logging.TracId(null, null, null, "LocalizationExtensionForm", "Localize(this Form form)");
                //Logging.Trace2.Write("Start", "Start Lokalizace", id);

                
                // lokalizace neni povolena ...
                if (Globals.LokalizacePovolit)
                {
                    
                    //Logging.Trace2.Write("Start", "Globals.LokalizacePovolit", id);
                    resman = new System.ComponentModel.ComponentResourceManager(form.GetType());
                    //resman = System.ComponentModel.ComponentResourceManager.CreateFileBasedResourceManager(form.Name, Globals.LocalizationDir, null);
                    //resman = (System.ComponentModel.ComponentResourceManager)System.Resources.ResourceManager.CreateFileBasedResourceManager(form.Name, Globals.LocalizationDir, null);
                    
                    //resman = new MyComponentResourceManager(form.GetType());

                    
                    // aplikace lokalizace na komponenty formulare (button, label, menu, ...
                    ApplyResourceControl(form);
                    ApplyResourceMenu(form);

                    resman.ReleaseAllResources();


                    System.Diagnostics.Debug.WriteLine("LocalizationExtensionForm Globals.LokalizacePovolit " + sw.ElapsedMilliseconds);
                    //Logging.Trace2.Write("Stop", "Globals.LokalizacePovolit", id);

                }

                

                // povolena vlastni lokalizace ...
                if (Globals.LokalizaceVlastniPovolit)
                {
                    #region prelozena s .resource souboru  
                    resman_resource = new ComponentResourceManager_resource(form.GetType(), Globals.LocalizationDir);

                    ApplyResourceControl_Resouce(form);
                    ApplyResourceMenu_resource(form);

                    resman_resource.ReleaseAllResources();

                    #endregion

                    #region z .resx souboru 
                    
                    resman_resX = new ComponentResourceManager_ResX(form.GetType());

                    List<string> filenames = new List<string>();
                    // najiti specifickych stringu podle zakaznika ... 
                    // postup nacitani lokalizace:
                    // 1) Fask.MST_W.Forms.LoginForm.sk.FASK.resx
                    // 2) Fask.MST_W.Forms.LoginForm.sk.resx
                    // 3) Fask.MST_W.Forms.LoginForm.resx
                    // 4) Lokalizace nastavena primo v formulari
                    filenames.AddRange(System.IO.Directory.GetFiles(Globals.LocalizationDir, form.GetType().Namespace + "." + form.Name + "." + Globals.LokalizaceZvolena.ToString() + ".*.resx").ToList());
                    string filename = form.GetType().Namespace + "." + form.Name + "." + Globals.LokalizaceZvolena.ToString() + ".resx";
                    filenames.Add(filename);
                    filename = form.GetType().Namespace + "." + form.Name + ".resx";
                    filenames.Add(filename);

                    resman_resX.filenames = filenames;


                    ApplyResourceControl_ResX(form);
                    ApplyResourceMenu_ResX(form);

                    resman_resX.ReleaseAllResources();



                    #endregion


                    #region Puvodna lokalizace
                    //Logging.Trace2.Write("Start", "Globals.LokalizaceVlastniPovolit", id);




                    //#region Lokalizace MyComponentManager2

                    ////// projiti vsech nalezenych zaznamu
                    ////foreach (string key in localizationData.Keys)
                    ////{
                    ////    object value = GetValue(key);
                    ////    //if (!string.IsNullOrEmpty(value))
                    ////    if (value != null)
                    ////    {
                    ////        // projiti vsech textboxu, labelu, checkboxu, ...
                    ////        MyApplyResourceControl(form, key, value);
                    ////        // projiti Menu itemu
                    ////        //ApplyResourceMenu(form, key, value);
                    ////    }
                    ////}
                    ////Logging.Trace2.Write("Middle-Start", "ApplyResourceControl_ResX", id);
                    //ApplyResourceControl_ResX(form);
                    //System.Diagnostics.Debug.WriteLine("LocalizationExtensionForm ApplyResourceControl_ResX " + sw.ElapsedMilliseconds);
                    ////Logging.Trace2.Write("Middle-Stop", "ApplyResourceControl_ResX", id);
                    ////Logging.Trace2.Write("Middle-Start", "ApplyResourceMenu_ResX", id);
                    //ApplyResourceMenu_ResX(form);
                    //System.Diagnostics.Debug.WriteLine("LocalizationExtensionForm ApplyResourceMenu_ResX " + sw.ElapsedMilliseconds);
                    ////Logging.Trace2.Write("Middle-Stop", "ApplyResourceMenu_ResX", id);

                    ////Logging.Trace2.Write("Middle-Start", "Myresman.ReleaseAllResources", id);
                    //Myresman.ReleaseAllResources();
                    //System.Diagnostics.Debug.WriteLine("LocalizationExtensionForm ReleaseAllResources " + sw.ElapsedMilliseconds);
                    ////Logging.Trace2.Write("Middle-Stop", "Myresman.ReleaseAllResources", id);

                    //#endregion

                    ////Logging.Trace2.Write("Stop", "Globals.LokalizaceVlastniPovolit", id);

                    ////localizationData.Clear();
                    #endregion
                }

                //Logging.Trace2.Write("Stop", "Konec Lokalizace", id);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "LocalizationExtensionForm.Localize");
            }
        }




        //private static object GetValue(string key)// , string defalutValue)
        //{
        //    try
        //    {
        //        return localizationData[key];
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}


        /// <summary>
        /// Aplikuje resource ze slozky Lokalizace do labelu, textboxu, checkboxu, ... (!! neumi lokalizovat menu, protoze menu neni Control !!)
        /// </summary>
        /// <param name="c"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        //private static void ApplyResourceControl(Control c, string key, string value)
        //{
        //    ////////////////////////////////////////////
        //    ///Typy z .resx souboru ktere sa prideluju//
        //    ////////////////////////////////////////////
        //    //OK/.Text : String [type] : jedna sa o string
        //    //OK/.Font : System.Drawing.Font [class] : jedna sa o tridu Font
        //    //-/.AutoScroll, .Localizable, .Skin,  : System.Boolean [type]: jedna se o true a false hodnoty
        //    //-/.PopisTextAlign ,.DataTextAlign   : System.Drawing.ContentAlignment [enum]: jedna se o polohu textu
        //    //OK/.Location : System.Drawing.Point [class] : jedna se o polohu prvku fotmat X; Y
        //    //OK/.ClientSize, .Size : System.Drawing.Size [class]: Jedna se o velkost prvku fotmat X; Y
        //    ///.TabIndex, .DataMaxLength, .PopisWidth,  : System.Int32 [type]: jedna o cislo int
        //    //OK/.Anchor : System.Windows.Forms.AnchorStyles [enum]: ke ktere strane bude prilepeny prvek


        //    // v resx souboru je vzdy komponenta ve formatu: 'Name.Text'
        //    if ((c.Name + ".Text") == key)
        //        c.Text = value;

        //    //// v resx souboru je vzdy komponenta ve formatu: 'Name.Font'
        //    //if ((c.Name + ".Font") == key)
        //    //{
        //    //    Font f = value.String2Font();
        //    //    if(f != null )
        //    //        c.Font = f;
                

        //    //    //Type typ = Type.GetType("System.Int32");
        //    //    //TypeCode.
        //    //    //System.Int32 i;
        //    //    //i.GetTypeCode();
        //    //    string t = string.Empty; //"Syste.Drawing.Font"
        //    //    if (t.Equals("System.Drawing.Point"))
        //    //    {
        //    //        System.Drawing.Point p = new Point();

        //    //    }
                    
        //    //}

        //    //System.Drawing.ContentAlignment
                

        //    //// v resx souboru je vzdy komponenta ve formatu: 'Name.TabIndex'
        //    //if ((c.Name + ".TabIndex") == key)
        //    //{
        //    //    Font f = value.String2Font();
        //    //    if (f != null)
        //    //        c.Font = f;
        //    //}
        //    //    c.TabIndex = Int32.Parse(value);

        //    //// v resx souboru je vzdy komponenta ve formatu: 'Name.Size'
        //    //if ((c.Name + ".Size") == key)
        //    //{
        //    //    Font f = value.String2Font();
        //    //    if (f != null)
        //    //        c.Font = f;
        //    //}
        //    //    c.Size =(System.Drawing.Size)value.String2Size();

        //    //// v resx souboru je vzdy komponenta ve formatu: 'Name.ClientSize'
        //    //if ((c.Name + ".ClientSize") == key)
        //    //{
        //    //    Size? s = value.String2Size();
        //    //    if (s != null)
        //    //        c.ClientSize = (Size)s;
        //    //}

        //    //// v resx souboru je vzdy komponenta ve formatu: 'Name.Location'
        //    //if ((c.Name + ".Location") == key)
        //    //{
        //    //    Point? p = value.String2Point();
        //    //    if (p!= null)
        //    //        c.Location = (Point)p;
        //    //}
                

        //    //// v resx souboru je vzdy komponenta ve formatu: 'Name.Anchor'
        //    //if ((c.Name + ".Anchor") == key)
        //    //    c.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), value.Trim(), true);

            


        //    // bralo by to data opet z puvodniho resource
        //    //resman.ApplyResources(c, c.Name, Fask.MST_W.Localization.Localization.Culture);
        //    foreach (Control k in c.Controls)
        //    {
        //        ApplyResourceControl(k, key, value);
        //    }
        //}


        /// <summary>
        /// Aplikuje resource ze slozky Lokalizace do labelu, textboxu, checkboxu, ... (!! neumi lokalizovat menu, protoze menu neni Control !!)
        /// </summary>
        /// <param name="c"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        //private static void MyApplyResourceControl(Control c, string key, object value)
        //{
        //    // bralo by to data opet z puvodniho resource
        //    Myresman.ApplyResources(c, c.Name, System.Globalization.CultureInfo.InvariantCulture);
        //    foreach (Control k in c.Controls)
        //    {
        //        MyApplyResourceControl(k, key, value);
        //    }
        //}


        private static void ApplyResourceControl_ResX(Control c)
        {
            // bralo by to data opet z puvodniho resource
            resman_resX.ApplyResources(c, c.Name, Localization.Culture);
            foreach (Control k in c.Controls)
            {
                ApplyResourceControl_ResX(k);
            }
        }


        /// <summary>
        /// Aplikuje resource z formulare podle zvolene lokalizace.
        /// </summary>
        /// <param name="c"></param>
        private static void ApplyResourceControl(Control c)
        {
            //resman.ApplyResources(c, c.Name, ci);
            resman.ApplyResources(c, c.Name, Localization.Culture);
            //resman_resource.ApplyResources(c, c.Name, Localization.Culture);
            foreach (Control k in c.Controls)
            {
                ApplyResourceControl(k);
            }
        }

        /// <summary>
        /// Aplikuje resource z formulare podle zvolene lokalizace.
        /// </summary>
        /// <param name="c"></param>
        private static void ApplyResourceControl_Resouce(Control c)
        {
            //resman.ApplyResources(c, c.Name, ci);
            resman_resource.ApplyResources(c, c.Name, Localization.Culture);
            foreach (Control k in c.Controls)
            {
                ApplyResourceControl_Resouce(k);
            }
        }




        /// <summary>
        /// Aplikace lokalizace formulare do menu.
        /// </summary>
        /// <param name="form"></param>
        private static void ApplyResourceMenu(Form form)
        {
            foreach (FieldInfo fi in form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (fi.FieldType.Name == "MenuItem")
                {
                    var mi = (MenuItem)fi.GetValue(form);
                    resman.ApplyResources(mi, fi.Name, Localization.Culture);
                    //mi.Text = Translate(fi.ReflectedType.Namespace,
                    //    fi.ReflectedType.Name + '.' + fi.Name, fi.FieldType.Name, mi.Text);
                }
            }
        }

        /// <summary>
        /// Aplikace lokalizace formulare do menu.
        /// </summary>
        /// <param name="form"></param>
        private static void ApplyResourceMenu_resource(Form form)
        {
            foreach (FieldInfo fi in form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (fi.FieldType.Name == "MenuItem")
                {
                    var mi = (MenuItem)fi.GetValue(form);
                    resman_resource.ApplyResources(mi, fi.Name, Localization.Culture);
                    //mi.Text = Translate(fi.ReflectedType.Namespace,
                    //    fi.ReflectedType.Name + '.' + fi.Name, fi.FieldType.Name, mi.Text);
                }
            }
        }

        private static void ApplyResourceMenu_ResX(Form form)
        {
            foreach (FieldInfo fi in form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (fi.FieldType.Name == "MenuItem")
                {
                    var mi = (MenuItem)fi.GetValue(form);
                    resman_resX.ApplyResources(mi, fi.Name, Localization.Culture);
                    //mi.Text = Translate(fi.ReflectedType.Namespace,
                    //    fi.ReflectedType.Name + '.' + fi.Name, fi.FieldType.Name, mi.Text);
                }
            }
        }

        //private static void ApplyResourceMenu(Form form, string key, string value)
        //{
        //    foreach (FieldInfo fi in form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        //    {
        //        if (fi.FieldType.Name == "MenuItem")
        //        {
        //            var mi = (MenuItem)fi.GetValue(form);

        //            // v resx souboru je vzdy komponenta ve formatu: 'Name.Text'
        //            if ((fi.Name + ".Text") == key)
        //                mi.Text = value;

        //            //resman.ApplyResources(mi, fi.Name, Fask.MST_W.Localization.Localization.Culture);
        //            //mi.Text = Translate(fi.ReflectedType.Namespace,
        //            //    fi.ReflectedType.Name + '.' + fi.Name, fi.FieldType.Name, mi.Text);
        //        }
        //    }
        //}
    }
}
