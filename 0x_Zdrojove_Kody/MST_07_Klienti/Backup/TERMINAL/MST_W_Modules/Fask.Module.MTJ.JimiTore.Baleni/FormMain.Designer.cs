namespace Fask.Module.MTJ.JimiTore.Baleni
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnKontrola = new System.Windows.Forms.Button();
            this.btnPrijem = new System.Windows.Forms.Button();
            this.btnPrijemZbytku = new System.Windows.Forms.Button();
            this.btnVydej = new System.Windows.Forms.Button();
            this.btnBaleni = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.Text = "Akce";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Konec";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnKontrola);
            this.panel1.Controls.Add(this.btnPrijem);
            this.panel1.Controls.Add(this.btnPrijemZbytku);
            this.panel1.Controls.Add(this.btnVydej);
            this.panel1.Controls.Add(this.btnBaleni);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 210);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // btnKontrola
            // 
            this.btnKontrola.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnKontrola.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKontrola.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.btnKontrola.Location = new System.Drawing.Point(0, 156);
            this.btnKontrola.Name = "btnKontrola";
            this.btnKontrola.Size = new System.Drawing.Size(239, 39);
            this.btnKontrola.TabIndex = 6;
            this.btnKontrola.Text = "Kontrola výdeje";
            this.btnKontrola.Click += new System.EventHandler(this.btnKontrola_Click);
            // 
            // btnPrijem
            // 
            this.btnPrijem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnPrijem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrijem.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.btnPrijem.Location = new System.Drawing.Point(0, 117);
            this.btnPrijem.Name = "btnPrijem";
            this.btnPrijem.Size = new System.Drawing.Size(239, 39);
            this.btnPrijem.TabIndex = 5;
            this.btnPrijem.Text = "Příjem dle podkladu";
            this.btnPrijem.Click += new System.EventHandler(this.btnPrijem_Click);
            // 
            // btnPrijemZbytku
            // 
            this.btnPrijemZbytku.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnPrijemZbytku.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrijemZbytku.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.btnPrijemZbytku.Location = new System.Drawing.Point(0, 78);
            this.btnPrijemZbytku.Name = "btnPrijemZbytku";
            this.btnPrijemZbytku.Size = new System.Drawing.Size(239, 39);
            this.btnPrijemZbytku.TabIndex = 4;
            this.btnPrijemZbytku.Text = "Příjem zbytků";
            this.btnPrijemZbytku.Click += new System.EventHandler(this.btnPrijemZbytku_Click);
            // 
            // btnVydej
            // 
            this.btnVydej.BackColor = System.Drawing.Color.Cyan;
            this.btnVydej.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVydej.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.btnVydej.Location = new System.Drawing.Point(0, 39);
            this.btnVydej.Name = "btnVydej";
            this.btnVydej.Size = new System.Drawing.Size(239, 39);
            this.btnVydej.TabIndex = 3;
            this.btnVydej.Text = "Výdej";
            this.btnVydej.Click += new System.EventHandler(this.btnVydej_Click);
            // 
            // btnBaleni
            // 
            this.btnBaleni.BackColor = System.Drawing.Color.LimeGreen;
            this.btnBaleni.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaleni.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.btnBaleni.Location = new System.Drawing.Point(0, 0);
            this.btnBaleni.Name = "btnBaleni";
            this.btnBaleni.Size = new System.Drawing.Size(239, 39);
            this.btnBaleni.TabIndex = 2;
            this.btnBaleni.Text = "Balení";
            this.btnBaleni.Click += new System.EventHandler(this.btnBaleni_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(239, 210);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormMain";
            this.Text = "Jimi Tore";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBaleni;
        private System.Windows.Forms.Button btnVydej;
        private System.Windows.Forms.Button btnPrijemZbytku;
        private System.Windows.Forms.Button btnPrijem;
        private System.Windows.Forms.Button btnKontrola;
    }
}