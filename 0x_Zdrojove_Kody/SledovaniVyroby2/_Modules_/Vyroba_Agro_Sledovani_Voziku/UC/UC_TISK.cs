
using Fask.Logging;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Fask.Constants;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.UC
{
    public partial class UC_TISK : UserControl
    {
        //private frmMain _parent;
        //private FASK_Events object_Fask_Events = new FASK_Events();


        public UC_TISK()
        {
            InitializeComponent();
        }

        private void SetText_Tisk(string txt)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetText_Tisk(txt);
                }));
                return;
            }

            TB.Text = txt;

        }


        private void BT_tisk_Click(object sender, EventArgs e)
        {
            #region zakomentovano - nepouziva se
            //try
            //{
            //    Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

            //    o.id = 1;
            //    o.loginid = "1";
            //    o.machineid = "1";
            //    o.dateeve = DateTime.Now;
            //    o.qty = 1;
            //    o.qtyReal = 1;
            //    o.description = "tisk";
            //    o.barcodeReaded = "333";
            //    o.barcodeSended = "333";
            //    o.zakazka = "xx";
            //    o.faskGUID = Guid.NewGuid();
            //    o.reportType = "xx";
            //    o.isProcessed = DateTime.Now; //
            //    o.IDO = "xx";
            //    o.scan1 = "xx";
            //    o.scan2 = "xx";
            //    o.scan3 = "xx";
            //    o.sensor = "xx";
            //    o.material = "xx";
            //    o.VPH = "xx";
            //    o.VPPol = 1;
            //    o.EAN_IS = "xx";
            //    o.IS_ID = "xx";
            //    o.NMBRPAL = "00301234560000000026";
            //    //x.SetstatusNull();
            //    o.status = AGRO.status_31;
            //    o.productionGuid = Guid.NewGuid();
            //    o.popis = "xx";
            //    o.QTYPACK = 10;
            //    o.PackType = "UP";
            //    o.WEIGHT = 250;

            //    // API komunikace
            //    //FASK_Events o = new FASK_Events();
            //    IRestResponse restResponse;
            //    string param = "agrocs";
            //    string JSON = "";

            //   // Log.Write("TISK START:" + o.NMBRPAL);
            //    ExceptionHandler2.Handle("TISK START:" + o.NMBRPAL, "Log_LV", "txt");

            //    JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(o);

            //    if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON, "tisk"))
            //    {
            //        throw new Exception("Komunikace s TISK se nezdarila");
            //    }

            //    //if (restResponse.StatusCode == HttpStatusCode.OK)
            //    //{
            //    //    var content = Newtonsoft.Json.JsonConvert.DeserializeObject<FASK_Events>(restResponse.Content);
            //    //}

            //    // var content = Newtonsoft.Json.JsonConvert.DeserializeObject<FASK_Events>(restResponse.Content);
            //    if(restResponse.Content == null)
            //    {
            //        SetText_Tisk(restResponse.Content);
            //        //Log.Write("TISK: nic nevraceno");
            //        ExceptionHandler2.Handle("TISK: nic nevraceno", "Log_LV", "txt");
            //    }
            //    else
            //    {
            //        SetText_Tisk(restResponse.Content);
            //        //Log.Write("TISK: navraceno");
            //        ExceptionHandler2.Handle("TISK: navraceno", "Log_LV", "txt");
            //    }


            //}
            //catch (Exception ex)
            //{
            //   // Log.Write(ex);
            //    ExceptionHandler2.Handle(ex);

            //} 
            #endregion



        }
    }
}
