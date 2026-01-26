namespace Fask.Vyroba_P.Scanner
{
    partial class FormScannerCOMSettings
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.pgObject = new System.Windows.Forms.PropertyGrid();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 728);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(772, 50);
            this.panelButtons.TabIndex = 2;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(652, 50);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 50);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // pgObject
            // 
            this.pgObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgObject.Location = new System.Drawing.Point(0, 0);
            this.pgObject.Name = "pgObject";
            this.pgObject.Size = new System.Drawing.Size(772, 446);
            this.pgObject.TabIndex = 3;
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard_without_arrows;
            this.keyboardcontrol1.Location = new System.Drawing.Point(0, 446);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(772, 282);
            this.keyboardcontrol1.TabIndex = 4;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // FormScannerCOMSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 778);
            this.ControlBox = false;
            this.Controls.Add(this.pgObject);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.panelButtons);
            this.Name = "FormScannerCOMSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scanner settings";
            this.Load += new System.EventHandler(this.FormScannerCOMSettings_Load);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.PropertyGrid pgObject;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
    }
}