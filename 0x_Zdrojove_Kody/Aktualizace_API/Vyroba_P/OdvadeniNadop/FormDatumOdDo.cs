using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Aktualizace_API.MySystem;
using Fask.Aktualizace_API.Extensions;
using Fask.Aktualizace_API.Forms;
using JR.Utils.GUI.Forms;
using System.Linq;
namespace Fask.Aktualizace_API.Odvadeni
{
    public partial class FormDatumOdDo : Form
    {
        public DateTime DatumOd
        {
            get
            {
                return this.dateTimePickerDatumOd.Value;
            }
            set
            {
                DatumOd = value;   
            }
        }
        public DateTime DatumDo
        {
            get
            {
                return this.dateTimePickerDatumDo.Value;
            }
            set
            {
                DatumDo = value;
            }
        }
        
        public FormDatumOdDo()
        {
            InitializeComponent();            
            
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormBaseButtonOKStorno_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            this.dateTimePickerDatumOd.Value = dt; //DateTime.Now;
            this.dateTimePickerDatumDo.Value = DateTime.Now;
            //ScannerStart();
        }

        private void FormBaseButtonOKStorno_KeyDown(object sender, KeyEventArgs e)
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

        public void PerformOK()
        {
            try
            {
                if (DatumOd > DatumDo)
                {
                    FlexibleMessageBox.Show(this, "Datum od je vìtší, než datum do", this.Text);
                    return;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
                return;
            }
            

            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            //ScannerStop();
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItemStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void cbDuvod_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        //private void ScannerStart()
        //{
        //    try
        //    {
        //        FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.Enable();
        //    }
        //    catch
        //    {
        //    }
        //}

        //private void ScannerStop()
        //{
        //    try
        //    {
        //        FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.Disable();
        //    }
        //    catch
        //    {
        //    }
        //}

        //private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        //{
        //    //if (e.BarcodeData.Trim().Length == 0)
        //    //    return;

        //    //string data = e.BarcodeData;

        //    //var corrections = crrdt.Where(s => s.desc == data);
        //    //if (corrections.Count() > 0)
        //    //{
        //    //    cbDuvod.SelectedItem = corrections.First();
        //    //    dateTimePickerDatumDo.Focus();
        //    //}
        //    //else
        //    //{
        //    //    cbDuvod.SelectedItem = null;
        //    //    cbDuvod.Focus();
        //    //}
        //}

        //void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        //{
        //    this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        //}

        //private void textBoxDelka_LostFocus(object sender, EventArgs e)
        //{
        //    TimeSpan time = TimeSpan.Zero;
        //    try
        //    {
        //        textBoxDelka.BackColor = SystemColors.Window;
        //        time = TimeSpan.Parse(this.textBoxDelka.Text);
        //    }
        //    catch
        //    {
        //        textBoxDelka.BackColor = Color.MistyRose;
        //        return;
        //    }

        //    try
        //    {
        //        dateTimePickerKonec.Value = dateTimePickerZacatek.Value + time;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex.Message, this.Text);
        //    }
        //}
    }
}

