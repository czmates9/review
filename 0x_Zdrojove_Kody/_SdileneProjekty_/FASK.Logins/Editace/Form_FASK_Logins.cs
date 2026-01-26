using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using FASK.Logins.Extensions;

namespace FASK.Logins.Editace
{
    public partial class Form_FASK_Logins : Form
    {
        private string _ConnectionString;

        private IKomunikace _Komunikace = null;

        public FASK.Logins.DataSets.Pristupy.FASK_LoginsRow rowLogins
        {
            get
            {
                try
                {
                    return ((DataRowView)(this.dg_Logins.BindingContext[bs_Logins].Current)).Row as FASK.Logins.DataSets.Pristupy.FASK_LoginsRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Form_FASK_Logins()
        {
            InitializeComponent();
        }
        public Form_FASK_Logins(string ConnectionString): this()
        {
            _ConnectionString = ConnectionString;
            _Komunikace = new SQL_Komunikace(_ConnectionString);
        }

        public Form_FASK_Logins(string adresa,
            string autorizace_DoAPI,
            string aliasDB,
            bool ishttps,
            int timeout,
            string TID,
            string TypeKlient
            ) : this()
        {
            _Komunikace = new API_Komunikace(adresa, autorizace_DoAPI, aliasDB, ishttps, timeout, TID, TypeKlient );
        }

        private void Form_FASK_Logins_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator_Login.Size = new Size(40, 40);
                this.progressIndicator_Login.Location = new Point(this.dg_Logins.Location.X + (this.dg_Logins.Width / 2) - (progressIndicator_Login.Size.Width / 2), this.dg_Logins.Location.Y + (this.dg_Logins.Height / 2) - (progressIndicator_Login.Size.Height / 2));

                this.progressIndicator_Auth.Size = new Size(40, 40);
                this.progressIndicator_Auth.Location = new Point(this.dg_Auth.Location.X + (this.dg_Auth.Width / 2) - (progressIndicator_Auth.Size.Width / 2), this.dg_Auth.Location.Y + (this.dg_Auth.Height / 2) - (progressIndicator_Auth.Size.Height / 2));

                splitContainer1.SplitterDistance = this.Width / 2;
            }
            catch { }
        }

        private void Form_FASK_Logins_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            this.WindowState = FormWindowState.Maximized;

            PerformVyhledat_Login(null);
        }

        #region Click Eventy

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

        private void btn_Filtr_Logins_Click(object sender, EventArgs e)
        {
            PerformVyhledat_Login(null);
        }

