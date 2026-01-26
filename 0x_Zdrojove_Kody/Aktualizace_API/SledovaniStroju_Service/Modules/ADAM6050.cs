using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;
using System.Net.NetworkInformation;
using SledovaniStroju_Service;

namespace FASK.Modules
{
    public class ADAM6050 : ModulBase
    {
        public const string MType = "ADAM6050";

        // TODO : objekt si bude udrzovat seznam posledniho a aktualniho stavu zarizeni pro porovnani
        //public bool[] stateLstDI = null;
        //public bool[] stateLstDO = null;
        //public bool[] stateActDI = null;
        //public bool[] stateActDO = null;
        //private bool[] o_DO = null;
        //private bool[] o_DI = null;

        private bool[] state_AuxFlagsLst = null;
        private bool[] state_AuxFlagsAct = null;
        private bool[] state_AuxFlags = null;
        private string errorAct = string.Empty;
        private string errorLst = string.Empty;

        // čítač, který počítá počet po sobě jdoucích špatných připojení
        private uint CurrentStateAttempt = 0;
        // čítač, který počítá počet po sobě jdoucích úspěšných pingů
        private uint CurrentSuccessfulPingAttempt = 0;

        private Advantech.Adam.AdamSocket _adam;
        public Advantech.Adam.AdamSocket ADAM
        {
            get { return _adam; }
            set { _adam = value; }
        }

        public ADAM6050(string ip)
            : base(ip)
        {
        }

        public override void Initialize()
        {
            _adam = new Advantech.Adam.AdamSocket(Advantech.Adam.AdamType.Adam6000);
            // TODO : dat do konfigurace ...
            // _adam.SetTimeout(1000, 1000, 1000);
            _adam.SetTimeout(Properties.Settings.Default.ConnectionTimeout, Properties.Settings.Default.SendingTimeout, Properties.Settings.Default.ReceivingTimeout);
            //Advantech.Adam.Configuration.
            //_adam.Configuration(
            //var conf = _adam.Configuration();
            //conf.ResetPassword
            //conf.errorAct
            // ??? 
            //System.Collections.ArrayList o_lstAdam = new System.Collections.ArrayList();
            //Advantech.Adam.AdamSocket.GetAdamDeviceList(1000, out o_lstAdam);
            
        }

        public override void Terminate()
        {
            if (this.Disconnect())
                _adam = null;
        }

