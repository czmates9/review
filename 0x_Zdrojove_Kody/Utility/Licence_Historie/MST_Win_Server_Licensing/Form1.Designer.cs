namespace MST_Win_Server_Licensing
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.dtExpiration = new System.Windows.Forms.DateTimePicker();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.txtNumberTerminal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtLicencePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dtExpirationRead = new System.Windows.Forms.DateTimePicker();
            this.txtContactRead = new System.Windows.Forms.TextBox();
            this.txtCompanyRead = new System.Windows.Forms.TextBox();
            this.txtNumberTerminalRead = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chkLicenceNeomezena = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(381, 326);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkLicenceNeomezena);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.dtExpiration);
            this.tabPage1.Controls.Add(this.txtContact);
            this.tabPage1.Controls.Add(this.txtCompany);
            this.tabPage1.Controls.Add(this.txtNumberTerminal);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(373, 300);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Vytvořit novou licenci";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(191, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 35);
            this.button2.TabIndex = 18;
            this.button2.Text = "Vytvořit licenci";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dtExpiration
            // 
            this.dtExpiration.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtExpiration.Location = new System.Drawing.Point(191, 39);
            this.dtExpiration.Name = "dtExpiration";
            this.dtExpiration.Size = new System.Drawing.Size(165, 20);
            this.dtExpiration.TabIndex = 17;
            // 
            // txtContact
            // 
            this.txtContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtContact.Location = new System.Drawing.Point(191, 122);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(165, 24);
            this.txtContact.TabIndex = 16;
            // 
            // txtCompany
            // 
            this.txtCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCompany.Location = new System.Drawing.Point(191, 92);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(165, 24);
            this.txtCompany.TabIndex = 15;
            // 
            // txtNumberTerminal
            // 
            this.txtNumberTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNumberTerminal.Location = new System.Drawing.Point(191, 9);
            this.txtNumberTerminal.Name = "txtNumberTerminal";
            this.txtNumberTerminal.Size = new System.Drawing.Size(165, 24);
            this.txtNumberTerminal.TabIndex = 14;
            this.txtNumberTerminal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberTerminal_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(7, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Kontakt na FASK:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(7, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Společnost:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(7, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Platnost licence:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(7, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Počet terminálu:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtLicencePath);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.dtExpirationRead);
            this.tabPage2.Controls.Add(this.txtContactRead);
            this.tabPage2.Controls.Add(this.txtCompanyRead);
            this.tabPage2.Controls.Add(this.txtNumberTerminalRead);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(373, 300);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Podepsat soubor s licencí";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtLicencePath
            // 
            this.txtLicencePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtLicencePath.Location = new System.Drawing.Point(192, 20);
            this.txtLicencePath.Name = "txtLicencePath";
            this.txtLicencePath.ReadOnly = true;
            this.txtLicencePath.Size = new System.Drawing.Size(165, 24);
            this.txtLicencePath.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(11, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 26);
            this.button1.TabIndex = 19;
            this.button1.Text = "Načíst licenci";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Location = new System.Drawing.Point(192, 176);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(165, 35);
            this.button3.TabIndex = 18;
            this.button3.Text = "Podepsat licenci";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dtExpirationRead
            // 
            this.dtExpirationRead.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtExpirationRead.Enabled = false;
            this.dtExpirationRead.Location = new System.Drawing.Point(192, 80);
            this.dtExpirationRead.Name = "dtExpirationRead";
            this.dtExpirationRead.Size = new System.Drawing.Size(165, 20);
            this.dtExpirationRead.TabIndex = 17;
            // 
            // txtContactRead
            // 
            this.txtContactRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtContactRead.Location = new System.Drawing.Point(192, 136);
            this.txtContactRead.Name = "txtContactRead";
            this.txtContactRead.ReadOnly = true;
            this.txtContactRead.Size = new System.Drawing.Size(165, 24);
            this.txtContactRead.TabIndex = 16;
            // 
            // txtCompanyRead
            // 
            this.txtCompanyRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCompanyRead.Location = new System.Drawing.Point(192, 106);
            this.txtCompanyRead.Name = "txtCompanyRead";
            this.txtCompanyRead.ReadOnly = true;
            this.txtCompanyRead.Size = new System.Drawing.Size(165, 24);
            this.txtCompanyRead.TabIndex = 15;
            // 
            // txtNumberTerminalRead
            // 
            this.txtNumberTerminalRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNumberTerminalRead.Location = new System.Drawing.Point(192, 50);
            this.txtNumberTerminalRead.Name = "txtNumberTerminalRead";
            this.txtNumberTerminalRead.ReadOnly = true;
            this.txtNumberTerminalRead.Size = new System.Drawing.Size(165, 24);
            this.txtNumberTerminalRead.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(8, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "Kontakt na FASK:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(8, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Společnost:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(8, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "Platnost licence:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(8, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 16);
            this.label12.TabIndex = 10;
            this.label12.Text = "Počet terminálu:";
            // 
            // chkLicenceNeomezena
            // 
            this.chkLicenceNeomezena.AutoSize = true;
            this.chkLicenceNeomezena.Location = new System.Drawing.Point(191, 66);
            this.chkLicenceNeomezena.Name = "chkLicenceNeomezena";
            this.chkLicenceNeomezena.Size = new System.Drawing.Size(83, 17);
            this.chkLicenceNeomezena.TabIndex = 19;
            this.chkLicenceNeomezena.Text = "Neomezená";
            this.chkLicenceNeomezena.UseVisualStyleBackColor = true;
            this.chkLicenceNeomezena.CheckedChanged += new System.EventHandler(this.chkLicenceNeomezena_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 326);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Licencování";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dtExpiration;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.TextBox txtNumberTerminal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dtExpirationRead;
        private System.Windows.Forms.TextBox txtContactRead;
        private System.Windows.Forms.TextBox txtCompanyRead;
        private System.Windows.Forms.TextBox txtNumberTerminalRead;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLicencePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkLicenceNeomezena;
    }
}

