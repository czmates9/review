namespace Fask.MST_W.Vydej_3
{
    partial class PolozkaDetailForm3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolozkaDetailForm3));
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.df_sscccode = new Fask.Graphic.DataField();
            this.snimatSW_l = new Fask.Graphic.DataField();
            this.datumnasklade_l = new Fask.Graphic.DataField();
            this.snimatDV_l = new Fask.Graphic.DataField();
            this.nasklade_l = new Fask.Graphic.DataField();
            this.sklad_l = new Fask.Graphic.DataField();
            this.snimatSN_l = new Fask.Graphic.DataField();
            this.davka_l = new Fask.Graphic.DataField();
            this.lbl_note = new Fask.Graphic.DataField();
            this.ItemNmbr_l = new Fask.Graphic.DataField();
            this.ItemDesc_l = new Fask.Graphic.DataField();
            this.baleni_l = new Fask.Graphic.DataField();
            this.CZ_CarKod_l = new Fask.Graphic.DataField();
            this.nacist_l = new Fask.Graphic.DataField();
            this.nacteno_l = new Fask.Graphic.DataField();
            this.lokace_l = new Fask.Graphic.DataField();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok_but
            // 
            this.ok_but.BitmapNormal = null;
            resources.ApplyResources(this.ok_but, "ok_but");
            this.ok_but.FocusMargin = 5;
            this.ok_but.Name = "ok_but";
            this.ok_but.Pressed = false;
            this.ok_but.Transparent = System.Drawing.Color.White;
            this.ok_but.Click += new System.EventHandler(this.hlavniMenu_but_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.ItemNmbr_l);
            this.panel1.Controls.Add(this.ItemDesc_l);
            this.panel1.Controls.Add(this.baleni_l);
            this.panel1.Controls.Add(this.CZ_CarKod_l);
            this.panel1.Controls.Add(this.nacist_l);
            this.panel1.Controls.Add(this.nacteno_l);
            this.panel1.Controls.Add(this.lokace_l);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.df_sscccode);
            this.panel2.Controls.Add(this.snimatSW_l);
            this.panel2.Controls.Add(this.datumnasklade_l);
            this.panel2.Controls.Add(this.snimatDV_l);
            this.panel2.Controls.Add(this.nasklade_l);
            this.panel2.Controls.Add(this.sklad_l);
            this.panel2.Controls.Add(this.snimatSN_l);
            this.panel2.Controls.Add(this.davka_l);
            this.panel2.Controls.Add(this.lbl_note);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // df_sscccode
            // 
            resources.ApplyResources(this.df_sscccode, "df_sscccode");
            this.df_sscccode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_sscccode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_sscccode.Data = "";
            this.df_sscccode.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_sscccode.DataMaxLength = 32767;
            this.df_sscccode.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_sscccode.MultiLine = false;
            this.df_sscccode.Name = "df_sscccode";
            this.df_sscccode.Popis = "SSCC";
            this.df_sscccode.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_sscccode.PopisWidth = 40;
            this.df_sscccode.ReadOnly = true;
            this.df_sscccode.TabStop = false;
            // 
            // snimatSW_l
            // 
            this.snimatSW_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.snimatSW_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.snimatSW_l.Data = "";
            this.snimatSW_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.snimatSW_l.DataMaxLength = 32767;
            this.snimatSW_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.snimatSW_l, "snimatSW_l");
            this.snimatSW_l.MultiLine = false;
            this.snimatSW_l.Name = "snimatSW_l";
            this.snimatSW_l.Popis = "SW";
            this.snimatSW_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.snimatSW_l.PopisWidth = 30;
            this.snimatSW_l.ReadOnly = true;
            this.snimatSW_l.TabStop = false;
            // 
            // datumnasklade_l
            // 
            resources.ApplyResources(this.datumnasklade_l, "datumnasklade_l");
            this.datumnasklade_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.datumnasklade_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.datumnasklade_l.Data = "";
            this.datumnasklade_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.datumnasklade_l.DataMaxLength = 32767;
            this.datumnasklade_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.datumnasklade_l.MultiLine = false;
            this.datumnasklade_l.Name = "datumnasklade_l";
            this.datumnasklade_l.Popis = "Datum";
            this.datumnasklade_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.datumnasklade_l.PopisWidth = 45;
            this.datumnasklade_l.ReadOnly = true;
            this.datumnasklade_l.TabStop = false;
            // 
            // snimatDV_l
            // 
            resources.ApplyResources(this.snimatDV_l, "snimatDV_l");
            this.snimatDV_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.snimatDV_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.snimatDV_l.Data = "";
            this.snimatDV_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.snimatDV_l.DataMaxLength = 32767;
            this.snimatDV_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.snimatDV_l.MultiLine = false;
            this.snimatDV_l.Name = "snimatDV_l";
            this.snimatDV_l.Popis = "DV";
            this.snimatDV_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.snimatDV_l.PopisWidth = 30;
            this.snimatDV_l.ReadOnly = true;
            this.snimatDV_l.TabStop = false;
            // 
            // nasklade_l
            // 
            resources.ApplyResources(this.nasklade_l, "nasklade_l");
            this.nasklade_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.nasklade_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nasklade_l.Data = "";
            this.nasklade_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.nasklade_l.DataMaxLength = 32767;
            this.nasklade_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.nasklade_l.MultiLine = false;
            this.nasklade_l.Name = "nasklade_l";
            this.nasklade_l.Popis = "Kusù";
            this.nasklade_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.nasklade_l.PopisWidth = 45;
            this.nasklade_l.ReadOnly = true;
            this.nasklade_l.TabStop = false;
            // 
            // sklad_l
            // 
            this.sklad_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.sklad_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sklad_l.Data = "";
            this.sklad_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.sklad_l.DataMaxLength = 32767;
            this.sklad_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.sklad_l, "sklad_l");
            this.sklad_l.MultiLine = false;
            this.sklad_l.Name = "sklad_l";
            this.sklad_l.Popis = "Sklad";
            this.sklad_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.sklad_l.PopisWidth = 45;
            this.sklad_l.ReadOnly = true;
            this.sklad_l.TabStop = false;
            // 
            // snimatSN_l
            // 
            this.snimatSN_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.snimatSN_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.snimatSN_l.Data = "";
            this.snimatSN_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.snimatSN_l.DataMaxLength = 32767;
            this.snimatSN_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.snimatSN_l, "snimatSN_l");
            this.snimatSN_l.MultiLine = false;
            this.snimatSN_l.Name = "snimatSN_l";
            this.snimatSN_l.Popis = "SN";
            this.snimatSN_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.snimatSN_l.PopisWidth = 30;
            this.snimatSN_l.ReadOnly = true;
            this.snimatSN_l.TabStop = false;
            // 
            // davka_l
            // 
            this.davka_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.davka_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.davka_l.Data = "";
            this.davka_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.davka_l.DataMaxLength = 32767;
            this.davka_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.davka_l, "davka_l");
            this.davka_l.MultiLine = false;
            this.davka_l.Name = "davka_l";
            this.davka_l.Popis = "Dávka";
            this.davka_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.davka_l.PopisWidth = 45;
            this.davka_l.ReadOnly = true;
            this.davka_l.TabStop = false;
            // 
            // lbl_note
            // 
            resources.ApplyResources(this.lbl_note, "lbl_note");
            this.lbl_note.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbl_note.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_note.Data = "";
            this.lbl_note.DataBackColor = System.Drawing.SystemColors.Window;
            this.lbl_note.DataMaxLength = 32767;
            this.lbl_note.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbl_note.MultiLine = false;
            this.lbl_note.Name = "lbl_note";
            this.lbl_note.Popis = "Poznámka";
            this.lbl_note.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbl_note.PopisWidth = 100;
            this.lbl_note.ReadOnly = true;
            this.lbl_note.TabStop = false;
            // 
            // ItemNmbr_l
            // 
            this.ItemNmbr_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ItemNmbr_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemNmbr_l.Data = "";
            this.ItemNmbr_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.ItemNmbr_l.DataMaxLength = 32767;
            this.ItemNmbr_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.ItemNmbr_l, "ItemNmbr_l");
            this.ItemNmbr_l.MultiLine = false;
            this.ItemNmbr_l.Name = "ItemNmbr_l";
            this.ItemNmbr_l.Popis = "Èíslo položky";
            this.ItemNmbr_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ItemNmbr_l.PopisWidth = 100;
            this.ItemNmbr_l.ReadOnly = true;
            this.ItemNmbr_l.TabStop = false;
            // 
            // ItemDesc_l
            // 
            this.ItemDesc_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ItemDesc_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemDesc_l.Data = "";
            this.ItemDesc_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.ItemDesc_l.DataMaxLength = 32767;
            this.ItemDesc_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.ItemDesc_l, "ItemDesc_l");
            this.ItemDesc_l.MultiLine = false;
            this.ItemDesc_l.Name = "ItemDesc_l";
            this.ItemDesc_l.Popis = "Název";
            this.ItemDesc_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ItemDesc_l.PopisWidth = 100;
            this.ItemDesc_l.ReadOnly = true;
            this.ItemDesc_l.TabStop = false;
            // 
            // baleni_l
            // 
            this.baleni_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.baleni_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.baleni_l.Data = "";
            this.baleni_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.baleni_l.DataMaxLength = 32767;
            this.baleni_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.baleni_l, "baleni_l");
            this.baleni_l.MultiLine = false;
            this.baleni_l.Name = "baleni_l";
            this.baleni_l.Popis = "Balení";
            this.baleni_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.baleni_l.PopisWidth = 100;
            this.baleni_l.ReadOnly = true;
            this.baleni_l.TabStop = false;
            // 
            // CZ_CarKod_l
            // 
            this.CZ_CarKod_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.CZ_CarKod_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CZ_CarKod_l.Data = "";
            this.CZ_CarKod_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.CZ_CarKod_l.DataMaxLength = 32767;
            this.CZ_CarKod_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.CZ_CarKod_l, "CZ_CarKod_l");
            this.CZ_CarKod_l.MultiLine = false;
            this.CZ_CarKod_l.Name = "CZ_CarKod_l";
            this.CZ_CarKod_l.Popis = "Èárový kód";
            this.CZ_CarKod_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.CZ_CarKod_l.PopisWidth = 100;
            this.CZ_CarKod_l.ReadOnly = true;
            this.CZ_CarKod_l.TabStop = false;
            // 
            // nacist_l
            // 
            this.nacist_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.nacist_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nacist_l.Data = "";
            this.nacist_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.nacist_l.DataMaxLength = 32767;
            this.nacist_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.nacist_l, "nacist_l");
            this.nacist_l.MultiLine = false;
            this.nacist_l.Name = "nacist_l";
            this.nacist_l.Popis = "Naèíst";
            this.nacist_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.nacist_l.PopisWidth = 100;
            this.nacist_l.ReadOnly = true;
            this.nacist_l.TabStop = false;
            // 
            // nacteno_l
            // 
            this.nacteno_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.nacteno_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nacteno_l.Data = "";
            this.nacteno_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.nacteno_l.DataMaxLength = 32767;
            this.nacteno_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.nacteno_l, "nacteno_l");
            this.nacteno_l.MultiLine = false;
            this.nacteno_l.Name = "nacteno_l";
            this.nacteno_l.Popis = "Naèteno";
            this.nacteno_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.nacteno_l.PopisWidth = 100;
            this.nacteno_l.ReadOnly = true;
            this.nacteno_l.TabStop = false;
            // 
            // lokace_l
            // 
            this.lokace_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lokace_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lokace_l.Data = "";
            this.lokace_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.lokace_l.DataMaxLength = 32767;
            this.lokace_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.lokace_l, "lokace_l");
            this.lokace_l.MultiLine = false;
            this.lokace_l.Name = "lokace_l";
            this.lokace_l.Popis = "Lokace";
            this.lokace_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lokace_l.PopisWidth = 100;
            this.lokace_l.ReadOnly = true;
            this.lokace_l.TabStop = false;
            // 
            // PolozkaDetailForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ok_but);
            this.Name = "PolozkaDetailForm3";
            this.Load += new System.EventHandler(this.PolozkaDetailForm3_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PolozkaDetailForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton ok_but;
        private Fask.Graphic.DataField snimatDV_l;
        private Fask.Graphic.DataField snimatSN_l;
        private Fask.Graphic.DataField baleni_l;
        private Fask.Graphic.DataField nacist_l;
        private Fask.Graphic.DataField nacteno_l;
        private Fask.Graphic.DataField snimatSW_l;
        private Fask.Graphic.DataField lokace_l;
        private Fask.Graphic.DataField davka_l;
        private Fask.Graphic.DataField sklad_l;
        private Fask.Graphic.DataField nasklade_l;
        private Fask.Graphic.DataField lbl_note;
        private Fask.Graphic.DataField datumnasklade_l;
        private System.Windows.Forms.Panel panel1;
        private Fask.Graphic.DataField ItemNmbr_l;
        private Fask.Graphic.DataField ItemDesc_l;
        private Fask.Graphic.DataField CZ_CarKod_l;
        private System.Windows.Forms.Panel panel2;
        private Fask.Graphic.DataField df_sscccode;

    }
}