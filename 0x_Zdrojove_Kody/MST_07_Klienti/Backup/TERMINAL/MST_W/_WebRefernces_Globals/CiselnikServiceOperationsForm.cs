using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;
using System.Net;

namespace Fask.MST_W._WebRefernces_Globals
{
    public partial class CiselnikServiceOperationsForm : Fask.MST_W.Forms.ServiceOperationsForm
    {
        public enum Operation
        {
            Zbozi,            
            Odberatele,
            Strediska,
            TypDokladu,
            Sklady,
            Pracovnici,
            Lokace,
            Uzivatele,
            Meny,
            ExportZbozi,
            ExportOdberatele,
            ExportSklady,
            ExportMeny,
            ExportStrediska,
            ExportPracovnici
        }
        private System.Threading.Thread threadReindexaceZbozi = null;
        private System.Threading.Thread threadReindexaceLokaci = null;
        private Operation operation;
        private _WebRefernces_Globals.CiselnikServiceSession cservice = null;
        private _WebRefernces_Globals.LoginServiceSession lservice = null;
        private string skladprefix = string.Empty;

        private CiselnikServiceOperationsForm()
        {
            InitializeComponent();
        }

        private CiselnikServiceOperationsForm(_WebRefernces_Globals.CiselnikServiceSession cservice, Operation operation, string skladprefix)
            : this()
        {
            this.cservice = cservice;
            this.operation = operation;
            this.skladprefix = skladprefix;
        }

        private CiselnikServiceOperationsForm(_WebRefernces_Globals.CiselnikServiceSession cservice, Operation operation)
            : this()
        {
            this.cservice = cservice;
            this.operation = operation;
            this.skladprefix = "";
        }

        private CiselnikServiceOperationsForm(_WebRefernces_Globals.LoginServiceSession loginservice, Operation operation)
            : this()
        {
            this.cservice = null;
            this.lservice = loginservice;
            this.operation = operation;
            this.skladprefix = "";
        }

        protected override void finalize()
        {
            base.finalize();
            if (this.threadReindexaceZbozi != null)
            {
                threadReindexaceZbozi.Abort();
            }

            if (this.threadReindexaceLokaci != null)
            {
                threadReindexaceLokaci.Abort();
            }
        }

        #region Entering methods

