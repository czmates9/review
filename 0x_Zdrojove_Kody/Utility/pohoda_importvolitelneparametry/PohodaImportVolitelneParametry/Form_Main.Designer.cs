namespace PohodaImportVolitelneParametry
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chb_Grafika = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.konfiguraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 58);
            this.button1.TabIndex = 0;
            this.button1.Text = "Import seznam cez XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(169, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 58);
            this.button2.TabIndex = 2;
            this.button2.Text = "Import Parametry cez XML";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chb_Grafika
            // 
            this.chb_Grafika.AutoSize = true;
            this.chb_Grafika.Location = new System.Drawing.Point(183, 169);
            this.chb_Grafika.Name = "chb_Grafika";
            this.chb_Grafika.Size = new System.Drawing.Size(60, 17);
            this.chb_Grafika.TabIndex = 3;
            this.chb_Grafika.Text = "Grafika";
            this.chb_Grafika.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(32, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "1st";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(177, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "2nd";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konfiguraceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(329, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // konfiguraceToolStripMenuItem
            // 
            this.konfiguraceToolStripMenuItem.Name = "konfiguraceToolStripMenuItem";
            this.konfiguraceToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.konfiguraceToolStripMenuItem.Text = "Konfigurace";
            this.konfiguraceToolStripMenuItem.Click += new System.EventHandler(this.konfiguraceToolStripMenuItem_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 226);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chb_Grafika);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Pohoda";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chb_Grafika;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem konfiguraceToolStripMenuItem;
    }
}

