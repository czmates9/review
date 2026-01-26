using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Inventura2
{
    public partial class NaplnPolozku : Forms.SejmiKodForm
    {
        private Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow _kancelar = null;
        private Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow _kancelarFinal = null;
        public Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow Kancelar { get { return _kancelarFinal; } }
        private Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow _lokace = null;
        private Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow _lokaceFinal = null;
        public Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow Lokace { get { return _lokaceFinal; } }
        private Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow _osoba = null;
        private Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow _osobaFinal = null;
        public Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow Osoba { get { return _osobaFinal; } }
        private Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow _stredisko = null;
        private Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow _strediskoFinal = null;
        public Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow Stredisko { get { return _strediskoFinal; } }

        private Color BackColorDataOrig = Color.White;

        private Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = null;
        public Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow MajetekRow
        {
            get { return mrow; }
            set
            {
                mrow = value;
                
                if (mrow.IsKANCELARNull())
                    _kancelar = null;
                else
                {
					Fask.SQLiteDBs.DataSets.Inventura2.KANCLDataTable dt_kancl = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByKANCL_Kancl(mrow.KANCELAR);
                    _kancelar = (dt_kancl.Count == 0) ? null : dt_kancl[0];
                }

                if (mrow.IsKLIC_LOKNull())
                    _lokace = null;
                else
                {
					Fask.SQLiteDBs.DataSets.Inventura2.LOKACEDataTable dt_lokace = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByKLIC_LOK_Lokace(mrow.KLIC_LOK);
                    _lokace = (dt_lokace.Count == 0) ? null : dt_lokace[0];
                }

                if (mrow.IsOSOBANull())
                    _osoba = null;
                else
                {
					Fask.SQLiteDBs.DataSets.Inventura2.OSOBYDataTable dt_osoby = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByOSOBA_ZODP_Osoby(mrow.OSOBA);
                    _osoba = (dt_osoby.Count == 0) ? null : dt_osoby[0];
                }

                if (mrow.IsSTREDNull())
                    _stredisko = null;
                else
                {
					Fask.SQLiteDBs.DataSets.Inventura2.UCSTRDataTable dt_strediska = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByStredisko_Stredisko(mrow.STRED);
                    _stredisko = (dt_strediska.Count == 0) ? null : dt_strediska[0];
                }

                UpdateForm();
            }
        }

        public NaplnPolozku()
            : base()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            MyInitializeCompoment();
            Cursor.Current = Cursors.Default;
        }

        private void MyInitializeCompoment()
        {
            panelKod.Dock = DockStyle.Bottom;
            this.BackColorDataOrig = labelEAN.DataBackColor;
        }

        private void MyDisposeComponent()
        {
        }

        public NaplnPolozku(
            string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni,
            Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow prow)
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            this.MajetekRow = prow;

            MyInitializeCompoment();
            Cursor.Current = Cursors.Default;
        }

        public NaplnPolozku(Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow prow)
            : this()
        {
            this.MajetekRow = prow;
            MyInitializeCompoment();
        }


        private void UpdateForm()
        {
            try
            {
                if (this.MajetekRow != null)
                {
                    labelNazev.Data = mrow.IsNAZEVNull() ? "-" : mrow.NAZEV.Trim();
                    labelEAN.Data = mrow.IsEANNull() ? "-" : mrow.EAN.Trim();
                    labelKategorie.Data = mrow.IsKATEGORIENull() ? "-" : mrow.KATEGORIE.Trim();

                    Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow tmpKancelar = null;
                    if (_kancelarFinal != null)
                        tmpKancelar = _kancelarFinal;
                    else if (_kancelar != null)
                        tmpKancelar = _kancelar;
                    else
						tmpKancelar = Inventura2.Inventura2_Instance.globalObject.active_kancelar;

                    if (tmpKancelar == null)
                        labelKancelar.Data = "-";
                    else
                    {
                        string ktext = tmpKancelar.IsTEXTNull() ? "-" : tmpKancelar.TEXT.Trim();
                        labelKancelar.Data = ktext;
                    }

                    if (tmpKancelar == null)
                        dfKancelarNazev.Data = "-";
                    else
                    {
                        string ktext = tmpKancelar.IsNAZEVNull() ? "-" : tmpKancelar.NAZEV.Trim();
                        dfKancelarNazev.Data = ktext;
                    }

                    Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow tmpLokace = null;
                    if (_lokaceFinal != null)
                        tmpLokace = _lokaceFinal;
                    else if (_lokace != null)
                        tmpLokace = _lokace;
                    else
						tmpLokace = Inventura2.Inventura2_Instance.globalObject.active_lokace;
                    
                    if (tmpLokace == null)
                        labelLokace.Data = "-";
                    else
                    {
                        string lokace1 = tmpLokace.IsLOKACE1Null() ? "-" : tmpLokace.LOKACE1.Trim();
                        string lokace2 = tmpLokace.IsLOKACE2Null() ? "-" : tmpLokace.LOKACE2.Trim();
                        string lokaceNazev = tmpLokace.NAZEV.Trim();
                        string lokaceEAN = tmpLokace.EANL.Trim();
                        labelLokace.Data = (lokace1 + "," + lokace2 + "," + lokaceNazev + "," + lokaceEAN);
                    }

                    Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow tmpOsoba = null;
                    if (_osobaFinal != null)
                        tmpOsoba = _osobaFinal;
                    else if (_osoba != null)
                        tmpOsoba = _osoba;
                    else
						tmpOsoba = Inventura2.Inventura2_Instance.globalObject.active_osoba;

                    if (tmpOsoba == null)
                        labelOsoba.Data = "-";
                    else
                    {
                        string otitul = tmpOsoba.IsTITULNull() ? "-" : tmpOsoba.TITUL.Trim();
                        string ojmeno = tmpOsoba.IsJMENONull() ? "-" : tmpOsoba.JMENO.Trim();
                        string oprijmeni = tmpOsoba.IsPRIJMENINull() ? "-" : tmpOsoba.PRIJMENI.Trim();
                        labelOsoba.Data = otitul + " " + ojmeno + " " + oprijmeni;
                    }

                    Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow tmpStredisko = null;
                    if (_strediskoFinal != null)
                        tmpStredisko = _strediskoFinal;
                    else if (_stredisko != null)
                        tmpStredisko = _stredisko;
                    else
						tmpStredisko = Inventura2.Inventura2_Instance.globalObject.active_stredisko;
                    if (tmpStredisko == null)
                        labelStredisko.Data = "-";
                    else
                    {
                        string snazev = tmpStredisko.IsNAZEVNull() ? "-" : tmpStredisko.NAZEV.Trim();
                        labelStredisko.Data = snazev;
                    }

                    //Kontroly na parametry
                    //Kancelar
                    if (_kancelarFinal != null)
                        labelKancelar.DataBackColor = Color.Green;
					else if (Inventura2.Inventura2_Instance.globalObject.active_kancelar == null)
                        labelKancelar.DataBackColor = this.BackColorDataOrig;
                    else
                    {
                        if (_kancelar == null)
                            labelKancelar.DataBackColor = Color.Red;
						else if (_kancelar.KANCL != Inventura2.Inventura2_Instance.globalObject.active_kancelar.KANCL)
                            labelKancelar.DataBackColor = Color.Red;
                        else
                            labelKancelar.DataBackColor = Color.Green;
                    }

                    //Lokace
                    if (_lokaceFinal != null)
                        labelLokace.DataBackColor = Color.Green;
					else if (Inventura2.Inventura2_Instance.globalObject.active_lokace == null)
                        labelLokace.DataBackColor = this.BackColorDataOrig;
                    else
                    {
                        if (_lokace == null)
                            labelLokace.DataBackColor = Color.Red;
						else if (_lokace.KLIC_LOK != Inventura2.Inventura2_Instance.globalObject.active_lokace.KLIC_LOK)
                            labelLokace.DataBackColor = Color.Red;
                        else
                            labelLokace.DataBackColor = Color.Green;
                    }

                    //Osoby
                    if (_osobaFinal != null)
                        labelOsoba.DataBackColor = Color.Green;
					else if (Inventura2.Inventura2_Instance.globalObject.active_osoba == null)
                        labelOsoba.DataBackColor = this.BackColorDataOrig;
                    else
                    {
                        if (_osoba == null)
                            labelOsoba.DataBackColor = Color.Red;
						else if (_osoba.OSOBA_ZODP != Inventura2.Inventura2_Instance.globalObject.active_osoba.OSOBA_ZODP)
                            labelOsoba.DataBackColor = Color.Red;
                        else
                            labelOsoba.DataBackColor = Color.Green;
                    }

                    //Strediska
                    if (_strediskoFinal != null)
                        labelStredisko.DataBackColor = Color.Green;
					else if (Inventura2.Inventura2_Instance.globalObject.active_stredisko == null)
                        labelStredisko.DataBackColor = this.BackColorDataOrig;
                    else
                    {
                        if (_stredisko == null)
                            labelStredisko.DataBackColor = Color.Red;
						else if (_stredisko.STREDISKO != Inventura2.Inventura2_Instance.globalObject.active_stredisko.STREDISKO)
                            labelStredisko.DataBackColor = Color.Red;
                        else
                            labelStredisko.DataBackColor = Color.Green;
                    }

                    //Mnozstvi
                    //decimal nacteno = (Globals.ta_inventur.NasnimanoKusu(mrow.I_CISLO) ?? 0);
                    decimal nacteno = mrow.NACTENO;

                    if (mrow.IsI_CISLONull())
                        labelNacteno.Data = "-";
                    else
                        //labelNacteno.Data = (Globals.ta_inventur.NasnimanoKusu(mrow.I_CISLO) ?? 0).ToString(Settings.UIFormatDesCisel);
                        labelNacteno.Data = nacteno.ToString(Settings.UIFormatDesCisel);

                    labelKusu.Data = (mrow.IsKUSUNull() ? 0 : mrow.KUSU).ToString(Settings.UIFormatDesCisel);

                }
                else
                {
                    labelStredisko.Data = "-";
                    labelNazev.Data = "-";
                    labelKancelar.Data = "-";
                    labelKusu.Data = "-";
                    labelNacteno.Data = "-";
                    labelOsoba.Data = "-";
                    labelLokace.Data = "-";
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void NaplnPolozku_Load(object sender, EventArgs e)
        {
            panelButtons.Visible = MST_Global.ShowPanelButtons;
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            UpdateForm();
        }

        protected override void PerformCancel()
        {
            this.MyDisposeComponent();
            base.PerformCancel();
        }

        protected override void PerformOK()
        {
            if (!this.PerformTest())
                return;

            this.MyDisposeComponent();
            base.PerformOK();
        }

        public bool PerformTest()
        {
            #region Test s rozhodnutimi
            try
            {
                string spolozkanazev = mrow.IsNAZEVNull() ? "" : mrow.NAZEV;
                if (spolozkanazev.Trim().Length == 0)
                    spolozkanazev = mrow.IsI_CISLONull() ? "" : "I_CISLO=" + mrow.I_CISLO.Trim();
                if (spolozkanazev.Trim().Length == 0)
                    spolozkanazev = "ID=" + mrow.ID.ToString();

                Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow tmpLokace = _lokaceFinal;
				if (tmpLokace == null && Inventura2.Inventura2_Instance.globalObject.active_lokace != null)
                {
					if (_lokace == null || _lokace.KLIC_LOK != Inventura2.Inventura2_Instance.globalObject.active_lokace.KLIC_LOK)
                    {
                        string slokacpuvodni = (_lokace == null ? "?" : _lokace.NAZEV.Trim());
						string slokacenova = Inventura2.Inventura2_Instance.globalObject.active_lokace.NAZEV.Trim();
                        DialogResult dr = MessageBoxBig.Show("Položka '" + spolozkanazev + "' je vedena na jiné lokaci '" + slokacpuvodni + "'\n\nChcete ji pøevést na aktuální lokaci '" + slokacenova + "'?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                        if (dr == DialogResult.Cancel)
                            return false;
                        else if (dr == DialogResult.No)
                            tmpLokace = _lokace;
                        else
							tmpLokace = Inventura2.Inventura2_Instance.globalObject.active_lokace;
                    }
                }

                Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow tmpKancl = _kancelarFinal;
				if (tmpKancl == null && Inventura2.Inventura2_Instance.globalObject.active_kancelar != null)
                {
					if (_kancelar == null || _kancelar.KANCL != Inventura2.Inventura2_Instance.globalObject.active_kancelar.KANCL)
                    {
                        string skanclpuvodni = _kancelar == null ? "?" : (_kancelar.IsTEXTNull() ? "" : _kancelar.TEXT.Trim());
                        if (skanclpuvodni.Trim().Length == 0)
                            skanclpuvodni = "KANCL=" + _kancelar.KANCL.Trim();
						string skanclnovy = Inventura2.Inventura2_Instance.globalObject.active_kancelar.IsTEXTNull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_kancelar.TEXT.Trim();
                        if (skanclnovy.Trim().Length == 0)
							skanclnovy = "KANCL=" + Inventura2.Inventura2_Instance.globalObject.active_kancelar.KANCL.Trim();
                        DialogResult dr = MessageBoxBig.Show("Položka '" + spolozkanazev + "' je vedena v jiné kanceláøi '" + skanclpuvodni + "'\n\nChcete ji pøevést na aktuální kanceláø '" + skanclnovy + "'?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                        if (dr == DialogResult.Cancel)
                            return false;
                        else if (dr == DialogResult.No)
                            tmpKancl = _kancelar;
                        else
							tmpKancl = Inventura2.Inventura2_Instance.globalObject.active_kancelar;
                    }
                }
                Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow tmpStredisko = _strediskoFinal;
				if (tmpStredisko == null && Inventura2.Inventura2_Instance.globalObject.active_stredisko != null)
                {
					if (_stredisko == null || _stredisko.STREDISKO != Inventura2.Inventura2_Instance.globalObject.active_stredisko.STREDISKO)
                    {
                        string sstrediskopuvodni = _stredisko == null ? "?" : (_stredisko.IsNAZEVNull() ? "" : _stredisko.NAZEV.Trim());
                        if (sstrediskopuvodni.Trim().Length == 0)
                            sstrediskopuvodni = "STREDISKO=" + _stredisko.STREDISKO.Trim();
						string sstrediskonovy = Inventura2.Inventura2_Instance.globalObject.active_stredisko.IsNAZEVNull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_stredisko.NAZEV.Trim();
                        if (sstrediskonovy.Trim().Length == 0)
							sstrediskonovy = "STREDISKO=" + Inventura2.Inventura2_Instance.globalObject.active_stredisko.STREDISKO.Trim();
                        DialogResult dr = MessageBoxBig.Show("Položka '" + spolozkanazev + "' je vedena na jiném støedisku '" + sstrediskopuvodni + "'\n\nChcete ji pøevést na aktuální støedisko '" + sstrediskonovy + "'?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                        if (dr == DialogResult.Cancel)
                            return false;
                        else if (dr == DialogResult.No)
                            //tmpLokace = _lokace;
                            tmpStredisko = _stredisko;
                        else
                            //tmpLokace = Globals.active_lokace;
							tmpStredisko = Inventura2.Inventura2_Instance.globalObject.active_stredisko;
                    }
                }
                Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow tmpOsoba = _osobaFinal;
				if (tmpOsoba == null && Inventura2.Inventura2_Instance.globalObject.active_osoba != null)
                {
					if (_osoba == null || _osoba.OSOBA_ZODP != Inventura2.Inventura2_Instance.globalObject.active_osoba.OSOBA_ZODP)
                    {
                        string sosobapuvodni = _osoba == null ? "?" : ((_osoba.IsTITULNull() ? "" : _osoba.TITUL.Trim() + " ") + (_osoba.IsJMENONull() ? "" : _osoba.JMENO.Trim() + " ") + (_osoba.IsPRIJMENINull() ? "" : _osoba.PRIJMENI.Trim()));
                        if (sosobapuvodni.Trim().Length == 0)
                            sosobapuvodni = _osoba.OSOBA_ZODP.ToString();
                        //14.4.2011 JiS : oprava chyby, pouzil se spatny objekt _osoba, misto Globals.active_osoba...
                        string sosobanova =
							(Inventura2.Inventura2_Instance.globalObject.active_osoba.IsTITULNull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_osoba.TITUL.Trim() + " ") +
							(Inventura2.Inventura2_Instance.globalObject.active_osoba.IsJMENONull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_osoba.JMENO.Trim() + " ") +
							(Inventura2.Inventura2_Instance.globalObject.active_osoba.IsPRIJMENINull() ? "" : Inventura2.Inventura2_Instance.globalObject.active_osoba.PRIJMENI.Trim());
                        if (sosobanova.Trim().Length == 0)
							sosobanova = Inventura2.Inventura2_Instance.globalObject.active_osoba.OSOBA_ZODP.ToString();
                        DialogResult dr = MessageBoxBig.Show("Položka '" + spolozkanazev + "' je vedena na jinou osobu '" + sosobapuvodni.Trim() + "'\n\nChcete ji pøevést na aktuální lokaci '" + sosobanova.Trim() + "'?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                        if (dr == DialogResult.Cancel)
                            return false;
                        else if (dr == DialogResult.No)
                            tmpOsoba = _osoba;
                        else
							tmpOsoba = Inventura2.Inventura2_Instance.globalObject.active_osoba;
                    }
                }

                _kancelarFinal = tmpKancl;
                _strediskoFinal = tmpStredisko;
                _lokaceFinal = tmpLokace;
                _osobaFinal = tmpOsoba;

                if (_kancelarFinal == null)
                    _kancelarFinal = _kancelar;
                if (_lokaceFinal == null)
                    _lokaceFinal = _lokace;
                if (_osobaFinal == null)
                    _osobaFinal = _osoba;
                if (_strediskoFinal == null)
                    _strediskoFinal = _stredisko;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return false;
            }
            finally
            {
                UpdateForm();
            }
            #endregion

            return true;
        }

        #region spatne....
        //bool lokaceOk;
        //bool kanclOK;
        //bool strediskoOK;
        //bool osobaOK;
        ////bool 
        ///// <summary>
        ///// Pokud je neco spatne vrati false
        ///// </summary>
        ///// <returns>je neco spatne</returns>
        //public bool VseOK()
        //{
        //    return (lokaceOk && kanclOK && strediskoOK && osobaOK);
        //}

        ///// <summary>
        ///// Otestuje zda je row v poradku 
        ///// </summary>
        ///// <returns></returns>
        //public bool PerformTest()
        //{
        //     lokaceOk = true;
        //     kanclOK = true;
        //     strediskoOK = true;
        //     osobaOK = true;
        //    try
        //    {
        //        Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow tmpLokace = _lokaceFinal;
        //        if (tmpLokace == null && Globals.active_lokace != null)
        //        {
        //            if (_lokace == null || _lokace.KLIC_LOK != Globals.active_lokace.KLIC_LOK)
        //            {
        //                lokaceOk = false;
        //            }
        //        }

        //        Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow tmpKancl = _kancelarFinal;
        //        if (tmpKancl == null && Globals.active_kancelar != null)
        //        {
        //            if (_kancelar == null || _kancelar.KANCL != Globals.active_kancelar.KANCL)
        //            {
        //                kanclOK = false;
        //            }
        //        }
        //        Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow tmpStredisko = _strediskoFinal;
        //        if (tmpStredisko == null && Globals.active_stredisko != null)
        //        {
        //            if (_stredisko == null || _stredisko.STREDISKO != Globals.active_stredisko.STREDISKO)
        //            {
        //             strediskoOK = false;
        //            }
        //        }
        //        Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow tmpOsoba = _osobaFinal;
        //        if (tmpOsoba == null && Globals.active_osoba != null)
        //        {
        //            if (_osoba == null || _osoba.OSOBA_ZODP != Globals.active_osoba.OSOBA_ZODP)
        //            {
        //                osobaOK = false;
        //            }
        //        }
        //        //todle si nastavim
        //        _kancelarFinal = _kancelar;
        //        _strediskoFinal = _stredisko;
        //        _lokaceFinal = _lokace;
        //        _osobaFinal = _osoba;

        //        return VseOK();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //        return false;
        //    }
        //}
        #endregion

        private void menuItem5_Click(object sender, EventArgs e)
        {
            zmenaLokace();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            zmenaKancelare();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            zmenaOsoby();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            zmenaStrediska();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void zmenaLokace()
        {
            try
            {
                ScannerStop();

                using (ZmenaLokace flokace = new ZmenaLokace())
                {
                    flokace.Owner = this;
                    flokace.Lokace = _lokace;
                    if (flokace.ShowDialog() == DialogResult.Cancel)
                        return;
                    //_lokace = flokace.Lokace;
                    _lokaceFinal = flokace.Lokace;
                }
                UpdateForm();

            }
            finally
            {
                ScannerStart();
            }
        }

        private void zmenaKancelare()
        {
            try
            {
                ScannerStop();

                using (ZmenaKancl fkancl = new ZmenaKancl())
                {
                    fkancl.Owner = this;
                    fkancl.Kancelar = _kancelar;
                    if (fkancl.ShowDialog() == DialogResult.Cancel)
                        return;
                    //_kancelar = fkancl.Kancelar;
                    _kancelarFinal = fkancl.Kancelar;
                }
                UpdateForm();

            }
            finally
            {
                ScannerStart();
            }
        }

        private void zmenaOsoby()
        {
            try
            {
                ScannerStart();

                using (ZmenaOsoba fosoba = new ZmenaOsoba())
                {
                    fosoba.Owner = this;
                    fosoba.Osoba = _osoba;
                    if (fosoba.ShowDialog() == DialogResult.Cancel)
                        return;
                    //_osoba = fosoba.Osoba;
                    _osobaFinal = fosoba.Osoba;
                }
                UpdateForm();

            }
            finally
            {
                ScannerStop();
            }
        }

        private void zmenaStrediska()
        {
            try
            {
                ScannerStart();

                using (ZmenaStredisko fstred = new ZmenaStredisko())
                {
                    fstred.Owner = this;
                    fstred.Stredisko = _stredisko;
                    if (fstred.ShowDialog() == DialogResult.Cancel)
                        return;
                    //_stredisko = fstred.Stredisko;
                    _strediskoFinal = fstred.Stredisko;
                }
                UpdateForm();

            }
            finally
            {
                ScannerStop();
            }
        }

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            this._kancelarFinal = null;
            this._lokaceFinal = null;
            this._osobaFinal = null;
            this._strediskoFinal = null;
        }

    }
}