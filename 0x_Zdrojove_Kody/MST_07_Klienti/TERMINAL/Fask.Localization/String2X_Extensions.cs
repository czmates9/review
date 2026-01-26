using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Reflection;
using System.ComponentModel;
using System.Security.Permissions; 

namespace Fask.Localization
{
    public static class String2X_Extensions
    {
        public static Font String2Font(this String s)
        {
            return String2X_Metody.String2Font(s);
        }

        public static float? String2Float(this String s)
        {
            return String2X_Metody.String2Float(s);
        }

        public static Size? String2Size(this String s)
        {
            return String2X_Metody.String2Size(s);
        }

        public static Point? String2Point(this String s)
        {
            return String2X_Metody.String2Point(s);
        }

        //public static Color? String2Color(this String s)
        //{
        //    return String2X_Metoday.String2Color(s);
        //}
    }


    public class String2X_Metody
    {
        /// <summary>
        /// Konvertuje string na Font ve formatu "Arial,12pt" anebo "Arial,12pt,style=Bolt"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Font String2Font(String s)
        {
            #region style fontu
            //FontStyle.Bold; // tučne
            //FontStyle.Italic; //Kurziva
            //FontStyle.Regular; // Nornalni
            //FontStyle.Strikeout;//preškrknuty
            //FontStyle.Underline;// podtrženy
            #endregion
            try
            {
                string name = "Arial";
                float emSize = 8;
                FontStyle style = FontStyle.Regular;

                //StringBuilder sb = new StringBuilder();                

                string[] FontJednotlive = s.Split(',');

                if (FontJednotlive.Length == 1) 
                {
                    name = FontJednotlive[0].Trim();
                }
                if (FontJednotlive.Length == 2)
                {
                    name = FontJednotlive[0].Trim();
                    emSize = (float)FontJednotlive[1].String2Float();

                }
                else if (FontJednotlive.Length == 3)
                {
                    name = FontJednotlive[0];
                    emSize = (float)FontJednotlive[1].String2Float();

                    string[] styl = FontJednotlive[2].Split('=');
                    if (styl.Length == 2)
                    {
                        if (styl[0].Trim() == "style")
                        {
                            style = (FontStyle)Enum.Parse(typeof(FontStyle), styl[1].Trim(), true);
                        }
                    }
                }
                else 
                {
                    return null;
                }
                

                Font f = new Font(name, emSize, style);
                return f;

            }
            catch (Exception e)
            {
                Logging.Log.Write(e);
                return null;
            }
        }

        /// <summary>
        /// Konvertuje String na Float ked obsahuje text, primarne na 12pt z velkosti fontu
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float? String2Float(String s)
        {
            float LokalFloat = 0;
            string InString = s.Trim();
            string OutString = String.Empty;


            try
            {
                foreach (char item in InString)
                {
                    if (char.IsNumber(item) || item == ',' || item == '.')
                    {
                        OutString += item;
                    }
                }

                LokalFloat = float.Parse(OutString);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return null;
            }
            return LokalFloat;
        }



        public static Size? String2Size(String s)
        {

            Size localsize = new Size();

            try
            {
                string[] sizearr = s.Split(',');
                if (sizearr.Length == 2)
                {
                    localsize.Width = int.Parse(sizearr[0]);
                    localsize.Height = int.Parse(sizearr[1]);
                }
                else
                {
                    localsize.Width = 0;
                    localsize.Height = 0;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return null;
            }

            if (localsize.Width == 0 && localsize.Height == 0)
                return null;
            else
                return localsize;
        }

        public static Point? String2Point(String s)
        {

            Point localsize = new Point();

            try
            {
                string[] sizearr = s.Split(',');
                if (sizearr.Length == 2)
                {
                    localsize.X = int.Parse(sizearr[0]);
                    localsize.Y = int.Parse(sizearr[1]);
                }
                else
                {
                    localsize.X = 0;
                    localsize.Y = 0;
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.Log.Write(ex);
                return null;
            }

            if (localsize.X == 0 && localsize.Y == 0)
                return null;
            else
                return localsize;
        }



        internal static Color? String2Color(string s)
        {
            //Color c = new Color();

            

            //switch (s)
            //{
            //    case "Black" :
            //        c = System.Drawing.Color.Black;
            //        break;
            //    default:
            //        c = System.Drawing.Color.Black;
            //        break;
            //}


            return System.Drawing.Color.Black;



        }
    }
}