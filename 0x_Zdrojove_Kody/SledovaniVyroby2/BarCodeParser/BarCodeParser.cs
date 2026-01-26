using System;
using System.Collections.Generic;
using System.Text;

namespace FASK.SledovaniVyroby.BarCodePars
{
    public class BarCodeParser
    {
        string barcode;
        public string BarcodeTemplate
        {
            get { return barcode; }
            set { barcode = value; }
        }

        public BarCodeParser(string _barcode)
        {
            barcode = _barcode;
        }

        public int GetIndexOfFirst(char code)
        {
            return barcode.IndexOf(code);
        }

        /// <summary>
        /// Celkova dlzka ciaroveho kodu
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return barcode.Length;
        }

        /// <summary>
        /// Dlzka daneho znaku v kode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int Length(char code)
        {
            int reversedIndex = ReverseString(barcode).IndexOf(code);
            if (reversedIndex == -1)
                return 0;
            return barcode.Length - reversedIndex - GetIndexOfFirst(code);
        }

        /// <summary>
        /// Receives string and returns the string with its letters reversed.
        /// </summary>
        private static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

    }
}
