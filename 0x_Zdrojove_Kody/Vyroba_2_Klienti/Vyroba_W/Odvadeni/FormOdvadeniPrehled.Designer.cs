namespace Fask.Vyroba_W.Odvadeni
{
    partial class FormOdvadeniPrehled
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
            this.panelLastOdvod = new System.Windows.Forms.Panel();
            this.dfTimeStateLast = new Fask.Graphic.DataField();
            this.dfLastQuantity = new Fask.Graphic.DataField();
            this.dfLastBarcodeP = new Fask.Graphic.DataField();
            this.dfLastITEMDESC = new Fask.Graphic.DataField();
            this.dfLastSOPNUMBER = new Fask.Graphic.DataField();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dfTimeStateActual = new Fask.Graphic.DataField();
            this.dfVppBarcodeP = new Fask.Graphic.DataField();
            this.dfVppVnditnum = new Fask.Graphic.DataField();
            this.dfVppItemdesc = new Fask.Graphic.DataField();
            this.dfVppItemnmbr = new Fask.Graphic.DataField();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dfVphSOPDESC = new Fask.Graphic.DataField();
            this.dfVphSOPNUMBE = new Fask.Graphic.DataField();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents.SuspendLayout();
            this.panelLastOdvod.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(117, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.AutoScroll = true;
            this.panelComponents.Controls.Add(this.panelLastOdvod);
            this.panelComponents.Controls.Add(this.panel2);
            this.panelComponents.Controls.Add(this.dfTimeStateActual);
            this.panelComponents.Controls.Add(this.dfVppBarcodeP);
            this.panelComponents.Controls.Add(this.dfVppVnditnum);
            this.panelComponents.Controls.Add(this.dfVppItemdesc);
            this.panelComponents.Controls.Add(this.dfVppItemnmbr);
            this.panelComponents.Controls.Add(this.panel1);
            this.panelComponents.Controls.Add(this.dfVphSOPDESC);
            this.panelComponents.Controls.Add(this.dfVphSOPNUMBE);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(237, 267);
            // 
            // panelLastOdvod
            // 
            this.panelLastOdvod.Controls.Add(this.dfTimeStateLast);
            this.panelLastOdvod.Controls.Add(this.dfLastQuantity);
            this.panelLastOdvod.Controls.Add(this.dfLastBarcodeP);
            this.panelLastOdvod.Controls.Add(this.dfLastITEMDESC);
            this.panelLastOdvod.Controls.Add(this.dfLastSOPNUMBER);
            this.panelLastOdvod.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLastOdvod.Location = new System.Drawing.Point(0, 137);
            this.panelLastOdvod.Name = "panelLastOdvod";
            this.panelLastOdvod.Size = new System.Drawing.Size(237, 96);
            // 
            // dfTimeStateLast
            // 
            this.dfTimeStateLast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfTimeStateLast.Data = "";
            this.dfTimeStateLast.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfTimeStateLast.DataMaxLength = 32767;
            this.dfTimeStateLast.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTimeStateLast.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfTimeStateLast.Location = new System.Drawing.Point(0, 76);
            this.dfTimeStateLast.MultiLine = false;
            this.dfTimeStateLast.Name = "dfTimeStateLast";
            this.dfTimeStateLast.Popis = "Stav";
            this.dfTimeStateLast.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTimeStateLast.PopisWidth = 80;
            this.dfTimeStateLast.ReadOnly = true;
            this.dfTimeStateLast.Size = new System.Drawing.Size(237, 19);
            this.dfTimeStateLast.TabIndex = 20;
            // 
            // dfLastQuantity
            // 
            this.dfLastQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfLastQuantity.Data = "";
            this.dfLastQuantity.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfLastQuantity.DataMaxLength = 32767;
            this.dfLastQuantity.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastQuantity.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfLastQuantity.Location = new System.Drawing.Point(0, 57);
            this.dfLastQuantity.MultiLine = false;
            this.dfLastQuantity.Name = "dfLastQuantity";
            this.dfLastQuantity.Popis = "Množství";
            this.dfLastQuantity.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastQuantity.PopisWidth = 80;
            this.dfLastQuantity.ReadOnly = true;
            this.dfLastQuantity.Size = new System.Drawing.Size(237, 19);
            this.dfLastQuantity.TabIndex = 19;
            // 
            // dfLastBarcodeP
            // 
            this.dfLastBarcodeP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfLastBarcodeP.Data = "";
            this.dfLastBarcodeP.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfLastBarcodeP.DataMaxLength = 32767;
            this.dfLastBarcodeP.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastBarcodeP.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfLastBarcodeP.Location = new System.Drawing.Point(0, 38);
            this.dfLastBarcodeP.MultiLine = false;
            this.dfLastBarcodeP.Name = "dfLastBarcodeP";
            this.dfLastBarcodeP.Popis = "Èíslo";
            this.dfLastBarcodeP.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastBarcodeP.PopisWidth = 80;
            this.dfLastBarcodeP.ReadOnly = true;
            this.dfLastBarcodeP.Size = new System.Drawing.Size(237, 19);
            this.dfLastBarcodeP.TabIndex = 18;
            // 
            // dfLastITEMDESC
            // 
            this.dfLastITEMDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfLastITEMDESC.Data = "";
            this.dfLastITEMDESC.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfLastITEMDESC.DataMaxLength = 32767;
            this.dfLastITEMDESC.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastITEMDESC.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfLastITEMDESC.Location = new System.Drawing.Point(0, 19);
            this.dfLastITEMDESC.MultiLine = false;
            this.dfLastITEMDESC.Name = "dfLastITEMDESC";
            this.dfLastITEMDESC.Popis = "Název";
            this.dfLastITEMDESC.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastITEMDESC.PopisWidth = 80;
            this.dfLastITEMDESC.ReadOnly = true;
            this.dfLastITEMDESC.Size = new System.Drawing.Size(237, 19);
            this.dfLastITEMDESC.TabIndex = 15;
            // 
            // dfLastSOPNUMBER
            // 
            this.dfLastSOPNUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfLastSOPNUMBER.Data = "";
            this.dfLastSOPNUMBER.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfLastSOPNUMBER.DataMaxLength = 32767;
            this.dfLastSOPNUMBER.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastSOPNUMBER.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfLastSOPNUMBER.Location = new System.Drawing.Point(0, 0);
            this.dfLastSOPNUMBER.MultiLine = false;
            this.dfLastSOPNUMBER.Name = "dfLastSOPNUMBER";
            this.dfLastSOPNUMBER.Popis = "Pøíkaz pøed.";
            this.dfLastSOPNUMBER.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastSOPNUMBER.PopisWidth = 80;
            this.dfLastSOPNUMBER.ReadOnly = true;
            this.dfLastSOPNUMBER.Size = new System.Drawing.Size(237, 19);
            this.dfLastSOPNUMBER.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 135);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(237, 2);
            // 
            // dfTimeStateActual
            // 
            this.dfTimeStateActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfTimeStateActual.Data = "";
            this.dfTimeStateActual.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfTimeStateActual.DataMaxLength = 32767;
            this.dfTimeStateActual.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTimeStateActual.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfTimeStateActual.Location = new System.Drawing.Point(0, 116);
            this.dfTimeStateActual.MultiLine = false;
            this.dfTimeStateActual.Name = "dfTimeStateActual";
            this.dfTimeStateActual.Popis = "Stav";
            this.dfTimeStateActual.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTimeStateActual.PopisWidth = 80;
            this.dfTimeStateActual.ReadOnly = true;
            this.dfTimeStateActual.Size = new System.Drawing.Size(237, 19);
            this.dfTimeStateActual.TabIndex = 15;
            // 
            // dfVppBarcodeP
            // 
            this.dfVppBarcodeP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfVppBarcodeP.Data = "";
            this.dfVppBarcodeP.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVppBarcodeP.DataMaxLength = 32767;
            this.dfVppBarcodeP.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppBarcodeP.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVppBarcodeP.Location = new System.Drawing.Point(0, 97);
            this.dfVppBarcodeP.MultiLine = false;
            this.dfVppBarcodeP.Name = "dfVppBarcodeP";
            this.dfVppBarcodeP.Popis = "Èíslo";
            this.dfVppBarcodeP.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppBarcodeP.PopisWidth = 80;
            this.dfVppBarcodeP.ReadOnly = true;
            this.dfVppBarcodeP.Size = new System.Drawing.Size(237, 19);
            this.dfVppBarcodeP.TabIndex = 6;
            // 
            // dfVppVnditnum
            // 
            this.dfVppVnditnum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfVppVnditnum.Data = "";
            this.dfVppVnditnum.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVppVnditnum.DataMaxLength = 32767;
            this.dfVppVnditnum.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppVnditnum.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVppVnditnum.Location = new System.Drawing.Point(0, 78);
            this.dfVppVnditnum.MultiLine = false;
            this.dfVppVnditnum.Name = "dfVppVnditnum";
            this.dfVppVnditnum.Popis = "Èár. kód";
            this.dfVppVnditnum.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppVnditnum.PopisWidth = 80;
            this.dfVppVnditnum.ReadOnly = true;
            this.dfVppVnditnum.Size = new System.Drawing.Size(237, 19);
            this.dfVppVnditnum.TabIndex = 5;
            // 
            // dfVppItemdesc
            // 
            this.dfVppItemdesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfVppItemdesc.Data = "";
            this.dfVppItemdesc.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVppItemdesc.DataMaxLength = 32767;
            this.dfVppItemdesc.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppItemdesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVppItemdesc.Location = new System.Drawing.Point(0, 40);
            this.dfVppItemdesc.MultiLine = false;
            this.dfVppItemdesc.Name = "dfVppItemdesc";
            this.dfVppItemdesc.Popis = "Název";
            this.dfVppItemdesc.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppItemdesc.PopisWidth = 80;
            this.dfVppItemdesc.ReadOnly = true;
            this.dfVppItemdesc.Size = new System.Drawing.Size(237, 38);
            this.dfVppItemdesc.TabIndex = 4;
            // 
            // dfVppItemnmbr
            // 
            this.dfVppItemnmbr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfVppItemnmbr.Data = "";
            this.dfVppItemnmbr.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVppItemnmbr.DataMaxLength = 32767;
            this.dfVppItemnmbr.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppItemnmbr.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVppItemnmbr.Location = new System.Drawing.Point(0, 40);
            this.dfVppItemnmbr.MultiLine = false;
            this.dfVppItemnmbr.Name = "dfVppItemnmbr";
            this.dfVppItemnmbr.Popis = "Operace";
            this.dfVppItemnmbr.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppItemnmbr.PopisWidth = 80;
            this.dfVppItemnmbr.ReadOnly = true;
            this.dfVppItemnmbr.Size = new System.Drawing.Size(237, 0);
            this.dfVppItemnmbr.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 2);
            // 
            // dfVphSOPDESC
            // 
            this.dfVphSOPDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfVphSOPDESC.Data = "";
            this.dfVphSOPDESC.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVphSOPDESC.DataMaxLength = 32767;
            this.dfVphSOPDESC.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVphSOPDESC.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVphSOPDESC.Location = new System.Drawing.Point(0, 19);
            this.dfVphSOPDESC.MultiLine = false;
            this.dfVphSOPDESC.Name = "dfVphSOPDESC";
            this.dfVphSOPDESC.Popis = "Popis";
            this.dfVphSOPDESC.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVphSOPDESC.PopisWidth = 80;
            this.dfVphSOPDESC.ReadOnly = true;
            this.dfVphSOPDESC.Size = new System.Drawing.Size(237, 19);
            this.dfVphSOPDESC.TabIndex = 1;
            // 
            // dfVphSOPNUMBE
            // 
            this.dfVphSOPNUMBE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfVphSOPNUMBE.Data = "";
            this.dfVphSOPNUMBE.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVphSOPNUMBE.DataMaxLength = 32767;
            this.dfVphSOPNUMBE.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVphSOPNUMBE.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVphSOPNUMBE.Location = new System.Drawing.Point(0, 0);
            this.dfVphSOPNUMBE.MultiLine = false;
            this.dfVphSOPNUMBE.Name = "dfVphSOPNUMBE";
            this.dfVphSOPNUMBE.Popis = "Pøíkaz";
            this.dfVphSOPNUMBE.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVphSOPNUMBE.PopisWidth = 80;
            this.dfVphSOPNUMBE.ReadOnly = true;
            this.dfVphSOPNUMBE.Size = new System.Drawing.Size(237, 19);
            this.dfVphSOPNUMBE.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 267);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(237, 71);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // FormOdvadeniPrehled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(237, 338);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormOdvadeniPrehled";
            this.Text = "Pøehled";
            this.Load += new System.EventHandler(this.FormOdvadeniPrehled_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdvadeniPrehled_KeyDown);
            this.panelComponents.ResumeLayout(false);
            this.panelLastOdvod.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelLastOdvod;
        private Fask.Graphic.DataField dfVphSOPNUMBE;
        private Fask.Graphic.DataField dfVphSOPDESC;
        private Fask.Graphic.DataField dfVppItemnmbr;
        private Fask.Graphic.DataField dfVppVnditnum;
        private Fask.Graphic.DataField dfVppItemdesc;
        private Fask.Graphic.DataField dfVppBarcodeP;
        private Fask.Graphic.DataField dfLastQuantity;
        private Fask.Graphic.DataField dfLastBarcodeP;
        private Fask.Graphic.DataField dfLastITEMDESC;
        private Fask.Graphic.DataField dfLastSOPNUMBER;
        private Fask.Graphic.DataField dfTimeStateActual;
        private Fask.Graphic.DataField dfTimeStateLast;
    }
}
