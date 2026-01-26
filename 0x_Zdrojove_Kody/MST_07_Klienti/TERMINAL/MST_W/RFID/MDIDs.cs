using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    public class MDIDs
    {
        public static System.Collections.Generic.Dictionary<ushort, string> models =
            new Dictionary<ushort,string>() {
                {0x001, "Impinj"},              {0x801, "Impinj XTID"},
                {0x002,	"Texas Instruments"},   {0x802,	"Texas Instruments XTID"},
                {0x003,	"Alien Technology"},    {0x803,	"Alien Technology XTID"},
                {0x004,	"Intelleflex"},         {0x804,	"Intelleflex XTID"},

                {0x005,	"Atmel"},               {0x805,	"Atmel XTID"}, 
                {0x006,	"NXP Semiconductors"},  {0x806,	"NXP Semiconductors XTID"},
                {0x007,	"ST Microelectronics"}, {0x807,	"ST Microelectronics XTID"},
                {0x008,	"EP Microelectronics"}, {0x808,	"EP Microelectronics XTID"},
                {0x009,	"Motorola"},            {0x809,	"Motorola XTID"},
                {0x00A,	"Sentech Snd Bhd"},     {0x80A,	"Sentech Snd Bhd XTID"},
                {0x00B,	"EM Microelectronics"}, {0x80B,	"EM Microelectronics XTID"},
                {0x00C,	"Renesas Technology Corp."},{0x80C,	"Renesas Technology Corp. XTID"},
                {0x00D,	"Mstar"},               {0x80D,	"Mstar XTID"},
                {0x00E,	"Tyco International"},  {0x80E,	"Tyco International XTID"},
                {0x00F,	"Quanray Electronics"}, {0x80F,	"Quanray Electronics XTID"},
                {0x010,	"Fujitsu"},             {0x810,	"Fujitsu XTID"},
                {0x011,	"LSIS"},                {0x811,	"LSIS XTID"},
                {0x012,	"CAEN RFID srl"},       {0x812,	"CAEN RFID srl XTID"},
                {0x013,	"Productivity Engineering Gesellschaft fuer IC Design mbH"},
                                                {0x813,	"Productivity Engineering Gesellschaft fuer IC Design mbH XTID"},
                {0x014,	"Federal Electric Corp."},{0x814,	"Federal Electric Corp. XTID"},
                {0x015,	"ON Semiconductor"},    {0x815,	"ON Semiconductor XTID"},
                {0x016,	"Ramtron"},             {0x816,	"Ramtron XTID"},
                {0x017,	"Tego"},                {0x817,	"Tego XTID"},
                {0x018,	"Ceitec S.A."},         {0x818,	"Ceitec S.A. XTID"},
                {0x019,	"CPA Wernher von Braun"},{0x819,	"CPA Wernher von Braun XTID"},
                {0x01A,	"TransCore"},           {0x81A,	"TransCore XTID"},
                {0x01B,	"Nationz"},             {0x81B,	"Nationz XTID"},
                {0x01C,	"Invengo"},             {0x81C,	"Invengo XTID"},
                {0x01D,	"Kiloway"},             {0x81D,	"Kiloway XTID"},
                {0x01E,	"Longjing Microelectronics Co. Ltd."},{0x81E,	"Longjing Microelectronics Co. Ltd. XTID"},
                {0x01F,	"Chipus Microelectronics"},{0x81F,	"Chipus Microelectronics XTID"},
                {0x020,	"ORIDAO"},              {0x820,	"ORIDAO XTID"},
                {0x021,	"Maintag"},             {0x821,	"Maintag XTID"},
                {0x022,	"Yangzhou Daoyuan Microelectronics Co. Ltd"},{0x822,	"Yangzhou Daoyuan Microelectronics Co. Ltd XTID"},
                {0x023,	"Gate Elektronik"},     {0x823,	"Gate Elektronik XTID"},
                {0x024,	"RFMicron, Inc."},      {0x824,	"RFMicron, Inc. XTID"},
                {0x025,	"RST-Invent LLC"},      {0x825,	"RST-Invent LLC XTID"},
                {0x026,	"Crystone Technology"}, {0x826,	"Crystone Technology XTID"},
                {0x027,	"Shanghai Fudan Microelectronics Group"},{0x827,	"Shanghai Fudan Microelectronics Group XTID"},
                {0x028,	"Farsens"},             {0x828,	"Farsens XTID"},
                {0x029,	"Giesecke & Devrient GmbH"},{0x829,	"Giesecke & Devrient GmbH XTID"},
                {0x02A,	"AWID"},                {0x82A,	"AWID XTID"},
                {0x02B,	"Unitec Semicondutores S/A"},{0x82B,	"Unitec Semicondutores S/A XTID"},
                {0x02C,	"Q-Free ASA"},          {0x82C,	"Q-Free ASA XTID"},
                {0x02D,	"Valid S.A."},          {0x82D,	"Valid S.A. XTID"},
                {0x02E,	"Fraunhofer IPMS"},     {0x82E,	"Fraunhofer IPMS XTID"}
            };
    }
}
