namespace Fask.MST_W.Vydej_3
{
    partial class SejmiKodHromadneInfoForm
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
            this.panelData = new System.Windows.Forms.Panel();
            this.dataGrid21 = new Fask.Graphic.DataGrid2();
            this.nacteno_l = new Fask.Graphic.DataField();
            this.nacist_l = new Fask.Graphic.DataField();
            this.ItemDesc_l = new Fask.Graphic.DataField();
            this.panelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid21)).BeginInit();
            this.SuspendLayout();
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.dataGrid21);
            this.panelData.Controls.Add(this.nacteno_l);
            this.panelData.Controls.Add(this.nacist_l);
            this.panelData.Controls.Add(this.ItemDesc_l);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(259, 254);
            // 
            // dataGrid21
            // 
            this.dataGrid21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid21.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid21.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid21.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid21.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid21.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid21.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid21.Location = new System.Drawing.Point(3, 18);
            this.dataGrid21.MultiSelect = false;
            this.dataGrid21.Name = "dataGrid21";
            this.dataGrid21.NumberFormat = "N";
            this.dataGrid21.RowHeightDefault = 23;
            this.dataGrid21.Size = new System.Drawing.Size(253, 213);
            this.dataGrid21.Sort = "";
            this.dataGrid21.SortByHeaderDoubleClick = false;
            this.dataGrid21.TabIndex = 15;
            this.dataGrid21.CurrentRowIndexChanged += new System.EventHandler(this.dataGrid21_CurrentRowIndexChanged);
            this.dataGrid21.CurrentCellChanged += new System.EventHandler(this.dataGrid21_CurrentCellChanged);
            // 
            // nacteno_l
            // 
            this.nacteno_l.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nacteno_l.Data = "nacteno";
            this.nacteno_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.nacteno_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.nacteno_l.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.nacteno_l.Location = new System.Drawing.Point(129, 234);
            this.nacteno_l.MultiLine = true;
            this.nacteno_l.Name = "nacteno_l";
            this.nacteno_l.Popis = "Naèteno :";
            this.nacteno_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.nacteno_l.PopisWidth = 71;
            this.nacteno_l.ReadOnly = true;
            this.nacteno_l.Size = new System.Drawing.Size(128, 18);
            this.nacteno_l.TabIndex = 14;
            // 
            // nacist_l
            // 
            this.nacist_l.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nacist_l.Data = "nacist";
            this.nacist_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.nacist_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.nacist_l.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.nacist_l.Location = new System.Drawing.Point(1, 234);
            this.nacist_l.MultiLine = true;
            this.nacist_l.Name = "nacist_l";
            this.nacist_l.Popis = "Naèíst :";
            this.nacist_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.nacist_l.PopisWidth = 57;
            this.nacist_l.ReadOnly = true;
            this.nacist_l.Size = new System.Drawing.Size(127, 18);
            this.nacist_l.TabIndex = 13;
            // 
            // ItemDesc_l
            // 
            this.ItemDesc_l.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemDesc_l.Data = "itemdesc";
            this.ItemDesc_l.DataBackColor = System.Drawing.SystemColors.Window;
            this.ItemDesc_l.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ItemDesc_l.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.ItemDesc_l.Location = new System.Drawing.Point(2, 1);
            this.ItemDesc_l.MultiLine = true;
            this.ItemDesc_l.Name = "ItemDesc_l";
            this.ItemDesc_l.Popis = "Popis :";
            this.ItemDesc_l.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ItemDesc_l.PopisWidth = 60;
            this.ItemDesc_l.ReadOnly = true;
            this.ItemDesc_l.Size = new System.Drawing.Size(256, 16);
            this.ItemDesc_l.TabIndex = 12;
            // 
            // SejmiKodHromadneInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(259, 254);
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Name = "SejmiKodHromadneInfoForm";
            this.panelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelData;
        private Fask.Graphic.DataField ItemDesc_l;
        private Fask.Graphic.DataField nacist_l;
        private Fask.Graphic.DataField nacteno_l;
        private Fask.Graphic.DataGrid2 dataGrid21;
    }
}
