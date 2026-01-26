using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Fask.Aktualizace_API.Extensions;
using Fask.Aktualizace_API.Forms;
using Fask.Logging;
using JR.Utils.GUI.Forms;

namespace Fask.Aktualizace_API.Odvadeni.Materialy
{
    public partial class FormMaterialVyberTP : Form
    {
        
        private Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable _zboziDatatable = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable ZboziDatatable
        {
            set { _zboziDatatable = value; }
        }

        public bool nevydavat = false;

        public FormMaterialVyberTP()
        {
            InitializeComponent();
            //MaR
            SetButtonWidth();
            ScannerStop();
            ScannerStart();

            nevydavat = false;
            dataGridView1.BackColor = Color.PaleGreen;
        }

        private void SetButtonWidth()
        {
            // Získáme šířku primárního monitoru
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Nastavíme šířku tlačítka na 80% šířky monitoru
            int buttonWidth = (int)(screenWidth * 0.33);

            // Nastavíme šířku tlačítka
            buttonStorno.Location = new Point(0 * buttonWidth, buttonStorno.Location.Y);
            buttonStorno.MaximumSize = new Size(buttonWidth, 0);

            //buttonStorno.Width = buttonWidth;
            btn_nezadavat.Location = new Point(1 * buttonWidth, buttonStorno.Location.Y);
            //btn_vlozit.Width = buttonWidth;
            buttonOK.Location = new Point(2 * buttonWidth, buttonStorno.Location.Y);
            buttonOK.MinimumSize = new Size(buttonWidth, 0);
            //buttonOK.Width = buttonWidth;
        }

        //MaR

        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow MaterialSelected 
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[this.fASKVyroba_TPBindingSource].Current)).Row as Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow;
                }
                catch
                {
                    return null;
                }
            }
        
        }

        private List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow> _fASK_Vyroba_TPRow
        {
            get
            {
                List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow> rows = new List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow>();

                foreach (DataGridViewRow selectedRow in dataGridView1.Rows)
                {
                    Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow;
                    rows.Add(row);
                }

                return rows;
            }
        }


        private List<string> _listNMBR;

        public List<string> ListNMBR
        {
            get
            {
                try
                {
                    return _listNMBR;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                _listNMBR = value;
            }

        }


        private void FormMaterialVyber_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);

            this.dataGridView1.LoadConfiguration(this.GetType().ToString());

            fASKVyroba_TPBindingSource.DataSource = _zboziDatatable;
            panelButtons_Resize(null, null);
            ScannerStop();
            ScannerStart();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void finalize()
        {
            this.dataGridView1.SaveConfiguration(this.GetType().ToString());
        }

        private void FormMaterialVyber_KeyDown(object sender, KeyEventArgs e)
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
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        #region scanner

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady_TP);
                FormMain.Scanner.DataReady += new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady_TP);
                FormMain.Scanner.Enable();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady_TP);
                FormMain.Scanner.Disable();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void ScannerEventHandlerMethod_TP(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            try
            {
                ScannerStop();

                if (e.BarcodeData.Trim().Length == 0)
                {
                    ScannerStart();
                    return;
                }




                List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow> rowsToRemove = new List<Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow>();
                bool odstranitZaznamy = false;


                foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow row in _zboziDatatable.Rows)
                {
                    if (row.CZ_CarKod == e.BarcodeData.Trim())
                    {
                        // Přidat řádek do seznamu pro odstranění
                        odstranitZaznamy = true;
                    }
                }

                if (odstranitZaznamy)
                {

                    foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow row in _zboziDatatable.Rows)
                    {
                        if (row.CZ_CarKod != e.BarcodeData.Trim())
                        {
                            // Přidat řádek do seznamu pro odstranění
                            //odstranitZaznamy = true;
                            rowsToRemove.Add(row);
                        }
                    }


                    // Odstranit shromážděné řádky po skončení iterace
                    foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow rowToRemove in rowsToRemove)
                    {
                        _zboziDatatable.Rows.Remove(rowToRemove);
                    }

                    if (_zboziDatatable.Count == 1)
                    {
                        fASKVyroba_TPBindingSource.DataSource = _zboziDatatable;

                        //VyberMaterialScanner(e.BarcodeData.Trim());

                        PerformOK();
                        return;
                    }


                }
                else
                {
                    ScannerStart();
                    return;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        void Scanner_DataReady_TP(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            try
            {
                this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod_TP), new object[] { sender, e });

            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "Adam_DataReady", false);
                ExceptionHandler2.Handle(ex);
            }
        }


        #endregion

        public void PerformOK()
        {

            if (this.dataGridView1.SelectedRows.Count > 1)
            {
                FlexibleMessageBox.Show("Je možné přidávat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);

                //using (var customMessageBox = new FormDialog(20, "Je možné přidávat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                //{
                //    customMessageBox.ShowDialog();
                //}

                return;
            }

            //TaD
            if (this.MaterialSelected == null)
            {
                FlexibleMessageBox.Show("Není vybrán materiál", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                //using (var customMessageBox = new FormDialog(20, "Není vybrán materiál", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                //{
                //    customMessageBox.ShowDialog();
                //}
                return;
            }

            this.finalize();
            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            this.finalize();

            ListNMBR = null;
            //pokud je zapnute dopocist vyberu vsechny zaznamy z datagridu a napisu do hodnoty zadano predpis
            if (Settings.Production_Material_Dopocist)
            {

                DialogResult dialogResult = FlexibleMessageBox.Show("Chcete dopočíst nezadané materiály?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                if (DialogResult.No == dialogResult)
                {
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                //using (var customMessageBox = new FormDialog(20, "Chcete dopočíst nezadané materiály?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
                //{
                //    //customMessageBox.ShowDialog();

                //    if (DialogResult.No == customMessageBox.ShowDialog())
                //    {
                //        this.DialogResult = DialogResult.Cancel;
                //        return;
                //    }
                //}

                //vratim vsechny zaznamy s dopoctem
                List<string> listMaterialNMBR = new List<string>();
                ListNMBR = new List<string>();


                foreach (SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow row in _fASK_Vyroba_TPRow)
                {
                    //if(row.IsZadane_MnNull() || (!row.IsZadane_MnNull() && row.Zadane_Mn != 0))
                    //listMaterialNMBR.Add(row.ITEMNMBR_fol);

                    if (row.IsZadane_MnNull())
                        listMaterialNMBR.Add(row.ITEMNMBR_fol);

                }

                ListNMBR = listMaterialNMBR;
            }
            else
            {
                //nic nedelam jen storno
                ListNMBR = null;
            }



            this.DialogResult = DialogResult.Cancel;
        }

        private void toolStripMenuItemOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void toolStripMenuItemStorno_Click(object sender, EventArgs e)
        {
             this.PerformCancel();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void btn_nezadavat_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 1)
            {
                FlexibleMessageBox.Show("Je možné nezadávat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);

                //using (var customMessageBox = new FormDialog(20, "Je možné nezadávat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk))
                //{
                //    customMessageBox.ShowDialog();

                
                //}
                return;
            }

            //TaD
            if (this.MaterialSelected == null)
            {
                 FlexibleMessageBox.Show("Není vybrán materiál", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                //using (var customMessageBox = new FormDialog(20, "Není vybrán materiál", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk))
                //{
                //    customMessageBox.ShowDialog();


                //}
                return;
            }

            nevydavat = true;
            ScannerStop();
            this.finalize();
            this.DialogResult = DialogResult.OK;
        }
    }
}
