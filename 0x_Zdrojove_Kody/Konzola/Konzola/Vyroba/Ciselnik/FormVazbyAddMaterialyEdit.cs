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

namespace Konzola.Vyroba
{

    public enum EditType
    {
        Unknown,
        AddMaterial,
        Edit,
        AddPolotovar

    }


    public partial class FormVazbyAddMaterialyEdit : Form
    {


        public string Koeficient;
        public string Alter;
        public bool VseUlozit;

        public string UserID;
        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow rowVyrobek;
        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow rowMaterial;

        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialDataTable dtMaterial;


        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow rowMaterial_zbozi;
        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow rowPolotovar;


        public string ProgressOD;
        public string ProgressDO;


        public EditType edittype;

        /// <summary>
        /// Provider pro vyhledavani filtrama
        /// </summary>
        private Fask.Interfaces.IMES providerVazby = null;

        public FormVazbyAddMaterialyEdit()
        {
            InitializeComponent();
        }

        private void FormProductionEdit_Load(object sender, EventArgs e)
        {
            try
            {

                label_Vyrobek.Text = rowVyrobek.ITEMDESC;

                InitProvider();

                if (providerVazby == null)
                    throw new Exception("Provider 'Vazby' není inicializován");
                

                //Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter taTP = new Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
               // taTP.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);

                Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable dt_onlyAlter;
                Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable dt ;


                //taTP.Fill_onlyALTERtable(ds_onlyAlter.FASK_Vyroba_TP, rowVyrobek.ITEMNMBR.Trim());

                if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Fill_onlyALTERtable)
                   dt_onlyAlter = ((Fask.Interfaces.Vazby.IVazby2_Fill_onlyALTERtable)providerVazby).Fill_onlyALTERtable(rowVyrobek.ITEMNMBR.Trim());
                else
                    throw new Exception("IVazby2_Fill_onlyALTERtable not implementet");
              


                switch (edittype)
                {
                    case EditType.Unknown:
                        break;
                    case EditType.AddMaterial:
                        label_Material.Text = rowMaterial_zbozi.ITEMDESC;
                        textBoxKoeficient.Text = "00,00";
                        labelProgress.Text = string.Format("Vloženo '{0}' z '{1}'", this.ProgressOD, this.ProgressDO);
                        //taTP.Fill_only_Koef_and_Alter(ds.FASK_Vyroba_TP, rowVyrobek.ITEMNMBR, rowMaterial_zbozi.ITEMNMBR);
                        comboBox_Alter.Items.AddRange(dt_onlyAlter.Select(null, "alter asc"));
                        comboBox_Alter.SelectedItem = null;
                        
                        break;
                    case EditType.Edit:
                        if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Fill_only_Koef_and_Alter)
                            dt = ((Fask.Interfaces.Vazby.IVazby2_Fill_only_Koef_and_Alter)providerVazby).Fill_only_Koef_and_Alter(rowVyrobek.ITEMNMBR, rowMaterial.ITEMNMBR);
                        else
                            throw new Exception("IVazby2_Fill_only_Koef_and_Alter not implementet");
                        //taTP.Fill_only_Koef_and_Alter(ds.FASK_Vyroba_TP, rowVyrobek.ITEMNMBR, rowMaterial.ITEMNMBR);
                        label_Material.Text = rowMaterial.ITEMDESC;
                        labelProgress.Text = string.Format("Vloženo '{0}' z '{1}'", this.ProgressOD, this.ProgressDO);
                        textBoxKoeficient.Text = dt[0].koef.Trim();
                        comboBox_Alter.Items.AddRange(dt_onlyAlter.Select(null, "alter asc"));
                        if (dt[0].IsalterNull())
                            comboBox_Alter.SelectedItem = null;
                        else
                            comboBox_Alter.SelectedText = dt[0].alter.Trim();
                        break;
                    case EditType.AddPolotovar:
                        label_Material.Text = rowPolotovar.ITEMDESC;
                        textBoxKoeficient.Text = "00,00";
                        labelProgress.Text = string.Format("Vloženo '{0}' z '{1}'", this.ProgressOD, this.ProgressDO);
                        comboBox_Alter.Visible = false;
                        label2.Visible = false;
                        //taTP.Fill_only_Koef_and_Alter(ds.FASK_Vyroba_TP, rowVyrobek.ITEMNMBR, rowPolotovar.ITEMNMBR);
                        //comboBox_Alter.Items.AddRange(ds_onlyAlter.FASK_Vyroba_TP.Select(null, "alter asc"));
                        //comboBox_Alter.SelectedItem = null;
                        break;
                    default:
                        break;
                }

