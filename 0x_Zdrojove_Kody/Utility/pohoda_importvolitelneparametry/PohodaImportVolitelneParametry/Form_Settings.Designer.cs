namespace PohodaImportVolitelneParametry
{
    partial class Form_Settings
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
            this.components = new System.ComponentModel.Container();
            this.tb_Catalog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_ICO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_LoginPohoda = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Heslo2Pohoda = new System.Windows.Forms.TextBox();
            this.tb_Heslo1Pohoda = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnStorno = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tb_PathPohoda = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_LoginSQL = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_Heslo2SQL = new System.Windows.Forms.TextBox();
            this.tb_Heslo1SQL = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_Data_Source = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tb_LoginUNC = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_Heslo2UNC = new System.Windows.Forms.TextBox();
            this.tb_Heslo1UNC = new System.Windows.Forms.TextBox();
            this.tb_UNCPath = new System.Windows.Forms.TextBox();
            this.tb_Letter = new System.Windows.Forms.TextBox();
            this.tb_Domain = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.cb_Provider = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_Catalog
            // 
            this.tb_Catalog.Location = new System.Drawing.Point(142, 56);
            this.tb_Catalog.Name = "tb_Catalog";
            this.tb_Catalog.Size = new System.Drawing.Size(165, 20);
            this.tb_Catalog.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Název Databáze Pohody";
            // 
            // tb_ICO
            // 
            this.tb_ICO.Location = new System.Drawing.Point(142, 30);
            this.tb_ICO.Name = "tb_ICO";
            this.tb_ICO.Size = new System.Drawing.Size(165, 20);
            this.tb_ICO.TabIndex = 0;
            this.tb_ICO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_ICO_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ICO společnosti";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_LoginPohoda);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_Heslo2Pohoda);
            this.groupBox1.Controls.Add(this.tb_Heslo1Pohoda);
            this.groupBox1.Location = new System.Drawing.Point(329, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 158);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Příhlásení do Pohody";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(289, 38);
            this.label6.TabIndex = 2;
            this.label6.Text = "Heslo se s konfigurace nenačítavá, pokud chcete heslo zmenit zadejte dvakrát totž" +
    "né heslo.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Heslo 2 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Heslo 1 :";
            // 
            // tb_LoginPohoda
            // 
            this.tb_LoginPohoda.Location = new System.Drawing.Point(130, 29);
            this.tb_LoginPohoda.Name = "tb_LoginPohoda";
            this.tb_LoginPohoda.Size = new System.Drawing.Size(165, 20);
            this.tb_LoginPohoda.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Login :";
            // 
            // tb_Heslo2Pohoda
            // 
            this.tb_Heslo2Pohoda.Location = new System.Drawing.Point(130, 84);
            this.tb_Heslo2Pohoda.Name = "tb_Heslo2Pohoda";
            this.tb_Heslo2Pohoda.PasswordChar = '@';
            this.tb_Heslo2Pohoda.Size = new System.Drawing.Size(165, 20);
            this.tb_Heslo2Pohoda.TabIndex = 2;
            // 
            // tb_Heslo1Pohoda
            // 
            this.tb_Heslo1Pohoda.Location = new System.Drawing.Point(130, 58);
            this.tb_Heslo1Pohoda.Name = "tb_Heslo1Pohoda";
            this.tb_Heslo1Pohoda.PasswordChar = '@';
            this.tb_Heslo1Pohoda.Size = new System.Drawing.Size(165, 20);
            this.tb_Heslo1Pohoda.TabIndex = 1;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnStorno);
            this.panelButtons.Controls.Add(this.btnOK);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 321);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(995, 72);
            this.panelButtons.TabIndex = 1;
            // 
            // btnStorno
            // 
            this.btnStorno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStorno.Location = new System.Drawing.Point(0, 0);
            this.btnStorno.Name = "btnStorno";
            this.btnStorno.Size = new System.Drawing.Size(476, 72);
            this.btnStorno.TabIndex = 0;
            this.btnStorno.Text = "Zrušit";
            this.btnStorno.UseVisualStyleBackColor = true;
            this.btnStorno.Click += new System.EventHandler(this.btnStorno_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(476, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(519, 72);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tb_PathPohoda
            // 
            this.tb_PathPohoda.Location = new System.Drawing.Point(459, 108);
            this.tb_PathPohoda.Name = "tb_PathPohoda";
            this.tb_PathPohoda.Size = new System.Drawing.Size(165, 20);
            this.tb_PathPohoda.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(370, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Cesta k Pohode";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tb_LoginSQL);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tb_Heslo2SQL);
            this.groupBox2.Controls.Add(this.tb_Heslo1SQL);
            this.groupBox2.Location = new System.Drawing.Point(12, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 158);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Příhlásení do SQL";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(289, 38);
            this.label8.TabIndex = 2;
            this.label8.Text = "Heslo se s konfigurace nenačítavá, pokud chcete heslo zmenit zadejte dvakrát totž" +
    "né heslo.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(75, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Heslo 2 :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(75, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Heslo 1 :";
            // 
            // tb_LoginSQL
            // 
            this.tb_LoginSQL.Location = new System.Drawing.Point(130, 29);
            this.tb_LoginSQL.Name = "tb_LoginSQL";
            this.tb_LoginSQL.Size = new System.Drawing.Size(165, 20);
            this.tb_LoginSQL.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(85, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Login :";
            // 
            // tb_Heslo2SQL
            // 
            this.tb_Heslo2SQL.Location = new System.Drawing.Point(130, 84);
            this.tb_Heslo2SQL.Name = "tb_Heslo2SQL";
            this.tb_Heslo2SQL.PasswordChar = '@';
            this.tb_Heslo2SQL.Size = new System.Drawing.Size(165, 20);
            this.tb_Heslo2SQL.TabIndex = 2;
            // 
            // tb_Heslo1SQL
            // 
            this.tb_Heslo1SQL.Location = new System.Drawing.Point(130, 58);
            this.tb_Heslo1SQL.Name = "tb_Heslo1SQL";
            this.tb_Heslo1SQL.PasswordChar = '@';
            this.tb_Heslo1SQL.Size = new System.Drawing.Size(165, 20);
            this.tb_Heslo1SQL.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(90, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Provider";
            // 
            // tb_Data_Source
            // 
            this.tb_Data_Source.Location = new System.Drawing.Point(142, 108);
            this.tb_Data_Source.Name = "tb_Data_Source";
            this.tb_Data_Source.Size = new System.Drawing.Size(165, 20);
            this.tb_Data_Source.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(70, 111);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Instance DB";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.tb_LoginUNC);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.tb_Heslo2UNC);
            this.groupBox3.Controls.Add(this.tb_Heslo1UNC);
            this.groupBox3.Location = new System.Drawing.Point(655, 140);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 158);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Příhlásení do Mapovana UNC cesty";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(6, 111);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(289, 38);
            this.label14.TabIndex = 2;
            this.label14.Text = "Heslo se s konfigurace nenačítavá, pokud chcete heslo zmenit zadejte dvakrát totž" +
    "né heslo.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(75, 87);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Heslo 2 :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(75, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Heslo 1 :";
            // 
            // tb_LoginUNC
            // 
            this.tb_LoginUNC.Location = new System.Drawing.Point(130, 29);
            this.tb_LoginUNC.Name = "tb_LoginUNC";
            this.tb_LoginUNC.Size = new System.Drawing.Size(165, 20);
            this.tb_LoginUNC.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(85, 32);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Login :";
            // 
            // tb_Heslo2UNC
            // 
            this.tb_Heslo2UNC.Location = new System.Drawing.Point(130, 84);
            this.tb_Heslo2UNC.Name = "tb_Heslo2UNC";
            this.tb_Heslo2UNC.PasswordChar = '@';
            this.tb_Heslo2UNC.Size = new System.Drawing.Size(165, 20);
            this.tb_Heslo2UNC.TabIndex = 2;
            // 
            // tb_Heslo1UNC
            // 
            this.tb_Heslo1UNC.Location = new System.Drawing.Point(130, 58);
            this.tb_Heslo1UNC.Name = "tb_Heslo1UNC";
            this.tb_Heslo1UNC.PasswordChar = '@';
            this.tb_Heslo1UNC.Size = new System.Drawing.Size(165, 20);
            this.tb_Heslo1UNC.TabIndex = 1;
            // 
            // tb_UNCPath
            // 
            this.tb_UNCPath.Location = new System.Drawing.Point(785, 56);
            this.tb_UNCPath.Name = "tb_UNCPath";
            this.tb_UNCPath.Size = new System.Drawing.Size(165, 20);
            this.tb_UNCPath.TabIndex = 5;
            // 
            // tb_Letter
            // 
            this.tb_Letter.Location = new System.Drawing.Point(785, 108);
            this.tb_Letter.Name = "tb_Letter";
            this.tb_Letter.Size = new System.Drawing.Size(165, 20);
            this.tb_Letter.TabIndex = 7;
            // 
            // tb_Domain
            // 
            this.tb_Domain.Location = new System.Drawing.Point(785, 82);
            this.tb_Domain.Name = "tb_Domain";
            this.tb_Domain.Size = new System.Drawing.Size(165, 20);
            this.tb_Domain.TabIndex = 6;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(720, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "UNC Cesta";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(703, 111);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "Pismeno Disku";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(733, 85);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "Domena";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.cb_Provider);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.groupBox3);
            this.panelMain.Controls.Add(this.tb_Catalog);
            this.panelMain.Controls.Add(this.tb_Data_Source);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.tb_UNCPath);
            this.panelMain.Controls.Add(this.label7);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.tb_Letter);
            this.panelMain.Controls.Add(this.label20);
            this.panelMain.Controls.Add(this.tb_Domain);
            this.panelMain.Controls.Add(this.label12);
            this.panelMain.Controls.Add(this.tb_ICO);
            this.panelMain.Controls.Add(this.label19);
            this.panelMain.Controls.Add(this.tb_PathPohoda);
            this.panelMain.Controls.Add(this.label13);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.label18);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(995, 321);
            this.panelMain.TabIndex = 0;
            // 
            // cb_Provider
            // 
            this.cb_Provider.FormattingEnabled = true;
            this.cb_Provider.Items.AddRange(new object[] {
            "Microsoft.Jet.OLEDB.4.0",
            "Microsoft.ACE.OLEDB.12.0",
            "SQLNCLI10",
            "SQLNCLI11"});
            this.cb_Provider.Location = new System.Drawing.Point(142, 82);
            this.cb_Provider.Name = "cb_Provider";
            this.cb_Provider.Size = new System.Drawing.Size(165, 21);
            this.cb_Provider.TabIndex = 11;
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 393);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nastavení";
            this.Load += new System.EventHandler(this.Form_Settings_Load);
            this.Resize += new System.EventHandler(this.Form_Settings_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tb_Catalog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_ICO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_LoginPohoda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Heslo2Pohoda;
        private System.Windows.Forms.TextBox tb_Heslo1Pohoda;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnStorno;
        private System.Windows.Forms.TextBox tb_PathPohoda;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_LoginSQL;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_Heslo2SQL;
        private System.Windows.Forms.TextBox tb_Heslo1SQL;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_Data_Source;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tb_LoginUNC;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tb_Heslo2UNC;
        private System.Windows.Forms.TextBox tb_Heslo1UNC;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tb_Domain;
        private System.Windows.Forms.TextBox tb_Letter;
        private System.Windows.Forms.TextBox tb_UNCPath;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ComboBox cb_Provider;
    }
}