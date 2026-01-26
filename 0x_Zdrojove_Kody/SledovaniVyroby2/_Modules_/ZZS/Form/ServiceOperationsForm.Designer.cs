namespace FASK.SledovaniVyroby.Module.ZZS.Forms
{
    partial class ServiceOperationsForm
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
            this.components = new System.ComponentModel.Container();
            this.labelDesc = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCancel = new Fask.Graphic.GraphicButton();
            this.SuspendLayout();
            // 
            // labelDesc
            // 
            this.labelDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelDesc.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelDesc.Location = new System.Drawing.Point(0, 43);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(342, 100);
            this.labelDesc.TabIndex = 3;
            this.labelDesc.Text = "Description";
            this.labelDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 43);
            this.panel1.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BitmapNormal = null;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonCancel.FocusMargin = 5;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(0, 274);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Pressed = false;
            this.buttonCancel.Size = new System.Drawing.Size(342, 49);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Zrušit";
            this.buttonCancel.Transparent = System.Drawing.Color.White;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ServiceOperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(342, 323);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelDesc);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ServiceOperationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServiceOperationsForm";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ServiceOperationsForm_Closing);
            this.Load += new System.EventHandler(this.ServiceOperationsForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServiceOperationsForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Timer timer1;
        private Fask.Graphic.GraphicButton buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}