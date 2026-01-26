namespace Fask.Vyroba_W.Forms
{
    partial class FormInputKod2
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
            this.panelComponents = new System.Windows.Forms.Panel();
            this.label_Mnoztvi = new System.Windows.Forms.Label();
            this.label_Lokace = new System.Windows.Forms.Label();
            this.label_Sklad = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLokaceName = new System.Windows.Forms.Label();
            this.labelSkladName = new System.Windows.Forms.Label();
            this.label_Nazev = new System.Windows.Forms.Label();
            this.label_Material = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(118, 56);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.label_Mnoztvi);
            this.panelComponents.Controls.Add(this.label_Lokace);
            this.panelComponents.Controls.Add(this.label_Sklad);
            this.panelComponents.Controls.Add(this.label3);
            this.panelComponents.Controls.Add(this.labelLokaceName);
            this.panelComponents.Controls.Add(this.labelSkladName);
            this.panelComponents.Controls.Add(this.label_Nazev);
            this.panelComponents.Controls.Add(this.label_Material);
            this.panelComponents.Controls.Add(this.label1);
            this.panelComponents.Controls.Add(this.textBoxKod);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(238, 239);
            // 
            // label_Mnoztvi
            // 
            this.label_Mnoztvi.Location = new System.Drawing.Point(91, 102);
            this.label_Mnoztvi.Name = "label_Mnoztvi";
            this.label_Mnoztvi.Size = new System.Drawing.Size(131, 20);
            this.label_Mnoztvi.Text = "-";
            // 
            // label_Lokace
            // 
            this.label_Lokace.Location = new System.Drawing.Point(91, 142);
            this.label_Lokace.Name = "label_Lokace";
            this.label_Lokace.Size = new System.Drawing.Size(131, 20);
            this.label_Lokace.Text = "-";
            // 
            // label_Sklad
            // 
            this.label_Sklad.Location = new System.Drawing.Point(91, 122);
            this.label_Sklad.Name = "label_Sklad";
            this.label_Sklad.Size = new System.Drawing.Size(131, 20);
            this.label_Sklad.Text = "-";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.Text = "Množství: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelLokaceName
            // 
            this.labelLokaceName.Location = new System.Drawing.Point(16, 142);
            this.labelLokaceName.Name = "labelLokaceName";
            this.labelLokaceName.Size = new System.Drawing.Size(59, 20);
            this.labelLokaceName.Text = "Lokace: ";
            this.labelLokaceName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSkladName
            // 
            this.labelSkladName.Location = new System.Drawing.Point(16, 122);
            this.labelSkladName.Name = "labelSkladName";
            this.labelSkladName.Size = new System.Drawing.Size(59, 20);
            this.labelSkladName.Text = "Sklad: ";
            this.labelSkladName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_Nazev
            // 
            this.label_Nazev.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_Nazev.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label_Nazev.Location = new System.Drawing.Point(0, 0);
            this.label_Nazev.Name = "label_Nazev";
            this.label_Nazev.Size = new System.Drawing.Size(238, 52);
            this.label_Nazev.Text = "Zadejte XXX";
            // 
            // label_Material
            // 
            this.label_Material.Location = new System.Drawing.Point(91, 58);
            this.label_Material.Name = "label_Material";
            this.label_Material.Size = new System.Drawing.Size(131, 44);
            this.label_Material.Text = "-";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.Text = "Material: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxKod
            // 
            this.textBoxKod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKod.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxKod.Location = new System.Drawing.Point(9, 165);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.Size = new System.Drawing.Size(213, 32);
            this.textBoxKod.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 239);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(238, 56);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 56);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Pøeskoè";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // FormInputKod2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormInputKod2";
            this.Text = "Zadejte kod";
            this.Load += new System.EventHandler(this.FormInputKod2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInputKod2_KeyDown);
            this.panelComponents.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        protected System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.Label label_Lokace;
        private System.Windows.Forms.Label label_Sklad;
        private System.Windows.Forms.Label labelLokaceName;
        private System.Windows.Forms.Label labelSkladName;
        private System.Windows.Forms.Label label_Material;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Nazev;
        private System.Windows.Forms.Label label_Mnoztvi;
        private System.Windows.Forms.Label label3;
    }
}
