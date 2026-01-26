namespace Fask.MST_W.Informations
{
    partial class OnLinePocetKusuSklad
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
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.lLocncode = new Fask.Graphic.DataField();
            this.lItemdesc = new Fask.Graphic.DataField();
            this.lItemnmbr = new Fask.Graphic.DataField();
            this.lMnozstvi = new Fask.Graphic.DataField();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.BitmapNormal = null;
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonOK.FocusMargin = 5;
            this.buttonOK.Location = new System.Drawing.Point(0, 255);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Pressed = false;
            this.buttonOK.Size = new System.Drawing.Size(240, 29);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Transparent = System.Drawing.Color.White;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // lLocncode
            // 
            this.lLocncode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lLocncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lLocncode.Data = "";
            this.lLocncode.DataBackColor = System.Drawing.SystemColors.Window;
            this.lLocncode.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lLocncode.Dock = System.Windows.Forms.DockStyle.Top;
            this.lLocncode.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lLocncode.Location = new System.Drawing.Point(0, 61);
            this.lLocncode.MultiLine = false;
            this.lLocncode.Name = "lLocncode";
            this.lLocncode.Popis = "Lokace";
            this.lLocncode.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lLocncode.PopisWidth = 100;
            this.lLocncode.ReadOnly = true;
            this.lLocncode.Size = new System.Drawing.Size(240, 23);
            this.lLocncode.TabIndex = 78;
            this.lLocncode.TabStop = false;
            // 
            // lItemdesc
            // 
            this.lItemdesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lItemdesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lItemdesc.Data = "";
            this.lItemdesc.DataBackColor = System.Drawing.SystemColors.Window;
            this.lItemdesc.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lItemdesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lItemdesc.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lItemdesc.Location = new System.Drawing.Point(0, 23);
            this.lItemdesc.MultiLine = false;
            this.lItemdesc.Name = "lItemdesc";
            this.lItemdesc.Popis = "Název";
            this.lItemdesc.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lItemdesc.PopisWidth = 100;
            this.lItemdesc.ReadOnly = true;
            this.lItemdesc.Size = new System.Drawing.Size(240, 38);
            this.lItemdesc.TabIndex = 76;
            this.lItemdesc.TabStop = false;
            // 
            // lItemnmbr
            // 
            this.lItemnmbr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lItemnmbr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lItemnmbr.Data = "";
            this.lItemnmbr.DataBackColor = System.Drawing.SystemColors.Window;
            this.lItemnmbr.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lItemnmbr.Dock = System.Windows.Forms.DockStyle.Top;
            this.lItemnmbr.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lItemnmbr.Location = new System.Drawing.Point(0, 0);
            this.lItemnmbr.MultiLine = false;
            this.lItemnmbr.Name = "lItemnmbr";
            this.lItemnmbr.Popis = "Èíslo položky";
            this.lItemnmbr.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lItemnmbr.PopisWidth = 100;
            this.lItemnmbr.ReadOnly = true;
            this.lItemnmbr.Size = new System.Drawing.Size(240, 23);
            this.lItemnmbr.TabIndex = 77;
            this.lItemnmbr.TabStop = false;
            // 
            // lMnozstvi
            // 
            this.lMnozstvi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lMnozstvi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lMnozstvi.Data = "";
            this.lMnozstvi.DataBackColor = System.Drawing.SystemColors.Window;
            this.lMnozstvi.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lMnozstvi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lMnozstvi.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lMnozstvi.Location = new System.Drawing.Point(0, 84);
            this.lMnozstvi.MultiLine = false;
            this.lMnozstvi.Name = "lMnozstvi";
            this.lMnozstvi.Popis = "Množství";
            this.lMnozstvi.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lMnozstvi.PopisWidth = 100;
            this.lMnozstvi.ReadOnly = true;
            this.lMnozstvi.Size = new System.Drawing.Size(240, 23);
            this.lMnozstvi.TabIndex = 79;
            this.lMnozstvi.TabStop = false;
            // 
            // OnLinePocetKusuSklad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 284);
            this.ControlBox = false;
            this.Controls.Add(this.lMnozstvi);
            this.Controls.Add(this.lLocncode);
            this.Controls.Add(this.lItemdesc);
            this.Controls.Add(this.lItemnmbr);
            this.Controls.Add(this.buttonOK);
            this.KeyPreview = true;
            this.Name = "OnLinePocetKusuSklad";
            this.Text = "Poèet kusù";
            this.Load += new System.EventHandler(this.OnLinePocetKusuSklad_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLinePocetKusuSklad_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonOK;
        private Fask.Graphic.DataField lLocncode;
        private Fask.Graphic.DataField lItemdesc;
        private Fask.Graphic.DataField lItemnmbr;
        private Fask.Graphic.DataField lMnozstvi;
    }
}