using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestovaciProjekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintData();
        }



        private void PrintData() 
        {

            PrintReportLibrary.DataSets.DS_Soupis ds = new PrintReportLibrary.DataSets.DS_Soupis();


            for (int i = 0; i < 100; i++)
            {
                PrintReportLibrary.DataSets.DS_Soupis.PolozkyRow row = ds.Polozky.NewPolozkyRow();

                row.ITEMDESC = i.ToString();
                row.ITEMNMBR = i.ToString();
                row.QTYSHPPD = "1";
                row.ITEMCODE = string.Empty;
                row.VNDITNUM = string.Empty;
                row.CZ_CarKod = string.Empty;
                row.QTYPACK = "0";
                row.QTYSHPPDMJ = "0";
                row.MJ = string.Empty;
                //row.QTY = item.QTY;
                row.NMBRPAL = string.Empty;
                row.SERLTNUM = string.Empty;

                ds.Polozky.AddPolozkyRow(row);
            }


            PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog();

            //PrintReportLibrary.PrintReport plr = new PrintReportLibrary.PrintReport();
            plr.CountEntries = "123456";
            plr.DS_Soupis = ds;

            List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
            tmplist.Add(PrintReportLibrary.TypeData.DS_Soupis_Polozky);

            plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
            plr.PrinterName = "HP LaserJet 2430 PCL6 Class Driver";
            plr.Typereport = PrintReportLibrary.TypeReport.DoPradelny_OK;
            plr.ShowPreview = true;
            plr.Print();
        }
    }
}
