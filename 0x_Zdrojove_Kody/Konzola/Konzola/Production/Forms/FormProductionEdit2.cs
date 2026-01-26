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

namespace Production.Forms
{
    public partial class FormProductionEdit2 : Form
    {
        private PRODUCTIONTYPE _producttype;
        public enum PRODUCTIONTYPE
        {
            ODVOD_START,
            ODVOD_STOP,
            KOREKCE_NEVAZANA_START,
            KOREKCE_NEVAZANA_STOP,
            KOREKCE_VAZANA_START,
            KOREKCE_VAZANA_STOP
        }

        /// <summary>
        /// záznam, který se bude upravovat
        /// </summary>
        public Production.DataServices.VyrobaDataSet.ProductionRow rowProduct { get; set; }
        /// <summary>
        /// Datum bylo upraveno podle najiteho paroveho zaznamu
        /// </summary>
        private bool dateedit;
        public FormProductionEdit2()
        {
            InitializeComponent();
        }

        private void FormProductionEdit_Load(object sender, EventArgs e)
        {
            try 
	        {	
                // zjištění o jaký typ záznamu se jedná
                // jedná se o odvod
                if (!rowProduct.IsTIMESTARTNull())
                {
                    _producttype = PRODUCTIONTYPE.ODVOD_START;
                    if (!rowProduct.IsTIMESTOPNull())
                    {
                        _producttype = PRODUCTIONTYPE.ODVOD_STOP;
                    }
                }
                else // jedná se o korekci
                {   
                    // start korekce vazane
                    if (!rowProduct.IsTIMECORSTARTNull() && !rowProduct.IsCountEntriesNull())
                    {
                        _producttype = PRODUCTIONTYPE.KOREKCE_VAZANA_START;
                        // stop korekce vazane
                        if (!rowProduct.IsTIMECORSTOPNull())
                        {
                            _producttype = PRODUCTIONTYPE.KOREKCE_VAZANA_STOP;
                        }
                    }
                    else
                    {
                        // start nevazane korekce
                        if (!rowProduct.IsTIMECORSTARTNull())
                        {
                            _producttype = PRODUCTIONTYPE.KOREKCE_NEVAZANA_START;
                            if (!rowProduct.IsTIMECORSTOPNull())
                            {
                                _producttype = PRODUCTIONTYPE.KOREKCE_NEVAZANA_STOP;
                            }
                        }
                    }

                }

                LoadProductionRow();
        
                //textBoxId.Text = Globals.Pracovnik.id.Trim();
                //textBoxJmeno.Text = Globals.Pracovnik.firstname + " " + Globals.Pracovnik.surname;
                //textBoxMnozstviStare.Text = rowProduct.qty.ToString();
                FormProductionEdit_Resize(null, null);
            }
	        catch (Exception ex)
	        {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);                
	        }
        }

