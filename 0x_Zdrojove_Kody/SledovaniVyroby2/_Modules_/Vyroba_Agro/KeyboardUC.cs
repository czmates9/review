using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro
{
    public partial class KeyboardUC : UserControl
    {
        private InformationUC op = null;

        public KeyboardUC()
        {
            InitializeComponent();

            this.AutoSize = true;
            this.Dock = DockStyle.Fill;
        }

        public KeyboardUC(InformationUC operace) :this()
        {
            this.op = operace;

            
            this.BtnEnter.DataBindings.Add("Text", op.klavesniceText, "BtnEnter", false, DataSourceUpdateMode.OnPropertyChanged);
            this.btn19.DataBindings.Add("Text", op.klavesniceText, "Btn19", false, DataSourceUpdateMode.OnPropertyChanged);
            this.btnF1.DataBindings.Add("Text", op.klavesniceText, "BtnF1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.btnF2.DataBindings.Add("Text", op.klavesniceText, "BtnF2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.btnF3.DataBindings.Add("Text", op.klavesniceText, "BtnF3", false, DataSourceUpdateMode.OnPropertyChanged);
            this.btnF4.DataBindings.Add("Text", op.klavesniceText, "BtnF4", false, DataSourceUpdateMode.OnPropertyChanged);
            this.btnF5.DataBindings.Add("Text", op.klavesniceText, "BtnF5", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button_Click(object sender, EventArgs e)
        {
            op.ButtonClick(sender, e);
        }
    }


    public class KeyboardText : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _btnEnter = "Enter";
        public string BtnEnter
        {
            get
            {
                return _btnEnter;
            }
            set
            {
                _btnEnter = value;
                OnPropertyChanged("BtnEnter");
            }
        }

        private string _btn19 = "OK";
        public string Btn19
        {
            get
            {
                return _btn19;
            }
            set
            {
                _btn19 = value;
                OnPropertyChanged("Btn19");
            }
        }

        private string _btnF1 = "F1";
        public string BtnF1
        {
            get
            {
                return _btnF1;
            }
            set
            {
                _btnF1 = value;
                OnPropertyChanged("BtnF1");
            }
        }

        private string _btnF2 = "F2";
        public string BtnF2
        {
            get
            {
                return _btnF2;
            }
            set
            {
                _btnF2 = value;
                OnPropertyChanged("BtnF2");
            }
        }

        private string _btnF3 = "F3";
        public string BtnF3
        {
            get
            {
                return _btnF3;
            }
            set
            {
                _btnF3 = value;
                OnPropertyChanged("BtnF3");
            }
        }

        private string _btnF4 = "F4";
        public string BtnF4
        {
            get
            {
                return _btnF4;
            }
            set
            {
                _btnF4 = value;
                OnPropertyChanged("BtnF4");
            }
        }


        private string _btnF5 = "F5";
        public string BtnF5
        {
            get
            {
                return _btnF5;
            }
            set
            {
                _btnF5 = value;
                OnPropertyChanged("BtnF5");
            }
        }

        public KeyboardText()
        {
        }

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
