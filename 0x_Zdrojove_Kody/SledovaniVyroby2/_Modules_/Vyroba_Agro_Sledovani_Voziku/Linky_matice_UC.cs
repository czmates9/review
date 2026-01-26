using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku
{
    public partial class Linky_matice_UC : UserControl
    {
        public Linky_matice_UC()
        {
            InitializeComponent();
        }

        #region Matice
        public bool L1
        {
            set
            {
                label_L1_value.Text = GetText(value);
                if (GetText(value) == "1")
                    label_L1_value.ForeColor = Color.Green;
                else
                    label_L1_value.ForeColor = Color.Red;
            }
        }

        public bool L2
        {
            set
            {
                label_L2_value.Text = GetText(value);
                if (GetText(value) == "1")
                    label_L2_value.ForeColor = Color.Green;
                else
                    label_L2_value.ForeColor = Color.Red;
            }
        }

        public bool L3
        {
            set
            {
                label_L3_value.Text = GetText(value);
                if (GetText(value) == "1")
                    label_L3_value.ForeColor = Color.Green;
                else
                    label_L3_value.ForeColor = Color.Red;
            }
        }

        public bool L4
        {
            set
            {
                label_L4_value.Text = GetText(value);
                if (GetText(value) == "1")
                    label_L4_value.ForeColor = Color.Green;
                else
                    label_L4_value.ForeColor = Color.Red;
            }
        }

        public string Pozice
        {
            set
            {
                //lab_pozice_Voz.TextAlign = BottomLeft;
                lab_pozice_Voz.Text = value;
             
            }
        }

        #endregion


        private string GetText(bool stav)
        {
            if (stav)
                return "1";
            else
                return "0";
        }
    }
}
