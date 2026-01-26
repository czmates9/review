using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FASK.SledovaniVyroby.ModuleIfc;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Forms
{
    public partial class FormLogin : Form, IModuleConnector
    {

        frmMain main = null;
        public FormLogin()
        {
            InitializeComponent();
        }

        public override string  Text
        {
	        get 
	        { 
		        return base.Text;
	        }
	        set 
	        { 
		        base.Text = value;
                label1.Text = value;
	        }
        }

        #region Povinne veci okna

        //Ikona oznameni
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }

        public void ReturnPortsToPreviousState()
        {
            //throw new NotImplementedException();
        }

        public void ClosePorts()
        {
            //throw new NotImplementedException();
        }

        public bool IsReadyToClose(out string message)
        {
            
            message = string.Empty;
            return true;
        }
        #endregion


        private void FormInputKod_Load(object sender, EventArgs e)
        { 
            //main = new frmMain();

            //main.Show();
            //this.Hide();

            
        }






        //public void PerformCancel()
        //{
        
        //    DialogResult = DialogResult.Cancel;
        //}

        public void PerformOK()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Focus();
            Application.DoEvents();

            
            try
            {
                if (this.textBoxID.Text.Trim().Length == 0)
                {
                    this.textBoxID.Focus();
                    this.textBoxID.SelectAll();
                    throw new Exception("Není zadáno ID pracovníka");
                }

                if (this.textBoxPassword.Text.Trim().Length == 0)
                {
                    this.textBoxPassword.Focus();
                    this.textBoxPassword.SelectAll();
                    throw new Exception("Není zadáno heslo");
                }





                #region MyRegion

                string SqlConnectionStringLocal = string.Empty;

                if (Database.Vyroba.LogUser(textBoxID.Text.Trim(), textBoxPassword.Text, SqlConnectionStringLocal))
                {

                    //TODO yavolat form main

                }
                else
                {
                    DialogResult drErr = MessageBox.Show("Pøihlášení se nezdaøilo, ID: " + textBoxID.Text.Trim() + ", " + SqlConnectionStringLocal, "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
     
                }

                #endregion


         
          

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }


        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

   
        private void FormInputKod_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            Control ctl;
            ctl = (Control)sender;
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (true)
                {
                    if (buttonOK.Focused && Keys.Enter == e.KeyCode)
                    {
                        PerformOK();
                    }
                }
                else
                {
                    
                    if (e.KeyCode == Keys.Enter)
                    {
                        PerformOK();
                    }
                    else
                        return;
                }
            }
            else
                return;
            
            e.Handled = true;
        }

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            try
            {
                SendKeys.Send(e.KeyboardKeyPressed);
            }
            catch (Exception ex)
            {
                var tmp = ex.Message;
            }
        }
    }
}