        private void PerformVyhledat_Login(string ID)
        {
            try
            {
                DataTable dtchanged = this.ds_Logins.FASK_Logins.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Login.IsBusy)
                {
                    bw_Login.CancelAsync();
                    while (bw_Login.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorLoginStart();

                FASK.Logins.Editace.Filtry_Login_A filtr = new FASK.Logins.Editace.Filtry_Login_A();
                if (!CreateFilter_Login(ref filtr))
                    return;

                RememberItem rem = new RememberItem(filtr, ID, null);

                int FirstDisplayedScrollingRowIndex = this.dg_Logins.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Login.RunWorkerAsync(rem);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Logins.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Logins.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorLoginStop();
                //Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Filtr_Auth_Click(object sender, EventArgs e)
        {
            PerformVyhledat_Auth();
        }


        private void PerformVyhledat_Auth()
        {
            try
            {
                DataTable dtchanged = this.ds_Auth.FASK_Logins_Auth.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Auth.IsBusy)
                {
                    bw_Auth.CancelAsync();
                    while (bw_Auth.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorAuthStart();

                FASK.Logins.Editace.Filtry_Auth_A filtr = new FASK.Logins.Editace.Filtry_Auth_A();
                if (!CreateFilter_Auth(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dg_Auth.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Auth.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Auth.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Auth.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorAuthStop();
                //Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Filtry

        private bool CreateFilter_Login(ref FASK.Logins.Editace.Filtry_Login_A filtr)
        {
            if (filtr == null)
                filtr = new FASK.Logins.Editace.Filtry_Login_A();

            filtr.USERID = tb_USERID.Text.Trim();
            filtr.FirstName = tb_FirstName.Text.Trim();
            filtr.SurName = tb_SurName.Text.Trim();

            filtr.dtp_Create = dtp_Create.Checked;
            filtr.dtp_ValidFrom = dtp_ValidFrom.Checked;
            filtr.dtp_ValidTo = dtp_ValidTo.Checked;

            filtr.dtp_Create_Value = dtp_Create.Value;
            filtr.dtp_ValidFrom_Value = dtp_ValidFrom.Value;
            filtr.dtp_ValidTo_Value = dtp_ValidTo.Value;
            
            return true;
        }

        private bool CreateFilter_Auth(ref FASK.Logins.Editace.Filtry_Auth_A filtr)
        {
            if (filtr == null)
                filtr = new FASK.Logins.Editace.Filtry_Auth_A();

            filtr.IDAgendy = tb_IDAgendy.Text.Trim();

            return true;
        }

        #endregion

        #region Progress indikatory

         private void ProgressIndicatorLoginStop()
         {
             progressIndicator_Login.Stop();
             progressIndicator_Login.Visible = false;
         }

         private void ProgressIndicatorAuthStop()
         {
             progressIndicator_Auth.Stop();
             progressIndicator_Auth.Visible = false;
         }


         private void ProgressIndicatorLoginStart()
         {
             try
             {
                 // prepocet stredu datagridu
                 this.progressIndicator_Login.Location = new Point(this.dg_Logins.Location.X + (this.dg_Logins.Width / 2) - (progressIndicator_Login.Size.Width / 2), this.dg_Logins.Location.Y + (this.dg_Logins.Height / 2) - (progressIndicator_Login.Size.Height / 2));
             }
             catch//(Exception ex)
             {
				 //Fask.Logging.ExceptionHandler2.HandleErrorLog(ex);
             }
             progressIndicator_Login.Start();
             progressIndicator_Login.Visible = true;
         }

         private void ProgressIndicatorAuthStart()
         {
             try
             {
                 // prepocet stredu datagridu
                 this.progressIndicator_Auth.Location = new Point(this.dg_Auth.Location.X + (this.dg_Auth.Width / 2) - (progressIndicator_Auth.Size.Width / 2), this.dg_Auth.Location.Y + (this.dg_Auth.Height / 2) - (progressIndicator_Auth.Size.Height / 2));
             }
             catch { }
             progressIndicator_Auth.Start();
             progressIndicator_Auth.Visible = true;
         }


         #endregion
        

        #region BackGroungWorker Login


        private void bw_Login_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{
            RememberItem Rem = (RememberItem)e.Argument;
            FASK.Logins.Editace.Filtry_Login_A filtr = Rem.filtr;
            DataSets.Pristupy ds = new DataSets.Pristupy();

            if (bw_Login.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            ds = _Komunikace.GetFiltrovanyLogins(filtr);    

            if (bw_Login.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            RememberItem tmprem = new RememberItem(null, Rem.ID, ds);

            e.Result = tmprem;
        }
        
         private void bw_Login_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_Logins = new DataSets.Pristupy();
                    bs_Logins.DataSource = ds_Logins;
                    //Log.Write(e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    ds_Logins = new DataSets.Pristupy();
                    bs_Logins.DataSource = ds_Logins;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    RememberItem Rem = (RememberItem)e.Result;
                    ds_Logins = Rem.ds;

                    if (ds_Logins == null)
                        ds_Logins = new DataSets.Pristupy();

                    bs_Logins.DataSource = ds_Logins;

                    if (!string.IsNullOrEmpty(Rem.ID))
                    {

                        var Rows = ds_Logins.FASK_Logins.Select("USERID = " + Rem.ID);

                        if (Rows.Count() == 1)
                        {

                            dg_Logins.ClearSelection();
                            bs_Logins.FindAndSelect(
                                new Key { PropertyName = ds_Logins.FASK_Logins.USERIDColumn.ColumnName, Value = Rem.ID }
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorLoginStop();
            }
        }

        #endregion

        #region BackGroungWorker Auth

         private void bw_Auth_DoWork(object sender, DoWorkEventArgs e)
         {
             try
             {
                 FASK.Logins.Editace.Filtry_Auth_A filtr = (FASK.Logins.Editace.Filtry_Auth_A)e.Argument;
                 FASK.Logins.DataSets.Pristupy ds = new FASK.Logins.DataSets.Pristupy();
                 
                 if (bw_Auth.CancellationPending)
                 {
                     e.Cancel = true;
                     return;
                 }

                 if (rowLogins != null)
                 {
                     filtr.USERID = rowLogins.USERID;
                     ds = _Komunikace.GetLogins_Auth(filtr);
                 }

                 if (bw_Auth.CancellationPending)
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

         private void bw_Auth_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
         {
             try
             {
                 if (e.Error != null)
                 {
                     ds_Auth = new DataSets.Pristupy();
                     bs_Auth.DataSource = ds_Auth;
                     //Log.Write(e.Error);
                     MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
                 else if (e.Cancelled)
                 {
                     ds_Auth = new DataSets.Pristupy();
                     bs_Auth.DataSource = ds_Auth;
                 }
                 else
                 {
                     ds_Auth = (FASK.Logins.DataSets.Pristupy)e.Result;
                     if (ds_Auth == null)
                         ds_Auth = new DataSets.Pristupy();

                     bs_Auth.DataSource = ds_Auth;
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

        #endregion

         private void dg_Logins_SelectionChanged(object sender, EventArgs e)
         {
             try
             {
                 if (rowLogins != null)
                 {
                     PerformVyhledat_Auth();
                 }
             }
             catch (Exception ex)
             {
                 //Log.Write(ex);
                 MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }

         private void btn_Add_Login_Click(object sender, EventArgs e)
         {
             PerformCreateRecord();
         }

         private void PerformCreateRecord()
         {
             try
             {

                 using (FormUzivateleEdit frmuziv = new FormUzivateleEdit(_Komunikace))
                 {
                     frmuziv.Text = "Nový uživatel";
                     if (frmuziv.ShowDialog(this) != DialogResult.OK)
                         return;


                     PerformVyhledat_Login(frmuziv.USERID);
                 }
             }
             catch (Exception ex)
             {
                 //Log.Write(ex);
                 MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }

         private void btn_Edit_Login_Click(object sender, EventArgs e)
         {
             PerformEditRecord();
         }

         private void PerformEditRecord()
         {
             try
             {

                 if (rowLogins == null)
                 {
                     MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                     return;
                 }

                 using (FormUzivateleEdit frmuziv = new FormUzivateleEdit(_Komunikace))
                 {
                     frmuziv.loginsrow = rowLogins;
                     frmuziv.Text = "Úprava uživatele";
                     if (frmuziv.ShowDialog(this) != DialogResult.OK)
                         return;

                     PerformVyhledat_Login(frmuziv.USERID);
                 }
             }
             catch (Exception ex)
             {
                 //Log.Write(ex);
                 MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }

         private void btn_Delete_Login_Click(object sender, EventArgs e)
         {
             PerformDeleteRecord();
         }

         private void PerformDeleteRecord()
         {
             if (rowLogins == null)
             {
                 MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                 return;
             }

             _Komunikace.Delete_Login(rowLogins.USERID);
             PerformVyhledat_Login(null);

         }

         private void btn_Delete_Auth_Click(object sender, EventArgs e)
         {
             PerformDeleteRecord_Auth();
         }

         private void PerformDeleteRecord_Auth()
         {
             if (dg_Auth.SelectedRows.Count == 0)
             {
                 MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                 return;
             }

             foreach (DataGridViewRow material in dg_Auth.SelectedRows)
             {
                 _Komunikace.Delete_Auth((((DataRowView)material.DataBoundItem).Row as FASK.Logins.DataSets.Pristupy.FASK_Logins_AuthRow).DEX_ROW_ID);
             }

             PerformVyhledat_Auth();

         }

         private void btn_Add_Auth_Click(object sender, EventArgs e)
         {
             Perform_Add_Auth();
         }

         private void Perform_Add_Auth()
         {
             try
             {
                 if (rowLogins == null)
                 {
                     MessageBox.Show("Není vybrán záznam pro přidelení práv.", this.Text, MessageBoxButtons.OK);
                     return;
                 }


                 using (Form_FASK_AGENDA frmuziv = new Form_FASK_AGENDA(ZOBRAZENI_TYP.VYBER ,_Komunikace))
                 {
                     frmuziv.Text = "Výber práv";
                     if (frmuziv.ShowDialog(this) != DialogResult.OK)
                         return;


                     foreach (DataGridViewRow item in frmuziv.dg_Agenda.SelectedRows)
                     {

                         FASK.Logins.DataSets.Pristupy.FASK_AGENDARow row = ((DataRowView)item.DataBoundItem).Row as FASK.Logins.DataSets.Pristupy.FASK_AGENDARow;

                         if (!_Komunikace.isExist_Auth(
                                rowLogins.USERID.Trim(),
                                row.AGENDAID.Trim()
                                ))
                         {
                             _Komunikace.Insert_Auth(
                                rowLogins.USERID.Trim(),
                                row.AGENDAID.Trim()
                                );
                         }
                     }

                     PerformVyhledat_Login(rowLogins.USERID);
                 }


             }
             catch (Exception ex)
             {
                 
                 //Log.Write(ex);
                 throw ex;
             }

         }

         private void btn_Edit_Prava_Click(object sender, EventArgs e)
         {
             try
             {

                 using (Form_FASK_AGENDA frmuziv = new Form_FASK_AGENDA( ZOBRAZENI_TYP.LIST,_Komunikace))
                 {
                     frmuziv.Text = "Úprava práv";
                     if (frmuziv.ShowDialog(this) != DialogResult.OK)
                         return;
 

                 }
             }
             catch (Exception ex)
             {
                 //Log.Write(ex);
                 MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }

        private void btn_Uziv_Import_Click(object sender, EventArgs e)
        {
            PerformImportLogins();
        }

        private void PerformImportLogins()
        {
            throw new NotImplementedException();
        }
    }
}
