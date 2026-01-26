using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_P.Forms;
using JR.Utils.GUI.Forms;

namespace Fask.Vyroba_P.Odvadeni
{
    public partial class FormHeslo : Form
    {

        private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _pracovnik = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            get { return _pracovnik; }
            //set { _vedouciSmenyLogin = value; }
        }

        public FormHeslo()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void FormHeslo_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);

           
            if (Globals.Pracovnik != null)
                textBoxID.Text = Globals.Pracovnik.id.Trim();

            textBoxID_Activate();
            
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void textBoxID_Activate()
        {
            textBoxID.SelectAll();
            textBoxID.Focus();
        }

        public void PerformCancel()
        {
           
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            try
            {
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter lta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter();
                //lta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable dtLogins = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Logins(textBoxID.Text.Trim());

                if (dtLogins.Rows.Count == 0)
                    throw new Exception("Uživatel neexistuje");

                if (dtLogins[0].psswd != textBoxHeslo.Text.Trim())
                {
                    throw new Exception("Heslo uživatele " + dtLogins[0].firstname.Trim() + " " + dtLogins[0].surname.Trim() + " není zadáno správně");
                }

                if (Settings.ZruseniPouzeVS)
                {
                    if (dtLogins[0].VS == 0)
                    {
                        throw new Exception("Uživatel " + dtLogins[0].firstname.Trim() + " " + dtLogins[0].surname.Trim() + " nemá pravo přístupu.");
                    }
                }

                this._pracovnik = dtLogins[0];

            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                textBoxID_Activate();
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void FormHeslo_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
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

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }
    }
}
