using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Konzola.Extensions;
using Konzola;
using System.Reflection;
using Fask.Interfaces.Classes;

namespace Konzola.Vyroba.Rozbory
{
    public partial class FormOdvod_EventsList_Storno : Form
    {

        protected Fask.Interfaces.IMES provider = null;

        private Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow _radek;
        public Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow Radek 
        {
            get { return _radek; }
            set { _radek = value; } 
        }

        public FormOdvod_EventsList_Storno()
        {
            InitializeComponent();
        }

        private void FormOdvod_EventsList_Storno_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;

                InitProvider();

                label10.Text = _radek.id.ToString();
                label20.Text = _radek.machineid.Trim();
                label30.Text = _radek.dateeve.ToString();
                label40.Text = _radek.qty.ToString();
                label50.Text = _radek.qtyReal.ToString();
                label60.Text = _radek.IsdescriptionNull() ? "-" : _radek.description.Trim();
                label70.Text = _radek.IsEAN_ISNull() ? "-" : _radek.EAN_IS.Trim();
                label80.Text = _radek.IsITEMDESCNull()? "-" : _radek.ITEMDESC.Trim();
                label90.Text = _radek.IszakazkaNull() ? "-" : _radek.zakazka.Trim();

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
        public void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    provider.InitProvider();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Perform_Ne()
        {
            try
            {
                DialogResult = DialogResult.No;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Perform_Ano()
        {
            try
            {

                int? statusek = -1;

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent_OnlineCheck))
                {
                    statusek = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent_OnlineCheck)provider).StornoEvent_OnlineCheck(_radek.faskGUID);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_Events_StornoEvent");
                }


                if (statusek != 0 && statusek != 900)
                {
                    MessageBox.Show("Stornovat lze pouze se statusem 0 nebo 900", this.Text, MessageBoxButtons.OK);
                    return;
                }


                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
                var row = dt.NewFASK_EventsRow();

                //row.id = _radek.id;

                row.loginid = _radek.loginid;
                row.machineid = _radek.machineid;
                row.dateeve = _radek.dateeve;
                row.qty = -_radek.qty;
                row.qtyReal = -_radek.qtyReal;

                //if (_radek.IsdescriptionNull())
                //    row.SetdescriptionNull();
                //else
                //    row.description = string.IsNullOrEmpty(_radek.description) ? "" : _radek.description.Trim();

                row.description = string.IsNullOrEmpty(_radek.description) ? "" : "S: " + _radek.description.Trim();

                row.barcodeReaded = string.IsNullOrEmpty(_radek.barcodeReaded) ? "" : _radek.barcodeReaded.Trim();
                row.barcodeSended = string.IsNullOrEmpty(_radek.barcodeSended) ? "" : _radek.barcodeSended.Trim();

                if (_radek.IszakazkaNull())
                    row.SetzakazkaNull();
                else
                    row.zakazka = string.IsNullOrEmpty(_radek.zakazka) ? "" : _radek.zakazka.Trim();

                if (_radek.IspopisNull())
                    row.SetpopisNull();
                else
                    row.popis = string.IsNullOrEmpty(_radek.popis) ? "" : _radek.popis.Trim();

                row.faskGUID = _radek.faskGUID;
                row.reportType = string.IsNullOrEmpty(_radek.reportType) ? "" : _radek.reportType.Trim();


                //if (_radek.IsisProcessedNull())
                //    row.SetisProcessedNull();
                //else
                //    row.isProcessed = _radek.isProcessed;

                //if (_radek.IsIDONull())
                //    row.SetIDONull();
                //else
                //    row.IDO = string.IsNullOrEmpty(_radek.IDO) ? "" : _radek.IDO.Trim();
                row.IDO = "SP" + _radek.id.ToString();

                if (_radek.Isscan1Null())
                    row.Isscan1Null();
                else
                    row.scan1 = string.IsNullOrEmpty(_radek.scan1) ? "" : _radek.scan1.Trim();

                if (_radek.Isscan2Null())
                    row.Setscan2Null();
                else
                    row.scan2 = string.IsNullOrEmpty(_radek.scan2) ? "" : _radek.scan2.Trim();

                if (_radek.Isscan3Null())
                    row.Setscan3Null();
                else
                    row.scan3 = string.IsNullOrEmpty(_radek.scan3) ? "" : _radek.scan3.Trim();

                if (_radek.IssensorNull())
                    row.SetsensorNull();
                else
                    row.sensor = string.IsNullOrEmpty(_radek.sensor) ? "" : _radek.sensor.Trim();

                if (_radek.IsmaterialNull())
                    row.SetmaterialNull();
                else
                    row.material = string.IsNullOrEmpty(_radek.material) ? "" : _radek.material.Trim();

                if (_radek.IsVPHNull())
                    row.SetVPHNull();
                else
                    row.VPH = string.IsNullOrEmpty(_radek.VPH) ? "" : _radek.VPH.Trim();

                if (_radek.IsVPPolNull())
                    row.SetVPPolNull();
                else
                    row.VPPol = _radek.VPPol;

                if (_radek.IsEAN_ISNull())
                    row.SetEAN_ISNull();
                else
                    row.EAN_IS = string.IsNullOrEmpty(_radek.EAN_IS) ? "" : _radek.EAN_IS.Trim();

                if (_radek.IsIS_IDNull())
                    row.SetIS_IDNull();
                else
                    row.IS_ID = string.IsNullOrEmpty(_radek.IS_ID) ? "" : _radek.IS_ID.Trim();

                if (_radek.IsNMBRPALNull())
                    row.SetNMBRPALNull();
                else
                    row.NMBRPAL = string.IsNullOrEmpty(_radek.NMBRPAL) ? "" : _radek.NMBRPAL.Trim();

                //if (_radek.IsstatusNull())
                //    row.SetstatusNull();
                //else
                //    row.status = _radek.status;

                row.status = 600;

                row.SetproductionGuidNull();// = _radek.productionGuid;

                if (_radek.IsQTYPACKNull())
                    row.SetQTYPACKNull();
                else
                    row.QTYPACK = _radek.QTYPACK;

                if (_radek.IsPackTypeNull())
                    row.SetPackTypeNull();
                else
                    row.PackType = string.IsNullOrEmpty(_radek.PackType) ? "" : _radek.PackType.Trim();


                if (_radek.IsWEIGHTNull())
                    row.SetWEIGHTNull();
                else
                    row.WEIGHT = _radek.WEIGHT;



                bool state = false;

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent))
                {
                    dt.AddFASK_EventsRow(row);
                    state = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent)provider).StornoEvent(row);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_Events_StornoEvent");
                }

                if (!state)
                    MessageBox.Show("Data nebyly stornována", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Yes;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void panelButton_Resize(object sender, EventArgs e)
        {
            try
            {
                int w = panelButton.Width / 2;
                int h = panelButton.Width;
                btn_Ano.Size = new Size(w, h);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btn_Ano_Click(object sender, EventArgs e)
        {
            Perform_Ano();
        }

        private void btn_Ne_Click(object sender, EventArgs e)
        {
            Perform_Ne();
        }

    }
}