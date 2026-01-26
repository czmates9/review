using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.MST_WINDOWS.ModuleIfc
{
    public interface IModuleConnector
    {
        /// <summary>
        /// Oznameni - implementuje Form
        /// </summary>
        NotifyIcon NotifyIconState { get; set; }
        /// <summary>
        /// Parent - implementuje Form
        /// </summary>
        Form MdiParent { get; set; }
        /// <summary>
        /// Stav - implementuje Form
        /// </summary>
        FormWindowState WindowState { get; set; }
        /// <summary>
        /// Status label, do ktereho posila modul info
        /// </summary>
        ToolStripStatusLabel StatusLabel { set; }
        /// <summary>
        /// Uzavreni modulu - implementuje Form
        /// </summary>
        void Close();
        /// <summary>
        /// Navrat portu do predchoziho stavu
        /// </summary>
        void ReturnPortsToPreviousState();
        /// <summary>
        /// Uzavreni portu
        /// </summary>
        void ClosePorts();
        /// <summary>
        /// Zobrazeni formu - implementuje Form
        /// </summary>
        void Show();

        /// <summary>
        /// Vrati true pokud muzeme modul uzavrit, false pokud ne + v message bude nejaka zprava, proc
        /// nelze modul uzavrt
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool IsReadyToClose(out string message);
    }
}
