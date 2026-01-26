using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

// TODO : Vyber prikazu ...

namespace Fask.Vyroba_W.Odvadeni
{
    public partial class FormOperaceVyber : Form
    {
		public List<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow> _Operace = null;
		public List<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow> Operace
        {
            set
            {
                _Operace = value;
                this.vyrobaCEDataSet.CZPRO_VPP.Clear();
                // naplni tabulku a zobrazeni daty ...
                foreach (var i in value)
                {
                    vyrobaCEDataSet.CZPRO_VPP.ImportRow(i);
                }
                this.vyrobaCEDataSet.AcceptChanges();
            }
        }

		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow OperaceVybrana
        {
            get
            {
                //return _PrikazVybrany;
				return ((DataRowView)this.cZPROVPPBindingSource.Current).Row as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow;
            }
            set
            {
                // TODO : nastaveni nalezeneho prikazu ... ???
            }
        }


        public FormOperaceVyber()
        {
            InitializeComponent();

            dataGrid1.Load(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString())); 
        }

        #region Scanner car.kodu 
        bool scannerefinalized = false;
        private void ScannerFinalize()
        {
            scannerefinalized = true;
            ScannerStop();
        }

        private void ScannerStart()
        {
            if (scannerefinalized)
                return;

            try
            {
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady += new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                Fask.Vyroba_W.Forms.FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                Fask.Vyroba_W.Forms.FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        void Scanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.MST_W.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        private void ScannerEventHandlerMethod(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            try
            {
                ScannerStop();

                string bcode = e.BarcodeData.Trim();

                if (bcode.Length == 0)
                    return;

                int operaci = 0;
                if ((operaci = NajdiOperaci(bcode)) > 0)
                {
                    if (operaci == 1)
                        this.PerformOK();
                }
                else
                {
                    MessageBox.Show("Operace '" + bcode + "' nenalezena", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }

        private int NajdiOperaci(string bcode)
        {
            var operace = this.vyrobaCEDataSet.CZPRO_VPP.Where(x => x.BarcodeP.Trim() == bcode);
            if (operace.Count() > 0)
            {
                this.cZPROVPPBindingSource.Filter = String.Format("BarcodeP={0}", bcode);
                return operace.Count();
            }
            else
            {
                this.cZPROVPPBindingSource.RemoveFilter();
                return 0;
            }
        }
        #endregion

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormOperaceVyber_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);

            //aktivace scanneru
            ScannerStart();
        }

        private void finalize()
        {
            this.ScannerFinalize();
            dataGrid1.Save(Path.Combine(MySystem.MyPath.ConfigDirectory, this.GetType().ToString())); 
        }

        private void FormOperaceVyber_KeyDown(object sender, KeyEventArgs e)
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
                if (this.OperaceVybrana == null)
                {
                    MessageBox.Show("Není vybrána operace", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            this.finalize();
            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            this.finalize();
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

        private void menuItemAkceOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void menuItemAkceStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

    }
}

