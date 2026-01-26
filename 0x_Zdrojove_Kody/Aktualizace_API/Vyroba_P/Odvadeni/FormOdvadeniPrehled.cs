using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.Aktualizace_API
{
    public partial class FormOdvadeniPrehled : Form
    {
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow _VPH { get; set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow _VPP { get; set; }
        public Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE _TimeStateActual =  Odvadeni.FormOdvadeni.TIMESTATE.Unknown;
        public Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE _TimeStateLast = Odvadeni.FormOdvadeni.TIMESTATE.Unknown;
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _LastProduction { get; set; }

        public FormOdvadeniPrehled()
        {
            InitializeComponent();
        }

        private void FormOdvadeniPrehled_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);

            panelButtons_Resize(null, null);
            UpdateUI();
        }


        private void UpdateUI()
        {
            try
            {
                if (_VPH != null)
                {
                    dfVphSOPNUMBE.Data = _VPH.SOPNUMBE.Trim();
                    dfVphSOPDESC.Data = _VPH.IsSOPDESCNull() ? "-" : _VPH.SOPDESC.Trim();
                }

                if (_VPP != null)
                {
                    dfVppBarcodeP.Data = _VPP.BarcodeP.Trim();
                    dfVppItemdesc.Data = _VPP.IsITEMDESCNull() ? "-" : _VPP.ITEMDESC.Trim();
                    dfVppItemnmbr.Data = _VPP.ITEMNMBR.Trim();
                    dfVppVnditnum.Data = _VPP.IsVNDITNUMNull() ? "-" : _VPP.VNDITNUM.Trim();
                }

                if (_LastProduction != null)
                {
                    dfLastBarcodeP.Data = _LastProduction.BarcodeP.Trim();
                    dfLastITEMNMBR.Data = _LastProduction.ITEMNMBR.Trim();
                    dfLastQuantity.Data = _LastProduction.qty.ToString();
                    dfLastSOPNUMBER.Data = _LastProduction.SOPNUMBE.Trim();
                }

                dfTimeStateActual.Data = _TimeStateActual.ToString();
                dfTimeStateLast.Data = _TimeStateLast.ToString();

                UpdateUITimeStateActualColor();
                UpdateUITimeStateLastColor();

            }
            catch
            {
            }
        }

        private void UpdateUITimeStateActualColor()
        {
            dfVphSOPDESC.BackColor =
                dfVphSOPNUMBE.BackColor =
                dfVppBarcodeP.BackColor =
                dfVppItemdesc.BackColor =
                dfVppItemnmbr.BackColor =
                dfVppVnditnum.BackColor =
                dfTimeStateActual.BackColor = GetColorForTimeState(_TimeStateActual);
        }

        private void UpdateUITimeStateLastColor()
        {
            dfLastBarcodeP.BackColor =
                dfLastITEMNMBR.BackColor =
                dfLastQuantity.BackColor =
                dfLastSOPNUMBER.BackColor =
                dfTimeStateLast.BackColor = GetColorForTimeState(_TimeStateLast);
        }

        private Color GetColorForTimeState(Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE ts)
        {
            switch (ts)
            {
                case Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE.Korekce_Zahajena:
                case Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE.Priprava_Zahajena:
                    return Color.Yellow;
                case Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE.Odvod_Zahajen:
                    return Color.Green;
                case Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE.Korekce_Dokoncena:
                case Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE.Priprava_Dokoncena:
                case Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE.Odvod_Dokoncen:
                    return Color.Red;
                case Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE.Unknown:
                case Aktualizace_API.Odvadeni.FormOdvadeni.TIMESTATE.Nezahajeno:
                default:
                    return Color.White;
            }
        }


        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelbutton.Width / 2, panelbutton.Height);
            buttonStorno.Size = nsize;
        }

        public void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void buttonStorno_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

    }
}