                //comboBox_.Items = dtTP;

                FormProductionEdit_Resize(null, null);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Inicializace providera Materialy
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVazby == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vazby.IVazby2).IsAssignableFrom(t))
                            {
                                providerVazby = (Fask.Interfaces.Vazby.IVazby2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVazby != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVazby.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Naplneni textboxu a datetimepickeru.
        /// </summary>

        private void FormProductionEdit_Resize(object sender, EventArgs e)
        {
            panel_Button_Right.Width = panel_Buttons.Width / 2;

            switch (edittype)
            {
                case EditType.Unknown:
                    break;
                case EditType.AddMaterial:
                    Add_resize();
                    break;
                case EditType.Edit:
                    Edit_resize();
                    break;
                case EditType.AddPolotovar:
                    Add_resize();
                    break;
                default:
                    break;
            }
        }

        private void Edit_resize()
        {
            Size nsize = new Size(panel_Buttons.Width / 2, panel_Buttons.Height);
            panel_Button_Right.Size = nsize;

            Size sizeForBtn = new System.Drawing.Size(panel_Button_Right.Width / 2, panel_Button_Right.Height);
            buttonJump.Size = sizeForBtn;
            buttonOK.Size = sizeForBtn;

        }

        private void Add_resize()
        {
            Size nsize = new Size(panel_Buttons.Width / 3, panel_Buttons.Height);
            panel_Button_Right.Size = nsize;

            //buttonStorno.Size = nsize;
            buttonOK.Size = nsize;
            buttonJump.Size = nsize;
            
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void PerformOK()
        {
            try
            {
                if (!ValidateData())
                    return;

                //Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
                //tpta.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);

                //Pridani vyrobku
                if (edittype == EditType.AddMaterial)
                {

                    int? IDL = null; //tpta.Scalar_MAX_IDL();

                    if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Scalar_MAX_IDL)
                        IDL = ((Fask.Interfaces.Vazby.IVazby2_Scalar_MAX_IDL)providerVazby).Scalar_MAX_IDL();
                    else
                        throw new Exception("IVazby2_Scalar_MAX_IDL not implementet");

                    int ID_L;

                    if (IDL.HasValue)
                        ID_L = (int)IDL + 1;
                    else
                    {
                        throw new Exception("Nenalezen ID Low, žádný minimalne výrobe nenalezen...");
                        //ID_L = 100;
                    }

                    if (string.IsNullOrEmpty(comboBox_Alter.Text))
                    {

                        if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)
                        {
                            int a = ((Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)providerVazby).Insert_VazbyAddVyrobky(
                            rowVyrobek.ID_L,
                            ID_L.ToString(),
                            rowVyrobek.ITEMNMBR.Trim(),
                            rowVyrobek.ITEMDESC.Trim(),
                            rowVyrobek.MJ.Trim(),
                            rowMaterial_zbozi.ITEMNMBR.Trim(),
                            rowMaterial_zbozi.ITEMDESC.Trim(),
                            rowMaterial_zbozi.MJ.Trim(),
                            string.IsNullOrEmpty(textBoxKoeficient.Text) ? string.Empty : Decimal.Parse(textBoxKoeficient.Text).ToString(),
                            FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                            DateTime.Now,
                            null,
                            "0");
                        }
                        else
                            throw new Exception("IVazby2_Insert_VazbyAddVyrobky not implementet");
                    }
                    else
                    {



                        if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)
                        {
                            int a = ((Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)providerVazby).Insert_VazbyAddVyrobky(
                                rowVyrobek.ID_L,
                                ID_L.ToString(),
                                    rowVyrobek.ITEMNMBR.Trim(),
                                    rowVyrobek.ITEMDESC.Trim(),
                                    rowVyrobek.MJ.Trim(),
                                    rowMaterial_zbozi.ITEMNMBR.Trim(),
                                    rowMaterial_zbozi.ITEMDESC.Trim(),
                                    rowMaterial_zbozi.MJ.Trim(),
                                    string.IsNullOrEmpty(textBoxKoeficient.Text) ? string.Empty : Decimal.Parse(textBoxKoeficient.Text).ToString(),
                                    FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                                    DateTime.Now,
                                    comboBox_Alter.Text.Trim(),
                                    "0");
                        }
                        else
                            throw new Exception("IVazby2_Insert_VazbyAddVyrobky not implementet");


                    }

                }

                    //Editace vyrobku
                else if (edittype == EditType.Edit)
                {
                    if (string.IsNullOrEmpty(comboBox_Alter.Text))
                    {
                        if (providerVazby is Fask.Interfaces.Vazby.IVazby2_UpdateEdit)
                        {
                           ((Fask.Interfaces.Vazby.IVazby2_UpdateEdit)providerVazby).UpdateEdit(
                                textBoxKoeficient.Text.Trim(),
                               FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                               DateTime.Now,
                               null,
                               rowVyrobek.ITEMNMBR.Trim(),
                               rowMaterial.ITEMNMBR.Trim());
                        }
                        else
                            throw new Exception("IVazby2_UpdateEdit not implementet");

                    
                    }
                    else
                    {

                        if (providerVazby is Fask.Interfaces.Vazby.IVazby2_UpdateEdit)
                        {
                            ((Fask.Interfaces.Vazby.IVazby2_UpdateEdit)providerVazby).UpdateEdit(
                               textBoxKoeficient.Text.Trim(),
                               FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                               DateTime.Now,
                               comboBox_Alter.Text.Trim(),
                               rowVyrobek.ITEMNMBR.Trim(),
                               rowMaterial.ITEMNMBR.Trim());
                        }
                        else
                            throw new Exception("IVazby2_UpdateEdit not implementet");

                  
                    }
                }
                else if (edittype == EditType.AddPolotovar)
                {
                    //if (string.IsNullOrEmpty(comboBox_Alter.Text))
                    //{

                    if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)
                    {
                        int a = ((Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)providerVazby).Insert_VazbyAddVyrobky(
                        rowVyrobek.ID_L.Trim(),
                        rowPolotovar.ID_L.Trim(),
                        rowVyrobek.ITEMNMBR.Trim(),
                        rowVyrobek.ITEMDESC.Trim(),
                        rowVyrobek.MJ.Trim(),
                        rowPolotovar.ITEMNMBR.Trim(),
                        rowPolotovar.ITEMDESC.Trim(),
                        rowPolotovar.MJ.Trim(),
                        textBoxKoeficient.Text.Trim(),
                        FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                        DateTime.Now,
                        null,
                        "0");
                    }
                    else
                        throw new Exception("IVazby2_Insert_VazbyAddVyrobky not implementet");


                }

                this.DialogResult = DialogResult.OK;

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
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(textBoxKoeficient.Text))
                    errorProvider1.SetError(textBoxKoeficient, "Koeficient musí být vyplněn");


                decimal tmp;
                if (!Decimal.TryParse(textBoxKoeficient.Text, out tmp))
                    errorProvider1.SetError(textBoxKoeficient, "Koeficient musí být číslo");

                if (tmp == 0)
                    errorProvider1.SetError(textBoxKoeficient, "Koeficient nesmí být nula");

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return IsAllValid();

        }
        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel1.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        private void FormProductionEdit_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void textBoxKoeficient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                //&& (e.KeyChar != ',')
                && (!NumberFormatInfo.CurrentInfo.NumberDecimalSeparator.Contains(e.KeyChar))
                ) 
            {
                e.Handled = true;
            }
        }

        private void textBoxKoeficient_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxKoeficient.Text, "  ^ [0-9]"))
            {
                textBoxKoeficient.Text = "";
            }
        }

        private void buttonJump_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }

        private void buttonVse_Click(object sender, EventArgs e)
        {


            if( string.IsNullOrEmpty(comboBox_Alter.Text))
                this.Alter = null;
            else
                this.Alter = comboBox_Alter.Text;

            this.VseUlozit = true;
            this.Koeficient = textBoxKoeficient.Text;

            this.DialogResult = DialogResult.Yes;
        }


    }
}