        /// <summary>
        /// Naplneni textboxu a datetimepickeru.
        /// </summary>
        private void LoadProductionRow()
        {
            // načtení dat
            textBoxCountEntries.Text = rowProduct.IsCountEntriesNull() ? string.Empty : rowProduct.CountEntries.ToString();
            textBoxSOPNUMBE.Text = rowProduct.IsSOPNUMBENull() ? string.Empty : rowProduct.SOPNUMBE.Trim();
            textBoxITEMNMBR.Text = rowProduct.IsITEMNMBRNull() ? string.Empty : rowProduct.ITEMNMBR.Trim();
            textBoxITEMTYPE.Text = rowProduct.IsITEMTYPENull() ? string.Empty : rowProduct.ITEMTYPE;
            textBoxITEMMJ.Text = rowProduct.IsITEMMJNull() ? string.Empty : rowProduct.ITEMMJ.Trim();
            textBoxORD.Text = rowProduct.IsORDNull() ? string.Empty : rowProduct.ORD.ToString();
            textBoxTIMEMODE.Text = rowProduct.IsTIMEMODENull() ? string.Empty : rowProduct.TIMEMODE.ToString();
            dateTimePickerTIMEPREPSTART.Value = rowProduct.IsTIMEPREPSTARTNull() ? DateTime.Now : rowProduct.TIMEPREPSTART;
            dateTimePickerTIMEPREPSTOP.Value = rowProduct.IsTIMEPREPSTOPNull() ? DateTime.Now : rowProduct.TIMEPREPSTOP;
            textBoxTIMEPREP.Text = rowProduct.IsTIMEPREPNull() ? string.Empty : rowProduct.TIMEPREP.ToString();
            textBoxTIMEUNIT.Text = rowProduct.IsTIMEUNITNull() ? string.Empty : rowProduct.TIMEUNIT.ToString();
            dateTimePickerTIMESTART.Value = rowProduct.IsTIMESTARTNull() ? DateTime.Now : rowProduct.TIMESTART;
            dateTimePickerTIMESTOP.Value = rowProduct.IsTIMESTOPNull() ? DateTime.Now : rowProduct.TIMESTOP;
            dateTimePickerTIMECORSTART.Value = rowProduct.IsTIMECORSTARTNull() ? DateTime.Now : rowProduct.TIMECORSTART;
            dateTimePickerTIMECORSTOP.Value = rowProduct.IsTIMECORSTOPNull() ? DateTime.Now : rowProduct.TIMECORSTOP;
            textBoxTIMECOR.Text = rowProduct.IsTIMECORSTOPNull() ? string.Empty : rowProduct.TIMECOR.ToString();
            textBoxTIMECRID.Text = rowProduct.IsTIMECRIDNull() ? string.Empty : rowProduct.TIMECRID.ToString();
            textBoxId.Text = rowProduct.id.ToString();
            textBoxId.Enabled = false;
            textBoxLoginid.Text = rowProduct.loginid.Trim();
            textBoxMachineid.Text = rowProduct.IsmachineidNull() ? string.Empty : rowProduct.machineid.Trim();
            textBoxOperationid.Text = rowProduct.IsoperationidNull() ? string.Empty : rowProduct.operationid.Trim();
            dateTimePickerDateeve.Value = rowProduct.dateeve;
            dateTimePickerDateeve.Enabled = false;
            textBoxQty.Text = rowProduct.qty.ToString();
            textBoxQtyReal.Text = rowProduct.qtyReal.ToString();
            textBoxQTYPACK.Text = rowProduct.IsQTYPACKNull() ? string.Empty : rowProduct.QTYPACK.ToString();
            textBoxQTYPACKMJ.Text = rowProduct.IsQTYPACKMJNull() ? string.Empty : rowProduct.QTYPACKMJ.Trim();
            textBoxDescription.Text = rowProduct.IsdescriptionNull() ? string.Empty : rowProduct.description.Trim();
            textBoxBarcodeP.Text = rowProduct.IsBarcodePNull() ? string.Empty : rowProduct.BarcodeP.Trim();
            textBoxUserID.Text = rowProduct.UserID.Trim();
            textBoxTermID.Text = rowProduct.TermID.ToString();
            dateTimePickerISOK.Value = rowProduct.IsISOKNull() ? DateTime.Now : rowProduct.ISOK;
            textBoxGUID.Text = rowProduct.GUID.ToString();
            textBoxGUID.Enabled = false;
            textBoxSOUBEHGUID.Text = rowProduct.IsSOUBEHGUIDNull() ? string.Empty : rowProduct.SOUBEHGUID.ToString();
            textBoxSOUBEHGUID.Enabled = false;
            textBoxCORRGUID.Text = rowProduct.IsCORRGUIDNull() ? string.Empty : rowProduct.CORRGUID.ToString();
            textBoxCORRGUID.Enabled = false;
            textBoxQtyOld.Text = rowProduct.IsqtyOldNull() ? string.Empty : rowProduct.qtyOld.ToString();
            textBoxidVS.Text = rowProduct.IsidVSNull() ? string.Empty : rowProduct.idVS.Trim();
            textBoxidVS.Enabled = false;
            dateTimePickerDateedit.Value = rowProduct.IsdateeditNull() ? DateTime.Now : rowProduct.dateedit;

            // vypnutí tlačítek
            textBoxCountEntries.Enabled = !rowProduct.IsCountEntriesNull();
            textBoxSOPNUMBE.Enabled = !rowProduct.IsSOPNUMBENull();
            textBoxITEMNMBR.Enabled = !rowProduct.IsITEMNMBRNull();
            textBoxITEMTYPE.Enabled = !rowProduct.IsITEMTYPENull();
            textBoxITEMMJ.Enabled = !rowProduct.IsITEMMJNull();
            textBoxORD.Enabled = !rowProduct.IsORDNull();
            textBoxTIMEMODE.Enabled = !rowProduct.IsTIMEMODENull();
            dateTimePickerTIMEPREPSTART.Enabled = !rowProduct.IsTIMEPREPSTARTNull();
            dateTimePickerTIMEPREPSTOP.Enabled = !rowProduct.IsTIMEPREPSTOPNull();
            textBoxTIMEPREP.Enabled = !rowProduct.IsTIMEPREPNull();
            textBoxTIMEUNIT.Enabled = !rowProduct.IsTIMEUNITNull();
            dateTimePickerTIMESTART.Enabled = !rowProduct.IsTIMESTARTNull();
            dateTimePickerTIMESTOP.Enabled = !rowProduct.IsTIMESTOPNull();
            dateTimePickerTIMECORSTART.Enabled = !rowProduct.IsTIMECORSTARTNull();
            dateTimePickerTIMECORSTOP.Enabled = !rowProduct.IsTIMECORSTOPNull();
            // nastaveno vždy na false, při potvrzení se dopočítá
            textBoxTIMECOR.Enabled = false;
            //textBoxTIMECOR.Enabled = !rowProduct.IsTIMECORSTOPNull();
            textBoxTIMECRID.Enabled = !rowProduct.IsTIMECRIDNull();
            //textBoxId.Text = rowProduct.id.ToString();
            //textBoxId.Enabled = false;
            //textBoxLoginid.Text = rowProduct.loginid.Trim();
            textBoxMachineid.Enabled = !rowProduct.IsmachineidNull();
            textBoxOperationid.Enabled = !rowProduct.IsoperationidNull();
            //dateTimePickerDateeve.Value = rowProduct.dateeve;
            //textBoxQty.Text = rowProduct.qty.ToString();
            //textBoxQtyReal.Text = rowProduct.qtyReal.ToString();
            textBoxQTYPACK.Enabled = !rowProduct.IsQTYPACKNull();
            textBoxQTYPACKMJ.Enabled = !rowProduct.IsQTYPACKMJNull();
            textBoxDescription.Enabled = !rowProduct.IsdescriptionNull();
            textBoxBarcodeP.Enabled = !rowProduct.IsBarcodePNull();
            //textBoxUserID.Text = rowProduct.UserID.Trim();
            //textBoxTermID.Text = rowProduct.TermID.ToString();
            dateTimePickerISOK.Enabled = !rowProduct.IsISOKNull();
            //textBoxGUID.Text = rowProduct.GUID.ToString();
            //textBoxGUID.Enabled = false;
            //textBoxSOUBEHGUID.Text = rowProduct.IsSOUBEHGUIDNull() ? string.Empty : rowProduct.SOUBEHGUID.ToString();
            //textBoxSOUBEHGUID.Enabled = false;
            //textBoxCORRGUID.Text = rowProduct.IsCORRGUIDNull() ? string.Empty : rowProduct.CORRGUID.ToString();
            //textBoxCORRGUID.Enabled = false;
            textBoxQtyOld.Enabled = false;
            //textBoxidVS.Enabled = rowProduct.IsidVSNull() ? string.Empty : rowProduct.idVS.Trim();
            //textBoxidVS.Enabled = false;
            dateTimePickerDateedit.Enabled = false;

            // najití datumu STOP
            FindTimeStop();
            //// odvadeni
            //if ((_producttype == PRODUCTIONTYPE.ODVOD_START) || (_producttype == PRODUCTIONTYPE.ODVOD_STOP))
            //{

            //}

        }

