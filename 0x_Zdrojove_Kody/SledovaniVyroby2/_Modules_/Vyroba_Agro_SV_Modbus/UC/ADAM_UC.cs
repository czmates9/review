using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_SV_Modbus.UC
{
    public partial class ADAM_UC : UserControl
    {

        //enum Input
        //{
        //    Linka1_RUN,
        //    Linka1_ok,
        //    Linka1_END


        //}

        public bool _adamTCP_Connected;
        public bool AdamTCP_Connected
        { 
            set { _adamTCP_Connected = value; }
        }

        public bool _adamP2P_Started;
        public bool AdamP2P_Started
        {
            set { _adamP2P_Started = value; }
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

        public bool[] DIData = null;

        public bool _adam_Test;
        public bool Adam_Test
        {
            set { _adam_Test = value; }
        }

        #region promenne DI popisek

        public string _DI0_popis;
        public string DI0_popis
        {
            set
            {
                _DI0_popis = value;
                lblDI0_text.Text = _DI0_popis;
            }
        }
        public string _DI1_popis;
        public string DI1_popis
        {
            set
            {
                _DI1_popis = value;
                lblDI1_text.Text = _DI1_popis;
            }
        }
        public string _DI2_popis;
        public string DI2_popis
        {
            set
            {
                _DI2_popis = value;
                lblDI2_text.Text = _DI2_popis;
            }
        }
        public string _DI3_popis;
        public string DI3_popis
        {
            set
            {
                _DI3_popis = value;
                lblDI3_text.Text = _DI3_popis;
            }
        }
        public string _DI4_popis;
        public string DI4_popis
        {
            set
            {
                _DI4_popis = value;
                lblDI4_text.Text = _DI4_popis;
            }
        }
        public string _DI5_popis;
        public string DI5_popis
        {
            set
            {
                _DI5_popis = value;
                lblDI5_text.Text = _DI5_popis;
            }
        }
        public string _DI6_popis;
        public string DI6_popis
        {
            set
            {
                _DI6_popis = value;
                lblDI6_text.Text = _DI6_popis;
            }
        }
        public string _DI7_popis;
        public string DI7_popis
        {
            set
            {
                _DI7_popis = value;
                lblDI7_text.Text = _DI7_popis;
            }
        }
        public string _DI8_popis;
        public string DI8_popis
        {
            set
            {
                _DI8_popis = value;
                lblDI8_text.Text = _DI8_popis;
            }
        }
        public string _DI9_popis;
        public string DI9_popis
        {
            set
            {
                _DI9_popis = value;
                lblDI9_text.Text = _DI9_popis;
            }
        }
        public string _DI10_popis;
        public string DI10_popis
        {
            set
            {
                _DI10_popis = value;
                lblDI10_text.Text = _DI10_popis;
            }
        }
        public string _DI11_popis;
        public string DI11_popis
        {
            set
            {
                _DI11_popis = value;
                lblDI11_text.Text = _DI11_popis;
            }
        } 
        #endregion

        //List<string> l = new List<string>();
        //Dictionary<Input, bool> dic = new Dictionary<Input, bool>();

        public ADAM_UC()
        {
            InitializeComponent();

            if(string.IsNullOrEmpty(_DI0_popis))
            lblDI0_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI1_popis))
                lblDI1_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI2_popis))
                lblDI2_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI3_popis))
                lblDI3_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI4_popis))
                lblDI4_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI5_popis))
                lblDI5_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI6_popis))
                lblDI6_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI7_popis))
                lblDI7_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI8_popis))
                lblDI8_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI9_popis))
                lblDI9_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI10_popis))
                lblDI10_text.Text = string.Empty;
            if (string.IsNullOrEmpty(_DI11_popis))
                lblDI11_text.Text = string.Empty;

            //dic.Add(Input.Linka1_END, "1");
            //var x = l[0];
            //var s1end = dic["Linka1_END"];

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
                 
                        if (_adamTCP_Connected)
                        {
                            labelAdamStavTCP.Text = "Připojen";
                            labelAdamStavTCP.ForeColor = Color.DarkGreen;
                        }
                        else
                        {
                            labelAdamStavTCP.Text = "Odpojen";
                            labelAdamStavTCP.ForeColor = Color.DarkRed;
                        }

                        if (_adamP2P_Started)
                        {
                            labelAdamStavP2P.Text = "Spuštěn";
                            labelAdamStavP2P.ForeColor = Color.DarkGreen;
                        }
                        else
                        {
                            labelAdamStavP2P.Text = "Zastaven";
                            labelAdamStavP2P.ForeColor = Color.DarkRed;
                        }

                        // vytazeni dat z adam
                        //DIData = adam.DIStatusLast;
                        // DOData = adam.DOStatusLast;
                        // DIValues = adam.DIValuesLast;
                    
                }
                catch (Exception ex)
                {
                    // TODO : vyjimka !!!
                    //Log.Write(ex.Message.ToString());
                    //Exceptions.Handler.ErrorHandle(ex.Message, "frmMainAgroVyroba.RefreshUI");
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }
                #endregion

                #region Zobrazeni hodnot z adam
                //try
                //{
                //    if ((DIValues != null) && (DIValues.Length > 0))
                //    {
                //        lCounter0.Text = DIValues[0].ToString();
                //        lCounter1.Text = DIValues[1].ToString();
                //        lCounter2.Text = DIValues[2].ToString();
                //        lCounter3.Text = DIValues[3].ToString();
                //        lCounter4.Text = DIValues[4].ToString();
                //        lCounter5.Text = DIValues[5].ToString();
                //    }
                //}
                //catch
                //{
                //}

                if (DIData != null && DIData.Length > 0)
                {
                    #region nastaveni labelu
                    if (DIData[0])
                    {
                        lblDI0.Text = "1";
                        lblDI0.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI0.Text = "0";
                        lblDI0.ForeColor = Color.Red;
                    }

                    if (DIData[1])
                    {
                        lblDI1.Text = "1";
                        lblDI1.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI1.Text = "0";
                        lblDI1.ForeColor = Color.Red;
                    }

                    if (DIData[2])
                    {
                        lblDI2.Text = "1";
                        lblDI2.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI2.Text = "0";
                        lblDI2.ForeColor = Color.Red;
                    }

                    if (DIData[3])
                    {
                        lblDI3.Text = "1";
                        lblDI3.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI3.Text = "0";
                        lblDI3.ForeColor = Color.Red;
                    }

                    if (DIData[4])
                    {
                        lblDI4.Text = "1";
                        lblDI4.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI4.Text = "0";
                        lblDI4.ForeColor = Color.Red;
                    }


                    if (DIData[5])
                    {
                        lblDI5.Text = "1";
                        lblDI5.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI5.Text = "0";
                        lblDI5.ForeColor = Color.Red;
                    }
                    if (DIData[6])
                    {
                        lblDI6.Text = "1";
                        lblDI6.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI6.Text = "0";
                        lblDI6.ForeColor = Color.Red;
                    }
                    if (DIData[7])
                    {
                        lblDI7.Text = "1";
                        lblDI7.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI7.Text = "0";
                        lblDI7.ForeColor = Color.Red;
                    }

#if true
                    if (DIData[8])
                    {
                        lblDI8.Text = "1";
                        lblDI8.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI8.Text = "0";
                        lblDI8.ForeColor = Color.Red;
                    }
                    if (DIData[9])
                    {
                        lblDI9.Text = "1";
                        lblDI9.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI9.Text = "0";
                        lblDI9.ForeColor = Color.Red;
                    }
                    if (DIData[10])
                    {
                        lblDI10.Text = "1";
                        lblDI10.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI10.Text = "0";
                        lblDI10.ForeColor = Color.Red;
                    }
                    if (DIData[11])
                    {
                        lblDI11.Text = "1";
                        lblDI11.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI11.Text = "0";
                        lblDI11.ForeColor = Color.Red;
                    } 
#endif

                    #endregion

                }
                #region old
                //if (DOData != null && DOData.Length > 0)
                //{
                //    #region nastaveni labelu
                //    if (DOData[0])
                //    {
                //        lblRL0.Text = "1";
                //        lblRL0.ForeColor = Color.Green;
                //    }
                //    else
                //    {
                //        lblRL0.Text = "0";
                //        lblRL0.ForeColor = Color.Red;
                //    }

                //    if (DOData[1])
                //    {
                //        lblRL1.Text = "1";
                //        lblRL1.ForeColor = Color.Green;
                //    }
                //    else
                //    {
                //        lblRL1.Text = "0";
                //        lblRL1.ForeColor = Color.Red;
                //    }

                //    if (DOData[2])
                //    {
                //        lblRL2.Text = "1";
                //        lblRL2.ForeColor = Color.Green;
                //    }
                //    else
                //    {
                //        lblRL2.Text = "0";
                //        lblRL2.ForeColor = Color.Red;
                //    }

                //    if (DOData[3])
                //    {
                //        lblRL3.Text = "1";
                //        lblRL3.ForeColor = Color.Green;
                //    }
                //    else
                //    {
                //        lblRL3.Text = "0";
                //        lblRL3.ForeColor = Color.Red;
                //    }

                //    if (DOData[4])
                //    {
                //        lblRL4.Text = "1";
                //        lblRL4.ForeColor = Color.Green;
                //    }
                //    else
                //    {
                //        lblRL4.Text = "0";
                //        lblRL4.ForeColor = Color.Red;
                //    }


                //    if (DOData[5])
                //    {
                //        lblRL5.Text = "1";
                //        lblRL5.ForeColor = Color.Green;
                //    }
                //    else
                //    {
                //        lblRL5.Text = "0";
                //        lblRL5.ForeColor = Color.Red;
                //    }
                //    #endregion
                //}
                #endregion
                #endregion

                #endregion


            }
            catch (Exception ex)
            {
                //Log.Write(ex.Message, "ex.Message>updateForm()");
               // Log.Write(ex.StackTrace, "ex.StackTrace>ex.updateForm()");
                throw ex;
            }
        }
    }
}
