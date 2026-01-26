using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_P.Forms;
using JR.Utils.GUI.Forms;

namespace Fask.Vyroba_P.Odvadeni
{
    public partial class FormVyberPrikaz : Form
    {

        string textform = string.Empty;

        private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _pracovnik = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            set
            {
                _pracovnik = value;
                UpdateTextForm();
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow _stroj = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Stroj
        {
            set
            {
                _stroj = value;
                UpdateTextForm();
            }
        }


        private void UpdateTextForm()
        {
            this.Text = textform;
            if (_pracovnik != null)
                this.Text += ", " + _pracovnik.ToString();

            if (_stroj != null)
                this.Text += ", " + _stroj.ToString();
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable vyrobniPrikazy = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable VyrobniPrikazy
        {
            set
            {
                vyrobniPrikazy = value;
                cZPROVPHBindingSource.DataSource = vyrobniPrikazy;

                UpdateStavGrig();
            }
        }

        private void UpdateStavGrig()
        {
            foreach (Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow dvphrow in vyrobniPrikazy)
            {
                int tid = Convert.ToInt32(dvphrow.TermID);

                if (tid == 0)
                {
                    //dwRow.DefaultCellStyle.BackColor = Color.Green;
                    //dwRow.Cells["termIDDataGridViewTextBoxColumn"].Value = "Pøipraveno";
                    dvphrow.Stav = "Pøipraveno";
                }
                else if (0 < tid && tid <= 100)
                {
                    if (tid == Settings.TerminalID)
                    {
                        //dwRow.DefaultCellStyle.BackColor = Color.Blue;
                        //dwRow.Cells["termIDDataGridViewTextBoxColumn"].Value = "Rozpracováno";
                        //dwRow.Cells["Stav"].Value = "Rozpracováno";
                        dvphrow.Stav = "Rozpracováno";
                    }
                    else
                    {
                        //dwRow.DefaultCellStyle.BackColor = Color.Red; //nelze zpracovat vyr.prikaz blokly jinym terminalem ...
                        //dwRow.Cells["termIDDataGridViewTextBoxColumn"].Value = "Rozpracováno [" + tid + "]";
                        //dwRow.Cells["Stav"].Value = "Rozpracováno [" + tid + "]";
                        dvphrow.Stav = "Rozpracováno [T:" + tid + "]";
                    }
                }
                else if (tid > 100)
                {
                    //dwRow.DefaultCellStyle.BackColor = Color.Red;
                    //dwRow.Cells["termIDDataGridViewTextBoxColumn"].Value = "Ukonèeno";
                    //dwRow.Cells["Stav"].Value = "Ukonèeno";
                    dvphrow.Stav = "Ukonèeno [T:" + (tid - 100) + "]";
                }                
            }
        }

        private void UpdateStavCell()
        {
            foreach (DataGridViewRow dwRow in dataGridView1.Rows)
            {
                DataRowView drowView = (DataRowView)dwRow.DataBoundItem;
                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow dvphRow = (Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow)drowView.Row;
                //int tid = Convert.ToInt32(dvphRow.TermID);

                //if (tid == 0)
                //{
                //    //dwRow.DefaultCellStyle.BackColor = Color.Green;
                //    dwRow.Cells["termIDDataGridViewTextBoxColumn"].Value = "Pøipraveno";
                //}
                //else if (0 < tid && tid <= 100)
                //{
                //    if (tid == Settings.TerminalID)
                //    {
                //        //dwRow.DefaultCellStyle.BackColor = Color.Blue;
                //        dwRow.Cells["termIDDataGridViewTextBoxColumn"].Value = "Rozpracováno";
                //        dwRow.Cells["Stav"].Value = "Rozpracováno";
                //    }
                //    else
                //    {
                //        //dwRow.DefaultCellStyle.BackColor = Color.Red; //nelze zpracovat vyr.prikaz blokly jinym terminalem ...
                //        //dwRow.Cells["termIDDataGridViewTextBoxColumn"].Value = "Rozpracováno [" + tid + "]";
                //        dwRow.Cells["Stav"].Value = "Rozpracováno [" + tid + "]";
                //    }
                //}
                //else if (tid > 100)
                //{
                //    //dwRow.DefaultCellStyle.BackColor = Color.Red;
                //    //dwRow.Cells["termIDDataGridViewTextBoxColumn"].Value = "Ukonèeno";
                //    dwRow.Cells["Stav"].Value = "Ukonèeno";
                //}

                if (dvphRow.SOPTYPE.Trim() == "F")
                {
                    //dwRow.DefaultCellStyle.BackColor = Color.LightGreen;
                    dwRow.Cells["sOPTYPEDataGridViewTextBoxColumn"].Style.BackColor = Color.LightGreen;
                }
                else if (dvphRow.SOPTYPE.Trim() == "P")
                {
                    //dwRow.DefaultCellStyle.BackColor = Color.LightBlue;
                    dwRow.Cells["sOPTYPEDataGridViewTextBoxColumn"].Style.BackColor = Color.LightBlue;
                }
            }
        }

        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow VyrobniPrikazVybrany
        {
            get
            {
                try
                {
                    return (cZPROVPHBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    return null;
                }
            }
        }

        public FormVyberPrikaz()
        {
            InitializeComponent();

            this.textform = this.Text;
        }

        private void FormPrikazVyber_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            ScannerStart();

            SettingLoad();

            dataGridView1.Paint += new PaintEventHandler(dataGridView1_Paint);
        }

        void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            UpdateStavCell();
        }

        private void SettingSave()
        {
            Settings.FormVyberPrikazSplitterDistance1 = splitContainer1.SplitterDistance;
            Settings.Update();
        }

        private void SettingLoad()
        {
            splitContainer1.SplitterDistance = Settings.FormVyberPrikazSplitterDistance1;
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;

            FlexibleMessageBox.Show(this, "Not Implemented");
        }

        void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        private void finalize()
        {
            ScannerStop();
            SettingSave();
        }

        public void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormInputKod_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else if (e.KeyCode == Keys.F2)
                {
                    toolStripButtonZmenaStavu_Click(null, null);
                }
                else if (e.KeyCode == Keys.F3)
                {
                    toolStripButtonTisk_Click(null, null);
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ucDetail1.DetialObject = new Classes.VyrobniPrikaz((cZPROVPHBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void toolStripButtonZmenaStavu_Click(object sender, EventArgs e)
        {

            try
            {
                #region Davkove zpracovani
                if (Settings.DavkoveZpracovani)
                {

                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter vppta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter();
                    //vppta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter vphta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
                    //vphta.Connection.ConnectionString = vppta.Connection.ConnectionString;

                    DataRowView drv = cZPROVPHBindingSource.Current as DataRowView;
                    Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph = drv.Row as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow;

                    if (rowvph == null)
                    {
                        FlexibleMessageBox.Show(this, "Není vybrán záznam", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    if (rowvph.TermID >= 100)
                    {
                        throw new Exception("Výrobní pøíkaz je již dokonèen");
                    }
                    if (rowvph.TermID == Settings.TerminalID)
                    {
                        //ok, pokraèuji dál, jsem to já ...
                        // moznost pokracovani, odblokovani, ukonceni vyrobniho prikazu...
                        using (FormBlokace fblokace = new FormBlokace(new Classes.VyrobniPrikaz(rowvph)))
                        {

                            Vyroba_P.WebServiceVyroba.VyrobniPrikazHlavicka wsvphlav = new Fask.Vyroba_P.WebServiceVyroba.VyrobniPrikazHlavicka();
                            wsvphlav.COUNTENTRIES = rowvph.CountEntries;
                            wsvphlav.SOPNUMBE = rowvph.SOPNUMBE;


                            DialogResult dr = fblokace.ShowDialog(this);
                            if (dr == DialogResult.Cancel) //storno operace
                                return;
                            else if (dr == DialogResult.Abort) //Uzavreni davky
                            {
                                bool uzavreno = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.VyrobniPrikazBlokace(Settings.TerminalID, wsvphlav, Fask.Vyroba_P.WebServiceVyroba.BlokaceTyp.Uzavrit);
                                if (!uzavreno)
                                {
                                    throw new Exception("Nepodaøilo se uzavøít výrobní pøíkaz");
                                }
                                else
                                {
                                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.BlokovatOperaceByCountEntriesSopnumbe_CZPRO_VPP((byte)(Settings.TerminalID + 100), wsvphlav.COUNTENTRIES, wsvphlav.SOPNUMBE);
                                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.BlokovatPrikazByCountEntriesSopnumbe_CZPRO_VPH((byte)(Settings.TerminalID + 100), wsvphlav.COUNTENTRIES, wsvphlav.SOPNUMBE);
                                    rowvph.TermID = (byte)(Settings.TerminalID + 100);
                                }
                                return;
                            }
                            else if (dr == DialogResult.Retry) //Odblokovani davky
                            {
                                bool odblokovano = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.VyrobniPrikazBlokace(Settings.TerminalID, wsvphlav, Fask.Vyroba_P.WebServiceVyroba.BlokaceTyp.OdBlokovat);
                                if (!odblokovano)
                                {
                                    throw new Exception("Nepodaøilo se odblokovat výrobní pøíkaz");
                                }
                                else
                                {
                                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.BlokovatOperaceByCountEntriesSopnumbe_CZPRO_VPP(0, wsvphlav.COUNTENTRIES, wsvphlav.SOPNUMBE);
                                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.BlokovatPrikazByCountEntriesSopnumbe_CZPRO_VPH(0, wsvphlav.COUNTENTRIES, wsvphlav.SOPNUMBE);
                                    rowvph.TermID = 0;
                                }
                                return;
                            }
                            //else //if (dr == DialogResult.OK) //Pokracovat ve vyrobe
                            //    ;

                        }
                    }
                    else if (rowvph.TermID <= 0)
                    {

                        if (FlexibleMessageBox.Show(this, "Opravdu blokovat výrobní pøíkaz?\n\n" + rowvph.ToString(), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                            == DialogResult.No)
                            return;

                        //Pokud je davkove zpracovani, tak se zablokuje vyrobni prikaz
                        //Jestlize se to nepovede, tak neni mozne vyrobni prikaz provest...
                        Fask.Vyroba_P.WebServiceVyroba.VyrobniPrikazHlavicka wsvphlavicka = new Fask.Vyroba_P.WebServiceVyroba.VyrobniPrikazHlavicka();
                        wsvphlavicka.COUNTENTRIES = rowvph.CountEntries;
                        wsvphlavicka.SOPNUMBE = rowvph.SOPNUMBE;

                        bool blokovano = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.VyrobniPrikazBlokace(Settings.TerminalID, wsvphlavicka, Fask.Vyroba_P.WebServiceVyroba.BlokaceTyp.Blokovat);
                        if (blokovano)
                        {
                            int raff = 0;
                            raff = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.BlokovatPrikazByCountEntriesSopnumbe_CZPRO_VPH(Settings.TerminalID, wsvphlavicka.COUNTENTRIES, wsvphlavicka.SOPNUMBE);
                            raff = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.BlokovatOperaceByCountEntriesSopnumbe_CZPRO_VPP(Settings.TerminalID, wsvphlavicka.COUNTENTRIES, wsvphlavicka.SOPNUMBE);
                            rowvph.TermID = Settings.TerminalID;
                        }
                        else
                        {
                            throw new Exception("Nepodaøilo se zablokovat výrobní pøíkaz");
                        }
                    }
                    else
                    {
                        throw new Exception("Výrobní pøíkaz je blokován terminálem s ID=" + rowvph.TermID);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChyba));
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

        }

        private void toolStripButtonTisk_Click(object sender, EventArgs e)
        {

            try
            {
                DataRowView drv = cZPROVPHBindingSource.Current as DataRowView;
                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph = drv.Row as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow;

                if (rowvph == null)
                {
                    FlexibleMessageBox.Show(this, "Není vybrán záznam", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                #region Tisk etiket
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter vppta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter();
                //vppta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dtvpp = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByCountEntriesSopnumbe_CZPRO_VPP(rowvph.CountEntries, rowvph.SOPNUMBE);

                //Nastavit vyrobni operaci
                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp = null;
                if (dtvpp.Rows.Count > 1)
                {
                    using (FormVyberPrikazOperace foperace = new FormVyberPrikazOperace())
                    {
                        foperace.Pracovnik = _pracovnik;
                        foperace.VyrobniPrikaz = rowvph;
                        foperace.VyrobniPrikazOperace = dtvpp;
                        if (foperace.ShowDialog(this) == DialogResult.Cancel)
                            return;

                        rowvpp = foperace.VyrobniPrikazOperaceVybrana;

                        if (rowvpp == null)
                            throw new Exception("Nebyla vybrána výrobní operace");

                    }
                }
                else
                { //pouze jedna ...
                    rowvpp = dtvpp[0];
                }

                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = null;

                if (!TiskEtiketa.TiskPriprava(this, _pracovnik, _stroj, rowvph, rowvpp, productionRow))
                    return;
                #endregion

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChyba));
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

        }


    }
}

