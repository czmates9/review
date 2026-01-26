using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class Odvod
    {
        Object oSynchronizationLock = new object();

        //aktualni (potvrzeny) car. kod
        private string _barcodePrv;
        public string BarcodePrevious
        {
            get
            {
                return _barcodePrv;
            }
        }
        private string _barcodeAct;
        public string BarcodeActual
        {
            get
            {
                return _barcodeAct;
            }
            set
            {
                lock (oSynchronizationLock)
                {
                    if (_barcodePrv != _barcodeAct)
                        _barcodePrv = _barcodeAct;
                    _barcodeAct = value;
                }
            }
        }

        #region prohazovani
        internal decimal _codeProhazCount = 0;
        public decimal CodeProhazCnt
        {
            get
            {
                lock (oSynchronizationLock)
                {
                    return _codeProhazCount;
                }
            }
        }
        public decimal CodeProhazCntIncrement(decimal increment)
        {
            lock (oSynchronizationLock)
            {
                _codeProhazCount += increment;
            }
            return _codeProhazCount;
        }

        public decimal CodeProhazCntGetAndReset()
        {
            lock (oSynchronizationLock)
            {
                decimal codeprohazcnt = _codeProhazCount;
                _codeProhazCount = 0;
                return codeprohazcnt;
            }
        }


        internal bool _codeProhazZapnutSpatnyKod = false;
        public bool CodeProhazON_SpatnyKod
        {
            get
            {
                lock (oSynchronizationLock)
                {
                    return _codeProhazZapnutSpatnyKod;
                }
            }
        }

        public bool CodeProhazZapnutSpatnyKoD(bool prohazZapnutSpatnyKod)
        {
            lock (oSynchronizationLock)
            {
                _codeProhazZapnutSpatnyKod = prohazZapnutSpatnyKod;
            }
            return _codeProhazZapnutSpatnyKod;
        }


        internal bool _codeProhazZapnut = false;
        public bool CodeProhazON
        {
            get
            {
                lock (oSynchronizationLock)
                {
                    return _codeProhazZapnut;
                }
            }
        }

        public bool CodeProhazZapnut(bool prohazZapnut)
        {
            lock (oSynchronizationLock)
            {
                _codeProhazZapnut = prohazZapnut;
            }
            return _codeProhazZapnut;
        }

        public bool CodeProhazZapnutAndReset()
        {
            lock (oSynchronizationLock)
            {
                bool codeprohaz = _codeProhazZapnut;
                _codeProhazZapnut = false;
                return codeprohaz;
            }
        }

        public bool CodeProhazZapnutGet()
        {

            return _codeProhazZapnut;
        }

        public decimal CodeProhazCntGet()
        {
            lock (oSynchronizationLock)
            {
                decimal codeprohazcnt = _codeProhazCount;
                return codeprohazcnt;
            }
        }
        #endregion

        #region CodeReadCnt Synchronization
        // pocet nactenych a potvrzenych kodu
        internal decimal _codeReadCnt = 0;
        public decimal CodeReadCnt
        {
            get
            {
                lock (oSynchronizationLock)
                {
                    return _codeReadCnt;
                }
            }
            //menit hodnotu muze jen tento objekt ...
            //private 
            //set
            //{
            //    lock (oSynchronizationLock)
            //    {
            //        _codeReadCnt = value;
            //    }
            //}
        }

        /// <summary>
        /// Provede synchronni increment promenne o urcenou hodnotu
        /// </summary>
        /// <param name="increment"></param>
        /// <returns></returns>
        public decimal CodeReadCntIncrement(decimal increment)
        {
            lock (oSynchronizationLock)
            {
                _codeReadCnt += increment;
            }
            return _codeReadCnt;
        }

        public decimal CodeReadCntGetAndReset()
        {
            lock (oSynchronizationLock)
            {
                decimal codereadcnt = _codeReadCnt;
                _codeReadCnt = 0;
                return codereadcnt;
            }
        }

        public decimal CodeReadCntGet()
        {
            lock (oSynchronizationLock)
            {
                decimal codereadcnt = _codeReadCnt;
                return codereadcnt;
            }
        }
        #endregion

        #region CodeNoReadCnt Synchronization
        //pocet nenactenych a potvrzenych kodu
        internal decimal _codeNoReadCnt = 0;
        public decimal CodeNoReadCnt
        {
            get
            {
                lock (oSynchronizationLock)
                {
                    return _codeNoReadCnt;
                }
            }
            // menit muze jen tento objekt
            //private 
            //set
            //{
            //    lock (oSynchronizationLock)
            //    {
            //        _codeNoReadCnt = value;
            //    }
            //}
        }
        
        /// <summary>
        /// Provede synchronni increment promenne o urcenou hodnotu
        /// </summary>
        /// <param name="increment"></param>
        /// <returns></returns>
        public decimal CodeNoReadCntIncrement(decimal increment)
        {
            lock (oSynchronizationLock)
            {
                _codeNoReadCnt += increment;
            }
            return _codeNoReadCnt;
        }

        public decimal CodeNoReadCntGetAndReset()
        {
            lock (oSynchronizationLock)
            {
                decimal codenoreadcnt = _codeNoReadCnt;
                _codeNoReadCnt = 0;
                return codenoreadcnt;
            }
        }

        public decimal CodeNoReadCntGet()
        {
            lock (oSynchronizationLock)
            {
                decimal codenoreadcnt = _codeNoReadCnt;
                return codenoreadcnt;
            }
        }

        /// <summary>
        /// Vraci Soucet hodnot : CodeReadCnt + CodeNoReadCnt
        /// </summary>
        public decimal CodeAllCount
        {
            get { return CodeReadCnt + CodeNoReadCnt; }
        }
        #endregion

        public Odvod()
        {
            this.Reset();
        }

        public void Reset()
        {
            BarcodeActual = string.Empty;
            //CodeReadCntGetAndReset();
            //CodeNoReadCntGetAndReset();
        }
    }
}
