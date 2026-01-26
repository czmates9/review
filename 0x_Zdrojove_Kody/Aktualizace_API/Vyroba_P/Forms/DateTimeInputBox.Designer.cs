namespace Fask.Vyroba_W.Forms
{
    partial class DateTimeInputBox
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(125, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 44);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStorno.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(125, 44);
            this.buttonStorno.TabIndex = 1;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelText.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labelText.Location = new System.Drawing.Point(4, 4);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(218, 20);
            this.labelText.Text = "Datum";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.CustomFormat = "";
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(4, 27);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(218, 40);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker2.CustomFormat = "HH:mm";
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(4, 99);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(218, 40);
            this.dateTimePicker2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonStorno);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 189);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 44);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 20);
            this.label1.Text = "Èas";
            // 
            // DateTimeInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(225, 233);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.labelText);
            this.KeyPreview = true;
            this.Name = "DateTimeInputBox";
            this.Text = "Vstup";
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}