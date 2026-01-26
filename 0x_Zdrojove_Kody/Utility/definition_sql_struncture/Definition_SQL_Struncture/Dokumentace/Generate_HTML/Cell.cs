using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Definition_SQL_Struncture.Dokumentace.Generate_HTML
{
    public class Cell
    {
        private string _value = string.Empty;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private bool _isClick = false;
        public bool IsClick
        {
            get { return _isClick; }
            set { _isClick = value; }
        }

        private string _pageToClick = string.Empty;
        public string PageToClick
        {
            get { return _pageToClick; }
            set { _pageToClick = value; }
        }

        private bool _isClickComment = false;
        public bool IsClickComment
        {
            get { return _isClickComment; }
            set { _isClickComment = value; }
        }
    }
}