        private void FormProductionEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
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
                Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter taProduction = new DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                taProduction.Connection.ConnectionString = Globals.ConnectionString;
                // warning nalezeno vice zakazek v soubehu, chcete upravit cas pouze u teto, nebo u vsech? (zobrazit warning)
                //List<Production.DataServices.VyrobaDataSet.ProductionRow> resSoubeh = new List<DataServices.VyrobaDataSet.ProductionRow>();
                Production.DataServices.VyrobaDataSet dsV = new DataServices.VyrobaDataSet();
                // radky, ktere patri k sobe
                List<Production.DataServices.VyrobaDataSet.ProductionRow> prRows = new List<DataServices.VyrobaDataSet.ProductionRow>();
                // zakazka v soubehu
                if (!rowProduct.IsSOUBEHGUIDNull())
                {
                    //resSoubeh.AddRange(taProduction.GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID));
                    taProduction.FillBySOUBEHGUID(dsV.Production, rowProduct.SOUBEHGUID);
                }
                else
                {
                    // korekce
                    if (!rowProduct.IsCORRGUIDNull())
                    {
                        //resSoubeh.AddRange(taProduction.GetDataBySOUBEHGUID(rowProduct.CORRGUID));
                        taProduction.FillByCORRGUID(dsV.Production, rowProduct.CORRGUID);
                        prRows.AddRange(dsV.Production);
                    }
                    else
                    {
                        // samotny radek (nemelo by sem dojit)
                        //resSoubeh.Add(rowProduct);
                        dsV.Production.ImportRow(rowProduct);
                        prRows.Add(rowProduct);
                    }
                }


                bool upravitCasUJedneZakazky = false;
                // kontrola TIMESTART nebo TIMESTOP, jestli byl zadan a zmenen
                if((_producttype == PRODUCTIONTYPE.ODVOD_START) || (_producttype == PRODUCTIONTYPE.ODVOD_STOP))
                {
                    // vice zakazek
                    if ((!rowProduct.IsTIMESTOPNull() && (rowProduct.TIMESTOP != dateTimePickerTIMESTOP.Value)) ||
                        (!rowProduct.IsTIMESTARTNull() && (rowProduct.TIMESTART != dateTimePickerTIMESTART.Value)))
                    {
                        var zakazky = dsV.Production.Where(x => !x.IsTIMESTOPNull());
                        if (zakazky.Count() > 1)
                        {
                            DialogResult dr = MessageBox.Show("Nalezeno více zakázek v souběhu, chcete upravit čas pouze u této zakázky?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                upravitCasUJedneZakazky = true;
                            }
                            else if (dr == DialogResult.No)
                            {
                                upravitCasUJedneZakazky = false;
                            }
                            else return;
                        }
                    }

                    //// kontrola TIMESTOP, jestli byl zadan a zmenen
                    //if (!rowProduct.IsTIMESTOPNull() && (rowProduct.TIMESTOP != dateTimePickerTIMESTOP.Value))
                    //{                        
                    //    // nalezeno vice zakazek 
                    //    //if(dsV.Production.Count > 2)
                    //    //{
                    //    //    if (DialogResult.Yes == MessageBox.Show("Nalezeno více zakázek v souběhu, chcete upravit čas pouze u této zakázky?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    //    //    {                                
                    //    //        upravitCasUJedneZakazky = true;
                    //    //    }
                    //    //    else 
                    //    //        upravitCasUJedneZakazky = false;
                    //    //}
                    //}
                    //else
                    //{
                    //    // kontrola, zdali byl zmenen timestart
                    //    if (!rowProduct.IsTIMESTARTNull() && (rowProduct.TIMESTART != dateTimePickerTIMESTART.Value))
                    //    {
                    //        //if (dsV.Production.Count > 2)
                    //        //{
                    //        //    if (DialogResult.Yes == MessageBox.Show("Nalezeno více zakázek v souběhu, chcete upravit čas pouze u této zakázky?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    //        //    {
                    //        //        upravitCasUJedneZakazky = true;
                    //        //    }
                    //        //    else 
                    //        //        upravitCasUJedneZakazky = false;
                    //        //}
                    //    }
                    //}
                }

                
                //prRows.Add(rowProduct);
                //prRows.Contains(rowProduct);
                if ((_producttype == PRODUCTIONTYPE.ODVOD_START) || (_producttype == PRODUCTIONTYPE.ODVOD_STOP))
                {
                    // nadop
                    if (rowProduct.IsITEMNMBRNull())
                    {
                        prRows.AddRange(dsV.Production.Where(x => (x.CountEntries == rowProduct.CountEntries) && (x.SOPNUMBE == rowProduct.SOPNUMBE)).ToList());
                    }
                    else  // zbytek
                    {
                        prRows.AddRange(dsV.Production.Where(x => (x.CountEntries == rowProduct.CountEntries) && (x.SOPNUMBE == rowProduct.SOPNUMBE) && (x.ITEMNMBR == rowProduct.ITEMNMBR)).ToList());
                    }
                    //dsV.Production.Where(
                }

