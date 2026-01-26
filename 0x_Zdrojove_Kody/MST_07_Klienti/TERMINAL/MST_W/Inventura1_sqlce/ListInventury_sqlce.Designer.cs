namespace Fask.MST_W.Inventura1_sqlce
{
    partial class ListInventury_sqlce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListInventury_sqlce));
            this.hlavickyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgI1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.graphicButtonStorno = new Fask.Graphic.GraphicButton();
            this.graphicButtonOK = new Fask.Graphic.GraphicButton();
            ((System.ComponentModel.ISupportInitialize)(this.hlavickyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgI1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hlavickyBindingSource
            // 
            this.hlavickyBindingSource.AllowNew = false;
            this.hlavickyBindingSource.DataMember = "Hlavicky";
            this.hlavickyBindingSource.DataSource = typeof(Fask.MST_W.Inventura1Service.Inventury1);
            this.hlavickyBindingSource.Sort = "";
            // 
            // dgI1
            // 
            this.dgI1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dgI1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgI1.CurrentRow = null;
            this.dgI1.DataSource = this.hlavickyBindingSource;
            resources.ApplyResources(this.dgI1, "dgI1");
            this.dgI1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dgI1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dgI1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dgI1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dgI1.MultiSelect = false;
            this.dgI1.Name = "dgI1";
            this.dgI1.NumberFormat = "N";
            this.dgI1.RowHeightDefault = 23;
            this.dgI1.Sort = "";
            this.dgI1.SortByHeaderDoubleClick = true;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.MappingName = "Hlavicky";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn1, "dataGridTextBoxColumn1");
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn2, "dataGridTextBoxColumn2");
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn3, "dataGridTextBoxColumn3");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.graphicButtonStorno);
            this.panel1.Controls.Add(this.graphicButtonOK);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // graphicButtonStorno
            // 
            this.graphicButtonStorno.BitmapNormal = null;
            resources.ApplyResources(this.graphicButtonStorno, "graphicButtonStorno");
            this.graphicButtonStorno.FocusMargin = 5;
            this.graphicButtonStorno.Name = "graphicButtonStorno";
            this.graphicButtonStorno.Pressed = false;
            this.graphicButtonStorno.Transparent = System.Drawing.Color.White;
            this.graphicButtonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // graphicButtonOK
            // 
            this.graphicButtonOK.BitmapNormal = null;
            resources.ApplyResources(this.graphicButtonOK, "graphicButtonOK");
            this.graphicButtonOK.FocusMargin = 5;
            this.graphicButtonOK.Name = "graphicButtonOK";
            this.graphicButtonOK.Pressed = false;
            this.graphicButtonOK.Transparent = System.Drawing.Color.White;
            this.graphicButtonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ListInventury_sqlce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dgI1);
            this.Controls.Add(this.panel1);
            this.Name = "ListInventury_sqlce";
            this.Load += new System.EventHandler(this.ListInventury_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ListInventury_sqlce_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListInventury_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.hlavickyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgI1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dgI1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.BindingSource hlavickyBindingSource;
        private System.Windows.Forms.Panel panel1;
        private Fask.Graphic.GraphicButton graphicButtonStorno;
        private Fask.Graphic.GraphicButton graphicButtonOK;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
    }
}