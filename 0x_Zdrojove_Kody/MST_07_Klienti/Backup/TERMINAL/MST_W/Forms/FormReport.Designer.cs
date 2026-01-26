namespace Fask.MST_W.Forms
{
    partial class FormReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReport));
            this.panelData = new System.Windows.Forms.Panel();
            this.df_PozadovanoOdberately = new Fask.Graphic.DataField();
            this.df_ZbyvaNaObjednavce = new Fask.Graphic.DataField();
            this.df_JizDodano = new Fask.Graphic.DataField();
            this.df_PozadovanoOdDodavatele = new Fask.Graphic.DataField();
            this.panelSkladExport = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.df_Expedice = new Fask.Graphic.DataField();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.df_SKLAD = new Fask.Graphic.DataField();
            this.df_CZ_CarKod = new Fask.Graphic.DataField();
            this.df_ITEMNMBR = new Fask.Graphic.DataField();
            this.df_DESC = new Fask.Graphic.DataField();
            this.panelBTN = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelData.SuspendLayout();
            this.panelSkladExport.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelBTN.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelData
            // 
            resources.ApplyResources(this.panelData, "panelData");
            this.panelData.Controls.Add(this.df_PozadovanoOdberately);
            this.panelData.Controls.Add(this.df_ZbyvaNaObjednavce);
            this.panelData.Controls.Add(this.df_JizDodano);
            this.panelData.Controls.Add(this.df_PozadovanoOdDodavatele);
            this.panelData.Controls.Add(this.panelSkladExport);
            this.panelData.Controls.Add(this.df_CZ_CarKod);
            this.panelData.Controls.Add(this.df_ITEMNMBR);
            this.panelData.Controls.Add(this.df_DESC);
            this.panelData.Name = "panelData";
            // 
            // df_PozadovanoOdberately
            // 
            this.df_PozadovanoOdberately.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_PozadovanoOdberately.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_PozadovanoOdberately.Data = "";
            this.df_PozadovanoOdberately.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_PozadovanoOdberately.DataMaxLength = 32767;
            this.df_PozadovanoOdberately.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.df_PozadovanoOdberately, "df_PozadovanoOdberately");
            this.df_PozadovanoOdberately.MultiLine = false;
            this.df_PozadovanoOdberately.Name = "df_PozadovanoOdberately";
            this.df_PozadovanoOdberately.Popis = "Požadováno odbìrateli:";
            this.df_PozadovanoOdberately.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_PozadovanoOdberately.PopisWidth = 140;
            this.df_PozadovanoOdberately.ReadOnly = true;
            this.df_PozadovanoOdberately.TabStop = false;
            // 
            // df_ZbyvaNaObjednavce
            // 
            this.df_ZbyvaNaObjednavce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_ZbyvaNaObjednavce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_ZbyvaNaObjednavce.Data = "";
            this.df_ZbyvaNaObjednavce.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_ZbyvaNaObjednavce.DataMaxLength = 32767;
            this.df_ZbyvaNaObjednavce.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.df_ZbyvaNaObjednavce, "df_ZbyvaNaObjednavce");
            this.df_ZbyvaNaObjednavce.MultiLine = true;
            this.df_ZbyvaNaObjednavce.Name = "df_ZbyvaNaObjednavce";
            this.df_ZbyvaNaObjednavce.Popis = "Zbývá na objednávce:";
            this.df_ZbyvaNaObjednavce.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_ZbyvaNaObjednavce.PopisWidth = 140;
            this.df_ZbyvaNaObjednavce.ReadOnly = true;
            this.df_ZbyvaNaObjednavce.TabStop = false;
            // 
            // df_JizDodano
            // 
            this.df_JizDodano.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_JizDodano.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_JizDodano.Data = "";
            this.df_JizDodano.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_JizDodano.DataMaxLength = 32767;
            this.df_JizDodano.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.df_JizDodano, "df_JizDodano");
            this.df_JizDodano.MultiLine = false;
            this.df_JizDodano.Name = "df_JizDodano";
            this.df_JizDodano.Popis = "Již dodáno:";
            this.df_JizDodano.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_JizDodano.PopisWidth = 140;
            this.df_JizDodano.ReadOnly = true;
            this.df_JizDodano.TabStop = false;
            // 
            // df_PozadovanoOdDodavatele
            // 
            this.df_PozadovanoOdDodavatele.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_PozadovanoOdDodavatele.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_PozadovanoOdDodavatele.Data = "";
            this.df_PozadovanoOdDodavatele.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_PozadovanoOdDodavatele.DataMaxLength = 32767;
            this.df_PozadovanoOdDodavatele.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.df_PozadovanoOdDodavatele, "df_PozadovanoOdDodavatele");
            this.df_PozadovanoOdDodavatele.MultiLine = false;
            this.df_PozadovanoOdDodavatele.Name = "df_PozadovanoOdDodavatele";
            this.df_PozadovanoOdDodavatele.Popis = "Požadovano od dodavatele:";
            this.df_PozadovanoOdDodavatele.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_PozadovanoOdDodavatele.PopisWidth = 140;
            this.df_PozadovanoOdDodavatele.ReadOnly = true;
            this.df_PozadovanoOdDodavatele.TabStop = false;
            // 
            // panelSkladExport
            // 
            this.panelSkladExport.Controls.Add(this.panelRight);
            this.panelSkladExport.Controls.Add(this.panelLeft);
            resources.ApplyResources(this.panelSkladExport, "panelSkladExport");
            this.panelSkladExport.Name = "panelSkladExport";
            this.panelSkladExport.Resize += new System.EventHandler(this.panelSkladExport_Resize);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.label2);
            this.panelRight.Controls.Add(this.df_Expedice);
            resources.ApplyResources(this.panelRight, "panelRight");
            this.panelRight.Name = "panelRight";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // df_Expedice
            // 
            this.df_Expedice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_Expedice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_Expedice.Data = "123456";
            this.df_Expedice.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_Expedice.DataMaxLength = 32767;
            this.df_Expedice.DataTextAlign = System.Drawing.ContentAlignment.TopCenter;
            resources.ApplyResources(this.df_Expedice, "df_Expedice");
            this.df_Expedice.ForeColor = System.Drawing.Color.Red;
            this.df_Expedice.MultiLine = false;
            this.df_Expedice.Name = "df_Expedice";
            this.df_Expedice.Popis = "";
            this.df_Expedice.PopisTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.df_Expedice.PopisWidth = 0;
            this.df_Expedice.ReadOnly = true;
            this.df_Expedice.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.df_SKLAD);
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Resize += new System.EventHandler(this.panelLeft_Resize);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // df_SKLAD
            // 
            this.df_SKLAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_SKLAD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_SKLAD.Data = "123456";
            this.df_SKLAD.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_SKLAD.DataMaxLength = 32767;
            this.df_SKLAD.DataTextAlign = System.Drawing.ContentAlignment.TopCenter;
            resources.ApplyResources(this.df_SKLAD, "df_SKLAD");
            this.df_SKLAD.ForeColor = System.Drawing.Color.Red;
            this.df_SKLAD.MultiLine = false;
            this.df_SKLAD.Name = "df_SKLAD";
            this.df_SKLAD.Popis = "";
            this.df_SKLAD.PopisTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.df_SKLAD.PopisWidth = 0;
            this.df_SKLAD.ReadOnly = true;
            this.df_SKLAD.TabStop = false;
            // 
            // df_CZ_CarKod
            // 
            this.df_CZ_CarKod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_CZ_CarKod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_CZ_CarKod.Data = "";
            this.df_CZ_CarKod.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_CZ_CarKod.DataMaxLength = 32767;
            this.df_CZ_CarKod.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.df_CZ_CarKod, "df_CZ_CarKod");
            this.df_CZ_CarKod.MultiLine = false;
            this.df_CZ_CarKod.Name = "df_CZ_CarKod";
            this.df_CZ_CarKod.Popis = " Èar. kod :";
            this.df_CZ_CarKod.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_CZ_CarKod.PopisWidth = 80;
            this.df_CZ_CarKod.ReadOnly = true;
            this.df_CZ_CarKod.TabStop = false;
            // 
            // df_ITEMNMBR
            // 
            this.df_ITEMNMBR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_ITEMNMBR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_ITEMNMBR.Data = "";
            this.df_ITEMNMBR.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_ITEMNMBR.DataMaxLength = 32767;
            this.df_ITEMNMBR.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.df_ITEMNMBR, "df_ITEMNMBR");
            this.df_ITEMNMBR.MultiLine = false;
            this.df_ITEMNMBR.Name = "df_ITEMNMBR";
            this.df_ITEMNMBR.Popis = "ID položky:";
            this.df_ITEMNMBR.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_ITEMNMBR.PopisWidth = 80;
            this.df_ITEMNMBR.ReadOnly = true;
            this.df_ITEMNMBR.TabStop = false;
            // 
            // df_DESC
            // 
            this.df_DESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_DESC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_DESC.Data = "";
            this.df_DESC.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_DESC.DataMaxLength = 32767;
            this.df_DESC.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.df_DESC, "df_DESC");
            this.df_DESC.MultiLine = false;
            this.df_DESC.Name = "df_DESC";
            this.df_DESC.Popis = "Popis položky:";
            this.df_DESC.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_DESC.PopisWidth = 80;
            this.df_DESC.ReadOnly = true;
            this.df_DESC.TabStop = false;
            // 
            // panelBTN
            // 
            this.panelBTN.Controls.Add(this.buttonStorno);
            this.panelBTN.Controls.Add(this.buttonOK);
            resources.ApplyResources(this.panelBTN, "panelBTN");
            this.panelBTN.Name = "panelBTN";
            // 
            // buttonStorno
            // 
            resources.ApplyResources(this.buttonStorno, "buttonStorno");
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelBTN);
            this.Name = "FormReport";
            this.Load += new System.EventHandler(this.ListNasnimForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormReport_Closing);
            this.Resize += new System.EventHandler(this.FormReport_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListNasnimForm_KeyDown);
            this.panelData.ResumeLayout(false);
            this.panelSkladExport.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelBTN.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataField df_PozadovanoOdberately;
        private Fask.Graphic.DataField df_ZbyvaNaObjednavce;
        private Fask.Graphic.DataField df_JizDodano;
        private Fask.Graphic.DataField df_PozadovanoOdDodavatele;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.Panel panelBTN;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private Fask.Graphic.DataField df_CZ_CarKod;
        private Fask.Graphic.DataField df_ITEMNMBR;
        private Fask.Graphic.DataField df_DESC;
        private Fask.Graphic.DataField df_SKLAD;
        private Fask.Graphic.DataField df_Expedice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelSkladExport;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;

    }
}