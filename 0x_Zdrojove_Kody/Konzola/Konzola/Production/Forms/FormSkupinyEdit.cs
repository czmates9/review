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
    public partial class FormSkupinyEdit : Form
    {
        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Production.DataServices.VyrobaDataSet.GroupsRow groupsrow { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        public Production.DataServices.VyrobaDataSet.GroupsRow newgroupsrow { get; set; }

        public FormSkupinyEdit()
        {
            InitializeComponent();            
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            FormUzivateleEdit_Resize(null, null);
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

                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.GroupsTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                // je úprava záznamu
                if (groupsrow != null)
                {
                    groupsrow.name = textBoxName.Text.Trim();
                    groupsrow.description = textBoxDescription.Text.Trim();
                    //lta.Update(groupsrow);
                }
                else   // nový záznam
                {
                    DataServices.VyrobaDataSet dataset = new DataServices.VyrobaDataSet();
                    DataServices.VyrobaDataSet.GroupsRow newZboziRow = dataset.Groups.NewGroupsRow();

                    //newgroupsrow = dataset.Groups.NewGroupsRow();
                    newZboziRow.id = textBoxId.Text.Trim();
                    newZboziRow.name = textBoxName.Text.Trim();
                    newZboziRow.description = textBoxDescription.Text.Trim();
                    newgroupsrow = newZboziRow;
                    //DataServices.VyrobaDataSet.LoginsDataTable loginsdatatable = new DataServices.VyrobaDataSet.LoginsDataTable();
                    //lta.Insert(textBoxId.Text.Trim(), textBoxName.Text.Trim(), textBoxDescription.Text.Trim());
                    //lta.Insert(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim(), 0);
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
                Production.DataServices.VyrobaDataSet dsV = new DataServices.VyrobaDataSet();
                errorProvider1.Clear();
                // vytváří se nový záznam
                if (groupsrow == null)
                {
                    if (string.IsNullOrEmpty(textBoxId.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxId, "Musíte zadat id skupiny");
                    }

                    // kontrola počtu znaků id
                    if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxId)))
                    {
                        if (textBoxId.Text.Trim().Length > dsV.Groups.idColumn.MaxLength)
                        {
                            errorProvider1.SetError(textBoxId, "ID může mít maximálně " + dsV.Groups.idColumn.MaxLength + " znaků");
                        }
                    }

                    // kontrola existence id
                    if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxId)))
                    {
                        Production.DataServices.VyrobaDataSetTableAdapters.GroupsTableAdapter adapter = new DataServices.VyrobaDataSetTableAdapters.GroupsTableAdapter();
                        adapter.Connection.ConnectionString = Globals.ConnectionString;

                        var groups = adapter.GetDataByID(textBoxId.Text.Trim()); //(x => x.id == textBoxId.Text.Trim());
                        if (groups.Count() > 0)
                        {
                            errorProvider1.SetError(textBoxId, "Zadané id skupiny již existuje");
                        }
                    }
                }

                if (string.IsNullOrEmpty(textBoxName.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxName, "Musíte zadat název skupiny");
                }

                // kontrola počtu znaků name
                if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxName)))
                {
                    if (textBoxName.Text.Trim().Length > dsV.Groups.nameColumn.MaxLength)
                    {
                        errorProvider1.SetError(textBoxName, "Název může mít maximálně " + dsV.Groups.nameColumn.MaxLength + " znaků");
                    }
                }

                // kontrola počtu znaků description
                if (textBoxDescription.Text.Trim().Length > dsV.Groups.descriptionColumn.MaxLength)
                {
                    errorProvider1.SetError(textBoxDescription, "Popis může mít maximálně " + dsV.Groups.descriptionColumn.MaxLength + " znaků");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return IsAllValid();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel2.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        private void FormUzivateleEdit_Shown(object sender, EventArgs e)
        {
            // je úprava záznamu, dojde k načtení dat
            if (groupsrow != null)
            {
                textBoxId.Enabled = false;
                textBoxId.Text = groupsrow.id.Trim();
                textBoxName.Text = groupsrow.name.Trim();
                textBoxDescription.Text = groupsrow.description.Trim();
            }
        }

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxHeslo_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPrijmeni_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxJmeno_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelButtons_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormUzivateleEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }
    }
}
