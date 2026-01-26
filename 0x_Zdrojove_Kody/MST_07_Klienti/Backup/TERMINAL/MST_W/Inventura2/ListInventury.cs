using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.MST_W.Inventura2
{
    public partial class ListInventury : System.Windows.Forms.Form
    {
        private Inventura2Service.Inventury2 inventury = null;

        private Inventura2Service.Inventury2.HlavickyRow SelectedRow
        {
            get
            {
                try
                {
                    DataRowView dr = (DataRowView)hlavickyBindingSource.Current;
                    Inventura2Service.Inventury2.HlavickyRow irow = dr.Row as Inventura2Service.Inventury2.HlavickyRow;
                    return irow;
                }
                catch
                {
                    return null;
                }
            }
        }
 
        public string Davka
        {
            get
            {
                try
                {
                    Inventura2Service.Inventury2.HlavickyRow irow = SelectedRow;
                    if (irow != null)
                        return irow.ID_INV;
                    else
                        return string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public ListInventury(Inventura2Service.Inventury2 inventury)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.inventury = inventury != null ? inventury : (new Fask.MST_W.Inventura2Service.Inventury2());
            this.panel1_Resize(null, null);
            this.Synchronize();
        }

        public ListInventury(string[] filenames)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.inventury = new Fask.MST_W.Inventura2Service.Inventury2();
            foreach (string filename in filenames)
            {
                try
                {
                    string davka = System.IO.Path.GetFileNameWithoutExtension(filename);
                    inventury.Hlavicky.AddHlavickyRow(davka);
                }
                catch { }
            }
            this.panel1_Resize(null, null);
            //this.Synchronize();
        }

        public void Synchronize()
        {
            this.inventury.AcceptChanges();
            for (int i = this.inventury.Hlavicky.Count - 1; i >= 0; i--)
            {
                Inventura2Service.Inventury2.HlavickyRow hlavicka = this.inventury.Hlavicky[i];
                string file = Path.Combine(Main.StorageDir, hlavicka.ID_INV.Trim() + "." + Main.Ext_Inventura2);
                if (File.Exists(file))
                    this.inventury.Hlavicky.RemoveHlavickyRow(hlavicka);
            }
            this.inventury.AcceptChanges();
        }

        private void ListInventury_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                DialogResult = DialogResult.Cancel;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                ProcessOK();
            }
        }

        private void ListInventury_Load(object sender, EventArgs e)
        {
            this.Text += " " + MST_Global.Inventura2Name.Trim();
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            updateForm();
            panel1_Resize(null, null);
        }

        private void updateForm()
        {
            hlavickyBindingSource.DataSource = inventury;
            hlavickyBindingSource.DataMember = inventury.Hlavicky.TableName;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ProcessOK();
        }

        private void ProcessOK()
        {
            if (SelectedRow != null)
                DialogResult = DialogResult.OK;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Width / 2, panel1.Height);
            graphicButtonOK.Size = nsize;
        }
    }
}