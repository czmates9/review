using Definition_SQL_Struncture.CreateStruncture;
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
    public partial class Definice_Validace : Form
    {

        float originalWidth;
        int splitPointX;

        public Definice_Validace()
        {
            InitializeComponent();

        }

        #region Stedovy panel
        private void panel1_MouseDown(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Capture mouse so that all mouse move messages go to this control
                (s as Control).Capture = true;

                // Record original column width
                originalWidth = tableLayoutPanel1.ColumnStyles[0].Width;

                // Record first clicked point
                // Convert to screen coordinates because this window will be a moving target
                Point windowPoint = (s as Control).PointToScreen(e.Location);
                splitPointX = windowPoint.X;
            }
        }

        private void panel1_MouseUp(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Release mouse capture
                if ((s as Control).Capture)
                    (s as Control).Capture = false;
            }
        }

        private void panel1_MouseMove(object s, MouseEventArgs e)
        {
            if ((s as Control).Capture)
            {
                Point windowPoint = (s as Control).PointToScreen(e.Location);

                // Calculate distance of mouse from splitPoint
                int offset = windowPoint.X - splitPointX;

                // Apply to originalWidth
                float newWidth = originalWidth + offset;

                // Clamp it.
                // The control in the left pane's MinimumSize.Width would be more appropriate than zero
                newWidth = Math.Max(0, newWidth);

                // Update column width
                if (Math.Abs(newWidth - tableLayoutPanel1.ColumnStyles[0].Width) >= 1)
                    tableLayoutPanel1.ColumnStyles[0].Width = newWidth;
            }
        }
        #endregion


        private void Definice_Validace_Load(object sender, EventArgs e)
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
            return false;
        }

        private void dg_SQL_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.dg_File.FirstDisplayedScrollingRowIndex = this.dg_SQL.FirstDisplayedScrollingRowIndex;
            }
            catch (Exception ex)
            {
            }
        }

        private void dg_File_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.dg_SQL.FirstDisplayedScrollingRowIndex = this.dg_File.FirstDisplayedScrollingRowIndex;
            }
            catch (Exception ex)
            {
            }
        }


        #region show/hide green rows


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ShowHide("dg_File", true);
                ShowHide("dg_SQL", true);
            }
            else
            {
                ShowHide("dg_File", false);
                ShowHide("dg_SQL", false);
            }
        }


        private void ShowHide(string DG , bool show)
        {
            if (DG == "dg_File")
            {
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dg_File.DataSource];
                currencyManager1.SuspendBinding();
                
                

                foreach (DataGridViewRow item in dg_File.Rows)
                {
                    if (item.DefaultCellStyle.BackColor == Color.Green)
                    {
                        try
                        {
                            item.Visible = show;
                        }
                        catch (Exception ex)
                        {
                            ;
                        }
                    }
                }

                currencyManager1.ResumeBinding();

            }
            else if (DG == "dg_SQL")
            {
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dg_SQL.DataSource];
                currencyManager1.SuspendBinding();



                foreach (DataGridViewRow item in dg_SQL.Rows)
                {
                    if (item.DefaultCellStyle.BackColor == Color.Green)
                    {
                        try
                        {
                            item.Visible = show;
                        }
                        catch (Exception ex)
                        {
                            ;
                        }
                    }
                }

                currencyManager1.ResumeBinding();

            }


        }


        #endregion

    }
}








   


    

