using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using System.Reflection;
using Fask.Graphic;
using System.Linq;
using Fask.MST_W.SQLite_Classes;

namespace Fask.MST_W.Prodej_3
{
    public partial class ProdejVyberDavky : System.Windows.Forms.Form
    {

        private ProdejDavkyDataSet prodejDavky = null;
        private DataView prodejDavkyView = null;

        private bool _enablenew = true;
        private bool _enabledelete = true;
        private bool _hesloProOtevreniRozpracovaneDavky = false;
        /// <summary>
        /// Požadovat heslo pro otevøení již rozpracované dávky.
        /// </summary>
        public bool HesloProOtevreniRozpracovaneDavky
        {
            get { return _hesloProOtevreniRozpracovaneDavky; }
            set { _hesloProOtevreniRozpracovaneDavky = value; }
        }

        private int _cislodavky = 0;
        public int CisloDavky
        {
            get { return _cislodavky; }
            set { _cislodavky = value; }
        }

        private ProdejDavkyDataSet.DavkyRow SelectedDavka
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[prodejDavkyView].Current as DataRowView).Row as ProdejDavkyDataSet.DavkyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public ProdejVyberDavky()
        {
            InitializeComponent();

            prodejDavky = new ProdejDavkyDataSet();
            prodejDavkyView = new DataView(prodejDavky.Davky);
            dataGrid1.DataSource = prodejDavkyView;

            InitializeDataGridView();

            MyInitializeGrid();
        }

        DataGrid2TextBoxColumn odavka = null;
        DataGrid2TextBoxColumn opocet = null;
        DataGrid2TextBoxColumn odb = null;
        DataGrid2TextBoxColumn odoklad = null;
        DataGrid2TextBoxColumn ostredisko = null;
        DataGrid2TextBoxColumn oskladz = null;
        DataGrid2TextBoxColumn oskladc = null;

        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = prodejDavky.Davky.TableName;

            odavka = new DataGrid2TextBoxColumn();
            odavka.HeaderText = "Dávka";
            odavka.MappingName = prodejDavky.Davky.CisloDavkyColumn.ColumnName;
            odavka.NullText = "-";
            odavka.Width = 50;
            ts.GridColumnStyles.Add(odavka);

            opocet = new DataGrid2TextBoxColumn();
            opocet.HeaderText = "Poèet";
            opocet.MappingName = prodejDavky.Davky.PolozekColumn.ColumnName;
            opocet.NullText = "-";
            opocet.Width = 50;
            ts.GridColumnStyles.Add(opocet);

            odb = new DataGrid2TextBoxColumn();
            odb.HeaderText = "Odbìratel";
            odb.MappingName = prodejDavky.Davky.OdberatelColumn.ColumnName;
            odb.NullText = "-";
            odb.Width = 50;
            ts.GridColumnStyles.Add(odb);

            odoklad = new DataGrid2TextBoxColumn();
            odoklad.HeaderText = "Doklad";
            odoklad.MappingName = prodejDavky.Davky.DokladColumn.ColumnName;
            odoklad.NullText = "-";
            odoklad.Width = 50;
            ts.GridColumnStyles.Add(odoklad);

            ostredisko = new DataGrid2TextBoxColumn();
            ostredisko.HeaderText = "Støedisko";
            ostredisko.MappingName = prodejDavky.Davky.StrediskoColumn.ColumnName;
            ostredisko.NullText = "-";
            ostredisko.Width = 50;
            ts.GridColumnStyles.Add(ostredisko);

            oskladz = new DataGrid2TextBoxColumn();
            oskladz.HeaderText = "Sklad zdroj";
            oskladz.MappingName = prodejDavky.Davky.SkladZdrojColumn.ColumnName;
            oskladz.NullText = "-";
            oskladz.Width = 50;
            ts.GridColumnStyles.Add(oskladz);

            oskladc = new DataGrid2TextBoxColumn();
            oskladc.HeaderText = "Sklad cíl";
            oskladc.MappingName = prodejDavky.Davky.SkladCilColumn.ColumnName;
            oskladc.NullText = "-";
            oskladc.Width = 50;
            ts.GridColumnStyles.Add(oskladc);

