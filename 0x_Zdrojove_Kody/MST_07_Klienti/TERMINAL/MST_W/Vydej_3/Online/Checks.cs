using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.Forms;
using System.Windows.Forms;

namespace Fask.MST_W.Vydej_3.Online
{
    public class Checks
    {
        /// <summary>
        /// Kontrola FEFO/FIFO pro zadanout polozku -> musi byt dostupna online kontorola
        /// </summary>
        /// <param name="itemnmbr">Cislo polozky</param>
        /// <param name="sklad_id">ID skladu</param>
        /// <param name="sn">Sarze / SN pro kontrolu</param>
        /// <param name="expirace">Zadana expirace pro porovnani, zda je pro sn shodna</param>
        /// <param name="CZ_SerNum_Track">Priznak sledovani polozky na SN/Sarzi</param>
        /// <param name="CZ_Expirace_Track">Priznak sledovani polozky na Expiraci</param>
        /// <param name="sita">Nasnimana data polozky</param>
        /// <returns>True => ok pokracovat, False: nelze pokracovat kontrola je neuspesna</returns>
        public static bool OnlineFIFOFEFOCheck(string itemnmbr, string sklad_id, string sn, DateTime? expirace, byte CZ_SerNum_Track, byte CZ_Expirace_Track, SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable sita)
        {
            #region Online Overeni FIFO/FEFO
            // overeni zadane/ziskane expirace/sarze online/lokalne
            // 1) natazeni materialu online dle itemnmbr, sklad
            // 2) natazeni materialu lokalne jiz zadanych v di
            // 3) vylouceni sarzi/expiraci z online, ktere jiz byly porizeny lokalne
            //    - sarze, ktera je na vystupu, tak zmizi dale z porovnavani
            // 4) zjisteni nejstarsi polozky
            //      a) je na expiraci? => najit nejmladsi expirace (jedna nebo vice)
            //      b) serazeni zbylych sarzi od nejstarsi a vyber prvniho(nejstarsiho) zaznamu
            //          - datum prijmu nejstarsi
            //          - cisla sarze od nejmensi
            //      c) pokud se datum expirace zadane a navrzene lisi, ta zobrazit hlaseni => ... ANO/NE
            //      d) pokud jsou stejene data, tak pokracovat -> ok

			//var online_ds = OnlineGetMaterial(itemnmbr, sklad_id, null);
            var go = Vydej.vydejInstance.globalObject;
            var online_ds = go.service_vydej.Online_GetMaterial(itemnmbr, sklad_id, null);    // vytahuji vsechny dostupne sarze pro polozku
            if (online_ds == null)
            {
                MessageBoxBig.Show("Nepodařilo se načíst seznam materiálů online", "FEFO/FIFO", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return false;
            }
            var materialy_online = online_ds.Items;
            //var materialy_lokal = sita.GetDataByItemnmbrSklid(itemnmbr, sklad_id).GroupBy(x => new { x.SERLTNUM });
            var materialy_lokal = sita.Where(x => x.ITEMNMBR == itemnmbr).GroupBy(x => new { x.SERLTNUM });

            VydejService.Vydej_Items_Online.ItemsRow sarze_used = null;
            // test, zda pouzita sarze je plne vykryta
            bool sarze_used_quantity_full = true;
            List<VydejService.Vydej_Items_Online.ItemsRow> sarze_not_used = new List<Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow>();
            VydejService.Vydej_Items_Online.ItemsRow sarze_to_use = null;

            // vylouceni jiz pouzitych sarzi, mnozstvi?
            foreach (VydejService.Vydej_Items_Online.ItemsRow mo in materialy_online)
            {
                //if (!materialy_lokal.Any(x => x.SERLTNUM.Trim() == mo.SERLTNUM.Trim() && x.QTYSHPPD < mo.QTYSHPPD)) -> toto nejde, mnozstvi musi pak byt pres sumu
                if (!materialy_lokal.Any(x =>
                    x.Key.SERLTNUM.Trim() == mo.Serltnum.Trim()
                    && x.Sum(y => y.QTYSHPPD) >= mo.Qty
                    ))
                    sarze_not_used.Add(mo);

                if (mo.Serltnum.Trim() == sn.Trim())
                {
                    sarze_used = mo;
                    var material = materialy_lokal.Where(x => x.Key.SERLTNUM.Trim() == sn.Trim());
                    sarze_used_quantity_full = true;
                    if (material.Count() > 0)
                        sarze_used_quantity_full = (material.First().Sum(x => x.QTYSHPPD) >= mo.Qty);
                }
            }

            // zjisteni co je nestarsi
            // je-li na expirace, tak vytahnu nejstarsi expirace z nepouzitych
            if (CZ_Expirace_Track > 0)
            {

				if (sarze_used == null)
				{
					string msg = string.Empty;

					if (CZ_SerNum_Track == 1)
						msg = "SN: {0} nenalezeno online!";

					if (CZ_SerNum_Track == 2)
						msg = "Šarže: {0} nenalezena online!";

					MessageBoxBig.Show(
					   String.Format(msg, sn.Trim())
					   , "Pozor!"
					   , MessageBoxButtons.OK
					   , MessageBoxBigIcon.Critical
					   , MessageBoxDefaultButton.Button1
					   );

					return false;
				}


                // 21.4.2021 JiS
                // tady zastavit, pokud pouzita sarze nema stejnou expiraci jako expirace zadana, pokud je sledovano na expiraci a expirace prisla jako paraemtr
                if ((expirace.HasValue) && (!sarze_used.IsExpirationNull()) && (expirace.Value.Date != sarze_used.Expiration.Date))
                {
					string msg = "Expirace pro vybranou šarži se neshodují!";
					string caption = "Kontrola šarže a expirace";

					if (CZ_SerNum_Track == 1)
					{
						msg = "Expirace pro vybrané SN se neshodují!";
						caption = "Kontrola SN a expirace";
					}


                    MessageBoxBig.Show(
                        String.Format(msg, sn.Trim(), expirace.Value.ToString(Main.dateFormatRRMMDD))
                        ,caption
                        , MessageBoxButtons.OK
                        , MessageBoxBigIcon.Critical
                        , MessageBoxDefaultButton.Button1
                        );
                    return false;
                }

                if (sarze_not_used.Count > 0)
                {
                    DateTime minExpiration = sarze_not_used.Min(x => x.IsExpirationNull() ? DateTime.MaxValue : x.Expiration);
                    sarze_not_used = sarze_not_used.Where(x => x.Expiration == minExpiration).ToList();
                }
            }
            // je-li na sarze, tak vytahnu nejstarsi dle data prijmu
            if (CZ_SerNum_Track > 0)
            {
                if (sarze_not_used.Count > 1)
                {
                    DateTime minPrijem = sarze_not_used.Min(x => x.IsPrijemNull() ? DateTime.MaxValue : x.Prijem);
                    sarze_not_used = sarze_not_used.Where(x => x.Prijem == minPrijem).ToList();
                }
            }

            // vytahnu sarzi s nejmensim cislem, ktere lze pouzit
            if (sarze_not_used.Count > 0)
            {
                string minSarze = sarze_not_used.Min(x => x.Serltnum.Trim());
                sarze_not_used = sarze_not_used.Where(x => x.Serltnum.Trim() == minSarze.Trim()).ToList();
            }

            // pokud jsem az tady, tak uz nevim dle ceho omezit
            // seradim podle indexu
            // a vratim 1.zaznam, pokud existuje
            if (sarze_not_used.Count > 0)
            {
                sarze_not_used = sarze_not_used.OrderBy(x => x.Index).ToList();
                if (sarze_not_used.Count() > 0)
                    sarze_to_use = sarze_not_used.First();
            }

            if (sarze_not_used.Count <= 0)
            {
				string msg2 = "Nejsou dostupné žádné šarže";
				string caption2 = "Výběr šarže";

				if (CZ_SerNum_Track == 1)
				{
					msg2 = "Nejsou dostupné žádné SN";
					caption2 = "Výběr SN";
				}

                MessageBoxBig.Show(msg2, caption2, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1);
                return false;
            }

            if (
                (sarze_to_use != null)
                && (sarze_used != null)
                && (sarze_to_use != sarze_used)
                && (sarze_used_quantity_full)
                )
            {
				//// zobrazeni o jine variante
				//DialogResult drExpirace = MessageBoxBig.Show(
				//    //String.Format("Použito:\n") +
				//    String.Format("Š:{0} E:{1} P:{2} Q:{3} L:{4}\n"
				//        , sarze_used.Serltnum.Trim()
				//        , sarze_used.IsExpirationNull() ? "-" : sarze_used.Expiration.ToString(Main.dateFormatRRMMDD)
				//        , sarze_used.IsPrijemNull() ? "-" : sarze_used.Prijem.ToString(Main.datetimeFormatDMYYYYHmm)
				//        , sarze_used.Qty.ToString(Settings.UIFormatDesCisel)
				//        , sarze_used.Locncode.Trim() //sarze_used.IsLOCNCODENull() ? "-" : sarze_used.LOCNCODE.Trim()
				//        ) +
				//    String.Format(">>> Použij níže <<<\n") +
				//    String.Format("Š:{0} E:{1} P:{2} Q:{3} L:{4}\n"
				//        , sarze_to_use.Serltnum.Trim()
				//        , sarze_to_use.IsExpirationNull() ? "-" : sarze_to_use.Expiration.ToString(Main.dateFormatRRMMDD)
				//        , sarze_to_use.IsPrijemNull() ? "-" : sarze_to_use.Prijem.ToString(Main.datetimeFormatDMYYYYHmm)
				//        , sarze_to_use.Qty.ToString(Settings.UIFormatDesCisel)
				//        , sarze_to_use.Locncode //sarze_to_use.IsLOCNCODENull() ? "-" : sarze_to_use.LOCNCODE.Trim()
				//    ) +
				//    String.Format("Pokračovat ?")
				//    , "Není vybrána nejstarší šarže!"   //, this.Text
				//    , MessageBoxButtons.YesNo
				//    , MessageBoxBigIcon.Warning);

				// zobrazeni o jine variante

				string txt1 = "Vybrána šarže";
				string txt2 = "Šarže: ";
				string txt3 = "Doporučená šarže";
				string cap = "Není vybrána nejstarší šarže!";

				if (CZ_SerNum_Track == 1)
				{
					txt1 = "Vybráné SN";
					txt2 = "SN: ";
					txt3 = "Doporučené SN";
					cap = "Není vybráno nejstarší SN!";
				}


				string msg = txt1 + Environment.NewLine;
				msg += txt2 + sarze_used.Serltnum.Trim() + Environment.NewLine;
				msg += "Exspirace: " + (sarze_used.IsExpirationNull() ? "-" : sarze_used.Expiration.ToString(Main.dateFormatRRMMDD)) + Environment.NewLine;
				msg += "Množství: " + sarze_used.Qty.ToString(Settings.UIFormatDesCisel) + Environment.NewLine;
				msg += Environment.NewLine;
				msg += txt3 + Environment.NewLine;
				msg += txt2 + sarze_to_use.Serltnum.Trim() + Environment.NewLine;
				msg += "Exspirace: " + (sarze_to_use.IsExpirationNull() ? "-" : sarze_to_use.Expiration.ToString(Main.dateFormatRRMMDD)) + Environment.NewLine;
				msg += "Množství: " + sarze_to_use.Qty.ToString(Settings.UIFormatDesCisel) + Environment.NewLine;
				msg += "Přesto použít vybranou?";


				DialogResult drExpirace = MessageBoxBig.Show(
					msg
					, cap
					, MessageBoxButtons.YesNo
					, MessageBoxBigIcon.Warning);


                if (drExpirace == DialogResult.No)
                    return false;


                // 3.6.2020 JiS : N20015 potvrzeni vedoucim(adminem)
                #region Verifikace uzivatelem a heslem pro potrvrzeni expirace
                //while (true)
                //{
                //    try
                //    {
                //        DialogResult drPwdCommit = Fask.MST_W.Forms.InputBox.Show("Potvrďte volbu heslem", string.Empty, out pwdcommit, false, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric, '*');
                //        if (drPwdCommit == DialogResult.Cancel)
                //            return false;

                //        // online overeni hesla
                //        var verified = Prodej_3.ProdejMain.prodejInstance.prodejs.Online_Expirace_Confirm(MST_Global.TerminalID, MST_Global.UserID.ToString(), pwdcommit);
                //        if (!verified)
                //            throw new Exception("Neautorizované použití šarže");
                //    }
                //    catch (Exception exVerified)
                //    {
                //        MessageBoxBig.Show(exVerified.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                //        continue;
                //    }

                //    break;
                //}
                #endregion

                #region Vlozeni zaznamu do UserEvents o poruseni pravidla FEFO/FIFO
                //// TODO : UserEvents doladit typy, id ... 
                //string machineid = string.Empty; // TODO : doplnit ?
                ////Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "useraction", "expirace", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, usermasterid, machineid, "Prodej", this._cislodavky, string.Empty, zbozi.ITEMNMBR.Trim(), sn.Trim(), mo.SERLTNUM.Trim()));
                //pwdcommit = pwdcommit.Length > 10 ? pwdcommit.Substring(0, 10) : pwdcommit; // zmenseni delky
                //Program.mstw.eventsUser.add(new Fask.Events.Event(
                //    Guid.NewGuid(),
                //    "useraction",
                //    "fifofefo",
                //    DateTime.Now,
                //    MST_Global.TerminalID,
                //    MST_Global.UserID,
                //    pwdcommit,
                //    machineid,
                //    "D",
                //    this._cislodavky,
                //    _typdokladu == null ? string.Empty : _typdokladu.doc_id.Trim(),
                //    itemnmbr,
                //    sn.Trim(),
                //    sarze_to_use.SERLTNUM.Trim()
                //    ));
                #endregion
            }

            return true;
            #endregion
        }

        /// <summary>
        /// Online kontrola vhodnosti pouziti expirace
        /// </summary>
        /// <param name="itemnmbr">Cislo polozky</param>
        /// <param name="sklad_id">id skladu polozky</param>
        /// <param name="sn">sarze, ktera se chce pouzit</param>
        /// <param name="expirace">expirace, ktera se chce pouzit</param>
        /// <returns>True - muzes, False - nemuzes</returns>
        public static bool OnlineOverExpiraci(string itemnmbr, string sklad_id, string sn, DateTime expirace)
        {
            #region Overeni expirace online ...
            // overeni zadane expirace
            var go = Vydej.vydejInstance.globalObject;
            while (true)
            {
                var StatusExpiration = go.service_vydej.Online_Expirace_Verify(itemnmbr, sklad_id, sn, expirace);
                if (StatusExpiration == null)
                {
                    var drChybaJakDal = MessageBoxBig.Show("Pokračovat bez ověření?", "Ověření Expirace", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                    if (drChybaJakDal == DialogResult.No)
                        continue;   //znovu pokus o overeni
                    else //if (drChybaJakDal == DialogResult.Yes)
                        return true;
                }
                else
                {
                    switch (StatusExpiration.State)
                    {
                        case Fask.MST_W.VydejService.StatusOverExpiraceState.ERROR:
                            // chyba, nelze pokracovat
                            MessageBoxBig.Show(StatusExpiration.Message, "Ověření Expirace", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                            return false;
                        //break;
                        case Fask.MST_W.VydejService.StatusOverExpiraceState.WARNING:
                            var drExpiraceWarning = MessageBoxBig.Show(StatusExpiration.Message, "Ověření Expirace", MessageBoxButtons.AbortRetryIgnore, MessageBoxBigIcon.Warning);
                            if (drExpiraceWarning == DialogResult.Abort)
                                return false;
                            else if (drExpiraceWarning == DialogResult.Retry)
                                continue;
                            else //if (drExpiraceWarning == DialogResult.Ignore)
                            {
                                // ok, ale vyzaduji potvrzeni heslem

                                // 3.6.2020 JiS : N20015 potvrzeni vedoucim(adminem)
                                #region Verifikace uzivatelem a heslem pro potrvrzeni expirace
                                string pwdcommit = string.Empty;
                                while (true)
                                {
                                    try
                                    {
                                        DialogResult drPwdCommit = InputBox.Show("Potvrďte volbu heslem", string.Empty, out pwdcommit, false, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric, '*');
                                        if (drPwdCommit == DialogResult.Cancel)
                                            return false;

                                        // online overeni hesla
                                        var verified = go.service_vydej.Online_Expirace_Confirm(MST_Global.TerminalID, MST_Global.UserID.ToString(), pwdcommit);
                                        if (!verified)
                                            throw new Exception("Neautorizované použití šarže");
                                    }
                                    catch (Exception exVerified)
                                    {
                                        MessageBoxBig.Show(exVerified.Message, "Ověření Expirace", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                                        continue;
                                    }

                                    break;
                                }
                                #endregion

                                #region Vlozeni zaznamu do UserEvents o poruseni pravidla FEFO/FIFO
                                // TODO : UserEvents doladit typy, id ... 
                                string machineid = string.Empty; // TODO : doplnit ?
                                //Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "useraction", "expirace", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, usermasterid, machineid, "Prodej", this._cislodavky, string.Empty, zbozi.ITEMNMBR.Trim(), sn.Trim(), mo.SERLTNUM.Trim()));
                                pwdcommit = pwdcommit.Length > 10 ? pwdcommit.Substring(0, 10) : pwdcommit; // zmenseni delky
                                Program.mstw.eventsUser.add(new Fask.Events.Event(
                                    Guid.NewGuid(),
                                    "useraction",
                                    "fifofefo",
                                    DateTime.Now,
                                    MST_Global.TerminalID,
                                    MST_Global.UserID,
                                    pwdcommit,
                                    machineid,
                                    "V",
                                    0, // TODO : poslat tam cislo davky, ale co sloucene ??? //go.Davka, 
                                    string.Empty, // TODO : dotahnout cislo dokladu polozky? 
                                    itemnmbr,
                                    sn.Trim(),
                                    expirace.ToString(Main.dateFormatRRMMDD)
                                    ));
                                #endregion
                                return true;
                            }
                        case Fask.MST_W.VydejService.StatusOverExpiraceState.OK:
                        default:
                            // vse v poradku, pouzit a ukonci
                            return true;
                    }
                }
            }
            #endregion
        }
    }
}
