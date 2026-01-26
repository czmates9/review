using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Production.Extensions;

namespace Production.Forms
{
    public partial class FormVyrobPrikazList : Form
    {
        public Production.DataServices.VyrobaDataSet.CZPRO_VPHRow SelectedVPHRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[this.bindingSourceVPH].Current)).Row as Production.DataServices.VyrobaDataSet.CZPRO_VPHRow;

                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Production.DataServices.VyrobaDataSet.CZPRO_VPPRow SelectedVPPRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView2.BindingContext[this.bindingSourceVPP].Current)).Row as Production.DataServices.VyrobaDataSet.CZPRO_VPPRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public FormVyrobPrikazList()
        {
            InitializeComponent();
            UpdateForm();
        }

        private void FormHlavickyVyrobPrikazList_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormHlavickyVyrobPrikazList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dataGridView1.LoadConfiguration(this.GetType().ToString() + "VPH");
                this.dataGridView2.LoadConfiguration(this.GetType().ToString() + "VPP");
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Aktualizace dat.
        /// </summary>
        private void UpdateForm()
        {
            try
            {
                int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
                this.vyrobaDataSet1.Clear();
                //this.vyrobaDataSet1.AcceptChanges();

                var adapterVPH = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                adapterVPH.Connection.ConnectionString = Globals.ConnectionString;
                adapterVPH.Fill(this.vyrobaDataSet1.CZPRO_VPH);
                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
                if (SelectedVPHRow != null)
                {
                    var adapterVPP = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                    adapterVPP.Connection.ConnectionString = Globals.ConnectionString;
                    //adapterVPP.Fill(this.vyrobaDataSet1.CZPRO_VPP);
                    adapterVPP.FillByCountEntriesAndSOPNUMBE(this.vyrobaDataSet1.CZPRO_VPP, SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformCancel()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void novýToolStripMenuItem_Click(object sender, EventArgs e)
        {            
        }

        private void buttonNovy_Click(object sender, EventArgs e)
        {            
        }

        private void buttonUpravit_Click(object sender, EventArgs e)
        {
            PerformEditVPPRecord();
        }

        private void upravitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformEditVPPRecord();
        }

        private void buttonOdstranit_Click(object sender, EventArgs e)
        {
        }

        private void PerformEditVPPRecord()
        {
            try
            {
                if(SelectedVPPRow == null)
                    MessageBox.Show("Není zvolena položka příkazu", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                using (FormVPPEdit frmzb = new FormVPPEdit())
                {
                    frmzb.Text = "Nové zboží pro: " + SelectedVPHRow.SOPDESC;
                    frmzb.rowVPP = SelectedVPPRow;
                    frmzb.rowVPH = SelectedVPHRow;
                    frmzb.ShowDialog();                    
                }


            }
            catch (Exception ex)
            {                
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (SelectedVPHRow == null)
                    return;
                //this.bindingSourceVPP.Filter = "CountEntries='" + SelectedVPHRow.CountEntries + "' AND SOPNUMBE='" + SelectedVPHRow.SOPNUMBE + "'";
                var adapterVPP = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                adapterVPP.Connection.ConnectionString = Globals.ConnectionString;
                adapterVPP.FillByCountEntriesAndSOPNUMBE(this.vyrobaDataSet1.CZPRO_VPP, SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        private void hlavickaVyrobníhoPrikazuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void PerformCreateVPHRecord()
        {
            try
            {
                using (FormVPHEdit frmzb = new FormVPHEdit())
                {
                    frmzb.Text = "Nový výrobní příkaz";
                    if (frmzb.ShowDialog(this) != DialogResult.OK)
                        return;
                    UpdateForm();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void polozkaVyrobnihoPrikazuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void PerformCreateVPPRecord()
        {
            try
            {
                if (SelectedVPHRow == null)
                {
                    MessageBox.Show("Není zvolena hlavička výrobního příkazu", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (FormVPPEdit frmzb = new FormVPPEdit())
                {
                    frmzb.Text = "Nové zboží pro: " + SelectedVPHRow.SOPDESC;
                    frmzb.rowVPH = SelectedVPHRow;

                    if (frmzb.ShowDialog(this) != DialogResult.OK)
                        return;
                    UpdateForm();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformDeleteVPHRecord()
        {
            try
            {
                if (SelectedVPHRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //MessageBox.Show("Chcete smazat uživatele " + SelectedRow.firstname + " " + SelectedRow.surname + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MessageBox.Show("Chcete odstranit příkaz " + SelectedVPHRow.SOPNUMBE.Trim()+ "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                /*
                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                lta.Delete(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);

                var lta2 = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                lta2.Connection.ConnectionString = Globals.ConnectionString;
                lta2.DeleteByCountEntriesSOPNUMBE(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);

                UpdateForm();
                */
                var lta2 = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                lta2.Connection.ConnectionString = Globals.ConnectionString;
                lta2.DeleteByCountEntriesSOPNUMBE(SelectedVPHRow.CountEntries, SelectedVPHRow.SOPNUMBE);

                SelectedVPHRow.Delete();

                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;

                //var lta2 = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                //lta2.Connection.ConnectionString = Globals.ConnectionString;

                //lta2.Update(vyrobaDataSet1.CZPRO_VPP);
                lta.Update(vyrobaDataSet1.CZPRO_VPH);
                vyrobaDataSet1.AcceptChanges();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDeleteVPPRecord()
        {
            try
            {
                if (SelectedVPPRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Chcete odstranit příkaz " + SelectedVPPRow.ITEMDESC.Trim() + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                SelectedVPPRow.Delete();
                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                lta.Update(vyrobaDataSet1.CZPRO_VPP);
                vyrobaDataSet1.AcceptChanges();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItemNovyPrikaz_Click(object sender, EventArgs e)
        {
            PerformCreateVPHRecord();
        }

        private void toolStripMenuItemPridatZbozi_Click(object sender, EventArgs e)
        {
            PerformCreateVPPRecord();
        }

        private void ToolStripMenuItemOdstranitPrikaz_Click(object sender, EventArgs e)
        {
            PerformDeleteVPHRecord();
        }

        private void ToolStripMenuItemOdstranitZbozi_Click(object sender, EventArgs e)
        {
            PerformDeleteVPPRecord();
        }

        private void ToolStripMenuItemUpravitPrikaz_Click(object sender, EventArgs e)
        {
            PerformEditVPHRecord();
        }

        private void PerformEditVPHRecord()
        {
            try
            {
                if (SelectedVPHRow == null)
                    MessageBox.Show("Není zvolen příkaz", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                using (FormVPHEdit frmzb = new FormVPHEdit())
                {
                    frmzb.Text = "Úprava výrobního příkazu";
                    frmzb.VPHrow = SelectedVPHRow;
                    frmzb.ShowDialog();

                }


            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuItemObnovit_Click(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void ToolStripMenuItemDuplikace_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedVPHRow == null)
                {
                    MessageBox.Show("Není zvolena hlavička výrobního příkazu pro duplikaci", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Production.DataServices.VyrobaDataSet ds =  this.vyrobaDataSet1;
                using (FormVPHEdit frmzb = new FormVPHEdit())
                {
                    frmzb.Text = "Duplikace výrobního příkazu";
                    frmzb.Duplikace = true;
                    if (frmzb.ShowDialog(this) != DialogResult.OK)
                        return;

                    var lta = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                    lta.Connection.ConnectionString = Globals.ConnectionString;

                    foreach (var item in ds.CZPRO_VPP)
                    {
                        lta.Insert(
                        frmzb.VPHrowCreated.CountEntries,
                        frmzb.VPHrowCreated.SOPNUMBE,
                        item.ITEMNMBR,
                        item.ITEMTYPE,
                        item.ITEMDESC,
                        item.ITEMMJ,
                        item.VNDDOCNMP,
                        item.VNDITNUM,
                        item.ORD,
                        item.BarcodeP,
                        item.LOCNCODE,
                        item.QTYSHPPD = (frmzb.DuplikovatMnozstvi) ? item.QTYSHPPD : 0,
                        item.QTYDOKON,
                        item.QTYPACK,
                        item.QTYPACKMJ,
                        item.TIMEMODE,
                        item.TIMEPREP,
                        item.TIMEUNIT,
                        item.DtProdT,
                        item.DtProdL,
                        item.SerNumT,
                        item.SerNumL,
                        item.VerT,
                        item.VerL,
                        item.TermID,
                        DateTime.Now);
                    }
                }

                    UpdateForm();

                
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVyrobPrikazList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dataGridView1.SaveConfiguration(this.GetType().ToString() + "VPH");
                this.dataGridView2.SaveConfiguration(this.GetType().ToString() + "VPP");
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
    }
}
