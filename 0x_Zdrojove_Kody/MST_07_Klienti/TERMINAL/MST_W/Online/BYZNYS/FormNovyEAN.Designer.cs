using Fask.MST_W.Online.BYZNYS.DatabaseOnlineTableAdapters;
namespace Fask.MST_W.Online.BYZNYS
{
    partial class FormNovyEAN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_prepocet1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_prepocet2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.jEDNOTKYBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.databaseOnlinePrijem = new Fask.MST_W.Online.BYZNYS.DatabaseOnline();
            this.cmb_mj = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_newean = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_klicma = new System.Windows.Forms.Label();
            this.lbl_nazevmat = new System.Windows.Forms.Label();
            this.jEDNOTKYTableAdapter = new Fask.MST_W.Online.BYZNYS.DatabaseOnlineTableAdapters.JEDNOTKYTableAdapter();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.gb_ulozit = new Fask.Graphic.GraphicButton();
            this.gb_zpet = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.jEDNOTKYBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseOnlinePrijem)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Zpět";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Uložit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(8, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.Text = "Přepočet 1 :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(8, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.Text = "Přepočet 2 :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_prepocet1
            // 
            this.txt_prepocet1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.txt_prepocet1.Location = new System.Drawing.Point(92, 93);
            this.txt_prepocet1.Name = "txt_prepocet1";
            this.txt_prepocet1.Size = new System.Drawing.Size(65, 19);
            this.txt_prepocet1.TabIndex = 0;
            this.txt_prepocet1.Text = "1";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(163, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.Text = "(Jmenovatel)";
            // 
            // txt_prepocet2
            // 
            this.txt_prepocet2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.txt_prepocet2.Location = new System.Drawing.Point(92, 113);
            this.txt_prepocet2.Name = "txt_prepocet2";
            this.txt_prepocet2.Size = new System.Drawing.Size(65, 19);
            this.txt_prepocet2.TabIndex = 1;
            this.txt_prepocet2.Text = "1";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(163, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.Text = "(Balení)";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(8, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 15);
            this.label5.Text = "MJ :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // jEDNOTKYBindingSource
            // 
            this.jEDNOTKYBindingSource.AllowNew = false;
            this.jEDNOTKYBindingSource.DataMember = "JEDNOTKY";
            this.jEDNOTKYBindingSource.DataSource = this.databaseOnlinePrijem;
            // 
            // databaseOnlinePrijem
            // 
            this.databaseOnlinePrijem.DataSetName = "DatabaseOnlinePrijem";
            this.databaseOnlinePrijem.Locale = new System.Globalization.CultureInfo("");
            this.databaseOnlinePrijem.Prefix = "";
            this.databaseOnlinePrijem.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cmb_mj
            // 
            this.cmb_mj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_mj.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
            this.cmb_mj.Items.Add("kg: kilogramy");
            this.cmb_mj.Location = new System.Drawing.Point(92, 133);
            this.cmb_mj.Name = "cmb_mj";
            this.cmb_mj.Size = new System.Drawing.Size(142, 20);
            this.cmb_mj.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.Text = "Nový EAN : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl_newean
            // 
            this.lbl_newean.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_newean.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lbl_newean.Location = new System.Drawing.Point(92, 0);
            this.lbl_newean.Name = "lbl_newean";
            this.lbl_newean.Size = new System.Drawing.Size(142, 15);
            this.lbl_newean.Text = "\"novy_ean\"";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(4, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 15);
            this.label7.Text = "Zboží :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl_klicma
            // 
            this.lbl_klicma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_klicma.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lbl_klicma.Location = new System.Drawing.Point(91, 20);
            this.lbl_klicma.Name = "lbl_klicma";
            this.lbl_klicma.Size = new System.Drawing.Size(143, 15);
            this.lbl_klicma.Text = "\"klic_ma\"";
            // 
            // lbl_nazevmat
            // 
            this.lbl_nazevmat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_nazevmat.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lbl_nazevmat.Location = new System.Drawing.Point(19, 40);
            this.lbl_nazevmat.Name = "lbl_nazevmat";
            this.lbl_nazevmat.Size = new System.Drawing.Size(202, 50);
            this.lbl_nazevmat.Text = "\"nazev_mat\"";
            // 
            // jEDNOTKYTableAdapter
            // 
            this.jEDNOTKYTableAdapter.ClearBeforeFill = true;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.gb_ulozit);
            this.panelButtons.Controls.Add(this.gb_zpet);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 248);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(238, 37);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // gb_ulozit
            // 
            this.gb_ulozit.BitmapNormal = null;
            this.gb_ulozit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_ulozit.FocusMargin = 5;
            this.gb_ulozit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gb_ulozit.Location = new System.Drawing.Point(109, 0);
            this.gb_ulozit.Name = "gb_ulozit";
            this.gb_ulozit.Pressed = false;
            this.gb_ulozit.Size = new System.Drawing.Size(129, 37);
            this.gb_ulozit.TabIndex = 1;
            this.gb_ulozit.Text = "Uložit";
            this.gb_ulozit.Transparent = System.Drawing.Color.White;
            this.gb_ulozit.Click += new System.EventHandler(this.gb_ulozit_Click);
            // 
            // gb_zpet
            // 
            this.gb_zpet.BitmapNormal = null;
            this.gb_zpet.Dock = System.Windows.Forms.DockStyle.Left;
            this.gb_zpet.FocusMargin = 5;
            this.gb_zpet.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gb_zpet.Location = new System.Drawing.Point(0, 0);
            this.gb_zpet.Name = "gb_zpet";
            this.gb_zpet.Pressed = false;
            this.gb_zpet.Size = new System.Drawing.Size(109, 37);
            this.gb_zpet.TabIndex = 0;
            this.gb_zpet.Text = "Zpět";
            this.gb_zpet.Transparent = System.Drawing.Color.White;
            this.gb_zpet.Click += new System.EventHandler(this.gb_zpet_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl_nazevmat);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_klicma);
            this.panel1.Controls.Add(this.txt_prepocet1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl_newean);
            this.panel1.Controls.Add(this.txt_prepocet2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmb_mj);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 159);
            // 
            // FormNovyEAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 285);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FormNovyEAN";
            this.Text = "Nový EAN zadání";
            this.Load += new System.EventHandler(this.FormNovyEAN_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormNovyEAN_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.jEDNOTKYBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseOnlinePrijem)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_prepocet1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_prepocet2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_mj;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_newean;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_klicma;
        private System.Windows.Forms.Label lbl_nazevmat;
        private DatabaseOnline databaseOnlinePrijem;
        private System.Windows.Forms.BindingSource jEDNOTKYBindingSource;
        private JEDNOTKYTableAdapter jEDNOTKYTableAdapter;
        private System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton gb_ulozit;
        private Fask.Graphic.GraphicButton gb_zpet;
        private System.Windows.Forms.Panel panel1;
    }
}