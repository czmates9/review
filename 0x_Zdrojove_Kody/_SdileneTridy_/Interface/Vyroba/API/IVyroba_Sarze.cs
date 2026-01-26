using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.API
{
    public interface IVyroba_Sarze : IMES
    {

        string ReturnSarze(string smenaID, string userID, string linkaID);

        bool ReturnID( string inID);

        bool ReturnHeslo( string inHESLO, string inID);

    }
}
