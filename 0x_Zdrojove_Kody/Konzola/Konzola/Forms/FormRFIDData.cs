using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konzola.Forms
{
    public partial class FormRFIDData : Form
    {
        public IRFIDProvider.IRFIDProvider RFID = null;

        public FormRFIDData()
        {
            InitializeComponent();

            RFID = RFIDFactory.RFIDFactory.Init();

            RFIDStart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Timer_Test_.Enabled = !Timer_Test_.Enabled;
                if (RFID.isOpen())
                {
                    RFIDStop(); // CloseRFID();

                }
                else
                {
                    RFIDStart(); // OpenRFID();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        private void RFIDStop()
        {
            try
            {
                if (RFID != null)
                {
                    RFID.DataReady -= new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.Stop();
                    btn_RFID.BackColor = Color.Red;
                    btn_RFID.Text = "Start Read";
                }

            }
            catch (Exception ex)
            {
                btn_RFID.BackColor = Color.Red;
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        private void RFIDStart()
        {
            try
            {
                if (RFID != null)
                {
                    RFID.DataReady -= new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.DataReady += new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.Start();

                    btn_RFID.BackColor = Color.Green;
                    btn_RFID.Text = "Stop Read";
                }

            }
            catch (Exception ex)
            {
                btn_RFID.BackColor = Color.Red;
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        void RFID_DataReady(object sender, IRFIDProvider.RFIDEventArgs e)
        {
            AddData(e.TagIDs);
        }


        public void AddData(List<string> list)
        {
            if (list == null)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { this.AddData(list); });
                return;
            }

            try
            {
                List<string> l = list;
                //nasnimaneKody.AddRange(list);
                //PridejNasnimanePolozky();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void FormRFIDData_FormClosed(object sender, FormClosedEventArgs e)
        {
            RFIDStop();
        }
    }
}
