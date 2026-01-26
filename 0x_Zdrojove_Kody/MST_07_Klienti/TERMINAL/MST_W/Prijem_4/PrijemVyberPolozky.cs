using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemVyberPolozky : System.Windows.Forms.Form
    {
        public PrijemVyberPolozky()
        {
            InitializeComponent();

            MyInitializeGrid();
            MyInitializeDetailLabels();
        }

        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow SelectedPE
        {
            get
            {
                try
                {
                    //return (dataGrid1.BindingContext[listPolozekView].Current as DataRowView).Row as ListPolozekDataSet.PolozkyRow;                    
                    //return (dataGrid1.BindingContext[bin.Polozky].Current as DataRowView).Row as ListPolozekDataSet.PolozkyRow;
                    return (cZMSTPEBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.SQLiteDBs.DataSets.Prijem PrijemData
        {
            set
            {
                cZMSTPEBindingSource.DataSource = value;
                dataGrid1.CurrentRowIndex = 0;
            }
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
            this.dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;
        }

        private void MyInitializeDetailLabels()
        {
            df_CZCARKOD.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.CZ_CarKodColumn.ColumnName].HeaderText + " :";
            df_ITEMDESC.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.ITEMDESCColumn.ColumnName].HeaderText + " :";
            df_itemnmbr.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.ITEMNMBRColumn.ColumnName].HeaderText + " :";
            df_LOCNCODE.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.LOCNCODEColumn.ColumnName].HeaderText + " :";
            df_QTYPACK.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.QTYPACKColumn.ColumnName].HeaderText + " :";
            df_VNDDOCNM.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.VNDDOCNMColumn.ColumnName].HeaderText + " :";
            df_VNDITNUM.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.VNDITNUMColumn.ColumnName].HeaderText + " :";
            dfNasnimano.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.NasnimanoColumn.ColumnName].HeaderText + " :";
            dfORD.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.ORDColumn.ColumnName].HeaderText + " :";
            dfPONUMBER.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.PONUMBERColumn.ColumnName].HeaderText + " :";
            dfQTYSHPPD.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.QTYSHPPDColumn.ColumnName].HeaderText + " :";
            dfZbyva.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.ZbyvaColumn.ColumnName].HeaderText + " :";
            dfMJ.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.MJColumn.ColumnName].HeaderText + " :";
            dfWEIGHT.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.WEIGHTColumn.ColumnName].HeaderText + " :";
            dfNMBRPAL.Popis = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.NMBRPALColumn.ColumnName].HeaderText + " :";
        }

        private void PrijemVyberPolozky_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            panelButtons_Resize(null, null);

            Rezim = RezimZobrazeni.List;
            
            this.dataGrid1.Focus();
        }

        private void finalize()
        {
            dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void PerformOK()
        {
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public enum RezimZobrazeni
        {
            List,
            Detail
        }

        private RezimZobrazeni _rezim;
        public RezimZobrazeni Rezim
        {
            get { return _rezim; }
            set
            {
                _rezim = value;
                SwitchRezim();
            }
        }

        private void SwitchRezim()
        {
            switch (_rezim)
            {
                case RezimZobrazeni.Detail:
                    panelGrid.Dock = DockStyle.None;
                    panelGrid.Hide();
                    panelDetail.Show();
                    panelDetail.Dock = DockStyle.Fill;
                    break;
                case RezimZobrazeni.List:
                default:
                    panelDetail.Dock = DockStyle.None;
                    panelDetail.Hide();
                    panelGrid.Show();
                    panelGrid.Dock = DockStyle.Fill;
                    break;
            }
        }

        private void UpdateForm()
        {
            df_CZCARKOD.Data =
            df_ITEMDESC.Data =
            df_itemnmbr.Data =
            df_LOCNCODE.Data =
            df_QTYPACK.Data =
            df_VNDDOCNM.Data =
            df_VNDITNUM.Data =
            dfNasnimano.Data =
            dfORD.Data =
            dfPONUMBER.Data =
            dfQTYSHPPD.Data =
            dfMJ.Data = 
            dfITEMCODE.Data = 
            dfWEIGHT.Data = 
            dfNMBRPAL.Data = 
            dfZbyva.Data = "-";

            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow selpe = this.SelectedPE;
            try
            {
                df_CZCARKOD.Data = selpe.CZ_CarKod.Trim();
                df_ITEMDESC.Data = selpe.ITEMDESC.Trim();
                df_itemnmbr.Data = selpe.ITEMNMBR.Trim();
                df_LOCNCODE.Data = selpe.LOCNCODE.Trim();
                df_QTYPACK.Data = selpe.QTYPACK.ToString(Settings.UIFormatDesCisel);
                df_VNDDOCNM.Data = selpe.VNDDOCNM.Trim();
                df_VNDITNUM.Data = selpe.VNDITNUM.Trim();
                dfNasnimano.Data = selpe.Nasnimano.ToString(Settings.UIFormatDesCisel);
                dfORD.Data = selpe.ORD.ToString();
                dfPONUMBER.Data = selpe.PONUMBER.Trim();
                dfQTYSHPPD.Data = selpe.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                dfZbyva.Data = selpe.Zbyva.ToString(Settings.UIFormatDesCisel);
                dfWEIGHT.Data = selpe.IsWEIGHTNull() ? "-" : selpe.WEIGHT.ToString(Settings.UIFormatDesCisel);
                dfITEMCODE.Data = selpe.IsITEMCODENull() ? "-" : selpe.ITEMCODE.Trim();
                dfMJ.Data = selpe.IsMJNull() ? "-" : selpe.MJ.Trim();
                dfNMBRPAL.Data = selpe.IsNMBRPALNull() ? "-" : selpe.NMBRPAL.Trim();
            }
            catch
            {
            }
        }

        private void dataGrid1_CurrentRowIndexChanged(object sender, EventArgs e)
        {
            this.UpdateForm();
        }

        private void PrijemVyberPolozky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PerformOK();
            else if (e.KeyCode == Keys.Escape)
                PerformCancel();
            else if (e.KeyCode == Keys.F1)
                Rezim = RezimZobrazeni.List;
            else if (e.KeyCode == Keys.F2)
                Rezim = RezimZobrazeni.Detail;
            else
                return;

            e.Handled = true;
        }

        private void buttonOK_Resize(object sender, EventArgs e)
        {
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            Rezim = RezimZobrazeni.List;
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            Rezim = RezimZobrazeni.Detail;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }
    }
}