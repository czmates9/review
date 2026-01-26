namespace Fask.MST_W.ServisModule
{
    partial class ServisAnoNe
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
            this.miAno = new System.Windows.Forms.MenuItem();
            this.miNe = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miPrerusit = new System.Windows.Forms.MenuItem();
            this.tbText = new System.Windows.Forms.Label();
            this.btnAno = new System.Windows.Forms.Button();
            this.btnNe = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnStorno = new System.Windows.Forms.Button();
            this.statusBarInfo = new System.Windows.Forms.StatusBar();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.miAno);
            this.menuItem1.MenuItems.Add(this.miNe);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.miPrerusit);
            this.menuItem1.Text = "Menu";
            // 
            // miAno
            // 
            this.miAno.Text = "Ano";
            this.miAno.Click += new System.EventHandler(this.miAno_Click);
            // 
            // miNe
            // 
            this.miNe.Text = "Ne";
            this.miNe.Click += new System.EventHandler(this.miNe_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Krok zpět";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // miPrerusit
            // 
            this.miPrerusit.Text = "Storno";
            this.miPrerusit.Click += new System.EventHandler(this.miPrerusit_Click);
            // 
            // tbText
            // 
            this.tbText.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbText.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.tbText.Location = new System.Drawing.Point(0, 0);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(242, 95);
            // 
            // btnAno
            // 
            this.btnAno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAno.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnAno.Location = new System.Drawing.Point(153, 0);
            this.btnAno.Name = "btnAno";
            this.btnAno.Size = new System.Drawing.Size(89, 38);
            this.btnAno.TabIndex = 3;
            this.btnAno.Text = "Ano";
            this.btnAno.Click += new System.EventHandler(this.btnAno_Click);
            // 
            // btnNe
            // 
            this.btnNe.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNe.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnNe.Location = new System.Drawing.Point(87, 0);
            this.btnNe.Name = "btnNe";
            this.btnNe.Size = new System.Drawing.Size(66, 38);
            this.btnNe.TabIndex = 9;
            this.btnNe.Text = "Ne";
            this.btnNe.Click += new System.EventHandler(this.btnNe_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnAno);
            this.panelButtons.Controls.Add(this.btnNe);
            this.panelButtons.Controls.Add(this.btnStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 207);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(242, 38);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // btnStorno
            // 
            this.btnStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStorno.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnStorno.Location = new System.Drawing.Point(0, 0);
            this.btnStorno.Name = "btnStorno";
            this.btnStorno.Size = new System.Drawing.Size(87, 38);
            this.btnStorno.TabIndex = 10;
            this.btnStorno.Text = "Storno";
            this.btnStorno.Click += new System.EventHandler(this.miPrerusit_Click);
            // 
            // statusBarInfo
            // 
            this.statusBarInfo.Location = new System.Drawing.Point(0, 183);
            this.statusBarInfo.Name = "statusBarInfo";
            this.statusBarInfo.Size = new System.Drawing.Size(242, 24);
            // 
            // ServisAnoNe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(242, 245);
            this.ControlBox = false;
            this.Controls.Add(this.statusBarInfo);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.tbText);
            this.Menu = this.mainMenu1;
            this.Name = "ServisAnoNe";
            this.Text = "ServisAnoNe";
            this.Load += new System.EventHandler(this.ServisAnoNe_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisAnoNe_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miAno;
        private System.Windows.Forms.MenuItem miNe;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miPrerusit;
        private System.Windows.Forms.Label tbText;
        private System.Windows.Forms.Button btnAno;
        private System.Windows.Forms.Button btnNe;
        protected System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.StatusBar statusBarInfo;
        private System.Windows.Forms.Button btnStorno;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}