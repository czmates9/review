using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FASK.SledovaniVyroby.Logging;

namespace FASK.SledovaniVyroby.Module.Rezacka
{
    public partial class HistoryUC : UserControl
    {
        private bool allow = false;
        /// <summary>
        /// Priznak, zda je povoleno - tzn zda zobrazovat historii operaci
        /// </summary>
        public bool Allow
        {
            get { return allow; }
            set { allow = value; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HistoryUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nacte drive provedene operace
        /// </summary>
        /// <param name="numberOfOperation">pocet operaci</param>
        public void readHistoricalOperation(int numberOfOperation)
        {
            //Jen pokud je povoleno
            if (!allow) return;

            //Reader
            SqlConnection conn = null;

            try
            {
                //Pripojeni
                conn = new SqlConnection(LogConfig.SqlConnectionStringLocal);
                conn.Open();

                //Prikaz
                string cmdText = Queries.getHistoricalOperations.Replace("$NUMBER$", numberOfOperation.ToString());
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                //Reader a tabulka
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                table.PrimaryKey = new DataColumn[] { table.Columns["faskGUID"] };

                //Uzavru spojeni
                closeConnection(conn);

                //Kontrola, zda je uz dost zaznamu
                if (table.Rows.Count < numberOfOperation)
                {
                    //Otevru spojeni znovu, ale na globalni db
                    conn.ConnectionString = LogConfig.SqlConnectionStringGlobal;
                    conn.Open();
                    //Zbytek zaznamu
                    int rest = numberOfOperation - table.Rows.Count;
                    
                    //Text
                    cmdText = Queries.getHistoricalOperations.Replace("$NUMBER$", rest.ToString());
                    cmd.CommandText = cmdText;
                    
                    //Tabulka a reader
                    DataTable table2 = new DataTable();
                    reader = cmd.ExecuteReader();
                    table2.Load(reader);
                    table2.PrimaryKey = new DataColumn[] { table.Columns["faskGUID"] };

                    //Slouceni
                    table.Merge(table2);
                }

                //Zobrazeni
                dgHistory.DataSource = table;
                dgHistory.Font = new Font("Microsoft Sans Serif", (float)8.25, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                closeConnection(conn);
            }
        }

        /// <summary>
        /// Uzavreni spojeni
        /// </summary>
        /// <param name="conn">spojeni</param>
        private void closeConnection(SqlConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open) conn.Close();
        }

        /// <summary>
        /// Uzavreni readeru
        /// </summary>
        /// <param name="reader">reader</param>
        private void closeReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed) reader.Close();
        }

        /// <summary>
        /// Pridani zaznamu do historie
        /// </summary>
        /// <param name="date">datum</param>
        /// <param name="ido">id operace</param>
        /// <param name="desc">popis</param>
        /// <param name="order">zakazka</param>
        /// <param name="material">material</param>
        /// <param name="scan1">prvni scan</param>
        /// <param name="scan2">druhy scan</param>
        /// <param name="scan3">treti scan</param>
        /// <param name="sensorTotal">hodnota sensoru</param>
        /// <param name="maxRows">maximalni pocet radku</param>
        public void addRecord(string date, string ido, string desc, string order, string material, string scan1, string scan2, string scan3, string sensorTotal, int maxRows)
        {
            //Pridavam zaznamjen pokud je kam
            if (dgHistory.DataSource != null)
            {
                //Poradi sloupcu je dano dotazem v queries - musi odpovidat!!
                DataTable table = (DataTable)dgHistory.DataSource;
                DataRow dr = table.NewRow();
                dr.ItemArray = new object[] { date, ido, desc, order, material, scan1, scan2, scan3, sensorTotal };
                table.Rows.InsertAt(dr, 0);
                dgHistory.DataSource = table;
                dgHistory.CurrentCell = dgHistory.Rows[0].Cells[0];
                //Kontrola poctu radku
                if (dgHistory.Rows.Count > maxRows)
                {
                    //Odstranim posledni radek
                    dgHistory.Rows.RemoveAt(dgHistory.Rows.Count - 1);
                }
            }
        }

        /// <summary>
        /// Vyber bunky v history datagridu
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void dgHistory_SelectionChanged(object sender, EventArgs e)
        {
            //dgHistory.ClearSelection();
        }

        /// <summary>
        /// Ulozi sirku sloupcu
        /// </summary>
        public void saveColumnsWidth()
        {
            //Pokud bylo neco vlozeno
            if (dgHistory.DataSource != null)
            {
                //Vymazu stare hodnoty
                RezackaConfig.config.ColumnsWidth.Rows.Clear();

                //Pokud ano (bylo vlozeno), byla to tabulka  
                foreach (DataGridViewColumn dgvc in dgHistory.Columns)
                {
                    RezackaConfig.config.ColumnsWidth.Rows.Add(new object[] { dgvc.Name, dgvc.Width });
                }

                //Ulozim
                RezackaConfig.Save();
            }
        }

        /// <summary>
        /// Nastave sirku sloupcu
        /// </summary>
        public void loadColumnsWidth()
        {
            //Pokud bylo neco vlozeno a nemaji se sloupce roztahovat automaticky
            if (dgHistory.DataSource != null && dgHistory.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.Fill)
            {
                for (int i = 0; i < RezackaConfig.config.ColumnsWidth.Rows.Count; i++)
                {
                    try { dgHistory.Columns[RezackaConfig.config.ColumnsWidth[i].ColumnName].Width = RezackaConfig.config.ColumnsWidth[i].Width; }
                    catch { /*Napr pokud se zmenil dotaz a sloupec se nepodari najit*/ }
                }
            }
        }
    }
}
