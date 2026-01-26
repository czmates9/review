namespace Fask.MST_W.Inventura2
{
    partial class ListInventury
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
            this.hlavickyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgI1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.hlavickyBindingSource.DataSource = typeof(Fask.MST_W.Inventura2Service.Inventury2);
            // 
            // dgI1
            // 
            this.dgI1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgI1.DataSource = this.hlavickyBindingSource;
            this.dgI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgI1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular);
            this.dgI1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dgI1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dgI1.Location = new System.Drawing.Point(0, 0);
            this.dgI1.Name = "dgI1";
            this.dgI1.Size = new System.Drawing.Size(247, 280);
            this.dgI1.TabIndex = 1;
            this.dgI1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.MappingName = "Hlavicky";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "»Ìslo inventury";
            this.dataGridTextBoxColumn1.MappingName = "ID_INV";
            this.dataGridTextBoxColumn1.NullText = "-";
            this.dataGridTextBoxColumn1.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.graphicButtonStorno);
            this.panel1.Controls.Add(this.graphicButtonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 280);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 38);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // graphicButtonStorno
            // 
            this.graphicButtonStorno.BitmapNormal = null;
            this.graphicButtonStorno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicButtonStorno.FocusMargin = 5;
            this.graphicButtonStorno.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.graphicButtonStorno.Location = new System.Drawing.Point(0, 0);
            this.graphicButtonStorno.Name = "graphicButtonStorno";
            this.graphicButtonStorno.Pressed = false;
            this.graphicButtonStorno.Size = new System.Drawing.Size(119, 38);
            this.graphicButtonStorno.TabIndex = 1;
            this.graphicButtonStorno.Text = "Storno";
            this.graphicButtonStorno.Transparent = System.Drawing.Color.White;
            this.graphicButtonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // graphicButtonOK
            // 
            this.graphicButtonOK.BitmapNormal = null;
            this.graphicButtonOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.graphicButtonOK.FocusMargin = 5;
            this.graphicButtonOK.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.graphicButtonOK.Location = new System.Drawing.Point(119, 0);
            this.graphicButtonOK.Name = "graphicButtonOK";
            this.graphicButtonOK.Pressed = false;
            this.graphicButtonOK.Size = new System.Drawing.Size(128, 38);
            this.graphicButtonOK.TabIndex = 0;
            this.graphicButtonOK.Text = "OK";
            this.graphicButtonOK.Transparent = System.Drawing.Color.White;
            this.graphicButtonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ListInventury
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(247, 318);
            this.ControlBox = false;
            this.Controls.Add(this.dgI1);
            this.Controls.Add(this.panel1);
            this.Name = "ListInventury";
            this.Text = "Vyberte inventuru";
            this.Load += new System.EventHandler(this.ListInventury_Load);
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
    }
}