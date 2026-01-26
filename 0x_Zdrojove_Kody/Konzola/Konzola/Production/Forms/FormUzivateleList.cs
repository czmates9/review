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
    public partial class FormUzivateleList : Form
    {
        /// <summary>
        /// Uživatel, který zboží upravuje
        /// </summary>
        public Production.DataServices.KonzolaDataSet.FASK_CONS_LoginsRow loginrow { get; set; }
        public Production.DataServices.VyrobaDataSet.LoginsRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[bindingSource1].Current)).Row as Production.DataServices.VyrobaDataSet.LoginsRow;

                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public FormUzivateleList()
        {
            InitializeComponent();
            UpdateForm();
        }

        private void FormUzivateleList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dataGridView1.LoadConfiguration(this.GetType().ToString()); 
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
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

        /// <summary>
        /// Aktualizace dat po aktualizaci.
        /// </summary>
        private void UpdateForm()
        {
            try
            {
                //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
                //lta.Connection.ConnectionString = Globals.ConnectionString;
                //lta.Fill(this.vyrobaDataSet1.Logins);
                System.Data.SqlClient.SqlDataAdapter da_filter = new System.Data.SqlClient.SqlDataAdapter();
                da_filter.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da_filter.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection();
                da_filter.SelectCommand.Connection.ConnectionString = Globals.ConnectionString;
                da_filter.SelectCommand.CommandText = "Select l.*, g.name groupname " +
                    "from Logins l " +
                    "left join VLoginsGroups vg on l.id = vg.loginid " +
                    "left join Groups g on vg.groupid = g.id";
                da_filter.Fill(this.vyrobaDataSet1.Logins);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleList_KeyDown(object sender, KeyEventArgs e)
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

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformCreateRecord();
        }

        private void PerformCreateRecord()
        {
            try
            {
                using (FormUzivateleEdit frmuziv = new FormUzivateleEdit())
                {
                    frmuziv.Text = "Nový uživatel";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
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

        private void buttonUpravit_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
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

                using (FormUzivateleEdit frmuziv = new FormUzivateleEdit())
                {
                    frmuziv.loginsrow = SelectedRow;
                    frmuziv.Text = "Úprava uživatele";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
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
                if (MessageBox.Show("Chcete odstranit uživatele " + SelectedRow.firstname.Trim() + " " + SelectedRow.surname.Trim() + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                SelectedRow.Delete();
                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                //lta.Delete(SelectedRow.id);
                lta.Update(this.vyrobaDataSet1.Logins);
                this.vyrobaDataSet1.AcceptChanges();
                //UpdateForm();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                     
        }

        private void buttonOdstranit_Click(object sender, EventArgs e)
        {
            PerformDeleteRecord();
        }

        private void FormUzivateleList_Resize(object sender, EventArgs e)
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

        private void FormUzivateleList_FormClosing(object sender, FormClosingEventArgs e)
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
