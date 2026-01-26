using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Inventura1_sqlce
{
    public partial class NaplnPolozku_sqlce : Forms.SejmiKodForm
    {

        private bool _ZobrazMnozstviNaSklade = true;
        public bool ZobrazMnozstviNaSklade
        {
            get { return _ZobrazMnozstviNaSklade; }
            set
            {
                _ZobrazMnozstviNaSklade = value;
                UpdateForm();
            }
        }

        private ListPolozky.PolozkyRow prow = null;
        public ListPolozky.PolozkyRow PROW
        {
            get { return prow; }
            set { prow = value; }
        }

        private Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3 = null;
        public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row I3Row
        {
            get { return i3; }
            set
            {
                i3 = value;
                UpdateForm();
            }
        }

        public NaplnPolozku_sqlce()
            : base()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            MyInitializeCompoment();
            Cursor.Current = Cursors.Default;
        }

        private void MyInitializeCompoment()
        {
            //Vydej.Vydej.CZMST_SEDataTable[0].
            //popis_l.Location = new Point(3, 189);
            //kod_tb.Location = new Point(3, 212);
            panelKod.Dock = DockStyle.Bottom;

            //if (i4ta == null)
            //    i4ta = new Fask.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I4TableAdapter();
            //try
            //{
            //    i4ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.DataDir, i3.CountEntries.ToString() + "." + Main.Inventura1I1);
            //}
            //catch { }
        }

        private void MyDisposeComponent()
        {
            //if (this.i4ta != null)
            //    this.i4ta.Dispose();
            //this.i4ta = null;
        }

        public NaplnPolozku_sqlce(
            string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni,
            ListPolozky.PolozkyRow prow, Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3)
            : base(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            this.prow = prow;
            this.i3 = i3;

            MyInitializeCompoment();
            Cursor.Current = Cursors.Default;
        }

        public NaplnPolozku_sqlce(ListPolozky.PolozkyRow prow, Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3)
            : this()
        {
            this.prow = prow;
            this.i3 = i3;
            MyInitializeCompoment();
        }


        private void UpdateForm()
        {
            try
            {
                labelNacist.Visible = this._ZobrazMnozstviNaSklade;

                if (prow != null)
                {
                    try { labelSklad.Data = prow.SkladID.Trim() + ":" + prow.Sklad.Trim(); }
                    catch { labelSklad.Data = "-"; }

                    labelItemDescr.Data = prow.ITEMDESC;
                    labelVNDNAME.Data = i3.VENDNAME;
                    labelBarCode.Data = i3.CZ_CarKod;
                    labelVNDITNUM.Data = i3.VNDITNUM;
                    labelNacist.Data = prow.QUANTITY.ToString(Settings.UIFormatDesCisel);

                    //decimal nacteno = Inventura1classForm.Inventura1classFormInstance.Nacteno(i1.ITEMNMBR);
                    //decimal nacteno = (decimal)(i4ta.Nasnimano(prow.ITEMNMBR) ?? 0);
                    decimal nacteno = prow.NASNIMANO;
                    labelNacteno.Data = nacteno.ToString(Settings.UIFormatDesCisel);
                    if (nacteno == prow.QUANTITY)
                    {
                        labelNacist.ForeColor = labelNacteno.ForeColor = Color.DarkGreen;
                    }
                    else if (nacteno > prow.QUANTITY)
                    {
                        labelNacist.ForeColor = labelNacteno.ForeColor = Color.Tomato;
                    }
                    else
                    {
                        labelNacist.ForeColor = labelNacteno.ForeColor = SystemColors.ControlText;
                    }

                    labelBaleni.Data = i3.QTYPACK.ToString(Settings.UIFormatDesCisel);
                    
                    dfMJ.Data = (i3 != null && !i3.IsMJNull()) ? i3.MJ.Trim() : "-";

                    dfKodPolozky.Data = !prow.IsITEMCODENull() ? prow.ITEMCODE.Trim() : "-";

                    dfCisloPolozky.Data = !prow.IsITEMNMBRNull() ? prow.ITEMNMBR.Trim() : "-";

                    labelLocnCode.Data = prow.LOCNCODE;

                    dfWeight.Data = (i3.IsWEIGHTNull() ? 0 : i3.WEIGHT).ToString(Settings.UIFormatDesCisel);

                    labelSNFind.Popis = labelSNTrack.Popis = MST_Global.SNCode;

                    if (prow.CZ_SERNUM_FIND > 0)
                        labelSNFind.Data = "Ano";
                    else
                        labelSNFind.Data = "Ne";
                    
                    if (prow.CZ_SERNUM_TRACK == 2)
                    {
                        labelSNTrack.Popis = "Šarže";
                        labelSNTrack.Data = "Ano";
                    }
                    else if (prow.CZ_SERNUM_TRACK > 0)
                    {
                        labelSNTrack.Data = "Ano";
                    }
                    else
                    {
                        labelSNTrack.Data = "Ne";
                    }
                }
                else
                {
                    labelVNDNAME.Data = 
                    labelItemDescr.Data =
                    labelBaleni.Data = 
                    labelNacist.Data = 
                    labelNacteno.Data = 
                    labelLocnCode.Data = 
                    labelBarCode.Data = 
                    labelSNFind.Data = 
                    labelSNTrack.Data = 
                    dfWeight.Data =
                    "-";
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void NaplnPolozku_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            panelButtons.Visible = MST_Global.ShowPanelButtons;
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            this.menuItem4.Enabled = Settings.Online_BYZNYS;
            this.miTisk.Enabled = MST_Global.PovolitPrintServer;
            UpdateForm();
        }

        protected override void PerformCancel()
        {
            this.MyDisposeComponent();
            base.PerformCancel();
        }

        protected override void PerformOK()
        {
            this.MyDisposeComponent();
            base.PerformOK();
        }

        private void NovyEAN()
        {
            try
            {
                if (!Settings.Online_BYZNYS)
                    return;

                this.ScannerStop();

                if (prow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1NaplnPolozkuSqlceNeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                string newean = Online.BYZNYS.Algorithms.InsertNewEAN(Convert.ToInt32(prow.ITEMNMBR), prow.ITEMDESC.Trim());
                if (newean != null)
                    i3.CZ_CarKod = newean;
                return;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void NaplnPolozku_sqlce_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            if (e.KeyCode == Keys.F5)
            {
                NovyEAN();
            }
            else
                return;

            e.Handled = true;
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            NovyEAN();
        }

        private void miTisk_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();
                ListPolozky.PolozkyRow i1 = this.prow;
                if (i1 == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1NaplnPolozkuSqlceNeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                
                //bool vytisteno = InventuraTisk.Print(pe, MST_Global.PrintServerTemplateNameInventuraPredloha);
                bool vytisteno = InventuraTisk.Print(i1, PrinterFactory.PrinterModules.InventuraPredloha);
                //Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", "print", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", pe.CountEntries, pe.PONUMBER, pe.ITEMNMBR, vytisteno.ToString(), null));
                Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "i", null, null, i1.ITEMNMBR.Trim(), vytisteno.ToString(), null));
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                ScannerStart();
            }             
        }

        private void NaplnPolozku_sqlce_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void NaplnPolozku_sqlce_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void NaplnPolozku_sqlce_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
        }

    }
}