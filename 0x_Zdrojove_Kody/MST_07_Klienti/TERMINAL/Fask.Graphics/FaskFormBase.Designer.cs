namespace Fask.Graphic
{
    partial class FaskFormBase
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
            this.panelData = new System.Windows.Forms.Panel();
            this.bOK = new Fask.Graphic.GraphicButton();
            this.bStorno = new Fask.Graphic.GraphicButton();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.SkyBlue;
            this.panelButtons.Controls.Add(this.bOK);
            this.panelButtons.Controls.Add(this.bStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 205);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(246, 35);
            // 
            // panelData
            // 
            this.panelData.BackColor = System.Drawing.Color.SkyBlue;
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(246, 205);
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.LightGreen;
            this.bOK.BitmapNormal = null;
            this.bOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOK.FocusMargin = 5;
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
            this.bStorno.BackColor = System.Drawing.Color.Pink;
            this.bStorno.BitmapNormal = null;
            this.bStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.bStorno.FocusMargin = 5;
            this.bStorno.Location = new System.Drawing.Point(0, 0);
            this.bStorno.Name = "bStorno";
            this.bStorno.Pressed = false;
            this.bStorno.Size = new System.Drawing.Size(116, 35);
            this.bStorno.TabIndex = 0;
            this.bStorno.Text = "Storno";
            this.bStorno.Transparent = System.Drawing.Color.White;
            this.bStorno.Click += new System.EventHandler(this.bStorno_Click);
            // 
            // FaskFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(246, 240);
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelButtons);
            this.Name = "FaskFormBase";
            this.Text = "Base Form";
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GraphicButton bOK;
        public GraphicButton bStorno;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel panelData;
    }
}