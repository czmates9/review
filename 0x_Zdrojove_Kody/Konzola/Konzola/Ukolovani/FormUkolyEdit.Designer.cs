namespace Konzola.Ukolovani
{
    partial class FormUkolyEdit
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.dateTimePickerDateFrom = new System.Windows.Forms.DateTimePicker();
            this.comboBoxPriority = new System.Windows.Forms.ComboBox();
            this.comboBoxState = new System.Windows.Forms.ComboBox();
            this.dateTimePickerDateTo = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.label1.Location = new System.Drawing.Point(91, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Název:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 391);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(358, 71);
            this.panelButtons.TabIndex = 2;
            this.panelButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelButtons_Paint);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(238, 71);
            this.buttonOK.TabIndex = 9;
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
            this.buttonStorno.TabIndex = 8;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxCode);
            this.panel2.Controls.Add(this.textBoxDescription);
            this.panel2.Controls.Add(this.textBoxName);
            this.panel2.Controls.Add(this.dateTimePickerDateFrom);
            this.panel2.Controls.Add(this.comboBoxPriority);
            this.panel2.Controls.Add(this.comboBoxState);
            this.panel2.Controls.Add(this.dateTimePickerDateTo);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(358, 391);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // textBoxCode
            // 
            this.textBoxCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCode.Location = new System.Drawing.Point(138, 73);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(191, 20);
            this.textBoxCode.TabIndex = 3;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(138, 47);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(191, 20);
            this.textBoxDescription.TabIndex = 2;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(138, 21);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(191, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // dateTimePickerDateFrom
            // 
            this.dateTimePickerDateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerDateFrom.Checked = false;
            this.dateTimePickerDateFrom.CustomFormat = "HH:mm:ss dd.MM.yyyy";
            this.dateTimePickerDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateFrom.Location = new System.Drawing.Point(137, 99);
            this.dateTimePickerDateFrom.Name = "dateTimePickerDateFrom";
            this.dateTimePickerDateFrom.ShowCheckBox = true;
            this.dateTimePickerDateFrom.Size = new System.Drawing.Size(192, 20);
            this.dateTimePickerDateFrom.TabIndex = 4;
            // 
            // comboBoxPriority
            // 
            this.comboBoxPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPriority.FormattingEnabled = true;
            this.comboBoxPriority.Location = new System.Drawing.Point(137, 177);
            this.comboBoxPriority.Name = "comboBoxPriority";
            this.comboBoxPriority.Size = new System.Drawing.Size(192, 21);
            this.comboBoxPriority.TabIndex = 7;
            // 
            // comboBoxState
            // 
            this.comboBoxState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxState.FormattingEnabled = true;
            this.comboBoxState.Location = new System.Drawing.Point(138, 150);
            this.comboBoxState.Name = "comboBoxState";
            this.comboBoxState.Size = new System.Drawing.Size(191, 21);
            this.comboBoxState.TabIndex = 6;
            // 
            // dateTimePickerDateTo
            // 
            this.dateTimePickerDateTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerDateTo.Checked = false;
            this.dateTimePickerDateTo.CustomFormat = "HH:mm:ss dd.MM.yyyy";
            this.dateTimePickerDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateTo.Location = new System.Drawing.Point(138, 124);
            this.dateTimePickerDateTo.Name = "dateTimePickerDateTo";
            this.dateTimePickerDateTo.ShowCheckBox = true;
            this.dateTimePickerDateTo.Size = new System.Drawing.Size(192, 20);
            this.dateTimePickerDateTo.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Priorita úkolu:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Stav úkolu:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Datum platnosti do:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Datum platnosti od:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kód úkolu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Popis:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormUkolyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormUkolyEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nový úkol";
            this.Load += new System.EventHandler(this.FormUzivateleEdit_Load);
            this.Shown += new System.EventHandler(this.FormUzivateleEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateTo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxPriority;
        private System.Windows.Forms.ComboBox comboBoxState;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateFrom;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxCode;
    }
}