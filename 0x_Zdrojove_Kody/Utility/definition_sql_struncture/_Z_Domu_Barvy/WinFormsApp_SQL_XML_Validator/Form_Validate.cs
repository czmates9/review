using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp_SQL_XML_Validator.CreateStruncture;

namespace WinFormsApp_SQL_XML_Validator
{
    public partial class Form_Validate : Form
    {

        public Form_Validate()
        {
            InitializeComponent();
        }

        private void Form_Validate_Load(object sender, EventArgs e)
        {
            bs_File.DataSource = ds_File;
            bs_SQL.DataSource = ds_SQL;


            dg_File.DefaultCellStyle.BackColor = Color.Red;
            dg_SQL.DefaultCellStyle.BackColor = Color.Red;

            ValidateData();
        }



        private void ValidateData()
        {

            foreach (DataGridViewRow item1 in dg_SQL.Rows)
            {

                DS_Information.SchemaColumnsRow row_sql = (DS_Information.SchemaColumnsRow)((DataRowView)item1.DataBoundItem).Row;



                foreach (DataGridViewRow item2 in dg_File.Rows)
                {
                    DS_Information.SchemaColumnsRow row_file = (DS_Information.SchemaColumnsRow)((DataRowView)item2.DataBoundItem).Row;

                    if (Compare(row_sql, row_file))
                    {
                        //Porovnat cele řadky
                        item1.DefaultCellStyle.BackColor = Color.Green;
                        item2.DefaultCellStyle.BackColor = Color.Green;
                        break;
                    }
                    else if (CompareDetail(row_sql, row_file))
                    {
                        //nazev tabulky a stloupce cesi, ale neco ine ne...

                        item1.DefaultCellStyle.BackColor = Color.Orange;
                        item2.DefaultCellStyle.BackColor = Color.Orange;
                    }

                    // Zostane červeny poku sa nachaza iba v jednem s dvoch

                }
            }
        }


        private bool Compare(DS_Information.SchemaColumnsRow dr1, DS_Information.SchemaColumnsRow dr2)
        {
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;

            return comparer.Equals(dr1, dr2);
        }

        private bool CompareDetail(DS_Information.SchemaColumnsRow dr1, DS_Information.SchemaColumnsRow dr2)
        {
            if (
                (dr1.TABLE_NAME.Trim() == dr2.TABLE_NAME.Trim()) &&
                (dr1.COLUMN_NAME.Trim() == dr2.COLUMN_NAME.Trim())
                )
            {
                return true;
            }
            return false ;
        }

    }
}
