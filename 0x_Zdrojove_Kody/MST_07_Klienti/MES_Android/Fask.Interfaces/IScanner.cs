using System;

namespace Fask.Interfaces
{
    public interface IScanner
    {

        public delegate void ScannerEventHandler(object sender, ScannerEventArgs e);

        public event ScannerEventHandler ScannerEvent;

        public delegate void StatusEventHandler(object sender, StatusEventArgs e);

        public event StatusEventHandler StatusEvent;

        void StartScanner();
        void StopScanner();
        void KillScanner();

    }

    public class ScannerEventArgs
    {
        public ScannerEventArgs(string data, string labelType)
        {
            Data = data;
            LabelType = labelType;
        }

        public string Data { get; }
        public string LabelType { get; }
    }

    public class StatusEventArgs
    {
        public StatusEventArgs(string msg)
        {
            Msg = msg;
        }

        public string Msg { get; }
    }
}
