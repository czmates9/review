using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.Constants
{
    public class AGRO
    {

        #region ID statusu

        public const int status_0 = 0;
        public const int status_1 = 1;
        public const int status_200 = 200;
        //TISK
        public const int status_40 = 40;
        //bocedi 1
        public const int status_21 = 21;
        public const int status_31 = 31;
        public const int status_41 = 41;
        public const int status_51 = 51;
        public const int status_61 = 61;
        public const int status_221 = 221;
        public const int status_231 = 231;
        //bocedi 2
        public const int status_22 = 22;
        public const int status_32 = 32;
        public const int status_42 = 42;
        public const int status_52 = 52;
        public const int status_62 = 62;
        public const int status_222 = 222;
        public const int status_232 = 232;
        //kapalky
        public const int status_24 = 24;
        public const int status_34 = 34;
        public const int status_44 = 44;
        public const int status_54 = 54;
        public const int status_64 = 64;
        public const int status_224 = 224;
        public const int status_234 = 234;

        #endregion

        #region Spolecne

        public const string BTN_vyhledat = "vyhledat";
        public const string BTN_deaktivovat = "deaktivovat";
        public const string BTN_zpet = "storno";

        #endregion

        #region Substraty

        public const string BTN_Linka1 = "Linka 1";
        public const string BTN_Linka2 = "Linka 2";
        public const string BTN_Linka3 = "Linka 3";
        public const string BTN_RV = "RV";
        public const string BTN_Bocedi1 = "Stretch 1";
        public const string BTN_Bocedi2 = "Stretch 2";
        public const string description_Tisk_ok = "tisk_aplikovano";
        public const string description_Tisk_NP = "tisk_neaplikovano_NP";
        public const string description_Tisk_FP = "tisk_neaplikovano_FP";
        public const string description_Tisk_error = "tisk_chyba";
        public const string description_PresunNaTisk = "presun na tisk";
        public const string comm_tisk = "tisk";
        public const string comm_fask = "fask";
        //dodelat nazvy logovacich souboru

        public const int linka_1 = 1;
        public const int linka_2 = 2;
        public const int linka_3 = 3;
        public const int bocedi_1 = 4;
        public const int bocedi_2 = 5;
        public const int rucni_vstup = 6;

        public const int falsak = 66;

        #endregion

        #region Roboty

        public const string BTN_Linka4_R = "Linka";
        public const string BTN_Bocedi3_R = "Strech";
        public const string BTN_RV_R = "RV";

        public static int linka_4_R = 7;
        public static int bocedi_3_R = 8;
        public static int rucni_vstup_R = 9;

        public static int falsak_R = 77;


        #endregion

        #region kapalky

        public const string BTN_Linka5_K = "Linka";
        public const string BTN_Bocedi4_K = "Strech";
        public const string BTN_RV_K = "RV";

        public static int linka_5_K = 10;
        public static int bocedi_4_K = 12;
        public static int rucni_vstup_K = 11;

        public static int falsak_K = 88;


        #endregion
    }
}