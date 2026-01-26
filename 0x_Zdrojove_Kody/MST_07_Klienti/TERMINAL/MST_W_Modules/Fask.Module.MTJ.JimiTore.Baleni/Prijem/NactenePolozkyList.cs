using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Module.MTJ.JimiTore.Baleni.Forms;

namespace Fask.Module.MTJ.JimiTore.Baleni.Prijem
{
    public partial class NactenePolozkyList : Form
    {
        private string prijemka = string.Empty;

        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni wsBaleni = null;
        private Color? backgroundColor = null;

        public Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej.ZboziRow zboziRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsVydej].Current)).Row as Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej.ZboziRow;                    
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    return null;
                }
            }
        }

        public NactenePolozkyList(string prijemka, Color backgroundColor)
        {
            try
            {
                InitializeComponent();

                this.prijemka = prijemka;

                sbinfo.Text = "PRJ: " + prijemka;

                Globals.Configuration = new Fask.Module.MTJ.JimiTore.Baleni.DataSets.Configuration();
                
                if (System.IO.File.Exists(Globals.ConfigurationFile))
                    Globals.Configuration.ReadXml(Globals.ConfigurationFile);

                if (Globals.Configuration.PovolitPodbarveniTlacitek)
                {
                    zpet_but.BackColor = backgroundColor;
                    ok_but.BackColor = backgroundColor;
                    this.backgroundColor = backgroundColor;
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "NactenePolozkyList");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            } 

            try
            {
                wsBaleni = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.Baleni();
                wsBaleni.Url = Globals.ServerAddress + "Baleni.asmx";
                wsBaleni.Timeout = Globals.ServerTimeout;

                //AktualizovatPolozky(objednavka);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "NactenePolozkyList load");
            }
        }

        private void ServisDynamickaTabulka_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                //Fask.Localization.LocalizationExtensionForm.Localize(this);
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                //this.Size = Forms.FormLocation.ScreenResolution;

                CreateGridStyles();

                InitializeGrid();

                ScannerStart();
                panelButtons_Resize(null, null);

                bsVydej.DataSource = dsVydej.Zbozi;
                dataGrid1.DataSource = bsVydej;

                dataGrid1.Focus();
                try
                {
                    dataGrid1.CurrentRowIndex = 0;
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dsVydej.Zbozi.TableName; // ds_servis.CZMST_Servis_Dynamic_Table.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Položka č.";
            dg.MappingName = dsVydej.Zbozi.matidColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Množství";
            dg.MappingName = dsVydej.Zbozi.mnozstviColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Název";
            dg.MappingName = dsVydej.Zbozi.nazevColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Délka";
            dg.MappingName = dsVydej.Zbozi.delkaColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Č.k.";
            dg.MappingName = dsVydej.Zbozi.CZ_CarKodColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);            

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "MJ";
            dg.MappingName = dsVydej.Zbozi.mjColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Globals.UIFormatDesCisel);
            // 8, 10
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Globals.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Globals.ConfigDir, this.GetType().ToString()));
        }

        private void ServisDynamickaTabulka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Back)
            {
                PerformDelete();
            }
            else if (e.KeyCode == Keys.F1)
            {
                miPridatPolozku_Click(null, null);
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            try
            {
                ScannerStop();
                DialogResult dr = MessageBoxBig.Show("Opravdu chcete přerušit snímání položek?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                if (dr != DialogResult.Yes)
                    return;

                if (dsVydej.Zbozi.Count > 0)
                {
                    dr = MessageBoxBig.Show("Existují neodeslaná data.\nChcete tyto data odeslat?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka so = ProcessPrijem();
                        Cursor.Current = Cursors.Default;
                        if (so.Result == Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUS.ERROR)
                        {
                            MessageBoxBig.Show("Data se nepodařilo uložit na server:\n'" + so.Message + "'", "Chyba", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            return;
                        }
                    }
                }

                finalize();
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PerformCancel");
                MessageBoxBig.Show(ex.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void finalize()
        {
            ScannerFinalize();

            this.dataGrid1.Save(Path.Combine(Globals.ConfigDir, this.GetType().ToString()));
        }

        #region scanner
        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {
            if (!scannserstart)
                return;

            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch (Exception)
            {
                //Logging.Log.Write(ex);
                //MessageBox.Show("Chyba  Scanneru : " + ex.Message, "Chyba");
                return;
            }
            EnableScanner();
        }

        public void EnableScanner()
        {
            if (Globals.Scanner != null)
                Globals.Scanner.Enable();
        }
        public void DisableScanner()
        {
            if (Globals.Scanner != null)
                Globals.Scanner.Disable();
        }

        private void ScannerStop()
        {
            try
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch
            {
            }
            try
            {
                DisableScanner();
            }
            catch
            {
            }
        }


        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData.Trim();
            findCode(barcode);
        }

        private void findCode(string barcode)
        {
            try
            {
                ScannerStop();

                PerformPridatPolozku(barcode);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }
        #endregion scanner

        

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                ScannerStop();

                if (dsVydej.Zbozi.Count == 0)
                {
                    MessageBoxBig.Show("Nejsou nasnímány záznamy", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                else
                {
                    // dotaz, zdali opravdu odeslat
                    DialogResult dr = MessageBoxBig.Show("Opravdu chcete ukončit snímání a odeslat data?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }
                Cursor.Current = Cursors.WaitCursor;
                Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka so = ProcessPrijem();
                Cursor.Current = Cursors.Default;
                if (so.Result == Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUS.ERROR)
                {
                    MessageBoxBig.Show("Data se nepodařilo uložit na server:\n'" + so.Message + "'", "Chyba", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }


                finalize();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                ScannerStart();
            }
        }

        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka ProcessPrijem()
        {
            Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka so = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusObjednavka();
            try
            {
                Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore data = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore();

                foreach (Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej.ZboziRow item in dsVydej.Zbozi)
                {
                    Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.X425_PRIJEMRow row = data.X425_PRIJEM.NewX425_PRIJEMRow();
                    row.PRIJEM_ID = prijemka;
                    row.BARCODE_ID = item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod;
                    row.MAT_ID = item.matid;
                    row.NAZEV_MAT = item.IsnazevNull() ? string.Empty : item.nazev;
                    row.MNOZSTVI = item.mnozstvi;
                    row.DELKA = item.IsdelkaNull() ? 0 : item.delka;
                    row.DAT_ZMENA = DateTime.Now;
                    row.FLAG_ZPRAC = 0;
                    row.TT_NAME = Globals.TermID.ToString();
                    row.USER_NAME = Globals.UserLogin;
                    data.X425_PRIJEM.AddX425_PRIJEMRow(row);
                }
                
                return wsBaleni.ProcessPrijem(Globals.UserLogin, Globals.TermID, prijemka, data);                
            }
            catch (Exception ex)
            {
                so.Message = ex.Message;
                so.Result = Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUS.ERROR;

                Fask.Logging.Log.Write(ex);
                return so;
            }
        }

        private void miDynamickaTabulka_Click(object sender, EventArgs e)
        {
        }

        private void dataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void zpet_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private void PerformDelete()
        {
            if (zboziRow == null)
                return;

            try
            {
                DialogResult dr = MessageBoxBig.Show("Opravdu chcete odstranit záznam '" + (zboziRow.IsnazevNull() ? string.Empty : zboziRow.nazev) + "'?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                if (dr != DialogResult.Yes)
                    return;

                dsVydej.Zbozi.RemoveZboziRow(zboziRow);
                dataGrid1.CurrentRowIndex = dataGrid1.CurrentRowIndex;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "PerformDelete");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void miPridatPolozku_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string kod = string.Empty;
                using (Forms.SejmiKodForm naplnp = new Fask.Module.MTJ.JimiTore.Baleni.Forms.SejmiKodForm("Zadejte čárový kód", Fask.Module.MTJ.JimiTore.Baleni.Forms.SejmiKodForm.TypeOfCode.AlphaNumeric, this.backgroundColor))
                {
                    naplnp.Text = "Zadejte čárový kód";
                    DialogResult dr = naplnp.ShowDialog();
                    if (dr != DialogResult.OK)
                        return;

                    kod = naplnp.Kod;
                }

                PerformPridatPolozku(kod);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "miPridatPolozku");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void PerformPridatPolozku(string barcode)
        {
            // 1) nacteni cisla prijemky (z predchoziho formulare)
            // 2) nacteni cisla zbozi (barcode)
            // 3) online f. pro nacteni informaci o zbozi (pripadne zobrazeni chyby)
            // 4) zadani mnozstvi pro prijem
            // 5) zadani mnozstvi pro tisk (resp. tisk ...) 
            // 7) tisk stitku (pokazde predvyplnit hodnotou: 1) -> zobrazit dialog pro zadani
            // 8) finalni potvrzeni vsech polozek a odeslani dat na server

            try
            {
                Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusPolozka sp = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusPolozka();

                // pokaždé se vrací pouze jedna položka ...
                Cursor.Current = Cursors.WaitCursor;
                Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore polozky = OnlineGetPolozky(prijemka, barcode, out sp);
                Cursor.Current = Cursors.Default;

                // nastala chyba, ukoncit zadavani ...
                if (sp.Result == Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUSPolozkaRes.ERROR)
                {
                    MessageBoxBig.Show(sp.Message, "Chyba", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                else if (sp.Result == Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUSPolozkaRes.WARNING)
                {
                    // chyba s možností pokračovat dál
                    DialogResult dr = MessageBoxBig.Show(sp.Message, "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
                    if (dr != DialogResult.Yes)
                        return;
                }

                if(polozky  == null || polozky.Zbozi.Count == 0)
                {
                    MessageBoxBig.Show("Nepodařilo se najít položku online", "Chyba", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.ZboziRow zrow = polozky.Zbozi[0];

                int mnozstvi = zrow.IsmnozstviNull() ? 0 : Convert.ToInt32(zrow.mnozstvi);
                // zadani mnozstvi
                // množství pro příjem je naplněno množstvím dle funkce mtj_fask_Prijem_CtrlMat ale uživatel může zadat jiné množství
                using (Forms.VydejPridatPolozku naplnp = new Forms.VydejPridatPolozku("Množství", Fask.Module.MTJ.JimiTore.Baleni.Forms.SejmiKodForm.TypeOfCode.Numeric, 0, false, false, mnozstvi.ToString(), zrow, true, true, false, this.backgroundColor))
                {
                    naplnp.Text = "Zadejte množství";
                    DialogResult dr = naplnp.ShowDialog();
                    if (dr != DialogResult.OK)
                        return;

                    mnozstvi = Convert.ToInt32(naplnp.Kod);
                }

                var item = dsVydej.Zbozi.NewZboziRow();

                item.matid = zrow.matid;
                item.CZ_CarKod = barcode;
                item.mnozstvi = mnozstvi;
                item.nazev = zrow.IsnazevNull() ? string.Empty : zrow.nazev;
                item.mj = zrow.IsmjNull() ? string.Empty : zrow.mj;
                item.delka = zrow.IsdelkaNull() ? 0 : zrow.delka;

                // tisk
                // množství pro tisk je naplněno vždy hodnotou 1 a uživatel může zadat jiné množství štítků a třeba i 0 (bez storna)
                bool printed = false;
                while (true)
                {
                    try
                    {
                        printed = BaleniTisk.Print(null, item, null, null, Fask.PrinterFactory.PrinterModules.JimiTorePrijem, true, true, 1);

                        if (!printed)
                        {
                            DialogResult drPrint = MessageBoxBig.Show("Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                            Logging.Log.Write("Chyba tisku, opakovat?:" + drPrint.ToString() + ",UID:" + Globals.UserID + ",ULogin:" + Globals.UserLogin + ",TID:" + Globals.TermID.ToString() + ",prijemka cislo:" + prijemka);
                            if (drPrint == DialogResult.Yes)
                                continue;
                            else    // pri chybe tisku umoznit pokracovat??
                                return;
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex, "MTJ.JimiTore.Baleni.Prijem,  Tisk");
                        if (MessageBoxBig.Show(ex.Message + "\nOpakovat?", "Chyba tisku", MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical)
                            == DialogResult.Yes)
                            continue;
                        else        // pri chybe tisku umoznit pokracovat??
                            return;
                    }
                }
                
                dsVydej.Zbozi.AddZboziRow(item);

                dataGrid1.CurrentRowIndex = dataGrid1.CurrentRowIndex;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "PrijemZbytku.NactenePolozkyList, PerformPridatPolozku");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void miSmazat_Click(object sender, EventArgs e)
        {
            PerformDelete();
        }

        private void miTisk_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                if (zboziRow == null)
                {
                    MessageBoxBig.Show("Není vybrán záznam pro tisk", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                BaleniTisk.Print(null, zboziRow, null, null, Fask.PrinterFactory.PrinterModules.JimiTorePrijem, true, true, 1);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "PrijemZbytku.NactenePolozkyList, miTisk_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore OnlineGetPolozky(string prijemka, string barcode, out Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusPolozka sp)
        {
            sp = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.StatusPolozka();

            try
            {
                return wsBaleni.PrijemGetZbozi(Globals.UserLogin, Globals.TermID, prijemka, barcode, out sp);
            }
            catch (Exception ex)
            {
                sp.Result = Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.STATUSPolozkaRes.ERROR;
                sp.Message = ex.Message;
                Logging.Log.Write(ex.Message, "PrijemZbytku.NactenePolozkyList, OnlineGetPolozky");

                return null;
            }
        }
    }
}