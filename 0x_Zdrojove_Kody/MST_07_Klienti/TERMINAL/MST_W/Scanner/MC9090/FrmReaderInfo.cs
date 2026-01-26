using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using System.IO;
namespace Fask.MST_W.Scanner.MC9090
{
    public partial class FrmReaderInfo : System.Windows.Forms.Form
    {
        Hashtable htReader;
        public FrmReaderInfo( Hashtable htReaderInfo)
        {
            InitializeComponent();
            htReader = htReaderInfo;
        }

        private void ReaderInfo_Load(object sender, EventArgs e)
        {
            if(htReader.ContainsKey("DeviceSerialNumber"))
                 m_txtSerNo.Text = htReader["DeviceSerialNumber"].ToString();
            
            //if (htReader.ContainsKey("DeviceModelNumber"))
            //   m_txtModNo.Text =  htReader["DeviceModelNumber"].ToString();
            
            //if (htReader.ContainsKey("ManufacturerName"))
            //    m_txtManuf.Text = htReader["ManufacturerName"].ToString();
            
            //if (htReader.ContainsKey("ManufactureDate"))
            //    m_txtManufDate.Text = htReader["ManufactureDate"].ToString();
            
            //if (htReader.ContainsKey("HardwareVersion"))
            //    m_txtHWVersion.Text = htReader["HardwareVersion"].ToString();
           
            //if (htReader.ContainsKey("BootLoaderVersion"))
            //    m_txtBLVersion.Text = htReader["BootLoaderVersion"].ToString(); 
            
            if (htReader.ContainsKey("FirmwareVersion"))
                m_txtFWVersion.Text = htReader["FirmwareVersion"].ToString();

            if (htReader.ContainsKey("SymbolSDKVersion"))
                txtSDKVersion.Text = htReader["SymbolSDKVersion"].ToString();

            if (htReader.ContainsKey("Model"))
               m_txtModNo.Text =  htReader["Model"].ToString();

            btnOk.Focus();
            
        }
    
    
    }
}