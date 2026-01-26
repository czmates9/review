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
    public partial class Csharp_datagridview_row_color : Form
    {
        public Csharp_datagridview_row_color()
        {
            InitializeComponent();
        }

        private void Csharp_datagridview_row_color_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Product ID", typeof(string));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Product Quantity", typeof(int));

            dt.Rows.Add("13ZD", "AAAA", 7);
            dt.Rows.Add("13ZD", "AAAA", 11);
            dt.Rows.Add("13ZD", "AAAA", 4);
            dt.Rows.Add("13ZD", "AAAA", 18);
            dt.Rows.Add("13ZD", "AAAA", 1);
            dt.Rows.Add("13ZD", "AAAA", 12);
            dt.Rows.Add("13ZD", "AAAA", 5);
            dt.Rows.Add("13ZD", "AAAA", 0);
            dt.Rows.Add("13ZD", "AAAA", 6);
            dt.Rows.Add("13ZD", "AAAA", 10);
            dt.Rows.Add("13ZD", "AAAA", 14);
            dt.Rows.Add("13ZD", "AAAA", 13);
            dt.Rows.Add("13ZD", "AAAA", 2);
            dt.Rows.Add("13ZD", "AAAA", 16);
            dt.Rows.Add("13ZD", "AAAA", 9);
            dt.Rows.Add("13ZD", "AAAA", 15);
            dt.Rows.Add("13ZD", "AAAA", 0);
            dt.Rows.Add("13ZD", "AAAA", 8);
            dt.Rows.Add("13ZD", "AAAA", 3);

            // Bind dataGridView with Datatable
            dataGridView1.DataSource = dt;
            RowsColor();
        }

        public void RowsColor()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int val = Int32.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                if (val < 5)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    if (val == 0)
                    {
                        //timer1.Start();
                    }
                }

                else if (val >= 5 && val < 10)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                else if (val >= 10 && val < 15)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }
    }
}
