using Symbol.RFID3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.IRFIDProvider
{
    

    public interface IRFIDProvider
    {

        int Count_Write_Pruchody { get; set; }

        void Start();
        void Stop();

        void init(string IP, uint PORT);

        void Start_Read_tags(List<ushort> anteny, int? cisloLinky, string volajici);

        void Stop_Read_tags();

        bool Write_tags(string EPC_zdroj,string EPC_cil, List<ushort> anteny);


        bool isOpen();

        bool PerformTagLocationing(string tagId, int cisloAnteny, int dobaLokalizace);

        string Info_DLL();

        event RFIDHandler DataReady;
        event RFID_ZEBRA_Handler DataReadyZEBRA;
    }

    public delegate void RFIDHandler(object sender, RFIDEventArgs e);
    public delegate void RFID_ZEBRA_Handler(object sender, RFID_ZABRA_EventArgs e);


    public class RFIDEventArgs : EventArgs
    {
        public RFIDEventArgs(List<string> tagIDs)
        {
            _tagIDs = tagIDs;
        }

        private List<string> _tagIDs = null;
        public List<string> TagIDs
        {
            get { return _tagIDs; }

        }
    }

    public class RFID_ZABRA_EventArgs : EventArgs
    {
        private int? _cisloLinky = null;
        public int? CisloLinky
        {
            get { return _cisloLinky; }
            set { _cisloLinky = value; }
        }

        private string _volajici = null;
        public string Volajici
        {
            get { return _volajici; }
            set { _volajici = value; }
        }


        public RFID_ZABRA_EventArgs(List<Item_Tag> tagIDs)
        {
            _tagIDs = tagIDs;
        }

        public RFID_ZABRA_EventArgs(List<Item_Tag> tagIDs, int? CisloLinky)
        {
            _tagIDs = tagIDs;
            _cisloLinky = CisloLinky;
        }

        public RFID_ZABRA_EventArgs(List<Item_Tag> tagIDs, int? CisloLinky, string volajici)
        {
            _tagIDs = tagIDs;
            _cisloLinky = CisloLinky;
            _volajici = volajici;
        }

        private List<Item_Tag> _tagIDs = null;
        public List<Item_Tag> TagIDs
        {
            get { return _tagIDs; }

        }
    }

    public class Item_Tag
    {
        /// <summary>
        /// Jedná se o EPC pamet
        /// </summary>
        private string _epc;
        public string EPC
        {
            get { return _epc; }
            set { _epc = value; }
        }


        /// <summary>
        /// Jedná se o ID Anteny
        /// </summary>
        private string _id_antena;
        public string ID_Antena
        {
            get { return _id_antena; }
            set { _id_antena = value; }
        }


    }

    public class Bag_Tags : Item_Tag
    {

        //public List<Item_Tag>

        /// <summary>
        /// Jedná se o ID Tagu
        /// </summary>
        private Item_Tag _id_tag;
        public Item_Tag ID_Tag
        {
            get { return _id_tag; }
            set { _id_tag = value; }
        }

        /// <summary>
        /// Jedná se o pocet nacteni
        /// </summary>
        private int _countTag;
        public int CountTag
        {
            get { return _countTag; }
            set { _countTag = value; }
        }

    }

}
