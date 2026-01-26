using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace Fask.Vyroba_P.Forms
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = "O aplikaci";
            this.labelProductName.Text = "Výroba - odvádění";
            this.labelVersion.Text = String.Format("{0}", AssemblyVersion);
            
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.labelDatabaze.Text = string.Format("IIS: {0}", Settings.Adresa_API);
        }


        public AboutBox(string zakaznik, DateTime expirace, string nazevIIS)
        {
            InitializeComponent();
            this.Text = "O aplikaci";
            //this.labelProductName.Text = "Konzola mistra";
            this.labelProductName.Text = "Výroba - odvádění";
            //[assembly: AssemblyInformationalVersion("Konzola 2 Pohoda-Cipisek Alfa")]
            this.labelVersion.Text = String.Format("{0}", AssemblyVersion);

            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;

            this.labelExpirace.Text = string.Format("Licence: Platná do {0}", expirace.ToShortDateString());
            this.labelZakaznik.Text = string.Format("Zákazník: {0}", zakaznik);


            //this.labelProvider.Text = string.Format("Provider: {0}", provider);
            this.labelDatabaze.Text = string.Format("Databáze: {0}", nazevIIS);

            //TODO MaR 25.4.2024 doplnit informace z licence


        }


        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion











    }
}
