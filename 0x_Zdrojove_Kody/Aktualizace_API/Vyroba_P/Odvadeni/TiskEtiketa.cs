using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Aktualizace_API.Forms;
using JR.Utils.GUI.Forms;

namespace Fask.Aktualizace_API.Odvadeni
{
    public class TiskEtiketa
    {
        public static bool TiskPriprava(Form owner, Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idpracovnik, Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idmachine, Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph, Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp, Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow)
        {
            Fask.SQLiteDBs.DataSets.TiskSablony.SablonyRow sablona = null;
            int pocetVytisku = 1;

            DialogResult dr = FlexibleMessageBox.Show(null, "Chcete vytisknout etikety?", "Tisk", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
                return false;
            else if (dr == DialogResult.Yes)
            { //pokracuje dal...
            }
            else //netisknout nic ...
            {
                return true;
            }

            #region Tisk etiket sarzi ... (Rokospol)
            using (Tisk.FormSablonaVyber ftisksablona = new Fask.Aktualizace_API.Tisk.FormSablonaVyber())
            {
                Fask.SQLiteDBs.DataSets.TiskSablony.SablonyRow[] tiskSablony = (Fask.SQLiteDBs.DataSets.TiskSablony.SablonyRow[])ftisksablona.TiskSablony.Sablony.Select("ITEMTYPE='" + rowvph.SOPTYPE + "'");
                if (tiskSablony.Length == 0 || tiskSablony.Length > 1)
                {
                    ftisksablona.TiskSablonaSet(rowvph.SOPTYPE);
                    if (ftisksablona.ShowDialog(owner) == DialogResult.Cancel)
                        return false;

                    sablona = ftisksablona.TiskSablona;
                }
                else
                {
                    sablona = tiskSablony[0];
                }

                if (String.Compare(rowvph.SOPTYPE, "P", true) == 0)
                    pocetVytisku = 1;
                else
                    pocetVytisku = Convert.ToInt32(productionRow == null ? 0 : productionRow.qty);

                using (FormInputKod finPocet = new FormInputKod())
                {
                    finPocet.Text = "Zadejte počet etiket k vytištění, " + idpracovnik.ToString() + (idmachine == null ? string.Empty : (", " + idmachine.ToString())) + (", " + rowvph.ToString()) + (", " + rowvpp.ToString());
                    finPocet.Kod = pocetVytisku.ToString();
                    if (finPocet.ShowDialog(owner) == DialogResult.Cancel)
                        return false;

                    pocetVytisku = int.Parse(finPocet.Kod);
                }
            }
            #endregion

            #region Priprava tiskovych parametru
            System.Collections.Generic.Dictionary<string, object> dictParams = new Dictionary<string, object>();

            //Pocet vytisku
            dictParams.Add("Q", pocetVytisku.ToString());
            dictParams.Add("QQ", String.Format(new String('0', 4), pocetVytisku));

            foreach (System.Data.DataColumn col in rowvph.Table.Columns)
            {
                if (!dictParams.ContainsKey(col.ColumnName))
                    dictParams.Add(col.ColumnName, rowvph[col]);
                else
                    dictParams[col.ColumnName] = rowvph[col];
            }
            foreach (System.Data.DataColumn col in rowvpp.Table.Columns)
            {
                if (!dictParams.ContainsKey(col.ColumnName))
                    dictParams.Add(col.ColumnName, rowvpp[col]);
                else
                    dictParams[col.ColumnName] = rowvpp[col];
            }
            #endregion

            #region Upresneni hodnot parametru pro tisk
            System.Collections.Generic.Dictionary<string, object> dictParamsSablona = new Dictionary<string, object>();
            try
            {
                StreamReader sr = new StreamReader(sablona.Soubor);
                string sablonaObsah = sr.ReadToEnd();
                sr.Close();
                sr = null;

                System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(sablonaObsah, @"\$[^\$.]*\$");
                //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("$
                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    string matchkey = match.Value.Replace("$", "");
                    if (!dictParamsSablona.ContainsKey(matchkey))
                        dictParamsSablona.Add(matchkey, string.Empty);
                    else
                        dictParamsSablona[matchkey] = string.Empty;

                    if (dictParams.ContainsKey(matchkey))
                    {
                        dictParamsSablona[matchkey] = dictParams[matchkey];
                    }
                }

                using (Tisk.FormDictionaryModify frmdictmod = new Fask.Aktualizace_API.Tisk.FormDictionaryModify(dictParamsSablona, dictParams))
                {
                    if (frmdictmod.ShowDialog(owner) == DialogResult.Cancel)
                        return false;                    
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("TiskEtiketa", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(null, ex.Message, "Tisk etiket", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            #endregion

            #region Tisk
            if (Aktualizace_API.Tisk.Tiskarna.Tiskni(sablona.PrinterName, sablona.Soubor, dictParamsSablona, rowvph.SOPNUMBE.Trim() + " : " + sablona.Nazev.Trim()))
            { // vytisteno
                //Zaznamenat udalost prihlaseni pracovnika
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, idmachine == null ? string.Empty : idmachine.id, DateTime.Now, Settings.UEventTiskEtikety, idpracovnik.id, Settings.TerminalID, rowvph.SOPNUMBE, Guid.NewGuid());
            }
            else
            {
                if (DialogResult.No == FlexibleMessageBox.Show(null, "Vytištění etiket se nezdařilo\nChcete pokračovat dále odvedením výroby?", "Tisk", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    return false;
            }
            #endregion

            return true;
        }
    }
}
