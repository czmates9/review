using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fask.Vyroba_P.Classes
{
    #region old
    public class CustomMessageBox : Form
    {
        private Label labelMessage;
        private PictureBox pictureBoxIcon;
        private Button buttonOK;
        private Button buttonNo;

        #region old
        public CustomMessageBox(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            InitializeComponent(message, caption, buttons, icon);
        }


    


        private void InitializeComponent(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            this.SuspendLayout();

            // Nastavení vzhledu formuláře
            //this.ClientSize = new System.Drawing.Size(300, 150);
           // this.ClientSize = CalculateSize(30); // Velikost 10% z velikosti obrazovky
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = caption;


            // Vytvoření labelu pro zobrazení zprávy
            Label labelMessage = new Label();
            labelMessage.Text = message;
            labelMessage.AutoSize = true;
            //labelMessage.Location = CalculatePoint(10, 10);
            labelMessage.Font = new Font(labelMessage.Font.FontFamily, CalculateFontSize(12), labelMessage.Font.Style);
            this.Controls.Add(labelMessage);

            // Nastavení ikony (pokud je zadaná)
            if (icon != MessageBoxIcon.None)
            {
                PictureBox pictureBoxIcon = new PictureBox();
                pictureBoxIcon.Image = SystemIcons.Information.ToBitmap(); // Zde můžete nahradit ikonu podle parametru icon
               // pictureBoxIcon.Location = CalculatePoint(10, 20);
                this.Controls.Add(pictureBoxIcon);
            }

            // Vytvoření tlačítek
            Button buttonOK = new Button();
            buttonOK.Text = "OK";
            buttonOK.DialogResult = DialogResult.OK;
           // buttonOK.Location = CalculatePoint(50, 90);
            buttonOK.Font = new Font(buttonOK.Font.FontFamily, CalculateFontSize(10), buttonOK.Font.Style);
            this.Controls.Add(buttonOK);

            // Přidání události pro uzavření formuláře po stisku OK
            buttonOK.Click += (sender, e) => this.Close();

            // Nastavení vzhledu tlačítek podle požadavku
            if (buttons == MessageBoxButtons.YesNo)
            {
                Button buttonYes = new Button();
                buttonYes.Text = "Ano";
                buttonYes.DialogResult = DialogResult.Yes;
               // buttonYes.Location = CalculatePoint(70, 90);
                buttonYes.Font = new Font(buttonYes.Font.FontFamily, CalculateFontSize(10), buttonYes.Font.Style);
                this.Controls.Add(buttonYes);

                // Přidání události pro uzavření formuláře po stisku Yes
                buttonYes.Click += (sender, e) => this.Close();
            }

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private float CalculateFontSize(float baseSize)
        {
            // Výpočet velikosti písma na základě aktuální velikosti formuláře
            return (this.ClientSize.Width * baseSize) / 300; // 300 je šířka formuláře, můžete přizpůsobit podle potřeby
        }


        private Size CalculateSize(int percentage)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int width = screenWidth * percentage / 100;
            int height = screenHeight * percentage / 100;

            return new Size(width, height);
        }

        private Point CalculateStartPosition(Size formSize)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int x = (screenWidth - formSize.Width) / 2;
            int y = (screenHeight - formSize.Height) / 2;

            return new Point(x, y);
        }

        private Point CalculatePoint(int xPercentage, int yPercentage)
        {
            int x = this.ClientSize.Width * xPercentage / 100;
            int y = this.ClientSize.Height * yPercentage / 100;

            return new Point(x, y);
        }
        #endregion




    }
    #endregion



    //class Program
    //{
    //    [STAThread]
    //    static void Main()
    //    {
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);

    //        // Použití vlastního MessageBoxu
    //        using (var customMessageBox = new CustomMessageBox("Hello, this is a custom message box!", "Custom MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
    //        {
    //            if (customMessageBox.ShowDialog() == DialogResult.Yes)
    //            {
    //                // Uživatel stiskl Yes
    //            }
    //            else
    //            {
    //                // Uživatel stiskl No
    //            }
    //        }
    //    }
    //}
}
