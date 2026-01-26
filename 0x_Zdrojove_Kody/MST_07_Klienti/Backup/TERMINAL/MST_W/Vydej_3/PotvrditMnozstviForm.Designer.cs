namespace Fask.MST_W.Vydej_3
{
    partial class PotvrditMnozstviForm
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
            this.nacist_l = new System.Windows.Forms.Label();
            this.nacteno_l = new System.Windows.Forms.Label();
            this.zpet_but = new System.Windows.Forms.Button();
            this.ok_but = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nacist_l
            // 
            this.nacist_l.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nacist_l.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.nacist_l.Location = new System.Drawing.Point(3, 0);
            this.nacist_l.Name = "nacist_l";
            this.nacist_l.Size = new System.Drawing.Size(234, 20);
            this.nacist_l.Text = "Naèíst: ";
            // 
            // nacteno_l
            // 
            this.nacteno_l.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nacteno_l.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.nacteno_l.Location = new System.Drawing.Point(3, 20);
            this.nacteno_l.Name = "nacteno_l";
            this.nacteno_l.Size = new System.Drawing.Size(234, 20);
            this.nacteno_l.Text = "Naèteno: ";
            // 
            // zpet_but
            // 
            this.zpet_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zpet_but.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.zpet_but.Location = new System.Drawing.Point(122, 241);
            this.zpet_but.Name = "zpet_but";
            this.zpet_but.Size = new System.Drawing.Size(115, 50);
            this.zpet_but.TabIndex = 10;
            this.zpet_but.Text = "Zpìt";
            // 
            // ok_but
            // 
            this.ok_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ok_but.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok_but.Location = new System.Drawing.Point(3, 241);
            this.ok_but.Name = "ok_but";
            this.ok_but.Size = new System.Drawing.Size(115, 50);
            this.ok_but.TabIndex = 9;
            this.ok_but.Text = "OK";
            // 
            // PotvrditMnozstviForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.zpet_but);
            this.Controls.Add(this.ok_but);
            this.Controls.Add(this.nacteno_l);
            this.Controls.Add(this.nacist_l);
            this.Name = "PotvrditMnozstviForm";
            this.Text = "Potvrïte množství";
            this.Load += new System.EventHandler(this.PotvrditMnozstviForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label nacist_l;
        private System.Windows.Forms.Label nacteno_l;
        private System.Windows.Forms.Button zpet_but;
        private System.Windows.Forms.Button ok_but;
    }
}