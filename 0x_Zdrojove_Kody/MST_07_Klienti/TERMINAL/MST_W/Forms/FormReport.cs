using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Forms
{
    public partial class FormReport : System.Windows.Forms.Form
    {

        //private DataSet _ds;
        //public DataSet ds 
        //{
        //    set { this._ds = value; }
        //    get{ return this._ds;}
        //}

        private decimal _naSklad;
        public decimal NaSklad 
        {
            set { this._naSklad = value; }
            get { return this._naSklad; }
        }

        private decimal _naExpedici;
        public decimal NaExpedici
        {
            set { this._naExpedici = value; }
            get { return this._naExpedici; }
        }

        #region DataNaZobrazeni

		public string CZ_CarKod = string.Empty;
		public string ITEMDESC = string.Empty;
		public string ITEMNMBR = string.Empty;

            public decimal MnozstviDodavatelePozadovano = 0;
            public decimal MnozstviDodavateleDodano = 0;
            public decimal MnozstviDodavateleDodat =0;
            public decimal MnozstviOdberateliPozadovano =0;
            public decimal MnozstviOdberatelumDodano =0;
            public decimal MnozstviOdberatelumDodat =0;
            public decimal Vysledek = 0;

        #endregion


		//private Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow _PERow;
		//public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow 
		//{
		//    set { this._PERow = value; }
		//    get{ return this._PERow;}
		//}


        public FormReport()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }        

        private void updateForm()
        {
            try
            {
                df_PozadovanoOdDodavatele.Data = "-";
                df_JizDodano.Data = "-";
                df_ZbyvaNaObjednavce.Data = "-";
                df_PozadovanoOdberately.Data = "-";
                //df_JizPrirazenoNaExpedici.Data = "-";
                //df_ZbyvaOdberatelum.Data = "-";

                df_CZ_CarKod.Data = "-";
                df_DESC.Data = "-";
                df_ITEMNMBR.Data = "-";

                df_Expedice.Data = "-";
                df_SKLAD.Data = "-";


                df_PozadovanoOdDodavatele.Data = this.MnozstviDodavatelePozadovano.ToString(Settings.UIFormatDesCisel);
                df_JizDodano.Data = this.MnozstviOdberatelumDodano.ToString(Settings.UIFormatDesCisel);
                df_ZbyvaNaObjednavce.Data = this.MnozstviOdberatelumDodat.ToString(Settings.UIFormatDesCisel);

                df_PozadovanoOdberately.Data = this.MnozstviOdberateliPozadovano.ToString(Settings.UIFormatDesCisel);
                //df_JizPrirazenoNaExpedici.Data = "";// dopocitat
                //df_ZbyvaOdberatelum.Data = this.MnozstviOdberatelumDodat.ToString(Settings.UIFormatDesCisel);



				//df_CZ_CarKod.Data = this._PERow.CZ_CarKod;
				//df_DESC.Data = this._PERow.IsITEMDESCNull() ? "-" : this._PERow.ITEMDESC;
				//df_ITEMNMBR.Data = this._PERow.IsITEMNMBRNull() ? "-" : this._PERow.ITEMNMBR;

				df_CZ_CarKod.Data = string.IsNullOrEmpty(this.CZ_CarKod) ? "-" : this.CZ_CarKod.Trim();
				df_DESC.Data = string.IsNullOrEmpty(this.ITEMDESC) ? "-" : this.ITEMDESC.Trim();
				df_ITEMNMBR.Data = string.IsNullOrEmpty(this.ITEMNMBR) ? "-" : this.ITEMNMBR.Trim();



                df_Expedice.Data = this._naExpedici.ToString(Settings.UIFormatDesCisel);
                df_SKLAD.Data = this._naSklad.ToString(Settings.UIFormatDesCisel);




            }
            catch //(Exception ex)
            {
                //MessageBoxBig.Show(ex.Message);
            }
        }




        private void ListNasnimForm_KeyDown(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformStorno();
            }
        }


        private void ListNasnimForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;


            updateForm();
        }


        private void finalize()
        {

        }

        private void PerformOK()
        {
            this.finalize();
            DialogResult = DialogResult.OK;
        }


        private void PerformStorno()
        {
            this.finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void FormReport_Resize(object sender, EventArgs e)
        {
            Size size = new Size(panelBTN.Width /2 , panelBTN.Height);
            buttonOK.Size = size;

        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformStorno();
        }

        private void panelSkladExport_Resize(object sender, EventArgs e)
        {
            Size size = new Size(panelSkladExport.Width / 2, panelSkladExport.Height);
            panelLeft.Size = size;
        }

        private void panelLeft_Resize(object sender, EventArgs e)
        {
            int height = panelSkladExport.Height/10;

            Size size_label = new Size(panelLeft.Width, height * 3);
            Size size_df = new Size(panelLeft.Width, height * 7);


            df_Expedice.Size = size_df;
            df_SKLAD.Size = size_df;
            label1.Size = size_label;
            label2.Size = size_label;
        }

        private void FormReport_Closing(object sender, CancelEventArgs e)
        {
            //writeLog("0", this.Name, "FormReport_Closing");
            //GC.Collect();
            //writeLog("1", this.Name, "FormReport_Closing");
            //GC.Collect();
            //writeLog("2", this.Name, "FormReport_Closing");
            //GC.WaitForPendingFinalizers();
            //writeLog("3", this.Name, "FormReport_Closing");
        }

    }
}