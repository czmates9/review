namespace Fask.MST_W.Forms
{
    partial class ServisSejmiKodForm
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
            this.kod_tb = new System.Windows.Forms.TextBox();
            this.popis_l = new System.Windows.Forms.Label();
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelKod = new System.Windows.Forms.Panel();
            this.statusBarInfo = new System.Windows.Forms.StatusBar();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemOK = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemCancel = new System.Windows.Forms.MenuItem();
            this.panelButtons.SuspendLayout();
            this.panelKod.SuspendLayout();
            this.SuspendLayout();
            // 
            // kod_tb
            // 
            this.kod_tb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kod_tb.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.kod_tb.Location = new System.Drawing.Point(3, 23);
            this.kod_tb.Name = "kod_tb";
            this.kod_tb.Size = new System.Drawing.Size(236, 22);
            this.kod_tb.TabIndex = 0;
            // 
            // popis_l
            // 
            this.popis_l.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.popis_l.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.popis_l.Location = new System.Drawing.Point(3, 3);
            this.popis_l.Name = "popis_l";
            this.popis_l.Size = new System.Drawing.Size(236, 20);
            this.popis_l.Text = "Vlož kód:";
            // 
            // zpet_but
            // 
            this.zpet_but.BitmapNormal = null;
            this.zpet_but.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zpet_but.FocusMargin = 5;
            this.zpet_but.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.zpet_but.Location = new System.Drawing.Point(0, 0);
            this.zpet_but.Name = "zpet_but";
            this.zpet_but.Pressed = false;
            this.zpet_but.Size = new System.Drawing.Size(116, 38);
            this.zpet_but.TabIndex = 17;
            this.zpet_but.Text = "Storno";
            this.zpet_but.Transparent = System.Drawing.Color.White;
            this.zpet_but.Click += new System.EventHandler(this.zpet_but_Click);
            // 
            // ok_but
            // 
            this.ok_but.BitmapNormal = null;
            this.ok_but.Dock = System.Windows.Forms.DockStyle.Right;
            this.ok_but.FocusMargin = 5;
            this.ok_but.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.ok_but.Location = new System.Drawing.Point(116, 0);
            this.ok_but.Name = "ok_but";
            this.ok_but.Pressed = false;
            this.ok_but.Size = new System.Drawing.Size(126, 38);
            this.ok_but.TabIndex = 16;
            this.ok_but.Text = "OK";
            this.ok_but.Transparent = System.Drawing.Color.White;
            this.ok_but.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.zpet_but);
            this.panelButtons.Controls.Add(this.ok_but);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 207);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(242, 38);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // panelKod
            // 
            this.panelKod.Controls.Add(this.popis_l);
            this.panelKod.Controls.Add(this.kod_tb);
            this.panelKod.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKod.Location = new System.Drawing.Point(0, 0);
            this.panelKod.Name = "panelKod";
            this.panelKod.Size = new System.Drawing.Size(242, 49);
            // 
            // statusBarInfo
            // 
            this.statusBarInfo.Location = new System.Drawing.Point(0, 183);
            this.statusBarInfo.Name = "statusBarInfo";
            this.statusBarInfo.Size = new System.Drawing.Size(242, 24);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemOK);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemCancel);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemOK
            // 
            this.menuItemOK.Text = "OK";
            this.menuItemOK.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Krok zpìt";
            this.menuItem2.Click += new System.EventHandler(this.krokzpet_but_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // menuItemCancel
            // 
            this.menuItemCancel.Text = "Storno";
            this.menuItemCancel.Click += new System.EventHandler(this.zpet_but_Click);
            // 
            // ServisSejmiKodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(242, 245);
            this.ControlBox = false;
            this.Controls.Add(this.statusBarInfo);
            this.Controls.Add(this.panelKod);
            this.Controls.Add(this.panelButtons);
            this.Menu = this.mainMenu1;
            this.Name = "ServisSejmiKodForm";
            this.Text = "Vlož kód";
            this.Load += new System.EventHandler(this.ServisSejmiKodForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ServisSejmiKodForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisSejmiKodForm_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelKod.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        protected System.Windows.Forms.Panel panelKod;
        protected System.Windows.Forms.TextBox kod_tb;
        protected System.Windows.Forms.Label popis_l;
        private System.Windows.Forms.StatusBar statusBarInfo;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemOK;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItemCancel;
    }
}