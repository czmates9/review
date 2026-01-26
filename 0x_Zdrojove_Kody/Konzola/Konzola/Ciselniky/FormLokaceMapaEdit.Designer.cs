namespace Konzola.Ciselniky
{
    partial class FormLokaceMapaEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbSklID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbType = new System.Windows.Forms.TextBox();
            this.tbLocncode = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Typ = new System.Windows.Forms.Button();
            this.btn_SKL_ID = new System.Windows.Forms.Button();
            this.panelNapoveda = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNapoveda = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbBarcode = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelNapoveda.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID lokace:";
            // 
            // tbSklID
            // 
            this.tbSklID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSklID.Location = new System.Drawing.Point(87, 21);
            this.tbSklID.Name = "tbSklID";
            this.tbSklID.Size = new System.Drawing.Size(228, 20);
            this.tbSklID.TabIndex = 0;
            this.tbSklID.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Typ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "ID skladu:";
            // 
            // tbType
            // 
            this.tbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbType.Location = new System.Drawing.Point(87, 125);
            this.tbType.Name = "tbType";
            this.tbType.Size = new System.Drawing.Size(228, 20);
            this.tbType.TabIndex = 5;
            this.tbType.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbLocncode
            // 
            this.tbLocncode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocncode.Location = new System.Drawing.Point(87, 47);
            this.tbLocncode.Name = "tbLocncode";
            this.tbLocncode.Size = new System.Drawing.Size(275, 20);
            this.tbLocncode.TabIndex = 2;
            this.tbLocncode.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 391);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(381, 71);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(261, 71);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 7;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Typ);
            this.panel2.Controls.Add(this.btn_SKL_ID);
            this.panel2.Controls.Add(this.panelNapoveda);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbDescription);
            this.panel2.Controls.Add(this.tbBarcode);
            this.panel2.Controls.Add(this.tbLocncode);
            this.panel2.Controls.Add(this.tbSklID);
            this.panel2.Controls.Add(this.tbType);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(381, 391);
            this.panel2.TabIndex = 1;
            // 
            // btn_Typ
            // 
            this.btn_Typ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Typ.Location = new System.Drawing.Point(322, 123);
            this.btn_Typ.Name = "btn_Typ";
            this.btn_Typ.Size = new System.Drawing.Size(40, 23);
            this.btn_Typ.TabIndex = 6;
            this.btn_Typ.Text = "...";
            this.btn_Typ.UseVisualStyleBackColor = true;
            this.btn_Typ.Click += new System.EventHandler(this.btn_Typ_Click);
            // 
            // btn_SKL_ID
            // 
            this.btn_SKL_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SKL_ID.Location = new System.Drawing.Point(322, 19);
            this.btn_SKL_ID.Name = "btn_SKL_ID";
            this.btn_SKL_ID.Size = new System.Drawing.Size(40, 23);
            this.btn_SKL_ID.TabIndex = 1;
            this.btn_SKL_ID.Text = "...";
            this.btn_SKL_ID.UseVisualStyleBackColor = true;
            this.btn_SKL_ID.Click += new System.EventHandler(this.btn_SKL_ID_Click);
            // 
            // panelNapoveda
            // 
            this.panelNapoveda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNapoveda.Controls.Add(this.groupBox1);
            this.panelNapoveda.Location = new System.Drawing.Point(3, 168);
            this.panelNapoveda.Name = "panelNapoveda";
            this.panelNapoveda.Size = new System.Drawing.Size(375, 220);
            this.panelNapoveda.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNapoveda);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 220);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nápověda";
            // 
            // tbNapoveda
            // 
            this.tbNapoveda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNapoveda.Location = new System.Drawing.Point(3, 16);
            this.tbNapoveda.Name = "tbNapoveda";
            this.tbNapoveda.ReadOnly = true;
            this.tbNapoveda.Size = new System.Drawing.Size(369, 201);
            this.tbNapoveda.TabIndex = 0;
            this.tbNapoveda.TabStop = false;
            this.tbNapoveda.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Označení:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Č. kod lokace:";
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.Location = new System.Drawing.Point(87, 99);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(275, 20);
            this.tbDescription.TabIndex = 4;
            this.tbDescription.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbBarcode
            // 
            this.tbBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBarcode.Location = new System.Drawing.Point(87, 73);
            this.tbBarcode.Name = "tbBarcode";
            this.tbBarcode.Size = new System.Drawing.Size(275, 20);
            this.tbBarcode.TabIndex = 3;
            this.tbBarcode.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormLokaceMapaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormLokaceMapaEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lokace";
            this.Load += new System.EventHandler(this.FormUzivateleEdit_Load);
            this.Shown += new System.EventHandler(this.FormUzivateleEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelNapoveda.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSklID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.TextBox tbLocncode;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbNapoveda;
        private System.Windows.Forms.Panel panelNapoveda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Button btn_SKL_ID;
        private System.Windows.Forms.Button btn_Typ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbBarcode;
    }
}