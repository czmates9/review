using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Konzola.Servis.Print
{
    public partial class FormPrintZdroj : Form
    {
        public FormPrintZdroj()
        {
            InitializeComponent();
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojDataTable CZMST_Servis_ZdrojDataTable
        {
            set;
            private get;
        }

        private void FormPrintZdroj_Load(object sender, EventArgs e)
        {

            this.reportViewer1.Reset();

            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;

            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReportZdroj.rdlc";
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "Konzola.Servis.Print.Templates.Report_Zdroj.rdlc";
            this.reportViewer1.LocalReport.ReportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Report_Zdroj.rdlc");

            Microsoft.Reporting.WinForms.LocalReport lrep = this.reportViewer1.LocalReport;

            {

                Servis dsServis = new Servis();

                foreach (var i in CZMST_Servis_ZdrojDataTable)
                {
                    var sz = dsServis.CZMST_Servis_Zdroj.NewCZMST_Servis_ZdrojRow();
                    {
                        sz.ID = i.ID;
                        sz.Oznaceni = i.Oznaceni;
                        if (!i.IsBarcodeNull())
                        {
                            sz.Barcode = i.Barcode;
                            sz.BarcodeImage = _Support_.Barcodes.GetBarcodeImage(i.Barcode, ZXing.BarcodeFormat.CODE_128);
                        }
                        if (!i.IsTypeNull())
                            sz.Type = i.Type;
                    }
                    dsServis.CZMST_Servis_Zdroj.AddCZMST_Servis_ZdrojRow(sz);
                } 
                
                var dsserviszdroj = new Microsoft.Reporting.WinForms.ReportDataSource();
                dsserviszdroj.Name = "Report_Servis_Zdroj";
                dsserviszdroj.Value = dsServis.CZMST_Servis_Zdroj;

                lrep.DataSources.Add(dsserviszdroj);
            }

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
