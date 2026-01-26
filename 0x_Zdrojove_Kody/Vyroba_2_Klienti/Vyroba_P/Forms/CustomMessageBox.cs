using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fask.Vyroba_P.Forms
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox(string message, string title, int fontSize)
        {
            InitializeComponent();

            // Nastavení textu a barvy
            this.Text = title;
            labelMessage.Text = message;
            labelMessage.ForeColor = Color.Red; // Nastavení červené barvy textu
            labelMessage.Font = new Font(labelMessage.Font.FontFamily, fontSize); // Nastavení velikosti písma

            // Zakázání tlačítek minimalizace a zavření (krížku)
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false; // Skryje i tlačítko zavření (X)
            this.StartPosition = FormStartPosition.CenterScreen; // Zobrazení uprostřed obrazovky
        }

        // Tato metoda se zobrazí jako dialog a umožňuje externě nastavit velikost textu
        public static void Show(string message, string title, int fontSize)
        {
            CustomMessageBox msgBox = new CustomMessageBox(message, title, fontSize);
            msgBox.ShowDialog();
        }

        // Metoda pro zavření formuláře
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
