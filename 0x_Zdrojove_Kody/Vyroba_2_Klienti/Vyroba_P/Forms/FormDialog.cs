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
    public partial class FormDialog : Form
    {
        private int x = 0, y = 0;
        private int _procent;

        public void SizeButton(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        private (int x, int y) VratVelikost()
        {
            return (x, y);
        }

     

        public FormDialog(int procent, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            _procent = procent;
            InitializeComponent();
            NastavParametry( procent,  message,  caption,  buttons,  icon);
        }


        private void NastavParametry(int procent, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
           // this.ClientSize = CalculateSize(procent); // Velikost 10% z velikosti obrazovky
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = caption;


            // Vytvoření labelu pro zobrazení zprávy
            //Label labelMessage = new Label();
            //labelMessage.Text = message;
            //labelMessage.AutoSize = true;
            ////labelMessage.Location = CalculatePoint(10, 10);
            //labelMessage.Font = new Font(labelMessage.Font.FontFamily, CalculateFontSize(procent), labelMessage.Font.Style);
            //this.Controls.Add(labelMessage);


            l_text.Text = message;
           // l_text.AutoSize = true;
            //labelMessage.Location = CalculatePoint(10, 10);
            //l_text.Font = new Font(l_text.Font.FontFamily, 18, l_text.Font.Style);
            l_text.Font = new Font(l_text.Font.FontFamily, CalculateFontSizeGB(procent), l_text.Font.Style);
            l_text.AutoSize = false; // Vypnout automatické nastavení velikosti na základě obsahu
            l_text.TextAlign = ContentAlignment.TopLeft; // Zarovnání textu vlevo nahoru
           // l_text.Size = new Size(200, 100); // Nastavit požadovanou velikost labelu
            l_text.Size = CalculateSizeGB(90);
            // this.Controls.Add(labelMessage);


            //if (icon == MessageBoxIcon.Information)
            //{
            //    pB_1.Size = new Size(30, 30);
            //    pB_1.Image = SystemIcons.Information.ToBitmap(); // Zde můžete nahradit ikonu podle parametru icon
                  
            //}



            // Nastavení vzhledu tlačítek podle požadavku
            if (buttons == MessageBoxButtons.YesNo)
            {
                btn_left.Text = "Ne";
                btn_left.DialogResult = DialogResult.No;
                // buttonOK.Location = CalculatePoint(50, 90);
                btn_left.Font = new Font(btn_left.Font.FontFamily, CalculateFontSize(procent), btn_left.Font.Style);

                btn_left.Click += (sender, e) => this.Close();


                btn_right.Text = "Ano";
                btn_right.DialogResult = DialogResult.Yes;
                // buttonYes.Location = CalculatePoint(70, 90);
                btn_right.Font = new Font(btn_right.Font.FontFamily, CalculateFontSize(procent), btn_right.Font.Style);
               
                btn_right.Click += (sender, e) => this.Close();
            }
            else if (buttons == MessageBoxButtons.OKCancel)
            {
                btn_left.Text = "Cancel";
                btn_left.DialogResult = DialogResult.Cancel;
                // buttonOK.Location = CalculatePoint(50, 90);
                btn_left.Font = new Font(btn_left.Font.FontFamily, CalculateFontSize(procent), btn_left.Font.Style);

                btn_left.Click += (sender, e) => this.Close();


                btn_right.Text = "OK";
                btn_right.DialogResult = DialogResult.OK;
                // buttonYes.Location = CalculatePoint(70, 90);
                btn_right.Font = new Font(btn_right.Font.FontFamily, CalculateFontSize(procent), btn_right.Font.Style);

                btn_right.Click += (sender, e) => this.Close();
            }
            else if (buttons == MessageBoxButtons.OK)
            {
                btn_left.Enabled = false;
                btn_left.Visible = false;
                btn_left.Text = "Cancel";
                btn_left.DialogResult = DialogResult.Cancel;
                // buttonOK.Location = CalculatePoint(50, 90);
                btn_left.Font = new Font(btn_left.Font.FontFamily, CalculateFontSize(procent), btn_left.Font.Style);

                btn_left.Click += (sender, e) => this.Close();


                btn_right.Text = "OK";
                btn_right.DialogResult = DialogResult.OK;
                // buttonYes.Location = CalculatePoint(70, 90);
                btn_right.Font = new Font(btn_right.Font.FontFamily, CalculateFontSize(procent), btn_right.Font.Style);

                btn_right.Click += (sender, e) => this.Close();
            }

            // btn_left.Size = CalculateSizeButtons(procent);
            // btn_right.Size = CalculateSizeButtons(procent);
        }

        private float CalculateFontSize(float baseSize)
        {
            // Výpočet velikosti písma na základě aktuální velikosti formuláře
            return (this.ClientSize.Width * baseSize) / 300; // 300 je šířka formuláře, můžete přizpůsobit podle potřeby
        }

        private float CalculateFontSizeGB(float baseSize)
        {
            // Výpočet velikosti písma na základě aktuální velikosti formuláře
            return (this.ClientSize.Width * baseSize) / gB_1.Width; // 300 je šířka formuláře, můžete přizpůsobit podle potřeby
        }

        private Size CalculateSize(int percentage)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int width = screenWidth * percentage / 100;
            int height = screenHeight * percentage / 100;

            return new Size(width, height);
        }

        private Size CalculateSizeGB(int percentage)
        {
            int screenWidth = gB_1.Width;
            int screenHeight = gB_1.Height;

            int width = screenWidth * percentage / 100;
            int height = screenHeight * (percentage-20) / 100;

            return new Size(width, height);
        }

        private Size CalculateSizeButtons(int percentage)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int width = screenWidth * percentage / 100 - screenWidth * 60 / 100;
            int height = screenHeight * percentage / 100 - screenHeight * 60 / 100;

            return new Size(width, height);
        }



    }
}
