using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Definition_SQL_Struncture
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }
        
        private void WriteError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void btn_DS_Info_Click(object sender, EventArgs e)
        {
            try
            {
                using (Definice_List fr = new Definice_List())
                {
                    this.Hide();
                    fr.ShowDialog();

                }
                this.Show();

            }
            catch (System.Exception ex)
            {
                WriteError(ex);
            }
        }

        private void btn_Dok_Click(object sender, EventArgs e)
        {
            try
            {
                using (Definice_Dokumentace fr = new Definice_Dokumentace())
                {
                    this.Hide();
                    fr.ShowDialog();

                }
                this.Show();

            }
            catch (System.Exception ex)
            {
                WriteError(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
