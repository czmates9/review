using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.IO;
using Production.Extensions;

namespace Production.Forms
{
    public partial class FormProductionList : Form
    {
        /// <summary>
        /// Zvolený VPH záznam.
        /// </summary>
        private Production.DataServices.VyrobaDataSet.CZPRO_VPHRow rowVPH
        {
            get
            {
                try
                {
                    //return ((DataRowView)(comboBoxVyrobniPrikaz.SelectedItem)).Row as Production.DataServices.VyrobaDataSet.CZPRO_VPHRow;
                    return comboBoxVyrobniPrikaz.SelectedItem as Production.DataServices.VyrobaDataSet.CZPRO_VPHRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

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

        /// <summary>
        /// Zvolený stroj
        /// </summary>
        private Production.DataServices.VyrobaDataSet.MachinesRow rowMachine
        {
            get
            {
                try
                {
                    return comboBoxStroj.SelectedItem as Production.DataServices.VyrobaDataSet.MachinesRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolená operace
        /// </summary>
        private Production.DataServices.VyrobaDataSet.OperationsRow rowOperation
        {
            get
            {
                try
                {
                    return comboBoxOperace.SelectedItem as Production.DataServices.VyrobaDataSet.OperationsRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolený Production záznam.
        /// </summary>
        private Production.DataServices.VyrobaDataSet.ProductionRow rowProduct
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[this.bindingSourceProduct].Current)).Row as Production.DataServices.VyrobaDataSet.ProductionRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private Production.DataServices.VyrobaDataSet.LoginsRow rowUzivatel
        {
            get
            {
                try
                {
                    //return ((DataRowView)(comboBoxUzivatel.SelectedItem)).Row as Production.DataServices.VyrobaDataSet.LoginsRow;
                    return comboBoxUzivatel.SelectedItem as Production.DataServices.VyrobaDataSet.LoginsRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private Production.DataServices.KonzolaDataSet.FASK_CONS_095Row rowZbozi
        {
            get
            {
                try
                {
                    //return ((DataRowView)(comboBoxZbozi.SelectedItem)).Row as Production.DataServices.KonzolaDataSet.FASK_CONS_095Row;
                    return comboBoxZbozi.SelectedItem as Production.DataServices.KonzolaDataSet.FASK_CONS_095Row;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }



        public FormProductionList()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
        }

        private void FormProductionList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // naplnění comboboxu zakázek
                var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                adapter.Connection.ConnectionString = Globals.ConnectionString;
                adapter.Fill(this.vyrobaDataSet1.CZPRO_VPH);
                comboBoxVyrobniPrikaz.Items.AddRange(this.vyrobaDataSet1.CZPRO_VPH.Select(null, "SOPNUMBE asc"));
                comboBoxVyrobniPrikaz.SelectedItem = null;

                // naplnění comboboxu zakázek
                var daGroups = new Production.DataServices.VyrobaDataSetTableAdapters.GroupsTableAdapter();
                daGroups.Connection.ConnectionString = Globals.ConnectionString;
                daGroups.Fill(this.vyrobaDataSet1.Groups);
                comboBoxSkupina.Items.AddRange(this.vyrobaDataSet1.Groups.Select(null, "name asc"));
                comboBoxSkupina.SelectedItem = null;

                // naplnění comboboxu strojů
                var daMachines = new Production.DataServices.VyrobaDataSetTableAdapters.MachinesTableAdapter();
                daMachines.Connection.ConnectionString = Globals.ConnectionString;
                daMachines.Fill(this.vyrobaDataSet1.Machines);
                comboBoxStroj.Items.AddRange(this.vyrobaDataSet1.Machines.Select(null, "name asc"));
                comboBoxStroj.SelectedItem = null;

                var daOperations = new Production.DataServices.VyrobaDataSetTableAdapters.OperationsTableAdapter();
                daOperations.Connection.ConnectionString = Globals.ConnectionString;
                daOperations.Fill(this.vyrobaDataSet1.Operations);
                comboBoxOperace.Items.AddRange(this.vyrobaDataSet1.Operations.Select(null, "name asc"));
                comboBoxOperace.SelectedItem = null;

                if (Globals.PouzivatTabulkuZbozi)
                {
                    // naplnění comboboxu zboží
                    var adapterZbozi = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
                    adapterZbozi.Connection.ConnectionString = Globals.ConnectionString;
                    adapterZbozi.Fill(this.konzolaDataSet1.FASK_CONS_095);
                    //comboBoxZbozi.DataSource = this.konzolaDataSet1.FASK_CONS_095;
                    //comboBoxZbozi.DisplayMember = "ITEMNMBR, ITEMDESC";
                    //comboBoxZbozi.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                    comboBoxZbozi.Items.AddRange(this.konzolaDataSet1.FASK_CONS_095.Select(null, "ITEMDESC asc"));
                    //foreach (var item in this.konzolaDataSet1.FASK_CONS_095)
                    //{
                    //    comboBoxZbozi.Items.Add(item);
                    //    comboBoxZbozi.AutoCompleteCustomSource.Add(item.ToString());
                    //}
                    comboBoxZbozi.SelectedItem = null;
                }
                else comboBoxZbozi.Enabled = false;
                //comboBoxZbozi.DisplayMember = "ITEMDESC";
                //comboBoxZbozi.ValueMember = "ITEMNMBR";
                //this.bindingSourceProduct.DataSource = null;

                var adapterUzivatel = new Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
                adapterUzivatel.Connection.ConnectionString = Globals.ConnectionString;
                adapterUzivatel.Fill(this.vyrobaDataSet1.Logins);
                //comboBoxUzivatel.DataSource = this.vyrobaDataSet1.Logins;
                comboBoxUzivatel.Items.AddRange(this.vyrobaDataSet1.Logins.Select(null, "surname asc, firstname asc"));
                comboBoxUzivatel.SelectedItem = null;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dataGridView1.LoadConfiguration(this.GetType().ToString());
                //Production.Extensions.DataGridViewColumnSelector cs = new DataGridViewColumnSelector(this.dataGridView1);

                // nastavení času
                dateTimePickerDatumDo.Value = dateTimePickerDatumOd.Value = DateTime.Now.AddSeconds(-DateTime.Now.Second);
                dateTimePickerDatumDo.Checked = false;
                dateTimePickerDatumOd.Checked = false;
                
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void textBoxVyrobniPrikaz_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    comboBoxVyrobniPrikaz.SelectedValue = textBoxVyrobniPrikaz.Text.Trim();
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void comboBoxVyrobniPrikaz_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rowVPH != null)
            //    label4.Text = rowVPH.SOPNUMBE.Trim();
            //else
            //    label4.Text = string.Empty;


            //try
            //{
            //    //if (comboBoxVyrobniPrikaz.SelectedIndex == -1)  // nic není zvoleno
            //    //    return;
            //    //if (rowVPH == null)      // nic není zvoleno
            //    //{
            //    //    textBoxVyrobniPrikaz.Text = string.Empty;
            //    //    textBoxZbozi.Text = string.Empty;
            //    //    comboBoxZbozi.SelectedItem = null;
            //    //    comboBoxZbozi.DataSource = null;
            //    //    return;
            //    //}
            //    if (comboBoxVyrobniPrikaz.SelectedIndex != -1)
            //    {
            //        textBoxVyrobniPrikaz.Text = rowVPH.SOPNUMBE.Trim();
            //        var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
            //        adapter.Connection.ConnectionString = Globals.ConnectionString;
            //        var dataVPP = adapter.GetDataByCountEntriesAndSOPNUMBE(rowVPH.CountEntries, rowVPH.SOPNUMBE);
            //        comboBoxZbozi.DataSource = dataVPP;
            //        comboBoxZbozi.SelectedIndex = -1;
            //        comboBoxZbozi.Text = string.Empty;
            //    }
            //    else
            //    {
            //        textBoxVyrobniPrikaz.Text = string.Empty;
            //        textBoxZbozi.Text = string.Empty;
            //        comboBoxZbozi.SelectedIndex = -1;
            //        comboBoxZbozi.DataSource = null;
            //    }

                
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}            
        }

        private void comboBoxZbozi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rowZbozi != null)
            //    label5.Text = rowZbozi.ToString();
            //else
            //    label5.Text = string.Empty;

            //try
            //{
            //    if (rowVPP == null) // není zvolené VPP  ... odstranit data z datagridviewu
            //    {
            //        textBoxZbozi.Text = string.Empty;
            //        this.bindingSourceProduct.DataSource = null;
            //        //this.dataGridView1.DataSource = null;
            //    }
            //    else
            //    {
            //        textBoxZbozi.Text = rowVPP.ITEMNMBR;

            //        var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //        adapter.Connection.ConnectionString = Globals.ConnectionString;
            //        var dataVPP = adapter.GetDataByCountEntriesAndSOPNUMBEandITEMNMBR(rowVPH.CountEntries, rowVPH.SOPNUMBE, rowVPP.ITEMNMBR);
            //        this.bindingSourceProduct.DataSource = dataVPP;
            //        //this.dataGridView1.DataSource = dataVPP;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}            
        }

        private void comboBoxUzivatel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rowUzivatel != null)
            //    label6.Text = rowUzivatel.ToString();
            //else
            //    label6.Text = string.Empty;
        }

        private void textBoxZbozi_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    comboBoxZbozi.SelectedValue = textBoxZbozi.Text;
            //    //if(comboBoxZbozi.Items.Contains(textBoxVyrobniPrikaz.Text.Trim()))
            //    //{
            //    //    comboBoxZbozi.SelectedValue = textBoxVyrobniPrikaz.Text.Trim();
            //    //}
            //    //else
            //    //{
            //    //    comboBoxZbozi.SelectedItem = null;
            //    //}
                
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void FormProductionList_KeyDown(object sender, KeyEventArgs e)
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

        private void upravitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PerformEditRecord();
        }

        private void PerformEditRecord()
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (rowProduct.IsTIMESTOPNull())
                {
                    MessageBox.Show("Zvolený záznam obsahuje pouze zahájení výroby", this.Text, MessageBoxButtons.OK);
                    return;
                }
                if (!rowProduct.IsTIMECORSTARTNull())
                {
                    MessageBox.Show("Korekci nelze upravovat", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormProductionEdit frmuziv = new FormProductionEdit())
                {
                    frmuziv.rowProduct = rowProduct;
                    frmuziv.Text = "Úprava výroby";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    var lta = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                    lta.Connection.ConnectionString = Globals.ConnectionString;
                    //lta.Delete(SelectedRow.ITEMNMBR);
                    lta.Update(frmuziv.rowProduct);
                    //lta.Update(this.vyrobaDataSet1.Production);
                    this.vyrobaDataSet1.AcceptChanges();
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

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformOK();            
        }

        private void PerformOK()
        {
            try
            {
                DataTable dtchanged = this.vyrobaDataSet1.Production.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje zmeny, pokracovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                //var adapterProduction = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                //adapterProduction.

                System.Data.SqlClient.SqlDataAdapter da_filter = new System.Data.SqlClient.SqlDataAdapter();
                da_filter.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da_filter.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection();
                da_filter.SelectCommand.Connection.ConnectionString = Globals.ConnectionString;
                //da_filter.SelectCommand.CommandText =
                //    "Select l.firstname, l.surname, z.ITEMDESC, p.* from Production p" +
                //    " left join FASK_CONS_095 z on z.itemnmbr = p.itemnmbr" +
                //    " left join Logins l on l.id = p.userid" +
                //    " Where";
                da_filter.SelectCommand.CommandText =
                    "Select l.firstname, l.surname,";
                if (Globals.PouzivatTabulkuZbozi)
                    da_filter.SelectCommand.CommandText += "z.ITEMDESC,";
                da_filter.SelectCommand.CommandText += "o.name operationName, m.name machineName, g.name groupName, p.* from Production p";


                if (Globals.PouzivatTabulkuZbozi)
                {
                    da_filter.SelectCommand.CommandText += " left join FASK_CONS_095 z on z.itemnmbr = p.itemnmbr";
                }

                da_filter.SelectCommand.CommandText += " left join Logins l on l.id = p.userid" +
                    " left join Operations o on o.id = p.operationid" + 
                    " left join Machines m on m.id = p.machineid" + 
                    " left join VLoginsGroups vg on l.id = vg.loginid" +
                    " left join Groups g on vg.groupid = g.id" +
                    " Where";
                // číslo zakázky
                if (rowVPH != null)
                {
                    da_filter.SelectCommand.CommandText += " p.SOPNUMBE = @SOPNUMBE";
                }
                else
                {
                    da_filter.SelectCommand.CommandText += " isnull(p.SOPNUMBE, '') like '%' + @SOPNUMBE + '%'";
                }
                da_filter.SelectCommand.Parameters.AddWithValue("@SOPNUMBE", rowVPH != null ? rowVPH.SOPNUMBE.Trim() : comboBoxVyrobniPrikaz.Text);

                if (Globals.PouzivatTabulkuZbozi)
                {
                    // hledaní zboží
                    if (rowZbozi != null)
                    {
                        da_filter.SelectCommand.CommandText += " AND p.ITEMNMBR=@itemdesc";
                    }
                    else
                    {
                        da_filter.SelectCommand.CommandText += " AND p.ITEMNMBR IN (" +
                        " select ITEMNMBR from FASK_CONS_095" +
                        " where ITEMDESC like '%' + @itemdesc + '%'" +
                        " union" +
                        " select ITEMNMBR from FASK_CONS_095" +
                        " where ITEMNMBR like '' + @itemdesc + '%' " +
                        " )";
                    }
                    da_filter.SelectCommand.Parameters.AddWithValue("@itemdesc", rowZbozi != null ? rowZbozi.ITEMNMBR.Trim() : comboBoxZbozi.Text);
                }

                // hledání uživatele
                if (!string.IsNullOrEmpty(comboBoxUzivatel.Text))
                {
                    if (comboBoxUzivatel.SelectedItem != null)
                    {
                        da_filter.SelectCommand.CommandText += " AND p.UserID=@name";
                    }
                    else
                    {
                        da_filter.SelectCommand.CommandText += " AND p.UserID IN (" +
                        " select distinct id from Logins" +
                        " where firstname like '%' + @name + '%'" +
                        " union" +
                        " select distinct id from Logins" +
                        " where surname like '%' + @name + '%'" +
                        " union" +
                        " select distinct id from Logins" +
                        " where id = @name" +
                        " )";
                    }
                    da_filter.SelectCommand.Parameters.AddWithValue("@name", rowUzivatel != null ? rowUzivatel.id.Trim() : comboBoxUzivatel.Text);
                }

                // hledání skupiny
                if (!string.IsNullOrEmpty(comboBoxSkupina.Text))
                {
                    if (comboBoxSkupina.SelectedItem != null)
                    {
                        da_filter.SelectCommand.CommandText += " AND g.name=@groupname";
                    }
                    else
                    {
                        da_filter.SelectCommand.CommandText += " AND isnull(g.name, '') like '%' + @groupname + '%'";
                    }
                    da_filter.SelectCommand.Parameters.AddWithValue("@groupname", rowGroups != null ? rowGroups.name : comboBoxSkupina.Text);
                }

                // hledání podle stroje
                if (!string.IsNullOrEmpty(comboBoxStroj.Text))
                {
                    if (comboBoxStroj.SelectedItem != null)
                    {
                        da_filter.SelectCommand.CommandText += " AND m.name=@machinename";
                    }
                    else
                    {
                        da_filter.SelectCommand.CommandText += " AND isnull(m.name, '') like '%' + @machinename + '%'";
                    }
                    da_filter.SelectCommand.Parameters.AddWithValue("@machinename", rowMachine != null ? rowMachine.name : comboBoxStroj.Text);
                }

                // hledání podle operace
                if (!string.IsNullOrEmpty(comboBoxOperace.Text))
                {
                    if (comboBoxOperace.SelectedItem != null)
                    {
                        da_filter.SelectCommand.CommandText += " AND o.name=@operationname";
                    }
                    else
                    {
                        da_filter.SelectCommand.CommandText += " AND isnull(o.name, '') like '%' + @operationname + '%'";
                    }
                    da_filter.SelectCommand.Parameters.AddWithValue("@operationname", rowOperation != null ? rowOperation.name : comboBoxOperace.Text);
                }

                // hledání podle datumu
                if (dateTimePickerDatumOd.Checked && dateTimePickerDatumDo.Checked)
                {
                    da_filter.SelectCommand.CommandText += " AND dateeve between @datumOd and @datumDo";
                    da_filter.SelectCommand.Parameters.AddWithValue("@datumOd", dateTimePickerDatumOd.Value);
                    da_filter.SelectCommand.Parameters.AddWithValue("@datumDo", dateTimePickerDatumDo.Value);
                }
                else
                {
                    if (dateTimePickerDatumOd.Checked)
                    {
                        da_filter.SelectCommand.CommandText += " AND dateeve > @datumOd";
                        da_filter.SelectCommand.Parameters.AddWithValue("@datumOd", dateTimePickerDatumOd.Value);
                    }
                    else if (dateTimePickerDatumDo.Checked)
                    {
                        da_filter.SelectCommand.CommandText += " AND dateeve < @datumDo";
                        da_filter.SelectCommand.Parameters.AddWithValue("@datumDo", dateTimePickerDatumDo.Value);
                    }
                }
                // zobrazit vsechny zakazky
                if (checkBoxOdvadeniVse.Checked)
                {
                    da_filter.SelectCommand.CommandText += " AND p.SOUBEHGUID is not null";
                }

                // zobrazit vsechny korekce
                if (checkBoxKorekceVse.Checked)
                {
                    da_filter.SelectCommand.CommandText += " AND p.CORRGUID is not null";
                }

                // zobrazit nedokonceny odvod vyroby
                if (checkBoxOdvadeniNedokoncene.Checked)
                {
                    da_filter.SelectCommand.CommandText += " AND p.SOUBEHGUID not IN (" +
                    " select distinct SOUBEHGUID from Production" +
                    " where" +
                    " SOUBEHGUID is not null and TIMESTOP is not null" +
                    " )";
                }
                // zobrazit nedokoncene korekce
                if (checkBoxKorekceNedokoncene.Checked)
                {
                    //da_filter.SelectCommand.CommandText += " AND p.CORRGUID not IN (" +
                    //" select distinct CORRGUID from Production" +
                    //" where" +
                    //" CORRGUID is not null and TIMECORSTOP is not null" +
                    //" )";
                    da_filter.SelectCommand.CommandText += " AND not exists ( " +
                    "select SOUBEHGUID " +
                    "from Production " +
                    "where " +
                    "(TIMESTOP is not null or TIMEPREPSTOP is not null) " +
                    "and " +
                    "SOUBEHGUID=p.SOUBEHGUID " +
                    ") " +
                    "and SOUBEHGUID is not null "
                    ;
                }

                da_filter.SelectCommand.CommandText += " order by dateeve desc";
                //Point p = Point.Empty;
                //try
                //{
                //    DataGridViewCell currentCell = this.dataGridView1.CurrentCell;
                //    p = new Point(currentCell.ColumnIndex, currentCell.RowIndex);
                //}
                //catch
                //{
                //}
                // test
                int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
                //int SelectedRowIndex = 0;
                //if (this.dataGridView1.SelectedRows.Count > 0) SelectedRowIndex = this.dataGridView1.SelectedRows[0].Index; //Save Current Selected Row Index

                this.vyrobaDataSet1.Production.Clear();
                this.vyrobaDataSet1.Production.AcceptChanges();

                this.vyrobaDataSet1.Production.BeginLoadData();
                //naplnim data ...
                da_filter.Fill(this.vyrobaDataSet1.Production);

                this.vyrobaDataSet1.Production.EndLoadData();

                //test
                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
                //if ((this.dataGridView1.Rows.Count - 1) >= SelectedRowIndex) this.dataGridView1.Rows[SelectedRowIndex].Selected = true; //Restore Selected Row
                //try
                //{
                //    this.dataGridView1.CurrentCell = this.dataGridView1[p.X, p.Y];
                //}
                //catch
                //{
                //}
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void bindingSourceZbozi_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void schvalitVybraneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na schválení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete schválit vybrané záznamy?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                var lta = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                lta.Connection.ConnectionString = Globals.ConnectionString;
                //lta.Delete(SelectedRow.ITEMNMBR);
                //lta.Update(frmuziv.rowProduct);
                ////lta.Update(this.vyrobaDataSet1.Production);
                //this.vyrobaDataSet1.AcceptChanges();

                //foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                //{

                //}
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    Production.DataServices.VyrobaDataSet.ProductionRow row = ((item.DataBoundItem as DataRowView).Row) as Production.DataServices.VyrobaDataSet.ProductionRow;
                    //Production.DataServices.VyrobaDataSet.ProductionRow row = item.DataBoundItem as Production.DataServices.VyrobaDataSet.ProductionRow;
                    //Production.DataServices.VyrobaDataSet.ProductionRow row = (Production.DataServices.VyrobaDataSet.ProductionRow) item;
                    // time stop je vyplnen, je potreba uprava ...
                    if (!row.IsTIMESTOPNull())
                    {
                        if (row.IsqtyOldNull())
                            row.qtyOld = row.qty;
                        row.idVS = Globals.Pracovnik.id;
                        row.dateedit = DateTime.Now;
                    }
                }
                lta.Update(this.vyrobaDataSet1.Production);
                this.vyrobaDataSet1.AcceptChanges();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.SelectAll();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProductionList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void checkBoxZakazky_CheckedChanged(object sender, EventArgs e)
        {
            // odškrtnutí filtru na zakázky
            //if (checkBoxZakazky.Checked)
            //    checkBoxNedokonceneKorekce.Checked = false;
        }

        private void checkBoxNedokonceneKorekce_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBoxNedokonceneKorekce.Checked)
            //    checkBoxZakazky.Checked = false;

        }

        private void buttonUpravit2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (rowProduct.IsTIMESTOPNull())
                //{
                //    MessageBox.Show("Zvolený záznam obsahuje pouze zahájení výroby", this.Text, MessageBoxButtons.OK);
                //    return;
                //}
                //if (!rowProduct.IsTIMECORSTARTNull())
                //{
                //    MessageBox.Show("Korekci nelze upravovat", this.Text, MessageBoxButtons.OK);
                //    return;
                //}

                using (FormProductionEdit2 frmuziv = new FormProductionEdit2())
                {
                    frmuziv.rowProduct = rowProduct;
                    frmuziv.Text = "Úprava výroby";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                    //lta.Connection.ConnectionString = Globals.ConnectionString;
                    //lta.Delete(SelectedRow.ITEMNMBR);
                    //lta.Update(frmuziv.rowProduct);
                    //lta.Update(this.vyrobaDataSet1.Production);
                    //this.vyrobaDataSet1.AcceptChanges();
                }
                // opetovne vyhledani zaznamu
                PerformOK();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUkoncitZakazky_Click(object sender, EventArgs e)
        {
            try
            {
                Production.DataServices.VyrobaDataSet dsOdvadeni = new DataServices.VyrobaDataSet();
                Production.DataServices.VyrobaDataSet dsKorekce = new DataServices.VyrobaDataSet();
                Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter taProduction = new DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                taProduction.Connection.ConnectionString = Globals.ConnectionString;

                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro ukončení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //List<Production.DataServices.VyrobaDataSet.ProductionRow> listProduction = new List<DataServices.VyrobaDataSet.ProductionRow>();

                //// výběr veškerých SOUBEHGUID a CORRGUID            
                //foreach (var item in this.dataGridView1.SelectedRows)
                //{
                //    listProduction.Add(item as Production.DataServices.VyrobaDataSet.ProductionRow);                
                //}
                //// unikátní guid
                //var selectedSOUBEHGUID = listProduction.Select(x => x.SOUBEHGUID).Distinct();
                //var selectedCORRGUID = listProduction.Select(x => x.CORRGUID).Distinct();
                //// najití všech dat podle zvolených SOUBEHGUID
                //foreach (var item in selectedSOUBEHGUID)
                //{
                //    //taProduction.FillBySOUBEHGUID(dsOdvadeni.Production, item);
                //    dsOdvadeni.Production.Merge(taProduction.GetDataBySOUBEHGUID(item));
                //}
                //// najití všech dat podle zvolených CORRGUID
                //foreach (var item in selectedCORRGUID)
                //{
                //    //taProduction.FillByCORRGUID(dsKorekce.Production, item);
                //    dsKorekce.Production.Merge(taProduction.GetDataByCORRGUID(item));
                //}

                //var finishedSOUBEHGUID = dsOdvadeni.Production.Where(x => !x.IsSOUBEHGUIDNull() && !x.IsTIMESTOPNull());
                //var unfinishedSOUBEHGUID = dsOdvadeni.Production.Where(x => !x.IsSOUBEHGUIDNull() && !x.IsTIMESTOPNull());

                //var finishedCORRGUID = dsKorekce.Production.Where(x=> !x.IsCORRGUIDNull()
                DateTime dtNow = DateTime.Now;
                DateTime dtStop;    // zadane datum ukonceni
                Decimal dMnozstvi;  // zadane mnozstvi
                if (this.dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné ukončovat zakázky/korekce pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                // konec korekce
                if (!rowProduct.IsTIMECORSTARTNull() && !rowProduct.IsTIMECORSTOPNull())
                {
                    MessageBox.Show("Korekce již byla ukončena", this.Text, MessageBoxButtons.OK);
                    return;
                }

                // ukonceni odvadeni
                if (!rowProduct.IsTIMESTARTNull() && (!rowProduct.IsTIMESTOPNull()))
                {
                    MessageBox.Show("Odvádění již bylo dokončeno", this.Text, MessageBoxButtons.OK);
                    return;
                }


                // zacatek odvadeni
                if (!rowProduct.IsTIMESTARTNull() && rowProduct.IsTIMESTOPNull() && !rowProduct.IsSOUBEHGUIDNull())
                {
                    // nacteni vsech zaznamu podle soubehguid
                    //dsOdvadeni.Production.Merge(taProduction.GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID));
                    var dataOdvedena = taProduction.GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID);
                    // najiti dokonceneho odvadeni
                    var odvodUkoncen = dataOdvedena.Where(x => !x.IsTIMESTOPNull());
                    if (odvodUkoncen.Count() > 0)
                    {
                        MessageBox.Show("Odvádění již bylo dokončeno", this.Text, MessageBoxButtons.OK);
                        return;
                    }
                    else   // dokonceni odvadeni
                    {
                        using (FormUkoncitZakazkuOdvadeni frmUkoncit = new FormUkoncitZakazkuOdvadeni())
                        {
                            frmUkoncit.Text = "Ukončení zakázky";
                            frmUkoncit.rowProduction = rowProduct;
                            if (frmUkoncit.ShowDialog(this) != DialogResult.OK)
                                return;
                            dtStop = frmUkoncit.Datum;
                            dMnozstvi = frmUkoncit.Mnozstvi;
                        }

                        foreach (var item in dataOdvedena)
                        {
                            Production.DataServices.VyrobaDataSet.ProductionRow nRow = dsOdvadeni.Production.NewProductionRow();
                            if (!item.IsCountEntriesNull())
                                nRow.CountEntries = item.CountEntries;
                            if (!item.IsSOPNUMBENull())
                                nRow.SOPNUMBE = item.SOPNUMBE;
                            if (!item.IsITEMNMBRNull())
                                nRow.ITEMNMBR = item.ITEMNMBR;
                            if (!item.IsITEMTYPENull())
                                nRow.ITEMTYPE = item.ITEMTYPE;
                            if (!item.IsITEMMJNull())
                                nRow.ITEMMJ = item.ITEMMJ;
                            if (!item.IsORDNull())
                                nRow.ORD = item.ORD; ;
                            if (!item.IsTIMEMODENull())
                                nRow.TIMEMODE = item.TIMEMODE;
                            if (!item.IsTIMEPREPSTARTNull())
                                nRow.TIMEPREPSTART = item.TIMEPREPSTART;
                            if (!item.IsTIMEPREPSTOPNull())
                                nRow.TIMEPREPSTOP = item.TIMEPREPSTOP;
                            if (!item.IsTIMEPREPNull())
                                nRow.TIMEPREP = item.TIMEPREP;
                            if (!item.IsTIMEUNITNull())
                                nRow.TIMEUNIT = item.TIMEUNIT;
                            if (!item.IsTIMESTARTNull())
                                nRow.TIMESTART = item.TIMESTART;
                            nRow.TIMESTOP = dtStop;             // Upravuje se!!
                            //if (!item.IsTIMESTOPNull())
                            //    nRow.TIMESTOP = item.TIMESTOP;
                            if (!item.IsTIMECORSTARTNull())
                                nRow.TIMECORSTART = item.TIMECORSTART;
                            if (!item.IsTIMECORSTOPNull())
                                nRow.TIMECORSTOP = item.TIMECORSTOP;
                            if (!item.IsTIMECORNull())
                                nRow.TIMECOR = item.TIMECOR;
                            if (!item.IsTIMECRIDNull())
                                nRow.TIMECRID = item.TIMECRID;
                            // id neupravovat
                            nRow.loginid = item.loginid;
                            if (!item.IsmachineidNull())
                                nRow.machineid = item.machineid;
                            if (!item.IsoperationidNull())
                                nRow.operationid = item.operationid;
                            // dateeve neupravovat
                            nRow.dateeve = dtStop;      // nastavuje se datum ukončení a ne datum události!!
                            nRow.qtyReal = nRow.qty = dMnozstvi;                  // Upravuje se !!
                            //nRow.qtyReal = item.qtyReal;
                            if (!item.IsQTYPACKNull())
                                nRow.QTYPACK = item.QTYPACK;
                            if (!item.IsQTYPACKMJNull())
                                nRow.QTYPACKMJ = item.QTYPACKMJ;
                            if (!item.IsdescriptionNull())
                                nRow.description = item.description;
                            if (!item.IsBarcodePNull())
                                nRow.BarcodeP = item.BarcodeP;
                            nRow.UserID = item.UserID;
                            nRow.TermID = item.TermID;
                            if (!item.IsISOKNull())
                                nRow.ISOK = item.ISOK;
                            nRow.GUID = Guid.NewGuid();
                            nRow.SOUBEHGUID = item.SOUBEHGUID;
                            // SOUBEHGUID
                            // CORRGUID
                            // porovnani qtyOld oproti predchozimu stavu
                            //if ((_producttype == PRODUCTIONTYPE.ODVOD_STOP) && (rowProduct.IsqtyOldNull()))
                            //    item.qtyOld = Convert.ToDecimal(textBoxQty.Text);
                            //if (!item.IsTIMEPREPSTARTNull())
                            //    nRow.TIMEPREPSTART = dateTimePickerTIMEPREPSTART.Value;
                            //if (!item.IsTIMEPREPSTOPNull())
                            //    nRow.TIMEPREPSTOP = dateTimePickerTIMEPREPSTOP.Value;
                            //if (!item.IsTIMECORSTARTNull())
                            //    nRow.TIMECORSTART = dateTimePickerTIMECORSTART.Value;
                            //if (!item.IsTIMECORSTOPNull())
                            //    nRow.TIMECORSTOP = dateTimePickerTIMECORSTOP.Value;

                            nRow.idVS = Globals.Pracovnik != null ? Globals.Pracovnik.id : string.Empty;
                            nRow.dateedit = dtNow;
                            //nRow.TIMESTOP = DateTime.Now;   // TODO: pridat doplnovani
                            //nRow.dateedit = dtNow;
                            //nRow.TIMEPREP = 0;
                            //nRow.qty = item.qtyReal = 99;   // TODO: pridat doplnovani
                            //nRow.GUID = Guid.NewGuid();
                            //if (!item.IsSOUBEHGUIDNull())
                            //    nRow.SOUBEHGUID = item.SOUBEHGUID;
                            //nRow.idVS = Globals.Pracovnik != null ? Globals.Pracovnik.id : string.Empty;
                            
                            //dsOdvadeni.Production.ImportRow(nRow);
                            dsOdvadeni.Production.AddProductionRow(nRow);
                        }
                    }
                }

                // zacatek korekce
                if (!rowProduct.IsTIMECORSTARTNull() && rowProduct.IsTIMECORSTOPNull() && !rowProduct.IsCORRGUIDNull())
                {
                    var dataOdvedena = taProduction.GetDataByCORRGUID(rowProduct.CORRGUID);
                    // najiti dokoncenych korekci
                    var korekceUkoncena = dataOdvedena.Where(x => !x.IsTIMECORSTOPNull());
                    if (korekceUkoncena.Count() > 0)
                    {
                        MessageBox.Show("Korekce již byla dokončena", this.Text, MessageBoxButtons.OK);
                        return;
                    }
                    else   // dokonceni korekce
                    {
                        var item = dataOdvedena.First();
                        using (FormUkoncitZakazkuOdvadeni frmUkoncit = new FormUkoncitZakazkuOdvadeni())
                        {
                            frmUkoncit.Text = "Ukončení korekce";
                            frmUkoncit.rowProduction = item;
                            if (frmUkoncit.ShowDialog(this) != DialogResult.OK)
                                return;
                            dtStop = frmUkoncit.Datum;
                            //dMnozstvi = frmUkoncit.Mnozstvi;
                        }
                        
                        Production.DataServices.VyrobaDataSet.ProductionRow nRow = dsOdvadeni.Production.NewProductionRow();
                        if (!item.IsCountEntriesNull())
                            nRow.CountEntries = item.CountEntries;
                        if (!item.IsSOPNUMBENull())
                            nRow.SOPNUMBE = item.SOPNUMBE;
                        if (!item.IsITEMNMBRNull())
                            nRow.ITEMNMBR = item.ITEMNMBR;
                        if (!item.IsITEMTYPENull())
                            nRow.ITEMTYPE = item.ITEMTYPE;
                        if (!item.IsITEMMJNull())
                            nRow.ITEMMJ = item.ITEMMJ;
                        if (!item.IsORDNull())
                            nRow.ORD = item.ORD; ;
                        if (!item.IsTIMEMODENull())
                            nRow.TIMEMODE = item.TIMEMODE;
                        if (!item.IsTIMEPREPSTARTNull())
                            nRow.TIMEPREPSTART = item.TIMEPREPSTART;
                        if (!item.IsTIMEPREPSTOPNull())
                            nRow.TIMEPREPSTOP = item.TIMEPREPSTOP;
                        if (!item.IsTIMEPREPNull())
                            nRow.TIMEPREP = item.TIMEPREP;
                        if (!item.IsTIMEUNITNull())
                            nRow.TIMEUNIT = item.TIMEUNIT;
                        if (!item.IsTIMESTARTNull())
                            nRow.TIMESTART = item.TIMESTART;
                        if (!item.IsTIMESTOPNull())
                            nRow.TIMESTOP = item.TIMESTOP;
                        if (!item.IsTIMECORSTARTNull())
                            nRow.TIMECORSTART = item.TIMECORSTART;
                        //if (!item.IsTIMECORSTOPNull())
                        nRow.TIMECORSTOP = dtStop;    // Upravuje se !!
                        item.TIMECOR = (float)(nRow.TIMECORSTOP - nRow.TIMECORSTART).TotalMinutes;
                        if (!item.IsTIMECORNull())
                            nRow.TIMECOR = item.TIMECOR;
                        if (!item.IsTIMECRIDNull())
                            nRow.TIMECRID = item.TIMECRID;
                        // id neupravovat
                        nRow.loginid = item.loginid;
                        if (!item.IsmachineidNull())
                            nRow.machineid = item.machineid;
                        if (!item.IsoperationidNull())
                            nRow.operationid = item.operationid;
                        // dateeve neupravovat
                        nRow.dateeve = dtStop;        // dateeve nastaveno na datum ukončení korekce!!                      
                        nRow.qty = item.qty;
                        nRow.qtyReal = item.qtyReal;
                        //nRow.qtyReal = item.qtyReal;
                        if (!item.IsQTYPACKNull())
                            nRow.QTYPACK = item.QTYPACK;
                        if (!item.IsQTYPACKMJNull())
                            nRow.QTYPACKMJ = item.QTYPACKMJ;
                        if (!item.IsdescriptionNull())
                            nRow.description = item.description;
                        if (!item.IsBarcodePNull())
                            nRow.BarcodeP = item.BarcodeP;
                        nRow.UserID = item.UserID;
                        nRow.TermID = item.TermID;
                        if (!item.IsISOKNull())
                            nRow.ISOK = item.ISOK;
                        nRow.GUID = Guid.NewGuid();
                        if(!item.IsSOUBEHGUIDNull())
                            nRow.SOUBEHGUID = item.SOUBEHGUID;
                        if (!item.IsCORRGUIDNull())
                            nRow.CORRGUID = item.CORRGUID;
                        // SOUBEHGUID
                        // CORRGUID
                        // porovnani qtyOld oproti predchozimu stavu
                        //if ((_producttype == PRODUCTIONTYPE.ODVOD_STOP) && (rowProduct.IsqtyOldNull()))
                        //    item.qtyOld = Convert.ToDecimal(textBoxQty.Text);
                        //if (!item.IsTIMEPREPSTARTNull())
                        //    nRow.TIMEPREPSTART = dateTimePickerTIMEPREPSTART.Value;
                        //if (!item.IsTIMEPREPSTOPNull())
                        //    nRow.TIMEPREPSTOP = dateTimePickerTIMEPREPSTOP.Value;
                        //if (!item.IsTIMECORSTARTNull())
                        //    nRow.TIMECORSTART = dateTimePickerTIMECORSTART.Value;
                        //if (!item.IsTIMECORSTOPNull())
                        //    nRow.TIMECORSTOP = dateTimePickerTIMECORSTOP.Value;

                        nRow.idVS = Globals.Pracovnik != null ? Globals.Pracovnik.id : string.Empty;
                        nRow.dateedit = dtNow;
                        //nRow.TIMESTOP = DateTime.Now;   // TODO: pridat doplnovani
                        //nRow.dateedit = dtNow;
                        //nRow.TIMEPREP = 0;
                        //nRow.qty = item.qtyReal = 99;   // TODO: pridat doplnovani
                        //nRow.GUID = Guid.NewGuid();
                        //if (!item.IsSOUBEHGUIDNull())
                        //    nRow.SOUBEHGUID = item.SOUBEHGUID;
                        //nRow.idVS = Globals.Pracovnik != null ? Globals.Pracovnik.id : string.Empty;

                        //dsOdvadeni.Production.ImportRow(nRow);
                        dsOdvadeni.Production.AddProductionRow(nRow);
                    }
                }

                taProduction.Update(dsOdvadeni.Production);
                dsOdvadeni.AcceptChanges();

                // opetovne vyhledani zaznamu
                PerformOK();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
