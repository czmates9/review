using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;

namespace Fask.Module.Print.Hanibal.Classes
{
    #region Puvodny kod
    
//    internal sealed class HanibalEAN_Settings : ApplicationSettingsBase
//    {
//        // Fields
//        private static HanibalEAN_Settings defaultInstance;

//        // Methods
//        static HanibalEAN_Settings()
//        {
//            defaultInstance = (HanibalEAN_Settings)SettingsBase.Synchronized(new HanibalEAN_Settings());
//        }



//        public HanibalEAN_Settings() { }
//        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e) { }
//        private void SettingsSavingEventHandler(object sender, CancelEventArgs e) { }

//        // Properties
//        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("doporučen\x00e1 cena")]
//        public string CenovkaCena1
//        {
//            get
//            {
//                return (string)this["CenovkaCena1"];
//            }
//            set
//            {
//                this["CenovkaCena1"] = value;
//            }
//        }
//        [DefaultSettingValue("cena po slevě"), UserScopedSetting, DebuggerNonUserCode]
//        public string CenovkaCena2
//        {
//            get
//            {
//                return (string)this["CenovkaCena2"];
//            }
//            set
//            {
//                this["CenovkaCena2"] = value;
//            }
//        }
//        [DefaultSettingValue("cena Hanibal"), UserScopedSetting, DebuggerNonUserCode]
//        public string CenovkaCena3
//{
//    get
//    {
//        return (string) this["CenovkaCena3"];
//    }
//    set
//    {
//        this["CenovkaCena3"] = value;
//    }
//}
 
//        [DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
//        public int CenovkaCenyBox
//        {
//            get
//            {
//                return (int)this["CenovkaCenyBox"];
//            }
//            set
//            {
//                this["CenovkaCenyBox"] = value;
//            }
//        }




//        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0")]
//        public int CenovkaCenySize
//        {
//            get
//            {
//                return (int)this["CenovkaCenySize"];
//            }
//            set
//            {
//                this["CenovkaCenySize"] = value;
//            }
//        }




//        [DefaultSettingValue("0"), DebuggerNonUserCode, UserScopedSetting]
//        public int CenovkaCenySpace
//        {
//            get
//            {
//                return (int)this["CenovkaCenySpace"];
//            }
//            set
//            {
//                this["CenovkaCenySpace"] = value;
//            }
//        }




//        [UserScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
//        public int CenovkaCTextBox
//        {
//            get
//            {
//                return (int)this["CenovkaCTextBox"];
//            }
//            set
//            {
//                this["CenovkaCTextBox"] = value;
//            }
//        }




//        [DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
//        public int CenovkaCTextSize
//        {
//            get
//            {
//                return (int)this["CenovkaCTextSize"];
//            }
//            set
//            {
//                this["CenovkaCTextSize"] = value;
//            }
//        }


//        [UserScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
//        public int CenovkaCTextSpace
//        {
//            get
//            {
//                return (int)this["CenovkaCTextSpace"];
//            }
//            set
//            {
//                this["CenovkaCTextSpace"] = value;
//            }
//        }




//        [UserScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
//        public int CenovkaEANBox
//        {
//            get
//            {
//                return (int)this["CenovkaEANBox"];
//            }
//            set
//            {
//                this["CenovkaEANBox"] = value;
//            }
//        }




//        [DebuggerNonUserCode, DefaultSettingValue("0"), UserScopedSetting]
//        public int CenovkaEANSize
//        {
//            get
//            {
//                return (int)this["CenovkaEANSize"];
//            }
//            set
//            {
//                this["CenovkaEANSize"] = value;
//            }
//        }




//        [DefaultSettingValue("0"), DebuggerNonUserCode, UserScopedSetting]
//        public int CenovkaEANSpace
//        {
//            get
//            {
//                return (int)this["CenovkaEANSpace"];
//            }
//            set
//            {
//                this["CenovkaEANSpace"] = value;
//            }
//        }




//        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0")]
//        public int CenovkaNazevBox
//        {
//            get
//            {
//                return (int)this["CenovkaNazevBox"];
//            }
//            set
//            {
//                this["CenovkaNazevBox"] = value;
//            }
//        }




//        [DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
//        public int CenovkaNazevSize
//        {
//            get
//            {
//                return (int)this["CenovkaNazevSize"];
//            }
//            set
//            {
//                this["CenovkaNazevSize"] = value;
//            }
//        }




//        [UserScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
//        public int CenovkaNazevSpace
//        {
//            get
//            {
//                return (int)this["CenovkaNazevSpace"];
//            }
//            set
//            {
//                this["CenovkaNazevSpace"] = value;
//            }
//        }




//        [DefaultSettingValue("250"), DebuggerNonUserCode, UserScopedSetting]
//        public int CenovkaWidth
//        {
//            get
//            {
//                return (int)this["CenovkaWidth"];
//            }
//            set
//            {
//                this["CenovkaWidth"] = value;
//            }
//        }




//        [DebuggerNonUserCode, DefaultSettingValue(""), UserScopedSetting]
//        public string DBName
//        {
//            get
//            {
//                return (string)this["DBName"];
//            }
//            set
//            {
//                this["DBName"] = value;
//            }
//        }




//        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("")]
//        public string DBPassword
//        {
//            get
//            {
//                return (string)this["DBPassword"];
//            }
//            set
//            {
//                this["DBPassword"] = value;
//            }
//        }




//        [UserScopedSetting, DefaultSettingValue(""), DebuggerNonUserCode]
//        public string DBServerName
//        {
//            get
//            {
//                return (string)this["DBServerName"];
//            }
//            set
//            {
//                this["DBServerName"] = value;
//            }
//        }




//        [DebuggerNonUserCode, DefaultSettingValue(""), UserScopedSetting]
//        public string DBUser
//        {
//            get
//            {
//                return (string)this["DBUser"];
//            }
//            set
//            {
//                this["DBUser"] = value;
//            }
//        }




//        public static HanibalEAN_Settings Default
//        {
//            get
//            {
//                return defaultInstance;
//            }
//        }
//        [UserScopedSetting, DefaultSettingValue("log"), DebuggerNonUserCode]
//        public string LogDirectory
//        {
//            get
//            {
//                return (string)this["LogDirectory"];
//            }
//            set
//            {
//                this["LogDirectory"] = value;
//            }
//        }




//        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
//        public string PohodaSklady
//        {
//            get
//            {
//                return (string)this["PohodaSklady"];
//            }
//            set
//            {
//                this["PohodaSklady"] = value;
//            }
//        }




//    }
    
