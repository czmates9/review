using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.ServerAccess;
using System.IO;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Ukolovani_1
{
    public partial class UkolovaniUkolEdit : Form
    {
        // TODO : Umoznit nastavit uzivateli notifikaci pomoci menu ... tlacitkem ..
        // pri zmene ukolu dojde ke zruseni notifikace ... 

        private _WebRefernces_Globals.UkolovaniServiceSession _ukolovaniService = null;

        //private int _ukol_uziv_id = 0;
        public Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOLRow _ukol = null;
        public Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVRow _ukol_uziv = null;

        //private Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter _ukol_ta = null;
        //private Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter _ukol_uziv_ta = null;
        //private Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter _ukol_state_ta = null;
        //private Fask.SQLiteDBs.DataSets.UkolyTableAdapters.UkolyTableAdapter _ukoly_ta = null;

        public UkolovaniUkolEdit(Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOLRow ukol , Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVRow ukol_uziv)
        {
            InitializeComponent();

            _ukol = ukol;
            _ukol_uziv = ukol_uziv;

            //_ukol_ta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter();
            //_ukol_uziv_ta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
            //_ukol_state_ta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter();

            //_ukol_ta.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Main.CiselnikUkolyDB);
            //_ukol_uziv_ta.Connection = _ukol_ta.Connection;
            //_ukol_state_ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUkolyDB);

            _ukolovaniService = new Fask.MST_W._WebRefernces_Globals.UkolovaniServiceSession();
            _ukolovaniService.Url = MST_Global.ServerAddress + "Ukolovani.asmx";
            _ukolovaniService.Timeout = MST_Global.TasksTimeout;
            _ukolovaniService.UpdateWebServiceCredentials();

        }


        private void finalize()
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Current = Cursors.Default;
        }

        private void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            // TODO : aktualizace stavu ...
            Fask.MST_W.UkolovaniService.StatusObject so = null;
            Fask.MST_W.UkolovaniService.Ukol u = new Fask.MST_W.UkolovaniService.Ukol();
            Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATERow ustate = (Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATERow)cmbStav.SelectedItem;
            try
            {
                u.id = _ukol_uziv.ID;
                u.state = ustate.State;
                if (ustate.IsEnd)
                    u.datefinished = DateTime.Now;
                u.note = dfNote.Data;

                if (_ukol_uziv.IsDateNotifyNull())
                    u.datenotify = null;
                else
                    u.datenotify = _ukol_uziv.DateNotify;

                if (!_ukol_uziv.IsDateNotifyNull() && _ukol_uziv.DateNotify <= DateTime.Now)
                { // pripomenuti zrusi, protoze je mensi nez aktualni ... 
                    DialogResult drNotify = MessageBoxBig.Show("Datum a čas je menší než aktuální!\nZrušit připomenutí?", "Připomenutí", MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Warning);
                    if (drNotify == DialogResult.Cancel)
                        return;
                    else if (drNotify == DialogResult.Yes)
                    { // zrusi se notifikace ...
                        u.datenotify = null;
                    }
                    else if (drNotify == DialogResult.No)
                    { //vyvolani dialogu pro zmenu data a casu ... 
                        DateTime newdtnotify = DateTime.Now;
                        DialogResult drNotifyDT = DateTimeInputBox.Show("Připomenutí", _ukol_uziv.DateNotify, out newdtnotify);
                        if (drNotifyDT == DialogResult.Cancel)
                            return;
                        else if (drNotifyDT == DialogResult.OK)
                            u.datenotify = newdtnotify;
                    }
                }

                /*
                // Pokud je ukol prenesen do koncoveho stavu, tak se pripomenuti zrusi ... 
                if (!ustate.IsEnd)
                { 
                    // comment : pokud uzivatel edituje ukol, tak tim rika, ze ho cetl/zmenil a notifikace se rusi
                    // pokud si nastavit notifikaci novou, tak se nastavi na novou hodnotu ...
                    //if (_ukol_uziv.IsDateNotifyNull())
                    //    u.datenotify = null;
                    //else
                    //    u.datenotify = _ukol_uziv.DateNotify;
                    if (_ukol_uziv.IsDateNotifyNull())
                    { // pripomenuti se smaze ...
                        DialogResult drNotifyAdd = MessageBoxBig.Show("Přidat datum a čas připomenutí?", "Připomenutí", MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                        if (drNotifyAdd == DialogResult.Cancel)
                            return;
                        else if (drNotifyAdd == DialogResult.No)
                            u.datenotify = null;
                        else if (drNotifyAdd == DialogResult.Yes)
                        {
                            DateTime newdtnotify = DateTime.Now;
                            DialogResult drNotifyDT = DateTimeInputBox.Show("Připomenutí", DateTime.Now + new TimeSpan(1, 0, 0), out newdtnotify);
                            if (drNotifyDT == DialogResult.Cancel)
                                return;
                            else if (drNotifyDT == DialogResult.OK)
                                u.datenotify = newdtnotify;
                        }
                    }
                    else if (_ukol_uziv.DateNotify <= DateTime.Now)
                    { // pripomenuti je mensi nez aktualni datum cas, tak se zepta, zda zmenit nebo zrusit
                        // TODO : prace s pripomenutim, dialog pripomenuti datum a cas ...
                        DialogResult drNotify = MessageBoxBig.Show("Zrušit datum a čas připomenutí?", "Připomenutí", MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                        if (drNotify == DialogResult.Cancel)
                            return;
                        else if (drNotify == DialogResult.Yes)
                        { // zrusi se notifikace ...
                            u.datenotify = null;
                        }
                        else if (drNotify == DialogResult.No)
                        { //vyvolani dialogu pro zmenu data a casu ... 
                            DateTime newdtnotify = DateTime.Now;
                            DialogResult drNotifyDT = DateTimeInputBox.Show("Připomenutí", _ukol_uziv.DateNotify, out newdtnotify);
                            if (drNotifyDT == DialogResult.Cancel)
                                return;
                            else if (drNotifyDT == DialogResult.OK)
                                u.datenotify = newdtnotify;
                        }
                    }
                    else
                    { // pripomenuti je vetsi nez aktualni datum cas, tak zustane ...
                        u.datenotify = _ukol_uziv.DateNotify;
                    }
                }
                */ //notification ... 

                so = _ukolovaniService.Update(MST_Global.TerminalID, MST_Global.UserID, ref u);
                if (so.Exception || so.StatusText != "OK")
                {
                    throw new Exception(so.StatusText);
                }

                // aktualizace zpet do db ...
                _ukol_uziv.State = u.state;
                _ukol_uziv.Note = u.note;

                if (u.datefinished.HasValue)
                    _ukol_uziv.DateFinished = u.datefinished.Value;
                else
                    _ukol_uziv.SetDateFinishedNull();
                if (u.datechanged.HasValue)
                    _ukol_uziv.DateChanged = u.datechanged.Value;
                else
                    _ukol_uziv.SetDateChangedNull();
                if (u.useridchanged.HasValue)
                    _ukol_uziv.UserIDChanged = u.useridchanged.Value;
                else
                    _ukol_uziv.SetUserIDChangedNull();
                if (u.datenotify.HasValue)
                    _ukol_uziv.DateNotify = u.datenotify.Value;
                else
                    _ukol_uziv.SetDateNotifyNull();

            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void menuItemKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void UkolovaniMain_Load(object sender, EventArgs e)
        {
            //nastaveni velikosti ...
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly ConUkoly = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly(Main.CiselnikUkolyDB))
			{
				ConUkoly.CZ_UKOL_STATE_Fill(ukoly.CZ_UKOL_STATE);
			}

            //_ukol_state_ta.Fill(ukoly.CZ_UKOL_STATE);

            foreach (Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATERow item in ukoly.CZ_UKOL_STATE)
            {
                cmbStav.Items.Add(item);
            }

            ReloadUkol();            
        }

        private void ReloadUkol()
        {
            try
            {
                //naplneni ukolu ...
                //_ukol_uziv_ta.FillByID(ukoly.CZ_UKOL_UZIV, this._ukol_uziv_id);
                //_ukol_ta.FillByID(ukoly.CZ_UKOL, ukoly.CZ_UKOL_UZIV[0].UkolID);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }

            UpdateForm();
        }

        private void UpdateForm()
        {
            try
            {
                dfName.Data = _ukol.Name.Trim();
                dfDescription.Data = _ukol.Description.Trim();
                dfNote.Data = _ukol_uziv.IsNoteNull() ? "-" : _ukol_uziv.Note.Trim();
                dfDateCreated.Data = _ukol.DateCreated.ToString();
                dfDateFrom.Data = _ukol.IsDateFromNull() ? "-" : _ukol.DateFrom.ToString();
                dfDateTo.Data = _ukol.IsDateToNull() ? "-" : _ukol.DateTo.ToString();
                dfDateNotify.Data = _ukol_uziv.IsDateNotifyNull() ? "-" : _ukol_uziv.DateNotify.ToString();

                cmbStav.SelectedItem = ukoly.CZ_UKOL_STATE.FindByState(_ukol_uziv.State);

                UpdateDateColors();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void UkolovaniMain_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else
            {
                e.Handled = false;
            }
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size s = new Size(panelButtons.Size.Width / 2, panelButtons.Size.Height);
            buttonOK.Size = s;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void graphicButton1_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void bNotifyDelete_Click(object sender, EventArgs e)
        {
            _ukol_uziv.SetDateNotifyNull();
            dfDateNotify.Data = _ukol_uziv.IsDateNotifyNull() ? "-" : _ukol_uziv.DateNotify.ToString();

            UpdateDateColors();
        }

        private void bNotifyHourPlus_Click(object sender, EventArgs e)
        {
            DateTime dtnotify = DateTime.Now;
            if (!_ukol_uziv.IsDateNotifyNull())
                dtnotify = _ukol_uziv.DateNotify;

            dtnotify = dtnotify.AddHours(1);

            _ukol_uziv.DateNotify = dtnotify;
            dfDateNotify.Data = _ukol_uziv.IsDateNotifyNull() ? "-" : _ukol_uziv.DateNotify.ToString();

            UpdateDateColors();
        }

        private void bNotifyHourMinus_Click(object sender, EventArgs e)
        {
            DateTime dtnotify = DateTime.Now;
            if (!_ukol_uziv.IsDateNotifyNull())
                dtnotify = _ukol_uziv.DateNotify;

            dtnotify = dtnotify.AddHours(-1);

            _ukol_uziv.DateNotify = dtnotify;
            dfDateNotify.Data = _ukol_uziv.IsDateNotifyNull() ? "-" : _ukol_uziv.DateNotify.ToString();

            UpdateDateColors();
        }

        private void bNotifyEnter_Click(object sender, EventArgs e)
        {
            DateTime newdtnotify = DateTime.Now;
            if (_ukol_uziv.IsDateNotifyNull())
                //newdtnotify = DateTime.Now + new TimeSpan(1, 0, 0);
                newdtnotify = DateTime.Now;
            else
                newdtnotify = _ukol_uziv.DateNotify;

            DialogResult drNotifyDT = DateTimeInputBox.Show("Připomenutí", newdtnotify, out newdtnotify);
            if (drNotifyDT == DialogResult.Cancel)
                return;
            else if (drNotifyDT == DialogResult.OK)
            {
                _ukol_uziv.DateNotify = newdtnotify;
                dfDateNotify.Data = _ukol_uziv.IsDateNotifyNull() ? "-" : _ukol_uziv.DateNotify.ToString();

                UpdateDateColors();

            }
        }

        private void UpdateDateColors()
        {
            if (_ukol.IsDateToNull())
                dfDateTo.BackColor = SystemColors.Window;
            else if (_ukol.DateTo <= DateTime.Now)
                dfDateTo.BackColor = Color.Red;
            else
                dfDateTo.BackColor = Color.LightGreen;


            if (_ukol_uziv.IsDateNotifyNull())
                dfDateNotify.BackColor = SystemColors.Window;
            else if (_ukol_uziv.DateNotify <= DateTime.Now)
                dfDateNotify.BackColor = Color.Red;
            else
                dfDateNotify.BackColor = Color.LightGreen;
        }

        private void UkolovaniUkolEdit_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void UkolovaniUkolEdit_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

    }
}