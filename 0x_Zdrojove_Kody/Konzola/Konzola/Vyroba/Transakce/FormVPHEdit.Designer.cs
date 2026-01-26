namespace Konzola.Vyroba
{
    partial class FormVPHEdit
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
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_Active = new System.Windows.Forms.ComboBox();
            this.btn_VPH = new System.Windows.Forms.Button();
            this.textBoxPopisZakazky = new System.Windows.Forms.TextBox();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.tB_Typ = new System.Windows.Forms.TextBox();
            this.textBoxCarovyKod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tB_DateProd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Číslo výrobní zakázky:";
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(185, 71);
            this.buttonStorno.TabIndex = 4;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 391);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(383, 71);
            this.panelButtons.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(185, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(198, 71);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Popis zakázky:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cb_Active);
            this.panel2.Controls.Add(this.btn_VPH);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxPopisZakazky);
            this.panel2.Controls.Add(this.textBoxId);
            this.panel2.Controls.Add(this.tB_DateProd);
            this.panel2.Controls.Add(this.tB_Typ);
            this.panel2.Controls.Add(this.textBoxCarovyKod);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(383, 462);
            this.panel2.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(95, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Typ:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Aktivní:";
            // 
            // cb_Active
            // 
            this.cb_Active.FormattingEnabled = true;
            this.cb_Active.Location = new System.Drawing.Point(129, 99);
            this.cb_Active.Name = "cb_Active";
            this.cb_Active.Size = new System.Drawing.Size(205, 21);
            this.cb_Active.TabIndex = 6;
            // 
            // btn_VPH
            // 
            this.btn_VPH.Location = new System.Drawing.Point(338, 19);
            this.btn_VPH.Name = "btn_VPH";
            this.btn_VPH.Size = new System.Drawing.Size(42, 23);
            this.btn_VPH.TabIndex = 5;
            this.btn_VPH.Text = "...";
            this.btn_VPH.UseVisualStyleBackColor = true;
            this.btn_VPH.Visible = false;
            this.btn_VPH.Click += new System.EventHandler(this.btn_VPH_Click);
            // 
            // textBoxPopisZakazky
            // 
            this.textBoxPopisZakazky.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPopisZakazky.Location = new System.Drawing.Point(129, 47);
            this.textBoxPopisZakazky.Name = "textBoxPopisZakazky";
            this.textBoxPopisZakazky.Size = new System.Drawing.Size(205, 20);
            this.textBoxPopisZakazky.TabIndex = 2;
            // 
            // textBoxId
            // 
            this.textBoxId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxId.Location = new System.Drawing.Point(129, 21);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(205, 20);
            this.textBoxId.TabIndex = 1;
            this.textBoxId.TextChanged += new System.EventHandler(this.textBoxId_TextChanged);
            // 
            // tB_Typ
            // 
            this.tB_Typ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Typ.Location = new System.Drawing.Point(129, 126);
            this.tB_Typ.Name = "tB_Typ";
            this.tB_Typ.Size = new System.Drawing.Size(205, 20);
            this.tB_Typ.TabIndex = 3;
            // 
            // textBoxCarovyKod
            // 
            this.textBoxCarovyKod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCarovyKod.Location = new System.Drawing.Point(129, 73);
            this.textBoxCarovyKod.Name = "textBoxCarovyKod";
            this.textBoxCarovyKod.Size = new System.Drawing.Size(205, 20);
            this.textBoxCarovyKod.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Čárový kód:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tB_DateProd
            // 
            this.tB_DateProd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_DateProd.Location = new System.Drawing.Point(129, 152);
            this.tB_DateProd.Name = "tB_DateProd";
            this.tB_DateProd.Size = new System.Drawing.Size(205, 20);
            this.tB_DateProd.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Oček. datum výroby:";
            // 
            // FormVPHEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 462);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "FormVPHEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormVPHEdit";
            this.Load += new System.EventHandler(this.FormVPHEdit_Load);
            this.Shown += new System.EventHandler(this.FormVPHEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVPHEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormVPHEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxPopisZakazky;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxCarovyKod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btn_VPH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_Active;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tB_Typ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tB_DateProd;
    }
}