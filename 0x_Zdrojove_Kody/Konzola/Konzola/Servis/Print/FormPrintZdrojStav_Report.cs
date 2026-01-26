using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Konzola._Support_;

namespace Konzola.Servis.Print
{
    public partial class FormPrintZdrojStav_Report : Form
    {
        public FormPrintZdrojStav_Report()
        {
            InitializeComponent();
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow _ZdrojStavRow
        {
            set;
            private get;
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow _ZdrojRow
        {
            private get;
            set;
        }

        private void FormPrintZdrojStav_Load(object sender, EventArgs e)
        {

            this.reportViewer1.Reset();

            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;

            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReportZdroj.rdlc";
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "Konzola.Servis.Print.Templates.Report_ZdrojStav.rdlc";
            this.reportViewer1.LocalReport.ReportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Report_ZdrojStav.rdlc");

            Microsoft.Reporting.WinForms.LocalReport lrep = this.reportViewer1.LocalReport;

            {

                Servis dsServis = new Servis();

                dsServis.CZMST_Servis_ZdrojStav.ImportRow(_ZdrojStavRow);
                dsServis.CZMST_Servis_ZdrojStav[0].ZdrojBarcode = _ZdrojRow.Barcode;
                dsServis.CZMST_Servis_ZdrojStav[0].ZdrojBarcodeImage = _Support_.Barcodes.GetBarcodeImage(_ZdrojRow.Barcode, ZXing.BarcodeFormat.CODE_128);
                
                var dsserviszdroj = new Microsoft.Reporting.WinForms.ReportDataSource();
                dsserviszdroj.Name = "Report_CZMST_Servis_ZdrojStav";
                dsserviszdroj.Value = dsServis.CZMST_Servis_ZdrojStav;

                lrep.DataSources.Add(dsserviszdroj);
            }

            this.reportViewer1.RefreshReport();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.PrintDialog();

                //Microsoft.Reporting.WinForms.LocalReport report = this.reportViewer1.LocalReport;
                //report.PrintToPrinter();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void asdfToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.PrintToPrinter();
        }

    }
}
