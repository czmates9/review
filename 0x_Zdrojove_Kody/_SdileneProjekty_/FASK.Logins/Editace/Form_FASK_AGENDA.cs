using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.Logins.Editace
{
    public enum ZOBRAZENI_TYP
    {
        LIST,
        VYBER,
        UNKNOWN
    }

    public partial class Form_FASK_AGENDA : Form
    {

        public FASK.Logins.DataSets.Pristupy.FASK_AGENDARow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_Agenda.BindingContext[bs_Agenda].Current)).Row as FASK.Logins.DataSets.Pristupy.FASK_AGENDARow;
                }
                catch
                {
                    return null;
                }
            }
        }


        private FASK.Logins.Editace.ZOBRAZENI_TYP _zobrazeni = FASK.Logins.Editace.ZOBRAZENI_TYP.UNKNOWN;
        public FASK.Logins.Editace.ZOBRAZENI_TYP Zobrazeni
        {
            get
            {
                return _zobrazeni;
            }
            set
            {
                if (_zobrazeni != value)
                {
                    _zobrazeni = value;
                    switch (_zobrazeni)
                    {
                        case FASK.Logins.Editace.ZOBRAZENI_TYP.LIST:
                            panelButtonsZobrazeniVyber.Hide();
                            panelButtonsZobrazeniList.Show();
                            menuStrip2.Items.Remove(tsmiMenuVyber);
                            this.WindowState = FormWindowState.Maximized;
                            break;
                        case FASK.Logins.Editace.ZOBRAZENI_TYP.VYBER:
                            panelButtonsZobrazeniList.Hide();
                            panelButtonsZobrazeniVyber.Show();
                            menuStrip2.Items.Remove(tsmiMenuList);
                            this.WindowState = FormWindowState.Normal;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

                public IKomunikace _Komunikace = null;


        public Form_FASK_AGENDA(FASK.Logins.Editace.ZOBRAZENI_TYP typzobrazeni,IKomunikace KomunikaceSQL)
        {
            InitializeComponent();
            Zobrazeni = typzobrazeni;
            _Komunikace = KomunikaceSQL;
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformCancel()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_Agenda_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FASK.Logins.Editace.Filtry_Agenda_A filtr = (FASK.Logins.Editace.Filtry_Agenda_A)e.Argument;
                FASK.Logins.DataSets.Pristupy ds = new FASK.Logins.DataSets.Pristupy();

                if (bw_Agenda.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                ds = _Komunikace.GetFASK_AGENDA_Filtrovana(filtr);

                if (bw_Agenda.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                throw ex;
            }
        }

        private void bw_Agenda_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    ds_Agenda = new DataSets.Pristupy();
                    bs_Agenda.DataSource = ds_Agenda;
                    //Log.Write(e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    ds_Agenda = new DataSets.Pristupy();
                    bs_Agenda.DataSource = ds_Agenda;
                }
                else
                {
                    ds_Agenda = (FASK.Logins.DataSets.Pristupy)e.Result;
                    if (ds_Agenda == null)
                        ds_Agenda = new DataSets.Pristupy();

                    bs_Agenda.DataSource = ds_Agenda;
                }
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorAuthStop();
            }
        }

        private void Form_FASK_AGENDA_Shown(object sender, EventArgs e)
        {

            this.progressIndicator_Auth.Size = new Size(40, 40);
            this.progressIndicator_Auth.Location = new Point(this.dg_Agenda.Location.X + (this.dg_Agenda.Width / 2) - (progressIndicator_Auth.Size.Width / 2), this.dg_Agenda.Location.Y + (this.dg_Agenda.Height / 2) - (progressIndicator_Auth.Size.Height / 2));

        }

        private void Form_FASK_AGENDA_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;

            PerformVyhledat();
        }

        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.ds_Agenda.FASK_AGENDA.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Agenda.IsBusy)
                {
                    bw_Agenda.CancelAsync();
                    while (bw_Agenda.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorAuthStop();

                FASK.Logins.Editace.Filtry_Agenda_A filtr = new FASK.Logins.Editace.Filtry_Agenda_A();
                if (!CreateFilter_Auth(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dg_Agenda.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Agenda.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Agenda.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Agenda.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorAuthStart();
                //Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CreateFilter_Auth(ref FASK.Logins.Editace.Filtry_Agenda_A filtr)
        {
            if (filtr == null)
                filtr = new FASK.Logins.Editace.Filtry_Agenda_A();

            filtr.AGENDAID = tb_AGENDAID.Text.Trim();
            filtr.NAME = tb_NAME.Text.Trim();
            filtr.DESCIPTION = tb_DESC.Text.Trim();

            return true;
        }

        #region Progress indikatory

        private void ProgressIndicatorAuthStop()
        {
            progressIndicator_Auth.Stop();
            progressIndicator_Auth.Visible = false;
        }


        private void ProgressIndicatorAuthStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator_Auth.Location = new Point(this.dg_Agenda.Location.X + (this.dg_Agenda.Width / 2) - (progressIndicator_Auth.Size.Width / 2), this.dg_Agenda.Location.Y + (this.dg_Agenda.Height / 2) - (progressIndicator_Auth.Size.Height / 2));
            }
            catch { }
            progressIndicator_Auth.Start();
            progressIndicator_Auth.Visible = true;
        }


        #endregion

        private void btn_AddPrava_Auth_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PerformOK()
        {
            try
            {
                if (Zobrazeni != ZOBRAZENI_TYP.VYBER)
                    return;

                if (dg_Agenda.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Není vybrán záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        private void btn_Delete_Auth_Click(object sender, EventArgs e)
        {
            PerformDelete();
        }

        private void PerformDelete()
        {
            if (dg_Agenda.SelectedRows.Count == 0)
            {
                MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                return;
            }


            foreach (DataGridViewRow item in dg_Agenda.SelectedRows)
            {

                //FASK.Logins.DataSets.Pristupy.FASK_AGENDARow row = ((DataRowView)item.DataBoundItem).Row as FASK.Logins.DataSets.Pristupy.FASK_AGENDARow;
                //_KomunikaceSQL.Delete_AGENDA(rowLogins.USERID);

            }

            PerformVyhledat();
        }

        private void btn_Edit_Auth_Click(object sender, EventArgs e)
        {
            PerformEdit();
        }

        private void PerformEdit()
        {
            throw new NotImplementedException();
        }

        private void btn_Add_Auth_Click(object sender, EventArgs e)
        {
            PerformAdd();
        }

        private void PerformAdd()
        {
            throw new NotImplementedException();
        }
    }
}
