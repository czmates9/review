using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace ZZS_Servis_096_AD
{
    [RunInstaller(true)]
    [System.ComponentModel.DesignerCategory("")]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            // spouštění pod účtem LocalSystem
            serviceProcessInstaller.Account = ServiceAccount.NetworkService;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            // unikátní název, název pro zobrazení a popis
            serviceInstaller.ServiceName = "ZZS_Servis_096_AD";
            serviceInstaller.DisplayName = "ZZS_Servis_096_AD";
            serviceInstaller.Description = "FASK Synchronizace Pracovniku s AD";

            // způsob spouštění
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
