using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;

using Konzola.Extensions;
using System.Reflection;

namespace Konzola.Vyroba.Transakce
{
    public partial class Form_DisponibilityPrehled : Form
    {

        private Fask.Interfaces.IMES providerP = null;

        public Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable TableDisp;


        #region Eventy formu

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Form_DisponibilityPrehled()
        {
            InitializeComponent();
            this.dg_validateData.UpdateColumnHeaderCellsByDatasource();
        }

        /// <summary>
        /// Load formu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_DisponibilityPrehled_Load(object sender, EventArgs e)
        {

            this.dg_validateData.LoadConfiguration(this.GetType().ToString());

            InitProvider();

            if (providerP == null)
                throw new Exception("Provider 'Vyrobky' není inicializován");


            if ((providerP != null) && (providerP is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametry))
            {

                Fask.Interfaces.Classes.Parametry param = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametry)providerP).GetParametry();
                button1.Enabled = param.PovolZaporneZasoby;
            }
            else
                throw new Exception("IParametry2_GetPovolZaporneZasoby not implementet");

            dg_validateData.DataSource = TableDisp;
        }

        /// <summary>
        /// Metoda volaná před 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_DisponibilityPrehled_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_validateData.SaveConfiguration(this.GetType().ToString());

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        #endregion

        /// <summary>
        /// Metoda navazana na event která odstranuje selectnuty řadek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_validateData_SelectionChanged(object sender, EventArgs e)
        {
            this.dg_validateData.ClearSelection();
        }

        /// <summary>
        /// Metoda navazana na event která ukončí okno 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Konec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        /// <summary>
        /// Metoda pro ukončeni okna
        /// </summary>
        private void PerformCancel()
        {
            try
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Metoda pro inicializaci provideru
        /// </summary>
        private void InitProvider()
        {
            #region Parametry
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerP == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Zbozi.IZbozi2).IsAssignableFrom(t))
                            {
                                providerP = (Fask.Interfaces.Ciselniky.Zbozi.IZbozi2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerP != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerP.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion


   
        }

        /// <summary>
        /// Metoda která určuje formát zobrazení DatraGridView, momentalne určuje barvi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_validateData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_validateData.Rows)
            {

                Fask.POHODA.Disponibility.ValidateData.VydejKontrolaRow radek = (Fask.POHODA.Disponibility.ValidateData.VydejKontrolaRow)((DataRowView)row.DataBoundItem).Row;


                //decimal Navrh = decimal.Parse(row.Cells["navrhDataGridViewTextBoxColumn"].Value.ToString().Trim());
                //decimal Sklad = decimal.Parse(row.Cells["skladDataGridViewTextBoxColumn"].Value.ToString().Trim());


                //decimal zbude = decimal.Parse(row.Cells["zBUDEDataGridViewTextBoxColumn"].Value.ToString().Trim());

                //decimal SKz = decimal.Parse(row.Cells["sTAVSKLADDataGridViewTextBoxColumn"].Value.ToString().Trim());
                //decimal pozadovano = decimal.Parse(row.Cells["qTYSHPPDDataGridViewTextBoxColumn"].Value.ToString().Trim());


                //object tmp = row.Cells["rEZJADataGridViewTextBoxColumn"].Value;
                //decimal? RezJA = null;

                //if (tmp != null)
                //{
                //    string tmpString = tmp.ToString();

                //    if (!string.IsNullOrEmpty(tmpString))
                //    {
                //        RezJA = decimal.Parse(tmpString.Trim());
                //    }
                //}


                //if ((radek.STAV_SKLAD >= 0) && (radek.ZBUDE >= 0) && (radek.STAV_SKLAD >= radek.ZBUDE))
                if (radek.STAV_SKLAD >= 0)
                {
                    //Tady projde každa položka ktera je disponibilny
                    //row.DefaultCellStyle.BackColor = Color.FromArgb(152, 230, 152); // green
                    row.DefaultCellStyle.BackColor = Color.LightGreen; // green

                    //if (zbude == 0)
                    if (radek.STAV_SKLAD == 0)
                    {
                        //row.DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 153); // orange
                        row.DefaultCellStyle.BackColor = Color.SandyBrown; // orange
                    }
                }
                else
                {
                    //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                    //info = new InfoValidace("ERR", item);
                    //row.DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 123); // red
                    row.DefaultCellStyle.BackColor = Color.Crimson; // red
                    //break;
                }
            }
        }

        /// <summary>
        /// Metoda navazana na Click tlačitka "Zpracuj tak jak je!"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Retry;
        }

        /// <summary>
        /// Search event pro toolpanel hledani...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_validateData.CurrentCell.ColumnIndex + 1 >= dg_validateData.ColumnCount;
                bool endrow = dg_validateData.CurrentCell.RowIndex + 1 >= dg_validateData.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_validateData.CurrentCell.ColumnIndex;
                    startRow = dg_validateData.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_validateData.CurrentCell.ColumnIndex + 1;
                    startRow = dg_validateData.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_validateData.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_validateData.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_validateData.CurrentCell = c;


        }
    }
}
