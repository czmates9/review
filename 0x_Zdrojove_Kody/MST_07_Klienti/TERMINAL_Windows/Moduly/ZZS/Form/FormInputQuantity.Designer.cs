namespace FASK.MST_WINDOWS.Module.ZZS
{
    partial class FormInputQuantity
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
            this.ucKeyboard1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.l_CelkemMJ = new System.Windows.Forms.Label();
            this.l_QtypackMJ = new System.Windows.Forms.Label();
            this.l_ItemMJ = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCelkemVBaleni = new System.Windows.Forms.TextBox();
            this.textBoxBaleni = new System.Windows.Forms.TextBox();
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
            this.buttonOK.Location = new System.Drawing.Point(147, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(580, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.ucKeyboard1);
            this.panelComponents.Controls.Add(this.l_CelkemMJ);
            this.panelComponents.Controls.Add(this.l_QtypackMJ);
            this.panelComponents.Controls.Add(this.l_ItemMJ);
            this.panelComponents.Controls.Add(this.label4);
            this.panelComponents.Controls.Add(this.label2);
            this.panelComponents.Controls.Add(this.label1);
            this.panelComponents.Controls.Add(this.textBoxCelkemVBaleni);
            this.panelComponents.Controls.Add(this.textBoxBaleni);
            this.panelComponents.Controls.Add(this.textBoxKod);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(727, 578);
            this.panelComponents.TabIndex = 0;
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeyboard1.KeyboardType = KeyboardClassLibrary.BoW.Numeric;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 220);
            this.ucKeyboard1.MinimumSize = new System.Drawing.Size(469, 256);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(727, 358);
            this.ucKeyboard1.TabIndex = 5;
            this.ucKeyboard1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.ucKeyboard1_UserKeyPressed);
            // 
            // l_CelkemMJ
            // 
            this.l_CelkemMJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.l_CelkemMJ.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.l_CelkemMJ.Location = new System.Drawing.Point(661, 170);
            this.l_CelkemMJ.Name = "l_CelkemMJ";
            this.l_CelkemMJ.Size = new System.Drawing.Size(63, 30);
            this.l_CelkemMJ.TabIndex = 1;
            this.l_CelkemMJ.Text = "[mj]";
            this.l_CelkemMJ.Visible = false;
            // 
            // l_QtypackMJ
            // 
            this.l_QtypackMJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.l_QtypackMJ.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.l_QtypackMJ.Location = new System.Drawing.Point(661, 100);
            this.l_QtypackMJ.Name = "l_QtypackMJ";
            this.l_QtypackMJ.Size = new System.Drawing.Size(63, 30);
            this.l_QtypackMJ.TabIndex = 1;
            this.l_QtypackMJ.Text = "[mj]";
            this.l_QtypackMJ.Visible = false;
            // 
            // l_ItemMJ
            // 
            this.l_ItemMJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.l_ItemMJ.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.l_ItemMJ.Location = new System.Drawing.Point(661, 33);
            this.l_ItemMJ.Name = "l_ItemMJ";
            this.l_ItemMJ.Size = new System.Drawing.Size(63, 30);
            this.l_ItemMJ.TabIndex = 1;
            this.l_ItemMJ.Text = "[mj]";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(4, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(720, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "Celkem v balení :";
            this.label4.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(720, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Balení :";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(720, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Množství :";
            // 
            // textBoxCelkemVBaleni
            // 
            this.textBoxCelkemVBaleni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCelkemVBaleni.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxCelkemVBaleni.Location = new System.Drawing.Point(22, 167);
            this.textBoxCelkemVBaleni.Name = "textBoxCelkemVBaleni";
            this.textBoxCelkemVBaleni.ReadOnly = true;
            this.textBoxCelkemVBaleni.Size = new System.Drawing.Size(637, 33);
            this.textBoxCelkemVBaleni.TabIndex = 0;
            this.textBoxCelkemVBaleni.Visible = false;
            // 
            // textBoxBaleni
            // 
            this.textBoxBaleni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBaleni.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxBaleni.Location = new System.Drawing.Point(22, 97);
            this.textBoxBaleni.Name = "textBoxBaleni";
            this.textBoxBaleni.ReadOnly = true;
            this.textBoxBaleni.Size = new System.Drawing.Size(637, 33);
            this.textBoxBaleni.TabIndex = 0;
            this.textBoxBaleni.Visible = false;
            // 
            // textBoxKod
            // 
            this.textBoxKod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKod.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxKod.Location = new System.Drawing.Point(22, 30);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.Size = new System.Drawing.Size(637, 33);
            this.textBoxKod.TabIndex = 0;
            this.textBoxKod.TextChanged += new System.EventHandler(this.textBoxKod_TextChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 578);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(727, 71);
            this.panelButtons.TabIndex = 1;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(147, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // FormInputQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(727, 649);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormInputQuantity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zadejte kod";
            this.Load += new System.EventHandler(this.FormInputKod_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInputKod_KeyDown);
            this.panelComponents.ResumeLayout(false);
            this.panelComponents.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        protected System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label l_ItemMJ;
        private System.Windows.Forms.Label l_CelkemMJ;
        private System.Windows.Forms.Label l_QtypackMJ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.TextBox textBoxCelkemVBaleni;
        protected System.Windows.Forms.TextBox textBoxBaleni;
        private KeyboardClassLibrary.Keyboardcontrol ucKeyboard1;
    }
}
