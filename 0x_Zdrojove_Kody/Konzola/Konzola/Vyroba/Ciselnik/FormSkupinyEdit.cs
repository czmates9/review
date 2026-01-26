using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Vyroba
{
    public partial class FormSkupinyEdit : Form
    {


        private Fask.Interfaces.IMES providerGropus = null;

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.GroupsRow groupsrow { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.GroupsRow newgroupsrow { get; set; }

        public FormSkupinyEdit()
        {
            InitializeComponent();            
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            FormUzivateleEdit_Resize(null, null);

            InitProvider();

            if (providerGropus == null)
                throw new Exception("Provider 'Groups' není inicializován");

        }

        private void InitProvider()
        {
            #region providerGroups
            try
            {

                if (providerGropus == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Groups.IGroups).IsAssignableFrom(t))
                            {
                                providerGropus = (Fask.Interfaces.Vyroba.Groups.IGroups)providerAssemlby.CreateInstance(t.FullName);
                                if (providerGropus != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }
                providerGropus.InitProvider();
           
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

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

                //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.GroupsTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                // je úprava záznamu
                if (groupsrow != null)
                {
                    groupsrow.name = textBoxName.Text.Trim();
                    groupsrow.description = textBoxDescription.Text.Trim();
                    //lta.Update(groupsrow);
                }
                else   // nový záznam
                {
                    Fask.Interfaces.DataSets.Vyroba dataset = new Fask.Interfaces.DataSets.Vyroba();
                    Fask.Interfaces.DataSets.Vyroba.GroupsRow newZboziRow = dataset.Groups.NewGroupsRow();

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
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
                Fask.Interfaces.DataSets.Vyroba dsV = new Fask.Interfaces.DataSets.Vyroba();
                errorProvider1.Clear();

                int Groups_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vyroba.ColumnsInfo_Groups["id"].MaxLength;
                int Groups_name_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vyroba.ColumnsInfo_Groups["name"].MaxLength;
                int Groups_description_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vyroba.ColumnsInfo_Groups["description"].MaxLength;
                

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
                        if (textBoxId.Text.Trim().Length > Groups_id_MaxLength)
                        {
                            errorProvider1.SetError(textBoxId, "ID může mít maximálně " + Groups_id_MaxLength + " znaků");
                        }
                    }

                    // kontrola existence id
                    if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxId)))
                    {
                        //var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.GroupsTableAdapter();
                        //adapter.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                        //var groups = adapter.GetDataByID(textBoxId.Text.Trim()); //(x => x.id == textBoxId.Text.Trim());\\
                        Fask.Interfaces.DataSets.Vyroba.GroupsDataTable groups;

                        if ((providerGropus != null) && providerGropus is Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID)
                            groups = ((Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID)providerGropus).Groups_GetDataByID(textBoxId.Text.Trim());
                        else
                            throw new Exception("IGroups_GetDataByID not implementet");

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
                    if (textBoxName.Text.Trim().Length > Groups_name_MaxLength)
                    {
                        errorProvider1.SetError(textBoxName, "Název může mít maximálně " + Groups_name_MaxLength + " znaků");
                    }
                }

                // kontrola počtu znaků description
                if (textBoxDescription.Text.Trim().Length > Groups_description_MaxLength)
                {
                    errorProvider1.SetError(textBoxDescription, "Popis může mít maximálně " + Groups_description_MaxLength + " znaků");
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