        /// <summary>
        /// Nacte aktualni stav zarizeni
        /// </summary>
        /// <remarks>Pokud uspeje nacteni stavu, tak se aktualni stav presune do historie a novy se nastavi jako aktualni</remarks>
        /// <returns>Pokud uspeje, tak vraci true, jinak false</returns>
        public override bool ReadStates()
        {
            try
            {
                // nepodařilo se připojit, proběhne ping na danou IP adresu 
                if (errorAct == Advantech.Common.ErrorCode.Socket_Connect_Fail.ToString())//Advantech.Adam.AdamSocket.
                {
                    // ping na IP adama
                    if (PingHost(this.IP))
                    {
                        CurrentSuccessfulPingAttempt++;
                        Fask.Logging.Log.Write(this.IP + " " + errorLst + " CurrentPingAttempt : " + CurrentSuccessfulPingAttempt + " (max: " + Properties.Settings.Default.NumberOfPingAttemptBeforeOpenConnection + ")");                        
                        // proběhl nastavený počet úspěšných pingů na Adama, proběhne pokus o připojení
                        if (testMaxPingTest())
                        {
                            if (!this.Connect())
                            {
                                CurrentSuccessfulPingAttempt = 0;
                                throw new Exception(this.IP + " Connect failed");
                            }
                        }
                        else throw new Exception(this.IP + " Connect failed (ping prosel)");
                    }
                    else   // ping neprošel, vynulování počtu úspěšných pingů
                    {
                        CurrentSuccessfulPingAttempt = 0;
                        throw new Exception(this.IP + " Connect failed (ping neprosel)");
                    }
                }
                else if (!this.Connect()) 
                {
                    CurrentSuccessfulPingAttempt = 0;
                    throw new Exception(this.IP + " Connect failed");
                }

                //bool result = _adam.DigitalInput(1).GetValues(1, 18, out o_bValues);
                //bool result = _adam.Modbus().ReadCoilStatus(1, 12, out o_DI) && _adam.Modbus().ReadCoilStatus(17, 6, out o_DO);

                //if (result)
                //{
                //    stateLstDI = stateActDI;
                //    stateLstDO = stateActDO;
                //    stateActDI = o_DI;
                //    stateActDO = o_DO;
                //}

                bool result = _adam.Configuration().GetGCL_AuxFlagStatus(out state_AuxFlags);
                if (result)
                {
                    state_AuxFlagsLst = state_AuxFlagsAct;
                    state_AuxFlagsAct = state_AuxFlags;
                }
                else throw new Exception(this.IP + " GetGCL_AuxFlagStatus failed");
                // vynulování čítače, který počítá počet po sobě jdoucích špatných připojení
                CurrentStateAttempt = 0;

                return result;

            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex, "Last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : string.Empty));
                Fask.Emailing.Email.SendEmail("Last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : string.Empty));
                // pokus o znovu navazeni spojeni ... 
                this.Disconnect();
                //this.Connect();
                CurrentStateAttempt++;

                if (this.testMaxConnection())
                {
                    Fask.Logging.Log.Write(this.IP + " Maximum test connection: " + CurrentStateAttempt);
                    this.state_AuxFlagsLst = this.state_AuxFlagsAct;
                    this.state_AuxFlagsAct = new bool[] { false, false, false, false, false, false, false, false, false };
                    CurrentStateAttempt = 0;
                    //throw new Exception(this.IP + " Maximum connection attempt");
                }
                return false;
            }
            finally
            {
                if (_adam != null)      // načtení typu chyby
                    errorAct = _adam.LastError.ToString();
                if (Properties.Settings.Default.AlwaysCloseConnection)
                    Disconnect();
            }
        }

        public override bool SaveStates()
        {
            // TODO : ulozit stav stroju, pokud se lisi od posledniho ... 
            //1. doslo ke zmene stavu?
            if (state_AuxFlagsAct == null)
                return false; //neni zmena

            bool zmena = false;
            if (state_AuxFlagsLst != null) //mohla byt zmena
            {
                // overeni zda se neco zmenilo ... 
                for (int i = 0; i < state_AuxFlagsAct.Length; i++)
                {
                    if ((state_AuxFlagsLst.Length >= (i + 1))
                        && state_AuxFlagsLst[i] != state_AuxFlagsAct[i])
                    {
                        zmena = true;
                        break; //doslo ke zmene, jinak pokracuji dal
                    }
                }
                if (errorLst != errorAct)
                    zmena = true;
            }
            else
            {
                zmena = true; //neni predchozi stav, tak to musi byt prvni po spusteni sluzby => ulozit
            }

            if (!zmena)
                return false; //nedoslo k ulozeni ... 

            //2. zjistit zda existuje nebo ne
            SledovaniStroju_Service.VyrobaSSTableAdapters.MachineStateSetTableAdapter madapter = new SledovaniStroju_Service.VyrobaSSTableAdapters.MachineStateSetTableAdapter();
            VyrobaSS mdataset = new VyrobaSS();
            madapter.FillByIP(mdataset.MachineStateSet, this.IP);

            VyrobaSS.MachineStateSetRow mrow = null;
            if (mdataset.MachineStateSet.Count > 0)
            {
                mrow = mdataset.MachineStateSet[0];
            }
            else
            {
                mrow = mdataset.MachineStateSet.NewMachineStateSetRow();
                mrow.IP = this.IP;
                mrow.DateModified = DateTime.Now;
                mdataset.MachineStateSet.AddMachineStateSetRow(mrow);
            }

            //3. naplnit stav
            mrow.DateModified = DateTime.Now;
            mrow.S1 = Convert.ToInt32(state_AuxFlagsAct[0]);
            mrow.S2 = Convert.ToInt32(state_AuxFlagsAct[1]);
            mrow.S3 = Convert.ToInt32(state_AuxFlagsAct[2]);
            mrow.S4 = Convert.ToInt32(state_AuxFlagsAct[3]);
            mrow.S5 = Convert.ToInt32(state_AuxFlagsAct[4]);
            mrow.S6 = Convert.ToInt32(state_AuxFlagsAct[5]);
            mrow.S7 = Convert.ToInt32(state_AuxFlagsAct[6]);            

            errorLst = errorAct;        // TODO: Změna i při naběhnutí spojení s Adamem
            mrow.LastError = errorAct;

            //4.aktualizovat
            madapter.Update(mrow);

            state_AuxFlagsLst = state_AuxFlagsAct;

            return true;

        }

        public override string ToString()
        {
            StringBuilder stateActString = new StringBuilder();
            stateActString.AppendLine(this.IP);

            if (_adam == null)
                stateActString.AppendLine("Not initialized, last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : errorAct));
            else if (!_adam.Connected)
                stateActString.AppendLine("Not connected, last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : errorAct));
            else if (state_AuxFlagsAct == null)
                stateActString.AppendLine("Not readed actual state, last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : errorAct));
            else
            {
                for (int i = 0; i < state_AuxFlagsAct.Length; i++)
                {
                    stateActString.AppendLine("[" + i + "] : " + state_AuxFlagsAct[i].ToString());
                }
            }
            return stateActString.ToString();
        }

        #region Private methods
        /// <summary>
        /// Pokusi se pripojit k zarizeni
        /// </summary>
        /// <returns>Pokud uspeje nebo je zarizeni pripojeno tak true, jinak false</returns>
        protected virtual bool Connect()
        {
            bool connected = false;

            if (_adam == null)
                this.Initialize();

            //if (_adam != null && !_adam.Connected)
            if (_adam != null)
            {
                connected = _adam.Connected;
                if (!connected)
                {
                    try
                    {
                        connected = _adam.Connect(this.IP, System.Net.Sockets.ProtocolType.Tcp, 502);
                    }
                    catch (Exception ex)
                    {
                        //Fask.Logging.Log.Write(this.IP + " connect to adam problem: " + ex.Message + ", last error: " + _adam.LastError.ToString());                        
                        Fask.Logging.Log.Write(ex, this.IP + " connect to adam problem, last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : string.Empty));
                        Fask.Emailing.Email.SendEmail(this.IP + " connect to adam problem, last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : string.Empty));
                    }
                }
            }
            else
            {
                // ??? co tady ???
                //connected = _adam.Connected;
                connected = false;
            }

            return connected;

        }

        /// <summary>
        /// Pokusi se odpojit od zarizeni
        /// </summary>
        /// <returns>Pokud uspeje tak true, jinak false</returns>
        protected virtual bool Disconnect()
        {
            try
            {
                if (_adam == null)
                    return true;

                //if (_adam != null && _adam.Connected)
                if (_adam != null)
                {
                    try
                    {
                        _adam.Disconnect();
                    }
                    catch (Exception e)
                    {
                        Fask.Logging.Log.Write(e, this.IP + " disconnect from adam problem, last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : string.Empty));
                        Fask.Emailing.Email.SendEmail(this.IP + " disconnect from adam problem, last error: " + (_adam != null ? errorAct = _adam.LastError.ToString() : string.Empty));
                    }
                }

                _adam = null;

                return true;

            }
            catch //(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Testování, zdali proběhlo maximum pokusů o opětovné připojení. Pokud ano, dojde k odpojení zařízení.
        /// </summary>
        /// <returns>true, pokud proběhlo maximum pokusů o opětovné připojení, jinak false</returns>
        private bool testMaxConnection()
        {
            return CurrentStateAttempt >= Properties.Settings.Default.MaxConnectionAttempt ? true : false;
        }

        /// <summary>
        /// Testování, zdali proběhlo maximum testů o úspěšný ping. Pokud ano, dojde k pokusu o připojení zařízení.
        /// </summary>
        /// <returns></returns>
        private bool testMaxPingTest()
        {
            return CurrentSuccessfulPingAttempt >= Properties.Settings.Default.NumberOfPingAttemptBeforeOpenConnection ? true : false;
        }

        #endregion

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false; 

            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
    }
}
