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
    public partial class UkolovaniMain : Form
    {
		//private _WebRefernces_Globals.UkolovaniServiceSession _ukolovaniService = null;

		//private Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter _ukol_ta = null;
		//private Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter _ukol_uziv_ta = null;
		//private Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter _ukol_state_ta = null;
		//private Fask.SQLiteDBs.DataSets.UkolyTableAdapters.UkolyTableAdapter _ukoly_ta = null;

		public static UkolovaniMain UkolovaniMainInstance = null;
		public GlobalObject globalObject = new GlobalObject();


        private Fask.SQLiteDBs.DataSets.Ukoly.UkolyRow SelectedUkol
        {
            get
            {
                try
                {
                    return (ukolyBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Ukoly.UkolyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public UkolovaniMain()
        {
            InitializeComponent();

            MyInitializeGrid();

			UkolovaniMainInstance = this;

			//_ukolovaniService = new Fask.MST_W._WebRefernces_Globals.UkolovaniServiceSession();
			//_ukolovaniService.Url = MST_Global.ServerAddress + "Ukolovani.asmx";
			//_ukolovaniService.Timeout = MST_Global.TasksTimeout;
			//_ukolovaniService.UpdateWebServiceCredentials();

			//_ukol_ta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter();
			//_ukol_uziv_ta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
			//_ukol_state_ta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter();
			//_ukoly_ta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.UkolyTableAdapter();

			//_ukol_ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUkolyDB);
			//_ukol_uziv_ta.Connection = _ukol_ta.Connection;
			//_ukol_state_ta.Connection = _ukol_ta.Connection;
			//_ukoly_ta.Connection = _ukol_ta.Connection;

            Filtr = Settings.UkolovaniFiltr;            
        }

        private void MyInitializeGrid()
        {
            this.dgUkoly.TableStyles[0].MappingName = ukoly._Ukoly.TableName;
            this.dgUkoly.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dgUkoly.Font = new Font(this.dgUkoly.Font.Name, Settings.UIGridFont, this.dgUkoly.Font.Style);
            this.dgUkoly.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void finalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.dgUkoly.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
            Settings.UkolovaniFiltr = Filtr;
            Settings.Update();
            Cursor.Current = Cursors.Default;
        }

        private void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
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

            ReloadUkoly();
        }

        private void ReloadUkoly()
        {
            try
            {
				//_ukol_ta.ClearBeforeFill = true;
				//_ukol_uziv_ta.ClearBeforeFill = true;
				//_ukol_state_ta.ClearBeforeFill = true;
				//_ukoly_ta.ClearBeforeFill = true;

				////naplneni ukolu ...
				//_ukol_ta.FillByUserID(ukoly.CZ_UKOL, MST_Global.UserID);
				//_ukol_uziv_ta.FillByUserID(ukoly.CZ_UKOL_UZIV, MST_Global.UserID);
				//_ukol_state_ta.Fill(ukoly.CZ_UKOL_STATE);
				//_ukoly_ta.FillByUserID(ukoly._Ukoly, MST_Global.UserID);

				ukoly.CZ_UKOL.Clear();
				ukoly.CZ_UKOL_UZIV.Clear();
				ukoly.CZ_UKOL_STATE.Clear();
				ukoly._Ukoly.Clear();

				UkolovaniMain.UkolovaniMainInstance.globalObject.controller_ukoly.CZ_UKOL_FillByUserID(ukoly.CZ_UKOL, MST_Global.UserID);
				UkolovaniMain.UkolovaniMainInstance.globalObject.controller_ukoly.CZ_UKOL_UZIV_FillByUserID(ukoly.CZ_UKOL_UZIV, MST_Global.UserID);
				UkolovaniMain.UkolovaniMainInstance.globalObject.controller_ukoly.CZ_UKOL_STATE_Fill(ukoly.CZ_UKOL_STATE);
				UkolovaniMain.UkolovaniMainInstance.globalObject.controller_ukoly.Ukoly_FillByUserID(ukoly._Ukoly, MST_Global.UserID);


            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }

            ukolyBindingSource.DataMember = ukoly._Ukoly.TableName;
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
                PerformOpen();
            }
            else if (e.KeyCode == Keys.D1)
            {
                UkolyAktualizace();
            }
            else if (e.KeyCode == Keys.F1)
            {
                menuItemZobrazeniVse_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                menuItemZobrazeniNove_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                menuItemZobrazeniAktivni_Click(null, null);
            }
            else if (e.KeyCode == Keys.F4)
            {
                menuItemZobrazeniDokoncene_Click(null, null);
            }
            else
            {
                e.Handled = false;
            }
        }

        private void menuItemActualize_Click(object sender, EventArgs e)
        {
            UkolyAktualizace();
        }

        private void UkolyAktualizace()
        {
            try
            {
				var so = UkolovaniMain.UkolovaniMainInstance.globalObject._ukolovaniService.PrepareDB(MST_Global.TerminalID, MST_Global.UserID, Path.GetFileName(Main.CiselnikUkolyDB));
                //Fask.MST_W.UkolovaniService.StatusObject so = _ukolovaniService.PrepareDB(MST_Global.TerminalID, MST_Global.UserID, Path.GetFileName(Main.CiselnikUkolyDB));
                if (so.StatusText != "OK")
                {
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                //Stazeni a aktualizace ciselniku ukolu a refresh ukolu, idealne zachovat aktualni vybrany ukol...
                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikUkolyDB);

                //zapamatovat aktualni vybrany ukol a po reloadu znovu na nej nalistovat
                ReloadUkoly();
                // TODO : nalistovat na posledni ukol ...
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            PerformOpen();
        }

        private void PerformOpen()
        {
            try
            {
                if (SelectedUkol == null)
                {
                    MessageBoxBig.Show("Není vybrán úkol", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOLRow u = ukoly.CZ_UKOL.FindByID(SelectedUkol.ID);
                Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVRow uuziv = ukoly.CZ_UKOL_UZIV.FindByID(SelectedUkol.ID_Uziv);

                using (UkolovaniUkolEdit uedit = new UkolovaniUkolEdit(u,uuziv))
                {
                    DialogResult dr = uedit.ShowDialog();
                    if (dr == DialogResult.Cancel)
                        return;
                    else if (dr == DialogResult.OK)
                    { // aktualizace lokalnich dat
						UkolovaniMain.UkolovaniMainInstance.globalObject.controller_ukoly.CZ_UKOL_UZIV_Update(uedit._ukol_uziv);
                        //_ukol_uziv_ta.Update(uedit._ukol_uziv); //tady se zmenil stav...

                        int position = ukolyBindingSource.Position;
                        ReloadUkoly();
                        ukolyBindingSource.Position = position;

                        ////aktualizace aktualniho pohledu ...
                        //if (uedit._ukol_uziv.IsDateFinishedNull())
                        //    SelectedUkol.SetDateFinishedNull();
                        //else
                        //    SelectedUkol.DateFinished = uedit._ukol_uziv.DateFinished;
                        
                        //if (uedit._ukol_uziv.IsDateChangedNull())
                        //    SelectedUkol.SetDateChangedNull();
                        //else
                        //    SelectedUkol.DateChanged = uedit._ukol_uziv.DateChanged;
                        
                        //if (uedit._ukol_uziv.IsDateNotifyNull())
                        //    SelectedUkol.SetDateNotifyNull();
                        //else
                        //    SelectedUkol.DateNotify = uedit._ukol_uziv.DateNotify;

                        //if (uedit._ukol_uziv.IsUserIDChangedNull())
                        //    SelectedUkol.SetUserIDChangedNull();
                        //else
                        //    SelectedUkol.UserIDChanged = uedit._ukol_uziv.UserIDChanged;

                        //SelectedUkol.Note = uedit._ukol_uziv.Note;
                        //SelectedUkol.State_Uziv = uedit._ukol_uziv.State;

                        //ukoly.AcceptChanges();
                    }
                }
                this.Show();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }


        #region Filtry nad ukoly
        // TODO : dodelat filtry pomoci Property ...
        private void menuItemZobrazeniVse_Click(object sender, EventArgs e)
        {
            try
            {
                Filtr = FiltrEnum.Vse;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void menuItemZobrazeniAktivni_Click(object sender, EventArgs e)
        {
            try
            {
                Filtr = FiltrEnum.Aktivni;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void menuItemZobrazeniDokoncene_Click(object sender, EventArgs e)
        {
            try
            {
                Filtr = FiltrEnum.Dokoncene;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void menuItemZobrazeniNove_Click(object sender, EventArgs e)
        {
            try
            {
                #region old ...
                //SqlCEDBs.DataSets.Ukoly.CZ_UKOL_STATERow[] ustates = (Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATERow[])ukoly.CZ_UKOL_STATE.Select("IsStart=True");
                //SqlCEDBs.DataSets.Ukoly.CZ_UKOL_STATERow ustate = null;

                //if (ustates.Length > 0)
                //    ustate = ustates[0];

                //if (ustate == null)
                //{
                //    MessageBoxBig.Show("Nenalezen výchozí stav pro nové úkoly", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                //    return;
                //}

                //ukolyBindingSource.Filter = "State_Uziv='" + ustate.State + "'";
                #endregion

                Filtr = FiltrEnum.Nove;

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }
        #endregion

        public enum FiltrEnum
        {
            Vse,
            Nove,
            Aktivni,
            Dokoncene
        }

        private FiltrEnum _filtr = FiltrEnum.Aktivni;
        public FiltrEnum Filtr
        {
            get { return _filtr; }
            set
            {
                _filtr = value;

                switch (_filtr)
                {
                    case FiltrEnum.Nove:
                        ukolyBindingSource.Filter = ukoly._Ukoly.IsStartColumn.ColumnName + "=True";
                        break;
                    case FiltrEnum.Aktivni:
                        //ukolyBindingSource.Filter = ukoly._Ukoly.DateFinishedColumn.ColumnName + " is null";
                        ukolyBindingSource.Filter = ukoly._Ukoly.IsEndColumn.ColumnName + "=False";
                        break;
                    case FiltrEnum.Dokoncene:
                        //ukolyBindingSource.Filter = ukoly._Ukoly.DateFinishedColumn.ColumnName + " is not null";
                        ukolyBindingSource.Filter = ukoly._Ukoly.IsEndColumn.ColumnName + "=True";
                        break;
                    case FiltrEnum.Vse:
                    default:
                        ukolyBindingSource.RemoveFilter();
                        break;
                }
                UpdateForm();
            }
        }

        private void UpdateForm()
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            // Filtr
            string f = "F:";
            switch (_filtr)
            {
                case FiltrEnum.Vse:
                    f += "V";
                    break;
                case FiltrEnum.Nove:
                    f += "N";
                    break;
                case FiltrEnum.Aktivni:
                    f += "A";
                    break;
                case FiltrEnum.Dokoncene:
                    f += "D";
                    break;
                default:
                    f += "-";
                    break;
            }
            string u = Ukolovani_1.Ukolovani_Checker.UkolyInfo(MST_Global.UserID) + " ";
            statusBar.Text = u + f;
        }

    }
}