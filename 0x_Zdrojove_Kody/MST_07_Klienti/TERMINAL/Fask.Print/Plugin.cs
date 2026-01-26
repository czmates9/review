using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Fask.MST_W.Forms;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Net;

namespace Fask.Print
{
    public class Plugin : MST_Interfaces.IPluginBase  
    {
        public static MST_Interfaces.IApplicationBase appBase;
        private static Fask.Print.Tisk.Tisk tisknuti = null;

        public static void Pracuj(string[] SN, Fask.MST_W.TiskData parametry, string id_terminal)
        {
            Fask.Print.Tisk.TiskData data = new Fask.Print.Tisk.TiskData();
            data.CountEntries = parametry.CountEntries;
            data.CZ_CarKod = parametry.CZ_CarKod;
            data.DAT_VYROBY = parametry.DAT_VYROBY;
            data.DATEDONE = parametry.DATEDONE;
            data.ITEMNMBR = parametry.ITEMNMBR;
            data.KOD_SW = parametry.KOD_SW;
            data.LOCNCODE = parametry.LOCNCODE;
            data.ORD = parametry.ORD;
            data.PONUMBER = parametry.PONUMBER;
            data.QTYPACK = parametry.QTYPACK;
            data.QTYSHPPD = parametry.QTYSHPPD;
            data.REZ_1 = parametry.REZ_1;
            data.REZ_2 = parametry.REZ_2;
            data.SERLTNUM = parametry.SERLTNUM;
            data.TIMEDONE = parametry.TIMEDONE;
            data.VNDDOCNM = parametry.VNDDOCNM;
            data.VNDITNUM = parametry.VNDITNUM;

            lock (SN)
            {
                lock (parametry)
                {
                    try
                    {
                        tisknuti.Terminal_Vytiskni(SN, data, Convert.ToInt32(id_terminal));
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, "Plugin.Print", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    }
                }
            }
        }

        #region IPluginBase Members

        public Plugin()
        {
            tisknuti = new Fask.Print.Tisk.Tisk();
            tisknuti.Url = Fask.MST_W.MST_Global.PrintServerAddress + "Tisk.asmx";
        }

        public bool Tiskni(string[] SN, Fask.MST_W.TiskData parametry, string id_terminal)
        {
            bool succed = false;
            try
            {
                Thread t = new Thread(delegate() { Pracuj(SN, parametry, id_terminal); });
                t.Start();

                succed = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return succed;
        }

        public string Titulek
        {
            get
            {
                return "Tiskne vlastni SN";
            }
        }

        public void Load(MST_Interfaces.IApplicationBase app)
        {
            appBase = app;
        }

        public string Nazev
        {
            get
            {
                return "Plugin Tisk";
            }
        }

        #endregion
    }
}
