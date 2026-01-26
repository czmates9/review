using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Symbol.RFID2;
namespace Fask.MST_W.Scanner.MC9090
{
    public partial class FrmReadAntennaInfo : System.Windows.Forms.Form
    {
        public FrmReadAntennaInfo(AntennaConfig[] antennaArray)
        {
            InitializeComponent();

            m_antConfig = antennaArray;

            if (antennaArray.Length == 0)
                throw new Exception("No Antenna Found!");
        }
        
        private AntennaConfig[] m_antConfig;

        public AntennaConfig[] GetAntenna
        {
            get
            {
                return m_antConfig;
            }
        }

        private void FrmReadAntennaInfo_Load(object sender, EventArgs e)
        {
            try
            {
                int index = 0;

                foreach (AntennaConfig antenna in m_antConfig)
                {
                    cmbAntennaName.Items.Add("Antenna " + (++index));
                    
                }
                cmbAntennaName.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CS_RFID2_Sample");
            }

        }


        private void cmbAntennaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = 0;

                foreach (AntennaConfig config in m_antConfig)
                {
                    if (index == cmbAntennaName.SelectedIndex)
                    {
                        txtTxPower.Text = config.TxPower.ToString();

                        if (config.IsEnabled)
                            txtConnected.Text = "Connected";
                        else
                            txtConnected.Text = "Not Connected";

                        ArrayList tagTypesInUseList = new ArrayList(config.TagTypesInUse);

                        if (tagTypesInUseList.Contains(TagType.EPCClass0))
                            txtClass0.Text = "Supported";
                        else
                            txtClass0.Text = "Not Supported";

                        if (tagTypesInUseList.Contains(TagType.EPCClass1))
                            txtClass1.Text = "Supported";
                        else
                            txtClass1.Text = "Not Supported";

                        if (tagTypesInUseList.Contains(TagType.EPCClass1_GEN2))
                            txtClass1gen2.Text = "Supported";
                        else
                            txtClass1gen2.Text = "Not Supported";                        
                    }

                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CS_RFID2_Sample");
            }
        }
    }
}