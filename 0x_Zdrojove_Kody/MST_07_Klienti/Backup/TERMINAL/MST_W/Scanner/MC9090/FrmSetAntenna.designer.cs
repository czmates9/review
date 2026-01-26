namespace Fask.MST_W.Scanner.MC9090
{
    partial class FrmSetAntenna
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
            this.chkClass1 = new System.Windows.Forms.CheckBox();
            this.chkClass0 = new System.Windows.Forms.CheckBox();
            this.chkGen2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblAntenna = new System.Windows.Forms.Label();
            this.lblTx = new System.Windows.Forms.Label();
            this.txtTxPower = new System.Windows.Forms.TextBox();
            this.chkConnected = new System.Windows.Forms.CheckBox();
            this.btnOk = new Fask.Graphic.GraphicButton();
            this.btnCancel = new Fask.Graphic.GraphicButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkClass1
            // 
            this.chkClass1.Checked = true;
            this.chkClass1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClass1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.chkClass1.Location = new System.Drawing.Point(14, 104);
            this.chkClass1.Name = "chkClass1";
            this.chkClass1.Size = new System.Drawing.Size(90, 20);
            this.chkClass1.TabIndex = 0;
            this.chkClass1.Text = "CLASS1";
            // 
            // chkClass0
            // 
            this.chkClass0.Checked = true;
            this.chkClass0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClass0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.chkClass0.Location = new System.Drawing.Point(14, 130);
            this.chkClass0.Name = "chkClass0";
            this.chkClass0.Size = new System.Drawing.Size(90, 20);
            this.chkClass0.TabIndex = 1;
            this.chkClass0.Text = "CLASS0";
            // 
            // chkGen2
            // 
            this.chkGen2.Checked = true;
            this.chkGen2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGen2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.chkGen2.Location = new System.Drawing.Point(104, 104);
            this.chkGen2.Name = "chkGen2";
            this.chkGen2.Size = new System.Drawing.Size(113, 20);
            this.chkGen2.TabIndex = 2;
            this.chkGen2.Text = "CLASS1GEN2";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(4, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 32);
            this.label1.Text = "Tag Types:\r\nsetting valid for inventory only";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.comboBox1.Location = new System.Drawing.Point(104, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblAntenna
            // 
            this.lblAntenna.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblAntenna.Location = new System.Drawing.Point(4, 23);
            this.lblAntenna.Name = "lblAntenna";
            this.lblAntenna.Size = new System.Drawing.Size(100, 20);
            this.lblAntenna.Text = "Antenna Name:";
            // 
            // lblTx
            // 
            this.lblTx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblTx.Location = new System.Drawing.Point(7, 172);
            this.lblTx.Name = "lblTx";
            this.lblTx.Size = new System.Drawing.Size(82, 20);
            this.lblTx.Text = "Tx Power:";
            // 
            // txtTxPower
            // 
            this.txtTxPower.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtTxPower.Location = new System.Drawing.Point(104, 172);
            this.txtTxPower.MaxLength = 3;
            this.txtTxPower.Name = "txtTxPower";
            this.txtTxPower.Size = new System.Drawing.Size(91, 21);
            this.txtTxPower.TabIndex = 10;
            this.txtTxPower.Text = "25";
            // 
            // chkConnected
            // 
            this.chkConnected.Checked = true;
            this.chkConnected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConnected.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.chkConnected.Location = new System.Drawing.Point(7, 213);
            this.chkConnected.Name = "chkConnected";
            this.chkConnected.Size = new System.Drawing.Size(100, 20);
            this.chkConnected.TabIndex = 12;
            this.chkConnected.Text = "Connect";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(60, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(55, 20);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(125, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 20);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 1F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(6, 53);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(223, 1);
            this.textBox1.TabIndex = 18;
            this.textBox1.Text = "textBox1";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 1F, System.Drawing.FontStyle.Regular);
            this.textBox2.Location = new System.Drawing.Point(6, 160);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(223, 1);
            this.textBox2.TabIndex = 19;
            this.textBox2.Text = "textBox2";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Tahoma", 1F, System.Drawing.FontStyle.Regular);
            this.textBox3.Location = new System.Drawing.Point(6, 205);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(223, 1);
            this.textBox3.TabIndex = 20;
            this.textBox3.Text = "textBox3";
            // 
            // lblUnit
            // 
            this.lblUnit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblUnit.Location = new System.Drawing.Point(198, 174);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(21, 18);
            this.lblUnit.Text = "DB";
            // 
            // FrmSetAntenna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(234, 280);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkConnected);
            this.Controls.Add(this.txtTxPower);
            this.Controls.Add(this.lblTx);
            this.Controls.Add(this.lblAntenna);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkGen2);
            this.Controls.Add(this.chkClass0);
            this.Controls.Add(this.chkClass1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetAntenna";
            this.Text = "Antenna Settings";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmSetAntenna_Closing);
            this.Load += new System.EventHandler(this.FrmSetAntenna_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkClass1;
        private System.Windows.Forms.CheckBox chkClass0;
        private System.Windows.Forms.CheckBox chkGen2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblAntenna;
        private System.Windows.Forms.Label lblTx;
        private System.Windows.Forms.TextBox txtTxPower;
        private System.Windows.Forms.CheckBox chkConnected;
        private Fask.Graphic.GraphicButton btnOk;
        private Fask.Graphic.GraphicButton btnCancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label lblUnit;
    }
}