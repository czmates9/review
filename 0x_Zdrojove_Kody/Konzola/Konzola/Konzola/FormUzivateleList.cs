using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Vyroba_Konzola.Extensions;
using System.Reflection;

namespace Vyroba_Konzola.Konzola
{
    public partial class FormUzivateleList : Form
    {
        private Fask.Console.Interfaces.IVyrobaKonzola providerKonzola = null;
        //private Fask.Console.Interfaces.DataSets.Konzola dsKonzola = new Fask.Console.Interfaces.DataSets.Konzola();
        /// <summary>
        /// Uživatel, který zboží upravuje
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow loginrow { get; set; }
        public Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[bindingSource1].Current)).Row as Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow;

                }
                catch
                {
                    return null;
                }
            }
        }

        public FormUzivateleList()
        {
            InitializeComponent();            
        }

        private void FormUzivateleList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dataGridView1.LoadConfiguration(this.GetType().ToString());

                // inicializace providera
                InitProvider();

                if (providerKonzola == null)
                    throw new Exception("Provider není inicializován");

                UpdateForm();                
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
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Settings.ProviderKonzola))
                {
                    if (providerKonzola == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Vyroba_Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Console.Interfaces.Konzola.IKonzola2).IsAssignableFrom(t))
                                {
                                    providerKonzola = (Fask.Console.Interfaces.Konzola.IKonzola2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerKonzola != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    if ((providerKonzola != null) && (providerKonzola is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                        ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerKonzola).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex, "Load Provider.Konzola");
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
                //System.Data.SqlClient.SqlDataAdapter da_filter = new System.Data.SqlClient.SqlDataAdapter();
                //da_filter.SelectCommand = new System.Data.SqlClient.SqlCommand();
                //da_filter.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection();
                //da_filter.SelectCommand.Connection.ConnectionString = Properties.Settings.Default.Production_ConncetionString;
                //da_filter.SelectCommand.CommandText = "Select l.*, g.name groupname " +
                //    "from Logins l " +
                //    "left join VLoginsGroups vg on l.id = vg.loginid " +
                //    "left join Groups g on vg.groupid = g.id";
                dsKonzola.Clear();
                //da_filter.Fill(this.vyrobaDataSet1.Logins);

                // vytváří se nový záznam, kontrola existence id
                if ((providerKonzola != null) && (providerKonzola is Fask.Console.Interfaces.Konzola.IKonzola2_GetUzivateleAOpravneni))
                    dsKonzola = ((Fask.Console.Interfaces.Konzola.IKonzola2_GetUzivateleAOpravneni)providerKonzola).GetUzivateleAOpravneni();
                else
                    throw new NotImplementedException("Provider neimplementuje IKonzola2_GetUzivateleAOpravneni.");


                bindingSource1.DataSource = dsKonzola;

            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!MySystem.LoginTest.UserLoginAdminTest())
                    return;

                using (Konzola.FormUzivateleEdit frmuziv = new Konzola.FormUzivateleEdit())
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
                if (!MySystem.LoginTest.UserLoginAdminTest())
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (Konzola.FormUzivateleEdit frmuziv = new Konzola.FormUzivateleEdit())
                {
                    frmuziv.loginsrow = SelectedRow;
                    frmuziv.loginsAuthrow = dsKonzola.FASK_Logins_Auth.Where(x => x.id == SelectedRow.id).First();
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
                if (!MySystem.LoginTest.UserLoginAdminTest())
                    return;
                
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedRow.id == Globals.Pracovnik.id)
                {
                    MessageBox.Show("Není možné odstranit aktuálně přihlášeného pracovníka", this.Text, MessageBoxButtons.OK);
                    return;
                }


                //MessageBox.Show("Chcete smazat uživatele " + SelectedRow.firstname + " " + SelectedRow.surname + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MessageBox.Show("Chcete odstranit uživatele " + SelectedRow.firstname.Trim() + " " + SelectedRow.surname.Trim() + "?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;


                if ((providerKonzola != null) && (providerKonzola is Fask.Console.Interfaces.Konzola.IKonzola2_DeleteUzivatelAOpravneni))
                    ((Fask.Console.Interfaces.Konzola.IKonzola2_DeleteUzivatelAOpravneni)providerKonzola).DeleteUzivatelAOpravneni(SelectedRow.id);
                else
                    throw new NotImplementedException("Provider neimplementuje IKonzola2_DeleteUzivatelAOpravneni.");


             UpdateForm();
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

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PerformEditRecord();
        }
    }
}
