using WinFormsApp_SQL_XML_Validator.CreateStruncture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp_SQL_XML_Validator
{
    public partial class Form1 : Form
    {

        string PPath = @"E:\_Windows_Projekty_\XML_SQL_Validace\WinFormsApp_SQL_XML_Validator\WinFormsApp_SQL_XML_Validator\bin\Debug";


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DS_Information ds_SQL = new DS_Information();
            DS_Information ds_File = new DS_Information();


            ds_SQL.ReadXml( Path.Combine(PPath,  "DS_Information_File.xml"));
            ds_File.ReadXml(Path.Combine(PPath, "DS_Information_SQL.xml"));

            using (Form_Validate frm = new Form_Validate())
            {

                frm.ds_File = ds_File;
                frm.ds_SQL = ds_SQL;


                frm.ShowDialog();

            }



        }
    }
}
