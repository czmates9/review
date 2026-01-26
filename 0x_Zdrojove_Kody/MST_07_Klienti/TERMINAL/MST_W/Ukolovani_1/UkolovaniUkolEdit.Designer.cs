namespace Fask.MST_W.Ukolovani_1
{
    partial class UkolovaniUkolEdit
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.bNotifyEnter = new System.Windows.Forms.Button();
            this.bNotifyDelete = new System.Windows.Forms.Button();
            this.bNotifyHourPlus = new System.Windows.Forms.Button();
            this.bNotifyHourMinus = new System.Windows.Forms.Button();
            this.dfDateNotify = new Fask.Graphic.DataField();
            this.dfDateTo = new Fask.Graphic.DataField();
            this.dfDateFrom = new Fask.Graphic.DataField();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbStav = new System.Windows.Forms.ComboBox();
            this.dfDateCreated = new Fask.Graphic.DataField();
            this.dfNote = new Fask.Graphic.DataField();
            this.dfDescription = new Fask.Graphic.DataField();
            this.dfName = new Fask.Graphic.DataField();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.graphicButton1 = new Fask.Graphic.GraphicButton();
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.ukoly = new Fask.SQLiteDBs.DataSets.Ukoly();
            this.ukolyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelMain.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ukoly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ukolyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.bNotifyEnter);
            this.panelMain.Controls.Add(this.bNotifyDelete);
            this.panelMain.Controls.Add(this.bNotifyHourPlus);
            this.panelMain.Controls.Add(this.bNotifyHourMinus);
            this.panelMain.Controls.Add(this.dfDateNotify);
            this.panelMain.Controls.Add(this.dfDateTo);
            this.panelMain.Controls.Add(this.dfDateFrom);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.cmbStav);
            this.panelMain.Controls.Add(this.dfDateCreated);
            this.panelMain.Controls.Add(this.dfNote);
            this.panelMain.Controls.Add(this.dfDescription);
            this.panelMain.Controls.Add(this.dfName);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(298, 231);
            // 
            // bNotifyEnter
            // 
            this.bNotifyEnter.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Italic);
            this.bNotifyEnter.Location = new System.Drawing.Point(178, 180);
            this.bNotifyEnter.Name = "bNotifyEnter";
            this.bNotifyEnter.Size = new System.Drawing.Size(41, 20);
            this.bNotifyEnter.TabIndex = 13;
            this.bNotifyEnter.Text = "Zadat";
            this.bNotifyEnter.Click += new System.EventHandler(this.bNotifyEnter_Click);
            // 
            // bNotifyDelete
            // 
            this.bNotifyDelete.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Italic);
            this.bNotifyDelete.Location = new System.Drawing.Point(142, 180);
            this.bNotifyDelete.Name = "bNotifyDelete";
            this.bNotifyDelete.Size = new System.Drawing.Size(33, 20);
            this.bNotifyDelete.TabIndex = 12;
            this.bNotifyDelete.Text = "X";
            this.bNotifyDelete.Click += new System.EventHandler(this.bNotifyDelete_Click);
            // 
            // bNotifyHourPlus
            // 
            this.bNotifyHourPlus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Italic);
            this.bNotifyHourPlus.Location = new System.Drawing.Point(107, 180);
            this.bNotifyHourPlus.Name = "bNotifyHourPlus";
            this.bNotifyHourPlus.Size = new System.Drawing.Size(33, 20);
            this.bNotifyHourPlus.TabIndex = 11;
            this.bNotifyHourPlus.Text = "+1H";
            this.bNotifyHourPlus.Click += new System.EventHandler(this.bNotifyHourPlus_Click);
            // 
            // bNotifyHourMinus
            // 
            this.bNotifyHourMinus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Italic);
            this.bNotifyHourMinus.Location = new System.Drawing.Point(72, 180);
            this.bNotifyHourMinus.Name = "bNotifyHourMinus";
            this.bNotifyHourMinus.Size = new System.Drawing.Size(33, 20);
            this.bNotifyHourMinus.TabIndex = 10;
            this.bNotifyHourMinus.Text = "-1H";
            this.bNotifyHourMinus.Click += new System.EventHandler(this.bNotifyHourMinus_Click);
            // 
            // dfDateNotify
            // 
            this.dfDateNotify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dfDateNotify.Data = "";
            this.dfDateNotify.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfDateNotify.DataMaxLength = 32767;
            this.dfDateNotify.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfDateNotify.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfDateNotify.Location = new System.Drawing.Point(3, 159);
            this.dfDateNotify.MultiLine = false;
            this.dfDateNotify.Name = "dfDateNotify";
            this.dfDateNotify.Popis = "Připomenutí";
            this.dfDateNotify.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfDateNotify.PopisWidth = 70;
            this.dfDateNotify.ReadOnly = true;
            this.dfDateNotify.Size = new System.Drawing.Size(292, 20);
            this.dfDateNotify.TabIndex = 8;
            // 
            // dfDateTo
            // 
            this.dfDateTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dfDateTo.Data = "";
            this.dfDateTo.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfDateTo.DataMaxLength = 32767;
            this.dfDateTo.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfDateTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfDateTo.Location = new System.Drawing.Point(3, 138);
            this.dfDateTo.MultiLine = false;
            this.dfDateTo.Name = "dfDateTo";
            this.dfDateTo.Popis = "Dokončit";
            this.dfDateTo.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfDateTo.PopisWidth = 70;
            this.dfDateTo.ReadOnly = true;
            this.dfDateTo.Size = new System.Drawing.Size(292, 20);
            this.dfDateTo.TabIndex = 8;
            // 
            // dfDateFrom
            // 
            this.dfDateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dfDateFrom.Data = "";
            this.dfDateFrom.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfDateFrom.DataMaxLength = 32767;
            this.dfDateFrom.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfDateFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfDateFrom.Location = new System.Drawing.Point(3, 117);
            this.dfDateFrom.MultiLine = false;
            this.dfDateFrom.Name = "dfDateFrom";
            this.dfDateFrom.Popis = "Začít";
            this.dfDateFrom.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfDateFrom.PopisWidth = 70;
            this.dfDateFrom.ReadOnly = true;
            this.dfDateFrom.Size = new System.Drawing.Size(292, 20);
            this.dfDateFrom.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(6, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.Text = "Stav";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbStav
            // 
            this.cmbStav.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStav.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.cmbStav.Location = new System.Drawing.Point(72, 202);
            this.cmbStav.Name = "cmbStav";
            this.cmbStav.Size = new System.Drawing.Size(223, 23);
            this.cmbStav.TabIndex = 3;
            // 
            // dfDateCreated
            // 
            this.dfDateCreated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dfDateCreated.Data = "";
            this.dfDateCreated.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfDateCreated.DataMaxLength = 32767;
            this.dfDateCreated.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfDateCreated.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfDateCreated.Location = new System.Drawing.Point(3, 96);
            this.dfDateCreated.MultiLine = false;
            this.dfDateCreated.Name = "dfDateCreated";
            this.dfDateCreated.Popis = "Vytvořeno";
            this.dfDateCreated.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfDateCreated.PopisWidth = 70;
            this.dfDateCreated.ReadOnly = true;
            this.dfDateCreated.Size = new System.Drawing.Size(292, 20);
            this.dfDateCreated.TabIndex = 4;
            // 
            // dfNote
            // 
            this.dfNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dfNote.Data = "";
            this.dfNote.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfNote.DataMaxLength = 200;
            this.dfNote.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfNote.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfNote.Location = new System.Drawing.Point(3, 76);
            this.dfNote.MultiLine = false;
            this.dfNote.Name = "dfNote";
            this.dfNote.Popis = "Poznámka";
            this.dfNote.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfNote.PopisWidth = 70;
            this.dfNote.ReadOnly = false;
            this.dfNote.Size = new System.Drawing.Size(292, 20);
            this.dfNote.TabIndex = 5;
            // 
            // dfDescription
            // 
            this.dfDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dfDescription.Data = "";
            this.dfDescription.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfDescription.DataMaxLength = 32767;
            this.dfDescription.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfDescription.Location = new System.Drawing.Point(3, 24);
            this.dfDescription.MultiLine = true;
            this.dfDescription.Name = "dfDescription";
            this.dfDescription.Popis = "Popis";
            this.dfDescription.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfDescription.PopisWidth = 70;
            this.dfDescription.ReadOnly = true;
            this.dfDescription.Size = new System.Drawing.Size(292, 51);
            this.dfDescription.TabIndex = 6;
            // 
            // dfName
            // 
            this.dfName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dfName.Data = "";
            this.dfName.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfName.DataMaxLength = 32767;
            this.dfName.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfName.Location = new System.Drawing.Point(3, 3);
            this.dfName.MultiLine = false;
            this.dfName.Name = "dfName";
            this.dfName.Popis = "Název";
            this.dfName.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfName.PopisWidth = 70;
            this.dfName.ReadOnly = true;
            this.dfName.Size = new System.Drawing.Size(292, 20);
            this.dfName.TabIndex = 7;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.graphicButton1);
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 231);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(298, 44);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // graphicButton1
            // 
            this.graphicButton1.BitmapNormal = null;
            this.graphicButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicButton1.FocusMargin = 2;
            this.graphicButton1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.graphicButton1.Location = new System.Drawing.Point(0, 0);
            this.graphicButton1.Name = "graphicButton1";
            this.graphicButton1.Pressed = false;
            this.graphicButton1.Size = new System.Drawing.Size(134, 44);
            this.graphicButton1.TabIndex = 1;
            this.graphicButton1.Text = "Storno";
            this.graphicButton1.Transparent = System.Drawing.Color.White;
            this.graphicButton1.Click += new System.EventHandler(this.graphicButton1_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.BitmapNormal = null;
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOK.FocusMargin = 2;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(134, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Pressed = false;
            this.buttonOK.Size = new System.Drawing.Size(164, 44);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.Transparent = System.Drawing.Color.White;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ukoly
            // 
            this.ukoly.DataSetName = "Ukoly";
            this.ukoly.Locale = new System.Globalization.CultureInfo("");
            this.ukoly.Prefix = "";
            this.ukoly.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ukolyBindingSource
            // 
            this.ukolyBindingSource.DataMember = "Ukoly";
            this.ukolyBindingSource.DataSource = this.ukoly;
            this.ukolyBindingSource.Sort = "";
            // 
            // UkolovaniUkolEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(298, 275);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.KeyPreview = true;
            this.Name = "UkolovaniUkolEdit";
            this.Text = "Úkol";
            this.Deactivate += new System.EventHandler(this.UkolovaniUkolEdit_Deactivate);
            this.Load += new System.EventHandler(this.UkolovaniMain_Load);
            this.Activated += new System.EventHandler(this.UkolovaniUkolEdit_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UkolovaniMain_KeyDown);
            this.panelMain.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ukoly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ukolyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private Fask.SQLiteDBs.DataSets.Ukoly ukoly;
        private Fask.Graphic.DataField dfNote;
        private Fask.Graphic.DataField dfDescription;
        private Fask.Graphic.DataField dfName;
        private System.Windows.Forms.BindingSource ukolyBindingSource;
        private System.Windows.Forms.ComboBox cmbStav;
        private Fask.Graphic.DataField dfDateCreated;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton buttonOK;
        private Fask.Graphic.GraphicButton graphicButton1;
        private Fask.Graphic.DataField dfDateFrom;
        private Fask.Graphic.DataField dfDateNotify;
        private Fask.Graphic.DataField dfDateTo;
        private System.Windows.Forms.Button bNotifyDelete;
        private System.Windows.Forms.Button bNotifyHourPlus;
        private System.Windows.Forms.Button bNotifyHourMinus;
        private System.Windows.Forms.Button bNotifyEnter;
    }
}