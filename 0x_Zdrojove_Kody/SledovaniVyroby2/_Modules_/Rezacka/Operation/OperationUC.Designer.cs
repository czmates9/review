namespace FASK.SledovaniVyroby.Module.Rezacka
{
    partial class OperationUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbOperation = new System.Windows.Forms.GroupBox();
            this.dgOperationInfo = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.txtScan3 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtOperationCode = new System.Windows.Forms.TextBox();
            this.txtScan2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnReadParams = new System.Windows.Forms.Button();
            this.txtScan1 = new System.Windows.Forms.TextBox();
            this.txtSensorValue = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOperationName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNextOperations = new System.Windows.Forms.TextBox();
            this.txtOperationBarcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgOperationHeader = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumberBM = new System.Windows.Forms.TextBox();
            this.txtMaterial = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtItemAbbr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperationInfo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperationHeader)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbOperation
            // 
            this.grbOperation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grbOperation.Controls.Add(this.dgOperationInfo);
            this.grbOperation.Controls.Add(this.panel1);
            this.grbOperation.Controls.Add(this.dgOperationHeader);
            this.grbOperation.Controls.Add(this.panel2);
            this.grbOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grbOperation.Location = new System.Drawing.Point(0, 0);
            this.grbOperation.Name = "grbOperation";
            this.grbOperation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grbOperation.Size = new System.Drawing.Size(431, 506);
            this.grbOperation.TabIndex = 68;
            this.grbOperation.TabStop = false;
            // 
            // dgOperationInfo
            // 
            this.dgOperationInfo.AllowUserToAddRows = false;
            this.dgOperationInfo.AllowUserToDeleteRows = false;
            this.dgOperationInfo.AllowUserToResizeRows = false;
            this.dgOperationInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgOperationInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOperationInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgOperationInfo.Location = new System.Drawing.Point(3, 449);
            this.dgOperationInfo.MultiSelect = false;
            this.dgOperationInfo.Name = "dgOperationInfo";
            this.dgOperationInfo.ReadOnly = true;
            this.dgOperationInfo.RowHeadersVisible = false;
            this.dgOperationInfo.RowHeadersWidth = 30;
            this.dgOperationInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgOperationInfo.Size = new System.Drawing.Size(425, 45);
            this.dgOperationInfo.TabIndex = 100;
            this.dgOperationInfo.Visible = false;
            this.dgOperationInfo.SelectionChanged += new System.EventHandler(this.dgOperationInfo_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.txtScan3);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.txtOperationCode);
            this.panel1.Controls.Add(this.txtScan2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.btnReadParams);
            this.panel1.Controls.Add(this.txtScan1);
            this.panel1.Controls.Add(this.txtSensorValue);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtOperationName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNextOperations);
            this.panel1.Controls.Add(this.txtOperationBarcode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 185);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 264);
            this.panel1.TabIndex = 90;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label22.Location = new System.Drawing.Point(18, 201);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(116, 20);
            this.label22.TabIndex = 103;
            this.label22.Text = "Scanování 3 : ";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtScan3
            // 
            this.txtScan3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScan3.Enabled = false;
            this.txtScan3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtScan3.Location = new System.Drawing.Point(141, 201);
            this.txtScan3.MaxLength = 37;
            this.txtScan3.Name = "txtScan3";
            this.txtScan3.ReadOnly = true;
            this.txtScan3.Size = new System.Drawing.Size(280, 20);
            this.txtScan3.TabIndex = 80;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label21.Location = new System.Drawing.Point(18, 175);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(116, 20);
            this.label21.TabIndex = 101;
            this.label21.Text = "Scanování 2 : ";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOperationCode
            // 
            this.txtOperationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperationCode.Enabled = false;
            this.txtOperationCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOperationCode.Location = new System.Drawing.Point(142, 71);
            this.txtOperationCode.MaxLength = 37;
            this.txtOperationCode.Name = "txtOperationCode";
            this.txtOperationCode.ReadOnly = true;
            this.txtOperationCode.Size = new System.Drawing.Size(280, 20);
            this.txtOperationCode.TabIndex = 30;
            // 
            // txtScan2
            // 
            this.txtScan2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScan2.Enabled = false;
            this.txtScan2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtScan2.Location = new System.Drawing.Point(141, 175);
            this.txtScan2.MaxLength = 37;
            this.txtScan2.Name = "txtScan2";
            this.txtScan2.ReadOnly = true;
            this.txtScan2.Size = new System.Drawing.Size(280, 20);
            this.txtScan2.TabIndex = 70;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(15, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 20);
            this.label7.TabIndex = 97;
            this.label7.Text = "Operace se senzorem : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label20.Location = new System.Drawing.Point(18, 149);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(116, 20);
            this.label20.TabIndex = 98;
            this.label20.Text = "Scanování 1 : ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnReadParams
            // 
            this.btnReadParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnReadParams.Location = new System.Drawing.Point(142, 30);
            this.btnReadParams.Name = "btnReadParams";
            this.btnReadParams.Size = new System.Drawing.Size(280, 35);
            this.btnReadParams.TabIndex = 20;
            this.btnReadParams.Text = "Ruční načtení operace";
            this.btnReadParams.UseVisualStyleBackColor = true;
            // 
            // txtScan1
            // 
            this.txtScan1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScan1.Enabled = false;
            this.txtScan1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtScan1.Location = new System.Drawing.Point(141, 149);
            this.txtScan1.MaxLength = 37;
            this.txtScan1.Name = "txtScan1";
            this.txtScan1.ReadOnly = true;
            this.txtScan1.Size = new System.Drawing.Size(280, 20);
            this.txtScan1.TabIndex = 60;
            // 
            // txtSensorValue
            // 
            this.txtSensorValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSensorValue.Enabled = false;
            this.txtSensorValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSensorValue.Location = new System.Drawing.Point(142, 227);
            this.txtSensorValue.MaxLength = 37;
            this.txtSensorValue.Name = "txtSensorValue";
            this.txtSensorValue.ReadOnly = true;
            this.txtSensorValue.Size = new System.Drawing.Size(280, 20);
            this.txtSensorValue.TabIndex = 90;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(15, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 88;
            this.label10.Text = "Kód operace : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOperationName
            // 
            this.txtOperationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperationName.Enabled = false;
            this.txtOperationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOperationName.Location = new System.Drawing.Point(142, 97);
            this.txtOperationName.MaxLength = 37;
            this.txtOperationName.Name = "txtOperationName";
            this.txtOperationName.ReadOnly = true;
            this.txtOperationName.Size = new System.Drawing.Size(280, 20);
            this.txtOperationName.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(15, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 95;
            this.label5.Text = "Následující operace : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(15, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 91;
            this.label2.Text = "Název operace : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNextOperations
            // 
            this.txtNextOperations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNextOperations.Enabled = false;
            this.txtNextOperations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNextOperations.Location = new System.Drawing.Point(142, 123);
            this.txtNextOperations.MaxLength = 37;
            this.txtNextOperations.Name = "txtNextOperations";
            this.txtNextOperations.ReadOnly = true;
            this.txtNextOperations.Size = new System.Drawing.Size(280, 20);
            this.txtNextOperations.TabIndex = 50;
            // 
            // txtOperationBarcode
            // 
            this.txtOperationBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperationBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOperationBarcode.Location = new System.Drawing.Point(142, 6);
            this.txtOperationBarcode.MaxLength = 37;
            this.txtOperationBarcode.Name = "txtOperationBarcode";
            this.txtOperationBarcode.Size = new System.Drawing.Size(280, 20);
            this.txtOperationBarcode.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(15, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 92;
            this.label3.Text = "Kód operace: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgOperationHeader
            // 
            this.dgOperationHeader.AllowUserToAddRows = false;
            this.dgOperationHeader.AllowUserToDeleteRows = false;
            this.dgOperationHeader.AllowUserToResizeRows = false;
            this.dgOperationHeader.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgOperationHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOperationHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgOperationHeader.Location = new System.Drawing.Point(3, 140);
            this.dgOperationHeader.MultiSelect = false;
            this.dgOperationHeader.Name = "dgOperationHeader";
            this.dgOperationHeader.ReadOnly = true;
            this.dgOperationHeader.RowHeadersVisible = false;
            this.dgOperationHeader.RowHeadersWidth = 30;
            this.dgOperationHeader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgOperationHeader.Size = new System.Drawing.Size(425, 45);
            this.dgOperationHeader.TabIndex = 5;
            this.dgOperationHeader.Visible = false;
            this.dgOperationHeader.SelectionChanged += new System.EventHandler(this.dgOperationHeader_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtNumberBM);
            this.panel2.Controls.Add(this.txtMaterial);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtItemAbbr);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtOrderNumber);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(425, 123);
            this.panel2.TabIndex = 107;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(15, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 20);
            this.label8.TabIndex = 114;
            this.label8.Text = "Počet běžných metrů: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumberBM
            // 
            this.txtNumberBM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumberBM.Enabled = false;
            this.txtNumberBM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNumberBM.Location = new System.Drawing.Point(141, 92);
            this.txtNumberBM.Name = "txtNumberBM";
            this.txtNumberBM.ReadOnly = true;
            this.txtNumberBM.Size = new System.Drawing.Size(280, 20);
            this.txtNumberBM.TabIndex = 113;
            // 
            // txtMaterial
            // 
            this.txtMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaterial.Enabled = false;
            this.txtMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMaterial.Location = new System.Drawing.Point(141, 40);
            this.txtMaterial.MaxLength = 37;
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.ReadOnly = true;
            this.txtMaterial.Size = new System.Drawing.Size(280, 20);
            this.txtMaterial.TabIndex = 111;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(18, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 20);
            this.label6.TabIndex = 112;
            this.label6.Text = "Materiál : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemAbbr
            // 
            this.txtItemAbbr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemAbbr.Enabled = false;
            this.txtItemAbbr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtItemAbbr.Location = new System.Drawing.Point(142, 66);
            this.txtItemAbbr.MaxLength = 37;
            this.txtItemAbbr.Name = "txtItemAbbr";
            this.txtItemAbbr.ReadOnly = true;
            this.txtItemAbbr.Size = new System.Drawing.Size(279, 20);
            this.txtItemAbbr.TabIndex = 109;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(18, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 20);
            this.label4.TabIndex = 110;
            this.label4.Text = "Zkratka položky : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNumber.Enabled = false;
            this.txtOrderNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOrderNumber.Location = new System.Drawing.Point(141, 14);
            this.txtOrderNumber.MaxLength = 37;
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.ReadOnly = true;
            this.txtOrderNumber.Size = new System.Drawing.Size(280, 20);
            this.txtOrderNumber.TabIndex = 107;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 108;
            this.label1.Text = "Číslo zakázky : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OperationUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.grbOperation);
            this.Name = "OperationUC";
            this.Size = new System.Drawing.Size(431, 506);
            this.grbOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOperationInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperationHeader)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbOperation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtScan3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtOperationCode;
        private System.Windows.Forms.TextBox txtScan2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Button btnReadParams;
        private System.Windows.Forms.TextBox txtScan1;
        private System.Windows.Forms.TextBox txtSensorValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOperationName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNextOperations;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtOperationBarcode;
        private System.Windows.Forms.DataGridView dgOperationInfo;
        private System.Windows.Forms.DataGridView dgOperationHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtMaterial;
        public System.Windows.Forms.TextBox txtItemAbbr;
        public System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtNumberBM;
    }
}
