using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fask.ModulePohodaXML.Konfigurace
{
    public partial class Konfig : Form
    {
        public Konfig()
        {
            InitializeComponent();
        }

        private void Konfig_Load(object sender, EventArgs e)
        {
            try
            {

                Globals_V1.LoadConfiguration();

                foreach (DataTable item in Globals_V1.Konfigurace.Tables)
                {
                    string TableName = item.TableName;

                    Button btn = new Button();
                    btn.Name = TableName;
                    btn.Text = TableName;
                    btn.Dock = DockStyle.Top;
                    btn.Click += Btn_Click;

                    panel1.Controls.Add(btn);
                }


            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button a = (Button)sender;

                using (Form_Properties frm = new Form_Properties())
                {
                    frm.Tabulka = a.Name;

                    frm.ShowDialog();
                }

            }
        }
    }
}
