namespace Fask.Vyroba_P.Odvadeni
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
            this.components = new System.ComponentModel.Container();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.buttonKorekce = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.panelDetail4Rezerva = new System.Windows.Forms.Panel();
            this.panelDetail3Celkem = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.labelKusu = new System.Windows.Forms.Label();
            this.panelDetail2Casy = new System.Windows.Forms.Panel();
            this.lblCasPripravny = new System.Windows.Forms.Label();
            this.lblCasJednotkovy = new System.Windows.Forms.Label();
            this.lblCasCelkovySKorekci = new System.Windows.Forms.Label();
            this.labelPripravnyCas = new System.Windows.Forms.Label();
            this.labelJednotkovyCas = new System.Windows.Forms.Label();
            this.labelKorekceCasu = new System.Windows.Forms.Label();
            this.labelCelkovyCas = new System.Windows.Forms.Label();
            this.lblCasKorekce = new System.Windows.Forms.Label();
            this.panelDetail1Info = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.labelVyrobniPrikaz = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelPracovnik = new System.Windows.Forms.Label();
            this.lblPolozka = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelStroj = new System.Windows.Forms.Label();
            this.timerDateTimeOperaceUpdate = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.labelZbyvaKusu = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelStartCas = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelStopCas = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            this.panelDetail3Celkem.SuspendLayout();
            this.panelDetail2Casy.SuspendLayout();
            this.panelDetail1Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Controls.Add(this.buttonKorekce);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 386);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(287, 138);
            this.panelButtons.TabIndex = 1;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 67);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(167, 71);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            this.buttonOK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Handle_KeyDown);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 67);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 2;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            this.buttonStorno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Handle_KeyDown);
            // 
            // buttonKorekce
            // 
            this.buttonKorekce.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonKorekce.Font = new System.Drawing.Font("Arial", 16F);
            this.buttonKorekce.Location = new System.Drawing.Point(0, 0);
            this.buttonKorekce.Name = "buttonKorekce";
            this.buttonKorekce.Size = new System.Drawing.Size(287, 67);
            this.buttonKorekce.TabIndex = 1;
            this.buttonKorekce.Text = "Korekce èasu (F1)";
            this.buttonKorekce.Click += new System.EventHandler(this.buttonKorekce_Click);
            this.buttonKorekce.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Handle_KeyDown);
            // 
            // panelComponents
            // 
            this.panelComponents.AutoScroll = true;
            this.panelComponents.Controls.Add(this.panelDetail4Rezerva);
            this.panelComponents.Controls.Add(this.panelDetail3Celkem);
            this.panelComponents.Controls.Add(this.panelDetail2Casy);
            this.panelComponents.Controls.Add(this.panelDetail1Info);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(287, 386);
            this.panelComponents.TabIndex = 0;
            // 
            // panelDetail4Rezerva
            // 
            this.panelDetail4Rezerva.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetail4Rezerva.Location = new System.Drawing.Point(0, 296);
            this.panelDetail4Rezerva.Name = "panelDetail4Rezerva";
            this.panelDetail4Rezerva.Size = new System.Drawing.Size(287, 10);
            this.panelDetail4Rezerva.TabIndex = 19;
            // 
            // panelDetail3Celkem
            // 
            this.panelDetail3Celkem.Controls.Add(this.labelZbyvaKusu);
            this.panelDetail3Celkem.Controls.Add(this.label4);
            this.panelDetail3Celkem.Controls.Add(this.label3);
            this.panelDetail3Celkem.Controls.Add(this.labelKusu);
            this.panelDetail3Celkem.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetail3Celkem.Location = new System.Drawing.Point(0, 245);
            this.panelDetail3Celkem.Name = "panelDetail3Celkem";
            this.panelDetail3Celkem.Size = new System.Drawing.Size(287, 51);
            this.panelDetail3Celkem.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Kusù :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelKusu
            // 
            this.labelKusu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKusu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labelKusu.Location = new System.Drawing.Point(120, 0);
            this.labelKusu.Name = "labelKusu";
            this.labelKusu.Size = new System.Drawing.Size(164, 20);
            this.labelKusu.TabIndex = 15;
            this.labelKusu.Text = "?";
            // 
            // panelDetail2Casy
            // 
            this.panelDetail2Casy.Controls.Add(this.labelStopCas);
            this.panelDetail2Casy.Controls.Add(this.label8);
            this.panelDetail2Casy.Controls.Add(this.labelStartCas);
            this.panelDetail2Casy.Controls.Add(this.label5);
            this.panelDetail2Casy.Controls.Add(this.lblCasPripravny);
            this.panelDetail2Casy.Controls.Add(this.lblCasJednotkovy);
            this.panelDetail2Casy.Controls.Add(this.lblCasCelkovySKorekci);
            this.panelDetail2Casy.Controls.Add(this.labelPripravnyCas);
            this.panelDetail2Casy.Controls.Add(this.labelJednotkovyCas);
            this.panelDetail2Casy.Controls.Add(this.labelKorekceCasu);
            this.panelDetail2Casy.Controls.Add(this.labelCelkovyCas);
            this.panelDetail2Casy.Controls.Add(this.lblCasKorekce);
            this.panelDetail2Casy.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetail2Casy.Location = new System.Drawing.Point(0, 109);
            this.panelDetail2Casy.Name = "panelDetail2Casy";
            this.panelDetail2Casy.Size = new System.Drawing.Size(287, 136);
            this.panelDetail2Casy.TabIndex = 17;
            // 
            // lblCasPripravny
            // 
            this.lblCasPripravny.Font = new System.Drawing.Font("Arial", 10F);
            this.lblCasPripravny.Location = new System.Drawing.Point(3, 0);
            this.lblCasPripravny.Name = "lblCasPripravny";
            this.lblCasPripravny.Size = new System.Drawing.Size(111, 20);
            this.lblCasPripravny.TabIndex = 6;
            this.lblCasPripravny.Text = "Pøípravný èas :";
            this.lblCasPripravny.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCasJednotkovy
            // 
            this.lblCasJednotkovy.Font = new System.Drawing.Font("Arial", 10F);
            this.lblCasJednotkovy.Location = new System.Drawing.Point(3, 20);
            this.lblCasJednotkovy.Name = "lblCasJednotkovy";
            this.lblCasJednotkovy.Size = new System.Drawing.Size(111, 20);
            this.lblCasJednotkovy.TabIndex = 8;
            this.lblCasJednotkovy.Text = "Jednotk. èas :";
            this.lblCasJednotkovy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCasCelkovySKorekci
            // 
            this.lblCasCelkovySKorekci.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblCasCelkovySKorekci.Location = new System.Drawing.Point(3, 104);
            this.lblCasCelkovySKorekci.Name = "lblCasCelkovySKorekci";
            this.lblCasCelkovySKorekci.Size = new System.Drawing.Size(111, 20);
            this.lblCasCelkovySKorekci.TabIndex = 12;
            this.lblCasCelkovySKorekci.Text = "Cel. èas s k. :";
            this.lblCasCelkovySKorekci.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPripravnyCas
            // 
            this.labelPripravnyCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPripravnyCas.Font = new System.Drawing.Font("Arial", 10F);
            this.labelPripravnyCas.Location = new System.Drawing.Point(120, 0);
            this.labelPripravnyCas.Name = "labelPripravnyCas";
            this.labelPripravnyCas.Size = new System.Drawing.Size(164, 20);
            this.labelPripravnyCas.TabIndex = 7;
            this.labelPripravnyCas.Text = "?";
            // 
            // labelJednotkovyCas
            // 
            this.labelJednotkovyCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelJednotkovyCas.Font = new System.Drawing.Font("Arial", 10F);
            this.labelJednotkovyCas.Location = new System.Drawing.Point(120, 20);
            this.labelJednotkovyCas.Name = "labelJednotkovyCas";
            this.labelJednotkovyCas.Size = new System.Drawing.Size(164, 20);
            this.labelJednotkovyCas.TabIndex = 9;
            this.labelJednotkovyCas.Text = "?";
            // 
            // labelKorekceCasu
            // 
            this.labelKorekceCasu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKorekceCasu.Font = new System.Drawing.Font("Arial", 10F);
            this.labelKorekceCasu.Location = new System.Drawing.Point(120, 40);
            this.labelKorekceCasu.Name = "labelKorekceCasu";
            this.labelKorekceCasu.Size = new System.Drawing.Size(164, 20);
            this.labelKorekceCasu.TabIndex = 11;
            this.labelKorekceCasu.Text = "?";
            // 
            // labelCelkovyCas
            // 
            this.labelCelkovyCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCelkovyCas.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labelCelkovyCas.Location = new System.Drawing.Point(119, 104);
            this.labelCelkovyCas.Name = "labelCelkovyCas";
            this.labelCelkovyCas.Size = new System.Drawing.Size(164, 20);
            this.labelCelkovyCas.TabIndex = 13;
            this.labelCelkovyCas.Text = "?";
            // 
            // lblCasKorekce
            // 
            this.lblCasKorekce.Font = new System.Drawing.Font("Arial", 10F);
            this.lblCasKorekce.Location = new System.Drawing.Point(3, 40);
            this.lblCasKorekce.Name = "lblCasKorekce";
            this.lblCasKorekce.Size = new System.Drawing.Size(111, 20);
            this.lblCasKorekce.TabIndex = 10;
            this.lblCasKorekce.Text = "Korekce èasu :";
            this.lblCasKorekce.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelDetail1Info
            // 
            this.panelDetail1Info.Controls.Add(this.label2);
            this.panelDetail1Info.Controls.Add(this.labelVyrobniPrikaz);
            this.panelDetail1Info.Controls.Add(this.label6);
            this.panelDetail1Info.Controls.Add(this.labelPracovnik);
            this.panelDetail1Info.Controls.Add(this.lblPolozka);
            this.panelDetail1Info.Controls.Add(this.label1);
            this.panelDetail1Info.Controls.Add(this.label7);
            this.panelDetail1Info.Controls.Add(this.labelStroj);
            this.panelDetail1Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetail1Info.Location = new System.Drawing.Point(0, 0);
            this.panelDetail1Info.Name = "panelDetail1Info";
            this.panelDetail1Info.Size = new System.Drawing.Size(287, 109);
            this.panelDetail1Info.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Výr. pøík. :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelVyrobniPrikaz
            // 
            this.labelVyrobniPrikaz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVyrobniPrikaz.Font = new System.Drawing.Font("Arial", 10F);
            this.labelVyrobniPrikaz.Location = new System.Drawing.Point(90, 5);
            this.labelVyrobniPrikaz.Name = "labelVyrobniPrikaz";
            this.labelVyrobniPrikaz.Size = new System.Drawing.Size(193, 20);
            this.labelVyrobniPrikaz.TabIndex = 7;
            this.labelVyrobniPrikaz.Text = "?";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(4, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Pracovník :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPracovnik
            // 
            this.labelPracovnik.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPracovnik.Font = new System.Drawing.Font("Arial", 10F);
            this.labelPracovnik.Location = new System.Drawing.Point(91, 65);
            this.labelPracovnik.Name = "labelPracovnik";
            this.labelPracovnik.Size = new System.Drawing.Size(193, 20);
            this.labelPracovnik.TabIndex = 3;
            this.labelPracovnik.Text = "?";
            // 
            // lblPolozka
            // 
            this.lblPolozka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPolozka.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPolozka.Location = new System.Drawing.Point(90, 25);
            this.lblPolozka.Name = "lblPolozka";
            this.lblPolozka.Size = new System.Drawing.Size(194, 40);
            this.lblPolozka.TabIndex = 1;
            this.lblPolozka.Text = "?";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Položka :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 10F);
            this.label7.Location = new System.Drawing.Point(4, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "Stroj :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStroj
            // 
            this.labelStroj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStroj.Font = new System.Drawing.Font("Arial", 10F);
            this.labelStroj.Location = new System.Drawing.Point(91, 85);
            this.labelStroj.Name = "labelStroj";
            this.labelStroj.Size = new System.Drawing.Size(193, 20);
            this.labelStroj.TabIndex = 5;
            this.labelStroj.Text = "?";
            // 
            // timerDateTimeOperaceUpdate
            // 
            this.timerDateTimeOperaceUpdate.Interval = 1000;
            this.timerDateTimeOperaceUpdate.Tick += new System.EventHandler(this.timerDateTimeOperaceUpdate_Tick);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(4, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Zbývá kusù :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelZbyvaKusu
            // 
            this.labelZbyvaKusu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelZbyvaKusu.Font = new System.Drawing.Font("Arial", 10F);
            this.labelZbyvaKusu.Location = new System.Drawing.Point(121, 20);
            this.labelZbyvaKusu.Name = "labelZbyvaKusu";
            this.labelZbyvaKusu.Size = new System.Drawing.Size(162, 20);
            this.labelZbyvaKusu.TabIndex = 8;
            this.labelZbyvaKusu.Text = "?";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(3, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Start èas :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStartCas
            // 
            this.labelStartCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStartCas.Font = new System.Drawing.Font("Arial", 10F);
            this.labelStartCas.Location = new System.Drawing.Point(120, 60);
            this.labelStartCas.Name = "labelStartCas";
            this.labelStartCas.Size = new System.Drawing.Size(164, 20);
            this.labelStartCas.TabIndex = 15;
            this.labelStartCas.Text = "?";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 10F);
            this.label8.Location = new System.Drawing.Point(3, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Stop èas :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStopCas
            // 
            this.labelStopCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStopCas.Font = new System.Drawing.Font("Arial", 10F);
            this.labelStopCas.Location = new System.Drawing.Point(120, 80);
            this.labelStopCas.Name = "labelStopCas";
            this.labelStopCas.Size = new System.Drawing.Size(164, 20);
            this.labelStopCas.TabIndex = 17;
            this.labelStopCas.Text = "?";
            // 
            // FormOperacePotvrzeni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(287, 524);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormOperacePotvrzeni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Potvrzení operace";
            this.Load += new System.EventHandler(this.FormOperacePotvrzeni_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOperacePotvrzeni_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            this.panelDetail3Celkem.ResumeLayout(false);
            this.panelDetail2Casy.ResumeLayout(false);
            this.panelDetail1Info.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label lblCasPripravny;
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
        private System.Windows.Forms.Label lblCasCelkovySKorekci;
        private System.Windows.Forms.Label lblCasJednotkovy;
        private System.Windows.Forms.Label labelKorekceCasu;
        private System.Windows.Forms.Label lblCasKorekce;
        public System.Windows.Forms.Button buttonKorekce;
        private System.Windows.Forms.Timer timerDateTimeOperaceUpdate;
        private System.Windows.Forms.Panel panelDetail4Rezerva;
        private System.Windows.Forms.Panel panelDetail3Celkem;
        private System.Windows.Forms.Panel panelDetail2Casy;
        private System.Windows.Forms.Panel panelDetail1Info;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelVyrobniPrikaz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelZbyvaKusu;
        private System.Windows.Forms.Label labelStopCas;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelStartCas;
        private System.Windows.Forms.Label label5;

    }
}
