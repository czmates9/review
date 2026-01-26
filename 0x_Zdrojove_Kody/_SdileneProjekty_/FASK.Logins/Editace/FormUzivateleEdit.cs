using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Fask.Logging;
using System.Reflection;

namespace FASK.Logins.Editace
{
    public partial class FormUzivateleEdit : Form
    {

        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public FASK.Logins.DataSets.Pristupy.FASK_LoginsRow loginsrow { get; set; }

        public string USERID = null;

        public IKomunikace _Komunikace = null;

        public FormUzivateleEdit(IKomunikace KomunikaceSQL)
        {
            InitializeComponent();
            _Komunikace = KomunikaceSQL;
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
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private void LoadData()
        {

            // je úprava záznamu, dojde k načtení dat
            if (loginsrow != null)
            {
                textBoxId.Enabled = false;
                textBoxId.Text = loginsrow.USERID.Trim();
                textBoxJmeno.Text = loginsrow.firstname.Trim();
                textBoxPrijmeni.Text = loginsrow.surname.Trim();
                textBoxHeslo.Text = loginsrow.psswd.Trim();
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

                USERID = textBoxId.Text.Trim();

                if (loginsrow != null)
                {
                    loginsrow.firstname = textBoxJmeno.Text.Trim(); ;
                    loginsrow.surname = textBoxPrijmeni.Text.Trim();
                    loginsrow.psswd = textBoxHeslo.Text.Trim();
            
                    _Komunikace.Update_Login_Row(loginsrow);

                }
                else   // nový záznam
                {
                    _Komunikace.Insert_Login(textBoxId.Text.Trim(), textBoxJmeno.Text.Trim(), textBoxPrijmeni.Text.Trim(), textBoxHeslo.Text.Trim());
                }


                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
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
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(textBoxId.Text.Trim()))
                    errorProvider1.SetError(textBoxId, "Musíte zadat ID uživatele"); 


                int IDtmp = 0;

                if (!int.TryParse(textBoxId.Text.Trim(), out IDtmp))
                {
                    errorProvider1.SetError(textBoxId, "ID uživatele musí být číslo!"); 
                }

                
                if (loginsrow == null)
                {

                    FASK.Logins.DataSets.Pristupy ds = new FASK.Logins.DataSets.Pristupy();

                    var log = _Komunikace.GetFiltrovanyLogins(new Filtry_Login_A());

                    var logins = log.FASK_Logins.Where(x => x.USERID.Trim() == textBoxId.Text.Trim());

                    if (logins.Count() > 0)
                    {
                        textBoxId.Focus();
                        textBoxId.SelectAll();
                        errorProvider1.SetError(textBoxId, "Zadané id uživatele již existuje");
                    }
                }

                if (string.IsNullOrEmpty(textBoxJmeno.Text.Trim()))
                {
                    textBoxJmeno.Focus();
                    errorProvider1.SetError(textBoxJmeno, "Musíte zadat jméno");
                }
                if (string.IsNullOrEmpty(textBoxPrijmeni.Text.Trim()))
                {
                    textBoxPrijmeni.Focus();
                    errorProvider1.SetError(textBoxPrijmeni, "Musíte zadat příjmení");
                }
                if (string.IsNullOrEmpty(textBoxHeslo.Text.Trim()))
                {
                    textBoxHeslo.Focus();
                    errorProvider1.SetError(textBoxHeslo, "Musíte zadat heslo");
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
            foreach (Control c in panel2.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
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
