using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Prodej_3
{
    public partial class ProdejSumar : System.Windows.Forms.Form, System.IDisposable
    {

        public string NMBRPAL { get; set; }

        public ProdejSumar()
        {
            InitializeComponent();
        }


        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }


        private void ProdejSumar_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(NMBRPAL))
                // 1) je cislo palety? => jen tuto paletu
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Fill_SOUHRNByNMBRPAL(this.prodej.CZMST_SOUHRN, NMBRPAL.Trim());
            else
                // 2) neni cislo palety => vse ... 
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Fill_SOUHRN(this.prodej.CZMST_SOUHRN);
            
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            MyInitializeGrid();

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            try
            {
                sbPal.Text = "Pal.: " + (NMBRPAL ?? string.Empty);
            }
            catch //(Exception ex)
            {
            }
            
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(System.IO.Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void PerformCancel()
        {
            this.dataGrid1.Save(System.IO.Path.Combine(Main.ConfigDir, this.GetType().ToString()));
            this.DialogResult = DialogResult.Cancel;
        }

        private void ProdejSumar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) 
            {
                this.PerformCancel();
            }

            e.Handled = true;
        }


    }
}