using System.Windows.Forms;
using System.IO.Ports;
using System;
using System.Data.SqlClient;
using System.Data;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.ErrorLog;
using System.Drawing;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Module.Rezacka
{
    /// <summary>
    /// Komponenta reprezentujici jednu operaci v GUI vcetne jednotlivych hodnot
    /// </summary>
    public partial class OperationUC : UserControl
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public OperationUC(bool free,ToolStripStatusLabel statusLabelParam)
        {
            InitializeComponent();
            current = new Operation(free);

            //Fonty - dedi od rodice a nevim, kde se nastavuje v designeru
            dgOperationInfo.Font = new Font("Microsoft Sans Serif", (float)8.25, FontStyle.Regular);
            dgOperationHeader.Font = new Font("Microsoft Sans Serif", (float)8.25, FontStyle.Regular);
        }

        private Operation current = null;
        /// <summary>
        /// Operace korespondujici se zobrazenymi daty
        /// </summary>
        public void setCurrentOperation(Operation op, string scan1, string scan2, string scan3, string zakazka, string material, string polozka)
        {
            //Nastavim soucasnou - kopie!
            current = new Operation(op);
            //Zobrazim soucasnou
            showCurrentOperation(scan1, scan2, scan3);
            //Zobrazi globals
            showGlobals(zakazka, material, polozka);
        }

        /// <summary>
        /// Nastavi operaci bez zobrazovani - inicializace
        /// </summary>
        /// <param name="op"></param>
        public void setCurrentOperation(bool free)
        {
            //Inicializace
            current = new Operation(free);
        }

        /// <summary>
        /// Zobrazeni globalnich promennych
        /// </summary>
        /// <param name="zakazka">Cislo zakazky</param>
        /// <param name="material">Oznaceni materialu</param>
        /// <param name="polozka">Zkratka polozky</param>
        private void showGlobals(string zakazka, string material, string polozka)
        {
            txtOrderNumber.Text = zakazka;
            txtMaterial.Text = material;
            txtItemAbbr.Text = polozka;
        }

        /// <summary>
        /// Donacteni dodatecnych informaci k operaci
        /// </summary>
        public void readAdditionalInformation(string zakazka, string material)
        {
            //Donacteni hlavicky
            if (current.SPHLAVICKA != string.Empty)
            {
                //Zobrazeni panelu s gridem gridu
                dgOperationHeader.Show();
                //Nacteni
                //readOperationHeader();
                readOperationAdd(readOperationHeaderHandler, current.SPHLAVICKA, zakazka, material);
            }
            else if (current.START)
            {
                dgOperationHeader.Hide();
            }

            //A dodatecnych informaci
            if (current.SPINFO != string.Empty)
            {
                //Zobrazeni panelu s gridem gridu
                dgOperationInfo.Show();
                //Nacteni
                //readOperationInfo();
                readOperationAdd(readOperationInfoHandler, current.SPINFO, zakazka, material);
            }
            else
            {
                dgOperationInfo.Hide();
            }
        }

        //INFO: k asynchronni operaci.
        /*
         * Asynchronni volani s dotazem na databazi:
         * 1.funkce readOperation*
         * 2.readOperation* pripravi sql prikaz, zaregistruje calback funkci (handle) handlerReadInformationAboutOperation a asynchronne zacne s vykonavanim
         * 3.funkce readOperation*Handler provede dokonceni a uzavreni vseho nutneho a Invoke (vyvola formularovou metodu) procReadInformationAboutOperation
         * 4.ReadOperation*Proc zpracuje to co se nacetlo pomoci readeru.
         */

        private void readOperationAdd(AsyncCallback callback, string procedureName, string zakazka, string material)
        {
            //Pripojeni
            SqlConnection conn = null;

            try
            {
                //Pripojeni
                conn = new SqlConnection(LogConfig.SqlConnectionStringGlobal);
                conn.Open();

                //Prikaz
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;

                //Parametry
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@machinetype", LogConfig.MachineType));
                cmd.Parameters.Add(new SqlParameter("@ido", current.IDO));
                string scan1res, scan2res, scan3res;
                getScans(out scan1res, out scan2res, out scan3res);
                cmd.Parameters.Add(new SqlParameter("@scan1", (scan1res == string.Empty) ? null : scan1res));
                cmd.Parameters.Add(new SqlParameter("@scan2", (scan2res == string.Empty) ? null : scan2res));
                cmd.Parameters.Add(new SqlParameter("@scan3", (scan3res == string.Empty) ? null : scan3res));
                cmd.Parameters.Add(new SqlParameter("@sensor", current.SENSOR));
                cmd.Parameters.Add(new SqlParameter("@zakazka", zakazka));
                cmd.Parameters.Add(new SqlParameter("@material", material));

                //Calback
                AsyncCallback ascb = new AsyncCallback(callback);

                //Asynchronni volani
                cmd.BeginExecuteReader(ascb, cmd, CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                closeConnection(conn);
                //Log.Write("Chyba při načítání dodatečných informací k operaci: " + ex.Message);
                ExceptionHandler2.Handle(ex);
            }
        }

        /// <summary>
        /// Handler pro asynchronni volani
        /// </summary>
        /// <param name="target">Hodnota od volajiciho - v tomto pripade SqlCommand</param>
        private void readOperationHeaderHandler(IAsyncResult result)
        {
            //Chvilku pockam - kvuli rezijim windows manageru a popisovaci okna
            System.Threading.Thread.Sleep(500);

            //Reader
            SqlDataReader reader = null;

            try
            {
                //Dokonceni cteni pomoci commandu
                SqlCommand cmd = (SqlCommand)result.AsyncState;
                reader = cmd.EndExecuteReader(result);

                //Vyvolani funkce
                Invoke(new readOperationAddDelegate(readOperationAddProc), new object[] { reader, dgOperationHeader });
            }
            catch (Exception ex)
            {
                closeReader(reader);
                //Log.Write("Chyba při načítání hlavičky k operaci: " + ex.Message);
                ExceptionHandler2.Handle(ex);
            }
        }

        /// <summary>
        /// Handler pro asynchronni volani pro nacteni infa
        /// </summary>
        /// <param name="target">Hodnota od volajiciho - v tomto pripade SqlCommand</param>
        private void readOperationInfoHandler(IAsyncResult result)
        {
            //Chvilku pockam - kvuli rezijim windows manageru a popisovaci okna
            System.Threading.Thread.Sleep(500);

            //Reader
            SqlDataReader reader = null;

            try
            {
                //Dokonceni cteni pomoci commandu
                SqlCommand cmd = (SqlCommand)result.AsyncState;
                reader = cmd.EndExecuteReader(result);

                //Vyvolani funkce
                Invoke(new readOperationAddDelegate(readOperationAddProc), new object[] { reader, dgOperationInfo });
            }
            catch (Exception ex)
            {
                closeReader(reader);
                
                //Log.Write("Chyba při načítání informací k operaci: " + ex.Message);
                ExceptionHandler2.Handle(ex);
            }
        }

        //Delegat pro zpracovani informaci
        private delegate void readOperationAddDelegate(SqlDataReader reader, DataGridView view);

        /// <summary>
        /// Zpracuje informace dane readerem, resp zobrazi je do view
        /// </summary>
        /// <param name="reader">datareader</param>
        /// <param name="view">dataview</param>
        private void readOperationAddProc(SqlDataReader reader, DataGridView view)
        {
            try
            {
                //Tabulka
                DataTable dt = new DataTable();
                dt.Load(reader);

                //Zobrazeni do gridu
                view.DataSource = dt;
                view.Refresh();
            }
            catch (Exception ex)
            {
                closeReader(reader);
                //Log.Write("Chyba při zpracování dodatečných informací k operaci: " + ex.Message);
                ExceptionHandler2.Handle(ex);
            }
        }

        /// <summary>
        /// Uzavre citac z databaze
        /// </summary>
        /// <param name="reader">citac</param>
        private void closeReader (SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed) reader.Close();
        }

        /// <summary>
        /// Uzavre pripojeni k db
        /// </summary>
        /// <param name="conn">pripojeni</param>
        private void closeConnection(SqlConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open) conn.Close();
        }

        /// <summary>
        /// Skryje a vymaze datagridy - napr po prechdu do initu
        /// </summary>
        public void hideDatagridsPanels()
        {
            dgOperationHeader.Hide();
            dgOperationInfo.Hide();
        }

        /// <summary>
        /// Zobrazi datagridy - napr po prechdu do initu
        /// </summary>
        //public void showDatarids()
        //{
        //    dgOperationHeader.Visible = true;
        //}

        /// <summary>
        /// Vrati operaci korespondujici s daty
        /// </summary>
        /// <returns></returns>
        public Operation getCurrentOperation()
        {
            return current;
        }

        /// <summary>
        /// Ziska naskenovane hodnoty
        /// </summary>
        /// <param name="scan1">prvni</param>
        /// <param name="scan2">druhou</param>
        /// <param name="scan3">treti</param>
        public void getScans(out string scan1, out string scan2, out string scan3)
        {
            scan1 = txtScan1.Text.Trim();
            scan2 = txtScan2.Text.Trim();
            scan3 = txtScan3.Text.Trim();   
        }

        /// <summary>
        /// Ziska minule globalni hodnoty
        /// </summary>
        /// <param name="scan1">zakazku</param>
        /// <param name="scan2">material</param>
        /// <param name="scan3">polozku</param>
        public void getGlobals(out string zakazka, out string material, out string polozka)
        {
            zakazka = txtOrderNumber.Text.Trim();
            material = txtMaterial.Text.Trim();
            polozka = txtItemAbbr.Text.Trim();
        }

        /// <summary>
        ///Text komponenty
        /// </summary>
        /// <param name="text">text komponenty</param>
        public string ComponentText
        {
            set { grbOperation.Text = value; }
        }

        /// <summary>
        /// Zobrazi data z aktualni operace do komponenty
        /// </summary>
        private void showCurrentOperation(string scan1, string scan2, string scan3)
        {
            try
            {
                //Zobrazeni infa o operaci
                txtOperationBarcode.Text = current.CK;
                txtOperationCode.Text = current.IDO;
                txtOperationName.Text = current.NAZEV;
                txtNextOperations.Text = current.NEXTOPERATIONS;
                /*
                chbEndOperation.Checked = current.KONEC;
                chbStartOperation.Checked = current.START;
                 */
                txtSensorValue.Text = Operation.sensorDescription(current.SENSOR);
                txtScan1.Text = scan1;
                txtScan2.Text = scan2;
                txtScan3.Text = scan3;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Vypublikovani operacniho kodu - napr zadaneho rucne
        /// (Nakonec je cely textbox vypublikovany, kvuli registraci udalosti ve frmRezacka)
        /// </summary>
        public string OperationBarcode
        {
            get { return txtOperationBarcode.Text.Trim(); }
        }

        /// <summary>
        /// Vyber bunky v info datagridu
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void dgOperationInfo_SelectionChanged(object sender, EventArgs e)
        {
            dgOperationInfo.ClearSelection();
        }

        /// <summary>
        /// Vyber bunky v header datagridu
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void dgOperationHeader_SelectionChanged(object sender, EventArgs e)
        {
            dgOperationHeader.ClearSelection();
        }

        /// <summary>
        /// Nastavi hodnotu zakazky k operaci
        /// </summary>
        //public void OperationOrderNumber { set { txtOrderNumber.Text = value; } }

        /// <summary>
        /// Nastavi hodnotu materialu
        /// </summary>
        //public void OperationMaterial { set { txtMaterial.Text = value; } }

        /// <summary>
        /// Nastavi hodnotu polozky
        /// </summary>
        //public void OperationItemAbbr { set { txtItemAbbr.Text = value; } }
    }
}