            dataGrid1.TableStyles.Add(ts);

            //dataGrid1.RowHeightDefault = Settings.ProdejVyberDavkyRowHeight;
        }

        public ProdejVyberDavky(bool enablenew)
            : this()
        {
            this._enablenew = enablenew;
            if (!this._enablenew)
            {
                this.menuItemNova.Enabled = false;
            }
        }

        public ProdejVyberDavky(bool enablenew, bool enabledelete)
            : this(enablenew)
        {
            this._enabledelete = enabledelete;
            if (!this._enabledelete)
            {
                this.menuItemNova.Enabled = false;
            }
        }

        private bool KontrolaCislaDavky(string cislodavky)
        {
            try
            {
                _cislodavky = int.Parse(cislodavky);
                if (_cislodavky < 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformNew()
        {
            string davkaprodej = string.Empty;

            if (Prodej.Globals.RangeEnable)
            {
                Config.CiselneRady cr = new Fask.MST_W.Config.CiselneRady();
                davkaprodej = cr.ProdejGetNext();
            }
            else
            {
                string value;
                if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejVyberDavkyZadejteCisloDavky, "", out value, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
                {
                    davkaprodej = value;
                }
                else
                {
                    return;
                }
            }

            if (!KontrolaCislaDavky(davkaprodej))
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberDavkyChybaVCisleDavky);
                return;
            }

            int davkaprodeji = int.Parse(davkaprodej);

            string dstFile = Path.Combine(Main.StorageDir, davkaprodeji.ToString() + "." + Main.Ext_Prodej);

            try
            {
                if (File.Exists(dstFile))
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberDavkyDavkaJizExistuje, davkaprodeji), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                else
                {

                    // TODO : Globalni metodu ... => Refactoring ... 
					string srcFile = Path.Combine(Main.SQLiteDBsDir, @"Prodej.sql");
					if (!File.Exists(srcFile))
					{
						//Neexistuje zakladaci SQLite script... stahnout z serveru?
						// na serveru je metoda Get_SQLlite_Script_DB();
						// jen takovz rychly navrh...
						var st = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_prodej.Get_SQLlite_Script_DB();
						if (!st.Exception)
						{
							var fs = File.Create(srcFile);
							byte[] bytes = Encoding.UTF8.GetBytes(st.StatusText);
							fs.Write(bytes, 0, bytes.Length);

						}
						else
						{
							throw new Exception(st.StatusText);
						}
					}

					SQLite_Helper helper = new SQLite_Helper();
					if (!helper.SQLite_CreateFile(dstFile, srcFile))
					{
						throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + "Prodej");
					}
					
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void PerformOK()
        {
            ProdejDavkyDataSet.DavkyRow selected = this.SelectedDavka;

            if (selected == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberDavkyNeniVybranaDavka);
                return;
            }

            //if (!(listBox1.SelectedItem is string))
            //{
            //    MessageBoxBig.Show("Dávka není oznaèena èíselným kódem");
            //    return;
            //}

            if (!KontrolaCislaDavky(selected.CisloDavky.ToString()))
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberDavkyChybaVCisleDavky);
                return;
            }
            if (_hesloProOtevreniRozpracovaneDavky && selected.Polozek > 0)
            {
                //MessageBoxBig.Show("Dávka je již naplnìna. Není možné pokraèovat v plnìní dané dávky.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberDavkyZadejteHesloProOdemceni, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                using (Fask.MST_W.Config.FormAdminAccess frmAccess = new Fask.MST_W.Config.FormAdminAccess(Fask.MST_W.Config.FormAdminAccess.AccessType.ProdejEditaceNaplnenaDavka))
                {
                    frmAccess.Text = Fask.Localization.Localization.Prodej3ProdejVyberDavkyHesloProOdemceni;
                    frmAccess.Location = MySystem.FormMidLocation.GetFormLocation(frmAccess.Size);
                    if (frmAccess.ShowDialog() != DialogResult.OK)
                    {
                        //DialogResult = DialogResult.Cancel;
                        return;
                    }
                }
                //MessageBoxBig.Show("Dávka je již naplnìna. Není možné pokraèovat v plnìní dané dávky.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                //return;
            }

            finalize();
            DialogResult = DialogResult.OK;

        }

        private void finalize()
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));

            /*
            Settings.ProdejVyberDavkyCisloDavkyWidth = odavka.Width;
            Settings.ProdejVyberDavkyDokladWidth = odoklad.Width;
            Settings.ProdejVyberDavkyOdberatelWidth = odb.Width;
            Settings.ProdejVyberDavkyPolozekWidth = opocet.Width;
            Settings.ProdejVyberDavkyRowHeight = dataGrid1.RowHeightDefault;
            Settings.ProdejVyberDavkySkladWidth = osklad.Width;
            Settings.ProdejVyberDavkyStrediskoWidth = ostredisko.Width;
            */
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));
        }

        private void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }


        private void ProdejVyberDavky_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            //panel1_Resize(null, null);
            // najde vsechny davky na disku

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Prodej);

                // pred zahajenim operaci vsechny datasety otevru, aby to bylo rychlejsi ... 
                // krome da_prodej, ktery otevira kazdou davku ...
                try { Prodej_3.ProdejMain.prodejInstance.globalObject.controller_odberatele.Connection.Open(); }
                catch { }
                try { Prodej_3.ProdejMain.prodejInstance.globalObject.controller_typdokladu.Connection.Open(); }
                catch { }
                try { Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.Connection.Open(); }
                catch { }
                try { Prodej_3.ProdejMain.prodejInstance.globalObject.controller_strediska.Connection.Open(); }
                catch { }

                prodejDavky.Davky.BeginLoadData();
                foreach (string filename in fileNames)
                {
                    int pocetpolozek = 0;
                    string di_docid = null;
                    string di_docid2 = null;
                    string di_odbid = null;
                    string di_sklid = null;
                    string di_sklid_dest = null;
                    string di_strid = null;
                    byte cfg_lok_mech = 0;
                    int davka = int.Parse(Path.GetFileNameWithoutExtension(filename));
                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow diFirstRow = null;

                    #region 1.Existujici zaznam z DI
                    using (var controller_davka_tmp = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(filename))
                    {
                        diFirstRow = controller_davka_tmp.CZMST_DI_GetFirstRecord();

                        if (diFirstRow != null)
                        {
                            try
                            {
                                object o = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_odberatele.Get_OdbDesc(diFirstRow.ODB_ID);
                                if (o != null && (o is string))
                                    di_odbid = (string)o;
                            }
                            catch { }

                            try
                            {

                                object o = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_typdokladu.GetLokMech_Status(diFirstRow.DOC_ID, diFirstRow.DOC_ID2);
                                if (o != null && (o is byte))
                                    cfg_lok_mech = (byte)o;
                                else cfg_lok_mech = 0;
                            }
                            catch { }

                            try
                            {
                                object o = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_typdokladu.GetDesc(diFirstRow.DOC_ID, diFirstRow.DOC_ID2);
                                if (o != null && (o is string))
                                    di_docid = (string)o;
                            }
                            catch { }


                            try
                            {
                                if (!Prodej.Globals.StrediskoText)
                                {
                                    object o = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_strediska.GetDescription(diFirstRow.STR_ID);
                                    if (o != null && (o is string))
                                        di_strid = (string)o;
                                }
                            }
                            catch { }

                            try
                            {
                                object o = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.GetSkladDesc(diFirstRow.SKL_ID);
                                if (o != null && (o is string))
                                    di_sklid = (string)o;
                            }
                            catch { }

                            try
                            {
                                object o = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.GetSkladDesc(diFirstRow.SKL_ID_DEST);
                                if (o != null && (o is string))
                                    di_sklid_dest = (string)o;
                            }
                            catch { }

                            try
                            {
								pocetpolozek = controller_davka_tmp.PocetPolozek_DI() ?? 0;

                            }
                            catch { }
                        }

                        ProdejDavkyDataSet.DavkyRow drow = prodejDavky.Davky.NewDavkyRow();
                        drow.CisloDavky = davka;
                        drow.Polozek = pocetpolozek;
                        drow.Odberatel = di_odbid;
                        drow.Doklad = di_docid;
                        drow.Stredisko = di_strid;
                        drow.SkladZdroj = di_sklid;
                        drow.SkladCil = di_sklid_dest;
                        drow.cfg_lok_mech = cfg_lok_mech;
                        prodejDavky.Davky.AddDavkyRow(drow);
                    }
                    #endregion
                }
                prodejDavky.Davky.EndLoadData();

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                // nakonec vse uzavru, pokud je otevreno ...
                if ((Prodej_3.ProdejMain.prodejInstance.globalObject.controller_odberatele.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                    Prodej_3.ProdejMain.prodejInstance.globalObject.controller_odberatele.Connection.Close();
                if ((Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                    Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.Connection.Close();
                if ((Prodej_3.ProdejMain.prodejInstance.globalObject.controller_strediska.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                    Prodej_3.ProdejMain.prodejInstance.globalObject.controller_strediska.Connection.Close();
                if ((Prodej_3.ProdejMain.prodejInstance.globalObject.controller_typdokladu.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                    Prodej_3.ProdejMain.prodejInstance.globalObject.controller_typdokladu.Connection.Close();

                Cursor.Current = Cursors.Default;
            }

			if (this.menuItemNova.Enabled)
			{
				//TaD Tady po otevreni automaticky (konfiguracne )vytvoøit novu davku?
				if (Settings.uia_prodej_novaDavka && prodejDavky.Davky.Count < 1)
				{
					PerformNew();
				} 
			}
        }

        private void ProdejVyberDavky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.F1)
            {
                if(this.menuItemNova.Enabled)
                    PerformNew();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                this.buttonOK_Click(null, null);
            }
            else if (e.KeyCode == Keys.Back)
            {
                this.buttonSmazat_Click(null, null);
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformNew();
        }

        private void buttonSmazat_Click(object sender, EventArgs e)
        {
            ProdejDavkyDataSet.DavkyRow selected = this.SelectedDavka;
            if (selected == null)
                return;

            // pokud jsou zapnuty lokace a davka obsahuje zaznamy, neni mozne davku smazat ...
            if (Prodej.Globals.TypDokladu && !selected.IsPolozekNull() && selected.Polozek > 0 && !selected.Iscfg_lok_mechNull() && selected.cfg_lok_mech > 0)
            {
                MessageBoxBig.Show("Je zapnutý lokaèní mechanismus, dávku není možné smazat, pokud obsahuje data.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }

            string davka = selected.CisloDavky.ToString();
            if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberDavkyZrusitDavkuDotaz, davka), Fask.Localization.Localization.Prodej3ProdejVyberDavkyZrusit, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
                == DialogResult.Yes)
            {
                try
                {
                    File.Delete(Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Prodej));
                    selected.Delete();
                    Config.CiselneRady cr = new Fask.MST_W.Config.CiselneRady();
                    cr.ProdejSetDeleted(davka);
                    if (Prodej.Globals.ProdejDialogSmazaniDavky)
                    {
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberDavkyDavkaOdstranena, davka), "", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                }
            }
        }

        private void buttonSmazat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonSmazat_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonStorno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonStorno_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonNovaDavka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonOK_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }
        /*
        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Width / 2, panel1.Height / 2);

            buttonSmazat.Location = new Point(0, 0);
            buttonSmazat.Size = nsize;

            buttonStorno.Location = new Point(0, nsize.Height);
            buttonStorno.Size = nsize;

            buttonNovaDavka.Location = new Point(nsize.Width, 0);
            buttonNovaDavka.Size = nsize;

            buttonOK.Location = new Point(nsize.Width, nsize.Height);
            buttonOK.Size = nsize;
        }
        */
        private void menuItemNova_Click(object sender, EventArgs e)
        {
            PerformNew();
        }

        private void menuItemVybrat_Click(object sender, EventArgs e)
        {
            this.buttonOK_Click(null, null);
        }

        private void menuItemSmazat_Click(object sender, EventArgs e)
        {
            this.buttonSmazat_Click(null, null);
        }

        private void menuItemZpet_Click(object sender, EventArgs e)
        {
            this.buttonStorno_Click(null, null);
        }

        private void ProdejVyberDavky_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ProdejVyberDavky_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

    }
}