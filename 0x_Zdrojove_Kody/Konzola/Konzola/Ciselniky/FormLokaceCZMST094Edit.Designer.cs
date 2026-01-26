namespace Konzola.Ciselniky
{
    partial class FormLokaceCZMST094Edit
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
            this.tB_Desc = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tB_Barcode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tB_LOCNCODE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tB_SKL_ID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tB_TYPE = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Označení lokace:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tB_Desc
            // 
            this.tB_Desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Desc.Location = new System.Drawing.Point(120, 92);
            this.tB_Desc.Name = "tB_Desc";
            this.tB_Desc.Size = new System.Drawing.Size(188, 20);
            this.tB_Desc.TabIndex = 4;
            this.tB_Desc.TextChanged += new System.EventHandler(this.textBoxJmeno_TextChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 219);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(334, 71);
            this.panelButtons.TabIndex = 2;
            this.panelButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelButtons_Paint);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(214, 71);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
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
            this.panel2.Controls.Add(this.tB_Barcode);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tB_LOCNCODE);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tB_SKL_ID);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tB_Desc);
            this.panel2.Controls.Add(this.tB_TYPE);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 219);
            this.panel2.TabIndex = 1;
            // 
            // tB_Barcode
            // 
            this.tB_Barcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Barcode.Location = new System.Drawing.Point(120, 118);
            this.tB_Barcode.Name = "tB_Barcode";
            this.tB_Barcode.Size = new System.Drawing.Size(188, 20);
            this.tB_Barcode.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Čár. kód lokace:";
            // 
            // tB_LOCNCODE
            // 
            this.tB_LOCNCODE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_LOCNCODE.Location = new System.Drawing.Point(120, 38);
            this.tB_LOCNCODE.Name = "tB_LOCNCODE";
            this.tB_LOCNCODE.Size = new System.Drawing.Size(189, 20);
            this.tB_LOCNCODE.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Lokace:";
            // 
            // tB_SKL_ID
            // 
            this.tB_SKL_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_SKL_ID.Location = new System.Drawing.Point(120, 12);
            this.tB_SKL_ID.Name = "tB_SKL_ID";
            this.tB_SKL_ID.Size = new System.Drawing.Size(189, 20);
            this.tB_SKL_ID.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "ID skladu:";
            // 
            // tB_TYPE
            // 
            this.tB_TYPE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_TYPE.Location = new System.Drawing.Point(120, 64);
            this.tB_TYPE.Name = "tB_TYPE";
            this.tB_TYPE.Size = new System.Drawing.Size(188, 20);
            this.tB_TYPE.TabIndex = 3;
            this.tB_TYPE.TextChanged += new System.EventHandler(this.textBoxPrijmeni_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Typ lokace:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormLokaceCZMST094Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 290);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormLokaceCZMST094Edit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nová lokace";
            this.Load += new System.EventHandler(this.FormLokaceCZMST094Edit_Load);
            this.Shown += new System.EventHandler(this.FormPracovniciEdit_Shown);
            this.Resize += new System.EventHandler(this.FormLokaceCZMST094Edit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_Desc;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.TextBox tB_TYPE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox tB_SKL_ID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tB_Barcode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tB_LOCNCODE;
        private System.Windows.Forms.Label label5;
    }
}