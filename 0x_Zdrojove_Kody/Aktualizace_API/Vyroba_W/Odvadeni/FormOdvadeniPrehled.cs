using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_W.Forms;

namespace Fask.Vyroba_W.Odvadeni
{
    public partial class FormOdvadeniPrehled : Form
    {

		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow _VPH { get; set; }
		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow _VPP { get; set; }
        public FormOdvadeni.TIMESTATE _TimeStateActual = FormOdvadeni.TIMESTATE.Unknown;
        public FormOdvadeni.TIMESTATE _TimeStateLast = FormOdvadeni.TIMESTATE.Unknown;
		public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _LastProduction { get; set; }

        public FormOdvadeniPrehled()
        {
            InitializeComponent();
        }

        private void FormOdvadeniPrehled_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
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
                    dfLastITEMDESC.Data = _LastProduction.ITEMDESC.Trim();
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
                dfLastITEMDESC.BackColor =
                dfLastQuantity.BackColor =
                dfLastSOPNUMBER.BackColor =
                dfTimeStateLast.BackColor = GetColorForTimeState(_TimeStateLast);
        }

        private Color GetColorForTimeState(FormOdvadeni.TIMESTATE ts)
        {
            switch (ts)
            {
                case FormOdvadeni.TIMESTATE.Korekce_Zahajena:
                case FormOdvadeni.TIMESTATE.Priprava_Zahajena:
                    return Color.Yellow;
                case FormOdvadeni.TIMESTATE.Odvod_Zahajen:
                    return Color.Green;
                case FormOdvadeni.TIMESTATE.Korekce_Dokoncena:
                case FormOdvadeni.TIMESTATE.Priprava_Dokoncena:
                case FormOdvadeni.TIMESTATE.Odvod_Dokoncen:
                    return Color.Red;
                case FormOdvadeni.TIMESTATE.Unknown:
                case FormOdvadeni.TIMESTATE.Nezahajeno:
                default:
                    return Color.White;
            }
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

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormOdvadeniPrehled_KeyDown(object sender, KeyEventArgs e)
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

