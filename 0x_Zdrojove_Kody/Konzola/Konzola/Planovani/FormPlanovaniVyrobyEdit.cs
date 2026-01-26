using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Globalization;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Planovani
{
    public partial class FormPlanovaniVyrobyEdit : Form
    {
        //public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_PVRow PV_Row;

        private Fask.Interfaces.IMES providerPV = null;

        public FormPlanovaniVyrobyEdit()
        {
            InitializeComponent();
        }

        private void FormPlanovaniVyrobyEdit_KeyDown(object sender, KeyEventArgs e)
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

        private void FormPlanovaniVyrobyEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormPlanovaniVyrobyEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerPV == null)
                    throw new Exception("Provider 'Predloha PV' není inicializován");

                LoadData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerPV == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.PV.IPV).IsAssignableFrom(t))
                            {
                                providerPV = (Fask.Interfaces.Vyroba.PV.IPV)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPV != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPV.InitProvider();
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            //if (PV_Row != null)
            //{
            //    //string a;
            //    DateTime b;
            //    //int c;
            //    //decimal d;
            //    bool e;

            //    tb_OBJ_NMBR.Text = PV_Row.IsOBJ_NMBRNull() ? string.Empty : PV_Row.OBJ_NMBR;
            //    tb_OBJ_DESC.Text = PV_Row.IsOBJ_DESCNull() ? string.Empty : PV_Row.OBJ_DESC;
            //    tb_OBJ_TYPE.Text = PV_Row.IsOBJ_TYPENull() ? string.Empty : PV_Row.OBJ_TYPE;
            //    tb_OBJ_COMPANY.Text = PV_Row.IsOBJ_COMPANYNull() ? string.Empty : PV_Row.OBJ_COMPANY;
            //    dtp_OBJ_DATE_FROM.Value = PV_Row.IsOBJ_DATE_FROMNull() ? DateTime.Now : PV_Row.OBJ_DATE_FROM;
            //    dtp_OBJ_DATE_TO.Value = PV_Row.IsOBJ_DATE_TONull() ? DateTime.Now : PV_Row.OBJ_DATE_TO;
            //    tb_OBJ_ORD.Text = PV_Row.IsOBJ_ORDNull() ? string.Empty : PV_Row.OBJ_ORD.ToString();
            //    tb_OBJ_ITEM_ORD.Text = PV_Row.IsOBJ_ITEM_ORDNull() ? string.Empty : PV_Row.OBJ_ITEM_ORD.ToString();
            //    tb_ITEMNMBR.Text = PV_Row.ITEMNMBR;
            //    tb_ITEMDESC.Text = PV_Row.IsITEMDESCNull() ? string.Empty : PV_Row.ITEMDESC;
            //    tb_ITEMCODE.Text = PV_Row.IsITEMCODENull() ? string.Empty : PV_Row.ITEMCODE;
            //    tb_QTY.Text = PV_Row.QTY.ToString();
            //    //b = PV_Row.DATE_ZAPLANOVANI;
            //    chb_VP_PRPS.Checked = PV_Row.VP_PRPS;
            //    tb_VP_PRPS_QTY.Text = PV_Row.IsVP_PRPS_QTYNull() ? string.Empty : PV_Row.VP_PRPS_QTY.ToString("0.000");
            //    //tb_VP_PRDCT_QTY.Text = PV_Row.IsVP_PRDCT_QTYNull() ? string.Empty : PV_Row.VP_PRDCT_QTY.ToString("0.000");
            //    tb_USERID.Text = PV_Row.IsUSERIDNull() ? string.Empty : PV_Row.USERID.ToString();

            //}
            //else
            //{

            tb_USERID.Text = FASK.Logins.Uzivatel.Instance.UserID;
            tb_USERID.ReadOnly = true;

            tb_OBJ_ORD.Enabled = false;
            tb_OBJ_ITEM_ORD.Enabled = false;





            // }
        }

        private void FormPlanovaniVyrobyEdit_Resize(object sender, EventArgs e)
        {
            button_Storno.Size = new Size(panelButtons.Width / 2, panelButtons.Height);
        }


        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            try
            {
                if (!ValidateData())
                    return;

                //TODO co se ma udelat?



                #region INSERT

                string OBJ_NMBR = tb_OBJ_NMBR.Text;
                string OBJ_DESC = tb_OBJ_DESC.Text;
                string OBJ_TYPE = tb_OBJ_TYPE.Text;
                string OBJ_COMPANY = tb_OBJ_COMPANY.Text;
                DateTime? OBJ_DATE_FROM = dtp_OBJ_DATE_FROM.Value;
                DateTime? OBJ_DATE_TO = dtp_OBJ_DATE_TO.Value;
                int? OBJ_ORD = string.IsNullOrEmpty(tb_OBJ_ORD.Text) ? null : (int?)int.Parse(tb_OBJ_ORD.Text);
                int? OBJ_ITEM_ORD = string.IsNullOrEmpty(tb_OBJ_ITEM_ORD.Text) ? null : (int?)int.Parse(tb_OBJ_ITEM_ORD.Text);
                string ITEMNMBR = tb_ITEMNMBR.Text;
                string ITEMDESC = tb_ITEMDESC.Text;
                string ITEMCODE = tb_ITEMCODE.Text;
                decimal QTY = decimal.Parse(tb_QTY.Text);


                DateTime? DATE_ZAPLANOVANI = (DateTime?)DateTime.Now;
                int? USERID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);
                bool VP_PRPS = chb_VP_PRPS.Checked;
                decimal? VP_PRPS_QTY = string.IsNullOrEmpty(tb_VP_PRPS_QTY.Text) ? null : (int?)int.Parse(tb_VP_PRPS_QTY.Text);
                string VP_PRPS_SOPNUMBE = tb_VP_PRPS_SOPNUMBE.Text;
                decimal? VP_PRDCT_QTY = null;

                int? Ref_PVH = null;

                bool state;

                if ((providerPV != null) && providerPV is Fask.Interfaces.Vyroba.PV.IPV_Insert_PV)
                {
                    state = ((Fask.Interfaces.Vyroba.PV.IPV_Insert_PV)providerPV).Insert(
                                                                                                    OBJ_NMBR,
                                                                                                    OBJ_DESC,
                                                                                                    OBJ_TYPE,
                                                                                                    OBJ_COMPANY,
                                                                                                    OBJ_DATE_FROM,
                                                                                                    OBJ_DATE_TO,
                                                                                                    OBJ_ORD,
                                                                                                    OBJ_ITEM_ORD,
                                                                                                    ITEMNMBR,
                                                                                                    ITEMDESC,
                                                                                                    ITEMCODE,
                                                                                                    QTY,
                                                                                                    DATE_ZAPLANOVANI,
                                                                                                    VP_PRPS,
                                                                                                    VP_PRPS_QTY,
                                                                                                    VP_PRPS_SOPNUMBE,
                                                                                                    VP_PRDCT_QTY,
                                                                                                    USERID,
                                                                                                    Ref_PVH);
                }
                else
                    throw new Exception("IPV_Insert_PV not implementet");

                if (!state)
                {
                    throw new Exception(string.Format("položka '{0}'({1}). Chyba pri Inserte... ", ITEMNMBR, ITEMDESC));
                }

                #endregion



                DialogResult = System.Windows.Forms.DialogResult.OK;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidateData()
        {
            try
            {
                ErrorsClear();

                if (string.IsNullOrEmpty(tb_ITEMNMBR.Text.Trim()))
                    errorProvider1.SetError(tb_ITEMNMBR, "Musíte zadat číslo položky");
                else if (tb_ITEMNMBR.Text.Length > 40)
                    errorProvider1.SetError(tb_ITEMNMBR, "Maximalne 40 znaků je povoleno.");


                if (string.IsNullOrEmpty(tb_QTY.Text.Trim()))
                    errorProvider1.SetError(tb_QTY, "Musíte zadat množství položky");
                else
                {
                    try
                    {
                        decimal.Parse(tb_QTY.Text.Trim());
                    }
                    catch
                    {
                        errorProvider1.SetError(tb_QTY, "Musíte zadat číslo.");
                    }
                }


                if (!string.IsNullOrEmpty(tb_VP_PRPS_QTY.Text))
                {
                    try
                    {
                        decimal.Parse(tb_VP_PRPS_QTY.Text);
                    }
                    catch
                    {
                        errorProvider1.SetError(tb_VP_PRPS_QTY, "Musíte zadat číslo anebo nechat prázdné.");
                    }
                }

                if (!string.IsNullOrEmpty(tb_OBJ_ORD.Text))
                {
                    try
                    {
                        int.Parse(tb_OBJ_ORD.Text);
                    }
                    catch
                    {
                        errorProvider1.SetError(tb_OBJ_ORD, "Musíte zadat číslo anebo nechat prázdné.");
                    }
                }

                if (!string.IsNullOrEmpty(tb_OBJ_ITEM_ORD.Text))
                {
                    try
                    {
                        int.Parse(tb_OBJ_ITEM_ORD.Text);
                    }
                    catch
                    {
                        errorProvider1.SetError(tb_OBJ_ITEM_ORD, "Musíte zadat číslo anebo nechat prázdné.");
                    }
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return IsAllValid();
        }


        private void ErrorsClear()
        {
            errorProvider1.Clear();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in PanelMain.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Ciselniky.FormZboziList frmzbozi = new Ciselniky.FormZboziList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
            {
                frmzbozi.Text = "Výběr zboží";

                if (frmzbozi.ShowDialog(this) != DialogResult.OK)
                    return;

                Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow SelectedRow = frmzbozi.FASK_ZASOBY_selectedRow;


                tb_ITEMDESC.Text = SelectedRow.ITEMDESC.Trim();
                tb_ITEMNMBR.Text = SelectedRow.ITEMNMBR.Trim();
                tb_ITEMCODE.Text = SelectedRow.ITEMCODE.Trim();

            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            PerformOK();

        }

        private void button_Storno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }
    }
}
