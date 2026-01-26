using Fask.Graphic;
namespace Fask.MST_W.Scanner.MC9090
{
    partial class FrmConfigurationMain
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
            this.panelData = new System.Windows.Forms.Panel();
            this.gbReader = new Fask.Graphic.GraphicButton();
            this.gbAntena = new Fask.Graphic.GraphicButton();
            this.gbReaderInfo = new Fask.Graphic.GraphicButton();
            this.gbAntenaInfo = new Fask.Graphic.GraphicButton();
            this.gbCapabilities = new Fask.Graphic.GraphicButton();
            this.panelButtons.SuspendLayout();
            this.panelData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.bOK);
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
            this.bOK.Location = new System.Drawing.Point(0, 0);
            this.bOK.Name = "bOK";
            this.bOK.Pressed = false;
            this.bOK.Size = new System.Drawing.Size(246, 35);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "Konec";
            this.bOK.Transparent = System.Drawing.Color.White;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.gbAntena);
            this.panelData.Controls.Add(this.gbReader);
            this.panelData.Controls.Add(this.gbCapabilities);
            this.panelData.Controls.Add(this.gbAntenaInfo);
            this.panelData.Controls.Add(this.gbReaderInfo);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(246, 205);
            // 
            // gbReader
            // 
            this.gbReader.BitmapNormal = null;
            this.gbReader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbReader.FocusMargin = 5;
            this.gbReader.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbReader.Location = new System.Drawing.Point(0, 120);
            this.gbReader.Name = "gbReader";
            this.gbReader.Pressed = false;
            this.gbReader.Size = new System.Drawing.Size(246, 40);
            this.gbReader.TabIndex = 2;
            this.gbReader.Text = "Reader";
            this.gbReader.Transparent = System.Drawing.Color.White;
            this.gbReader.Click += new System.EventHandler(this.gbReader_Click);
            // 
            // gbAntena
            // 
            this.gbAntena.BitmapNormal = null;
            this.gbAntena.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAntena.FocusMargin = 5;
            this.gbAntena.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbAntena.Location = new System.Drawing.Point(0, 160);
            this.gbAntena.Name = "gbAntena";
            this.gbAntena.Pressed = false;
            this.gbAntena.Size = new System.Drawing.Size(246, 40);
            this.gbAntena.TabIndex = 3;
            this.gbAntena.Text = "Antena";
            this.gbAntena.Transparent = System.Drawing.Color.White;
            this.gbAntena.Click += new System.EventHandler(this.gbAntena_Click);
            // 
            // gbReaderInfo
            // 
            this.gbReaderInfo.BitmapNormal = null;
            this.gbReaderInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbReaderInfo.FocusMargin = 5;
            this.gbReaderInfo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbReaderInfo.Location = new System.Drawing.Point(0, 0);
            this.gbReaderInfo.Name = "gbReaderInfo";
            this.gbReaderInfo.Pressed = false;
            this.gbReaderInfo.Size = new System.Drawing.Size(246, 40);
            this.gbReaderInfo.TabIndex = 4;
            this.gbReaderInfo.Text = "Reader info";
            this.gbReaderInfo.Transparent = System.Drawing.Color.White;
            this.gbReaderInfo.Click += new System.EventHandler(this.gbReaderInfo_Click);
            // 
            // gbAntenaInfo
            // 
            this.gbAntenaInfo.BitmapNormal = null;
            this.gbAntenaInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAntenaInfo.FocusMargin = 5;
            this.gbAntenaInfo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbAntenaInfo.Location = new System.Drawing.Point(0, 40);
            this.gbAntenaInfo.Name = "gbAntenaInfo";
            this.gbAntenaInfo.Pressed = false;
            this.gbAntenaInfo.Size = new System.Drawing.Size(246, 40);
            this.gbAntenaInfo.TabIndex = 5;
            this.gbAntenaInfo.Text = "Antena Info";
            this.gbAntenaInfo.Transparent = System.Drawing.Color.White;
            this.gbAntenaInfo.Click += new System.EventHandler(this.gbAntenaInfo_Click);
            // 
            // gbCapabilities
            // 
            this.gbCapabilities.BitmapNormal = null;
            this.gbCapabilities.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCapabilities.FocusMargin = 5;
            this.gbCapabilities.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbCapabilities.Location = new System.Drawing.Point(0, 80);
            this.gbCapabilities.Name = "gbCapabilities";
            this.gbCapabilities.Pressed = false;
            this.gbCapabilities.Size = new System.Drawing.Size(246, 40);
            this.gbCapabilities.TabIndex = 6;
            this.gbCapabilities.Text = "Capabilities";
            this.gbCapabilities.Transparent = System.Drawing.Color.White;
            this.gbCapabilities.Click += new System.EventHandler(this.gbCapabilities_Click);
            // 
            // FrmConfigurationMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(246, 240);
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelButtons);
            this.Name = "FrmConfigurationMain";
            this.Text = "MC9090 RFID Configuration";
            this.panelButtons.ResumeLayout(false);
            this.panelData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GraphicButton bOK;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel panelData;
        private GraphicButton gbAntenaInfo;
        private GraphicButton gbReaderInfo;
        private GraphicButton gbAntena;
        private GraphicButton gbReader;
        private GraphicButton gbCapabilities;
    }
}