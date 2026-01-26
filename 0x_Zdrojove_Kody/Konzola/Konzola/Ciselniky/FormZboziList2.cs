using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Vyroba_Konzola.Extensions;
using FirebirdSql.Data.FirebirdClient;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Vyroba_Konzola.Ciselniky
{
    public partial class FormZboziList2 : BaseForm
    {
        public FormZboziList2(bool allowMultiSelect, Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
            : base(allowMultiSelect, typZobrazeni)
        {
            InitializeComponent();
            dataGridView1.DataSource = bindingSource1;

            if (Zobrazeni == Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.Menu = menuStrip2;
                
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void FormZboziList2_Load(object sender, EventArgs e)
        {
            if (Zobrazeni == Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                panelButtonsZobrazeniList.Init();
            }

            advancedDataGridViewSearchToolBar1.SetColumns(dataGridView1.Columns);
        }

        private void FormZboziList2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Zobrazeni == Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString());
            }
        }




        #region Override metody

        public override void PerformVyhledat()
        {


            try
            {
                DataTable dtchanged = this.dsZbozi.CZMST095.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwLoadZbozi.IsBusy)
                {
                    bwLoadZbozi.CancelAsync();
                    while (bwLoadZbozi.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Console.Interfaces.Classes.ZboziListFiltr filtr = new Fask.Console.Interfaces.Classes.ZboziListFiltr();
                //if (!CreateFilter(ref filtr))
                //    return;

                int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadZbozi.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //base.PerformVyhledat();
        }

        public override void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Settings.ProviderKonzola))
                {
                    if ( provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Vyroba_Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Console.Interfaces.Ciselniky.IZbozi2).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Console.Interfaces.Ciselniky.IZbozi2)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    //if (providerZbozi != null)
                    //    ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerZbozi).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                    if ((provider != null) && (provider is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                        ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)provider).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;



                }
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex, "Load Provider.Zbozi");
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region BW Load Zbozi

        private void bwLoadZbozi_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Console.Interfaces.Classes.ZboziListFiltr filtr = (Fask.Console.Interfaces.Classes.ZboziListFiltr)e.Argument;
                Fask.Console.Interfaces.DataSets.Zbozi ds = new Fask.Console.Interfaces.DataSets.Zbozi();

                if (bwLoadZbozi.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Console.Interfaces.Ciselniky.IZbozi2_GetFiltrovaneZbozi))
                {
                    ds = ((Fask.Console.Interfaces.Ciselniky.IZbozi2_GetFiltrovaneZbozi)provider).GetFiltrovaneZbozi(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_GetFiltrovaneZbozi");
                }

                if (bwLoadZbozi.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        private void bwLoadZbozi_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsZbozi = new Fask.Console.Interfaces.DataSets.Zbozi();
                    bindingSource1.DataSource = dsZbozi;
                    Log.Write(e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsZbozi = new Fask.Console.Interfaces.DataSets.Zbozi();
                    bindingSource1.DataSource = dsZbozi;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsZbozi = (Fask.Console.Interfaces.DataSets.Zbozi)e.Result;
                    if (dsZbozi == null)
                        dsZbozi = new Fask.Console.Interfaces.DataSets.Zbozi();

                    bindingSource1.DataSource = dsZbozi;
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }


        #endregion

    }
}
