using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID2;
namespace Fask.MST_W.Scanner.MC9090
{
    public partial class FrmSetAntenna : System.Windows.Forms.Form
    {
        public FrmSetAntenna(AntennaConfig[] AntennaConfigArray)
        {
            InitializeComponent();
            InitializeAntennaNames(AntennaConfigArray);
        }

        bool bValidate = true;
        internal int m_txMin = 0;
        internal int m_txMax = 30;

        internal bool setAntennaConfig = false;
        internal int configAntennaIndex = 0;

        private void InitializeAntennaNames(AntennaConfig[] AntennaConfigArray)
        {
            m_allantConfig = AntennaConfigArray;

            if (AntennaConfigArray.Length > 0)
            {
                int count = 0;
                foreach (AntennaConfig antenna in AntennaConfigArray)
                    comboBox1.Items.Add("Antenna " + (++count));
            }
            else
                throw new Exception("No Antenna Found!");
        }

        private AntennaConfig m_antConfig;
        private AntennaConfig[] m_allantConfig;

        public AntennaConfig SetAntenna
        {
            get
            {
                return m_antConfig;
            }
        }

        public int ConfigAntennaIndex
        {
            get
            {
                return configAntennaIndex;
            }
        }

        public AntennaConfig[] SetAllAntenna
        {
            get
            {
                return m_allantConfig;
            }
        }

        private void FrmSetAntenna_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void GetConfigurationDetails()
        {
            try
            {
                int index = 0;

                foreach (AntennaConfig config in m_allantConfig)
                {

                    if (index == comboBox1.SelectedIndex)
                    {
                        txtTxPower.Text = config.TxPower.ToString();

                        if (config.IsEnabled)
                            chkConnected.Checked = true;
                        else
                            chkConnected.Checked = false;

                        ArrayList tagTypesInUseList = new ArrayList(config.TagTypesInUse);

                        if (tagTypesInUseList.Contains(TagType.EPCClass0))
                            chkClass0.Checked = true;
                        else
                            chkClass0.Checked = false;

                        if (tagTypesInUseList.Contains(TagType.EPCClass1))
                            chkClass1.Checked = true;
                        else
                            chkClass1.Checked = false;

                        if (tagTypesInUseList.Contains(TagType.EPCClass1_GEN2))
                            chkGen2.Checked = true;
                        else
                            chkGen2.Checked = false;
                    }

                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private bool Validate()
        {
            try
            {
                if (int.Parse(txtTxPower.Text.ToString()) > m_txMax || int.Parse(txtTxPower.Text.ToString()) < m_txMin)
                {
                    MessageBox.Show("Enter Tx Power between " + m_txMin + "-" + m_txMax, this.Text);
                    return false;
                }
                else
                    return true;
            }
            catch
            {
                MessageBox.Show("Enter Tx Power between " + m_txMin + "-" + m_txMax, this.Text);
                return false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!Validate())
            {
                bValidate = false;
                return;
            }
            try
            {
                ArrayList tagTypeList = new ArrayList();

                if (chkClass0.Checked)
                    tagTypeList.Add(TagType.EPCClass0);

                if (chkClass1.Checked)
                    tagTypeList.Add(TagType.EPCClass1);

                if (chkGen2.Checked)
                    tagTypeList.Add(TagType.EPCClass1_GEN2);

                //m_antConfig = new AntennaConfig(comboBox1.SelectedItem.ToString(), (TagType[])tagTypeList.ToArray(typeof(TagType)),
                //               Convert.ToUInt32(txtTxPower.Text.ToString()), 0,chkConnected.Checked,
                //               chkConnected.Checked);

                int index = 0;
                foreach (AntennaConfig config in m_allantConfig)
                {
                    if (index == comboBox1.SelectedIndex)
                    {
                        m_antConfig = config;
                        configAntennaIndex = index;
                        break;
                    }

                    index++;
                }

                m_antConfig.TagTypesInUse = (TagType[])tagTypeList.ToArray(typeof(TagType));
                m_antConfig.TxPower = (byte)Convert.ToUInt32(txtTxPower.Text);
                m_antConfig.IsEnabled = chkConnected.Checked;
                               
                bValidate = true;
                setAntennaConfig = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CS_RFID2_Sample");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bValidate = true;
            setAntennaConfig = false;
            this.Close();
        }

        private void FrmSetAntenna_Closing(object sender, CancelEventArgs e)
        {
            if (!bValidate)
                e.Cancel = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetConfigurationDetails();
        }
    }
}