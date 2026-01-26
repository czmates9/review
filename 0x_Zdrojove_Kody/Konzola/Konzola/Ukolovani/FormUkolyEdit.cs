using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Konzola;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Ukolovani
{
    public partial class FormUkolyEdit : Form
    {
        private Fask.Interfaces.IMES providerUkol = null;

        /// <summary>
        /// Vynucení aktualizace z důvodu změny záznamu oproti načtenému
        /// </summary>
        public bool forceRefresh { get; set; }

        /// <summary>
        /// Úkol, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow rowUkolEdit { get; set; }

        /// <summary>
        /// nově vytvořený záznam
        /// </summary>
        public Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow rowUkolNew { get; set; }

        /// <summary>
        /// Úkol, který se má duplikovat.
        /// </summary>
        //public Production.DataServices.UkolovaniDataset.CZ_UKOLRow rowUkolDuplikace { get; set; }

        /// <summary>
        /// Jedná se o duplikace, zobrazí se dotaz pro duplikování množství.
        /// </summary>
        public bool Duplikace = false;
        public bool DuplikovatStav = false;

        /// <summary>
        /// Zvolený stav úkolu
        /// </summary>
        private Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATERow rowState
        {
            get
            {
                try
                {
                    return comboBoxState.SelectedItem as Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATERow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public FormUkolyEdit()
        {
            InitializeComponent();            
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormUzivateleEdit_Resize(null, null);

                if (providerUkol == null)
                    throw new Exception("Provider 'Ukolovani' není inicializován");

                Fask.Interfaces.DataSets.Ukolovani dsU = new Fask.Interfaces.DataSets.Ukolovani();

                // naplneni comboboxu priorit
                comboBoxPriority.Items.Add(0);
                comboBoxPriority.Items.Add(1);
                comboBoxPriority.Items.Add(2);
                comboBoxPriority.Items.Add(3);
                comboBoxPriority.SelectedItem = 3;

                // naplneni comboboxu stavu ukolu
                //var adapter = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter();
                ////adapter.Connection.ConnectionString = Globals.ConnectionString;
                //adapter.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //adapter.Fill(dsU.CZ_UKOL_STATE);


                if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL_STATE))
                    ((Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL_STATE)providerUkol).Fill_CZ_UKOL_STATE(dsU);
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Fill_CZ_UKOL_STATE");

                comboBoxState.Items.AddRange(dsU.CZ_UKOL_STATE.Select(null, "State asc"));
                //comboBoxState.ValueMember = "State";
                //comboBoxState.SelectedItem = null;
                int pos = comboBoxState.FindString("N");
                comboBoxState.SelectedIndex = pos;
                LoadData();
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerUkol == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ukolovani.IUkolovani).IsAssignableFrom(t))
                                {
                                    providerUkol = (Fask.Interfaces.Ukolovani.IUkolovani)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerUkol != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerUkol.InitProvider();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadData()
        {
            try
            {
                Fask.Interfaces.DataSets.Ukolovani dsU = new Fask.Interfaces.DataSets.Ukolovani();

                //Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter taState = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOL_STATETableAdapter();
                ////taState.Connection.ConnectionString = Globals.ConnectionString;
                //taState.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                // je úprava záznamu, dojde k načtení dat
                if (rowUkolEdit != null)
                {
                    textBoxName.Text = rowUkolEdit.Name.Trim();
                    textBoxDescription.Text = rowUkolEdit.Description.Trim();
                    textBoxCode.Text = rowUkolEdit.IsCodeNull() ? string.Empty : rowUkolEdit.Code.Trim();
                    dateTimePickerDateFrom.Checked = !rowUkolEdit.IsDateFromNull();
                    if (!rowUkolEdit.IsDateFromNull())
                    {
                        dateTimePickerDateFrom.Value = rowUkolEdit.DateFrom;
                    }

                    dateTimePickerDateTo.Checked = !rowUkolEdit.IsDateToNull();
                    if (!rowUkolEdit.IsDateToNull())
                    {
                        dateTimePickerDateTo.Value = rowUkolEdit.DateTo;
                    }

                    //taState.Fill(dsU.CZ_UKOL_STATE);

                    if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL_STATE))
                        ((Fask.Interfaces.Ukolovani.IUkolovani_Fill_CZ_UKOL_STATE)providerUkol).Fill_CZ_UKOL_STATE(dsU);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Fill_CZ_UKOL_STATE");

                    var state = dsU.CZ_UKOL_STATE.Where(x => x.State == rowUkolEdit.State);
                    if (state.Count() > 0)
                    {
                        //comboBoxState.SelectedValue = state.First().State;
                        //comboBoxState.SelectedItem = state.First();
                        int pos = comboBoxState.FindString(state.First().ToString());
                        comboBoxState.SelectedIndex = pos;
                    }

                    comboBoxPriority.SelectedItem = rowUkolEdit.Priority;
                }
            }
            catch (Exception)
            {
                throw;
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
            //System.Data.SqlClient.SqlTransaction trx = null;
            //var taUkol = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOLTableAdapter();
            //var taUkolUziv = new Production.DataServices.UkolovaniDatasetTableAdapters.CZ_UKOL_UZIVTableAdapter();

            try
            {
                if (!ValidateData())
                    return;

                if (Duplikace)
                {
                    DialogResult dr = MessageBox.Show("Chcete duplikovat i aktuální stav úkolu? Pokud ne, budou veškeré stavy nastaveny do stavu " + rowState.ToString() +  ".", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                        DuplikovatStav = true;
                    else
                        DuplikovatStav = false;
                }

                ////taUkol.Connection.ConnectionString = Globals.ConnectionString;
                //taUkol.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                ////taUkolUziv.Connection.ConnectionString = Globals.ConnectionString;
                //taUkolUziv.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                // je úprava záznamu
                if (!Duplikace && rowUkolEdit != null)
                {
                    //taUkol.Connection.Open();
                    //trx = taUkol.Connection.BeginTransaction(IsolationLevel.Serializable);
                    //taUkol.MyTransaction = trx;

                    // kontrola, zdali se mezitím nezměnil ...                    
                    
                    //var dtUkol = taUkol.GetDataByID(rowUkolEdit.ID);

                    Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLDataTable dtUkol;

                    if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_GetDataByID_CZ_UKOL))
                       dtUkol = ((Fask.Interfaces.Ukolovani.IUkolovani_GetDataByID_CZ_UKOL)providerUkol).GetDataByID(rowUkolEdit.ID);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_GetDataByID_CZ_UKOL");

                    // zaznam nalezen
                    if (dtUkol.Count > 0)
                    {
                        Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_COMPAREDataTable dtCmpUkol = new Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_COMPAREDataTable();
                        dtCmpUkol.ImportRow(rowUkolEdit);
                        dtCmpUkol.ImportRow(dtUkol.First());

                        IEqualityComparer<Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_COMPARERow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(dtCmpUkol[0], dtCmpUkol[1]);
                        if (!isMatch)
                        {
                            if (DialogResult.Yes != MessageBox.Show("Záznam byl od posledního načtení změněn. Přejete si ho přesto upravit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                forceRefresh = true;
                                this.DialogResult = DialogResult.Cancel;
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                        return;
                    }

                    rowUkolEdit.Name = textBoxName.Text.Trim();
                    rowUkolEdit.Description = textBoxDescription.Text.Trim();
                    if (rowUkolEdit.IsCodeNull() && string.IsNullOrEmpty(textBoxCode.Text))
                    {
                        // nic nebylo a neni vyplneno ... nemenit hodnotu
                    }
                    else
                        rowUkolEdit.Code = textBoxCode.Text.Trim();

                    if (dateTimePickerDateFrom.Checked)
                        rowUkolEdit.DateFrom = dateTimePickerDateFrom.Value;
                    else
                        rowUkolEdit.SetDateFromNull();

                    if (dateTimePickerDateTo.Checked)
                        rowUkolEdit.DateTo = dateTimePickerDateTo.Value;
                    else
                        rowUkolEdit.SetDateToNull();

                    rowUkolEdit.State = rowState.State;
                    rowUkolEdit.Priority = Convert.ToInt32(comboBoxPriority.Text.Trim());

                    //taUkol.Update(rowUkolEdit);

                    if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Update_Row_CZ_UKOL))
                       ((Fask.Interfaces.Ukolovani.IUkolovani_Update_Row_CZ_UKOL)providerUkol).UpdateRow(rowUkolEdit);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Update_Row_CZ_UKOL");


                    //if (trx != null)
                    //    trx.Commit();
                }
                else   // nový záznam
                {
                    Fask.Interfaces.DataSets.Ukolovani dataset = new Fask.Interfaces.DataSets.Ukolovani();
                    Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow newRowUkol = dataset.CZ_UKOL.NewCZ_UKOLRow();
                    int id;

                    // vytvari se novy zaznam
                    if (!Duplikace)
                    {
                        newRowUkol.Name = textBoxName.Text.Trim();
                        //if (!string.IsNullOrEmpty(textBoxDescription.Text.Trim()))
                        newRowUkol.Description = string.IsNullOrEmpty(textBoxDescription.Text.Trim()) ? "Nezadáno" : textBoxDescription.Text.Trim(); // dataset.CZ_UKOL.DescriptionColumn.DefaultValue.ToString() : textBoxDescription.Text.Trim();
                        if (!string.IsNullOrEmpty(textBoxCode.Text.Trim()))
                            newRowUkol.Code = textBoxCode.Text.Trim();
                        newRowUkol.CreatorID = Convert.ToInt32(FASK.Logins.Uzivatel.Instance.UserID);
                        newRowUkol.DateCreated = DateTime.Now;
                        if (dateTimePickerDateFrom.Checked)
                            newRowUkol.DateFrom = dateTimePickerDateFrom.Value;
                        if (dateTimePickerDateTo.Checked)
                            newRowUkol.DateTo = dateTimePickerDateTo.Value;
                        newRowUkol.State = rowState.State;
                        // Kind
                        // Type
                        newRowUkol.Priority = Convert.ToInt32(comboBoxPriority.Text.Trim());
                        // PartnerID
                        rowUkolNew = newRowUkol;
                    }
                    else
                    {
                        // zaznam se duplikuje
                        //taUkol.Connection.Open();
                        //taUkolUziv.Connection = taUkol.Connection;

                        //trx = taUkol.Connection.BeginTransaction(IsolationLevel.Serializable);
                        //taUkol.MyTransaction = trx;                        
                        //taUkolUziv.MyTransaction = trx;

                        if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL))
                        {      id=((Fask.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL)providerUkol).Insert
                            (
                            textBoxName.Text.Trim(),
                            string.IsNullOrEmpty(textBoxDescription.Text.Trim()) ? "Nezadáno" : textBoxDescription.Text.Trim(),
                            string.IsNullOrEmpty(textBoxCode.Text.Trim()) ? null : textBoxCode.Text.Trim(),
                            Convert.ToInt32(FASK.Logins.Uzivatel.Instance.UserID),
                            DateTime.Now,
                            dateTimePickerDateFrom.Checked ? dateTimePickerDateFrom.Value : (DateTime?) null,
                            dateTimePickerDateTo.Checked ? dateTimePickerDateTo.Value : (DateTime?) null,
                            rowState.State,
                            null,
                            null,
                            Convert.ToInt32(comboBoxPriority.Text.Trim()),
                            null);
                    }
                        else
                            throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Insert_CZ_UKOL");


                        //int id = Convert.ToInt32(taUkol.InsertQuery(
                        //    textBoxName.Text.Trim(),
                        //    string.IsNullOrEmpty(textBoxDescription.Text.Trim()) ? "Nezadáno" : textBoxDescription.Text.Trim(),
                        //    string.IsNullOrEmpty(textBoxCode.Text.Trim()) ? null : textBoxCode.Text.Trim(),
                        //    Convert.ToInt32(Globals.Pracovnik.id),
                        //    DateTime.Now,
                        //    dateTimePickerDateFrom.Checked ? dateTimePickerDateFrom.Value : (DateTime?) null,
                        //    dateTimePickerDateTo.Checked ? dateTimePickerDateTo.Value : (DateTime?) null,
                        //    rowState.State,
                        //    null,
                        //    null,
                        //    Convert.ToInt32(comboBoxPriority.Text.Trim()),
                        //    null));

                        
                        //var dtUsers = taUkolUziv.GetDataByUkolID(rowUkolEdit.ID);

                        Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable dtUsers;

                        if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_GetDataByID_CZ_UKOL_UZIV))
                            dtUsers = ((Fask.Interfaces.Ukolovani.IUkolovani_GetDataByID_CZ_UKOL_UZIV)providerUkol).GetDataByID(rowUkolEdit.ID);
                        else
                            throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_GetDataByID_CZ_UKOL_UZIV");


                        //taUkol.Update(rowUkolEdit);

                        foreach (var item in dtUsers)
                        {
                            //taUkolUziv.Insert(
                            //    id,
                            //    item.UserID,
                            //    DuplikovatStav ? item.State : rowState.State,
                            //    item.IsDateChangedNull() ? (DateTime?) null : item.DateChanged,
                            //    item.IsUserIDChangedNull() ? (int?) null : item.UserIDChanged,
                            //    item.IsNoteNull() ? null : item.Note,
                            //    item.IsDateNotifyNull() ? (DateTime?) null : item.DateNotify,
                            //    item.IsDateFinishedNull() ? (DateTime?) null : item.DateFinished);

                            if ((providerUkol != null) && (providerUkol is Fask.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL_UZIV))
                            {
                                ((Fask.Interfaces.Ukolovani.IUkolovani_Insert_CZ_UKOL_UZIV)providerUkol).Insert(
    id,
    item.UserID,
    DuplikovatStav ? item.State : rowState.State,
    item.IsDateChangedNull() ? (DateTime?)null : item.DateChanged,
    item.IsUserIDChangedNull() ? (int?)null : item.UserIDChanged,
    item.IsNoteNull() ? null : item.Note,
    item.IsDateNotifyNull() ? (DateTime?)null : item.DateNotify,
    item.IsDateFinishedNull() ? (DateTime?)null : item.DateFinished);
                            }
                            else
                                throw new NotImplementedException("Provider neobsahuje implemetaci IUkolovani_Insert_CZ_UKOL_UZIV");
                        }

                        rowUkolNew = dataset.CZ_UKOL.NewCZ_UKOLRow();

                        // prirazeni id, podle ktereho se nasledne automaticky vybere zvoleny zaznam
                        rowUkolNew.ID = id;

                        //if (trx != null)
                        //    trx.Commit();
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                //try
                //{
                //    if (trx != null)
                //        trx.Rollback();
                //}
                //catch (Exception ex2)
                //{
                //    Log.Write(ex2);
                //}
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //if ((taUkol.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                //    taUkol.Connection.Close();
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
                Fask.Interfaces.DataSets.Ukolovani dsU = new Fask.Interfaces.DataSets.Ukolovani();
                errorProvider1.Clear();
                // ID neresit - autoinkrement
                // Name - resit
                // Description - neresit
                // Code neresit
                // DateCreated - neresit
                // DateFrom - neresit
                // DateTo - neresit (pouze kontrola, pokud je vyplneno oboje, tak aby bylo vetsi nez DateFrom)
                // State - musi byt zvolen
                // Kind ?? nevim co to je
                // Type ?? nevim co to je
                // Priority - defaultne zvolit prioritu 3
                // PartnerID ?? nevim co to je

                // vytváří se nový záznam
                if (rowUkolEdit == null)
                {
                    if (string.IsNullOrEmpty(textBoxName.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxName, "Musíte zadat název úkolu");
                    }

                    int CZ_UKOL_Name_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Ukoly.ColumnsInfo_CZ_UKOL["Name"].MaxLength;

                    // kontrola počtu znaků id
                    if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxName)))
                    {
                        if (textBoxName.Text.Trim().Length > CZ_UKOL_Name_MaxLength)
                        {
                            errorProvider1.SetError(textBoxName, "Název úkolu může mít maximálně " + CZ_UKOL_Name_MaxLength + " znaků");
                        }
                    }
                }

                // datum do je vybrano a mensi, nez soucasne datum
                if (dateTimePickerDateTo.Checked && dateTimePickerDateTo.Value <= DateTime.Now)
                {
                    errorProvider1.SetError(dateTimePickerDateTo, "Datum platnosti do je větší než aktuální datum");
                }

                // je vybrano datum od i datum do
                if (dateTimePickerDateFrom.Checked && dateTimePickerDateTo.Checked)
                {
                    // datum do je mensi nez datum od
                    if (dateTimePickerDateTo.Value <= dateTimePickerDateFrom.Value)
                    {
                        if (string.IsNullOrEmpty(errorProvider1.GetError(dateTimePickerDateTo)))
                        {
                            errorProvider1.SetError(dateTimePickerDateTo, "Datum platnosti do je menší než datum platnosti od");
                        }
                    }
                }

                if (rowState == null)
                {
                    errorProvider1.SetError(comboBoxState, "Stav úkolu není zvolen");
                }

                // neni zvolena priorita ... ale muze byt napsana rucne
                if (comboBoxPriority.SelectedItem == null)
                {
                    if (string.IsNullOrEmpty(comboBoxPriority.Text.Trim()))
                    {
                        errorProvider1.SetError(comboBoxPriority, "Priorita úkolu není zvolena");
                    }
                    else
                    {
                        // neco je zadano ...
                        int id;
                        bool status = Int32.TryParse(comboBoxPriority.Text.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out id);
                        if (status)
                        {
                            if (id < 0)
                            {
                                errorProvider1.SetError(comboBoxPriority, "Priorita musí být kladné číslo");
                            }
                        }
                        else
                            errorProvider1.SetError(comboBoxPriority, "Priorita musí být číslo");
                    }
                    
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
