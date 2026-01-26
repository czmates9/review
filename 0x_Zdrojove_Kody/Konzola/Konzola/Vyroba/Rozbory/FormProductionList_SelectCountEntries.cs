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
    public partial class FormProductionList_SelectCountEntries : Form
    {
        public Fask.Interfaces.DataSets.Vyroba AVyrobaDataSet
        {
            get { return this.dsP; }
            set
            {
                this.dsP = value;
                this.bsP.DataSource = this.dsP;
            }
        }

        /// <summary>
        ///Vybrany řadek Vyrobku
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.ProductionImportRow rowProduction
        {
            get
            {
                try
                {
                    return ((DataRowView)(this.dgP.BindingContext[bsP].Current)).Row as Fask.Interfaces.DataSets.Vyroba.ProductionImportRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Eventy formu

        public FormProductionList_SelectCountEntries()
        {
            InitializeComponent();

            this.dgP.UpdateColumnHeaderCellsByDatasource();
        }

        private void FormProductionList_SelectCountEntries_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;


            this.dgP.LoadConfiguration(this.GetType().ToString());

            advancedDataGridViewSearchToolBar1.SetColumns(dgP.Columns);

            //if (this.vyrobaDataSet1 != null)
            //    this.vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();

            //bindingSource1.DataSource = this.vyrobaDataSet1;

        }

        private void FormProductionList_SelectCountEntries_KeyDown(object sender, KeyEventArgs e)
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

        private void FormProductionList_SelectCountEntries_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dgP.SaveConfiguration(this.GetType().ToString());
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

            if (rowProduction == null)
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
            textBox1.Text = rowProduction.CountEntries.ToString();
            //string countentries = this.vyrobaDataSet1.Production[e.RowIndex]["CountEntries"].ToString();
            // = countentries;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                    //PerformOK();
                    PerformVybrat();
            }
            catch
            {
            }
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgP.CurrentCell.ColumnIndex + 1 >= dgP.ColumnCount;
                bool endrow = dgP.CurrentCell.RowIndex + 1 >= dgP.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgP.CurrentCell.ColumnIndex;
                    startRow = dgP.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgP.CurrentCell.ColumnIndex + 1;
                    startRow = dgP.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgP.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgP.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgP.CurrentCell = c;

        }



    }
}
