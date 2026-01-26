using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Konzola.Extensions;

namespace Konzola.Vyroba
{
    public partial class FormProductionSourcesList_SelectCountEntries : Form
    {
        public Fask.Interfaces.DataSets.Vyroba AVyrobaDataSet
        {
            get { return this.dsPS; }
            set
            {
                this.dsPS = value;
                this.bsPS.DataSource = this.dsPS;
            }
        }

        /// <summary>
        ///Vybrany řadek Vyrobku
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.Production_SourcesImportRow rowPS
        {
            get
            {
                try
                {
                    return ((DataRowView)(this.dgPS.BindingContext[bsPS].Current)).Row as Fask.Interfaces.DataSets.Vyroba.Production_SourcesImportRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Eventy formu

        public FormProductionSourcesList_SelectCountEntries()
        {
            InitializeComponent();
            this.dgPS.UpdateColumnHeaderCellsByDatasource();
        }

        private void FormProductionSourcesList_SelectCountEntries_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;

            this.dgPS.LoadConfiguration(this.GetType().ToString());

            //if (this.vyrobaDataSet1 != null)
            //    this.vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();

            //foreach (Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow item in this._psDT)
            //{
            //    var row = this.vyrobaDataSet1.Production_Sources.NewProduction_SourcesRow();
            //    row.CountEntries = item.CountEntries;

            //    //row.description = item.description;
            //    row.SOPNUMBE = item.SOPNUMBE;
            //    //if (item.IsISOKNull())
            //    //    row.SetISOKNull();
            //    //else
            //    //    row.ISOK = item.ISOK;

            //    if (item.IsSKL_IDNull())
            //        row.SetSKL_IDNull();
            //    else
            //        row.SKL_ID = item.SKL_ID;

            //    //row.loginid = item.loginid;

            //    //if (item.IsdateeveNull())
            //    //    row.SetdateeveNull();
            //    //else
            //        //row.dateeve = item.dateeve;
            //        //row.qty = item.qty;
            //        //row.qtyReal = item.qtyReal;
            //        //row.UserID = item.UserID;
            //        //row.TermID = item.TermID;
            //        row.GUID = item.GUID;

            //    this.vyrobaDataSet1.Production_Sources.AddProduction_SourcesRow(row);

            //}

            //bindingSource1.DataSource = this.vyrobaDataSet1;

        }

        private void FormProductionSourcesList_SelectCountEntries_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //PerformOK();
                    PerformVybrat();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormProductionSourcesList_SelectCountEntries_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dgPS.SaveConfiguration(this.GetType().ToString());
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            PerformVybrat();
        }

        private void PerformCancel()
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void PerformVybrat()
        {
            //if (string.IsNullOrEmpty(textBox1.Text))
            //    return;

            if (rowPS == null)
                return;

            
            //try
            //{
            //    this._countentries = int.Parse(textBox1.Text);
            //}
            //catch{}

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox1.Text = rowPS.CountEntries.ToString();
            //string countentries = this.vyrobaDataSet1.Production[e.RowIndex]["CountEntries"].ToString();
            // = countentries;
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgPS.CurrentCell.ColumnIndex + 1 >= dgPS.ColumnCount;
                bool endrow = dgPS.CurrentCell.RowIndex + 1 >= dgPS.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgPS.CurrentCell.ColumnIndex;
                    startRow = dgPS.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgPS.CurrentCell.ColumnIndex + 1;
                    startRow = dgPS.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgPS.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgPS.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgPS.CurrentCell = c;



        }



    }
}
