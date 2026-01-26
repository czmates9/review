using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FASK.Palety_SSCC.SQLite.Classes
{
    public class SSCC_Generator
    {

        /// <summary>
        /// Tahle metoda by mnela sloužit pro vytažení Countu od někud, a vratí kount pro danou sequenci
        /// </summary>
        /// <param name="sequence_ID">ID sequence pro kterou vrací count</param>
        /// <param name="MachineID">ID terminalu, muže sloužit jak filtr pro count</param>
        /// <param name="Count">O kolik má skočit count</param>
        /// <returns></returns>
        private int Get_SSCC_Sequence(
            int sequence_ID,
            int Count
            )
        {

            // Popis logiky
            // 1. zistit zda existuje sequence dle požadovaneho ID
            // 1.1 ANO - Tak vytahne Count a přičte k nemu o kolik se ma posunout a uloží
            // 1.2 NE - tak vytvořit s defaultní ID sequence a count 0

            DataSets.DS_SSCC ds = new DataSets.DS_SSCC();
            Classes.Database db = new Database();

            int SequenceOUT = 1;

            //podivat se do DB a pokud nic neni tak dat insert
            db.Fill_CZMST_SSCC_SEQUENCE_ByID(ds.CZMST_SSCC_SEQUENCE, sequence_ID);

            if(ds.CZMST_SSCC_SEQUENCE != null && ds.CZMST_SSCC_SEQUENCE.Count > 0)
            {
                var row = ds.CZMST_SSCC_SEQUENCE.First();
                SequenceOUT = row.sequence_count + Count;
                db.Update_CZMST_SSCC_SEQUENCE(sequence_ID, SequenceOUT);
            }
            else
            {
                db.Insert_CZMST_SSCC_SEQUENCE(sequence_ID, SequenceOUT);
            }

            return SequenceOUT;
        }

        public string Get_Next_SSCC(
            int sequence_ID,
            int Count,
            int Machine_ID
            )
        {
            try
            {

                //zkontrolovat jestli file prd existuje

                //existuje zakladaci skript

                SQLite_Helper helper = new SQLite_Helper();
                if (!File.Exists(helper.FilePathSQLite_prd))
                {
                    if (!helper.SQLite_CreateFile())
                    {
                        throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro SSCC");
                    }
                }

                //-----konec prace se souborem-------

                DataSets.DS_SSCC ds = new DataSets.DS_SSCC();
                Database db = new Database();

                var sequence = Get_SSCC_Sequence(sequence_ID, Count);

                db.Fill_CZMST_SSCC_PARAMETERS_ByID(ds.CZMST_SSCC_PARAMETERS, sequence_ID);

                string SSCC = string.Empty;
                string GCP = string.Empty;
                int GCP_Count = 0;
                string LV = string.Empty;

                if (ds.CZMST_SSCC_PARAMETERS != null && ds.CZMST_SSCC_PARAMETERS.Count > 0)
                {

                    var row = ds.CZMST_SSCC_PARAMETERS.First();

                    GCP = row.GCP.ToString();
                    GCP_Count = row.GCP_count;


                }
                else
                {
                    int gcp_int = 123456;
                    GCP = gcp_int.ToString();

                    GCP_Count = 7;

                    db.Insert_CZMST_SSCC_PARAMETERS(sequence_ID, "Paleta", Machine_ID, gcp_int, GCP_Count);
                }


              

                LV = Machine_ID.ToString();

                if (LV.Length == 1)
                {
                    SSCC += "00";
                }
                else if (LV.Length == 2)
                {
                    SSCC += "0";
                }
                else //if (LV.Length != 1)
                    throw new Exception("LV je  jak jeden nebo dva znaky");
               

                SSCC += LV;

                SSCC += GCP.PadLeft(GCP_Count, '0');

                SSCC += sequence.ToString().PadLeft(20 - 1 - SSCC.Length, '0');

                SSCC += BarCodes.CountParity_Modulo10(SSCC);

                if (SSCC.Length != 20)
                    throw new Exception("SSCC ma jiny počet znaku jak 20");

                return SSCC;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