                // datum editace
                DateTime dtNow = DateTime.Now;
                foreach (var item in dsV.Production)
                {
                    item.idVS = Globals.Pracovnik != null ? Globals.Pracovnik.id : string.Empty;
                    item.dateedit = dtNow;
                    item.UserID = textBoxUserID.Text.Trim();
                    item.TermID = Convert.ToByte(textBoxTermID.Text);
                    item.loginid = textBoxLoginid.Text.Trim();
                    if (!item.IsmachineidNull() && textBoxMachineid.Enabled)
                        item.machineid = textBoxMachineid.Text.Trim();
                    if (!item.IsoperationidNull() && textBoxOperationid.Enabled)
                        item.operationid = textBoxOperationid.Text.Trim();
                    // uprava aktualniho zaznamu
                    //if (rowProduct.id == item.id)
                    // uprava souvisejicich zaznamu (napr. soucasne korekce start/stop, pripadne zakazka start/stop, pripadne nalezena zakazka v soubehu a jeji ekvivalent)
                    if(prRows.Contains(item))
                    {
                        // porovnani, jestli dany sloupec je null a soucasne zapla jeho komponenta (textbox a pod.)
                        // kvuli parovym udajum, aby se napr. pri editaci zaznamu pouze s TIMESTOP needitovat cas u zaznamu s TIMESTART
                        if (!item.IsCountEntriesNull() && textBoxCountEntries.Enabled)
                            item.CountEntries = Convert.ToInt32(textBoxCountEntries.Text);
                        if (!item.IsSOPNUMBENull() && textBoxSOPNUMBE.Enabled)
                            item.SOPNUMBE = textBoxSOPNUMBE.Text.Trim();
                        if (!item.IsITEMNMBRNull() && textBoxITEMNMBR.Enabled)
                            item.ITEMNMBR = textBoxITEMNMBR.Text.Trim();
                        if (!item.IsITEMTYPENull() && textBoxITEMTYPE.Enabled)
                            item.ITEMTYPE = textBoxITEMTYPE.Text.Trim();
                        if (!item.IsITEMMJNull() && textBoxITEMMJ.Enabled)
                            item.ITEMMJ = textBoxITEMMJ.Text.Trim();
                        if (!item.IsORDNull() && textBoxORD.Enabled)
                            item.ORD = Convert.ToInt32(textBoxORD.Text);
                        if (!item.IsTIMEMODENull() && textBoxTIMEMODE.Enabled)
                            item.TIMEMODE = Convert.ToInt32(textBoxTIMEMODE.Text);
                        if (!item.IsTIMEPREPSTARTNull() && dateTimePickerTIMEPREPSTART.Enabled)
                            item.TIMEPREPSTART = dateTimePickerTIMEPREPSTART.Value;
                        if (!item.IsTIMEPREPSTOPNull() && dateTimePickerTIMEPREPSTOP.Enabled)
                            item.TIMEPREPSTOP = dateTimePickerTIMEPREPSTOP.Value;
                        if (!item.IsTIMEPREPNull() && textBoxTIMEPREP.Enabled)
                            item.TIMEPREP = float.Parse(textBoxTIMEPREP.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                        if (!item.IsTIMEUNITNull() && textBoxTIMEUNIT.Enabled)
                            item.TIMEUNIT = float.Parse(textBoxTIMEUNIT.Text.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                        if (!item.IsTIMESTARTNull() && dateTimePickerTIMESTART.Enabled)
                            item.TIMESTART = dateTimePickerTIMESTART.Value;
                        if (!item.IsTIMESTOPNull() && dateTimePickerTIMESTOP.Enabled)
                            item.TIMESTOP = dateTimePickerTIMESTOP.Value;
                        if (!item.IsTIMECORSTARTNull() && dateTimePickerTIMECORSTART.Enabled)
                            item.TIMECORSTART = dateTimePickerTIMECORSTART.Value;
                        if (!item.IsTIMECORSTOPNull() && dateTimePickerTIMECORSTOP.Enabled)
                            item.TIMECORSTOP = dateTimePickerTIMECORSTOP.Value;
                        // korekce ukoncena
                        if (!item.IsTIMECORNull()) // && textBoxTIMECOR.Enabled)
                        {
                            // bylo mozne zmenit upravit casy
                            if (rowProduct.id == item.id)
                            {
                                item.TIMECOR = (float)(dateTimePickerTIMECORSTOP.Value - dateTimePickerTIMECORSTART.Value).TotalMinutes;
                            }
                            else
                            {
                                // nebylo mozne upravit stop cas
                                item.TIMECOR = (float)(item.TIMECORSTOP - dateTimePickerTIMECORSTART.Value).TotalMinutes;
                            }
                        }

                        if (!item.IsTIMECRIDNull() && textBoxTIMECRID.Enabled)
                            item.TIMECRID = Convert.ToInt32(textBoxTIMECRID.Text);
                        // id neupravovat                                                
                        // dateeve neupravovat
                        // pokud vybrany zaznam je ukonceni odvodu, nastavi se zvolene mnozstvi, jinak 0
                        item.qty = ((item.id == rowProduct.id) && (_producttype == PRODUCTIONTYPE.ODVOD_STOP)) ? Convert.ToDecimal(textBoxQty.Text) : 0;
                        item.qtyReal = ((item.id == rowProduct.id) && (_producttype == PRODUCTIONTYPE.ODVOD_STOP)) ? Convert.ToDecimal(textBoxQtyReal.Text) : 0;
                        if (!item.IsQTYPACKNull() && textBoxQTYPACK.Enabled)
                            item.QTYPACK = Convert.ToDecimal(textBoxQTYPACK.Text);
                        if (!item.IsQTYPACKMJNull() && textBoxQTYPACKMJ.Enabled)
                            item.QTYPACKMJ = textBoxQTYPACKMJ.Text.Trim();
                        if (!item.IsdescriptionNull() && textBoxDescription.Enabled)
                            item.description = textBoxDescription.Text.Trim();
                        if (!item.IsBarcodePNull() && textBoxBarcodeP.Enabled)
                            item.BarcodeP = textBoxBarcodeP.Text.Trim();                        
                        if (!item.IsISOKNull() && dateTimePickerISOK.Enabled)
                            item.ISOK = dateTimePickerISOK.Value;
                        // GUID
                        // SOUBEHGUID
                        // CORRGUID
                        // porovnani qtyOld oproti predchozimu stavu
                        if ((_producttype == PRODUCTIONTYPE.ODVOD_STOP) && (item.IsqtyOldNull() && (item.id == rowProduct.id)))
                            item.qtyOld = Convert.ToDecimal(textBoxQty.Text);
                        //if(!rowProduct.IsqtyOldNull() && (rowProduct.qtyOld != Convert.ToDecimal(textBoxQtyOld.Text)))
                        //{
                        //    // TODO: vypnout editaci puvodniho mnozstvi a datum editace
                        //    item.qtyOld = Convert.ToDecimal(textBoxQtyOld.Text);
                        //}                        
                    }
                    else  // uprava jineho zaznamu, upravuji se pouze casy
                    {
                        if (!item.IsTIMEPREPSTARTNull() && dateTimePickerTIMEPREPSTART.Enabled)
                            item.TIMEPREPSTART = dateTimePickerTIMEPREPSTART.Value;
                        if (!item.IsTIMEPREPSTOPNull() && dateTimePickerTIMEPREPSTOP.Enabled)
                            item.TIMEPREPSTOP = dateTimePickerTIMEPREPSTOP.Value;
                        if (!item.IsTIMECORSTARTNull() && dateTimePickerTIMECORSTART.Enabled)
                            item.TIMECORSTART = dateTimePickerTIMECORSTART.Value;
                        if (!item.IsTIMECORSTOPNull() && dateTimePickerTIMECORSTOP.Enabled)
                            item.TIMECORSTOP = dateTimePickerTIMECORSTOP.Value;
                        if (!upravitCasUJedneZakazky)
                        {
                            if (!item.IsTIMESTARTNull() && dateTimePickerTIMESTART.Enabled)
                                item.TIMESTART = dateTimePickerTIMESTART.Value;
                            if (!item.IsTIMESTOPNull() && dateTimePickerTIMESTOP.Enabled)
                                item.TIMESTOP = dateTimePickerTIMESTOP.Value;
                        }
                        // ma se upravit cas u vsech zakazek
                        //if (!upravitCasUJedneZakazky)
                        //{
                        //    if (!item.IsTIMESTARTNull())
                        //        item.TIMESTART = dateTimePickerTIMESTART.Value;
                        //    if (!item.IsTIMESTOPNull())
                        //        item.TIMESTOP = dateTimePickerTIMESTOP.Value;
                        //}
                        //else // uprava casu pouze u jedne zakazky ... hleda se parova
                        //{
                        //    if ((item.CountEntries == rowProduct.CountEntries) && (item.SOPNUMBE == rowProduct.SOPNUMBE))
                        //    {
                        //        if (!item.IsTIMESTARTNull())
                        //            item.TIMESTART = dateTimePickerTIMESTART.Value;
                        //        if (!item.IsTIMESTOPNull())
                        //            item.TIMESTOP = dateTimePickerTIMESTOP.Value;
                        //    }
                        //}

                    }                   
                }
                
                taProduction.Update(dsV.Production);
                dsV.AcceptChanges();
                

                //rowProduct.qtyOld = rowProduct.qty;
                ////rowProduct.qty = Convert.ToDecimal(textBoxMnozstviNove.Text);
                
                //rowProduct.dateedit = DateTime.Now;
                //rowProduct.idVS = Globals.Pracovnik.id;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();
                // naplnění daty pro kontrolu
                Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter taLogins = new DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
                taLogins.Connection.ConnectionString = Globals.ConnectionString;
                Production.DataServices.VyrobaDataSet dsV = new DataServices.VyrobaDataSet();
                
                //taLogins.Fill(ds.Logins);

                Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter taVPH = new DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                taVPH.Connection.ConnectionString = Globals.ConnectionString;
                //taVPH.Fill(ds.CZPRO_VPH);

                Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter taVPP = new DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
                taVPP.Connection.ConnectionString = Globals.ConnectionString;
                //taVPP.Fill(ds.CZPRO_VPP);                

                Production.DataServices.VyrobaDataSetTableAdapters.MachinesTableAdapter taMachines = new DataServices.VyrobaDataSetTableAdapters.MachinesTableAdapter();
                taMachines.Connection.ConnectionString = Globals.ConnectionString;
                //taMachines.Fill(ds.Machines);

                Production.DataServices.VyrobaDataSetTableAdapters.OperationsTableAdapter taOperations = new DataServices.VyrobaDataSetTableAdapters.OperationsTableAdapter();
                taOperations.Connection.ConnectionString = Globals.ConnectionString;
                //taOperations.Fill(ds.Operations);

                Production.DataServices.VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter tavMachinesOperations = new DataServices.VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter();
                tavMachinesOperations.Connection.ConnectionString = Globals.ConnectionString;                

                Production.DataServices.VyrobaDataSetTableAdapters.CorrectsTableAdapter taCorrects = new DataServices.VyrobaDataSetTableAdapters.CorrectsTableAdapter();
                taCorrects.Connection.ConnectionString = Globals.ConnectionString;

                bool status;
                int _countEntries = 0;
                // validace COUNTENTRIES, enabled se nemusi zatim kontrolovat
                if (!rowProduct.IsCountEntriesNull())
                {
                    if (string.IsNullOrEmpty(textBoxCountEntries.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxCountEntries, "Číslo dávky musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        status = Int32.TryParse(textBoxCountEntries.Text, out _countEntries);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxCountEntries, "Číslo dávky musí být číslo");
                        }
                    }
                }

                // validace SOPNUMBE
                if (!rowProduct.IsSOPNUMBENull())
                {
                    if (string.IsNullOrEmpty(textBoxSOPNUMBE.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxSOPNUMBE, "Číslo výrobní zakázky musí být vyplněno");
                    }
                }

                // kontrola, zdali existuje zaznam v VPH
                if (!rowProduct.IsCountEntriesNull() && !rowProduct.IsSOPNUMBENull())
                {
                    if(string.IsNullOrEmpty(errorProvider1.GetError(textBoxCountEntries)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxSOPNUMBE)))
                    {
                        var _validCountEntriesSOPNUMBE = taVPH.GetDataByCountEntriesSOPNUMBE(Convert.ToInt32(textBoxCountEntries.Text), textBoxSOPNUMBE.Text);
                        if (_validCountEntriesSOPNUMBE.Count() == 0)
                        {
                            errorProvider1.SetError(textBoxSOPNUMBE, "Výrobní příkaz s číslem dávky: " + textBoxCountEntries.Text.Trim() + " a číslem výrobní zakázky: " + textBoxSOPNUMBE.Text.Trim() + " nebyl nalezen");
                            errorProvider1.SetError(textBoxCountEntries, "Výrobní příkaz s číslem dávky: " + textBoxCountEntries.Text.Trim() + " a číslem výrobní zakázky: " + textBoxSOPNUMBE.Text.Trim() + " nebyl nalezen");
                            //throw new Exception("Výrobní příkaz s číslem dávky: " + textBoxCountEntries.Text.Trim() + " a číslem výrobní zakázky: " + textBoxSOPNUMBE.Text.Trim() + " nebyl nalezen");
                        }
                    }
                }

                // kontrola, zdali existuje zaznam v VPP
                if (!rowProduct.IsITEMNMBRNull())
                {
                    if (string.IsNullOrEmpty(textBoxITEMNMBR.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxITEMNMBR, "Číslo položky musí být vyplněno");
                    }
                    else
                    {
                        if(string.IsNullOrEmpty(errorProvider1.GetError(textBoxCountEntries)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxSOPNUMBE)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxITEMNMBR)))
                        {

                            //taVPP.FillByCountEntriesSOPNUMBEITEMNMBR(ds.CZPRO_VPP, Convert.ToInt32(textBoxCountEntries.Text), textBoxSOPNUMBE.Text.Trim(), textBoxITEMNMBR.Text.Trim());
                            var _validCountEntriesSOPNUMBEITEMNMBR = taVPP.GetDataByCountEntriesSOPNUMBEITEMNMBR(Convert.ToInt32(textBoxCountEntries.Text), textBoxSOPNUMBE.Text.Trim(), textBoxITEMNMBR.Text.Trim());
                            if (_validCountEntriesSOPNUMBEITEMNMBR.Count() == 0)
                            {
                                errorProvider1.SetError(textBoxITEMNMBR, "Položka s číslem: " + textBoxITEMNMBR.Text.Trim() + " nebyla nalezena");
                            }
                        }
                    }
                }

                if (!rowProduct.IsORDNull())
                {
                    if (string.IsNullOrEmpty(textBoxORD.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxORD, "Pořadí položky musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        int _ORD;
                        status = Int32.TryParse(textBoxORD.Text, out _ORD);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxORD, "Pořadí položky musí být číslo");
                        }
                        else
                        {
                            if (_ORD < 0)
                            {
                                errorProvider1.SetError(textBoxORD, "Pořadí položky musí být větší než nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola timemode
                if (!rowProduct.IsTIMEMODENull())
                {
                    if (string.IsNullOrEmpty(textBoxTIMEMODE.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxTIMEMODE, "Typ sledování času musí být vyplněn");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        int _timemode;
                        status = Int32.TryParse(textBoxTIMEMODE.Text, out _timemode);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxTIMEMODE, "Typ sledování času musí být číslo");
                        }
                        else
                        {
                            if((_timemode < 0) || (_timemode > 2))
                            {
                                errorProvider1.SetError(textBoxTIMEMODE, "Typ sledování času musí být v rozmezí 0 až 2");
                            }
                        }
                    }
                }

                // kontrola TIMEPREPSTART
                if(!rowProduct.IsTIMEPREPSTARTNull() && !rowProduct.IsTIMEPREPSTOPNull())
                {
                    if (dateTimePickerTIMEPREPSTART.Value >= dateTimePickerTIMEPREPSTOP.Value)
                    {
                        errorProvider1.SetError(dateTimePickerTIMEPREPSTOP, "Stop čas přípravy musí být větší než Start čas přípravy");
                    }
                }                

                // kontrola TIMESTART
                if (!rowProduct.IsTIMESTARTNull())
                {
                    if (dateedit || !rowProduct.IsTIMESTOPNull())
                    {
                        if (dateTimePickerTIMESTART.Value >= dateTimePickerTIMESTOP.Value)
                        {
                            errorProvider1.SetError(dateTimePickerTIMESTOP, "Čas ukončení musí být větší než čas zahájení");
                        }
                    }
                }

                // kontrola TIMECORSTART
                if (!rowProduct.IsTIMECORSTARTNull())
                {
                    if (dateedit || !rowProduct.IsTIMECORSTOPNull())
                    {
                        if (dateTimePickerTIMECORSTART.Value >= dateTimePickerTIMECORSTOP.Value)
                        {
                            errorProvider1.SetError(dateTimePickerTIMECORSTOP, "Čas ukončení korekce musí být větší než čas zahájení korekce");
                        }
                    }
                }

                // kontrola loginid (id vedouciho smeny)
                if (!string.IsNullOrEmpty(textBoxLoginid.Text.Trim()))
                {
                    var _validLoginid = taLogins.GetDataByID(textBoxLoginid.Text.Trim());

                    //var _validLoginid = ds.Logins.Where(x => x.id == textBoxLoginid.Text.Trim());
                    // nenalezen
                    if (_validLoginid.Count() == 0)
                    {
                        errorProvider1.SetError(textBoxLoginid, "Vedoucí směny nebyl nalezen");
                    }
                    else
                    {
                        // kontrola, zdali je uživatel vedoucí směny
                        if (_validLoginid.First().VS != 1)
                        {
                            errorProvider1.SetError(textBoxLoginid, "Uživatel "+ _validLoginid.First().firstname.Trim() + " " + _validLoginid.First().surname.Trim() +" není vedoucí směny");
                        }

                    }
                }

                // kontrola machineid
                if (!rowProduct.IsmachineidNull())
                {
                    if (string.IsNullOrEmpty(textBoxMachineid.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxMachineid, "Id stroje musí být vyplněno");
                    }
                    else
                    {
                        var _validMachineid = taMachines.GetDataByID(textBoxMachineid.Text.Trim());
                        //var _validMachineid = ds.Machines.Where(x => x.id == textBoxMachineid.Text.Trim());
                        if (_validMachineid.Count() == 0)
                        {
                            errorProvider1.SetError(textBoxMachineid, "Id stroje nebylo nalezeno");
                        }
                    }
                }

                // kontrola operationid
                if (!rowProduct.IsoperationidNull())
                {
                    if (string.IsNullOrEmpty(textBoxOperationid.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxOperationid, "Id operace musí být vyplněno");
                    }
                    else
                    {
                        var _validOperationid = taOperations.GetDataByID(textBoxOperationid.Text.Trim());
                        //var _validOperationid = ds.Operations.Where(x => x.id == textBoxOperationid.Text.Trim());
                        if (_validOperationid.Count() == 0)
                        {
                            errorProvider1.SetError(textBoxOperationid, "Id stroje nebylo nalezeno");
                        }
                    }
                }


                // kontrola machineid a operationid
                if (!rowProduct.IsmachineidNull() && !rowProduct.IsoperationidNull())
                {
                    if (string.IsNullOrEmpty(textBoxMachineid.Text.Trim()) && (string.IsNullOrEmpty(textBoxOperationid.Text.Trim())))
                    {
                        var _validMachineidOperationid = tavMachinesOperations.GetDataByMachineIDoperationID(textBoxMachineid.Text.Trim(), textBoxOperationid.Text.Trim());

                        if (_validMachineidOperationid.Count == 0)
                        {
                            errorProvider1.SetError(textBoxMachineid, "K id stroje " + textBoxMachineid.Text.Trim() + " nebyla nalezena operace s id " + textBoxOperationid.Text.Trim());
                        }
                    }
                }
                
                // kontrola QTYPACKMJ
                if (!rowProduct.IsQTYPACKMJNull())
                {
                    if (textBoxQTYPACKMJ.Text.Trim().Length > dsV.Production.QTYPACKMJColumn.MaxLength)
                    {
                        errorProvider1.SetError(textBoxQTYPACKMJ, "Měrná jednotka balení může mít maximálně " + dsV.Production.QTYPACKMJColumn.MaxLength + " znaků");
                    }
                    // empty muze byt
                    
                }

                // kontrola qty                
                if (string.IsNullOrEmpty(textBoxQty.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxQty, "Počet kusů musí být vyplněn");
                }
                else   // kontrola, zdali je cislo
                {
                    decimal _qty;
                    status = Decimal.TryParse(textBoxQty.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qty);
                    if (!status)
                    {
                        errorProvider1.SetError(textBoxQty, "Počet kusů musí být číslo");
                    }
                    else
                    {
                        if (_qty < 0)
                        {
                            errorProvider1.SetError(textBoxQty, "Počet kusů musí být větší než nebo rovno 0");
                        }
                    }
                }

                // kontrola qtyReal                
                if (string.IsNullOrEmpty(textBoxQtyReal.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxQtyReal, "Počet kusů sejmuto musí být vyplněn");
                }
                else   // kontrola, zdali je cislo
                {
                    decimal _qty;
                    status = Decimal.TryParse(textBoxQtyReal.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qty);
                    if (!status)
                    {
                        errorProvider1.SetError(textBoxQtyReal, "Počet kusů sejmuto musí být číslo");
                    }
                    else
                    {
                        if (_qty < 0)
                        {
                            errorProvider1.SetError(textBoxQtyReal, "Počet kusů sejmuto musí být větší než nebo rovno 0");
                        }
                    }
                }

                // kontrola QTYPACK
                if (!rowProduct.IsQTYPACKNull())
                {
                    if (string.IsNullOrEmpty(textBoxQTYPACK.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxQTYPACK, "Množství v balení musí být vyplněno");
                    }
                    else   // kontrola, zdali je cislo
                    {
                        decimal _qty;
                        status = Decimal.TryParse(textBoxQTYPACK.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _qty);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxQTYPACK, "Množství v balení musí být číslo");
                        }
                        else
                        {
                            if (_qty < 0)
                            {
                                errorProvider1.SetError(textBoxQTYPACK, "Množství v balení musí být větší než nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola UserID
                if(string.IsNullOrEmpty(textBoxUserID.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxUserID, "Id uživatele musí být vyplněno");
                }
                else
                {
                    var _validUserid = taLogins.GetDataByID(textBoxUserID.Text.Trim());

                    //var _validLoginid = ds.Logins.Where(x => x.id == textBoxLoginid.Text.Trim());
                    // nenalezen
                    if (_validUserid.Count() == 0)
                    {
                        errorProvider1.SetError(textBoxUserID, "Id uživatele nebylo nalezeno");
                    }
                }

                // kontrola TermID
                if (string.IsNullOrEmpty(textBoxTermID.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxTermID, "Id terminálu musí být vyplněno");
                }
                else
                {
                    byte _termid;
                    status = Byte.TryParse(textBoxTermID.Text, out _termid);
                    if (!status)
                    {
                        errorProvider1.SetError(textBoxTermID, "Id terminálu musí být číslo");
                    }
                }

                // kontrola TIMEUNIT
                if (!rowProduct.IsTIMEUNITNull())
                {
                    if (string.IsNullOrEmpty(textBoxTIMEUNIT.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxTIMEUNIT, "Jednotkový čas musí být vyplněn");
                    }
                    else
                    {
                        float timeunit;
                        status = float.TryParse(textBoxTIMEUNIT.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out timeunit);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxTIMEUNIT, "Jednotkový čas musí být číslo");
                        }
                        else
                        {
                            if (timeunit < 0)
                            {
                                errorProvider1.SetError(textBoxTIMEUNIT, "Jednotkový čas musí být větší, nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola TIMEPREP
                if (!rowProduct.IsTIMEPREPNull())
                {
                    if (string.IsNullOrEmpty(textBoxTIMEPREP.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxTIMEPREP, "Přípravný čas musí být vyplněn");
                    }
                    else
                    {
                        float timeprep;
                        status = float.TryParse(textBoxTIMEPREP.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out timeprep);
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxTIMEPREP, "Přípravný čas musí být číslo");
                        }
                        else
                        {
                            if (timeprep < 0)
                            {
                                errorProvider1.SetError(textBoxTIMEPREP, "Přípravný čas musí být větší, nebo rovno 0");
                            }
                        }
                    }
                }

                // kontrola TIMECRID, 0 - nesouvisejici se zakazkou, 1 - souvisejici
                if (!rowProduct.IsTIMECRIDNull())
                {
                    if (string.IsNullOrEmpty(textBoxTIMECRID.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxTIMECRID, "Id korekce musí být vyplněno");
                    }
                    else  // nějaký text je vyplněn
                    {
                        int _timecrid;
                        status = Int32.TryParse(textBoxTIMECRID.Text, out _timecrid);
                        // není vyplněno číslo
                        if (!status)
                        {
                            errorProvider1.SetError(textBoxTIMECRID, "Id korekce musí být číslo");
                        }
                        else
                        {
                            var _validTimecrid = taCorrects.GetDataByID(_timecrid);
                            // id korekce nenalezeno
                            if (_validTimecrid.Count == 0)
                            {
                                errorProvider1.SetError(textBoxTIMECRID, "Id korekce nebylo nalezeno");
                            }
                            else
                            {
                                // korekce vazana k vyrobe
                                if ((_producttype == PRODUCTIONTYPE.KOREKCE_VAZANA_START) || (_producttype == PRODUCTIONTYPE.KOREKCE_VAZANA_STOP))
                                {

                                    if (_validTimecrid.First().Production != 1)
                                    {
                                        errorProvider1.SetError(textBoxTIMECRID, "Zvolená korekce není vázaná k výrobě, i když má být");
                                    }
                                }
                                else  // nevazana korekce
                                {
                                    if (_validTimecrid.First().Production != 0)
                                    {
                                        errorProvider1.SetError(textBoxTIMECRID, "Zvolená korekce je vázaná k výrobě, i když nemá být");
                                    }
                                }
                            }
                        }
                    }
                }

                // kontrola description
                //if (!rowProduct.IsdescriptionNull())

                // kontrola BarcodeP
                if (!rowProduct.IsBarcodePNull())
                {
                    if (string.IsNullOrEmpty(textBoxBarcodeP.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxBarcodeP, "Čáč. kód položky musí být vyplněn");
                    }
                    else
                    {
                        // pokud neni chyba v potrebnych hodnotach, probehne validace
                        if (string.IsNullOrEmpty(errorProvider1.GetError(textBoxCountEntries)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxSOPNUMBE)) && string.IsNullOrEmpty(errorProvider1.GetError(textBoxITEMNMBR)))
                        {
                            var _validBarcodeP = taVPP.GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(Convert.ToInt32(textBoxCountEntries.Text.Trim()), textBoxSOPNUMBE.Text.Trim(), textBoxITEMNMBR.Text.Trim(), textBoxBarcodeP.Text.Trim());
                            if (_validBarcodeP.Count == 0)
                            {
                                errorProvider1.SetError(textBoxBarcodeP, "Položka s čár. kódem " + textBoxBarcodeP.Text.Trim() + " nebyla nalezena");
                            }
                        }
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

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel1.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        /// <summary>
        /// Najití a vyplnění datumu podle všech zakázek/korekcí.
        /// </summary>
        private void FindTimeStop()
        {
            try
            {
                Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter taProduction = new DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                taProduction.Connection.ConnectionString = Globals.ConnectionString;

                //List<Production.DataServices.VyrobaDataSet.ProductionRow> prRows = new List<DataServices.VyrobaDataSet.ProductionRow>();

                Production.DataServices.VyrobaDataSet dsV = new DataServices.VyrobaDataSet();

                // není vyplněno datum stop, hledá se aktuální
                if (rowProduct.IsTIMECORSTOPNull() && rowProduct.IsTIMESTOPNull())
                {                    
                    // kontrola TIMESTART
                    if (!rowProduct.IsSOUBEHGUIDNull())
                    {
                        //resSoubeh.AddRange(taProduction.GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID));
                        taProduction.FillBySOUBEHGUID(dsV.Production, rowProduct.SOUBEHGUID);
                    }
                    else
                    {
                        // korekce
                        if (!rowProduct.IsCORRGUIDNull())
                        {
                            //resSoubeh.AddRange(taProduction.GetDataBySOUBEHGUID(rowProduct.CORRGUID));
                            taProduction.FillByCORRGUID(dsV.Production, rowProduct.CORRGUID);
                            //prRows.AddRange(dsV.Production);
                        }
                    }

                    var _timestop = dsV.Production.Where(x => !x.IsTIMESTOPNull() || !x.IsTIMECORSTOPNull());
                    // nalezena nejaka ukoncena korekce/zakazka
                    if (_timestop.Count() > 0)
                    {
                        var record = _timestop.First();
                        // timestop vyplnen
                        if (!record.IsSOUBEHGUIDNull() && !record.IsTIMESTOPNull())
                        {
                            dateedit = true;
                            dateTimePickerTIMESTOP.Value = record.TIMESTOP;
                        }
                        else
                        {
                            // timecorstop vyplnen
                            if (!record.IsCORRGUIDNull() && !record.IsTIMECORSTOPNull())
                            {
                                dateedit = true;
                                dateTimePickerTIMECORSTOP.Value = record.TIMECORSTOP;
                            }
                        }
                        
                    }
                    //if ((_producttype == PRODUCTIONTYPE.ODVOD_START) || (_producttype == PRODUCTIONTYPE.ODVOD_STOP))
                    //{
                    //    // nadop
                    //    if (rowProduct.IsITEMNMBRNull())
                    //    {
                    //        prRows.AddRange(dsV.Production.Where(x => (x.CountEntries == rowProduct.CountEntries) && (x.SOPNUMBE == rowProduct.SOPNUMBE)).ToList());
                    //    }
                    //    else  // zbytek
                    //    {
                    //        prRows.AddRange(dsV.Production.Where(x => (x.CountEntries == rowProduct.CountEntries) && (x.SOPNUMBE == rowProduct.SOPNUMBE) && (x.ITEMNMBR == rowProduct.ITEMNMBR)).ToList());
                    //    }
                    //    //dsV.Production.Where(
                    //}
                }

                
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
