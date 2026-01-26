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
    public partial class DI_UC : UserControl
    {
        public DI_UC()
        {
            InitializeComponent();
        }

        public bool _lab_value1;
        public bool Lab_value1
        {
            set 
            {
                _lab_value1 = value;
              
            }
        }

        public bool _lab_value2;
        public bool Lab_value2
        {
            set { _lab_value2 = value; }
        }

        public bool _lab_value3;
        public bool Lab_value3
        {
            set { _lab_value3 = value; }
        }

        public bool _lab_value4;
        public bool Lab_value4
        {
            set { _lab_value4 = value; }
        }

        public string _gb_value;
        public string Gb_value
        {
            set 
            { 
                _gb_value = value;
                GB_value.Text = _gb_value;
            }
        }

        public string _lab_text1;
        public string Lab_text1
        {
            set 
            {
                _lab_text1 = value;
                lab_name_1.Text = _lab_text1;
            }
        }

        public string _lab_text2;
        public string Lab_text2
        {
            set
            {
                _lab_text2 = value;
                lab_name_2.Text = _lab_text2;
            }
        }

        public string _lab_text3;
        public string Lab_text3
        {
            set
            {
                _lab_text3 = value;
                lab_name_3.Text = _lab_text3;
            }
        }

        public string _lab_text4;
        public string Lab_text4
        {
            set
            {
                _lab_text4 = value;
                lab_name_4.Text = _lab_text4;
            }
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

                #region ADAM HW

                #region Zjisteni savu ADAM
                //bool[] DIData = null;
                // bool[] DOData = null;
                // int[] DIValues = null;

                try
                {
                    
                    

                    if (_lab_value1)
                    {
                        l_value_1.Text = "1";
                        l_value_1.ForeColor = Color.Green;
                    }
                    else
                    {
                        l_value_1.Text = "0";
                        l_value_1.ForeColor = Color.Red;
                    }

                    if (_lab_value2)
                    {
                        l_value_2.Text = "1";
                        l_value_2.ForeColor = Color.Green;
                    }
                    else
                    {
                        l_value_2.Text = "0";
                        l_value_2.ForeColor = Color.Red;
                    }

                    if (_lab_value3)
                    {
                        l_value_3.Text = "1";
                        l_value_3.ForeColor = Color.Green;
                    }
                    else
                    {
                        l_value_3.Text = "0";
                        l_value_3.ForeColor = Color.Red;
                    }

                    if (_lab_value4)
                    {
                        l_value_4.Text = "1";
                        l_value_4.ForeColor = Color.Green;
                    }
                    else
                    {
                        l_value_4.Text = "0";
                        l_value_4.ForeColor = Color.Red;
                    }



                }
                catch (Exception ex)
                {
                    // TODO : vyjimka !!!
                    //Log.Write(ex.Message.ToString());
                    Exceptions.Handler.ErrorHandle(ex.Message, "frmMainAgroVyroba.RefreshUI");
                }
                #endregion

                #region Zobrazeni hodnot z adam
                
                #endregion

                #endregion


            }
            catch (Exception ex)
            {
                //Log.Write(ex.Message, "ex.Message>updateForm()");
                // Log.Write(ex.StackTrace, "ex.StackTrace>ex.updateForm()");
                ExceptionHandler2.Handle(ex);
            }
        }

        private void DI_UC_Load(object sender, EventArgs e)
        {
            updateForm();
        }
    }
}
