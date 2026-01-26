using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Rezacka
{
    //Trida reprezentujici konecny automat
    internal static class fsm
    {
        //Vyhodnoti zda po soucasne operaci muze nastat operace budouci
        //Pokud je future VOLNA, pak i current je predana volna a 
        //pokud je future VYROB, pak i current je predana vyrobni
        internal static bool evaluateState(Operation currentOperation, Operation futureOperation)
        {
            //V INIT stavu muze nasledovat
            //A. startovni operace
            //B. startovni volne operace
            // => obecne startovi operace
            if (currentOperation.IDO == string.Empty)
            {
                if (futureOperation.START)
                {
                    return true;
                }
            }
            //Po nejake operaci muze nastat
            //A. operace co je v next
            else
            {
                if (isOperationInOperationNextList(futureOperation.IDO, currentOperation.NEXTOPERATIONS))
                {
                    return true;
                }
            }

            //Neznamy stav
            return false;
        }

        //Pomocna funkce, ktera overi, zda je operace v seznamu operaci (IDO v operation_next)
        private static bool isOperationInOperationNextList(string operation, string operationNextList)
        {
            //Rozdeleni dle oddelovace = carka
            string[] opList = operationNextList.Split(new char[] { ',' });
            //Pruchod a odstraneni prebytecnych mezer a kontrola => jen jeden cykl
            for (int i = 0; i < opList.Length; i++)
            {
                //Nalezena operace v seznamu nasledniku
                if (operation == opList[i].Trim()) return true;
            }
            //Nenalezena
            return false;
        }
    }
}
