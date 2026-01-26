namespace Fask.MST_W.Inventura2
{
    partial class Inventura2
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
            this.buttonKonec = new Fask.Graphic.GraphicButton();
            this.vratitDavku_but = new Fask.Graphic.GraphicButton();
            this.odesliHotovouDavku_but = new Fask.Graphic.GraphicButton();
            this.buttonZpracujDavku = new Fask.Graphic.GraphicButton();
            this.buttonStahniDavku = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonKonec
            // 
            this.buttonKonec.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonKonec.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.buttonKonec.Location = new System.Drawing.Point(0, 242);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(234, 45);
            this.buttonKonec.TabIndex = 4;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click_1);
            // 
            // vratitDavku_but
            // 
            this.vratitDavku_but.Dock = System.Windows.Forms.DockStyle.Top;
            this.vratitDavku_but.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.vratitDavku_but.Location = new System.Drawing.Point(0, 140);
            this.vratitDavku_but.Name = "vratitDavku_but";
            this.vratitDavku_but.Size = new System.Drawing.Size(234, 50);
            this.vratitDavku_but.TabIndex = 3;
            this.vratitDavku_but.Text = "Vrátit dávku";
            this.vratitDavku_but.Click += new System.EventHandler(this.vratitDavku_but_Click);
            this.vratitDavku_but.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vratitDavku_but_KeyDown);
            // 
            // odesliHotovouDavku_but
            // 
            this.odesliHotovouDavku_but.Dock = System.Windows.Forms.DockStyle.Top;
            this.odesliHotovouDavku_but.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.odesliHotovouDavku_but.Location = new System.Drawing.Point(0, 90);
            this.odesliHotovouDavku_but.Name = "odesliHotovouDavku_but";
            this.odesliHotovouDavku_but.Size = new System.Drawing.Size(234, 50);
            this.odesliHotovouDavku_but.TabIndex = 2;
            this.odesliHotovouDavku_but.Text = "Odeslat dávku";
            this.odesliHotovouDavku_but.Click += new System.EventHandler(this.odesliHotovouDavku_but_Click);
            this.odesliHotovouDavku_but.KeyDown += new System.Windows.Forms.KeyEventHandler(this.odesliHotovouDavku_but_KeyDown);
            // 
            // buttonZpracujDavku
            // 
            this.buttonZpracujDavku.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonZpracujDavku.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.buttonZpracujDavku.Location = new System.Drawing.Point(0, 0);
            this.buttonZpracujDavku.Name = "buttonZpracujDavku";
            this.buttonZpracujDavku.Size = new System.Drawing.Size(234, 45);
            this.buttonZpracujDavku.TabIndex = 0;
            this.buttonZpracujDavku.Text = "Zpracuj dávku";
            this.buttonZpracujDavku.Click += new System.EventHandler(this.buttonZpracujDavku_Click);
            this.buttonZpracujDavku.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonZpracujDavku_KeyDown);
            // 
            // buttonStahniDavku
            // 
            this.buttonStahniDavku.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonStahniDavku.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.buttonStahniDavku.Location = new System.Drawing.Point(0, 45);
            this.buttonStahniDavku.Name = "buttonStahniDavku";
            this.buttonStahniDavku.Size = new System.Drawing.Size(234, 45);
            this.buttonStahniDavku.TabIndex = 1;
            this.buttonStahniDavku.Text = "Stáhni dávku";
            this.buttonStahniDavku.Click += new System.EventHandler(this.buttonStahniDavku_Click);
            this.buttonStahniDavku.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonStahniDavku_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.vratitDavku_but);
            this.panel1.Controls.Add(this.buttonKonec);
            this.panel1.Controls.Add(this.odesliHotovouDavku_but);
            this.panel1.Controls.Add(this.buttonStahniDavku);
            this.panel1.Controls.Add(this.buttonZpracujDavku);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 287);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.Text = "Modul";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Konec";
            this.menuItem2.Click += new System.EventHandler(this.buttonKonec_Click_1);
            // 
            // Inventura2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(234, 287);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Inventura2";
            this.Text = "Inventura 2";
            this.Load += new System.EventHandler(this.Inventura2Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Inventura2Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonKonec;
        private Fask.Graphic.GraphicButton vratitDavku_but;
        private Fask.Graphic.GraphicButton odesliHotovouDavku_but;
        private Fask.Graphic.GraphicButton buttonZpracujDavku;
        private Fask.Graphic.GraphicButton buttonStahniDavku;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}