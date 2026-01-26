using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Konzola.Extensions;

namespace Konzola.Vydej
{
    public partial class Form_Predloha_Rekapitulace : Form
    {

        private string FormatText = "Celková váha dávky '{0}' je {1:0.000} Kg";



        public Fask.Interfaces.DataSets.Vydej.CZMST_SE_RekapRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(this.dg_rekap.BindingContext[bs_rekap].Current)).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SE_RekapRow;
                }
                catch
                {
                    return null;
                }
            }
        }


     
        public Form_Predloha_Rekapitulace()
        {
            InitializeComponent();

            this.dg_rekap.UpdateColumnHeaderCellsByDatasource();

            label1.Text = string.Format(FormatText, "X" , "X");
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (SelectedRow == null)
                return;

             //DataGridViewSelectedRowCollection row = dg_rekap.SelectedRows;

             //foreach (DataGridViewRow item in row)
             //{
             //    var a = item;
             //}


             EnumerableRowCollection<Fask.Interfaces.DataSets.Vydej.CZMST_SERow> GrupData = ds_rekap.CZMST_SE.Where(x => x.CountEntries == SelectedRow.CountEntries);

             decimal Vaha = 0;

             foreach (Fask.Interfaces.DataSets.Vydej.CZMST_SERow item in GrupData)
             {
                 decimal tmpVaha = item.IsWEIGHTNull() ? 0 : item.WEIGHT;

                 Vaha += (item.QTYSHPPD * tmpVaha);
             }


             label1.Text = string.Format(FormatText, SelectedRow.CountEntries, Vaha);
        }

        private void Form_Predloha_Rekapitulace_Load(object sender, EventArgs e)
        {

            this.dg_rekap.LoadConfiguration(this.GetType().ToString());

            var GrupData = ds_rekap.CZMST_SE.GroupBy(x => x.CountEntries);


            foreach (var item in GrupData)
            {
                ds_local.CZMST_SE_Rekap.AddCZMST_SE_RekapRow(item.Key);
            }

            ds_local.CZMST_SE_Rekap.AcceptChanges();

            bs_rekap.DataSource = ds_local;
        }

        private void Form_Predloha_Rekapitulace_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Form_Predloha_Rekapitulace_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dg_rekap.SaveConfiguration(this.GetType().ToString());
        }


    }
}
