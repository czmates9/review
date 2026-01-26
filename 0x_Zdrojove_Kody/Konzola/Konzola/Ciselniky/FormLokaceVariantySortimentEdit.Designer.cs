namespace Konzola.Ciselniky
{
    partial class FormLokaceVariantySortimentEdit
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
            this.tbITEMNMBR = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSKLID = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BTN_SKL_ID_LOCNCODE = new System.Windows.Forms.Button();
            this.btnVybratItemnmbrRucne = new System.Windows.Forms.Button();
            this.cbbTypLokace = new System.Windows.Forms.ComboBox();
            this.panelNapoveda = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNapoveda = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbLOCNCODE = new System.Windows.Forms.TextBox();
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
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID skladu:";
            // 
            // tbITEMNMBR
            // 
            this.tbITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbITEMNMBR.Location = new System.Drawing.Point(75, 21);
            this.tbITEMNMBR.Name = "tbITEMNMBR";
            this.tbITEMNMBR.Size = new System.Drawing.Size(206, 20);
            this.tbITEMNMBR.TabIndex = 1;
            this.tbITEMNMBR.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Typ lokace:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "ID materiálu:";
            // 
            // tbSKLID
            // 
            this.tbSKLID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSKLID.Location = new System.Drawing.Point(75, 47);
            this.tbSKLID.Name = "tbSKLID";
            this.tbSKLID.Size = new System.Drawing.Size(206, 20);
            this.tbSKLID.TabIndex = 3;
            this.tbSKLID.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
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
            this.panel2.Controls.Add(this.BTN_SKL_ID_LOCNCODE);
            this.panel2.Controls.Add(this.btnVybratItemnmbrRucne);
            this.panel2.Controls.Add(this.cbbTypLokace);
            this.panel2.Controls.Add(this.panelNapoveda);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbLOCNCODE);
            this.panel2.Controls.Add(this.tbSKLID);
            this.panel2.Controls.Add(this.tbITEMNMBR);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(381, 391);
            this.panel2.TabIndex = 1;
            // 
            // BTN_SKL_ID_LOCNCODE
            // 
            this.BTN_SKL_ID_LOCNCODE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_SKL_ID_LOCNCODE.Location = new System.Drawing.Point(287, 47);
            this.BTN_SKL_ID_LOCNCODE.Name = "BTN_SKL_ID_LOCNCODE";
            this.BTN_SKL_ID_LOCNCODE.Size = new System.Drawing.Size(75, 46);
            this.BTN_SKL_ID_LOCNCODE.TabIndex = 2;
            this.BTN_SKL_ID_LOCNCODE.Text = "...";
            this.BTN_SKL_ID_LOCNCODE.UseVisualStyleBackColor = true;
            this.BTN_SKL_ID_LOCNCODE.Click += new System.EventHandler(this.BTN_SKL_ID_LOCNCODE_Click);
            // 
            // btnVybratItemnmbrRucne
            // 
            this.btnVybratItemnmbrRucne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVybratItemnmbrRucne.Location = new System.Drawing.Point(287, 18);
            this.btnVybratItemnmbrRucne.Name = "btnVybratItemnmbrRucne";
            this.btnVybratItemnmbrRucne.Size = new System.Drawing.Size(75, 23);
            this.btnVybratItemnmbrRucne.TabIndex = 2;
            this.btnVybratItemnmbrRucne.Text = "Vyhledat";
            this.btnVybratItemnmbrRucne.UseVisualStyleBackColor = true;
            this.btnVybratItemnmbrRucne.Click += new System.EventHandler(this.btnVybratItemnmbrRucne_Click);
            // 
            // cbbTypLokace
            // 
            this.cbbTypLokace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTypLokace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTypLokace.FormattingEnabled = true;
            this.cbbTypLokace.Location = new System.Drawing.Point(74, 99);
            this.cbbTypLokace.Name = "cbbTypLokace";
            this.cbbTypLokace.Size = new System.Drawing.Size(287, 21);
            this.cbbTypLokace.TabIndex = 5;
            // 
            // panelNapoveda
            // 
            this.panelNapoveda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNapoveda.Controls.Add(this.groupBox1);
            this.panelNapoveda.Location = new System.Drawing.Point(3, 126);
            this.panelNapoveda.Name = "panelNapoveda";
            this.panelNapoveda.Size = new System.Drawing.Size(375, 262);
            this.panelNapoveda.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNapoveda);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 262);
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
            this.tbNapoveda.Size = new System.Drawing.Size(369, 243);
            this.tbNapoveda.TabIndex = 0;
            this.tbNapoveda.TabStop = false;
            this.tbNapoveda.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Lokace:";
            // 
            // tbLOCNCODE
            // 
            this.tbLOCNCODE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLOCNCODE.Location = new System.Drawing.Point(75, 73);
            this.tbLOCNCODE.Name = "tbLOCNCODE";
            this.tbLOCNCODE.Size = new System.Drawing.Size(206, 20);
            this.tbLOCNCODE.TabIndex = 4;
            this.tbLOCNCODE.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormLokaceVariantySortimentEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormLokaceVariantySortimentEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Varianta lokace materiálu";
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
        private System.Windows.Forms.TextBox tbITEMNMBR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSKLID;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbNapoveda;
        private System.Windows.Forms.Panel panelNapoveda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbLOCNCODE;
        private System.Windows.Forms.ComboBox cbbTypLokace;
        private System.Windows.Forms.Button btnVybratItemnmbrRucne;
        private System.Windows.Forms.Button BTN_SKL_ID_LOCNCODE;
    }
}