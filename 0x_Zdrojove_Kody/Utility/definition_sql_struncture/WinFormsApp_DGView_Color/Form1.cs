using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp_DGView_Color
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("SurName", typeof(string));
            dt.Columns.Add("Age", typeof(int));


            dt.Rows.Add("A", "A", 1);
            dt.Rows.Add("B", "B", 2);
            dt.Rows.Add("C", "C", 3);
            dt.Rows.Add("D", "D", 4);
            dt.Rows.Add("E", "E", 5);
            dt.Rows.Add("F", "F", 6);
            dt.Rows.Add("G", "G", 7);
            dt.Rows.Add("H", "H", 8);
            dt.Rows.Add("I", "I", 9);
            dt.Rows.Add("J", "J", 10);
            dt.Rows.Add("K", "K", 11);
            dt.Rows.Add("L", "L", 12);
            dt.Rows.Add("M", "M", 13);

            dataGridView1.DataSource = dt;

        }

        private void ChangeColor()
        {

            #region pokus 1
            //foreach (DataGridViewRow item in dataGridView1.Rows)
            //{
            //    if (item.DataBoundItem == null)
            //        continue;

            //    DataRow myRow = (item.DataBoundItem as DataRowView).Row;

            //        // do something with your DataRow…

            //        object[] obj = myRow.ItemArray;

            //        int tmp = (int)obj[2] % 2;

            //        if (tmp == 0)
            //        {
            //            item.DefaultCellStyle.BackColor = Color.Green;
            //        }
            //} 
            #endregion


            #region pokus 2


            for (int i = 0; i < dataGridView1.Rows.Count ; i++)
            {
                var Row = dataGridView1.Rows[i];
                var Cell = Row.Cells[2];
                string Value = Cell.Value.ToString();

                int val = Int32.Parse(Value);

                int tmp = val % 2;

                if (tmp == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

            }


            #endregion

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            InitData();
            ChangeColor();

        }
    }
}
