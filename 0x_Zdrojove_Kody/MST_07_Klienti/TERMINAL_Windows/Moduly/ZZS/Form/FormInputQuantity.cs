using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Fask.Vyroba_P.Forms;
//using JR.Utils.GUI.Forms;

namespace FASK.MST_WINDOWS.Module.ZZS
{
    public partial class FormInputQuantity : Form
    {
        string textform = string.Empty;
        private decimal _kod;

        public decimal Kod
        {
            get { return this._kod; }
            set { this._kod = value; }
        }

        //private Data.VyrobaCEDataSet.LoginsRow _pracovnik = null;
        //public Data.VyrobaCEDataSet.LoginsRow Pracovnik
        //{
        //    set
        //    {
        //        _pracovnik = value;
        //        UpdateTextForm();
        //    }
        //}

        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Zbozi.CZMST095Row _ZboziRow;
        public FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Zbozi.CZMST095Row ZboziRow 
        {
            get { return this._ZboziRow; }
            set { this._ZboziRow = value; }
        }

        private void UpdateTextForm()
        {
            //this.Text = textform;
            //if (_pracovnik != null)
            //    this.Text += ", " + _pracovnik.ToString();

            //if (vph != null)
            //    this.Text += ", " + vph.SOPNUMBE.Trim() + ":" + vph.SOPTYPE.Trim();

            //if (vpp != null)
            //    this.Text += ", " + vpp.ITEMDESC.Trim();
        }

        //private Data.VyrobaCEDataSet.CZPRO_VPHRow vph = null;
        //private Data.VyrobaCEDataSet.CZPRO_VPPRow vpp = null;
        public FormInputQuantity()
        {
            InitializeComponent();

            this.textform = this.Text;
        }
            
        //public FormInputQuantity(Data.VyrobaCEDataSet.CZPRO_VPHRow vph, Data.VyrobaCEDataSet.CZPRO_VPPRow vpp) : this()
        //{
        //    this.vph = vph;
        //    this.vpp = vpp;
            
        //    UpdateTextForm();
        //}


        private decimal _mnozstvi = 0;
        public decimal Mnozstvi
        {
            get { return _mnozstvi; }
            set
            {
                _mnozstvi = value;
                this.textBoxKod.Text = _mnozstvi.ToString("0.####");
                this.textBoxKod.SelectAll();
            }
        }

        private void FormInputKod_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new Point(0,0);
            //this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(new Point(0, 0));
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.WindowState = FormWindowState.Maximized;
            panelButtons_Resize(null, null);
            //textBoxKod_Activate();
            //ScannerStart();

            l_ItemMJ.Text = "[" + this._ZboziRow.MJ + "]";
        }

        //private void textBoxKod_Activate()
        //{
        //    textBoxKod.SelectAll();
        //    textBoxKod.Focus();
        //}

        private void ScannerStart()
        {
            try
            {
                //FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                //FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                //FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                //FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                //FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        //private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        //{
        //    if (e.BarcodeData.Trim().Length == 0)
        //        return;

        //    this.Kod = e.BarcodeData.Trim();
            
        //    this.PerformOK();
        //}

        //void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        //{
        //    this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        //}

        public void PerformCancel()
        {
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {

            if (this.textBoxKod.Text.Trim().Length == 0)
            {
                MessageBox.Show(this,"Musíte zadat hodnotu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                //FlexibleMessageBox.Show(this, "Musíte zadat hodnotu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                //textBoxKod_Activate();
                return;
            }

            try
            {
                _mnozstvi = decimal.Parse(this.textBoxKod.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Množství", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //FlexibleMessageBox.Show(this, ex.Message, "Množství", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ScannerStop();
            DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormInputKod_KeyDown(object sender, KeyEventArgs e)
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

        private void textBoxKod_TextChanged(object sender, EventArgs e)
        {
            //textBoxKod.BackColor = SystemColors.Window;
            //try
            //{
            //    decimal mn = decimal.Parse(textBoxKod.Text);

            //    l_ItemMJ.Text = vpp.IsITEMMJNull() ? "?" : vpp.ITEMMJ.Trim();
            //    textBoxBaleni.Text = vpp.QTYPACK.ToString("0.####");
            //    l_QtypackMJ.Text = vpp.IsQTYPACKMJNull() ? "?" : vpp.QTYPACKMJ.Trim();
            //    l_CelkemMJ.Text = l_QtypackMJ.Text;

            //    textBoxCelkemVBaleni.Text = (mn * vpp.QTYPACK).ToString("0.####");
            //}
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //    //FlexibleMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxKod.BackColor = Color.Magenta;
            //}
        }

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }


    }
}

