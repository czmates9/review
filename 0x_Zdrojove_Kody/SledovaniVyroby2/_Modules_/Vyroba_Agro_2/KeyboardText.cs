using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro
{
    public class KeyboardText : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _btnEnter = "Enter";
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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

        //private string _btnF6 = "F6";
        //[Browsable(false)]
        //public string BtnF6
        //{
        //    get
        //    {
        //        return _btnF6;
        //    }
        //    set
        //    {
        //        _btnF5 = value;
        //        OnPropertyChanged("BtnF6");
        //    }
        //}


        public KeyboardText()
        {
        }

        public KeyboardText(string bF1, string bF2, string bF3, string bF4, string bF5, string b19, string bEnter)
        {
            this.BtnF1 = bF1;
            this.BtnF2 = bF2;
            this.BtnF3 = bF3;
            this.BtnF4 = bF4;
            this.BtnF5 = bF5;
            this.Btn19 = b19;
            this.BtnEnter = bEnter;
        }

        //public KeyboardText(string bF1, string bF2, string bF3, string bF4, string bF5, string bF6, string b19, string bEnter)
        //{
        //    this.BtnF1 = bF1;
        //    this.BtnF2 = bF2;
        //    this.BtnF3 = bF3;
        //    this.BtnF4 = bF4;
        //    this.BtnF5 = bF5;
        //    this.BtnF6 = bF6;
        //    this.Btn19 = b19;
        //    this.BtnEnter = bEnter;
        //}

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
