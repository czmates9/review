using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Classes
{
    public class AssemblyInfo
    {
        public string dllPath { get; set; }
        public string buttonText { get; set; }
        public bool isButtonEnabled { get; set; }
        public string keyCode { get; set; }
        public EventHandler eventHandler { get; set; }

        public AssemblyInfo()
        {
        }

        public AssemblyInfo(string dllPath, string buttonText, bool buttonEnabled, string keyValue, EventHandler eventHandler)
        {
            this.dllPath = dllPath;
            this.buttonText = buttonText;
            this.isButtonEnabled = buttonEnabled;
            this.keyCode = keyValue;
            this.eventHandler = eventHandler;
        }

    }
}
