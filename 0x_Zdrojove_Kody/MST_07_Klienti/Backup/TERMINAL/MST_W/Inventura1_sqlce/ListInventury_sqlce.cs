using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Graphic;

namespace Fask.MST_W.Inventura1_sqlce
{
    public partial class ListInventury_sqlce : System.Windows.Forms.Form
    {
        private Inventura1Service.Inventury1 inventury = null;

        private Inventura1Service.Inventury1.HlavickyRow SelectedRow
        {
            get
            {
                try
                {
                    DataRowView dr = (DataRowView)hlavickyBindingSource.Current;
                    Inventura1Service.Inventury1.HlavickyRow irow = dr.Row as Inventura1Service.Inventury1.HlavickyRow;
                    return irow;
                }
                catch
                {
                    return null;
                }
            }
        }
 
        public int Davka
        {
            get
            {
                try
                {
                    Inventura1Service.Inventury1.HlavickyRow irow = SelectedRow;
                    if (irow != null)
                        return irow.CountEntries;
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public ListInventury_sqlce(Inventura1Service.Inventury1 inventury)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.inventury = inventury != null ? inventury : (new Fask.MST_W.Inventura1Service.Inventury1());
            this.panel1_Resize(null, null);
            InitializeDataGridView();
            MyInitializeGrid();
            //this.Synchronize();
        }

        public ListInventury_sqlce(string[] filenames)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.inventury = new Fask.MST_W.Inventura1Service.Inventury1();
            InitializeDataGridView();
            MyInitializeGrid();

            foreach (string filename in filenames)
            {
                //SqlCEDBs.DataSets.Inventura1TableAdapters.CZMST_I1HTableAdapter i1h_ta = null;
                //try
                //{
                //    int davka = Convert.ToInt32(System.IO.Path.GetFileNameWithoutExtension(filename));

                //    i1h_ta = new Fask.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I1HTableAdapter();
                //    i1h_ta.Connection.ConnectionString = "Data source=" + filename;

                //    Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1HDataTable i1h_dt = i1h_ta.GetData();
                //    if (i1h_dt.Count > 0)
                //        inventury.Hlavicky.AddHlavickyRow(i1h_dt[0].CountEntries, i1h_dt[0].IsDescriptionNull() ? "-" : i1h_dt[0].Description, i1h_dt[0].Status);
                //    else
                //        inventury.Hlavicky.AddHlavickyRow(davka, "-", 0);
                //}
                //catch { }
                //finally
                //{
                //    if (i1h_ta != null && i1h_ta.Connection != null && i1h_ta.Connection.State == ConnectionState.Open)
                //        i1h_ta.Connection.Close();
                //}
                using (var controller_i1_davka = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura1(filename))
                {
                    try
                    {
                        //var i1h_dt = controller_i1_davka.CZMST_I1H_GetData();
                        var i1h_dt = controller_i1_davka.GetData_I1H();
                        if (i1h_dt.Count > 0)
                            inventury.Hlavicky.AddHlavickyRow(i1h_dt[0].CountEntries, i1h_dt[0].IsDescriptionNull() ? "-" : i1h_dt[0].Description, i1h_dt[0].Status);
                        else
                            inventury.Hlavicky.AddHlavickyRow(
                                Convert.ToInt32(System.IO.Path.GetFileNameWithoutExtension(filename)), // cislo davky
                                "-",
                                0
                                );
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                    }
                }
            }
            this.panel1_Resize(null, null);
            //this.Synchronize();
        }

        //public void Synchronize()
        //{
        //    this.inventury.AcceptChanges();
        //    for (int i = this.inventury.Hlavicky.Count - 1; i >= 0; i--)
        //    {
        //        Inventura1Service.Inventury1.HlavickyRow hlavicka = this.inventury.Hlavicky[i];
        //        string file = Path.Combine(Main.DataDir, hlavicka.CountEntries.ToString() + "." + Main.Inventura1I1);
        //        if (File.Exists(file))
        //            this.inventury.Hlavicky.RemoveHlavickyRow(hlavicka);
        //    }
        //    this.inventury.AcceptChanges();
        //}

        private void ListInventury_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                DialogResult = DialogResult.Cancel;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                ProcessOK();
            }
        }

        private void ListInventury_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            updateForm();
            panel1_Resize(null, null);
        }

        private void updateForm()
        {
            hlavickyBindingSource.DataSource = inventury;
            hlavickyBindingSource.DataMember = inventury.Hlavicky.TableName;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ProcessOK();
        }

        private void ProcessOK()
        {
            if (SelectedRow != null)
                DialogResult = DialogResult.OK;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Width / 2, panel1.Height);
            graphicButtonOK.Size = nsize;
        }

        private void ListInventury_sqlce_Closing(object sender, CancelEventArgs e)
        {
            finalize();
        }

        private void finalize()
        {
            this.dgI1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void MyInitializeGrid()
        {
            this.dgI1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dgI1.Font = new Font(this.dgI1.Font.Name, Settings.UIGridFont, this.dgI1.Font.Style);
            this.dgI1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void InitializeDataGridView()
        {
            try
            {
                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = inventury.Hlavicky.TableName;

                DataGrid2TextBoxColumn dgdavka = new DataGrid2TextBoxColumn();
                dgdavka.HeaderText = "Dávka";
                dgdavka.MappingName = inventury.Hlavicky.CountEntriesColumn.ColumnName;
                dgdavka.NullText = "-";
                dgdavka.Width = 50;
                ts.GridColumnStyles.Add(dgdavka);

                DataGrid2TextBoxColumn dgpopis = new DataGrid2TextBoxColumn();
                dgpopis.HeaderText = "Popis";
                dgpopis.MappingName = inventury.Hlavicky.DescriptionColumn.ColumnName;
                dgpopis.NullText = "-";
                dgpopis.Width = 50;
                ts.GridColumnStyles.Add(dgpopis);

                DataGrid2TextBoxColumn dgstatus = new DataGrid2TextBoxColumn();
                dgstatus.HeaderText = "Stav";
                dgstatus.MappingName = inventury.Hlavicky.StateColumn.ColumnName;
                dgstatus.NullText = "-";
                dgstatus.Width = 50;
                ts.GridColumnStyles.Add(dgstatus);

                this.dgI1.TableStyles.Add(ts);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }
    }
}