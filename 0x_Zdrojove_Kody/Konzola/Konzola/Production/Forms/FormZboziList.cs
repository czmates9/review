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
    public partial class FormZboziList : Form
    {
        /// <summary>
        /// Uživatel, který zboží upravuje
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Konzola.FASK_CONS_LoginsRow loginrow { get; set; }

        public Production.DataServices.KonzolaDataSet.FASK_CONS_095Row SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[bindingSource1].Current)).Row as Production.DataServices.KonzolaDataSet.FASK_CONS_095Row;

                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public FormZboziList()
        {
            InitializeComponent();
            UpdateForm();
        }

        private void FormZboziList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormZboziList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                FormZboziList_Resize(null, null);
                this.dataGridView1.LoadConfiguration(this.GetType().ToString());
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformCreateRecord();
        }

        private void buttonUpravit_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
        }

        private void buttonOdstranit_Click(object sender, EventArgs e)
        {
            PerformDeleteRecord();
        }

        private void novýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCreateRecord();
        }

        private void upravitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformEditRecord();    
        }

        private void odstranitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformDeleteRecord();
        }

        private void PerformEditRecord()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormZboziEdit frmuziv = new FormZboziEdit())
                {
                    frmuziv.loginrow = loginrow;
                    frmuziv.zbozirow = SelectedRow;
                    frmuziv.Text = "Úprava zboží";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;
                    //UpdateForm();
                    var lta = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
                    lta.Connection.ConnectionString = Globals.ConnectionString;
                    //lta.Delete(SelectedRow.ITEMNMBR);
                    lta.Update(frmuziv.zbozirow);
                    //lta.Update(this.vyrobaDataSet1.Production);
                    this.konzolaDataSet1.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDeleteRecord()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //MessageBox.Show("Chcete smazat uživatele " + SelectedRow.firstname + " " + SelectedRow.surname + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MessageBox.Show("Chcete odstranit zboží " + SelectedRow.ITEMDESC.Trim() + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                SelectedRow.Delete();
                var lta = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                //lta.Delete(SelectedRow.ITEMNMBR);
                lta.Update(this.konzolaDataSet1.FASK_CONS_095);
                this.konzolaDataSet1.AcceptChanges();
                //this.dataGridView1.Update();
                //UpdateForm();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }

        private void PerformCreateRecord()
        {
            try
            {
                using (FormZboziEdit frmzb = new FormZboziEdit())
                {
                    frmzb.loginrow = loginrow;
                    frmzb.Text = "Nové zboží";
                    if (frmzb.ShowDialog(this) != DialogResult.OK)
                        return;
                    var lta = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
                    lta.Connection.ConnectionString = Globals.ConnectionString;
                    
                    //this.konzolaDataSet1.FASK_CONS_095.AddFASK_CONS_095Row(frmzb.zbozirow);
                    //this.konzolaDataSet1.FASK_CONS_095.ImportRow(frmzb.zbozirow);
                    //frmzb.zbozirow.SetAdded();
                    //lta.Update(frmzb.zbozirow);
                    //this.konzolaDataSet1.AcceptChanges();
                    //lta.Update(this.konzolaDataSet1.FASK_CONS_095);
                    //this.konzolaDataSet1.AcceptChanges();
                    //frmzb.zbozirow.SetAdded();
                    //this.konzolaDataSet1.FASK_CONS_095.ImportRow(frmzb.zbozirow);
                    //lta.Update(this.konzolaDataSet1.FASK_CONS_095);
                    //this.konzolaDataSet1.AcceptChanges();

                    lta.Insert(
                        frmzb.zbozirow.ITEMNMBR,
                        frmzb.zbozirow.ITEMDESC,
                        frmzb.zbozirow.VNDITNUM,
                        frmzb.zbozirow.CZ_CarKod,
                        frmzb.zbozirow.LOCNCODE,
                        frmzb.zbozirow.SKL_ID,
                        frmzb.zbozirow.QTY,
                        frmzb.zbozirow.QTYPACK,
                        frmzb.zbozirow.MJ,
                        frmzb.zbozirow.DMJ,
                        frmzb.zbozirow.TAXRATE,
                        frmzb.zbozirow.PRICE0,
                        frmzb.zbozirow.PRICE1,
                        frmzb.zbozirow.PRICE2,
                        frmzb.zbozirow.PRICE3,
                        frmzb.zbozirow.PRICE4,
                        frmzb.zbozirow.PRICE5,
                        frmzb.zbozirow.CZ_SerNum_Track,
                        frmzb.zbozirow.CZ_SerNum_Delka,
                        frmzb.zbozirow.CZ_Rez1_Track,
                        frmzb.zbozirow.CZ_Rez2_Track,
                        frmzb.zbozirow.CZ_Rez3_Track,
                        frmzb.zbozirow.CZ_Rez4_Track,
                        frmzb.zbozirow.REZ1,
                        frmzb.zbozirow.ITEMCODE,
                        frmzb.zbozirow.ODB_ID,
                        frmzb.zbozirow.TIMEPREP,
                        frmzb.zbozirow.TIMEUNIT,
                        frmzb.zbozirow.TIMEFROM,
                        frmzb.zbozirow.TIMETO,
                        frmzb.zbozirow.LSTMod,
                        frmzb.zbozirow.loginid,
                        frmzb.zbozirow.TIMEMODE
                        );

                    lta.Fill(this.konzolaDataSet1.FASK_CONS_095);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// Aktualizace dat po aktualizaci.
        /// </summary>
        private void UpdateForm()
        {
            try
            {
                int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
                var lta = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                lta.Fill(this.konzolaDataSet1.FASK_CONS_095);
                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormZboziList_Resize(object sender, EventArgs e)
        {

        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void obnovitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void FormZboziList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dataGridView1.SaveConfiguration(this.GetType().ToString());
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