    #endregion


    #region nova trida

    public static class HanibalEAN_Settings //: ApplicationSettingsBase
    {
        // Fields
        //private static HanibalEAN_Settings defaultInstance;

        // Methods
        //public static HanibalEAN_Settings()
        //{
        //    //defaultInstance = (HanibalEAN_Settings)SettingsBase.Synchronized(new HanibalEAN_Settings());
        //}



        //public HanibalEAN_Settings() { }
        //private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e) { }
        //private void SettingsSavingEventHandler(object sender, CancelEventArgs e) { }

        // Properties
        //[DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("doporučen\x00e1 cena")]


        //private static string _CenovkaCena1 = "doporučená cena";
        //public static string CenovkaCena1
        //{
        //    get
        //    {
        //        return _CenovkaCena1;// (string)this["CenovkaCena1"];
        //    }
        //    set
        //    {
        //       _CenovkaCena1 = value;
        //        //this["CenovkaCena1"] = value;
        //    }
        //}


        ////[DefaultSettingValue("cena po slevě"), UserScopedSetting, DebuggerNonUserCode]
        //private static string _CenovkaCena2 = "cena po slevě";
        //public static string CenovkaCena2
        //{
        //    get
        //    {
        //        return _CenovkaCena2;//(string)this["CenovkaCena2"];
        //    }
        //    set
        //    {
        //        _CenovkaCena2 = value;
        //        //this["CenovkaCena2"] = value;
        //    }
        //}
        
        
        ////[DefaultSettingValue("cena Hanibal"), UserScopedSetting, DebuggerNonUserCode]
        //private static string _CenovkaCena3 = "cena Hanibal";
        //public static string CenovkaCena3
        //{
        //    get
        //    {
        //        return _CenovkaCena3;  //(string)this["CenovkaCena3"];
        //    }
        //    set
        //    {
        //        _CenovkaCena3 = value;
        //        //this["CenovkaCena3"] = value;
        //    }
        //}

        //[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
        //public int CenovkaCenyBox
        //{
        //    get
        //    {
        //        return (int)this["CenovkaCenyBox"];
        //    }
        //    set
        //    {
        //        this["CenovkaCenyBox"] = value;
        //    }
        //}




        //[UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0")]
        //public int CenovkaCenySize
        //{
        //    get
        //    {
        //        return (int)this["CenovkaCenySize"];
        //    }
        //    set
        //    {
        //        this["CenovkaCenySize"] = value;
        //    }
        //}




        //[DefaultSettingValue("0"), DebuggerNonUserCode, UserScopedSetting]
        //public int CenovkaCenySpace
        //{
        //    get
        //    {
        //        return (int)this["CenovkaCenySpace"];
        //    }
        //    set
        //    {
        //        this["CenovkaCenySpace"] = value;
        //    }
        //}




