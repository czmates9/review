namespace Fask.Vyroba_W.Odvadeni
{
    partial class FormOperacePotvrzeni
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelKOR_MAT = new System.Windows.Forms.Panel();
            this.buttonKorekce = new System.Windows.Forms.Button();
            this.buttonmaterial = new System.Windows.Forms.Button();
            this.panelbuttonOK_CANCEL = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.labelKorekceCasu = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelStroj = new System.Windows.Forms.Label();
            this.labelPracovnik = new System.Windows.Forms.Label();
            this.labelCelkovyCas = new System.Windows.Forms.Label();
            this.labelJednotkovyCas = new System.Windows.Forms.Label();
            this.labelPripravnyCas = new System.Windows.Forms.Label();
            this.labelKusu = new System.Windows.Forms.Label();
            this.lblPolozka = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerDateTimeOperaceUpdate = new System.Windows.Forms.Timer();
            this.panelButtons.SuspendLayout();
            this.panelKOR_MAT.SuspendLayout();
            this.panelbuttonOK_CANCEL.SuspendLayout();
            this.panelComponents.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.panelKOR_MAT);
            this.panelButtons.Controls.Add(this.panelbuttonOK_CANCEL);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 195);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(238, 100);
            // 
            // panelKOR_MAT
            // 
            this.panelKOR_MAT.Controls.Add(this.buttonKorekce);
            this.panelKOR_MAT.Controls.Add(this.buttonmaterial);
            this.panelKOR_MAT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKOR_MAT.Location = new System.Drawing.Point(0, 0);
            this.panelKOR_MAT.Name = "panelKOR_MAT";
            this.panelKOR_MAT.Size = new System.Drawing.Size(238, 50);
            // 
            // buttonKorekce
            // 
            this.buttonKorekce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonKorekce.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.buttonKorekce.Location = new System.Drawing.Point(0, 0);
            this.buttonKorekce.Name = "buttonKorekce";
            this.buttonKorekce.Size = new System.Drawing.Size(120, 50);
            this.buttonKorekce.TabIndex = 3;
            this.buttonKorekce.Text = "Korekce";
            this.buttonKorekce.Click += new System.EventHandler(this.buttonKorekce_Click);
            // 
            // buttonmaterial
            // 
            this.buttonmaterial.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonmaterial.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.buttonmaterial.Location = new System.Drawing.Point(120, 0);
            this.buttonmaterial.Name = "buttonmaterial";
            this.buttonmaterial.Size = new System.Drawing.Size(118, 50);
            this.buttonmaterial.TabIndex = 4;
            this.buttonmaterial.Text = "Material";
            this.buttonmaterial.Click += new System.EventHandler(this.buttonmaterial_Click);
            // 
            // panelbuttonOK_CANCEL
            // 
            this.panelbuttonOK_CANCEL.Controls.Add(this.buttonOK);
            this.panelbuttonOK_CANCEL.Controls.Add(this.buttonStorno);
            this.panelbuttonOK_CANCEL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelbuttonOK_CANCEL.Location = new System.Drawing.Point(0, 50);
            this.panelbuttonOK_CANCEL.Name = "panelbuttonOK_CANCEL";
            this.panelbuttonOK_CANCEL.Size = new System.Drawing.Size(238, 50);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(118, 50);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 50);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.AutoScroll = true;
            this.panelComponents.Controls.Add(this.labelKorekceCasu);
            this.panelComponents.Controls.Add(this.panelButtons);
            this.panelComponents.Controls.Add(this.label9);
            this.panelComponents.Controls.Add(this.labelStroj);
            this.panelComponents.Controls.Add(this.labelPracovnik);
            this.panelComponents.Controls.Add(this.labelCelkovyCas);
            this.panelComponents.Controls.Add(this.labelJednotkovyCas);
            this.panelComponents.Controls.Add(this.labelPripravnyCas);
            this.panelComponents.Controls.Add(this.labelKusu);
            this.panelComponents.Controls.Add(this.lblPolozka);
            this.panelComponents.Controls.Add(this.label7);
            this.panelComponents.Controls.Add(this.label6);
            this.panelComponents.Controls.Add(this.label5);
            this.panelComponents.Controls.Add(this.label2);
            this.panelComponents.Controls.Add(this.label4);
            this.panelComponents.Controls.Add(this.label3);
            this.panelComponents.Controls.Add(this.label1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(238, 295);
            // 
            // labelKorekceCasu
            // 
            this.labelKorekceCasu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKorekceCasu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelKorekceCasu.Location = new System.Drawing.Point(120, 100);
            this.labelKorekceCasu.Name = "labelKorekceCasu";
            this.labelKorekceCasu.Size = new System.Drawing.Size(115, 20);
            this.labelKorekceCasu.Text = "?";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label9.Location = new System.Drawing.Point(3, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 20);
            this.label9.Text = "Korekce èasu :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStroj
            // 
            this.labelStroj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStroj.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelStroj.Location = new System.Drawing.Point(90, 40);
            this.labelStroj.Name = "labelStroj";
            this.labelStroj.Size = new System.Drawing.Size(145, 20);
            this.labelStroj.Text = "?";
            // 
            // labelPracovnik
            // 
            this.labelPracovnik.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPracovnik.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelPracovnik.Location = new System.Drawing.Point(90, 20);
            this.labelPracovnik.Name = "labelPracovnik";
            this.labelPracovnik.Size = new System.Drawing.Size(145, 20);
            this.labelPracovnik.Text = "?";
            // 
            // labelCelkovyCas
            // 
            this.labelCelkovyCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCelkovyCas.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labelCelkovyCas.Location = new System.Drawing.Point(120, 120);
            this.labelCelkovyCas.Name = "labelCelkovyCas";
            this.labelCelkovyCas.Size = new System.Drawing.Size(115, 20);
            this.labelCelkovyCas.Text = "?";
            // 
            // labelJednotkovyCas
            // 
            this.labelJednotkovyCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelJednotkovyCas.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelJednotkovyCas.Location = new System.Drawing.Point(120, 80);
            this.labelJednotkovyCas.Name = "labelJednotkovyCas";
            this.labelJednotkovyCas.Size = new System.Drawing.Size(115, 20);
            this.labelJednotkovyCas.Text = "?";
            // 
            // labelPripravnyCas
            // 
            this.labelPripravnyCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPripravnyCas.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.labelPripravnyCas.Location = new System.Drawing.Point(120, 60);
            this.labelPripravnyCas.Name = "labelPripravnyCas";
            this.labelPripravnyCas.Size = new System.Drawing.Size(115, 20);
            this.labelPripravnyCas.Text = "?";
            // 
            // labelKusu
            // 
            this.labelKusu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKusu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labelKusu.Location = new System.Drawing.Point(120, 140);
            this.labelKusu.Name = "labelKusu";
            this.labelKusu.Size = new System.Drawing.Size(115, 20);
            this.labelKusu.Text = "?";
            // 
            // lblPolozka
            // 
            this.lblPolozka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPolozka.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.lblPolozka.Location = new System.Drawing.Point(90, 0);
            this.lblPolozka.Name = "lblPolozka";
            this.lblPolozka.Size = new System.Drawing.Size(145, 20);
            this.lblPolozka.Text = "?";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(3, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 20);
            this.label7.Text = "Stroj :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(3, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 20);
            this.label6.Text = "Pracovník :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(3, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 20);
            this.label5.Text = "Cel. èas s k. :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.Text = "Jednotk. èas :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(3, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 20);
            this.label4.Text = "Pøípravný èas :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.Text = "Kusù :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "Položka :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // timerDateTimeOperaceUpdate
            // 
            this.timerDateTimeOperaceUpdate.Interval = 1000;
            this.timerDateTimeOperaceUpdate.Tick += new System.EventHandler(this.timerDateTimeOperaceUpdate_Tick);
            // 
            // FormOperacePotvrzeni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.KeyPreview = true;
            this.Name = "FormOperacePotvrzeni";
            this.Text = "Potvrzení operace";
            this.Load += new System.EventHandler(this.FormOperacePotvrzeni_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOperacePotvrzeni_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelKOR_MAT.ResumeLayout(false);
            this.panelbuttonOK_CANCEL.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelStroj;
        private System.Windows.Forms.Label labelPracovnik;
        private System.Windows.Forms.Label labelCelkovyCas;
        private System.Windows.Forms.Label labelJednotkovyCas;
        private System.Windows.Forms.Label labelPripravnyCas;
        private System.Windows.Forms.Label labelKusu;
        private System.Windows.Forms.Label lblPolozka;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelKorekceCasu;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Button buttonKorekce;
        private System.Windows.Forms.Timer timerDateTimeOperaceUpdate;
        private System.Windows.Forms.Button buttonmaterial;
        private System.Windows.Forms.Panel panelbuttonOK_CANCEL;
        private System.Windows.Forms.Panel panelKOR_MAT;

    }
}
