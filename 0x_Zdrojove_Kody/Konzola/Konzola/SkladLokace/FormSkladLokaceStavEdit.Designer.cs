namespace Konzola.SkladLokace
{
    partial class FormSkladLokaceStavEdit
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
            this.tbSerltnum = new System.Windows.Forms.TextBox();
            this.tbLocncode = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSkladLokace_CIL_Vyhledat = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbLocncodeCIL = new System.Windows.Forms.TextBox();
            this.tbSklIDCIL = new System.Windows.Forms.TextBox();
            this.btnPracovnikVyhledat = new System.Windows.Forms.Button();
            this.btn_RFID_Load = new System.Windows.Forms.Button();
            this.btnUserVyhledat = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbQTY_owner = new System.Windows.Forms.TextBox();
            this.tbPracovnikID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbUserID = new System.Windows.Forms.TextBox();
            this.btnSkladLokaceVyhledat = new System.Windows.Forms.Button();
            this.btnItemnmbrVyhledat = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPohybType = new System.Windows.Forms.ComboBox();
            this.panelNapoveda = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNapoveda = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbQtyshppd = new System.Windows.Forms.TextBox();
            this.tbItemnmbr = new System.Windows.Forms.TextBox();
            this.tbITEMDESC = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDocumentNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.label1.Location = new System.Drawing.Point(9, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID lokace:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbSklID
            // 
            this.tbSklID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSklID.Location = new System.Drawing.Point(115, 160);
            this.tbSklID.Name = "tbSklID";
            this.tbSklID.Size = new System.Drawing.Size(245, 20);
            this.tbSklID.TabIndex = 5;
            this.tbSklID.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Šarže / SN:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "ID skladu:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbSerltnum
            // 
            this.tbSerltnum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSerltnum.Location = new System.Drawing.Point(115, 280);
            this.tbSerltnum.Name = "tbSerltnum";
            this.tbSerltnum.Size = new System.Drawing.Size(245, 20);
            this.tbSerltnum.TabIndex = 8;
            this.tbSerltnum.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbLocncode
            // 
            this.tbLocncode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocncode.Location = new System.Drawing.Point(115, 190);
            this.tbLocncode.Name = "tbLocncode";
            this.tbLocncode.Size = new System.Drawing.Size(245, 20);
            this.tbLocncode.TabIndex = 7;
            this.tbLocncode.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 535);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(477, 71);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(169, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(308, 71);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(169, 71);
            this.buttonStorno.TabIndex = 1;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSkladLokace_CIL_Vyhledat);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.tbLocncodeCIL);
            this.panel2.Controls.Add(this.tbSklIDCIL);
            this.panel2.Controls.Add(this.btnPracovnikVyhledat);
            this.panel2.Controls.Add(this.btn_RFID_Load);
            this.panel2.Controls.Add(this.btnUserVyhledat);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.tbQTY_owner);
            this.panel2.Controls.Add(this.tbPracovnikID);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbUserID);
            this.panel2.Controls.Add(this.btnSkladLokaceVyhledat);
            this.panel2.Controls.Add(this.btnItemnmbrVyhledat);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbPohybType);
            this.panel2.Controls.Add(this.panelNapoveda);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbQtyshppd);
            this.panel2.Controls.Add(this.tbLocncode);
            this.panel2.Controls.Add(this.tbItemnmbr);
            this.panel2.Controls.Add(this.tbSklID);
            this.panel2.Controls.Add(this.tbITEMDESC);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.tbDocumentNumber);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.tbSerltnum);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(477, 535);
            this.panel2.TabIndex = 1;
            // 
            // btnSkladLokace_CIL_Vyhledat
            // 
            this.btnSkladLokace_CIL_Vyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSkladLokace_CIL_Vyhledat.Location = new System.Drawing.Point(380, 220);
            this.btnSkladLokace_CIL_Vyhledat.Name = "btnSkladLokace_CIL_Vyhledat";
            this.btnSkladLokace_CIL_Vyhledat.Size = new System.Drawing.Size(75, 50);
            this.btnSkladLokace_CIL_Vyhledat.TabIndex = 19;
            this.btnSkladLokace_CIL_Vyhledat.Text = "Vyhledat";
            this.btnSkladLokace_CIL_Vyhledat.UseVisualStyleBackColor = true;
            this.btnSkladLokace_CIL_Vyhledat.Click += new System.EventHandler(this.btnSkladLokace_CIL_Vyhledat_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(9, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "ID skladu Cíl:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(9, 254);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "ID lokace Cíl:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbLocncodeCIL
            // 
            this.tbLocncodeCIL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocncodeCIL.Location = new System.Drawing.Point(115, 250);
            this.tbLocncodeCIL.Name = "tbLocncodeCIL";
            this.tbLocncodeCIL.Size = new System.Drawing.Size(245, 20);
            this.tbLocncodeCIL.TabIndex = 20;
            // 
            // tbSklIDCIL
            // 
            this.tbSklIDCIL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSklIDCIL.Location = new System.Drawing.Point(115, 220);
            this.tbSklIDCIL.Name = "tbSklIDCIL";
            this.tbSklIDCIL.Size = new System.Drawing.Size(245, 20);
            this.tbSklIDCIL.TabIndex = 18;
            // 
            // btnPracovnikVyhledat
            // 
            this.btnPracovnikVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPracovnikVyhledat.Enabled = false;
            this.btnPracovnikVyhledat.Location = new System.Drawing.Point(380, 340);
            this.btnPracovnikVyhledat.Name = "btnPracovnikVyhledat";
            this.btnPracovnikVyhledat.Size = new System.Drawing.Size(75, 23);
            this.btnPracovnikVyhledat.TabIndex = 13;
            this.btnPracovnikVyhledat.Text = "vyhledat";
            this.btnPracovnikVyhledat.UseVisualStyleBackColor = true;
            this.btnPracovnikVyhledat.Click += new System.EventHandler(this.btnPracovnikVyhledat_Click);
            // 
            // btn_RFID_Load
            // 
            this.btn_RFID_Load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_RFID_Load.Location = new System.Drawing.Point(380, 277);
            this.btn_RFID_Load.Name = "btn_RFID_Load";
            this.btn_RFID_Load.Size = new System.Drawing.Size(75, 23);
            this.btn_RFID_Load.TabIndex = 9;
            this.btn_RFID_Load.Text = "načíst";
            this.btn_RFID_Load.UseVisualStyleBackColor = true;
            this.btn_RFID_Load.Click += new System.EventHandler(this.btn_RFID_Load_Click);
            // 
            // btnUserVyhledat
            // 
            this.btnUserVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUserVyhledat.Enabled = false;
            this.btnUserVyhledat.Location = new System.Drawing.Point(380, 310);
            this.btnUserVyhledat.Name = "btnUserVyhledat";
            this.btnUserVyhledat.Size = new System.Drawing.Size(75, 23);
            this.btnUserVyhledat.TabIndex = 11;
            this.btnUserVyhledat.Text = "vyhledat";
            this.btnUserVyhledat.UseVisualStyleBackColor = true;
            this.btnUserVyhledat.Click += new System.EventHandler(this.btnUserVyhledat_Click);
            // 
            // label11
            // 
            this.label11.Enabled = false;
            this.label11.Location = new System.Drawing.Point(9, 374);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Počet:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(9, 344);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Pracovnik:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbQTY_owner
            // 
            this.tbQTY_owner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQTY_owner.Enabled = false;
            this.tbQTY_owner.Location = new System.Drawing.Point(115, 370);
            this.tbQTY_owner.Name = "tbQTY_owner";
            this.tbQTY_owner.Size = new System.Drawing.Size(340, 20);
            this.tbQTY_owner.TabIndex = 14;
            this.tbQTY_owner.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbPracovnikID
            // 
            this.tbPracovnikID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPracovnikID.Enabled = false;
            this.tbPracovnikID.Location = new System.Drawing.Point(115, 340);
            this.tbPracovnikID.Name = "tbPracovnikID";
            this.tbPracovnikID.Size = new System.Drawing.Size(245, 20);
            this.tbPracovnikID.TabIndex = 12;
            this.tbPracovnikID.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 314);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Uživatel:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbUserID
            // 
            this.tbUserID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserID.Location = new System.Drawing.Point(115, 310);
            this.tbUserID.Name = "tbUserID";
            this.tbUserID.Size = new System.Drawing.Size(245, 20);
            this.tbUserID.TabIndex = 10;
            this.tbUserID.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // btnSkladLokaceVyhledat
            // 
            this.btnSkladLokaceVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSkladLokaceVyhledat.Location = new System.Drawing.Point(380, 160);
            this.btnSkladLokaceVyhledat.Name = "btnSkladLokaceVyhledat";
            this.btnSkladLokaceVyhledat.Size = new System.Drawing.Size(75, 50);
            this.btnSkladLokaceVyhledat.TabIndex = 6;
            this.btnSkladLokaceVyhledat.Text = "Vyhledat";
            this.btnSkladLokaceVyhledat.UseVisualStyleBackColor = true;
            this.btnSkladLokaceVyhledat.Click += new System.EventHandler(this.btnSkladLokaceVyhledat_Click);
            // 
            // btnItemnmbrVyhledat
            // 
            this.btnItemnmbrVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemnmbrVyhledat.Location = new System.Drawing.Point(380, 69);
            this.btnItemnmbrVyhledat.Name = "btnItemnmbrVyhledat";
            this.btnItemnmbrVyhledat.Size = new System.Drawing.Size(75, 23);
            this.btnItemnmbrVyhledat.TabIndex = 3;
            this.btnItemnmbrVyhledat.Text = "vyhledat";
            this.btnItemnmbrVyhledat.UseVisualStyleBackColor = true;
            this.btnItemnmbrVyhledat.Click += new System.EventHandler(this.btnItemnmbrVyhledat_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Typ pohybu:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbPohybType
            // 
            this.cbPohybType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPohybType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPohybType.FormattingEnabled = true;
            this.cbPohybType.Location = new System.Drawing.Point(115, 10);
            this.cbPohybType.Name = "cbPohybType";
            this.cbPohybType.Size = new System.Drawing.Size(340, 21);
            this.cbPohybType.TabIndex = 0;
            this.cbPohybType.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // panelNapoveda
            // 
            this.panelNapoveda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNapoveda.Controls.Add(this.groupBox1);
            this.panelNapoveda.Location = new System.Drawing.Point(3, 408);
            this.panelNapoveda.Name = "panelNapoveda";
            this.panelNapoveda.Size = new System.Drawing.Size(471, 123);
            this.panelNapoveda.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNapoveda);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 123);
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
            this.tbNapoveda.Size = new System.Drawing.Size(465, 104);
            this.tbNapoveda.TabIndex = 0;
            this.tbNapoveda.TabStop = false;
            this.tbNapoveda.Text = "";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "ID materiálu:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Množství:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbQtyshppd
            // 
            this.tbQtyshppd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQtyshppd.Location = new System.Drawing.Point(115, 130);
            this.tbQtyshppd.Name = "tbQtyshppd";
            this.tbQtyshppd.Size = new System.Drawing.Size(340, 20);
            this.tbQtyshppd.TabIndex = 4;
            this.tbQtyshppd.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbItemnmbr
            // 
            this.tbItemnmbr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbItemnmbr.Location = new System.Drawing.Point(115, 70);
            this.tbItemnmbr.Name = "tbItemnmbr";
            this.tbItemnmbr.Size = new System.Drawing.Size(245, 20);
            this.tbItemnmbr.TabIndex = 2;
            this.tbItemnmbr.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            this.tbItemnmbr.Leave += new System.EventHandler(this.tbItemnmbr_Leave);
            // 
            // tbITEMDESC
            // 
            this.tbITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbITEMDESC.Enabled = false;
            this.tbITEMDESC.Location = new System.Drawing.Point(115, 100);
            this.tbITEMDESC.Name = "tbITEMDESC";
            this.tbITEMDESC.ReadOnly = true;
            this.tbITEMDESC.Size = new System.Drawing.Size(340, 20);
            this.tbITEMDESC.TabIndex = 5;
            this.tbITEMDESC.TabStop = false;
            this.tbITEMDESC.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Název mat:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDocumentNumber
            // 
            this.tbDocumentNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDocumentNumber.Location = new System.Drawing.Point(115, 40);
            this.tbDocumentNumber.Name = "tbDocumentNumber";
            this.tbDocumentNumber.Size = new System.Drawing.Size(340, 20);
            this.tbDocumentNumber.TabIndex = 1;
            this.tbDocumentNumber.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Č. dokumentu:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormSkladLokaceStavEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 606);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormSkladLokaceStavEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lokace";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSkladLokaceStavEdit_FormClosed);
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
        private System.Windows.Forms.TextBox tbSerltnum;
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
        private System.Windows.Forms.TextBox tbQtyshppd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbPohybType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbItemnmbr;
        private System.Windows.Forms.Button btnItemnmbrVyhledat;
        private System.Windows.Forms.Button btnSkladLokaceVyhledat;
        private System.Windows.Forms.TextBox tbDocumentNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnUserVyhledat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbUserID;
        private System.Windows.Forms.TextBox tbITEMDESC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPracovnikVyhledat;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbPracovnikID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbQTY_owner;
        private System.Windows.Forms.Button btn_RFID_Load;
        private System.Windows.Forms.Button btnSkladLokace_CIL_Vyhledat;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbLocncodeCIL;
        private System.Windows.Forms.TextBox tbSklIDCIL;
    }
}