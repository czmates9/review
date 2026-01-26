namespace Fask.MST_W.Vydej_3
{
    partial class Detail
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
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.labelDetail = new System.Windows.Forms.Label();
            this.labelDetailColumn = new System.Windows.Forms.Label();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.BitmapNormal = null;
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonOK.FocusMargin = 5;
            this.buttonOK.Location = new System.Drawing.Point(0, 265);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Pressed = false;
            this.buttonOK.Size = new System.Drawing.Size(240, 29);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.Transparent = System.Drawing.Color.White;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelDetail
            // 
            this.panelDetail.AutoScroll = true;
            this.panelDetail.Controls.Add(this.labelDetailColumn);
            this.panelDetail.Controls.Add(this.labelDetail);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(0, 0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(240, 265);
            this.panelDetail.Visible = false;
            // 
            // labelDetail
            // 
            this.labelDetail.Location = new System.Drawing.Point(47, 0);
            this.labelDetail.Name = "labelDetail";
            this.labelDetail.Size = new System.Drawing.Size(190, 26);
            // 
            // labelDetailColumn
            // 
            this.labelDetailColumn.Location = new System.Drawing.Point(0, 0);
            this.labelDetailColumn.Name = "labelDetailColumn";
            this.labelDetailColumn.Size = new System.Drawing.Size(41, 26);
            // 
            // Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.buttonOK);
            this.KeyPreview = true;
            this.Name = "Detail";
            this.Text = "Detail";
            this.Load += new System.EventHandler(this.Detail_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Detail_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Detail_KeyDown);
            this.panelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonOK;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label labelDetail;
        private System.Windows.Forms.Label labelDetailColumn;
    }
}