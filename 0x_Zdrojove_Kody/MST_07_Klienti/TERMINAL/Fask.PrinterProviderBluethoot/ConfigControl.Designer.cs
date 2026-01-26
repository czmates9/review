namespace Fask.PrinterProviderBluetooth
{
    partial class ConfigControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.buttonTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 20);
            this.label1.Text = "Bluetooth";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(4, 24);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(94, 30);
            this.buttonTest.TabIndex = 1;
            this.buttonTest.Text = "Test";
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // ConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.label1);
            this.Name = "ConfigControl";
            this.Size = new System.Drawing.Size(273, 236);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonTest;
    }
}
