using Fask.Vyroba_P.ServerAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.Vyroba_P.Forms
{
    public partial class FormPrehledFiltr : Form
    {
        #region Promenne na zobrazeni

        private string _Pracovnik;
        public string Pracovnik 
        {
            set { this._Pracovnik = value; }
            get { return this._Pracovnik; }
        }

        private string _Obdobi;
        public string Obdobi
        {
            set { this._Obdobi = value; }
            get { return this._Obdobi; }
        }

        private string _PocetNormohodin;
        public string PocetNormohodin
        {
            set { this._PocetNormohodin = value; }
            get { return this._PocetNormohodin; }
        }

        private string _korekce_KeSchvaleni;
        public string korekce_KeSchvaleni
        {
            set { this._korekce_KeSchvaleni = value; }
            get { return this._korekce_KeSchvaleni; }
        }

        private string _korekce_NeSchvalene;
        public string korekce_NeSchvalene
        {
            set { this._korekce_NeSchvalene = value; }
            get { return this._korekce_NeSchvalene; }
        }

        private string _korekce_Schvalene;
        public string korekce_Schvalene
        {
            set { this._korekce_Schvalene = value; }
            get { return this._korekce_Schvalene; }
        }
        

        #endregion


        #region Promenne Filtr

        private DateTime? _DatumOd;
        public DateTime? DatumOd
        {
            set { this._DatumOd = value; }
            get { return this._DatumOd; }
        }

        private DateTime? _DatumDo;
        public DateTime? DatumDo
        {
            set { this._DatumDo = value; }
            get { return this._DatumDo; }
        }

        private bool _NedokonceneZakazky;
        public bool NedokonceneZakazky
        {
            set { this._NedokonceneZakazky = value; }
            get { return this._NedokonceneZakazky; }
        }

        private string _Osoba;
        public string Osoba
        {
            set { this._Osoba = value; }
            get { return this._Osoba; }
        }
        
        private string _Stroj;
        public string Stroj
        {
            set { this._Stroj = value; }
            get { return this._Stroj; }
        }

        private string _Zakazka;
        public string Zakazka
        {
            set { this._Zakazka = value; }
            get { return this._Zakazka; }
        }

        #endregion



        public Vyroba_P.WebServiceVyroba.FiltersHistory filter;


        public FormPrehledFiltr()
        {
            InitializeComponent();

            filter = new Fask.Vyroba_P.WebServiceVyroba.FiltersHistory();

            label_Pracovník.Text = "-";
            label_Obdobi.Text = "-";

            label_PocetNormohodin.Text = "-";

            label_korekce_KeSchvaleni.Text = "-";
            label_korekce_NeSchvalene.Text = "-";
            label_korekce_Schvalene.Text = "-";

        }

        private void FormPrehledFiltr_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Location = Settings.ApplicationPosition;
                this.Icon = Properties.Resources.logo_FASK2;
                this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);

                #region Defaultne hodnoty
                //if (this.filter.filtrDatumDo == null) 
                //{
                //    this.filter.filtrDatumDo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, DateTimeKind.Local);
                //}

                //if (this.filter.filtrDatumOd == null) 
                //{
                //    this.filter.filtrDatumOd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1, DateTimeKind.Local);
                //}

                if (this.filter.filtrOsoba == string.Empty)
                {
                    if (Globals._Pracovnik == null)
                        return;


                        this.filter.filtrOsoba = Globals._Pracovnik.id;
                }

                //if(this.filter.filtrZakazka == string.Empty)
                //if(this.filter.filtrStroj == string.Empty)
                //this.filter.filtrNedokonceneZakazky; // bool netrapi nas
                #endregion
            }
            catch (Exception ex) 
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            try
            {
                ReadFromSQL();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }


        }

        private void FormPrehledFiltr_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
       
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
                if (!e.Shift && !e.Control && !e.Alt)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        PerformOK();
                    }
                    if (e.KeyCode == Keys.Escape)
                    {
                        PerformStorno();
                    }
                    else
                        return;
                }
                else
                    return;

                e.Handled = true;
            


        }

        private void PerformStorno()
        {
   
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformStorno();
        }

       private void ReadFromSQL()         
        {
            try
            {
                var x = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Production_Filter(filter);
                if (x.FASK_Filter.Count != 0)
                    Write(x.FASK_Filter[0]);

                #region puvodny spusb nacteni
                //WebServiceVyroba.VyrobaDataSet_StatistikaOdvadeni ds = new Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet_StatistikaOdvadeni();

                //WebServiceVyroba.VyrobaDataSet_StatistikaOdvadeni.FASK_FilterDataTable fdt = ds.FASK_Filter;

                ////ds = vyrobaS.Production_Filter(filter);

                //foreach (Vyroba_P.WebServiceVyroba.VyrobaDataSet_StatistikaOdvadeni.FASK_FilterRow item in ds.FASK_Filter)
                //{
                //    var row = fdt.NewFASK_FilterRow();

                //    if (item.IsDatumOdNull())
                //        row.SetDatumOdNull();
                //    else
                //        row.DatumOd = item.DatumOd;

                //    if (item.IsDatumDoNull())
                //        row.SetDatumDoNull();
                //    else
                //        row.DatumDo = item.DatumDo;

                //    if (item.IsFirstnameNull())
                //        row.SetFirstnameNull();
                //    else
                //        row.Firstname = item.Firstname;

                //    if (item.IsSurnameNull())
                //        row.SetSurnameNull();
                //    else
                //        row.Surname = item.Surname;

                //    if (item.IsPocetNormohodinNull())
                //        row.SetPocetNormohodinNull();
                //    else
                //        row.PocetNormohodin = item.PocetNormohodin;


                //    if (item.Iskorekce_KeSchvaleniNull())
                //        row.Setkorekce_KeSchvaleniNull();
                //    else
                //        row.korekce_KeSchvaleni = item.korekce_KeSchvaleni;

                //    if (item.Iskorekce_NeSchvaleneNull())
                //        row.Setkorekce_NeSchvaleneNull();
                //    else
                //        row.korekce_NeSchvalene = item.korekce_NeSchvalene;

                //    if (item.Iskorekce_SchvaleneNull())
                //        row.Setkorekce_SchvaleneNull();
                //    else
                //        row.korekce_Schvalene = item.korekce_Schvalene;

                //    //row.DatumDo = item.DatumDo;
                //    //row.Firstname = item.Firstname;
                //    //row.Surname = item.Surname;
                //    //row.PocetNormohodin = item.PocetNormohodin;
                //    //row.korekce_KeSchvaleni = item.korekce_KeSchvaleni;
                //    //row.korekce_NeSchvalene = item.korekce_NeSchvalene;
                //    //row.korekce_Schvalene = item.korekce_Schvalene;

                //    fdt.AddFASK_FilterRow(row);
                //}

                //Write(fdt[0]);
                #endregion
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                //sql prikaz sa nevykonal tak to hodi throw heslo nenalezeno nebo se nepripojilo k serveru
                //return false;

            }
            finally
            {
                //if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                //{
                //    sqlConn.Close();
                //}

            }
        }


        private void Write(Fask.Vyroba_P.WebServiceVyroba.VyrobaDataSet_StatistikaOdvadeni.FASK_FilterRow fdt) 
        {
            //fdt.Firstname == null 
            //fdt.Firstname == System.DBNull.Value;
            label_Pracovník.Text = (fdt.IsFirstnameNull() ? string.Empty : fdt.Firstname.Trim()) + " " + (fdt.IsSurnameNull() ? string.Empty : fdt.Surname.Trim());
            label_Obdobi.Text = (fdt.IsDatumOdNull() ? DateTime.MinValue.ToShortDateString() : fdt.DatumOd.ToShortDateString().Trim()) + "-" + (fdt.IsDatumDoNull() ? DateTime.MaxValue.ToShortDateString() : fdt.DatumDo.ToShortDateString().Trim());
            label_PocetNormohodin.Text = fdt.IsPocetNormohodinNull() ? string.Empty : fdt.PocetNormohodin.Trim();
            label_korekce_KeSchvaleni.Text = fdt.Iskorekce_KeSchvaleniNull() ? string.Empty : fdt.korekce_KeSchvaleni.Trim();
            label_korekce_NeSchvalene.Text = fdt.Iskorekce_NeSchvaleneNull() ? string.Empty : fdt.korekce_NeSchvalene.Trim();
            label_korekce_Schvalene.Text = fdt.Iskorekce_SchvaleneNull() ? string.Empty : fdt.korekce_Schvalene.Trim();
        
        }


    }
}
