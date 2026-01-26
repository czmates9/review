using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;

namespace Production.Forms
{
    public partial class FormUzivateleEdit : Form
    {
        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Production.DataServices.VyrobaDataSet.LoginsRow loginsrow { get; set; }

        /// <summary>
        /// Vybraná skupina
        /// </summary>
        private Production.DataServices.VyrobaDataSet.GroupsRow rowGroups
        {
            get
            {
                try
                {
                    return comboBoxSkupina.SelectedItem as Production.DataServices.VyrobaDataSet.GroupsRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public FormUzivateleEdit()
        {
            InitializeComponent();            
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormUzivateleEdit_Resize(null, null);

                LoadData();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void LoadData()
        {
            // načtení skupin
            var taGroups = new Production.DataServices.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            taGroups.Connection.ConnectionString = Globals.ConnectionString;
            var ds = new Production.DataServices.VyrobaDataSet();            
            taGroups.Fill(ds.Groups);
            //comboBoxSkupina.DataSource = ds.Groups;§
            comboBoxSkupina.Items.Add("");
            comboBoxSkupina.Items.AddRange(ds.Groups.Select(null, "name asc"));
            //comboBoxSkupina.ValueMember = "id";
            //comboBoxSkupina.DataSource = taGroups.GetData();
            comboBoxSkupina.SelectedItem = null;

            // je úprava záznamu, dojde k načtení dat
            if (loginsrow != null)
            {
                textBoxId.Enabled = false;
                textBoxId.Text = loginsrow.id.Trim();
                textBoxJmeno.Text = loginsrow.firstname.Trim();
                textBoxPrijmeni.Text = loginsrow.surname.Trim();
                textBoxHeslo.Text = loginsrow.psswd.Trim();
                checkBoxVedouciSmeny.Checked = Convert.ToBoolean(loginsrow.VS);

                // najiti vazby, pokud je
                var taLoginsGroups = new Production.DataServices.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
                taLoginsGroups.Connection.ConnectionString = Globals.ConnectionString;
                var dtLoginsGroups = taLoginsGroups.GetDataByLoginID(loginsrow.id);

                // nalezeno
                if (dtLoginsGroups.Count > 0)
                {
                    comboBoxSkupina.SelectedItem =  ds.Groups.Where(x => x.id == dtLoginsGroups.First().groupid).First();
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void FormUzivateleEdit_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            try
            {
                if (!ValidateData())
                    return;

                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                // je úprava záznamu
                if (loginsrow != null)
                {
                    loginsrow.firstname = textBoxJmeno.Text.Trim(); ;
                    loginsrow.surname = textBoxPrijmeni.Text.Trim();
                    loginsrow.psswd = textBoxHeslo.Text.Trim();
                    loginsrow.VS = checkBoxVedouciSmeny.Checked ? (byte)1 : (byte)0;
                    lta.Update(loginsrow);
                }
                else   // nový záznam
                {
                    //DataServices.VyrobaDataSet.LoginsDataTable loginsdatatable = new DataServices.VyrobaDataSet.LoginsDataTable();
                    lta.Insert(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim(), checkBoxVedouciSmeny.Checked ? (byte)1 : (byte)0);
                    //lta.Insert(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim(), 0);
                }
                // odstraneni vazeb podle id uzivatele
                var taLoginsGroups = new Production.DataServices.VyrobaDataSetTableAdapters.VLoginsGroupsTableAdapter();
                taLoginsGroups.Connection.ConnectionString = Globals.ConnectionString;
                taLoginsGroups.DeleteByLoginID(textBoxId.Text.Trim());

                if (rowGroups != null)
                {
                    // pridani skupiny
                    taLoginsGroups.Insert(textBoxId.Text.Trim(), rowGroups.id);
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {                
                if(string.IsNullOrEmpty(textBoxId.Text.Trim()))
                    throw new Exception("Musíte zadat id uživatele");
                
                if (loginsrow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter adapter = new DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
                    adapter.Connection.ConnectionString = Globals.ConnectionString;
                    DataServices.VyrobaDataSet.LoginsDataTable loginsdatatable = new DataServices.VyrobaDataSet.LoginsDataTable();
                    adapter.Fill(loginsdatatable);
                    
                    var logins = loginsdatatable.Where(x => x.id == textBoxId.Text.Trim());
                    if (logins.Count() > 0)
                    {
                        textBoxId.Focus();
                        textBoxId.SelectAll();
                        throw new Exception("Zadané id uživatele již existuje");
                    }
                }

                if (string.IsNullOrEmpty(textBoxJmeno.Text.Trim()))
                {
                    textBoxJmeno.Focus();
                    throw new Exception("Musíte zadat jméno");
                }
                if (string.IsNullOrEmpty(textBoxPrijmeni.Text.Trim()))
                {
                    textBoxPrijmeni.Focus();
                    throw new Exception("Musíte zadat příjmení");
                }
                if (string.IsNullOrEmpty(textBoxHeslo.Text.Trim()))
                {
                    textBoxHeslo.Focus();
                    throw new Exception("Musíte zadat heslo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void FormUzivateleEdit_Shown(object sender, EventArgs e)
        {         
        }

        private void FormUzivateleEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            this.PerformOK();
        }
    }
}
