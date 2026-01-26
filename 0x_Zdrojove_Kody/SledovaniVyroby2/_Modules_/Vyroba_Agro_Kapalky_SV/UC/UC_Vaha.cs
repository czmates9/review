using Fask.Logging;
using FASK.SledovaniVyroby.ErrorLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Kapalky_SV.UC
{
    public partial class UC_Vaha : UserControl
    {
        #region Promenne 

        public string L_1_text
        {
            set
            {
                L_1_value.Text = value;
               
            }
            get
            {
                return L_1_value.Text;
            }
        }

        public string L_3_text
        {
            set
            {
                L_3_value.Text = value;

            }
            get
            {
                return L_3_value.Text;
            }
        }

        private bool _Vaha_1_Connected;
        public bool Vaha_1_Connected()
        {
                return _Vaha_1_Connected;
            
        }

        public bool Vaha_1_value
        {
            get
            {
                if (_Vaha_1_Connected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion


        public UC_Vaha()
        {
            InitializeComponent();
            GB_value.Text = "Váhy nastavení";
            _Vaha_1_Connected = true;
            L_1_value.Text = "Váha 1: ";
            BT_1_value.Text = "ON";
            BT_3_value.Text = "OFF";

        }

        public void updateForm()
        {
            try
            {
                if (this.InvokeRequired)
                {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                    this.BeginInvoke(new MethodInvoker(() => { updateForm(); }));
                    return;
                }


                if (_Vaha_1_Connected)
                {
                    L_3_value.Text = "Připojena";
                    L_3_value.ForeColor = Color.Green;
                    BT_1_value.Enabled = false;
                    BT_3_value.Enabled = true;
                }
                else
                {
                    L_3_value.Text = "Odpojena";
                    L_3_value.ForeColor = Color.Red;
                    BT_1_value.Enabled = true;
                    BT_3_value.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex.Message, "ex.Message>updateForm()");
                // Log.Write(ex.StackTrace, "ex.StackTrace>ex.updateForm()");
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        private void UC_Vaha_Load(object sender, EventArgs e)
        {
            updateForm();
        }

        private void BT_1_value_Click(object sender, EventArgs e)
        {
            _Vaha_1_Connected = true;
            updateForm();
        }

         private void BT_3_value_Click(object sender, EventArgs e)
        {
            _Vaha_1_Connected = false;
            updateForm();
        }
    }
}
