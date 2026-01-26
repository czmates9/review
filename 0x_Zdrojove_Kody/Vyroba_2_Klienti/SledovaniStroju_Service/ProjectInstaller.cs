using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;



using System.ServiceProcess;


namespace FASK
{
    [RunInstaller(true)]
    [System.ComponentModel.DesignerCategory("")]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
