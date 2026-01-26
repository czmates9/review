using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vyroba_Agro.Classes
{
    public class History
    {
        public History()
        {
            this.barcode = string.Empty;
            this.countNoRead = this.countRead = 0;
        }

        public History(string code, int countNoRead, int countRead)
        {
            this.barcode = code;
            this.countNoRead = countNoRead;
            this.countRead = countRead;
        }


        private string barcode;
        public string Barcode
        {
            get
            {
                return barcode;
            }
            set
            {
                barcode = value;
            }
        }

        private int countRead;
        public int CountRead
        {
            get
            {
                return countRead;
            }
            set
            {
                countRead = value;
            }
        }

        private int countNoRead;
        public int CountNoRead
        {
            get
            {
                return countNoRead;
            }
            set
            {
                countNoRead = value;
            }
        }

    }
}
