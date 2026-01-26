using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.POHODA.Disponibility
{
    /// <summary>
    /// Jedna se o tridu ktera zabespecuje kontrolu Diponibility vuči IS POHODA
    /// </summary>
    public  class CheckDisp
    {
        /// <summary>
        /// Hlavní metoda pomoci které se provadi kontrola Disponibility
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
       public StatusInfo KontrolaDisponibility(ValidateData ds, string ConnectionString)
        { 
            StatusInfo si = null;

            //1. Doplním data z pohody do Datasetu
            FillDataFromPohoda(ds,ConnectionString);

            //2. Metoda se samotnou logikou kontroly Disponibility
            //InfoValidace info = ValidateDataFromPohoda(ds);
            InfoValidace info = ValidateDataFromPohodaV2(ds);


            //Rozhovovaci logika zda pusim anebo ne
            foreach (var item in info.DTVydej)
            {
                #region puvodne
                ////if (item.STAV_SKLAD >= 0)
                //if ((item.STAV_SKLAD >= 0) && (item.ZBUDE >= 0) && (item.STAV_SKLAD >= item.ZBUDE))
                //{
                //    //Tady projde každa položka ktera je disponibilny
                //}
                //else
                //{
                //    //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                //    info = new InfoValidace("ERR", item);
                //    break;
                //} 
                #endregion

                #region 8.1.2020 ZdD + JaS uprava podminek

                if (item.IsREZ_JANull() || string.IsNullOrEmpty(item.REZ_JA))
                {

                    if ((item.STAV_SKLAD >= 0) && (item.ZBUDE >= 0) && (item.STAV_SKLAD >= item.ZBUDE))
                    {
                        //Tady projde každa položka ktera je disponibilny
                    }
                    else
                    {
                        //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                        info = new InfoValidace("ERR", item);
                        break;
                    }

                }
                else
                {
                    if ((item.STAV_SKLAD >= 0))
                    {
                        //Tady projde každa položka ktera je disponibilny
                    }
                    else
                    {
                        //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                        info = new InfoValidace("ERR", item);
                        break;
                    }
                }

                #endregion


            }

            //Logika ktera se rozhoduje zda se z nalezene nedisponibilni polozky sestavi message pro informovani uživatele alebo pošle pouze status OK
            if (info.Status == "OK")
            {
                //ve je OK a mužeme pokračovat
                si = new StatusInfo(0, "OK");
            }
            else if (info.Status == "ERR")
            {

                string Message = string.Empty;



                Message += "Nedostatek zásoby " + info.RadekVydej.SKz_IDS.Trim() + " v IS POHODA" + Environment.NewLine;
                Message += info.RadekVydej.IsSOPNUMBENull() ? string.Empty : "Obj.:" + info.RadekVydej.SOPNUMBE.Trim() + Environment.NewLine;
                Message += info.RadekVydej.IsITEMNMBRNull() ? string.Empty : "Pol.:" + info.RadekVydej.ITEMNMBR.Trim() + Environment.NewLine;
                Message += info.RadekVydej.IsSKz_IDSNull() ? string.Empty : "Kod:" + info.RadekVydej.SKz_IDS.Trim() + Environment.NewLine;
                Message += info.RadekVydej.IsSKz_EANNull() ? string.Empty : "EAN:" + info.RadekVydej.SKz_EAN.Trim() + Environment.NewLine;
                Message += info.RadekVydej.IsSKL_IDNull() ? string.Empty : "Sklad:" + info.RadekVydej.SKL_ID + Environment.NewLine;

                Message += info.RadekVydej.IsQTYSHPPDNull() ? string.Empty : "MN:" + info.RadekVydej.QTYSHPPD.ToString() + Environment.NewLine;
                Message += info.RadekVydej.IsZBUDENull() ? string.Empty : "ErrMN:" + info.RadekVydej.ZBUDE.ToString() + Environment.NewLine;

                //si = new StatusInfo(1, string.Format("Obj.:{0}" + Environment.NewLine + "Pol.:{1}" + Environment.NewLine + "MN:{2}" + Environment.NewLine + "ErrMN:{3} ", info.RadekVydej.SOPNUMBE, info.RadekVydej.ITEMNMBR, info.RadekVydej.QTYSHPPD, info.RadekVydej.ZBUDE));
                si = new StatusInfo(2, Message);
            }
            else
            {
                si = new StatusInfo(2, info.Status);
            }

            return si;
 
        }

        /// <summary>
        /// Metoda ktera skontroluje disponibilitu položek ale vrati celou tabulku s provedenyma vypočtama, nic víc neřeší
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
       public Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable KontrolaDisponibilityDT(ValidateData ds, string ConnectionString)
       {

           //1. Doplním data z pohody do Datasetu
           FillDataFromPohoda(ds, ConnectionString);

           //2. Metoda se samotnou logikou kontroly Disponibility
           InfoValidace info = ValidateDataFromPohodaV2(ds);

           return info.DTVydej;

       }

        #region FillDataFromPohoda

        /// <summary>
        /// Metoda pro dotažení dat z Pohody pro všechny zadané řádky
        /// </summary>
        /// <param name="ds">Dataset naplnení hodnota ke kontrole</param>
        /// <param name="ConnectionString">ConnectionString do pohody</param>
        private void FillDataFromPohoda(ValidateData ds, string ConnectionString)
        {

            // \TODO: optimaloyovat pomoci otevreneho objektu Connection

            System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection(ConnectionString);

            try
            {
                con.Open();

                foreach (ValidateData.DataDispRow item in ds.DataDisp)
                {
                    if (item.IsSOPNUMBENull() && item.IsORDNull())
                    {
						try
						{
							//Pokud je stloupec SOPNUMBE a ORD NULL tak to znamená že se jedná o kontrolu dat bez předohy z OBJ 

							ValidateData.DataDispDataTable dt = Kontrola_GetData_ITEMNMBR(con, int.Parse(item.ITEMNMBR.Trim()));

							if ((dt != null) && (dt.Count > 0))
							{
								item.SKz_Rezer = dt.First().IsSKz_RezerNull() ? 0 : dt.First().SKz_Rezer;
								item.SKz_StavZ = dt.First().IsSKz_StavZNull() ? 0 : dt.First().SKz_StavZ;
								item.SKz_EAN = dt.First().IsSKz_EANNull() ? string.Empty : dt.First().SKz_EAN;
								item.SKz_IDS = dt.First().IsSKz_IDSNull() ? string.Empty : dt.First().SKz_IDS;
							}

							if (dt == null)
								throw new Exception(String.Format("Kontrola_GetData_ITEMNMBR: dt == null : {0}", dt == null));
							if (dt.Count <= 0)
								throw new Exception(String.Format("Kontrola_GetData_ITEMNMBR: dt.Count <= 0 : {0}", dt.Count <= 0));
							if (dt.First().IsSKz_RezerNull())
								throw new Exception(String.Format("Kontrola_GetData_ITEMNMBR: dt.First().IsSKz_RezerNull() : {0}", dt.First().IsSKz_RezerNull()));
						}
						catch (System.Exception ex)
						{
							string Message = string.Empty;

							Message += item.IsSOPNUMBENull() ? string.Empty : "Obj.:" + item.SOPNUMBE.Trim() + Environment.NewLine;
							Message += item.IsITEMNMBRNull() ? string.Empty : "Pol.:" + item.ITEMNMBR.Trim() + Environment.NewLine;
							Message += item.IsSKz_IDSNull() ? string.Empty : "Kod:" + item.SKz_IDS.Trim() + Environment.NewLine;
							Message += item.IsSKz_EANNull() ? string.Empty : "EAN:" + item.SKz_EAN.Trim() + Environment.NewLine;
							Message += item.IsSKL_IDNull() ? string.Empty : "Sklad:" + item.SKL_ID + Environment.NewLine;

							//throw new Exception(string.Format(Message, item.SOPNUMBE), ex);
                            throw new Exception(Message, ex);
                        }
					}
                    else
                    {
						try
						{
							//Pokud neni stloupec SOPNUMBE a ORD NULL tak to znamená že se jedná o kontrolu dat s předohy z OBJ

							ValidateData.DataDispDataTable dt = Kontrola_GetData_ITEMNMBR_SOPNUMBE(con, item.SOPNUMBE.Trim(), int.Parse(item.ITEMNMBR.Trim()));

							if ((dt != null) && (dt.Count > 0))
							{
								item.OBJ_Rezer = dt.First().IsOBJ_RezerNull() ? false : dt.First().OBJ_Rezer;
								item.SKz_Rezer = dt.First().IsSKz_RezerNull() ? 0 : dt.First().SKz_Rezer;
								item.SKz_StavZ = dt.First().IsSKz_StavZNull() ? 0 : dt.First().SKz_StavZ;
								item.SKz_EAN = dt.First().IsSKz_EANNull() ? string.Empty : dt.First().SKz_EAN;
								item.SKz_IDS = dt.First().IsSKz_IDSNull() ? string.Empty : dt.First().SKz_IDS;
							}

							if (dt == null)
								throw new Exception(String.Format("Kontrola_GetData_ITEMNMBR_SOPNUMBE: dt == null : {0}", dt == null));
							if (dt.Count <= 0)
								throw new Exception(String.Format("Kontrola_GetData_ITEMNMBR_SOPNUMBE: dt.Count <= 0 : {0}", dt.Count <= 0));
							if (dt.First().IsSKz_RezerNull())
								throw new Exception(String.Format("Kontrola_GetData_ITEMNMBR_SOPNUMBE: dt.First().IsSKz_RezerNull() : {0}", dt.First().IsSKz_RezerNull()));

							//decimal? tmpdec = Get_OBJpol_ZbyvaDodat(item.ORD, ConnectionString);
							decimal? tmpdec = Get_OBJpol_ZbyvaDodatCelkemZaOBJ(item.SOPNUMBE, item.ITEMNMBR, con);

							if (tmpdec.HasValue)
								item.OBJPol_QTY_ZbyvaDodatCelkem = (decimal)tmpdec;
							else
								item.SetOBJPol_QTY_ZbyvaDodatCelkemNull();

							decimal? tmpdecpol = Get_OBJpol_ZbyvaDodat(item.ORD, con);

							if (tmpdecpol.HasValue)
								item.OBJPol_QTY_ZbyvaDodatPolozka = (decimal)tmpdecpol;
							else
								item.SetOBJPol_QTY_ZbyvaDodatPolozkaNull();
						}
						catch (System.Exception ex)
						{
							string Message = string.Empty;

							Message += item.IsITEMNMBRNull() ? string.Empty : "Pol.:" + item.ITEMNMBR.Trim() + Environment.NewLine;
							Message += item.IsSKz_IDSNull() ? string.Empty : "Kod:" + item.SKz_IDS.Trim() + Environment.NewLine;
							Message += item.IsSKz_EANNull() ? string.Empty : "EAN:" + item.SKz_EAN.Trim() + Environment.NewLine;
							Message += item.IsSKL_IDNull() ? string.Empty : "Sklad:" + item.SKL_ID + Environment.NewLine;

							throw new Exception(string.Format(Message, item.SOPNUMBE), ex);
						}

                    }

                }
            }
            finally
            {

                if ((con != null) && (con.State & ConnectionState.Open) == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Metoda pro dokažení dat z POHODY
        /// </summary>
        /// <param name="CS">ConnectionString do pohody</param>
        /// <param name="Cislo">SOPNUMBE, Cislo na tabulke OBJ</param>
        /// <param name="ID">Identifikator, ITEMNMBR co je ID z tabulky SKz </param>
        /// <returns>Dokažené data pro naplnění</returns>
        private ValidateData.DataDispDataTable Kontrola_GetData_ITEMNMBR_SOPNUMBE(System.Data.OleDb.OleDbConnection con, string Cislo, int ID)
        {
            ValidateData.DataDispDataTable dataTable = new ValidateData.DataDispDataTable();
            Kontrola_Fill_ITEMNMBR_SOPNUMBE(dataTable, con, Cislo, ID);
            return dataTable;
        }

        /// <summary>
        /// Metoda pro dokažení dat z POHODY
        /// </summary>
        /// <param name="dataTable">Dokažené data pro naplnění</param>
        /// <param name="CS">ConnectionString do pohody</param>
        /// <param name="Cislo">SOPNUMBE, Cislo na tabulke OBJ</param>
        /// <param name="ID">Identifikator, ITEMNMBR co je ID z tabulky SKz</param>
        private void Kontrola_Fill_ITEMNMBR_SOPNUMBE(ValidateData.DataDispDataTable dataTable, System.Data.OleDb.OleDbConnection con, string Cislo, int ID)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT o.Rezer AS OBJ_Rezer, s.Rezer AS SKz_Rezer, s.StavZ AS SKz_StavZ, s.IDS as SKz_IDS, s.EAN as SKz_EAN  " +
                                                "FROM dbo.OBJ AS o LEFT OUTER JOIN dbo.OBJpol AS p ON p.RefAg = o.ID " +
                                                "LEFT OUTER JOIN dbo.SKz AS s ON s.ID = p.RefSKz " +
                                                "WHERE (o.Cislo = ?) AND (s.ID = ?)";
                da.SelectCommand.Parameters.AddWithValue("?", Cislo);
                da.SelectCommand.Parameters.AddWithValue("?", ID);

                da.SelectCommand.Connection = con;

                da.Fill(dataTable);

				string x = String.Format("C:{0}, ID:{1}, CS{2})", Cislo, ID, con.ConnectionString );

				if (dataTable == null)
					throw new Exception(String.Format("Kontrola_Fill_ITEMNMBR_SOPNUMBE: dataTable == null : {0}, x = {1}", dataTable == null, x));
				if (dataTable.Count <= 0)
					throw new Exception(String.Format("Kontrola_Fill_ITEMNMBR_SOPNUMBE: dataTable.Count <= 0 : {0}, x = {1}", dataTable.Count <= 0, x));
				if (dataTable.First().IsSKz_RezerNull())
					throw new Exception(String.Format("Kontrola_Fill_ITEMNMBR_SOPNUMBE: dataTable.First().IsSKz_RezerNull() : {0}, x = {1}", dataTable.First().IsSKz_RezerNull(), x));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// Metoda pro dokažení dat z POHODY
        /// </summary>
        /// <param name="CS">ConnectionString do pohody</param>
        /// <param name="ID">Identifikator, ITEMNMBR co je ID z tabulky SKz </param>
        /// <returns>Dokažené data pro naplnění</returns>
        private ValidateData.DataDispDataTable Kontrola_GetData_ITEMNMBR(System.Data.OleDb.OleDbConnection con, int ID)
        {
            ValidateData.DataDispDataTable dataTable = new ValidateData.DataDispDataTable();
            Kontrola_Fill_ITEMNMBR(dataTable, con, ID);
            return dataTable;
        }


        /// <summary>
        /// Metoda pro dokažení dat z POHODY
        /// </summary>
        /// <param name="CS">ConnectionString do pohody</param>
        /// <param name="ID">Identifikator, ITEMNMBR co je ID z tabulky SKz </param>
        /// <returns>Dokažené data pro naplnění</returns>
        private void Kontrola_Fill_ITEMNMBR(ValidateData.DataDispDataTable dataTable, System.Data.OleDb.OleDbConnection con, int ID)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.OleDb.OleDbCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"SELECT s.Rezer AS SKz_Rezer, s.StavZ AS SKz_StavZ, s.IDS as SKz_IDS, s.EAN as SKz_EAN " +
                                                "FROM dbo.SKz AS s " +
                                                "WHERE (s.ID = ?)";
                da.SelectCommand.Parameters.AddWithValue("?", ID);

                da.SelectCommand.Connection = con;

                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


		/// <summary>
		/// Metoda která vrati množstvi ktere je na položce  na Objednavce
		/// </summary>
		/// <param name="ORD">ID řádku</param>
		/// <param name="CS">Connection string</param>
		/// <returns></returns>
		internal decimal? Get_OBJpol_ZbyvaDodat(int ORD, System.Data.OleDb.OleDbConnection con)
		{
			//System.Data.OleDb.OleDbConnection connection = null;
			System.Data.OleDb.OleDbCommand command = null;
			//System.Data.OleDb.OleDbDataAdapter adapter = null;

			try
			{

				//connection = new System.Data.OleDb.OleDbConnection(CS);
				command = new System.Data.OleDb.OleDbCommand();
				//adapter = new System.Data.OleDb.OleDbDataAdapter();


				command.CommandText = "select Mnozstvi - Dodano" +
					" from OBJPol " +
					" where ID = " + ORD.ToString();


				command.Connection = con;
				//connection.Open();

				object rez_jaOBJECT = command.ExecuteScalar();

				//connection.Close();



				if (rez_jaOBJECT is double)
				{
					return (decimal)((double)rez_jaOBJECT);
				}
				else
				{

					return null;
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda která vrati množstvi ktere je na položce na CELOU Objednavku
		/// </summary>
		/// <param name="ORD">ID řádku</param>
		/// <param name="CS">Connection string</param>
		/// <returns></returns>
		internal decimal? Get_OBJpol_ZbyvaDodatCelkemZaOBJ(string SOPNUMBE, string ITEMNMBR, System.Data.OleDb.OleDbConnection con)
		{
			//System.Data.OleDb.OleDbConnection connection = null;
			System.Data.OleDb.OleDbCommand command = null;

			try
			{

				//connection = con;
				command = new System.Data.OleDb.OleDbCommand();

				command.CommandText =
						"SELECT SUM(Mnozstvi - Dodano) " +
						" FROM OBJPol as pol " +
						" LEFT JOIN OBJ as o ON o.ID = pol.RefAg " +
						" WHERE o.Cislo = '" + SOPNUMBE.Trim() + "' " +
						" AND pol.RefSKz = " + ITEMNMBR.Trim();

				command.Connection = con;
				//connection.Open();

				object rez_jaOBJECT = command.ExecuteScalar();

				//connection.Close();

				if (rez_jaOBJECT is double)
				{
					return (decimal)((double)rez_jaOBJECT);
				}
				else
					return null;

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


        #endregion

        #region ValidateData
        
        /// <summary>
        /// Metdoa ktera nese cely algoritmus kontroly disponibility
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public InfoValidace ValidateDataFromPohodaV2(ValidateData ds)
        {

            //1.definice promennych a flagu
            //2. Prochazeni řadek po řadku. Nutno je aby data pred vstupen do tohoto algoritmu byly OrderBy ITEMNMBR
            //2.1 Kontrola predchoziho radku, zda obsahuje stejny ITEMNMBR jak aktualny
            //2.2 Zistetni zda je položka na rezervovanej Objednavke, pokud ne tak se bere pouze zadane mnozstvi
            //2.3 Algoritmus kontroly, vypočet podle toho zda je to prvni nalez položky anebo n-ty vyskyt podle itemnmbr
            //2.4 Tvorba noveno řadku s datama

            ValidateData localds = new ValidateData();
            Fask.POHODA.Disponibility.ValidateData.VydejKontrolaRow PrevRow = null;

            try
            {

                if ((ds != null) && (ds.DataDisp != null) && (ds.DataDisp.Count > 0))
                {
                    #region 1. definice
                    decimal RezervovanoJA = 0;
                    decimal RezervovanoOstatni = 0;
                    decimal PolozekZbude = 0;
                    decimal PolozekStavSklad = 0;
                    decimal PolozekStavSkladPred = 0;
                    
                    bool Flag_SameITEMNMBR = false;
                    bool Flag_Rezervovano = false;
                    #endregion
                    #region 2. Foreach

                    //foreach (ValidateData.DataDispRow item in ds.DataDisp.OrderBy(x=> x.ITEMNMBR ).OrderByDescending(x => x.OBJ_Rezer))
                    foreach (ValidateData.DataDispRow item in ds.DataDisp)
                    {
                        var Radek = localds.VydejKontrola.NewVydejKontrolaRow();

                        #region 2.1 Kontrola predchoziho radku

                        if (PrevRow != null)
                        {
                            if (item.ITEMNMBR.Trim() == PrevRow.ITEMNMBR.Trim())
                                Flag_SameITEMNMBR = true;
                            else
                                Flag_SameITEMNMBR = false;
                        }
                        else
                        {
                            Flag_SameITEMNMBR = false;
                        }

                        #endregion

                        #region 2.2 Zistetni zda je položka na rezervovanej Objednavke
                        Flag_Rezervovano = item.IsOBJ_RezerNull() ? false : item.OBJ_Rezer;

                        #endregion

                        #region 2.3 Algoritmus

                        decimal mnozstvi_navic = 0;
                        decimal mnozstvi_rezervovano = 0;

						RezervovanoOstatni = item.IsSKz_RezerNull() ? 0 : item.SKz_Rezer;
						RezervovanoJA = 0;

                        #region 5.11.2019 Testovani, ruzne varianty vypočtu

                        //if (!item.IsOBJPol_QTY_ZbyvaDodatCelkemNull())
                        //{
                        //    var objpolqtyrez = item.OBJPol_QTY_ZbyvaDodatCelkem < 0 ? 0 : item.OBJPol_QTY_ZbyvaDodatCelkem; // kolik mam na Objednavce

                        //    RezervovanoOstatni -= objpolqtyrez;
                        //    RezervovanoJA = objpolqtyrez;
                        //    if (Flag_SameITEMNMBR)
                        //    {

                        //        decimal vydano = item.SKz_StavZ - PrevRow.STAV_SKLAD;
                        //        RezervovanoJA -= vydano;
                        //        //RezervovanoJA = RezervovanoJA < 0 ? 0 : RezervovanoJA;
                        //        RezervovanoJA = Math.Max(0, RezervovanoJA);
                        //    }

                        //}

                        //if (Flag_Rezervovano)
                        //{
                        //    mnozstvi_navic = item.QTY - RezervovanoJA; // jedna se o množstvi navíc nad Rezevaci
                        //    mnozstvi_navic = mnozstvi_navic < 0 ? 0 : mnozstvi_navic; // pokud je zaporne anebo nula tak neni nic navic
                        //    //mnozstvi_rezervovano = Math.Max(RezervovanoJA, item.QTY); // množstvi na vydej z rezevovanych
                        //    mnozstvi_rezervovano = item.QTY - mnozstvi_navic;
                        //}
                        //else
                        //{
                        //    mnozstvi_navic = item.QTY; // pokud polozka neni rezervovana tak se bere pouze zadane množstvi
                        //    mnozstvi_rezervovano = 0; // rezervovano je nula
                        //}

                        //if (Flag_SameITEMNMBR)
                        //{
                        //    //  \TODO : nutne otestovat (a pravdepodbne jeste i upravit algoritmus ?)

                        //    //RezervovanoOstatni = PrevRow.REZ_OSTATNI - mnozstvi_rezervovano; // odečet mojich z skutecnych rezervovanych 
                        //    //RezervovanoOstatni = item.SKz_Rezer - mnozstvi_rezervovano; // od rezevraci odečtu co chci vydat 
                        //    //PolozekZbude = Math.Max(0, (PrevRow.STAV_SKLAD - PrevRow.REZ_OSTATNI)) - mnozstvi_navic; // pro n tu polozku z realneho stavu odecte co chce vydat
                        //    //PolozekZbude = Math.Max(0, (PrevRow.STAV_SKLAD - item.SKz_Rezer)) - mnozstvi_navic; // pro n tu polozku z realneho stavu odecte co chce vydat
                        //    //PolozekZbude = Math.Max(0,PrevRow.ZBUDE)  - mnozstvi_navic; // pro n tu polozku z realneho stavu odecte co chce vydat
                        //    PolozekZbude = PrevRow.ZBUDE - mnozstvi_navic; // pro n tu polozku z realneho stavu odecte co chce vydat
                        //    //PolozekStavSklad = PrevRow.STAV_SKLAD - item.QTY;
                        //    //PolozekStavSklad = PolozekZbude >= 0 ? PrevRow.STAV_SKLAD - item.QTY : PrevRow.STAV_SKLAD;
                        //    PolozekStavSklad = PrevRow.STAV_SKLAD - item.QTY;
                        //    // \TODO : ?PolozekZbude >= MinLim z IS POHODA 

                        //    PolozekStavSkladPred = PrevRow.STAV_SKLAD;
                        //}
                        //else
                        //{
                        //    //RezervovanoOstatni = item.SKz_Rezer - mnozstvi_rezervovano; // od rezevraci odečtu co chci vydat 
                        //    //PolozekZbude = Math.Max(0, (item.SKz_StavZ - item.SKz_Rezer)) - mnozstvi_navic; // zbude volnych nad ramec rezervaci minus moje navic
                        //    PolozekZbude = item.SKz_StavZ - item.SKz_Rezer - mnozstvi_navic; // zbude volnych nad ramec rezervaci minus moje navic
                        //    //PolozekStavSklad = PolozekZbude >= 0 ? item.SKz_StavZ - item.QTY : item.SKz_StavZ; // pokud je počet zbyte zaporny tak se neprovede odpočet ze stavu skladu
                        //    PolozekStavSklad = item.SKz_StavZ - item.QTY;
                        //    // \TODO : ?PolozekZbude >= MinLim z IS POHODA 

                        //    PolozekStavSkladPred = item.SKz_StavZ;
                        //} 

                        #endregion

                        #region 5.11.2019 k dnešnimu dni konečna verze vypočtu

                        if (Flag_Rezervovano)
                        {
                            // 25.11.2019 : TaD + ZdD + JiS upraveno do podminky rezervovano, lebo pouze Objednavka muže byt rezervovana
                            // pokud je z objednavky : zbyvadodat neni null
                            // a plati pouze pro rezervovanou objednavku
                            if (!item.IsOBJPol_QTY_ZbyvaDodatCelkemNull())
                            {
                                // docasna promenna, ktera rika kolik zbyva dodat, pokud je >= 0, protoze muze byt preplnena ... 
                                var objpolqtyrez = item.OBJPol_QTY_ZbyvaDodatCelkem < 0 ? 0 : item.OBJPol_QTY_ZbyvaDodatCelkem; // kolik mam na Objednavce

                                RezervovanoOstatni -= objpolqtyrez;
                                RezervovanoJA = objpolqtyrez;
                                if (Flag_SameITEMNMBR)
                                {

                                    decimal vydano = item.SKz_StavZ - PrevRow.STAV_SKLAD;
                                    RezervovanoJA -= vydano;
                                    RezervovanoJA = Math.Max(0, RezervovanoJA);
                                }
                            }

                            mnozstvi_navic = item.QTY - RezervovanoJA;
                            mnozstvi_navic = mnozstvi_navic < 0 ? 0 : mnozstvi_navic; 
                            mnozstvi_rezervovano = item.QTY - mnozstvi_navic;
                        }
                        else
                        {
                            mnozstvi_navic = item.QTY;
                            mnozstvi_rezervovano = 0; 
                        }

                        if (Flag_SameITEMNMBR)
                        {
                            PolozekZbude = PrevRow.ZBUDE - mnozstvi_navic; 
                            PolozekStavSklad = PrevRow.STAV_SKLAD - item.QTY;
                            PolozekStavSkladPred = PrevRow.STAV_SKLAD;
                        }
                        else
                        {
							decimal tmpSKz_StavZ = item.IsSKz_StavZNull() ? 0 : item.SKz_StavZ;
							decimal tmpSKz_Rezer = item.IsSKz_RezerNull() ? 0 : item.SKz_Rezer;

							PolozekZbude = tmpSKz_StavZ - tmpSKz_Rezer - mnozstvi_navic; 
                            PolozekStavSklad = item.SKz_StavZ - item.QTY;
                            PolozekStavSkladPred = item.SKz_StavZ;
                        }

                        #endregion

                        #endregion

                        #region 2.4 Tvorba noveho radku

						Radek.ITEMDESC = item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC; // \TODO Treba doplnit, pro kontrolu vydej na konzole
						Radek.DEX_ROW_ID = item.IsDEX_ROW_IDNull() ? -1 : item.DEX_ROW_ID; // \TODO Treba doplnit pro kontrolu Vydej na konzole
                        Radek.ITEMNMBR = item.ITEMNMBR.Trim();
                        Radek.QTYSHPPD = item.QTY;

                        Radek.REZ_OSTATNI = RezervovanoOstatni;

                        Radek.SOPNUMBE = item.IsSOPNUMBENull() ? null : item.SOPNUMBE.Trim();
                        
                        if(item.IsORDNull())
                            Radek.SetORDNull(); 
                        else 
                            Radek.ORD =  item.ORD;

                        Radek.STAV_SKLAD_PRED = PolozekStavSkladPred;
                        //Radek.STAV_SKLAD = item.SKz_StavZ;
                        
                        Radek.STAV_SKLAD = PolozekStavSklad; // do dalsiho kola prenasim stav zasoby - vydane
                        //Radek.STAV_SKLAD = item.SKz_StavZ - item.QTY; // do dalsiho kola prenasim stav zasoby - vydane

                        Radek.ZADAT = item.QTY;
                        Radek.ZBUDE = PolozekZbude; //polozek zbyva k volnemu vydeji ...

                        Radek.REZ_JA = Flag_Rezervovano ? RezervovanoJA.ToString("0.000") : null;

                        if(item.IsOBJPol_QTY_ZbyvaDodatPolozkaNull())
                        {
                            Radek.SetQTY_OBJ_PohodaNull();
                        }
                        else
                        {
                            Radek.QTY_OBJ_Pohoda = item.OBJPol_QTY_ZbyvaDodatPolozka;
                        }

                        

                        Radek.SKz_EAN = item.IsSKz_EANNull() ? null : item.SKz_EAN;
                        Radek.SKz_IDS = item.IsSKz_IDSNull() ? null : item.SKz_IDS;
                        Radek.SKL_ID = item.IsSKL_IDNull() ? null : item.SKL_ID;

                        #endregion

                        PrevRow = Radek;

                        localds.VydejKontrola.AddVydejKontrolaRow(Radek);
                    }

                    #endregion
                    localds.VydejKontrola.AcceptChanges();

                }

                return new InfoValidace("OK", localds.VydejKontrola);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
