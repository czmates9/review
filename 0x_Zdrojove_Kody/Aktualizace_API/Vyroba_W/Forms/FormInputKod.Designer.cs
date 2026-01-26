namespace Fask.Vyroba_W.Forms
{
    partial class FormInputKod
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(518, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.textBoxKod);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(638, 384);
            // 
            // textBoxKod
            // 
            this.textBoxKod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKod.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxKod.Location = new System.Drawing.Point(20, 23);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.Size = new System.Drawing.Size(595, 32);
            this.textBoxKod.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 384);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(638, 71);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // FormInputKod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormInputKod";
            this.Text = "Zadejte kod";
            this.Load += new System.EventHandler(this.FormInputKod_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInputKod_KeyDown);
            this.panelComponents.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        protected System.Windows.Forms.TextBox textBoxKod;
    }
}
