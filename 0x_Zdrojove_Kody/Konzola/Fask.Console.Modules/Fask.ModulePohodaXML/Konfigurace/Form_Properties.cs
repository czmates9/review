using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fask.ModulePohodaXML
{
    public partial class Form_Properties : Form
    {
        public string Tabulka;

        public Form_Properties()
        {
            InitializeComponent();
        }

        private void Form_Properties_Load(object sender, EventArgs e)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                #region old

                #region Test 1
                //Dictionary<string, Dictionary<string, string>> All = new Dictionary<string, Dictionary<string, string>>();

                //foreach (DataTable item in Globals_V1.Konfigurace.Tables)
                //{
                //    Dictionary<string, string> DicTabulka = new Dictionary<string, string>();

                //    foreach (DataColumn Col in item.Columns)
                //    {
                //        string val = item.Rows[0][Col.ColumnName].ToString();
                //        DicTabulka.Add(Col.ColumnName, val);
                //    }

                //    All.Add(item.TableName, DicTabulka);
                //}

                //propertyGrid1.SelectedObjects = All.Values.ToArray(); 
                #endregion

                #region Test 2

                //Dictionary<string, string> DicTabulka = new Dictionary<string, string>();

                //foreach (DataColumn Col in Globals_V1.Konfigurace.PohodaInfo.Columns)
                //{
                //    string val = Globals_V1.Konfigurace.PohodaInfo[0][Col.ColumnName].ToString();
                //    DicTabulka.Add(Col.ColumnName, val);
                //}

                //propertyGrid1.SelectedObject = DicTabulka;

                #endregion

                #region Test 3, Example, Funguje

                //IDictionary d = new Hashtable();
                //d["Hello"] = "World";
                //d["Meaning"] = 42;
                //d["Shade"] = Color.ForestGreen;

                //propertyGrid1.SelectedObject = new DictionaryPropertyGridAdapter(d);

                #endregion

                #region Test 4, Funguje pro 1 tabulku

                //Dictionary<string, string> DicTabulka = new Dictionary<string, string>();

                //foreach (DataColumn Col in Globals_V1.Konfigurace.PohodaInfo.Columns)
                //{
                //    string val = Globals_V1.Konfigurace.PohodaInfo[0][Col.ColumnName].ToString();
                //    DicTabulka.Add(Col.ColumnName, val);
                //}

                //propertyGrid1.SelectedObject = new DictionaryPropertyGridAdapter(DicTabulka);

                #endregion

                #region Test 5, Nefunguje

                //Dictionary<string, Dictionary<string, string>> All = new Dictionary<string, Dictionary<string, string>>();

                //foreach (DataTable item in Globals_V1.Konfigurace.Tables)
                //{
                //    Dictionary<string, string> DicTabulka = new Dictionary<string, string>();

                //    foreach (DataColumn Col in item.Columns)
                //    {
                //        string val = item.Rows[0][Col.ColumnName].ToString();
                //        DicTabulka.Add(Col.ColumnName, val);
                //    }

                //    All.Add(item.TableName, DicTabulka);
                //}

                //propertyGrid1.SelectedObject = new DictionaryPropertyGridAdapter(All);

                #endregion

                #region TEST 6 , nevim...

                //FASK_Dictionery fASK_Dictionery = new FASK_Dictionery();

                ////foreach (DataColumn Col in Globals_V1.Konfigurace.PohodaInfo.Columns)
                ////{
                ////    string val = Globals_V1.Konfigurace.PohodaInfo[0][Col.ColumnName].ToString();
                ////    fASK_Dictionery.PohodaInfo.Add(Col.ColumnName, val);
                ////}

                //foreach (DataColumn Col in Globals_V1.Konfigurace.ConnectionStrings.Columns)
                //{
                //    string val = Globals_V1.Konfigurace.ConnectionStrings[0][Col.ColumnName].ToString();
                //    fASK_Dictionery.ConnectionStrings.Add(Col.ColumnName, val);
                //}

                //propertyGrid1.SelectedObjects = fASK_Dictionery.L.ToArray();


                #endregion


                #endregion

                #region Test 1 Wrapper

                //List<RowWrapper> rowWrappers = new List<RowWrapper>();

                ////RowWrapper wrapper_cs = new RowWrapper(Globals_V1.Konfigurace.ConnectionStrings[0], "ConnectionStrings");
                //RowWrapper wrapper_com = new RowWrapper(Globals_V1.Konfigurace.ComunicatorDriveMapping[0], "ComunicatorDriveMapping");

                ////rowWrappers.Add(wrapper_cs);
                //rowWrappers.Add(wrapper_com);

                //propertyGrid1.SelectedObjects = rowWrappers.ToArray();


                #endregion

                #region Test 2 wrapper + typ tabulky

                RowWrapper wrapper = new RowWrapper(Globals_V1.Konfigurace.Tables[Tabulka].Rows[0], Tabulka);
                propertyGrid1.SelectedObject = wrapper;
                #endregion

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if(propertyGrid1.SelectedObject is RowWrapper)
            {
               RowWrapper rw = (RowWrapper)propertyGrid1.SelectedObject;

               DataRowView drw = rw.GetRowView();

                Globals_V1.Konfigurace.Tables[Tabulka].Rows[0].Delete();
                Globals_V1.Konfigurace.Tables[Tabulka].Rows.Add(drw.Row);

                Globals_V1.SaveConfiguration();
            }

            DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
