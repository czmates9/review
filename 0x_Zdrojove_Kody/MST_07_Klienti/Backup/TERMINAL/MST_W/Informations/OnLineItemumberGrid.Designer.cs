namespace Fask.MST_W.Informations
{
    partial class OnLineItemumberGrid
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
            this.detailPanel = new System.Windows.Forms.Panel();
            this.listGrid = new Fask.Graphic.DataGrid2();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuListDetail = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.listGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonOK.Location = new System.Drawing.Point(0, 317);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(485, 29);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // detailPanel
            // 
            this.detailPanel.AutoScroll = true;
            this.detailPanel.Location = new System.Drawing.Point(232, 32);
            this.detailPanel.Name = "detailPanel";
            this.detailPanel.Size = new System.Drawing.Size(250, 279);
            // 
            // listGrid
            // 
            this.listGrid.BackColorAlternating = System.Drawing.Color.Gold;
            this.listGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.listGrid.CurrentRow = null;
            this.listGrid.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.listGrid.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.listGrid.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.listGrid.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.listGrid.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.listGrid.Location = new System.Drawing.Point(3, 32);
            this.listGrid.MultiSelect = false;
            this.listGrid.Name = "listGrid";
            this.listGrid.NumberFormat = "N";
            this.listGrid.RowHeightDefault = 23;
            this.listGrid.Size = new System.Drawing.Size(223, 279);
            this.listGrid.Sort = "";
            this.listGrid.SortByHeaderDoubleClick = true;
            this.listGrid.TabIndex = 1;
            this.listGrid.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuListDetail);
            // 
            // menuListDetail
            // 
            this.menuListDetail.Text = "List / Detail";
            this.menuListDetail.Click += new System.EventHandler(this.menuListDetail_Click);
            // 
            // OnLineItemumberGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(485, 346);
            this.ControlBox = false;
            this.Controls.Add(this.listGrid);
            this.Controls.Add(this.detailPanel);
            this.Controls.Add(this.buttonOK);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "OnLineItemumberGrid";
            this.Text = "Položka informace";
            this.Load += new System.EventHandler(this.OnLineItemumberGrid_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnLineItemumberGrid_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLineItemumberGrid_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.listGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonOK;
        private System.Windows.Forms.Panel detailPanel;
        private Fask.Graphic.DataGrid2 listGrid;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuListDetail;
    }
}