        public static bool KatalogZboziExport(CiselnikServiceSession ciselnikS, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportZbozi, skladprefix))
            {
                cso.Description = "Probíhá export číselníku zboží";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogMenyExport(CiselnikServiceSession ciselnikS)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportMeny))
            {
                cso.Description = "Probíhá export číselníku měn";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogStrediskaExport(CiselnikServiceSession ciselnikS)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportStrediska))
            {
                cso.Description = "Probíhá export číselníku středisek";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogPracovniciExport(CiselnikServiceSession ciselnikS)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportPracovnici))
            {
                cso.Description = "Probíhá export číselníku pracovníků";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogOdberateleExport(CiselnikServiceSession ciselnikS)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportOdberatele))
            {
                cso.Description = "Probíhá export číselníku odběratelů";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogSkladyExport(CiselnikServiceSession ciselnikS)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportSklady))
            {
                cso.Description = "Probíhá export číselníku skladů";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogZbozi(_WebRefernces_Globals.CiselnikServiceSession cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Zbozi, skladprefix))
            {
                cso.Description = "Stahuje se číselník zboží";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogUzivatelu(_WebRefernces_Globals.LoginServiceSession lservice)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(lservice, Operation.Uzivatele))
            {
                cso.Description = "Stahuje se číselník uživatelů";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogSklady(_WebRefernces_Globals.CiselnikServiceSession cservice)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Sklady, string.Empty))
            {
                cso.Description = "Stahuje se číselník skladů";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogOdberatele(_WebRefernces_Globals.CiselnikServiceSession cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Odberatele, skladprefix))
            {
                cso.Description = "Stahuje se číselník odběratelů";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogTypDokladu(_WebRefernces_Globals.CiselnikServiceSession cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.TypDokladu, skladprefix))
            {
                cso.Description = "Stahuje se číselník typů dokladů";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogStrediska(_WebRefernces_Globals.CiselnikServiceSession cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Strediska, skladprefix))
            {
                cso.Description = "Stahuje se číselník středisek";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogPracovnici(_WebRefernces_Globals.CiselnikServiceSession cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Pracovnici, skladprefix))
            {
                cso.Description = "Stahuje se číselník pracovníků";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogMen(_WebRefernces_Globals.CiselnikServiceSession cservice)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Meny))
            {
                cso.Description = "Stahuje se číselník měn";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogLokace(_WebRefernces_Globals.CiselnikServiceSession cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Lokace, skladprefix))
            {
                cso.Description = "Stahuje se číselník lokací";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }
        
		#endregion

        private void CiselnikServiceOperationsForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

			this.BeginInvoke((System.Threading.ThreadStart)delegate()
			{
				ProcessRequest();
			});

        }

		private void ProcessRequest()
		{
			try
			{
				switch (operation)
				{
					case Operation.Zbozi:
						aresult = cservice.BeginKatalogZboziDBPrepare(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogZboziEnd), null);
						break;
					case Operation.Odberatele:
						aresult = cservice.BeginKatalogOdberateleDBPrepare(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogOdberateleEnd), null);
						break;
					case Operation.Strediska:
						aresult = cservice.BeginKatalogStrediskaDBPrepare(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogStrediskaEnd), null);
						break;
					case Operation.TypDokladu:
						aresult = cservice.BeginKatalogTypDokladuDBPrepare(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogTypDokladuEnd), null);
						break;
					case Operation.Sklady:
						aresult = cservice.BeginKatalogSkladyDBPrepare(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogSkladuEnd), null);
						break;
					case Operation.Pracovnici:
						aresult = cservice.BeginKatalogPracovniciDBPrepare(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogPracovniciEnd), null);
						break;
					case Operation.Lokace:
						aresult = cservice.BeginKatalogLokaceDBPrepare(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogLokaceEnd), null);
						break;
					case Operation.Uzivatele:
						aresult = lservice.BeginGetKatalogUzivatele(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogUzivateleEnd), null);
						break;
					case Operation.Meny:
						aresult = cservice.BeginKatalogMenyDBPrepare(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogMenyEnd), null);
						break;
					case Operation.ExportZbozi:
						aresult = cservice.BeginKatalogZboziExport(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogZboziExportEnd), null);
						break;
					case Operation.ExportOdberatele:
						aresult = cservice.BeginKatalogOdberateleExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogOdberateleExportEnd), null);
						break;
					case Operation.ExportSklady:
						aresult = cservice.BeginKatalogSkladyExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogSkladyExportEnd), null);
						break;
					case Operation.ExportPracovnici:
						aresult = cservice.BeginKatalogPracovniciExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogPracovniciExportEnd), null);
						break;
					case Operation.ExportMeny:
						aresult = cservice.BeginKatalogMenyExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogMenyExportEnd), null);
						break;
					case Operation.ExportStrediska:
						aresult = cservice.BeginKatalogStrediskaExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogStrediskaExportEnd), null);
						break;
					default:
						this.BeginInvoke(new VoidDelegate(PerformCancel));
						break;
				}

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		protected override void PerformCancel()
		{
			try
			{
				//if (cservice != null)
				//    cservice.Abort();

				//cservice = null;

				var tmpcservice = cservice;
				cservice = null;
				if (tmpcservice != null)
					tmpcservice.Abort();

			}
			catch (Exception exAbort)
			{
				Logging.Log.Write(exAbort);
			}
			base.PerformCancel();
		}


		#region Zbozi

		private void ProcessKatalogZboziEnd(IAsyncResult ares)
        {
            try
            {
                if (cservice == null) //Doslo k abrotu, tak ven
                {
                    return;
                }

                CiselnikService.StatusObject done = cservice.EndKatalogZboziDBPrepare(ares);

				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogZboziProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
        }

		private void ProcessKatalogZboziProcess(CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikZboziDB);
                
				//ReindexaceCiselnikZbozi(Main.CiselnikZboziDB);
                //threadReindexaceZbozi = new System.Threading.Thread((System.Threading.ThreadStart)delegate() { ReindexaceCiselnikZbozi(Main.CiselnikZboziDB); });
                //threadReindexaceZbozi.Start();

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

        private void ReindexaceCiselnikZboziEnd()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((Action)delegate() { this.ReindexaceCiselnikZboziEnd(); });
                return;
            }

            try
            {
                this.BeginInvoke((Action)delegate() { this.PerformOK(); });
            }
            catch (Exception ex)
            {
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
        }

		private void ProcessKatalogZboziExportEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogZboziExport(ares);
				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogZboziExportProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogZboziExportProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}


		private void ReindexaceCiselnikZbozi(string fileciselnik)
		{
            using (var sceconn = new System.Data.SQLite.SQLiteConnection("Data source=" + fileciselnik))
            {
                try
                {
                    this.BeginInvoke((System.Threading.ThreadStart)delegate()
                    {
                        this.Description = "Kontrola dat";
                    });

                    sceconn.Open();

                    int pocetindexu = 13;
                    int pocetindexucount = 1;

                    this.BeginInvoke((System.Threading.ThreadStart)delegate()
                    {
                        this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                    });

                    using (var scec = sceconn.CreateCommand())
                    {
                        scec.CommandText = "Select * from czmst095 where itemdesc='zamsehf'";
                        scec.ExecuteNonQuery();


                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });
                        scec.CommandText = "Select * from czmst095 where vnditnum='98765214'";
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });
                        scec.CommandText = "Select * from czmst095 where cz_carkod='98765214'";
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });
                        scec.CommandText = "Select * from czmst095 where skl_id='98765214'";
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });
                        scec.CommandText = "Select * from czmst095 where itemnmbr='98765214'";
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });
                        scec.CommandText = "Select * from czmst095 where itemcode='dasdcxyc'";
                        scec.ExecuteNonQuery();


                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });
                        scec.CommandText = "Select * from czmst095 where odb_id='dasdcxyc'";
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });
                        scec.CommandText = "Select * from czmst095 where serltnum='dasdcxyc'";
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });

                        scec.CommandText = "select count(*) from czmst095";
                        scec.CommandType = CommandType.Text;
                        scec.ExecuteScalar();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });

                        scec.CommandText = "select * from czmst095M where ITEMNMBR='98765214'";
                        scec.CommandType = CommandType.Text;
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });

                        scec.CommandText = "select * from czmst095M where ITEMNMBR='98765214' and mena_ID='CZK'";
                        scec.CommandType = CommandType.Text;
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });

                        scec.CommandText = "select * from czmst095M where ITEMNMBR='98765214' and mena_ID='CZK' and PRICEX=0";
                        scec.CommandType = CommandType.Text;
                        scec.ExecuteNonQuery();

                        this.BeginInvoke((System.Threading.ThreadStart)delegate()
                        {
                            this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                        });

                        scec.CommandText = "select count(*) from czmst095M";
                        scec.CommandType = CommandType.Text;
                        scec.ExecuteNonQuery();
                    }

                    this.BeginInvoke((Action)delegate() { this.ReindexaceCiselnikZboziEnd(); });
                }
                catch (Exception e)
                {
                    Logging.Log.Write(e);
                }
                finally
                {
                    if ((sceconn != null) && ((sceconn.State & ConnectionState.Open) == ConnectionState.Open))
                        sceconn.Close();

                    this.threadReindexaceZbozi = null;
                }
            }
		}

		#endregion

		#region Odberatele

		private void ProcessKatalogOdberateleEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				var done = cservice.EndKatalogOdberateleDBPrepare(ares);
				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogOdberateleProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogOdberateleProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikOdberateleDB);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogOdberateleExportEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogOdberateleExport(ares);

				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogOdberateleExportEnd(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogOdberateleExportEnd(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		#endregion

		#region Strediska

		private void ProcessKatalogStrediskaEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				var done = cservice.EndKatalogStrediskaDBPrepare(ares);
				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogStrediskaProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogStrediskaProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
					throw new Exception(done.StatusText);

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikStrediskaDB);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogStrediskaExportEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogSkladyExport(ares);
				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogStrediskaExportProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogStrediskaExportProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}


		#endregion

		#region Meny

        private void ProcessKatalogMenyEnd(IAsyncResult ares)
        {
            try
            {
                if (cservice == null) //Doslo k abrotu, tak ven
                {
                    return;
                }

                var done = cservice.EndKatalogMenyDBPrepare(ares);
				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogMenyProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
        }

		private void ProcessKatalogMenyProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
					throw new Exception(done.StatusText);

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikMenDB);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogMenyExportEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogSkladyExport(ares);
				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogMenyExportProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogMenyExportProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		#endregion

		#region TypDokladu

		private void ProcessKatalogTypDokladuEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				var done = cservice.EndKatalogTypDokladuDBPrepare(ares);

				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogTypDokladuProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogTypDokladuProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
					throw new Exception(done.StatusText);

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikTypDokladuDB);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		#endregion

		#region Uzivatele

		private void ProcessKatalogUzivateleEnd(IAsyncResult ares)
		{
			try
			{
				if (lservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				LoginService.StatusObject done = lservice.EndGetKatalogUzivatele(ares);
				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogUzivateleProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogUzivateleProcess(LoginService.StatusObject done)
		{
			try
			{
				if (lservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}


				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikUzivateleDB);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}


		#endregion

		#region Sklady

		private void ProcessKatalogSkladuEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				var done = cservice.EndKatalogSkladyDBPrepare(ares);

				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogSkladuProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogSkladuProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

				FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikSkladyDB);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogSkladyExportEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogSkladyExport(ares);

				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogSkladyExportProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogSkladyExportProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		#endregion

		#region Pracovnici

		private void ProcessKatalogPracovniciExportEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogSkladyExport(ares);
				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogPracovniciExportProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
			}
		}

		private void ProcessKatalogPracovniciExportProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				if (done.Exception)
					throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}


		private void ProcessKatalogPracovniciEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				var done = cservice.EndKatalogPracovniciDBPrepare(ares);

				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogPracovniciProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogPracovniciProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
					throw new Exception(done.StatusText);

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikPracovniciDB);

				this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		#endregion

		#region Lokace

		private void ProcessKatalogLokaceEnd(IAsyncResult ares)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

				var done = cservice.EndKatalogLokaceDBPrepare(ares);

				this.BeginInvoke((VoidDelegate)delegate()
				{
					this.ProcessKatalogLokaceProcess(done);
				});
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
		}

		private void ProcessKatalogLokaceProcess(Fask.MST_W.CiselnikService.StatusObject done)
		{
			try
			{
				if (cservice == null) //Doslo k abrotu, tak ven
				{
					return;
				}

                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
					throw new Exception(done.StatusText);

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikLokaceDB);

                //ReindexaceCiselnikLokaci();
                //threadReindexaceLokaci = new System.Threading.Thread((System.Threading.ThreadStart)delegate() { this.ReindexaceCiselnikLokaci(Main.CiselnikLokaceDB); });
                //threadReindexaceLokaci.Start();
				
                this.BeginInvoke(new VoidDelegate(PerformOK));
			}
			catch (Exception ex)
			{
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
        }

        private void ReindexaceCiselnikLokaci(string fileciselnik)
		{
            using (System.Data.SQLite.SQLiteConnection sceconn = new System.Data.SQLite.SQLiteConnection("Data source=" + fileciselnik))
            {
                try
                {
                    this.BeginInvoke((System.Threading.ThreadStart)delegate()
                    {
                        this.Description = "Kontrola dat";
                    });

                    sceconn.Open();

                    int pocetindexu = 1;
                    int pocetindexucount = 1;

                    this.BeginInvoke((System.Threading.ThreadStart)delegate()
                    {
                        this.Description = "Obnovení indexů " + (pocetindexucount++) + " / " + pocetindexu;
                    });

                    using (var scec = sceconn.CreateCommand())
                    {
                        scec.CommandText = "Select * from czmst094 where skl_id='zamsehf'";
                        scec.ExecuteNonQuery();
                    }

                    this.BeginInvoke((Action)delegate() { this.ReindexaceCiselnikLokaciEnd(); });
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                }
                finally
                {
                    if (sceconn != null && sceconn.State == ConnectionState.Open)
                        sceconn.Close();

                    this.threadReindexaceLokaci = null;
                }
            }
        }

        private void ReindexaceCiselnikLokaciEnd()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((Action)delegate() { this.ReindexaceCiselnikZboziEnd(); });
                return;
            }

            try
            {
                this.BeginInvoke((Action)delegate() { this.PerformOK(); });
            }
            catch (Exception ex)
            {
                this.BeginInvoke((VoidDelegate)delegate()
                {
                    Exceptions.ExceptionVizualize.Show(ex);
                    this.PerformCancel();
                });
            }
        }

        #endregion

    }
}

