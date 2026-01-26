namespace Konzola.Forms
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelDatabaze = new System.Windows.Forms.Label();
            this.labelProvider = new System.Windows.Forms.Label();
            this.labelExpirace = new System.Windows.Forms.Label();
            this.labelZakaznik = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.labelVerze = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.67626F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.32374F));
            this.tableLayoutPanel.Controls.Add(this.labelDatabaze, 0, 8);
            this.tableLayoutPanel.Controls.Add(this.labelProvider, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.labelExpirace, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.labelZakaznik, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.labelVerze, 1, 9);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 10;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.69591F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(561, 193);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelDatabaze
            // 
            this.labelDatabaze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDatabaze.Location = new System.Drawing.Point(312, 151);
            this.labelDatabaze.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelDatabaze.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelDatabaze.Name = "labelDatabaze";
            this.labelDatabaze.Size = new System.Drawing.Size(246, 17);
            this.labelDatabaze.TabIndex = 27;
            this.labelDatabaze.Text = "Databáze:";
            this.labelDatabaze.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProvider
            // 
            this.labelProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProvider.Location = new System.Drawing.Point(312, 133);
            this.labelProvider.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelProvider.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProvider.Name = "labelProvider";
            this.labelProvider.Size = new System.Drawing.Size(246, 17);
            this.labelProvider.TabIndex = 26;
            this.labelProvider.Text = "Provider:";
            this.labelProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelExpirace
            // 
            this.labelExpirace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelExpirace.Location = new System.Drawing.Point(312, 114);
            this.labelExpirace.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelExpirace.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelExpirace.Name = "labelExpirace";
            this.labelExpirace.Size = new System.Drawing.Size(246, 17);
            this.labelExpirace.TabIndex = 25;
            this.labelExpirace.Text = "Licence:";
            this.labelExpirace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelZakaznik
            // 
            this.labelZakaznik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelZakaznik.Location = new System.Drawing.Point(312, 95);
            this.labelZakaznik.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelZakaznik.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelZakaznik.Name = "labelZakaznik";
            this.labelZakaznik.Size = new System.Drawing.Size(246, 17);
            this.labelZakaznik.TabIndex = 24;
            this.labelZakaznik.Text = "Zákazník:";
            this.labelZakaznik.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = global::Konzola.Properties.Resources.logo_FASK3;
            this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 9);
            this.logoPictureBox.Size = new System.Drawing.Size(300, 165);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 23;
            this.logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelProductName.Location = new System.Drawing.Point(312, 0);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(246, 17);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVersion.Location = new System.Drawing.Point(312, 19);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(246, 17);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Location = new System.Drawing.Point(312, 38);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(246, 17);
            this.labelCopyright.TabIndex = 21;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCompanyName.Location = new System.Drawing.Point(312, 57);
            this.labelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(246, 17);
            this.labelCompanyName.TabIndex = 22;
            this.labelCompanyName.Text = "Company Name";
            this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVerze
            // 
            this.labelVerze.AutoSize = true;
            this.labelVerze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVerze.Location = new System.Drawing.Point(309, 171);
            this.labelVerze.Name = "labelVerze";
            this.labelVerze.Size = new System.Drawing.Size(249, 22);
            this.labelVerze.TabIndex = 28;
            this.labelVerze.Text = "Verze:";
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 211);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutBox_KeyDown);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelExpirace;
        private System.Windows.Forms.Label labelZakaznik;
        private System.Windows.Forms.Label labelDatabaze;
        private System.Windows.Forms.Label labelProvider;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.Label labelVerze;
    }
}
