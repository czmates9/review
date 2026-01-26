using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.Aktualizace_API.Ukolovani
{
    public partial class UkolovaniUkolEdit : Form
    {
        public Fask.Aktualizace_API.Data.Ukoly.CZ_UKOLRow _ukol = null;
        public Fask.Aktualizace_API.Data.Ukoly.CZ_UKOL_UZIVRow _ukol_uziv = null;

        public UkolovaniUkolEdit(Fask.Aktualizace_API.Data.Ukoly.CZ_UKOLRow ukol, Fask.Aktualizace_API.Data.Ukoly.CZ_UKOL_UZIVRow ukol_uziv)
        {
            InitializeComponent();
        }
    }
}
