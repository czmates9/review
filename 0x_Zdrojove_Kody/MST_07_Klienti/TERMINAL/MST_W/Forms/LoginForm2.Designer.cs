
namespace Fask.MST_W.Forms
{
    partial class LoginForm2
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm2));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.konec_but = new System.Windows.Forms.Button();
			this.ok_but = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panelHeslo = new System.Windows.Forms.Panel();
			this.chkPasswordShow = new System.Windows.Forms.CheckBox();
			this.heslo_tb = new System.Windows.Forms.TextBox();
			this.labelHeslo = new System.Windows.Forms.Label();
			this.panelLogin = new System.Windows.Forms.Panel();
			this.login_tb = new System.Windows.Forms.ComboBox();
			this.labelLogin = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panelHeslo.SuspendLayout();
			this.panelLogin.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem4);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.MenuItems.Add(this.menuItem3);
			resources.ApplyResources(this.menuItem1, "menuItem1");
			// 
			// menuItem2
			// 
			resources.ApplyResources(this.menuItem2, "menuItem2");
			// 
			// menuItem3
			// 
			resources.ApplyResources(this.menuItem3, "menuItem3");
			// 
			// menuItem4
			// 
			this.menuItem4.MenuItems.Add(this.menuItem5);
			resources.ApplyResources(this.menuItem4, "menuItem4");
			// 
			// menuItem5
			// 
			resources.ApplyResources(this.menuItem5, "menuItem5");
			this.menuItem5.Click += new System.EventHandler(this.buttonLoginsUpdate_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.konec_but);
			this.panel1.Controls.Add(this.ok_but);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
			// 
			// konec_but
			// 
			resources.ApplyResources(this.konec_but, "konec_but");
			this.konec_but.Name = "konec_but";
			this.konec_but.TabStop = false;
			this.konec_but.Click += new System.EventHandler(this.konec_but_Click);
			// 
			// ok_but
			// 
			resources.ApplyResources(this.ok_but, "ok_but");
			this.ok_but.Name = "ok_but";
			this.ok_but.TabStop = false;
			this.ok_but.Click += new System.EventHandler(this.ok_but_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panelHeslo);
			this.panel2.Controls.Add(this.panelLogin);
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Name = "panel2";
			// 
			// panelHeslo
			// 
			this.panelHeslo.Controls.Add(this.chkPasswordShow);
			this.panelHeslo.Controls.Add(this.heslo_tb);
			this.panelHeslo.Controls.Add(this.labelHeslo);
			resources.ApplyResources(this.panelHeslo, "panelHeslo");
			this.panelHeslo.Name = "panelHeslo";
			// 
			// chkPasswordShow
			// 
			resources.ApplyResources(this.chkPasswordShow, "chkPasswordShow");
			this.chkPasswordShow.Name = "chkPasswordShow";
			this.chkPasswordShow.CheckStateChanged += new System.EventHandler(this.chkPasswordShow_CheckStateChanged);
			// 
			// heslo_tb
			// 
			resources.ApplyResources(this.heslo_tb, "heslo_tb");
			this.heslo_tb.Name = "heslo_tb";
			// 
			// labelHeslo
			// 
			resources.ApplyResources(this.labelHeslo, "labelHeslo");
			this.labelHeslo.Name = "labelHeslo";
			// 
			// panelLogin
			// 
			this.panelLogin.Controls.Add(this.login_tb);
			this.panelLogin.Controls.Add(this.labelLogin);
			resources.ApplyResources(this.panelLogin, "panelLogin");
			this.panelLogin.Name = "panelLogin";
			// 
			// login_tb
			// 
			resources.ApplyResources(this.login_tb, "login_tb");
			this.login_tb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.login_tb.Name = "login_tb";
			// 
			// labelLogin
			// 
			resources.ApplyResources(this.labelLogin, "labelLogin");
			this.labelLogin.Name = "labelLogin";
			// 
			// LoginForm2
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Menu = this.mainMenu1;
			this.Name = "LoginForm2";
			this.Load += new System.EventHandler(this.LoginForm2_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm2_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panelHeslo.ResumeLayout(false);
			this.panelLogin.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button konec_but;
        private System.Windows.Forms.Button ok_but;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Panel panelHeslo;
        private System.Windows.Forms.CheckBox chkPasswordShow;
        private System.Windows.Forms.TextBox heslo_tb;
        private System.Windows.Forms.Label labelHeslo;
        private System.Windows.Forms.ComboBox login_tb;
        private System.Windows.Forms.Label labelLogin;
    }
}