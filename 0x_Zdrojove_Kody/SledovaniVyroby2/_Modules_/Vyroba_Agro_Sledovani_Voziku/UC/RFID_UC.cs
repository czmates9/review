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

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.UC
{
    public partial class RFID_UC : UserControl
    {
        public string Tag_Text_W
        {
            set
            {
                l_Tag_Value_W.Text = value;
                if(value.Length == 24)
                {
                    l_Tag_Value_W.ForeColor = Color.Orange;
                }
            }
            get 
            {
                return l_Tag_Value_W.Text;
            }
        }

        public string Tag_Text_R
        {
            set
            {
                l_Tag_Value_R.Text = value;
                if (value.Length == 24)
                {
                    l_Tag_Value_R.ForeColor = Color.Orange;
                }
            }
            get
            {
                return l_Tag_Value_W.Text;
            }
        }


        private string GetText(bool stav)
        {
            if (stav)
                return "1";
            else
                return "0";
        }


        public bool _RFID_1_Connected;
        public bool RFID_1_Connected
        {
            set { //_RFID_1_Connected = value;
                if (GetText(value) == "1")
                {
                    l_R_L.Text = "Připojen";
                    l_R_L.ForeColor = Color.Green;
                }
                else
                {
                    l_R_L.Text = "Odpojen";
                    l_R_L.ForeColor = Color.Red;
                }
            }
        }

        public bool _RFID_2_Connected;
        public bool RFID_2_Connected
        {
            set { _RFID_2_Connected = value;
                if (GetText(value) == "1")
                {
                    l_R_V.Text = "Připojen";
                    l_R_V.ForeColor = Color.Green;
                }
                else
                {
                    l_R_V.Text = "Odpojen";
                    l_R_V.ForeColor = Color.Red;
                }
            
            }
        }

        public bool _chB_R1_ANT1;
        public bool ChB_R1_ANT1
        {
            set
            {
                if (value)
                {
                    _chB_R1_ANT1 = true;
                }
                else
                {
                    _chB_R1_ANT1 = false;
                }

            }
        }
        public bool _chB_R1_ANT2;
        public bool ChB_R1_ANT2
        {
            set
            {
                if (value)
                {
                    _chB_R1_ANT2 = true;
                }
                else
                {
                    _chB_R1_ANT2 = false;
                }

            }
        }
        public bool _chB_R1_ANT3;
        public bool ChB_R1_ANT3
        {
            set
            {
                if (value)
                {
                    _chB_R1_ANT3 = true;
                }
                else
                {
                    _chB_R1_ANT3 = false;
                }

            }
        }
        public bool _chB_R1_ANT4;
        public bool ChB_R1_ANT4
        {
            set
            {
                if (value)
                {
                    _chB_R1_ANT4 = true;
                }
                else
                {
                    _chB_R1_ANT4 = false;
                }

            }
        }

        public bool _chB_R2_ANT1;
        public bool ChB_R2_ANT1
        {
            set
            {
                if (value)
                {
                    _chB_R2_ANT1 = true;
                }
                else
                {
                    _chB_R2_ANT1 = false;
                }

            }
        }
        public bool _chB_R2_ANT2;
        public bool ChB_R2_ANT2
        {
            set
            {
                if (value)
                {
                    _chB_R2_ANT2 = true;
                }
                else
                {
                    _chB_R2_ANT2 = false;
                }

            }
        }
        public bool _chB_R2_ANT3;
        public bool ChB_R2_ANT3
        {
            set
            {
                if (value)
                {
                    _chB_R2_ANT3 = true;
                }
                else
                {
                    _chB_R2_ANT3 = false;
                }

            }
        }
        public bool _chB_R2_ANT4;
        public bool ChB_R2_ANT4
        {
            set
            {
                if (value)
                {
                    _chB_R2_ANT4 = true;
                }
                else
                {
                    _chB_R2_ANT4 = false;
                }

            }
        }

        public RFID_UC()
        {
            InitializeComponent();
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

                #region Anteny checkBox
                #region Anteny leva strana
                chB1_ANT_1.Enabled = false;
                if (_chB_R1_ANT1)
                {
                    chB1_ANT_1.Checked = true;
                    chB1_ANT_1.BackColor = Color.Orange;
                }
                chB1_ANT_2.Enabled = false;
                if (_chB_R1_ANT2)
                {
                    chB1_ANT_2.Checked = true;
                    chB1_ANT_2.BackColor = Color.Orange;
                }
                chB1_ANT_3.Enabled = false;
                if (_chB_R1_ANT3)
                {
                    chB1_ANT_3.Checked = true;
                    chB1_ANT_3.BackColor = Color.Orange;
                }
                chB1_ANT_4.Enabled = false;
                if (_chB_R1_ANT4)
                {
                    chB1_ANT_4.Checked = true;
                    chB1_ANT_4.BackColor = Color.Orange;
                }
                #endregion

                #region Anteny prava strana
                chB2_ANT_1.Enabled = false;
                if (_chB_R2_ANT1)
                {
                    chB2_ANT_1.Checked = true;
                    chB2_ANT_1.BackColor = Color.Orange;
                }
                chB2_ANT_2.Enabled = false;
                if (_chB_R2_ANT2)
                {
                    chB2_ANT_2.Checked = true;
                    chB2_ANT_2.BackColor = Color.Orange;
                }
                chB2_ANT_3.Enabled = false;
                if (_chB_R2_ANT3)
                {
                    chB2_ANT_3.Checked = true;
                    chB2_ANT_3.BackColor = Color.Orange;
                }
                chB2_ANT_4.Enabled = false;
                if (_chB_R2_ANT4)
                {
                    chB2_ANT_4.Checked = true;
                    chB2_ANT_4.BackColor = Color.Orange;
                }
                #endregion

                #endregion




            }
            catch (Exception ex)
            {
                //Log.Write(ex.Message, "ex.Message>updateForm()");
                // Log.Write(ex.StackTrace, "ex.StackTrace>ex.updateForm()");
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        private void RFID_UC_Load(object sender, EventArgs e)
        {
            updateForm();
        }
    }

}