        //[UserScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
        //public int CenovkaCTextBox
        //{
        //    get
        //    {
        //        return (int)this["CenovkaCTextBox"];
        //    }
        //    set
        //    {
        //        this["CenovkaCTextBox"] = value;
        //    }
        //}




        //[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
        //public int CenovkaCTextSize
        //{
        //    get
        //    {
        //        return (int)this["CenovkaCTextSize"];
        //    }
        //    set
        //    {
        //        this["CenovkaCTextSize"] = value;
        //    }
        //}


        //[UserScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
        //public int CenovkaCTextSpace
        //{
        //    get
        //    {
        //        return (int)this["CenovkaCTextSpace"];
        //    }
        //    set
        //    {
        //        this["CenovkaCTextSpace"] = value;
        //    }
        //}




        //[UserScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
        //public int CenovkaEANBox
        //{
        //    get
        //    {
        //        return (int)this["CenovkaEANBox"];
        //    }
        //    set
        //    {
        //        this["CenovkaEANBox"] = value;
        //    }
        //}




        //[DebuggerNonUserCode, DefaultSettingValue("0"), UserScopedSetting]
        //public int CenovkaEANSize
        //{
        //    get
        //    {
        //        return (int)this["CenovkaEANSize"];
        //    }
        //    set
        //    {
        //        this["CenovkaEANSize"] = value;
        //    }
        //}




        //[DefaultSettingValue("0"), DebuggerNonUserCode, UserScopedSetting]
        //public int CenovkaEANSpace
        //{
        //    get
        //    {
        //        return (int)this["CenovkaEANSpace"];
        //    }
        //    set
        //    {
        //        this["CenovkaEANSpace"] = value;
        //    }
        //}




        //[UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0")]
        //public int CenovkaNazevBox
        //{
        //    get
        //    {
        //        return (int)this["CenovkaNazevBox"];
        //    }
        //    set
        //    {
        //        this["CenovkaNazevBox"] = value;
        //    }
        //}




        //[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
        //public int CenovkaNazevSize
        //{
        //    get
        //    {
        //        return (int)this["CenovkaNazevSize"];
        //    }
        //    set
        //    {
        //        this["CenovkaNazevSize"] = value;
        //    }
        //}




        //[UserScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
        //public int CenovkaNazevSpace
        //{
        //    get
        //    {
        //        return (int)this["CenovkaNazevSpace"];
        //    }
        //    set
        //    {
        //        this["CenovkaNazevSpace"] = value;
        //    }
        //}




        //[DefaultSettingValue("250"), DebuggerNonUserCode, UserScopedSetting]
        //public int CenovkaWidth
        //{
        //    get
        //    {
        //        return (int)this["CenovkaWidth"];
        //    }
        //    set
        //    {
        //        this["CenovkaWidth"] = value;
        //    }
        //}




        //[DebuggerNonUserCode, DefaultSettingValue(""), UserScopedSetting]
        //public string DBName
        //{
        //    get
        //    {
        //        return (string)this["DBName"];
        //    }
        //    set
        //    {
        //        this["DBName"] = value;
        //    }
        //}




        //[DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("")]
        //public string DBPassword
        //{
        //    get
        //    {
        //        return (string)this["DBPassword"];
        //    }
        //    set
        //    {
        //        this["DBPassword"] = value;
        //    }
        //}




        //[UserScopedSetting, DefaultSettingValue(""), DebuggerNonUserCode]
        //public string DBServerName
        //{
        //    get
        //    {
        //        return (string)this["DBServerName"];
        //    }
        //    set
        //    {
        //        this["DBServerName"] = value;
        //    }
        //}




        //[DebuggerNonUserCode, DefaultSettingValue(""), UserScopedSetting]
        //public string DBUser
        //{
        //    get
        //    {
        //        return (string)this["DBUser"];
        //    }
        //    set
        //    {
        //        this["DBUser"] = value;
        //    }
        //}




        //public static HanibalEAN_Settings Default
        //{
        //    get
        //    {
        //        return defaultInstance;
        //    }
        //}
        //[UserScopedSetting, DefaultSettingValue("log"), DebuggerNonUserCode]
        //public string LogDirectory
        //{
        //    get
        //    {
        //        return (string)this["LogDirectory"];
        //    }
        //    set
        //    {
        //        this["LogDirectory"] = value;
        //    }
        //}




        //[UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        //public string PohodaSklady
        //{
        //    get
        //    {
        //        return (string)this["PohodaSklady"];
        //    }
        //    set
        //    {
        //        this["PohodaSklady"] = value;
        //    }
        //}




    }
    

    #endregion


}
