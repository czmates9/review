
namespace FASK.SledovaniVyroby.LabelPrint
{
    public class clsPrint
    {
        //Privatni promenna pro tisk
        private LabelPrinting.PrintServerService.Tisk print=null;

        //Konstruktor
        public clsPrint(string address, int timeout)
        {
            //Nastaveni serveru
            print = new LabelPrinting.PrintServerService.Tisk();
            print.Url = address + "/Tisk.asmx";
            print.Timeout = timeout;
        }

        //Vlastni vytisteni
        public bool Print(int terminalID,string templateName, LabelPrinting.PrintServerService.TiskParams printerParams,LabelPrinting.PrintServerService.DSValues printData,int count)
        {
            return print.Etiketa(terminalID, templateName, printerParams, printData, count);
        }
    }
}
