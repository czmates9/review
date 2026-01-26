using Fask.Graphic;
namespace Fask.ScannerProviderSerialPort
{
    partial class ScannerSerialPortSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelButtons = new System.Windows.Forms.Panel();
            this.bOK = new Fask.Graphic.GraphicButton();
            this.bStorno = new Fask.Graphic.GraphicButton();
            this.panelData = new System.Windows.Forms.Panel();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panelData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.bOK);
            this.panelButtons.Controls.Add(this.bStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 205);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(246, 35);
            // 
            // bOK
            // 
            this.bOK.BitmapNormal = null;
            this.bOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOK.FocusMargin = 5;
            this.bOK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.bOK.Location = new System.Drawing.Point(116, 0);
            this.bOK.Name = "bOK";
            this.bOK.Pressed = false;
            this.bOK.Size = new System.Drawing.Size(130, 35);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.Transparent = System.Drawing.Color.White;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bStorno
            // 
            this.bStorno.BitmapNormal = null;
            this.bStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.bStorno.FocusMargin = 5;
            this.bStorno.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.bStorno.Location = new System.Drawing.Point(0, 0);
            this.bStorno.Name = "bStorno";
            this.bStorno.Pressed = false;
            this.bStorno.Size = new System.Drawing.Size(116, 35);
            this.bStorno.TabIndex = 0;
            this.bStorno.Text = "Storno";
            this.bStorno.Transparent = System.Drawing.Color.White;
            this.bStorno.Click += new System.EventHandler(this.bStorno_Click);
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.label1);
            this.panelData.Controls.Add(this.cbPort);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(246, 205);
            // 
            // cbPort
            // 
            this.cbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cbPort.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.cbPort.Items.Add("COM1");
            this.cbPort.Items.Add("COM2");
            this.cbPort.Items.Add("COM3");
            this.cbPort.Items.Add("COM4");
            this.cbPort.Items.Add("COM5");
            this.cbPort.Location = new System.Drawing.Point(78, 4);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(100, 19);
            this.cbPort.TabIndex = 0;
            this.cbPort.Text = "COM1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.Text = "Port :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ScannerSerialPortSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(246, 240);
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelButtons);
            this.Name = "ScannerSerialPortSettingsForm";
            this.Text = "Serial port scanner settings";
            this.panelButtons.ResumeLayout(false);
            this.panelData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GraphicButton bOK;
        public GraphicButton bStorno;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPort;
    }
}