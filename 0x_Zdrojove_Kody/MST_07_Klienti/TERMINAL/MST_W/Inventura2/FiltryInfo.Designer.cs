namespace Fask.MST_W.Inventura2
{
    partial class FiltryInfo
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemKonec = new System.Windows.Forms.MenuItem();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.df_STRED = new Fask.Graphic.DataField();
            this.df_OSOBA = new Fask.Graphic.DataField();
            this.df_KANCL = new Fask.Graphic.DataField();
            this.df_LOKACE = new Fask.Graphic.DataField();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemKonec);
            // 
            // menuItemKonec
            // 
            this.menuItemKonec.Text = "Konec";
            this.menuItemKonec.Click += new System.EventHandler(this.menuItemKonec_Click);
            // 
            // panelDetail
            // 
            this.panelDetail.AutoScroll = true;
            this.panelDetail.Controls.Add(this.df_STRED);
            this.panelDetail.Controls.Add(this.df_OSOBA);
            this.panelDetail.Controls.Add(this.df_KANCL);
            this.panelDetail.Controls.Add(this.df_LOKACE);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(0, 0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(374, 304);
            // 
            // df_STRED
            // 
            this.df_STRED.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.df_STRED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_STRED.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_STRED.Data = "";
            this.df_STRED.DataBackColor = System.Drawing.Color.White;
            this.df_STRED.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_STRED.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.df_STRED.Location = new System.Drawing.Point(3, 109);
            this.df_STRED.MultiLine = false;
            this.df_STRED.Name = "df_STRED";
            this.df_STRED.Popis = "Středisko";
            this.df_STRED.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_STRED.PopisWidth = 50;
            this.df_STRED.ReadOnly = true;
            this.df_STRED.Size = new System.Drawing.Size(368, 53);
            this.df_STRED.TabIndex = 54;
            this.df_STRED.TabStop = false;
            // 
            // df_OSOBA
            // 
            this.df_OSOBA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.df_OSOBA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_OSOBA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_OSOBA.Data = "";
            this.df_OSOBA.DataBackColor = System.Drawing.Color.White;
            this.df_OSOBA.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_OSOBA.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.df_OSOBA.Location = new System.Drawing.Point(3, 162);
            this.df_OSOBA.MultiLine = false;
            this.df_OSOBA.Name = "df_OSOBA";
            this.df_OSOBA.Popis = "Osoba";
            this.df_OSOBA.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_OSOBA.PopisWidth = 50;
            this.df_OSOBA.ReadOnly = true;
            this.df_OSOBA.Size = new System.Drawing.Size(368, 53);
            this.df_OSOBA.TabIndex = 53;
            this.df_OSOBA.TabStop = false;
            // 
            // df_KANCL
            // 
            this.df_KANCL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.df_KANCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_KANCL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_KANCL.Data = "";
            this.df_KANCL.DataBackColor = System.Drawing.Color.White;
            this.df_KANCL.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_KANCL.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.df_KANCL.Location = new System.Drawing.Point(3, 56);
            this.df_KANCL.MultiLine = false;
            this.df_KANCL.Name = "df_KANCL";
            this.df_KANCL.Popis = "Kancelář";
            this.df_KANCL.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_KANCL.PopisWidth = 50;
            this.df_KANCL.ReadOnly = true;
            this.df_KANCL.Size = new System.Drawing.Size(368, 53);
            this.df_KANCL.TabIndex = 45;
            this.df_KANCL.TabStop = false;
            // 
            // df_LOKACE
            // 
            this.df_LOKACE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.df_LOKACE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.df_LOKACE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.df_LOKACE.Data = "";
            this.df_LOKACE.DataBackColor = System.Drawing.Color.White;
            this.df_LOKACE.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_LOKACE.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.df_LOKACE.Location = new System.Drawing.Point(3, 3);
            this.df_LOKACE.MultiLine = false;
            this.df_LOKACE.Name = "df_LOKACE";
            this.df_LOKACE.Popis = "Lokace";
            this.df_LOKACE.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_LOKACE.PopisWidth = 50;
            this.df_LOKACE.ReadOnly = true;
            this.df_LOKACE.Size = new System.Drawing.Size(368, 53);
            this.df_LOKACE.TabIndex = 49;
            this.df_LOKACE.TabStop = false;
            // 
            // FiltryInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(374, 304);
            this.ControlBox = false;
            this.Controls.Add(this.panelDetail);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FiltryInfo";
            this.Text = "Aktuální filtry";
            this.Load += new System.EventHandler(this.FiltryInfo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nasnimane_KeyDown);
            this.panelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemKonec;
        private System.Windows.Forms.Panel panelDetail;
        private Fask.Graphic.DataField df_KANCL;
        private Fask.Graphic.DataField df_LOKACE;
        private Fask.Graphic.DataField df_OSOBA;
        private Fask.Graphic.DataField df_STRED;
    }
}