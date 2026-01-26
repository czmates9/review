
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace Fask.Server.Interfaces.Classes
{
    //Upgrade na prenosy sqlce dbs...
    [Serializable]
    public class StatusObject
    {
        public enum Operations
        {
            PrijemGenerateDavka,
            VydejGenerateDavka
        }

        #region staticke metody pro konstrukci status objektu
        /// <summary>
        /// Vytvori X Nacte status object z uvedeneho uloziste s parametry typu string
        /// </summary>
        /// <param name="baseDirectory">Vychozi adresar pro uloziste status objectu</param>
        /// <param name="operation">Identifikator operace, ktera je provadena</param>
        /// <param name="ids">seznam parametru (parametry jsou transformovany do formatu "id1_id2_id3[_idx].so")</param>
        /// <returns>SO existujici nebo novy neulozeny (Exists=false)</returns>
        public static StatusObject Create(string baseDirectory, Operations operation, params string[] ids)
        {
            try
            {
                string sopath = Path.Combine(baseDirectory, operation.ToString() + "_" + String.Join("_", ids)) + ".so";
                StatusObject so = StatusObject.Load(sopath);
                if (so == null)
                    so = new StatusObject(sopath);
                return so;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// status file (plna cesta)
        /// </summary>
        public string _statusFile = string.Empty;

        #region Properties
        private string _statusText = string.Empty;
        /// <summary>
        /// Status zpracovani
        /// </summary>
        public string StatusText
        {
            get { return this._statusText; }
            set { this._statusText = value; }
        }

        private bool _exception = false;
        /// <summary>
        /// Pokud nastane nejaka vyjimka, tak nastavuje priznak pro ukonceni opreace na klientovi ...
        /// </summary>
        public bool Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }

        private DateTime _datumzapisu = DateTime.Now;
        public DateTime DatumZapisu
        {
            get { return _datumzapisu; }
            set { _datumzapisu = value; }
        }

        [System.ComponentModel.DefaultValue(false)]
        public bool Finished { get; set; }
        #endregion

        public StatusObject()
        {
        }

        public StatusObject(string statusFile)
            : this()
        {
            this._statusFile = statusFile;
        }

        public void SetOK()
        {
            StatusText = "OK";
            Finished = true;    //19.4.2016 JiS, StatusText="OK" => Finished=true
            Exception = false;
        }

        public void SetException(Exception ex)
        {
            this.StatusText = ex.Message;
            this.Exception = true;
        }

        /// <summary>
        /// Test, zda soubor v ulozisti existuje
        /// </summary>
        public bool Exists
        {
            get { return File.Exists(this._statusFile); }
        }

        /// <summary>
        /// Aktualizuje aktualni objekt ze souboru, ktery existuje v ulozisti
        /// </summary>
        /// <returns>True: pokud soubor existuje a byl nacten, False: soubor neexistuje a nebyl nacten</returns>
        public bool Read()
        {
            try
            {
                if (!File.Exists(this._statusFile))
                    return false;

                //StreamReader sr = new StreamReader(this._statusFile);
                //this.StatusText = sr.ReadToEnd();
                //sr.Close();
                //sr = null;

                StatusObject so = null;
                using (Stream stream = new FileStream(this._statusFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                {
                    IFormatter formatter = new BinaryFormatter();
                    so = (StatusObject)formatter.Deserialize(stream);
                }
                this.StatusText = so.StatusText;
                this.Exception = so.Exception;
                this.DatumZapisu = so.DatumZapisu;
                this.Finished = so.Finished;

                return true;
            }
            catch (Exception ex)
            {
                //Log.writeErrorLog("StatusRead: " + ex.Message);
                this.Exception = true;
                return false;
            }
        }

        /// <summary>
        /// Nacte ze souboru data a vytvori statusobject
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Pokud existuje, tak je nacten obsah, jinak vraci null</returns>
        public static StatusObject Load(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return null;

                //StatusObject so = null;
                //using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                //{
                //    IFormatter formatter = new BinaryFormatter();
                //    so = (StatusObject)formatter.Deserialize(stream);
                //}
                StatusObject so = new StatusObject(path);
                if (!so.Read()) //soubor nebyl nacten => neexistuje nebo nejaky problem...
                    return null;

                return so;
            }
            catch (Exception ex)
            {                
                //Log.writeErrorLog("StatusObjectLoad: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Nacte status ze souboru, pokud dojde k vyjimce, tak vrati prazdny StatusObject.StatusText
        /// </summary>
        /// <param name="statusFile"></param>
        /// <returns></returns>
        [Obsolete("Nahrazeno metodou static Load(path) => ktera vraci null, pokud nenalezen, nenacten", true)]
        private static StatusObject Get(string statusFile)
        {
            try
            {
                StatusObject so = new StatusObject(statusFile);
                so.Read();
                return so;
            }
            catch (Exception ex)
            {
                //Log.writeErrorLog("StatusObject.Get(" + statusFile + "): " + ex.Message);
                StatusObject so = new StatusObject(statusFile);
                return so;
            }
        }

        /// <summary>
        /// Zapise aktualni status object do status file
        /// </summary>
        /// <returns></returns>
        public bool Write()
        {
            try
            {
                this.DatumZapisu = DateTime.Now;

                if (!System.IO.Directory.Exists(Path.GetDirectoryName(this._statusFile)))
                    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(this._statusFile));

                //StreamWriter sw = new StreamWriter(this._statusFile, false);
                //sw.Write(this.StatusText);
                //sw.Close();
                //sw = null;

                //Stream stream = new FileStream(this._statusFile, FileMode.Create, FileAccess.Write, FileShare.Read);
                using (Stream stream = new FileStream(this._statusFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite | FileShare.Delete))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                }
                //stream.Flush();
                //stream.Close();

                return true;
            }
            catch (Exception ex)
            {
                //Log.writeErrorLog("StatusWrite: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Zapise do uloziste status file s uvedenym statustextem...
        /// </summary>
        /// <param name="statusText"></param>
        /// <returns></returns>
        public bool Write(string statusText)
        {
            this.StatusText = statusText;
            return this.Write();
        }

        /// <summary>
        /// Smaze z uloziste status file
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            if (File.Exists(this._statusFile))
                File.Delete(this._statusFile);
            return true;
        }
    }
}
