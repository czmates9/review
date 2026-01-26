namespace FASK.SledovaniVyroby.Main.ErrorLog
{
    partial class ErrorLogDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBoxErrorLog = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxErrorLog, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 262);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // richTextBoxErrorLog
            // 
            this.richTextBoxErrorLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxErrorLog.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxErrorLog.Name = "richTextBoxErrorLog";
            this.richTextBoxErrorLog.ReadOnly = true;
            this.richTextBoxErrorLog.Size = new System.Drawing.Size(278, 216);
            this.richTextBoxErrorLog.TabIndex = 0;
            this.richTextBoxErrorLog.Text = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.buttonOK, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonClearLog, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 225);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(278, 34);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Location = new System.Drawing.Point(81, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(94, 28);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClearLog.Location = new System.Drawing.Point(181, 3);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(94, 28);
            this.buttonClearLog.TabIndex = 1;
            this.buttonClearLog.Text = "Vymaž log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // ErrorLogDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "ErrorLogDialog";
            this.Text = "Logování chyb";
            this.Load += new System.EventHandler(this.ErrorLogDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ErrorLogDialog_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBoxErrorLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonClearLog;
    }
}