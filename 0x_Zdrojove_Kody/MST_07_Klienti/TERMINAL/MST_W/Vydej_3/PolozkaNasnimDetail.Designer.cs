namespace Fask.MST_W.Vydej_3
{
    partial class PolozkaNasnimDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolozkaNasnimDetail));
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sklad_l = new Fask.Graphic.DataField();
            this.sn_l = new Fask.Graphic.DataField();
            this.ItemNmbr_l = new Fask.Graphic.DataField();
            this.ItemDesc_l = new Fask.Graphic.DataField();
            this.baleni_l = new Fask.Graphic.DataField();
            this.CZ_CarKod_l = new Fask.Graphic.DataField();
            this.nacteno_l = new Fask.Graphic.DataField();
            this.lokace_l = new Fask.Graphic.DataField();
            this.panel1.SuspendLayout();
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
            this.panel1.Controls.Add(this.sn_l);
            this.panel1.Controls.Add(this.sklad_l);
            this.panel1.Controls.Add(this.ItemNmbr_l);
            this.panel1.Controls.Add(this.ItemDesc_l);
            this.panel1.Controls.Add(this.baleni_l);
            this.panel1.Controls.Add(this.CZ_CarKod_l);
            this.panel1.Controls.Add(this.nacteno_l);
            this.panel1.Controls.Add(this.lokace_l);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
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
            // sn_l
            // 
            this.sn_l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.sn_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sn_l.Data = "";
            this.sn_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.sn_l.DataMaxLength = 32767;
            this.sn_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            resources.ApplyResources(this.sn_l, "sn_l");
            this.sn_l.MultiLine = false;
            this.sn_l.Name = "sn_l";
            this.sn_l.Popis = "SN";
            this.sn_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.sn_l.PopisWidth = 45;
            this.sn_l.ReadOnly = true;
            this.sn_l.TabStop = false;
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
            // PolozkaNasnimDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ok_but);
            this.Name = "PolozkaNasnimDetail";
            this.Load += new System.EventHandler(this.PolozkaNasnimDetail_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PolozkaNasnimDetail_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton ok_but;
        private Fask.Graphic.DataField baleni_l;
        private Fask.Graphic.DataField nacteno_l;
        private Fask.Graphic.DataField lokace_l;
        private Fask.Graphic.DataField sn_l;
        private Fask.Graphic.DataField sklad_l;
        private System.Windows.Forms.Panel panel1;
        private Fask.Graphic.DataField ItemNmbr_l;
        private Fask.Graphic.DataField ItemDesc_l;
        private Fask.Graphic.DataField CZ_CarKod_l;

    }
}