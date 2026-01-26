namespace Fask.PrinterProviderWebService
{
    partial class ConfigControl
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
            this.tabControlWebService = new System.Windows.Forms.TabControl();
            this.tabPageWSConfig = new System.Windows.Forms.TabPage();
            this.panelParams = new System.Windows.Forms.Panel();
            this.checkBoxOneWayPrint = new System.Windows.Forms.CheckBox();
            this.comboBoxTemplate = new System.Windows.Forms.ComboBox();
            this.comboBoxPrinterName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTimeout = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabPageWSInfo = new System.Windows.Forms.TabPage();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.buttonWindowsIdentity = new System.Windows.Forms.Button();
            this.buttonWriteLog = new System.Windows.Forms.Button();
            this.buttonHelloWorld = new System.Windows.Forms.Button();
            this.buttonTemplates = new System.Windows.Forms.Button();
            this.buttonPrinters = new System.Windows.Forms.Button();
            this.tabControlWebService.SuspendLayout();
            this.tabPageWSConfig.SuspendLayout();
            this.panelParams.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.tabPageWSInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlWebService
            // 
            this.tabControlWebService.Controls.Add(this.tabPageWSConfig);
            this.tabControlWebService.Controls.Add(this.tabPageWSInfo);
            this.tabControlWebService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlWebService.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.tabControlWebService.Location = new System.Drawing.Point(0, 0);
            this.tabControlWebService.Name = "tabControlWebService";
            this.tabControlWebService.SelectedIndex = 0;
            this.tabControlWebService.Size = new System.Drawing.Size(240, 300);
            this.tabControlWebService.TabIndex = 0;
            // 
            // tabPageWSConfig
            // 
            this.tabPageWSConfig.AutoScroll = true;
            this.tabPageWSConfig.Controls.Add(this.panelParams);
            this.tabPageWSConfig.Controls.Add(this.panelButtons);
            this.tabPageWSConfig.Location = new System.Drawing.Point(4, 23);
            this.tabPageWSConfig.Name = "tabPageWSConfig";
            this.tabPageWSConfig.Size = new System.Drawing.Size(232, 273);
            this.tabPageWSConfig.Text = "Config";
            // 
            // panelParams
            // 
            this.panelParams.AutoScroll = true;
            this.panelParams.Controls.Add(this.checkBoxOneWayPrint);
            this.panelParams.Controls.Add(this.comboBoxTemplate);
            this.panelParams.Controls.Add(this.comboBoxPrinterName);
            this.panelParams.Controls.Add(this.label2);
            this.panelParams.Controls.Add(this.textBoxAddress);
            this.panelParams.Controls.Add(this.textBoxName);
            this.panelParams.Controls.Add(this.label3);
            this.panelParams.Controls.Add(this.label5);
            this.panelParams.Controls.Add(this.label1);
            this.panelParams.Controls.Add(this.label4);
            this.panelParams.Controls.Add(this.textBoxTimeout);
            this.panelParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParams.Location = new System.Drawing.Point(0, 0);
            this.panelParams.Name = "panelParams";
            this.panelParams.Size = new System.Drawing.Size(232, 238);
            // 
            // checkBoxOneWayPrint
            // 
            this.checkBoxOneWayPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOneWayPrint.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxOneWayPrint.Location = new System.Drawing.Point(50, 58);
            this.checkBoxOneWayPrint.Name = "checkBoxOneWayPrint";
            this.checkBoxOneWayPrint.Size = new System.Drawing.Size(179, 20);
            this.checkBoxOneWayPrint.TabIndex = 20;
            this.checkBoxOneWayPrint.Text = "Nečekat na odpověď serveru";
            // 
            // comboBoxTemplate
            // 
            this.comboBoxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxTemplate.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxTemplate.Location = new System.Drawing.Point(73, 99);
            this.comboBoxTemplate.Name = "comboBoxTemplate";
            this.comboBoxTemplate.Size = new System.Drawing.Size(156, 19);
            this.comboBoxTemplate.TabIndex = 14;
            // 
            // comboBoxPrinterName
            // 
            this.comboBoxPrinterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPrinterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxPrinterName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxPrinterName.Location = new System.Drawing.Point(73, 80);
            this.comboBoxPrinterName.Name = "comboBoxPrinterName";
            this.comboBoxPrinterName.Size = new System.Drawing.Size(156, 19);
            this.comboBoxPrinterName.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.Text = "Address :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddress.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxAddress.Location = new System.Drawing.Point(72, 0);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(157, 19);
            this.textBoxAddress.TabIndex = 10;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxName.Location = new System.Drawing.Point(72, 19);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(157, 19);
            this.textBoxName.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(3, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 19);
            this.label3.Text = "Name :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(3, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 19);
            this.label5.Text = "Templates :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 19);
            this.label1.Text = "Printer :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(3, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 19);
            this.label4.Text = "Timeout :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxTimeout
            // 
            this.textBoxTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTimeout.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxTimeout.Location = new System.Drawing.Point(72, 38);
            this.textBoxTimeout.Name = "textBoxTimeout";
            this.textBoxTimeout.Size = new System.Drawing.Size(157, 19);
            this.textBoxTimeout.TabIndex = 8;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonRefresh);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 238);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(232, 35);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRefresh.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.buttonRefresh.Location = new System.Drawing.Point(100, 4);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(88, 28);
            this.buttonRefresh.TabIndex = 9;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSave.Location = new System.Drawing.Point(6, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(88, 28);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Save";
            // 
            // tabPageWSInfo
            // 
            this.tabPageWSInfo.AutoScroll = true;
            this.tabPageWSInfo.Controls.Add(this.textBoxInfo);
            this.tabPageWSInfo.Controls.Add(this.buttonWindowsIdentity);
            this.tabPageWSInfo.Controls.Add(this.buttonWriteLog);
            this.tabPageWSInfo.Controls.Add(this.buttonHelloWorld);
            this.tabPageWSInfo.Controls.Add(this.buttonTemplates);
            this.tabPageWSInfo.Controls.Add(this.buttonPrinters);
            this.tabPageWSInfo.Location = new System.Drawing.Point(4, 23);
            this.tabPageWSInfo.Name = "tabPageWSInfo";
            this.tabPageWSInfo.Size = new System.Drawing.Size(232, 273);
            this.tabPageWSInfo.Text = "Info";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInfo.Location = new System.Drawing.Point(3, 3);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxInfo.Size = new System.Drawing.Size(157, 267);
            this.textBoxInfo.TabIndex = 11;
            // 
            // buttonWindowsIdentity
            // 
            this.buttonWindowsIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWindowsIdentity.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.buttonWindowsIdentity.Location = new System.Drawing.Point(166, 139);
            this.buttonWindowsIdentity.Name = "buttonWindowsIdentity";
            this.buttonWindowsIdentity.Size = new System.Drawing.Size(63, 28);
            this.buttonWindowsIdentity.TabIndex = 10;
            this.buttonWindowsIdentity.Text = "WinIdentity";
            this.buttonWindowsIdentity.Click += new System.EventHandler(this.buttonWindowsIdentity_Click);
            // 
            // buttonWriteLog
            // 
            this.buttonWriteLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWriteLog.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.buttonWriteLog.Location = new System.Drawing.Point(166, 105);
            this.buttonWriteLog.Name = "buttonWriteLog";
            this.buttonWriteLog.Size = new System.Drawing.Size(63, 28);
            this.buttonWriteLog.TabIndex = 10;
            this.buttonWriteLog.Text = "WriteLog";
            this.buttonWriteLog.Click += new System.EventHandler(this.buttonWriteLog_Click);
            // 
            // buttonHelloWorld
            // 
            this.buttonHelloWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelloWorld.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.buttonHelloWorld.Location = new System.Drawing.Point(166, 71);
            this.buttonHelloWorld.Name = "buttonHelloWorld";
            this.buttonHelloWorld.Size = new System.Drawing.Size(63, 28);
            this.buttonHelloWorld.TabIndex = 10;
            this.buttonHelloWorld.Text = "HelloWorld";
            this.buttonHelloWorld.Click += new System.EventHandler(this.buttonHelloWorld_Click);
            // 
            // buttonTemplates
            // 
            this.buttonTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTemplates.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.buttonTemplates.Location = new System.Drawing.Point(166, 37);
            this.buttonTemplates.Name = "buttonTemplates";
            this.buttonTemplates.Size = new System.Drawing.Size(63, 28);
            this.buttonTemplates.TabIndex = 10;
            this.buttonTemplates.Text = "Templates";
            this.buttonTemplates.Click += new System.EventHandler(this.buttonTemplates_Click);
            // 
            // buttonPrinters
            // 
            this.buttonPrinters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrinters.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.buttonPrinters.Location = new System.Drawing.Point(166, 3);
            this.buttonPrinters.Name = "buttonPrinters";
            this.buttonPrinters.Size = new System.Drawing.Size(63, 28);
            this.buttonPrinters.TabIndex = 10;
            this.buttonPrinters.Text = "Printers";
            this.buttonPrinters.Click += new System.EventHandler(this.buttonPrinters_Click);
            // 
            // ConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tabControlWebService);
            this.Name = "ConfigControl";
            this.Size = new System.Drawing.Size(240, 300);
            this.tabControlWebService.ResumeLayout(false);
            this.tabPageWSConfig.ResumeLayout(false);
            this.panelParams.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.tabPageWSInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlWebService;
        private System.Windows.Forms.TabPage tabPageWSConfig;
        private System.Windows.Forms.TabPage tabPageWSInfo;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelParams;
        internal System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox textBoxAddress;
        internal System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox textBoxTimeout;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button buttonPrinters;
        private System.Windows.Forms.TextBox textBoxInfo;
        internal System.Windows.Forms.Button buttonTemplates;
        internal System.Windows.Forms.ComboBox comboBoxPrinterName;
        internal System.Windows.Forms.ComboBox comboBoxTemplate;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Button buttonRefresh;
        internal System.Windows.Forms.Button buttonHelloWorld;
        internal System.Windows.Forms.Button buttonWriteLog;
        internal System.Windows.Forms.Button buttonWindowsIdentity;
        internal System.Windows.Forms.CheckBox checkBoxOneWayPrint;

    }
}
