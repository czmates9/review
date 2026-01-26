using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.Aktualizace_API.ucDataViews.Historie
{
    public partial class dlgFilterSelector : Form
    {
        public static Historie.Casy LastCas = Casy.Denni;
        public static Historie.Osoba LastOsoba = Osoba.Prihlaseny;
        public static Historie.Stroj LastStroj = Stroj.Nastaveny;

        public Historie.Casy Cas
        {
            get
            {
                if (rbCasDenni.Checked)
                    return Casy.Denni;
                else if (rbCasMesicni.Checked)
                    return Casy.Mesicni;
                else
                    return Casy.DleFiltru;
            }
            set
            {
                switch (value)
                {
                    case Casy.Denni:
                        rbCasDenni.Checked = true;
                        break;
                    case Casy.Mesicni:
                        rbCasMesicni.Checked = true;
                        break;
                    default:
                        rbCasDleFiltru.Checked = true;
                        break;
                }
            }
        }

        public Historie.Osoba Osoba
        {
            get
            {
                if (rbUzivatelPrihlaseny.Checked)
                    return Osoba.Prihlaseny;
                else
                    return Osoba.DleFiltru;
            }
            set
            {
                switch (value)
                {
                    case Osoba.Prihlaseny:
                        rbUzivatelPrihlaseny.Checked = true;
                        break;
                    default:
                        rbUzivatelDleFiltru.Checked = true;
                        break;
                }
            }
        }

        public Historie.Stroj Stroj
        {
            get
            {
                if (rbStrojNastaveny.Checked)
                    return Stroj.Nastaveny;
                else
                    return Stroj.DleFiltru;
            }
            set
            {
                switch (value)
                {
                    case Stroj.Nastaveny:
                        rbStrojNastaveny.Checked = true;
                        break;
                    default:
                        rbStrojDleFiltru.Checked = true;
                        break;
                }
            }
        }


        public dlgFilterSelector()
        {
            InitializeComponent();

            LastLoad();
        }

        private void LastLoad()
        {
            this.Cas = LastCas;
            this.Osoba = LastOsoba;
            this.Stroj = LastStroj;
        }

        private void LastSave()
        {
            LastCas = this.Cas;
            LastOsoba = this.Osoba;
            LastStroj = this.Stroj;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            LastSave();

            this.DialogResult = DialogResult.OK;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            // ???
            LastSave();

            this.DialogResult = DialogResult.Cancel;
        }
    }
}
