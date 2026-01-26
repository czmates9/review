using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Konzola.Extensions;

namespace Konzola.Terminal
{

    public partial class TestDisp : Form
    {
        public TestDisp()
        {
            InitializeComponent();
            this.dgData.UpdateColumnHeaderCellsByDatasource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Konzola.Terminal.Test_Disp.DataSet_Disp.DisponibilityVariantyDataTable dt = new Test_Disp.DataSet_Disp.DisponibilityVariantyDataTable();
                Konzola.Terminal.Test_Disp.DataSet_DispTableAdapters.DisponibilityVariantyTableAdapter ta = new Test_Disp.DataSet_DispTableAdapters.DisponibilityVariantyTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(@"Data Source=faskcz-cv004\sql_2014;Initial Catalog=Test_TaD_Disponibilita;User ID=sa;Password=sasa");

                ta.Fill(dt);
                Kontrola(dt);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Kontrola(Konzola.Terminal.Test_Disp.DataSet_Disp.DisponibilityVariantyDataTable dt)
        {
            Fask.POHODA.Disponibility.ValidateData dsDisp = new Fask.POHODA.Disponibility.ValidateData();

            //int Count = 1;

            foreach (Konzola.Terminal.Test_Disp.DataSet_Disp.DisponibilityVariantyRow item in dt)
            {
                Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

                Row.ITEMNMBR = item.ITEMNMBR.ToString();
                Row.SKL_ID = "1";
                Row.QTY = item.QTY;
                Row.SetSOPNUMBENull();
                Row.SetORDNull();
                Row.SKz_Rezer = item.SKz_Rezer;
                Row.SKz_StavZ = item.SKzStavZ;

                if (item.IsOBJ_RezerNull())
                {
                    Row.SetOBJ_RezerNull();
                    Row.SetOBJPol_QTY_ZbyvaDodatCelkemNull();
                    Row.SetOBJPol_QTY_ZbyvaDodatPolozkaNull();
                }
                else
                {
                    Row.OBJ_Rezer = true;
                    Row.OBJPol_QTY_ZbyvaDodatCelkem = item.OBJ_Rezer;
                    Row.OBJPol_QTY_ZbyvaDodatPolozka = item.OBJ_Rezer;
                }

                //Count++;

                dsDisp.DataDisp.AddDataDispRow(Row);
            }

            dsDisp.DataDisp.AcceptChanges();

            Fask.POHODA.Disponibility.CheckDisp dispClass = new Fask.POHODA.Disponibility.CheckDisp();


            //var info = dispClass.KontrolaDisponibilityDT(dsDisp, @"Provider=SQLNCLI11;Data Source=faskcz-cv004\sql_2014;Initial Catalog=StwPh_12345678_2019;User ID=sa;Password=sasa");
            var tmp = dispClass.ValidateDataFromPohodaV2(dsDisp);
            var info = tmp.DTVydej;

            var columnsprepoklad = info.Columns.Add("Predpoklad",typeof(string));
            var columnsvysledek  = info.Columns.Add("Vysledek", typeof(string));
            var columnszbude = info.Columns.Add("ZbudeAnoNe", typeof(string));


            Dictionary<string, Stav> List = new Dictionary<string, Stav>();

           

            foreach (var item in info)
            {
                // TODO: TaD + JiS moznost projit cely datatable a zalogovat všechny chyby, ale na terminal poslat pouze prvni?, časem cely seznam...

                string Vysledek = "NE";

                //OK
                if (
                    (item.STAV_SKLAD >= 0)
                    &&
                    (item.ZBUDE >= 0)
                    &&
                    (item.STAV_SKLAD >= item.ZBUDE)
                   )
                {
                    Vysledek = "ANO";
                }

                #region  NEEE
                //OK
                //if (item.ZBUDE >= 0)
                //{
                //    Vysledek = "ANO";
                //}

                ////NE
                //if (item.STAV_SKLAD >= item.ZBUDE)
                //{
                //    Vysledek = "ANO";
                //}

                ////NE
                //if (item.STAV_SKLAD >= item.QTYSHPPD)
                //{
                //    Vysledek = "ANO";
                //}

                //NE
                //if (item.STAV_SKLAD > 0)
                //{
                //    Vysledek = "ANO";
                //}
                
                //NE puvodni test
                //string Vysledek = "ANO";
                //if (item.IsREZ_JANull() || string.IsNullOrEmpty(item.REZ_JA))
                //{
                //    //v tomto pripade se jedna o NErezervovanou polozku
                //    if (item.ZBUDE < 0)
                //    {

                //        Vysledek = "NE";
                //        //return new InfoValidace("ERR", item);
                //    }
                //}
                //else
                //{
                //    //v tomto připade je to rezervovana polozka
                //    if (item.QTYSHPPD > item.STAV_SKLAD)
                //    {
                //        Vysledek = "NE";
                //        //return new InfoValidace("ERR", item);
                //    }
                //} 
                #endregion

                EnumerableRowCollection<Konzola.Terminal.Test_Disp.DataSet_Disp.DisponibilityVariantyRow> pred = dt.Where(x => x.ITEMNMBR == int.Parse(item.ITEMNMBR)); 

                if((pred != null) && (pred.Count() > 0))
                {
                    item[columnsprepoklad] = pred.First().Predpoklad;
                    item[columnsvysledek] = Vysledek;
                    item[columnszbude] = item.ZBUDE < 0 ? "NE" : "ANO";
                    List.Add(item.ITEMNMBR, new Konzola.Terminal.Stav(pred.First().Predpoklad, Vysledek));
                    string predpoklad = pred.First().Predpoklad;
                    if (Vysledek != predpoklad)
                    {
                        //co to je? ukladam sam sebe do sebe
                        //predpoklad = predpoklad;
                    }
                }
            }

            dgData.DataSource = info;

            bool rozdil = false;
            foreach (string entry in List.Keys)
            {
                // do something with entry.Value or entry.Key
                Stav s = List[entry];
                if (s.GetRozdil())
                {
                    rozdil = true;
                    //MessageBox.Show("Rozdil nastav pro ITEMNMBR :'" + entry + "'");
                    System.Diagnostics.Debug.WriteLine("Rozdil nastal pro ITEMNMBR :'" + entry + "'");
                }
            }

            if (!rozdil)
            {
                System.Diagnostics.Debug.WriteLine("Vse vypada OK :)");
            }

            //if (info.ID != 0)
            //{
            //    throw new Exception(info.Description);
            //}
        }

        private void TestDisp_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            this.WindowState = FormWindowState.Maximized;

            this.dgData.LoadConfiguration(this.GetType().ToString());
            advancedDataGridViewSearchToolBar1.SetColumns(dgData.Columns);

        }

        private void TestDisp_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dgData.SaveConfiguration(this.GetType().ToString());
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgData.CurrentCell.ColumnIndex + 1 >= dgData.ColumnCount;
                bool endrow = dgData.CurrentCell.RowIndex + 1 >= dgData.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgData.CurrentCell.ColumnIndex;
                    startRow = dgData.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgData.CurrentCell.ColumnIndex + 1;
                    startRow = dgData.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgData.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgData.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgData.CurrentCell = c;

        }


    }

    public class Stav
    {
        public string Predpoklad;
        public string Vysledek;

        public Stav(string predpoklad, string vysledek)
        {
            this.Predpoklad = predpoklad;
            this.Vysledek = vysledek;
        }

        public bool GetRozdil()
        {
            if (Predpoklad.Trim() != Vysledek.Trim())
                return true;
            else
                return false;
        }
    }

}
