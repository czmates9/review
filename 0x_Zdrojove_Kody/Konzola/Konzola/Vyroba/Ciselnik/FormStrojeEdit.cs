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
    public partial class FormStrojeEdit : Form
    {
        private Fask.Interfaces.DataSets.Vyroba _newDs;   // drží při životě tabulku pro newmachinesrow
        public Fask.Interfaces.DataSets.Vyroba NewDataSet => _newDs;


        private Fask.Interfaces.IMES providerMachines = null;

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.MachinesRow machinesrow { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.MachinesRow newmachinesrow { get; set; }

        public FormStrojeEdit()
        {
            InitializeComponent();            
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            FormUzivateleEdit_Resize(null, null);

            InitProvider();

            if (providerMachines == null)
                throw new Exception("Provider 'Groups' není inicializován");

        }

        private void InitProvider()
        {
            #region providerGroups
            try
            {

                if (providerMachines == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Groups.IGroups).IsAssignableFrom(t))
                            {
                                providerMachines = (Fask.Interfaces.Vyroba.Groups.IGroups)providerAssemlby.CreateInstance(t.FullName);
                                if (providerMachines != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }
                providerMachines.InitProvider();
           
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
                if (machinesrow != null)
                {
                    machinesrow.name = textBoxName.Text.Trim();
                    machinesrow.description = textBoxDescription.Text.Trim();
                    machinesrow.SKL_ID = tB_StrojSklad.Text.Trim();
                    machinesrow.LOCNCODE = tB_StrojLokace.Text.Trim();

                    newmachinesrow = null; // pro jistotu
                    //lta.Update(groupsrow);
                }
                else   // nový záznam
                {
                    _newDs = new Fask.Interfaces.DataSets.Vyroba(); // uložit do fieldu, ne do lokální proměnné!

                    var newMachinesRow = _newDs.Machines.NewMachinesRow();

                    //newgroupsrow = dataset.Groups.NewGroupsRow();
                    newMachinesRow.id = textBoxId.Text.Trim();
                    newMachinesRow.name = textBoxName.Text.Trim();
                    newMachinesRow.description = textBoxDescription.Text.Trim();
                    newMachinesRow.SKL_ID = tB_StrojSklad.Text.Trim();
                    newMachinesRow.LOCNCODE = tB_StrojLokace.Text.Trim();
                    // KLÍČ: přidat do tabulky => RowState bude Added (ne Detached)
                    _newDs.Machines.AddMachinesRow(newMachinesRow);

                    // a teď ho můžeš vrátit přes parametr
                    newmachinesrow = newMachinesRow;
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

                //int Groups_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vyroba.ColumnsInfo_Groups["id"].MaxLength;
                //int Groups_name_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vyroba.ColumnsInfo_Groups["name"].MaxLength;
                //int Groups_description_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vyroba.ColumnsInfo_Groups["description"].MaxLength;
                

                // vytváří se nový záznam
                if (machinesrow == null)
                {
                    if (string.IsNullOrEmpty(textBoxId.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxId, "Musíte zadat id stroje");
                    }

                    //// kontrola počtu znaků id
                    //if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxId)))
                    //{
                    //    if (textBoxId.Text.Trim().Length > Groups_id_MaxLength)
                    //    {
                    //        errorProvider1.SetError(textBoxId, "ID může mít maximálně " + Groups_id_MaxLength + " znaků");
                    //    }
                    //}

                    // kontrola existence id
                    if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxId)))
                    {
                        //var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.GroupsTableAdapter();
                        //adapter.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                        //var groups = adapter.GetDataByID(textBoxId.Text.Trim()); //(x => x.id == textBoxId.Text.Trim());\\
                        Fask.Interfaces.DataSets.Vyroba.MachinesDataTable machines;

                        if ((providerMachines != null) && providerMachines is Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID)
                            machines = ((Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID)providerMachines).Machines_GetDataByID(textBoxId.Text.Trim());
                        else
                            throw new Exception("Machines_GetDataByID not implementet");

                        if (machines.Count() > 0)
                        {
                            errorProvider1.SetError(textBoxId, "Zadané id stroje již existuje");
                        }
                    }
                }

                if (string.IsNullOrEmpty(textBoxName.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxName, "Musíte zadat název stroje");
                }

                if (string.IsNullOrEmpty(textBoxDescription.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxDescription, "Musíte zadat popis stroje");
                }

                //// kontrola počtu znaků name
                //if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxName)))
                //{
                //    if (textBoxName.Text.Trim().Length > Groups_name_MaxLength)
                //    {
                //        errorProvider1.SetError(textBoxName, "Název může mít maximálně " + Groups_name_MaxLength + " znaků");
                //    }
                //}

                //// kontrola počtu znaků description
                //if (textBoxDescription.Text.Trim().Length > Groups_description_MaxLength)
                //{
                //    errorProvider1.SetError(textBoxDescription, "Popis může mít maximálně " + Groups_description_MaxLength + " znaků");
                //}
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
            if (machinesrow != null)
            {
                textBoxId.Enabled = false;
                textBoxId.Text = machinesrow.id.Trim();
                textBoxName.Text = machinesrow.name.Trim();
                textBoxDescription.Text = machinesrow.description.Trim();

                tB_StrojSklad.Text = machinesrow.IsSKL_IDNull() ? string.Empty: machinesrow.SKL_ID.Trim();
                tB_StrojLokace.Text = machinesrow.IsLOCNCODENull() ? string.Empty : machinesrow.LOCNCODE.Trim();
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
