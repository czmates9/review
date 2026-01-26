namespace Fask.Vyroba_P
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
            this.panelbutton = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.panelLastOdvod = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dfTimeStateLast = new Fask.Graphic.DataField();
            this.dfLastSOPNUMBER = new Fask.Graphic.DataField();
            this.dfLastQuantity = new Fask.Graphic.DataField();
            this.dfLastITEMNMBR = new Fask.Graphic.DataField();
            this.dfLastBarcodeP = new Fask.Graphic.DataField();
            this.dfTimeStateActual = new Fask.Graphic.DataField();
            this.dfVppBarcodeP = new Fask.Graphic.DataField();
            this.dfVppVnditnum = new Fask.Graphic.DataField();
            this.dfVppItemdesc = new Fask.Graphic.DataField();
            this.dfVppItemnmbr = new Fask.Graphic.DataField();
            this.dfVphSOPDESC = new Fask.Graphic.DataField();
            this.dfVphSOPNUMBE = new Fask.Graphic.DataField();
            this.panelbutton.SuspendLayout();
            this.panelComponents.SuspendLayout();
            this.panelLastOdvod.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelbutton
            // 
            this.panelbutton.Controls.Add(this.buttonOK);
            this.panelbutton.Controls.Add(this.buttonStorno);
            this.panelbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelbutton.Location = new System.Drawing.Point(0, 561);
            this.panelbutton.Name = "panelbutton";
            this.panelbutton.Size = new System.Drawing.Size(844, 100);
            this.panelbutton.TabIndex = 0;
            this.panelbutton.TabStop = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(376, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(468, 100);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(376, 100);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.UseVisualStyleBackColor = true;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            this.buttonStorno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonStorno_KeyDown);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.panelLastOdvod);
            this.panelComponents.Controls.Add(this.panel2);
            this.panelComponents.Controls.Add(this.panel1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(844, 561);
            this.panelComponents.TabIndex = 1;
            // 
            // panelLastOdvod
            // 
            this.panelLastOdvod.Controls.Add(this.dfTimeStateLast);
            this.panelLastOdvod.Controls.Add(this.dfLastSOPNUMBER);
            this.panelLastOdvod.Controls.Add(this.dfLastQuantity);
            this.panelLastOdvod.Controls.Add(this.dfLastITEMNMBR);
            this.panelLastOdvod.Controls.Add(this.dfLastBarcodeP);
            this.panelLastOdvod.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLastOdvod.Location = new System.Drawing.Point(0, 360);
            this.panelLastOdvod.Name = "panelLastOdvod";
            this.panelLastOdvod.Size = new System.Drawing.Size(844, 255);
            this.panelLastOdvod.TabIndex = 0;
            this.panelLastOdvod.TabStop = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dfTimeStateActual);
            this.panel2.Controls.Add(this.dfVppBarcodeP);
            this.panel2.Controls.Add(this.dfVppVnditnum);
            this.panel2.Controls.Add(this.dfVppItemdesc);
            this.panel2.Controls.Add(this.dfVppItemnmbr);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(844, 255);
            this.panel2.TabIndex = 1;
            this.panel2.TabStop = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dfVphSOPDESC);
            this.panel1.Controls.Add(this.dfVphSOPNUMBE);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 105);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = true;
            // 
            // dfTimeStateLast
            // 
            this.dfTimeStateLast.BackColor = System.Drawing.SystemColors.Control;
            this.dfTimeStateLast.Data = "";
            this.dfTimeStateLast.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfTimeStateLast.DataMaxLength = 32767;
            this.dfTimeStateLast.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTimeStateLast.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfTimeStateLast.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfTimeStateLast.Location = new System.Drawing.Point(0, 200);
            this.dfTimeStateLast.MultiLine = false;
            this.dfTimeStateLast.Name = "dfTimeStateLast";
            this.dfTimeStateLast.Popis = "Stav";
            this.dfTimeStateLast.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTimeStateLast.PopisWidth = 200;
            this.dfTimeStateLast.ReadOnly = true;
            this.dfTimeStateLast.Size = new System.Drawing.Size(844, 50);
            this.dfTimeStateLast.TabIndex = 0;
            this.dfTimeStateLast.TabStop = false;
            // 
            // dfLastSOPNUMBER
            // 
            this.dfLastSOPNUMBER.BackColor = System.Drawing.SystemColors.Control;
            this.dfLastSOPNUMBER.Data = "";
            this.dfLastSOPNUMBER.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfLastSOPNUMBER.DataMaxLength = 32767;
            this.dfLastSOPNUMBER.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastSOPNUMBER.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfLastSOPNUMBER.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfLastSOPNUMBER.Location = new System.Drawing.Point(0, 150);
            this.dfLastSOPNUMBER.MultiLine = false;
            this.dfLastSOPNUMBER.Name = "dfLastSOPNUMBER";
            this.dfLastSOPNUMBER.Popis = "Příkaz před.";
            this.dfLastSOPNUMBER.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastSOPNUMBER.PopisWidth = 200;
            this.dfLastSOPNUMBER.ReadOnly = true;
            this.dfLastSOPNUMBER.Size = new System.Drawing.Size(844, 50);
            this.dfLastSOPNUMBER.TabIndex = 16;
            this.dfLastSOPNUMBER.TabStop = false;
            // 
            // dfLastQuantity
            // 
            this.dfLastQuantity.BackColor = System.Drawing.SystemColors.Control;
            this.dfLastQuantity.Data = "";
            this.dfLastQuantity.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfLastQuantity.DataMaxLength = 32767;
            this.dfLastQuantity.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastQuantity.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfLastQuantity.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfLastQuantity.Location = new System.Drawing.Point(0, 100);
            this.dfLastQuantity.MultiLine = false;
            this.dfLastQuantity.Name = "dfLastQuantity";
            this.dfLastQuantity.Popis = "Množství";
            this.dfLastQuantity.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastQuantity.PopisWidth = 200;
            this.dfLastQuantity.ReadOnly = true;
            this.dfLastQuantity.Size = new System.Drawing.Size(844, 50);
            this.dfLastQuantity.TabIndex = 19;
            this.dfLastQuantity.TabStop = false;
            // 
            // dfLastITEMNMBR
            // 
            this.dfLastITEMNMBR.BackColor = System.Drawing.SystemColors.Control;
            this.dfLastITEMNMBR.Data = "";
            this.dfLastITEMNMBR.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfLastITEMNMBR.DataMaxLength = 32767;
            this.dfLastITEMNMBR.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastITEMNMBR.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfLastITEMNMBR.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfLastITEMNMBR.Location = new System.Drawing.Point(0, 50);
            this.dfLastITEMNMBR.MultiLine = false;
            this.dfLastITEMNMBR.Name = "dfLastITEMNMBR";
            this.dfLastITEMNMBR.Popis = "Operace";
            this.dfLastITEMNMBR.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastITEMNMBR.PopisWidth = 200;
            this.dfLastITEMNMBR.ReadOnly = true;
            this.dfLastITEMNMBR.Size = new System.Drawing.Size(844, 50);
            this.dfLastITEMNMBR.TabIndex = 15;
            this.dfLastITEMNMBR.TabStop = false;
            // 
            // dfLastBarcodeP
            // 
            this.dfLastBarcodeP.BackColor = System.Drawing.SystemColors.Control;
            this.dfLastBarcodeP.Data = "";
            this.dfLastBarcodeP.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfLastBarcodeP.DataMaxLength = 32767;
            this.dfLastBarcodeP.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastBarcodeP.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfLastBarcodeP.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfLastBarcodeP.Location = new System.Drawing.Point(0, 0);
            this.dfLastBarcodeP.MultiLine = false;
            this.dfLastBarcodeP.Name = "dfLastBarcodeP";
            this.dfLastBarcodeP.Popis = "Číslo";
            this.dfLastBarcodeP.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfLastBarcodeP.PopisWidth = 200;
            this.dfLastBarcodeP.ReadOnly = true;
            this.dfLastBarcodeP.Size = new System.Drawing.Size(844, 50);
            this.dfLastBarcodeP.TabIndex = 18;
            this.dfLastBarcodeP.TabStop = false;
            // 
            // dfTimeStateActual
            // 
            this.dfTimeStateActual.BackColor = System.Drawing.SystemColors.Control;
            this.dfTimeStateActual.Data = "";
            this.dfTimeStateActual.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfTimeStateActual.DataMaxLength = 32767;
            this.dfTimeStateActual.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTimeStateActual.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfTimeStateActual.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfTimeStateActual.Location = new System.Drawing.Point(0, 200);
            this.dfTimeStateActual.MultiLine = false;
            this.dfTimeStateActual.Name = "dfTimeStateActual";
            this.dfTimeStateActual.Popis = "Stav";
            this.dfTimeStateActual.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTimeStateActual.PopisWidth = 200;
            this.dfTimeStateActual.ReadOnly = true;
            this.dfTimeStateActual.Size = new System.Drawing.Size(844, 50);
            this.dfTimeStateActual.TabIndex = 15;
            this.dfTimeStateActual.TabStop = false;
            // 
            // dfVppBarcodeP
            // 
            this.dfVppBarcodeP.BackColor = System.Drawing.SystemColors.Control;
            this.dfVppBarcodeP.Data = "";
            this.dfVppBarcodeP.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVppBarcodeP.DataMaxLength = 32767;
            this.dfVppBarcodeP.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppBarcodeP.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVppBarcodeP.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfVppBarcodeP.Location = new System.Drawing.Point(0, 150);
            this.dfVppBarcodeP.MultiLine = false;
            this.dfVppBarcodeP.Name = "dfVppBarcodeP";
            this.dfVppBarcodeP.Popis = "Číslo";
            this.dfVppBarcodeP.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppBarcodeP.PopisWidth = 200;
            this.dfVppBarcodeP.ReadOnly = true;
            this.dfVppBarcodeP.Size = new System.Drawing.Size(844, 50);
            this.dfVppBarcodeP.TabIndex = 6;
            this.dfVppBarcodeP.TabStop = false;
            // 
            // dfVppVnditnum
            // 
            this.dfVppVnditnum.BackColor = System.Drawing.SystemColors.Control;
            this.dfVppVnditnum.Data = "";
            this.dfVppVnditnum.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVppVnditnum.DataMaxLength = 32767;
            this.dfVppVnditnum.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppVnditnum.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVppVnditnum.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfVppVnditnum.Location = new System.Drawing.Point(0, 100);
            this.dfVppVnditnum.MultiLine = false;
            this.dfVppVnditnum.Name = "dfVppVnditnum";
            this.dfVppVnditnum.Popis = "Čár. kód";
            this.dfVppVnditnum.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppVnditnum.PopisWidth = 200;
            this.dfVppVnditnum.ReadOnly = true;
            this.dfVppVnditnum.Size = new System.Drawing.Size(844, 50);
            this.dfVppVnditnum.TabIndex = 5;
            this.dfVppVnditnum.TabStop = false;
            // 
            // dfVppItemdesc
            // 
            this.dfVppItemdesc.BackColor = System.Drawing.SystemColors.Control;
            this.dfVppItemdesc.Data = "";
            this.dfVppItemdesc.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVppItemdesc.DataMaxLength = 32767;
            this.dfVppItemdesc.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppItemdesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVppItemdesc.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfVppItemdesc.Location = new System.Drawing.Point(0, 50);
            this.dfVppItemdesc.MultiLine = false;
            this.dfVppItemdesc.Name = "dfVppItemdesc";
            this.dfVppItemdesc.Popis = "Název";
            this.dfVppItemdesc.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppItemdesc.PopisWidth = 200;
            this.dfVppItemdesc.ReadOnly = true;
            this.dfVppItemdesc.Size = new System.Drawing.Size(844, 50);
            this.dfVppItemdesc.TabIndex = 4;
            this.dfVppItemdesc.TabStop = false;
            // 
            // dfVppItemnmbr
            // 
            this.dfVppItemnmbr.BackColor = System.Drawing.SystemColors.Control;
            this.dfVppItemnmbr.Data = "";
            this.dfVppItemnmbr.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVppItemnmbr.DataMaxLength = 32767;
            this.dfVppItemnmbr.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppItemnmbr.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVppItemnmbr.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfVppItemnmbr.Location = new System.Drawing.Point(0, 0);
            this.dfVppItemnmbr.MultiLine = false;
            this.dfVppItemnmbr.Name = "dfVppItemnmbr";
            this.dfVppItemnmbr.Popis = "Operace";
            this.dfVppItemnmbr.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVppItemnmbr.PopisWidth = 200;
            this.dfVppItemnmbr.ReadOnly = true;
            this.dfVppItemnmbr.Size = new System.Drawing.Size(844, 50);
            this.dfVppItemnmbr.TabIndex = 3;
            this.dfVppItemnmbr.TabStop = false;
            // 
            // dfVphSOPDESC
            // 
            this.dfVphSOPDESC.BackColor = System.Drawing.SystemColors.Control;
            this.dfVphSOPDESC.Data = "";
            this.dfVphSOPDESC.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVphSOPDESC.DataMaxLength = 32767;
            this.dfVphSOPDESC.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVphSOPDESC.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVphSOPDESC.Font = new System.Drawing.Font("Tahoma", 18F);
            this.dfVphSOPDESC.Location = new System.Drawing.Point(0, 50);
            this.dfVphSOPDESC.MultiLine = false;
            this.dfVphSOPDESC.Name = "dfVphSOPDESC";
            this.dfVphSOPDESC.Popis = "Popis";
            this.dfVphSOPDESC.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVphSOPDESC.PopisWidth = 200;
            this.dfVphSOPDESC.ReadOnly = true;
            this.dfVphSOPDESC.Size = new System.Drawing.Size(844, 50);
            this.dfVphSOPDESC.TabIndex = 1;
            this.dfVphSOPDESC.TabStop = false;
            // 
            // dfVphSOPNUMBE
            // 
            this.dfVphSOPNUMBE.BackColor = System.Drawing.SystemColors.Control;
            this.dfVphSOPNUMBE.Data = "";
            this.dfVphSOPNUMBE.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfVphSOPNUMBE.DataMaxLength = 32767;
            this.dfVphSOPNUMBE.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVphSOPNUMBE.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfVphSOPNUMBE.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dfVphSOPNUMBE.Location = new System.Drawing.Point(0, 0);
            this.dfVphSOPNUMBE.MultiLine = false;
            this.dfVphSOPNUMBE.Name = "dfVphSOPNUMBE";
            this.dfVphSOPNUMBE.Popis = "Příkaz";
            this.dfVphSOPNUMBE.PopisTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfVphSOPNUMBE.PopisWidth = 200;
            this.dfVphSOPNUMBE.ReadOnly = true;
            this.dfVphSOPNUMBE.Size = new System.Drawing.Size(844, 50);
            this.dfVphSOPNUMBE.TabIndex = 0;
            this.dfVphSOPNUMBE.TabStop = false;
            // 
            // FormOdvadeniPrehled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(844, 661);
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelbutton);
            this.Name = "FormOdvadeniPrehled";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormOdvadeniPrehled";
            this.Load += new System.EventHandler(this.FormOdvadeniPrehled_Load);
            this.panelbutton.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            this.panelLastOdvod.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelbutton;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Panel panelComponents;
        private System.Windows.Forms.Panel panelLastOdvod;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private Fask.Graphic.DataField dfVphSOPNUMBE;
        private Fask.Graphic.DataField dfVppBarcodeP;
        private Fask.Graphic.DataField dfVppVnditnum;
        private Fask.Graphic.DataField dfVppItemdesc;
        private Fask.Graphic.DataField dfVppItemnmbr;
        private Fask.Graphic.DataField dfVphSOPDESC;
        private Fask.Graphic.DataField dfTimeStateLast;
        private Fask.Graphic.DataField dfLastSOPNUMBER;
        private Fask.Graphic.DataField dfLastQuantity;
        private Fask.Graphic.DataField dfLastITEMNMBR;
        private Fask.Graphic.DataField dfLastBarcodeP;
        private Fask.Graphic.DataField dfTimeStateActual;


    }
}